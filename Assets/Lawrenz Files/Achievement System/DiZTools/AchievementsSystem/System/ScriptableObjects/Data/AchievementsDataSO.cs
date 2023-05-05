using System.Collections.Generic;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public class AchievementsDataSO : ScriptableObject
    {
        //Achievements Done
        [SerializeField] private List<StringBoolTuple> m_achievementsDone = new List<StringBoolTuple>();

        //Achievements Data List
        [SerializeField] private List<StringBoolTuple> m_achievementsDataBool = new List<StringBoolTuple>();
        [SerializeField] private List<StringIntTuple> m_achievementsDataInt = new List<StringIntTuple>();


        #region GETTERS

        public List<StringBoolTuple> AchievementsDone { get => m_achievementsDone; }
        public List<StringBoolTuple> AchievementsDataBool { get => m_achievementsDataBool; }
        public List<StringIntTuple> AchievementsDataInt { get => m_achievementsDataInt; }

        #endregion


        public void CopyTo(AchievementsData achievementsDataSIF)
        {
            m_achievementsDone.CopyToDictionary(achievementsDataSIF.AchievementsDone);
            m_achievementsDataBool.CopyToDictionary(achievementsDataSIF.AchievementsDataBool);
            m_achievementsDataInt.CopyToDictionary(achievementsDataSIF.AchievementsDataInt);
        }

        public void RefreshAchievementsData(AchievementsData achievementsDataSIF)
        {
            for (int i = 0; i < AchievementsDone.Count; i++)
            {
                if (achievementsDataSIF.AchievementsDone.ContainsKey(AchievementsDone[i].item1))
                {
                    AchievementsDone[i].item2 = achievementsDataSIF.AchievementsDone[AchievementsDone[i].item1];
                }
            }

            for (int i = 0; i < AchievementsDataBool.Count; i++)
            {
                if (achievementsDataSIF.AchievementsDataBool.ContainsKey(AchievementsDataBool[i].item1))
                {
                    AchievementsDataBool[i].item2 = achievementsDataSIF.AchievementsDataBool[AchievementsDataBool[i].item1];
                }
            }

            for (int i = 0; i < AchievementsDataInt.Count; i++)
            {
                if (achievementsDataSIF.AchievementsDataInt.ContainsKey(AchievementsDataInt[i].item1))
                {
                    AchievementsDataInt[i].item2 = achievementsDataSIF.AchievementsDataInt[AchievementsDataInt[i].item1];
                }
            }
        }
    }
}
