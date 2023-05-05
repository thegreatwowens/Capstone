using System.Collections.Generic;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public class AchievementsList : ScriptableObject
    {
        [HideInInspector] public List<Achievement> achievements = new List<Achievement>();
    }
}
