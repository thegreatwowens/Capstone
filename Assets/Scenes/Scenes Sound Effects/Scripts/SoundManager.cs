using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [Range(0, 1)]
    public float MasterVolume = 1;
    public SoundClip[] bGMusic,fXClip;
    
    [SerializeField]
    AudioMixer mixer;
    private float master,music,soundFX;
    public AudioSource bGSource,fXSource;
    
    void Awake()
    {
        if(Instance == null){
                Instance = this;
                DontDestroyOnLoad(this.gameObject);

        }else{
            Destroy(this.gameObject);
        }

    }

    Scene currentScene;

void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnDisable() {
      SceneManager.sceneLoaded -= OnSceneLoaded;
    }
   public void PlayMusic(string name,bool isLooping){
         SoundClip sound =     Array.Find(bGMusic,x => x.name == name);
            
            if(sound == null){
                
                print ("No sound Named: "+ name+" at "+ bGMusic.ToString());

            } 
            else{
                if(isLooping){
                        bGSource.loop = true;
                }else
                    bGSource.loop = false;   
            
                bGSource.clip = sound.clip;
                bGSource.Play();
            }

    }
    public void StopMusic(){
            bGSource.Stop();
    }
    public void PlaySoundFx(string name){
        SoundClip sound =  Array.Find(fXClip,x => x.name == name);
            
            if(sound == null){
                
                print ("No sound Named: "+ name+" at "+ fXSource.ToString());

            } 
            else{

                fXSource.PlayOneShot(sound.clip);
            }
        
    }

    public void VolumeSliderMaster (float volume){
         mixer.SetFloat("Master",MathF.Log10(volume)*20);
         master = MathF.Log10(volume)*20;
    }
    public void VolumeSliderSoundFx(float volume){
                mixer.SetFloat("SoundFx",MathF.Log10(volume)*20);
                soundFX = MathF.Log10(volume)*20;
    }
    public void VolumeSliderMusic(float volume){
                mixer.SetFloat("BGMusic",MathF.Log10(volume)*20);
                music = MathF.Log10(volume)*20;
    }
    
    public float ReturnMasterVolume(){
        
        return master;
    }
    
    public float ReturnSoundFx(){

        return soundFX;
    }
    
    public float ReturnMusic(){
        return music;
    } 


}
