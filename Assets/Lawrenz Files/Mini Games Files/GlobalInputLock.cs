
using UnityEngine;
using Invector.vCharacterController;
using QInventory;
using PixelCrushers.DialogueSystem;

public class GlobalInputLock : MonoBehaviour
{
    [SerializeField]
    vThirdPersonInput playerinput;
    [SerializeField]
    InventoryInputControl control;
    bool session;

    private void Update()
    {
        
        if(DialogueManager.Instance.isConversationActive && control.isActiveAndEnabled)
        {
            session = true;
            playerinput.SetLockAllInput(true);
          //  playerinput.SetLockCameraInput(true);
          //  playerinput.SetLockBasicInput(true);
           // playerinput.SetLockCameraInput(true);
            control.DisableInputs();

        }   else{
               control.EnableInput();
                  playerinput.SetLockAllInput(false);
        }

    }
    public void EnableAllMovements()
    {
            session = false;
    
    }
    public void DisableAllPlayerInputMovement() {
        session = true;
    }

}
