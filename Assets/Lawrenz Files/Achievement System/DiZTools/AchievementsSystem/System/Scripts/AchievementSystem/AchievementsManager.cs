using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public class AchievementsManager : SingletonDontDestroyOnLoad<AchievementsManager>
    {
        [Header("Display Achievement")]
        [SerializeField] private AchievementUI m_achievementUI = null;
        [SerializeField] private Animator m_achievementAnimator = null;

        [Header("Data")]
        [SerializeField] private AchievementsDataSO m_achievementsDataSO = null;
        [SerializeField] private AchievementsList m_achievements = null;

        [Header("Saving")]
        [SerializeField, Range(10f, 600f)] private float m_timeBetweenSave = 60f;

        //Animation stuff
        private readonly string DISPLAY_ACHIEVEMENT_TRIGGER_NAME = "DisplayAchievement";
        private readonly float ACHIEVEMENT_DISPLAY_WAIT_BETWEEN_TIME = 0f;

        //Saving cache
        private string m_achievementFolderPath;
        private string m_achievementSavePath;
        private float m_timeSinceLastSave = 0f;

        //Displaying cache
        private List<Achievement> m_achievementDisplayQueue = new List<Achievement>();
        private bool m_isDisplayingAnAchievement = false;

        //cache
        private int m_achievementCheckIndex = 0;

#if UNITY_EDITOR
        //cache for editor
        private float m_timeSinceLastDataRefresh = 0f;
#endif


        #region PROPERTIES

        public AchievementsData AchievementsData { get; private set; }

        #endregion


        private void Start()
        {
            m_achievementFolderPath = Path.Combine(Application.persistentDataPath, PathUtils.achievementFolderName);
            m_achievementSavePath = Path.Combine(m_achievementFolderPath, PathUtils.achievementFileName + PathUtils.saveExtension);

            SetupAchievementsData();

            //Initialize Achievements Data for the first time (if it is the first time)
            if (!File.Exists(m_achievementSavePath))
                ResetAchievementsData();

            LoadAchievementsData();

            m_initialized = true;
        }

        private void Update()
        {
            CheckAchievements();

            if (m_achievementDisplayQueue.Count != 0 && !m_isDisplayingAnAchievement)
                StartCoroutine(DisplayAchievementPopup());

            m_timeSinceLastSave += Time.deltaTime;
            if (m_timeSinceLastSave >= m_timeBetweenSave)
            {
                SaveAchievementsData();
            }

#if UNITY_EDITOR
            m_timeSinceLastDataRefresh += Time.deltaTime;
            if (m_timeSinceLastDataRefresh >= 1f)
                m_achievementsDataSO.RefreshAchievementsData(AchievementsData);
#endif
        }

        public void CheckAchievements()
        {
            if (!AchievementsData.IsAchievementDone(m_achievements.achievements[m_achievementCheckIndex].NameID))
            {
                bool checkAchievement = m_achievements.achievements[m_achievementCheckIndex].CheckAchievement(AchievementsData);
                if (checkAchievement)
                    ValidateAchievement(m_achievements.achievements[m_achievementCheckIndex]);
            }

            m_achievementCheckIndex = (m_achievementCheckIndex + 1) % m_achievements.achievements.Count;
        }

        public void ValidateAchievement(Achievement achievement)
        {
            //Add the selected achievement in the display queue
            m_achievementDisplayQueue.Add(achievement);

            //Validate the selected achievement
            AchievementsData.SetAchievementDone(achievement.NameID);

            //Save achievementsData
            SaveAchievementsData();
        }

        private IEnumerator DisplayAchievementPopup()
        {
            m_isDisplayingAnAchievement = true;

            Achievement achievement = m_achievementDisplayQueue[0];

            m_achievementUI.NameID.text = achievement.NameID;
            m_achievementUI.Description.text = achievement.Description;
            m_achievementUI.Thumbnail.sprite = achievement.Thumbnail;

            m_achievementAnimator.SetTrigger(DISPLAY_ACHIEVEMENT_TRIGGER_NAME);

            yield return null;

            float waitTime = m_achievementAnimator.GetCurrentAnimatorStateInfo(0).length;

            yield return new WaitForSecondsRealtime(waitTime + ACHIEVEMENT_DISPLAY_WAIT_BETWEEN_TIME);

            m_achievementDisplayQueue.RemoveAt(0);

            m_isDisplayingAnAchievement = false;
        }

        private void OnDestroy()
        {
            if (!m_initialized)
                return;

            SaveAchievementsData();
        }

        #region SAVES & LOADS ACHIEVEMENTS DATA

        private void SetupAchievementsData()
        {
            AchievementsData = new AchievementsData();
            m_achievementsDataSO.CopyTo(AchievementsData);
        }

        private void LoadAchievementsData()
        {
            if (!File.Exists(m_achievementSavePath))
                return;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(m_achievementSavePath, FileMode.Open);
            AchievementsData = (AchievementsData)bf.Deserialize(file);
            file.Close();

            //JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto };
            //string achievementsDataJSON = File.ReadAllText(m_achievementSavePath);
            //AchievementsData = JsonConvert.DeserializeObject<AchievementsData>(achievementsDataJSON, jsonSerializerSettings);
        }

        public void SaveAchievementsData()
        {
            if (!Directory.Exists(m_achievementFolderPath))
                Directory.CreateDirectory(m_achievementFolderPath);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(m_achievementSavePath);
            bf.Serialize(file, AchievementsData);
            file.Close();

            //JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto };
            //string achievementsDataJSON = JsonConvert.SerializeObject(AchievementsData, Formatting.Indented, jsonSerializerSettings);
            //File.WriteAllText(m_achievementSavePath, achievementsDataJSON);

            m_timeSinceLastSave = 0f;
        }

        public void ResetAchievementsData()
        {
            AchievementsData.InitializeAchievementsData(m_achievements);
            SaveAchievementsData();
        }

        #endregion
    }
}
