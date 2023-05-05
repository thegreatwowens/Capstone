using System.Collections.Generic;
using UnityEngine;
using OwnCode;
using PixelCrushers.DialogueSystem;
namespace QInventory
{
    public class InventoryInputControl : MonoBehaviour
    {
        GameObject Player;
        PlayMakerFSM fsm;
        bool session;
        [SerializeField]
        List<InventoryUIControl> inventoryUIControls = new List<InventoryUIControl>();
        InventoryControlExtension extension;
        private void Start()
        {
            extension = GetComponent<InventoryControlExtension>();        }
        private void Update()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            fsm = PlayMakerFSM.FindFsmOnGameObject(Player, "Mouse Settings Default");
           

            for (int i = 0; i < inventoryUIControls.Count; i++)
            {
                
                if (!session) {
                    if (Input.GetKeyDown(inventoryUIControls[i].m_KeyCode))
                    {
                        if (inventoryUIControls[i].m_InventoryUI != null)
                        {
                            Q_GameMaster.Instance.inventoryManager.PlayOpenPanelClip();
                            Q_GameMaster.Instance.inventoryManager.toolTip.tooltip.SetActive(false);
                            fsm.SendEvent("UINavigation");
                            inventoryUIControls[i].m_InventoryUI.SetActive(!inventoryUIControls[i].m_InventoryUI.activeSelf);
                        }
                     
                    }
                }
                else
                {
                    extension.DisableInputs();
                }
                
             

            }
        }
        public void DisableInputs()
        {
           
            session = true;
        }
        public void EnableInput() {
           
            session = false;
        }
        
    }
}

