using UnityEngine;
using Invector.vCharacterController;
using QInventory;
public class GlobalInputLock : MonoBehaviour
{
    [SerializeField]
    vThirdPersonInput playerinput;
    [SerializeField]
    InventoryInputControl control;
    bool session;

    private void Update()
    {
        
        if(session)
        {
            session = true;
            playerinput.SetLockAllInput(true);
          //  playerinput.SetLockCameraInput(true);
          //  playerinput.SetLockBasicInput(true);
           // playerinput.SetLockCameraInput(true);
            control.DisableInputs();

        }
        else
        {
            playerinput.SetLockAllInput(false);
          //  playerinput.SetLockCameraInput(false);
          //  playerinput.SetLockBasicInput(false);
          //  playerinput.SetLockCameraInput(false);
            control.EnableInput();
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
