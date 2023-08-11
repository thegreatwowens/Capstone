using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabler : MonoBehaviour
{
   GameObject disable;

    void Start()
    {
        
       StartCoroutine(DisableOnStart());
       
    }

    IEnumerator DisableOnStart(){

        yield return new WaitForSeconds(.5f);
        disable = GameObject.Find("Dialogue Panel");
        disable.SetActive(false);
        yield return new WaitForSeconds(1);
         MouseSettingsInput.Instance.EnableMouseUI();

    }

}
