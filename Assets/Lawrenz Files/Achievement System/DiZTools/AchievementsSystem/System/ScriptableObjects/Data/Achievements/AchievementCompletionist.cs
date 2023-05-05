namespace DiZTools_AchievementsSystem
{
    public class AchievementCompletionist : Achievement
    {
        public override bool CheckAchievement(AchievementsData achievementsData)
        {
            return achievementsData.AreAllAchievementsDoneExceptCompletionist(m_nameID);
        }
    }
}
