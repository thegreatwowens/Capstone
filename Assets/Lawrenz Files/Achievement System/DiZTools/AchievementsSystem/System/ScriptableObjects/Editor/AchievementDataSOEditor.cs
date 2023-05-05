using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    [CustomEditor(typeof(AchievementsDataSO))]
    public class AchievementDataSOEditor : Editor
    {
        private AchievementsDataSO achievementDataSO;

        private string m_achievementFolderPath;
        private string m_achievementSavePath;

        private void OnEnable()
        {
            achievementDataSO = (AchievementsDataSO)target;

            RefreshAchievementsData();
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;

            EditorGUILayout.Space(10f);

            GUILayout.Label("Achievements Done", EditorStyles.boldLabel);

            for (int i = 0; i < achievementDataSO.AchievementsDone.Count; i++)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.Width(400f));

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400f));
                GUILayout.Label(achievementDataSO.AchievementsDone[i].item1, GUILayout.Width(300f));
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginHorizontal(GUILayout.Width(50f));
                GUILayout.FlexibleSpace();
                GUILayout.Toggle(achievementDataSO.AchievementsDone[i].item2, GUIContent.none);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.Width(400f));

            EditorGUILayout.Space(20f);

            GUILayout.Label("Achievements Data Bool", EditorStyles.boldLabel);

            for (int i = 0; i < achievementDataSO.AchievementsDataBool.Count; i++)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.Width(400f));

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400f));
                GUILayout.Label(achievementDataSO.AchievementsDataBool[i].item1, GUILayout.Width(300f));
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginHorizontal(GUILayout.Width(50f));
                GUILayout.FlexibleSpace();
                GUILayout.Toggle(achievementDataSO.AchievementsDataBool[i].item2, GUIContent.none);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.Width(400f));

            EditorGUILayout.Space(20f);

            GUILayout.Label("Achievements Data Int", EditorStyles.boldLabel);

            for (int i = 0; i < achievementDataSO.AchievementsDataInt.Count; i++)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.Width(400f));

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400f));
                GUILayout.Label(achievementDataSO.AchievementsDataInt[i].item1, GUILayout.Width(300f));
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginHorizontal(GUILayout.Width(50f));
                GUILayout.FlexibleSpace();
                GUILayout.Label(achievementDataSO.AchievementsDataInt[i].item2 + "");
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, GUILayout.Width(400f));
        }

        private void RefreshAchievementsData()
        {
            m_achievementFolderPath = Path.Combine(Application.persistentDataPath, PathUtils.achievementFolderName);
            m_achievementSavePath = Path.Combine(m_achievementFolderPath, PathUtils.achievementFileName + PathUtils.saveExtension);

            if (!File.Exists(m_achievementSavePath))
                return;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(m_achievementSavePath, FileMode.Open);
            AchievementsData achievementsData = (AchievementsData)bf.Deserialize(file);
            file.Close();

            achievementDataSO.RefreshAchievementsData(achievementsData);
        }
    }
}