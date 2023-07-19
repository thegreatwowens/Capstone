
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSoundCaller : MonoBehaviour
{  

  public bool isMainMenu;
  public bool isTimeline;
  public bool isHardware;


void Start()
{

    if(isMainMenu){
          SoundManager.Instance.PlayMusic("BGMusicMainMenu",true);
    }else if(isTimeline)
    {
            SoundManager.Instance.PlayMusic("BGMusicTimeline",true);
    }else if (isHardware){
           SoundManager.Instance.PlayMusic("BGMusicHardware",true);
    }















  
}
  public void resetVolume(){
      SoundManager.Instance.bGSource.volume =1;
  }

  public void StopMusic(){
      SoundManager.Instance.StopMusic();
        }


}

