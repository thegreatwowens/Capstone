using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversionGamePanel : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvas;
    private bool showUI;
    private bool hideUI;
   public void ShowUI() { showUI = true; }
  public void HideUI() { hideUI = true; }

    private void Start()
    {
    }
    private void Update()
    {

        if (showUI)
        {
            canvas.alpha += Time.deltaTime;

            if (canvas.alpha == 1)
            {
                canvas.interactable = true;
                canvas.blocksRaycasts = true;
                canvas.alpha = 1;
                showUI = false;
            }
        }

        if (hideUI)
        {
            canvas.alpha -= Time.deltaTime*3f;

            if (canvas.alpha <= 0)
            {
                canvas.interactable = false;
                canvas.blocksRaycasts = false;
                canvas.alpha = 0;
                hideUI = false;
            }
        }
    }
}
   

