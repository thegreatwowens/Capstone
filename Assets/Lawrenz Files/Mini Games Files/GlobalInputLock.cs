
using UnityEngine;
using Invector.vCharacterController;
using QInventory;
using PixelCrushers.DialogueSystem;
using Lovatto.MiniMap;

public class GlobalInputLock : Sequencer
{
    public static GlobalInputLock Instance;
    [SerializeField]
    vThirdPersonInput playerinput;
    [SerializeField]
    InventoryInputControl control;
    bool session;
    void Awake()
    {
        if(Instance == null){
            Instance = this;
            
        }
    }

    private  new void Update()
    {
        
        if(DialogueManager.Instance.isConversationActive || isPlaying || session )
        {
            playerinput.SetLockAllInput(true);
          //  playerinput.SetLockCameraInput(true);
          //  playerinput.SetLockBasicInput(true);
           // playerinput.SetLockCameraInput(true);
            control.DisableInputs();
         
            

        }
           else if (!DialogueManager.Instance.isConversationActive || !isPlaying || !session){
               control.EnableInput();
                  playerinput.SetLockAllInput(false);
        }   else{
              control.EnableInput();
                  playerinput.SetLockAllInput(false);
        }
        

    }
    public void EnableAllMovements()
    {
            session = false;
            MouseSettingsInput.Instance.DisableMouseUI();
            bl_MiniMap.ActiveMiniMap.enabled =true;
    }
    public void DisableAllPlayerInputMovement() {
        session = true;
        MouseSettingsInput.Instance.EnableMouseUI();
        bl_MiniMap.ActiveMiniMap.enabled =false;
    }

}
