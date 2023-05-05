using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMainMenuScript : MonoBehaviour
{
    [SerializeField]
    CanvasGroup mainMenuPanel;
    [SerializeField]
    CanvasGroup confirmationPanel;

    bool mainMenuOpened;
    bool confirmationOpened;
    bool mainmenufadein;
    bool confirmationfadein;

    IEnumerator MainMenuOpened()
    {
        
        yield return new WaitForSeconds(1);

        

    }

    private void MainMenuClicked()
    {
        MainMenuOpened();
    }

    private void Update()
    {
        if (mainmenufadein)
        {
            mainMenuPanel.interactable = true;
            mainMenuPanel.blocksRaycasts = true;
            

        }
    }
}
