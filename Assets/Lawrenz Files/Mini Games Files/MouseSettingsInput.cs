using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMaker;

public class MouseSettingsInput : MonoBehaviour
{
    public static MouseSettingsInput Instance;
    public   GameObject player;
    bool enable;
    bool disable;
    bool attackmode;
   public PlayMakerFSM fsm;
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
      //  player = GameObject.FindGameObjectWithTag("Player");
    //  fsm = PlayMakerFSM.FindFsmOnGameObject(player,"Mouse Settings Default");
        
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
    public void DisableMouseUI()
    {
        disable = true;
    }
}
