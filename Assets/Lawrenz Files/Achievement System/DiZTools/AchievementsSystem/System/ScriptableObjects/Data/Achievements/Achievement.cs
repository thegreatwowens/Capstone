using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public abstract class Achievement : ScriptableObject
    {
        [SerializeField] protected string m_nameID;
        [SerializeField] protected string m_description;
        [SerializeField] protected Sprite m_thumbnail;


        #region GETTERS / SETTERS

        public string NameID { get => m_nameID; }
        public string Description { get => m_description; }
        public Sprite Thumbnail { get => m_thumbnail; }

        public void PasteValues(string nameID, string description, Sprite thumbnail)
        {
            m_nameID = nameID;
            m_description = description;
            m_thumbnail = thumbnail;
        }

        #endregion


        public abstract bool CheckAchievement(AchievementsData achievementsData);
    }
}