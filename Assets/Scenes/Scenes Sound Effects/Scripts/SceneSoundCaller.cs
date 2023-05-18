
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSoundCaller : MonoBehaviour
{  

  public bool isMainMenu;
  public bool isTimeline;


void Start()
{

    if(isMainMenu){
          SoundManager.Instance.PlayMusic("BGMusicMainMenu",true);
    }else if(isTimeline)
    {
            SoundManager.Instance.PlayMusic("BGMusicTimeline",true);
    }

  
}
  public void resetVolume(){
      SoundManager.Instance.bGSource.volume =1;
  }

  public void StopMusic(){
      SoundManager.Instance.StopMusic();
        }


}
