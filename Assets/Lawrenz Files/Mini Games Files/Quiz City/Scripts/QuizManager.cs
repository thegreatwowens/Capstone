using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.SequencerCommands;
using UnityEngine.Events;
using Invector.vCharacterController.vActions;

public class QuizManager : SequencerCommandDelay
{
    [SerializeField]
    private string currentQuestionDebug;
    [SerializeField]
    private string currentAnswerDebug;
    [SerializeField]
    private string debugScore;
    [SerializeField]
    DoorNotification restrict;
    [Header("Refrences")]
    [SerializeField]
    vTriggerGenericAction vGenericAction;
    [SerializeField]
     ConversionGamePanel panel;
    [SerializeField]
    TextMeshProUGUI questionField;
    [SerializeField]
    TextMeshProUGUI timerFeed;
    [SerializeField]
    TMP_InputField answerfield;
    [SerializeField]
    DialogueSystemTrigger trigger;
    [SerializeField]
    TextMeshProUGUI questionsLeft;
    [SerializeField]
    ConversionGamePanel mainPanel;
    [SerializeField]
    QuizEnabler quizEnabler;
    [SerializeField]
    GlobalInputLock globallock;
    public bool session;
    [Space]
    [Header("Available Questions")]
    [SerializeField]
    List<QuestionGenerated> questions = new List<QuestionGenerated>();
    [Header("Questions That are available at Runtime")]
    #region List of Question per Difficulty
    [SerializeField]
    List<QuestionGenerated> givenQuestions = new List<QuestionGenerated>();
    [Header("Event Callers")]
    public UnityEvent onCorrect;
    public UnityEvent onWrong;
    public UnityEvent onExit;
    public UnityEvent onCompleted;
    public UnityEvent onTimesUp;
    [SerializeField]
    MouseSettingsInput mouse;
    #endregion
    #region Private Variables
    int selection { get; set; }

    int currentQuestioncount { get; set; }
    int easy = 10;
    int normal = 20;
    int hard = 30;
    int questiontotal { get; set; }
    float timeMinutes;
    float timeSeconds;
    float settime = 0;
    string endtype { get; set; }
    int questionCountHolder { get; set; }
    string currentAnswer { get; set; }
    string currentQuestion { get; set; }
    int scorecount { get; set; }
    string currentDifficulty { get; set; }
    int resultqeustion;
    [SerializeField]
    private bool onquestion;
    List<int> randomInts = new List<int>();
    vThirdPersonInput _character;
    

    #endregion


    private  new void  Start()
    {

        
    }

    IEnumerator randomizer()
    {
        Debug.Log(CurrentQuestion());
        yield return new WaitForEndOfFrame();
        StopCoroutine(randomizer());

    }
    private new void Update()
    {
        _character = GameObject.FindGameObjectWithTag("Player").GetComponent<vThirdPersonInput>();

        if (session)
        {
            settime -= Time.deltaTime;
            trigger.enabled = false;
            vGenericAction.enabled = false;
            timeMinutes = Mathf.FloorToInt(settime / 60);
            timeSeconds = Mathf.FloorToInt(settime % 60);

            if (questionCountHolder <= 0 )
            {
                if(onCompleted != null) { onCompleted.Invoke(); }
                panel.HideUI();
                session = false;
            }
            if(settime <= 0)
            {
                panel.HideUI();
                session = false;
                if(onTimesUp != null)
                {
                    onTimesUp.Invoke();
                }
            }
            
        }
        timerFeed.text = timeMinutes.ToString("0") + ":" + timeSeconds.ToString("00");
        currentAnswerDebug = currentAnswer;
        currentQuestionDebug = currentQuestion;
        debugScore = scorecount.ToString();
       

    }
 

    public void Checker()
    {
        if (currentAnswer.ToLower().Equals(answerfield.text.ToLower()))//currentAnswer.ToLower().Contains(answerfiel.text.ToLower()) == currentAnswer) answerfield.text.ToLower() == currentAnswer.ToLower()||
        {
           
            CorrectAnswer();
            scorecount++;
            currentQuestioncount++;
            answerfield.text = "";
            answerfield.Select();
        }
        else
        { 
            WrongAnswer();
            currentQuestioncount++;
            answerfield.text = "";
            answerfield.Select();
        }
     if(questionCountHolder <= 0)
        {
            panel.HideUI();
            
        }
    }

    public string returnDifficulty()
    {

        return currentDifficulty;
    }
    public int returnScoreCount()
    {
        return scorecount;
    }
    public int returnQuestionCount()
    {
        return resultqeustion;
        
    }
    public void resetScoreCount()
    {
        scorecount = 0;
    }
    public void QuestionPopulator(string difficulty)
    {
        for (int i = 0; i <= questions.Count -1 ; i++)
        {
            randomInts.Add(i);         
        }
        randomInts.Shuffle();
        if (difficulty == "easy")
        {
            for (int i =0;i<easy;i++)
            {
                

                givenQuestions.Add(questions[randomInts[i]]);
            }
            settime = 120f;
            questiontotal = easy;
            questionCountHolder = easy;
            questionsLeft.text = "QuestionIndex: 1/10 QuestionIndex Left: 10";
            resultqeustion = questionCountHolder;
            currentDifficulty = "easy";
        }    
        if(difficulty == "normal")
        {
            for (int i = 0; i <= normal-1; i++)
            {
                givenQuestions.Add(questions[randomInts[i]]);
            }
            settime = 240f;
            questiontotal = normal;
            questionCountHolder = normal;
            questionsLeft.text = "QuestionIndex: 1/20 QuestionIndex Left: 20";
            resultqeustion = questionCountHolder;
            currentDifficulty = "normal";
        }
        if(difficulty == "hard")
        {
            for (int i = 0; i <= hard-1; i++)
            {
                givenQuestions.Add(questions[randomInts[i]]);
            }
            settime = 360f;
            questiontotal = hard;
            questionCountHolder = hard;
            questionsLeft.text = "QuestionIndex: 1/30 QuestionIndex Left: 30";
            resultqeustion = questionCountHolder;
            currentDifficulty = "hard";
        }
    }

    public void StartGame()
    {
        if (_character.cc.isStrafing)
        {
            restrict.EquippedNotification();
            return;
        }
        else
        {
            panel.ShowUI();
            session = true;
            globallock.DisableAllPlayerInputMovement();
            questionField.text = CurrentQuestion();
            currentAnswer = CurrentAnswer();
            currentQuestioncount = 1;
        }
       
        
    }

    public void EndSession()
    {
        mouse.DiableMouseUI();
        session = false;
        sequencer.m_delayTimeLeft = 0;
        globallock.EnableAllMovements();
        trigger.enabled = true;
        vGenericAction.enabled = true;
        
    }

    private string CurrentQuestion()
    {
        currentQuestion = givenQuestions[selection].question;

        return currentQuestion;
    }
    private string CurrentAnswer()
    {
        
            currentAnswer = givenQuestions[selection].answer;
        return currentAnswer;
    }
    private void CorrectAnswer()
    {
        givenQuestions.Remove(givenQuestions[selection]);
        questionCountHolder--;
        SetQuestion();
        UpdateUI();
        questionField.text = CurrentQuestion();
        Debug.Log("Correct is Called.");
        if(onCorrect != null) { onCorrect.Invoke(); }
    }
    private void WrongAnswer()
    {
        givenQuestions.Remove(givenQuestions[selection]);
        questionCountHolder--;
        SetQuestion();
        UpdateUI();
        questionField.text = CurrentQuestion();
        Debug.Log("Wrong is Called.");
        if (onWrong != null) { onWrong.Invoke(); }


    }
    private void SetQuestion()
    {
        selection = Random.Range(0, givenQuestions.Count);
        StartCoroutine(randomizer());
        currentAnswer = CurrentAnswer();
    }
    private void UpdateUI()
    {
        questionsLeft.text = "QuestionIndex: " + currentQuestioncount.ToString() + "/" + questiontotal+ " QuestionIndex Left: "+givenQuestions.Count;
        
    }

    public void QuitEndsession()
    {
        globallock.EnableAllMovements();
        quizEnabler.ResetChoices();
        session = false;
        sequencer.m_delayTimeLeft = 0;
       // _character.SetLockCameraInput(false);
      ///  _character.SetLockUpdateMoveDirection(false);
      //  _character.SetLockBasicInput(false);
        trigger.enabled = true;
        vGenericAction.enabled = true;
        mouse.DiableMouseUI();
        mainPanel.HideUI();
        resetScoreCount();
        answerfield.text = "";
        resetScoreCount();
        givenQuestions.Clear();
        
    }
}
