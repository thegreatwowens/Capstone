using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalSoundScript : MonoBehaviour
{
    SoundManager manager;
 
   private enum Typeofsound
    {
        Select,
        UISound,
        BackgroundMusic,
        SoundFx

    }
   [Header("Values")]
    public string audioName;
    [SerializeField]
    Typeofsound type;
    private void Awake()
    {
        manager = FindObjectOfType<SoundManager>();
    }
    private void OnValidate()
    {
        if (audioName == null)
        {
            Debug.Log("No audio name in object:" + gameObject.name);
        }
    }
    private void Update()
    {
       
    }
    public void Play()
    {
            manager.Play(audioName,type.ToString());
    }
    public void PlayOneShot()
    {
      
    }

}
