using UnityEngine;
using PixelCrushers.DialogueSystem;
using DiZTools_AchievementsSystem;



    public class AchievementTrigger : MonoBehaviour
    {
        
        public void AchievementDetroitExploration(){

            AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.DetroitHardware, true);
        }

        public void CityExplorer(){
         AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.CityExplorer,true);
        }
      void OnEnable()
      {
        Lua.RegisterFunction("DetroitExploration", this, SymbolExtensions.GetMethodInfo(() => AchievementDetroitExploration()));
        Lua.RegisterFunction("CityExplorer", this, SymbolExtensions.GetMethodInfo(() => CityExplorer()));
      }

      void OnDisable()
      {
       Lua.UnregisterFunction("DetroitExploration");
       Lua.UnregisterFunction("CityExplorer");
      }


    }

