using UnityEngine;
using System.Collections.Generic;
using QInventory;

namespace DiZTools_AchievementsSystem
{
    public class AchievementTrigger : MonoBehaviour
    {
        List<GameObject> achievementLocked;
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {

            if (InventoryManager.GetPlayerAttributeCurrentValue("Binary Intelegence") != 5)
            {
                achievementLocked[4].SetActive(false);
            }

            if (InventoryManager.GetPlayerAttributeCurrentValue("Windows Installation Expert") != 5)
            {
                achievementLocked[5].SetActive(false);
            }

            //AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.HammerOfDetroit, true);
            //achievementLocked[0].SetActive(false);

        }

        public void CityExplorer(){
            AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.CityExplorer,true);
        }
    }
}
