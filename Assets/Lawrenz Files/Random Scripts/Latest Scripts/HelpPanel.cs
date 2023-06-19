using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    [SerializeField]
    CanvasGroup _helpPanel,_pulsingText;
    float uiSpeed = .3f;
 
 private void Start() {
     pulse();
 }
 private void pulse(){
        LeanTween.alphaCanvas(_pulsingText,1,1).setLoopPingPong();

       
 }
 void Update()
 {
    if(Input.GetKeyDown(KeyCode.F3)){
        OpenPanel();
    }
    if(Input.GetKeyDown(KeyCode.Escape)){
        ClosedPanel();
    }
  
 }
 public void OpenPanel(){
      LeanTween.cancel(_pulsingText.gameObject);
     _helpPanel.interactable = !_helpPanel.interactable;
     _helpPanel.blocksRaycasts = !_helpPanel.blocksRaycasts;
        LeanTween.alphaCanvas(_helpPanel,1,uiSpeed);
 }
 public void ClosedPanel(){
    pulse();
    _helpPanel.interactable = !_helpPanel.interactable;
    _helpPanel.blocksRaycasts = !_helpPanel.blocksRaycasts;
    LeanTween.alphaCanvas(_helpPanel,0,uiSpeed);

 }
}
