using UnityEngine;
using DiZTools_AchievementsSystem;

public class AchievementExternalSender : MonoBehaviour
{
  
  public void CityExplorer(){
    AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.CityExplorer,true);
  }
}
