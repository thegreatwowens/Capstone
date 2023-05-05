using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;
using UnityEngine.Events;
using PixelCrushers.DialogueSystem;
public class WeapoRestriction : MonoBehaviour
{
    [SerializeField]
    DoorNotification door;
    DialogueSystemTrigger trigger;
    vThirdPersonInput input;
    bool checking;
    public UnityEvent CanInteract;
    private void Awake()
    {
        trigger = GetComponent<DialogueSystemTrigger>();
    }
    private void Update()
    {
        input = FindObjectOfType<vThirdPersonInput>();

        if (checking)
        {
            if (input.cc.isStrafing)
            {
                door.EquippedNotification();
                checking = false;
            }
            else
            {
                trigger.OnUse();
                CanInteract.Invoke();
                checking = false;
            }

        }
    }

    public void CheckRestriction()
    {
        checking = true;

    }

}
