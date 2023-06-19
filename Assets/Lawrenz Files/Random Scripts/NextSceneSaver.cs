using UnityEngine;
using QInventory;
using PixelCrushers;
using PixelCrushers.DialogueSystem;


public class NextSceneSaver : MonoBehaviour
{
    public bool saveSpawnSavedLocation;

    public GameObject SpawnLocation;
    public  void NextSceneSave()
    {
        InventoryManager.SaveInventoryData();
        SaveSystem.SaveToSlotImmediate(1);
      
      if(saveSpawnSavedLocation){
            SaveSystem.playerSpawnpoint = SpawnLocation;
      }
    }

}
