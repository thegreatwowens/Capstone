using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    [Serializable]
    public class AchievementsData
    {
        [SerializeField] private Dictionary<string, bool> m_achievementsDone = new Dictionary<string, bool>();
        [SerializeField] private Dictionary<string, bool> m_achievementsDataBool = new Dictionary<string, bool>();
        [SerializeField] private Dictionary<string, int> m_achievementsDataInt = new Dictionary<string, int>();


        #region GETTERS

        public Dictionary<string, bool> AchievementsDone { get => m_achievementsDone; }
        public Dictionary<string, bool> AchievementsDataBool { get => m_achievementsDataBool; }
        public Dictionary<string, int> AchievementsDataInt { get => m_achievementsDataInt; }

        #endregion


        public void InitializeAchievementsData(AchievementsList achievements)
        {
            //Initialize (Reset) achievements done 
            m_achievementsDone.Clear();
            for (int index = 0; index < achievements.achievements.Count; index++)
                m_achievementsDone.Add(achievements.achievements[index].NameID, false);

            //Reset achievements data bool
            KeyValuePair<string, bool> achievementDataBool;
            for (int index = 0; index < m_achievementsDataBool.Count; index++)
            {
                achievementDataBool = m_achievementsDataBool.ElementAt(index);
                m_achievementsDataBool[achievementDataBool.Key] = default;
            }

            //Reset achievements data int
            KeyValuePair<string, int> achievementDataInt;
            for (int index = 0; index < m_achievementsDataInt.Count; index++)
            {
                achievementDataInt = m_achievementsDataInt.ElementAt(index);
                m_achievementsDataInt[achievementDataInt.Key] = default;
            }
        }

        public T GetAchievementDataValue<T>(string achievementDataName)
        {
            if (typeof(T) == typeof(bool))
                return (T)(object)m_achievementsDataBool[achievementDataName];
            else if (typeof(T) == typeof(int))
                return (T)(object)m_achievementsDataInt[achievementDataName];
            else
                return default;
        }

        public void UpdateAchievementData(string achievementDataName, bool value) { m_achievementsDataBool[achievementDataName] = value; }
        public void UpdateAchievementData(string achievementDataName, int value) { m_achievementsDataInt[achievementDataName] += value; }

        public bool IsAchievementDone(string nameID) { return m_achievementsDone[nameID]; }
        public void SetAchievementDone(string nameID) { m_achievementsDone[nameID] = true; }

        public bool AreAllAchievementsDoneExceptCompletionist(string completionistNameID)
        {
            KeyValuePair<string, bool> achievementDone;
            for (int index = 0; index < m_achievementsDone.Count; index++)
            {
                achievementDone = m_achievementsDone.ElementAt(index);
                if (achievementDone.Key != completionistNameID && achievementDone.Value == false)
                    return false;
            }
            return true;
        }
    }
}