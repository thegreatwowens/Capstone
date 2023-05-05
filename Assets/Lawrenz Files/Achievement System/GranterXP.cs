using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QInventory;

public class GranterXP : MonoBehaviour
{
    [SerializeField]
    float expValue;
    PlayerLevelHandler expHandler;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        expHandler = FindObjectOfType<PlayerLevelHandler>();
    }
    public void GrantXP()
    {
        InventoryManager.ChangePlayerAttributeValue("Experience", expValue, Effect.Restore);
    }
}
