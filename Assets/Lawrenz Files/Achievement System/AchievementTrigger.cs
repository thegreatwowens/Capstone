using UnityEngine;
using PixelCrushers.DialogueSystem;
using DiZTools_AchievementsSystem;
using PixelCrushers;

public class AchievementTrigger : MonoBehaviour
    {

      void Awake()
      {
        if(Instance == null){
          Instance = this;
        }
      }
        public static AchievementTrigger Instance;
      [SerializeField]
       GameObject _cityExplorer,_detroitHardware,_mainHardwareUnlock,_EasyQuiz,_mediumQuiz,_hardQuiz,_binaryInteract;

        public void AchievementDetroitExploration(){

            AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.DetroitHardware, true);
            _detroitHardware.SetActive(false);
            SaveSystem.SaveToSlotImmediate(1);
            
        }

        public void CityExplorer(){
         AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.CityExplorer,true);
          _cityExplorer.SetActive(false);
          SaveSystem.SaveToSlotImmediate(1);
        }

        public void BinaryInteract(){
          AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.Binary, true);
            _binaryInteract.SetActive(false);
            SaveSystem.SaveToSlotImmediate(1);

        }
        
        public void MainHardwareUnlock(){
          AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.UnlockMainHardware, true);
           _mainHardwareUnlock.SetActive(false);
           SaveSystem.SaveToSlotImmediate(1);
        }
        
        public void EasyQuiz(){
          AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.QuizMasterEasy, true);
            _EasyQuiz.SetActive(false);
            SaveSystem.SaveToSlotImmediate(1);
        }
        public void MeduimQuiz(){
          AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.QuizMasterMedium, true);
          _mediumQuiz.SetActive(false);
          SaveSystem.SaveToSlotImmediate(1);

        }
        public void HardQuiz(){
          AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.QuizHard, true);
          _mainHardwareUnlock.SetActive(false);
          SaveSystem.SaveToSlotImmediate(1);
        }

        public void ScoreCheck(int Score , Quizdificulty difficulty){
                if(Score == 5 && difficulty == Quizdificulty.easy)
                        EasyQuiz();
                if(Score == 10 && difficulty == Quizdificulty.medium)
                        MeduimQuiz();
                 if(Score == 20 && difficulty == Quizdificulty.hard)
                        HardQuiz();
          
        }
        
      void OnEnable()
      {
        Lua.RegisterFunction("AchievementDetroitExploration", this, SymbolExtensions.GetMethodInfo(() => AchievementDetroitExploration()));
        Lua.RegisterFunction("CityExplorer", this, SymbolExtensions.GetMethodInfo(() => CityExplorer()));
        Lua.RegisterFunction("BinaryInteract", this, SymbolExtensions.GetMethodInfo(() => BinaryInteract()));
        Lua.RegisterFunction("MainHardwareUnlock", this, SymbolExtensions.GetMethodInfo(() => MainHardwareUnlock()));
        Lua.RegisterFunction("EasyQuiz", this, SymbolExtensions.GetMethodInfo(() => EasyQuiz()));
        Lua.RegisterFunction("MeduimQuiz", this, SymbolExtensions.GetMethodInfo(() => MeduimQuiz()));
        Lua.RegisterFunction(" ", this, SymbolExtensions.GetMethodInfo(() => HardQuiz()));

      }

      void OnDisable()
      {
       Lua.UnregisterFunction("AchievementDetroitExploration");
       Lua.UnregisterFunction("CityExplorer");
       Lua.UnregisterFunction("BinaryInteract");
       Lua.UnregisterFunction("MainHardwareUnlock");
       Lua.UnregisterFunction("EasyQuiz");
       Lua.UnregisterFunction("MeduimQuiz");
       Lua.UnregisterFunction("HardQuiz");

      }


    }

