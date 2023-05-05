using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QInventory;

public class NextSceneSaver : MonoBehaviour
{
    private void Start()
    {
       
    }

    public static void NextSceneSave()
    {
        InventoryManager.SaveInventoryData();
    }
    private void Update()
    {
        
    }
}
