using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [Range(0, 1)]
    public float MasterVolume = 1;
    public Sound[] UISounds;
    public Sound[] SoundEffects;
    public Sound[] BackgroundMusic;

    AudioMixerGroup mixerGroup;




    private void Awake()
    {
      
        if ( instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Application.targetFrameRate = 200;

        foreach (Sound s in UISounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = mixerGroup;
        }
        foreach (Sound s in SoundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = mixerGroup;
        }
        foreach (Sound s in BackgroundMusic)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;

            s.source.outputAudioMixerGroup = mixerGroup;
        }

    }
    private void Update()
    {


    }
    public void Play(string name,string type)
    {
       if(type == "UISound")
        {
            Sound UIEffects = Array.Find(UISounds, sound => sound.name == name);
            UIEffects.source.Play();
        }
        if (type == "BackgroundMusic")
        {
            
            Sound _BackgroundMusic = Array.Find(BackgroundMusic, sound => sound.name == name);
            _BackgroundMusic.source.Play();
        }
        if(type == "SoundFx") {
            Sound _SoundEffects = Array.Find(SoundEffects, sound => sound.name == name);
            _SoundEffects.source.Play();
        }
 

    }
    public void Play(string name)
    {
        Sound _BackgroundMusic = Array.Find(BackgroundMusic, sound => sound.name == name);
        _BackgroundMusic.source.Play();

    }

    public void Stop(string name)
    {
        Sound _BackgroundMusic = Array.Find(BackgroundMusic, sound => sound.name == name);
        _BackgroundMusic.source.Stop();
    }
    public void Stop()
    {
        Sound _BackgroundMusic = Array.Find(BackgroundMusic, sound => sound.name == name);
        _BackgroundMusic.source.Stop();
    }
    public void Stop(string name,string type)
    {
        if (type == "UISound")
        {
            Sound s = Array.Find(UISounds, sound => sound.name == name);
            s.source.Stop();
            Debug.Log("Stopped Sound");
        }
           
    }
    public void PauseMusic(string name, string type)
    {
        if(type == " UISound") {
            Sound UIEffects = Array.Find(UISounds, sound => sound.name == name);
            UIEffects.source.Pause();
        }
        if (type == "BackgroundMusic")
        {
            Sound _BackgroundMusic = Array.Find(BackgroundMusic, sound => sound.name == name);
            _BackgroundMusic.source.Pause();
        }
        if (type == "SoundFx") { Sound _SoundEffects = Array.Find(SoundEffects, sound => sound.name == name);
            _SoundEffects.source.Pause();
        }
    }
    private void Start()
    {

    }



}
