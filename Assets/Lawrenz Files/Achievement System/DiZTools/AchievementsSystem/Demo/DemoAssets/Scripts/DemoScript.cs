using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public class DemoScript : MonoBehaviour
    {
        private void Update()
        {
            //Trigger the "bool" achievement example
            if (Input.GetKeyDown(KeyCode.B))
            {
               // AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.Bool_Achievement_Example, true);
            }

            //Trigger the "int" achievement example
            if (Input.GetKeyDown(KeyCode.I))
            {
                //AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.Int_Achievement_Example, +1);
            }

            //Reset all achievements data
            if (Input.GetKeyDown(KeyCode.R))
            {
              ///  AchievementsManager.Instance.ResetAchievementsData();
            }
        }
    }
}
