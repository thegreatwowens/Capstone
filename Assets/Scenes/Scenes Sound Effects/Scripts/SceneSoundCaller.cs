
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSoundCaller : MonoBehaviour
{  

  public bool isMainMenu;
  public bool isTimeline;
  public bool isHardware;
  public bool isCity;
  public bool isNetworkRoom;
  


void Start()
{

    if(isMainMenu){
          SoundManager.Instance.PlayMusic("BGMusicMainMenu",true);
    }else if(isTimeline)
    {
            SoundManager.Instance.PlayMusic("BGMusicTimeline",true);
    }else if (isHardware){
           SoundManager.Instance.PlayMusic("BGMusicHardware",true);
    }else if( isCity){
            SoundManager.Instance.PlayMusic("GeneralSound",true);

    }else if (isNetworkRoom){
            SoundManager.Instance.PlayMusic("NetworkRoom",true);
    }















  
}
  public void resetVolume(){
      SoundManager.Instance.bGSource.volume =1;
  }

  public void StopMusic(){
      SoundManager.Instance.StopMusic();
        }


}

