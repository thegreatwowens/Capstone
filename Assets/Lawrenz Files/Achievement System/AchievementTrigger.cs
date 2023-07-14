using UnityEngine;
using PixelCrushers.DialogueSystem;
using DiZTools_AchievementsSystem;

public class AchievementTrigger : MonoBehaviour
    {
      [SerializeField]
       GameObject _cityExplorer,_detroitHardware,_mainHardwareUnlock,_EasyQuiz,_mediumQuiz,_hardQuiz,_binaryInteract;

        public void AchievementDetroitExploration(){

            AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.DetroitHardware, true);
            _detroitHardware.SetActive(false);
        }

        public void CityExplorer(){
         AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.CityExplorer,true);
          _cityExplorer.SetActive(false);
        }

        public void BinaryInteract(){
            _binaryInteract.SetActive(false);

        }
        
        public void MainHardwareUnlock(){
           _mainHardwareUnlock.SetActive(false);
        }
        
        public void EasyQuiz(){
            _EasyQuiz.SetActive(false);
        }
        public void MeduimQuiz(){
          _mediumQuiz.SetActive(false);

        }
        public void HardQuiz(){
          _mainHardwareUnlock.SetActive(false);
        }

        
      void OnEnable()
      {
        Lua.RegisterFunction("DetroitExploration", this, SymbolExtensions.GetMethodInfo(() => AchievementDetroitExploration()));
        Lua.RegisterFunction("CityExplorer", this, SymbolExtensions.GetMethodInfo(() => CityExplorer()));
        Lua.RegisterFunction("BinaryInteract", this, SymbolExtensions.GetMethodInfo(() => BinaryInteract()));
        Lua.RegisterFunction("MainHardwareUnlock", this, SymbolExtensions.GetMethodInfo(() => MainHardwareUnlock()));
        Lua.RegisterFunction("EasyQuiz", this, SymbolExtensions.GetMethodInfo(() => EasyQuiz()));
        Lua.RegisterFunction("MeduimQuiz", this, SymbolExtensions.GetMethodInfo(() => MeduimQuiz()));
        Lua.RegisterFunction("HardQuiz", this, SymbolExtensions.GetMethodInfo(() => HardQuiz()));

      }

      void OnDisable()
      {
       Lua.UnregisterFunction("DetroitExploration");
       Lua.UnregisterFunction("CityExplorer");
       Lua.UnregisterFunction("BinaryInteract");
       Lua.UnregisterFunction("MainHardwareUnlock");
       Lua.UnregisterFunction("EasyQuiz");
       Lua.UnregisterFunction("MeduimQuiz");
       Lua.UnregisterFunction("HardQuiz");

      }


    }

