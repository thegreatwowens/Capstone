using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QInventory;

namespace DiZTools_AchievementsSystem {

    public class AttributeRewarder : MonoBehaviour
    {
        InventoryManager inventoryManager;
        AchievementTrigger achievementTrigger;
        PlayerLevelHandler levelHandler;
        GranterXP grantxp;
        private void Update()
        {
            achievementTrigger = GameObject.FindGameObjectWithTag("Player").GetComponent<AchievementTrigger>();
            grantxp = GetComponent<GranterXP>();
        }
        public  void RewardPlayerFromConversion()
        {

            if (InventoryManager.GetPlayerAttributeCurrentValue("Binary Intelegence") != InventoryManager.GetPlayerAttributeMaxValue("Binary Intelegence"))
            { InventoryManager.ChangePlayerAttributeValue("Binary Intelegence", 1, Effect.Restore);
                
                if(grantxp != null) { grantxp.GrantXP(); }
               
                }

            if (InventoryManager.GetPlayerAttributeCurrentValue("Binary Intelegence") == InventoryManager.GetPlayerAttributeMaxValue("Binary Intelegence"))
            {    }
           //     achievementTrigger.BinaryMastery();






        }
        public static void RewardPlayerFromQuiz()
        {
        }

        
    }
}

