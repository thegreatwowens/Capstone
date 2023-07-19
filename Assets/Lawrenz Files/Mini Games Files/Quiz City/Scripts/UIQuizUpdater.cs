using UnityEngine;
using TMPro;


public class UIQuizUpdater : MonoBehaviour
{

    [SerializeField]
    QuizManager1 manager;
    [Header("Panels")]
    float uiSpeed =.5f;
    [SerializeField]
   private CanvasGroup _mainPanel, _difficultyPickPanel, _instructionPanel,_ingamePanel,_resultPanel;
    
    
/// UI Variables

    [Space]
    [Header("In-Game Values")]
   // Quiz Update UI
    [SerializeField]
    TextMeshProUGUI _currentQuestionUI;
    [SerializeField]
    TMP_InputField _answerField;

    [Space]
    [SerializeField]
    TextMeshProUGUI _questionsleft;
    [SerializeField]
    TextMeshProUGUI _scoreResult;
 
    
    public void UpdateUI(string currentQuestion,int questionsLeft){

        _currentQuestionUI.text = currentQuestion;
        _answerField.text = "";
        _questionsleft.text = $"Question:{questionsLeft} out of {manager.Getdifficulty()}";
    }
    public void UpdatePanel(CanvasGroup panel,bool value){
        LeanTween.alphaCanvas(panel,value?1:0,uiSpeed);
        panel.interactable = value;
        panel.blocksRaycasts =value;
    }
    public void GameInit(){
        UpdatePanel(_mainPanel,true);
    }

    public void selectedDificulty(){
        UpdatePanel(_difficultyPickPanel,false);
        UpdatePanel(_instructionPanel,true);
    }
    public void StartGame(){
        UpdatePanel(_instructionPanel,false);
        UpdatePanel(_ingamePanel,true);
    }
    
    public void ShowResult(int Score, Quizdificulty difficulty){
            if(difficulty == Quizdificulty.easy){
                 _scoreResult.text =  $"{Score}/5"; 
            }
            if(difficulty == Quizdificulty.medium){
                 _scoreResult.text =  $"{Score}/10"; 
            }
            if(difficulty == Quizdificulty.hard){
                 _scoreResult.text =  $"{Score}/20"; 
            }
            
         UpdatePanel(_resultPanel,true);
         UpdatePanel(_ingamePanel,false);
    }
    public void EndGame(){
        UpdatePanel(_mainPanel,false);
        UpdatePanel(_difficultyPickPanel,true);
        UpdatePanel(_instructionPanel,false);
        UpdatePanel(_ingamePanel,false);
        UpdatePanel(_resultPanel,false);
    }
    
}
