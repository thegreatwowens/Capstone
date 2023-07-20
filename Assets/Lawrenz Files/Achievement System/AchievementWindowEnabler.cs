using UnityEngine;
using DiZTools_AchievementsSystem;

public class AchievementWindowEnabler: MonoBehaviour
{
  
  [SerializeField]
  GameObject Imageholder;

  
  public void DisableImage(){

        Imageholder.SetActive(false);

  }
}
