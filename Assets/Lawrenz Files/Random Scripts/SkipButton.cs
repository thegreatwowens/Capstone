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

        director.time = 5.55;
        effectFirstText.Stop();
        firstSentence =true;
    }else if(!secondSentence&& effectFirstText.isPlaying == false){
        director.time = 16;
        effectSecondText.Stop();
        secondSentence =true;
    }else if(!thirdsentence && effectSecondText.isPlaying == false){
        director.time = 28;
        effectThirdText.Stop();
        thirdsentence = true;
    }
    
}
}
