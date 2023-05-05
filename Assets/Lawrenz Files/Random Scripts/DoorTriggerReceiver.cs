 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
public class DoorTriggerReceiver : MonoBehaviour
{
    [SerializeField]
    ConversionGamePanel panel;
    [SerializeField]
    DoorOpeningScript door;

    private void Update()
    {


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !door.waitforCollision)
        {
            ShowTrigger();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")&& !door.waitforCollision)
            HideTrigger();
    }

    private void ShowTrigger()
    {
        panel.ShowUI();

    }
    private void HideTrigger() { panel.HideUI(); }
}
