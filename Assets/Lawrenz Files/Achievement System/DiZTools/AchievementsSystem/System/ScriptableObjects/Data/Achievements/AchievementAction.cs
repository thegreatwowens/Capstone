using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public class AchievementAction : Achievement
    {
        [SerializeField, AchievementsGlossaryDropDown] private string m_achievementDataName = default;
        [SerializeField] private int m_doneCondition = 1;


        #region GETTERS

        public string AchievementDataName { get => m_achievementDataName; }

        #endregion


        public override bool CheckAchievement(AchievementsData achievementsData)
        {
            return achievementsData.GetAchievementDataValue<int>(m_achievementDataName) >= m_doneCondition;
        }
    }
}
