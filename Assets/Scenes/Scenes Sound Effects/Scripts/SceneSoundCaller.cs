using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundCaller : MonoBehaviour
{
    [SerializeField]
    ScriptableExternalSound[] sounds;
    SoundManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<SoundManager>();   
        
        
    }

    private void Start()
    {
        foreach (ScriptableExternalSound sound in sounds)
        {
          
        }

        foreach (ScriptableExternalSound sound in sounds)
        {
            manager.Play(sound.audioName);
        }

    }
      public void ResetSceneSounds()
    {
        manager.Stop();

    }


}
