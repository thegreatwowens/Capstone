using UnityEngine;
using System.Collections.Generic;
using QInventory;

namespace DiZTools_AchievementsSystem {
    public class AchievementTrigger : MonoBehaviour
    {
        [SerializeField]
        Transform RightHandSlot;
        Transform child;
        [SerializeField]
        List<GameObject> achievementLocked;

        
        private void Update()
        {
            if (RightHandSlot != null)
            {
                if (RightHandSlot.transform.childCount > 0)
                {
                    child = RightHandSlot.transform.GetChild(0);

                    if (child.CompareTag("Weapon"))
                        HammerFirstTimeEquoippedCaller();
                }
            }
            else 
            return;


            if(InventoryManager.GetPlayerAttributeCurrentValue("Binary Intelegence") != 5)
            {
                achievementLocked[4].SetActive(false);
            }

            if(InventoryManager.GetPlayerAttributeCurrentValue("Windows Installation Expert") != 5)
            {
                achievementLocked[5].SetActive(false);
            }
            
        }
        private void HammerFirstTimeEquoippedCaller()
        {
            AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.HammerOfDetroit, true);
            achievementLocked[0].SetActive(false);

        }
        public void EasyQuizCaller()
        {
            AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.EasyQuizComplete,true);
            achievementLocked[1].SetActive(false);
        }
        public void NormalQuizCaller()
        {
            AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.NormalQuizComplete, true);
            achievementLocked[2].SetActive(false);
        }
        public void HardQuizCaller()
        {
            AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.HardQuizComplete, true);
            achievementLocked[3].SetActive(false);

        }
        public void BinaryMastery()
        {
            AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.BinaryMastery, true);
            achievementLocked[4].SetActive(false);
        }

        public void WindowsAchievement()
        {
            AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.WindowsExpert,+5);
            achievementLocked[5].SetActive(false);
        }
        public void NetworkAchievement()
        {
            AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.NetworkingExpert,+5);
            achievementLocked[5].SetActive(false);
        }

    }
}
