using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMaker;

public class MouseSettingsInput : MonoBehaviour
{
    GameObject player;
    bool enable;
    bool disable;
    bool attackmode;
    PlayMakerFSM fsm;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
      fsm = PlayMakerFSM.FindFsmOnGameObject(player,"Mouse Settings Default");
        
        if (enable)
        {
            fsm.SendEvent("UINavigation");
            enable = false;

        }
        if (disable)
        {
            fsm.SendEvent("MouseSettingDefault");
            disable = false;
        }
    }

    public void EnableMouseUI()
    {
        enable = true;

    }
    public void DiableMouseUI()
    {
        disable = true;
    }
}
