using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public class AchievementEvent : Achievement
    {
        [SerializeField, AchievementsGlossaryDropDown] private string m_achievementDataName = default;


        #region GETTERS

        public string AchievementDataName { get => m_achievementDataName; }

        #endregion


        public override bool CheckAchievement(AchievementsData achievementsData)
        {
            return achievementsData.GetAchievementDataValue<bool>(m_achievementDataName);
        }
    }
}
