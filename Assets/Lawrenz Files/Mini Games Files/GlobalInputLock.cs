
using UnityEngine;
using Invector.vCharacterController;
using QInventory;
using PixelCrushers.DialogueSystem;

public class GlobalInputLock : MonoBehaviour
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

    private void Update()
    {
        
        if(DialogueManager.Instance.isConversationActive && control.isActiveAndEnabled|| session)
        {
            playerinput.SetLockAllInput(true);
          //  playerinput.SetLockCameraInput(true);
          //  playerinput.SetLockBasicInput(true);
           // playerinput.SetLockCameraInput(true);
            control.DisableInputs();
            

        }
           else if (!DialogueManager.Instance.isConversationActive){
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
    }
    public void DisableAllPlayerInputMovement() {
        session = true;
        MouseSettingsInput.Instance.EnableMouseUI();
    }

}
