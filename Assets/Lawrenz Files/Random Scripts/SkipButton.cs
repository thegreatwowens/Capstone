using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using PixelCrushers.DialogueSystem;

public class SkipButton : MonoBehaviour
{


public PlayableDirector director;
public TextMeshProTypewriterEffect effectFirstText;

public TextMeshProTypewriterEffect effectSecondText;

public TextMeshProTypewriterEffect effectThirdText;
 void Awake() {
    
}


bool firstSentence,secondSentence,thirdsentence;

public void SkipButtonClicked(){
    if(!firstSentence){

        director.time = 4.9;
        effectFirstText.Stop();
        firstSentence =true;
    }else if(!secondSentence&& effectFirstText.isPlaying == false){
        director.time = 19.85;
        effectSecondText.Stop();
        secondSentence =true;
    }else if(!thirdsentence && effectSecondText.isPlaying == false){
        director.time = 37.25;
        effectThirdText.Stop();
        thirdsentence = true;
    }
    
}
void Update()
{
    if(Input.GetKeyDown(KeyCode.Space))
    {
        SkipButtonClicked();
    }
    if(director.time >=4.9){
        firstSentence =true;

    }
    if(director.time>=19.85)
    secondSentence = true;

    if(director.time >=37.25)
    thirdsentence = true;

}
}
