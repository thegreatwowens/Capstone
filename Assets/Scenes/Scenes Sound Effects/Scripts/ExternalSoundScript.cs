using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalSoundScript : MonoBehaviour
{
    

void Start()
{
}

    public void UIClicks(){
        SoundManager.Instance.PlaySoundFx("UIClick");
    }       
    public void TypeEffects(){
        SoundManager.Instance.PlaySoundFx("TypeWriterSound");
    }

}
