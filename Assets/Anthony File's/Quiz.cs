using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Invector.vCharacterController;
using UnityEngine.Events;

public class Quiz : MonoBehaviour
{
    public UnityEvent onRight;
    public UnityEvent onWrong;
    public UnityEvent onComplete;
    #region References
    [Header("Questtion And Answer Area")]
    public string[] questionList;
    public string curQuestion;
    public string[] answerList;
    public string curAnswer;
    int itemCount;
    [SerializeField]
    int score;


    [Header("UI")]
    public TextMeshProUGUI questionText;
    public TMP_InputField answerArea;
    public Button submitBtn;
    public TextMeshProUGUI scoreText;

    int selection;
    private bool session;
    [SerializeField]
    private ConversionGamePanel panel;

    [SerializeField]
    private vThirdPersonInput _character;
    #endregion 
    void Update()
    {

        questionText.text = QuestionList();
        curAnswer = CurrentAnswer();
        _character = GameObject.FindGameObjectWithTag("Player").GetComponent<vThirdPersonInput>();

        if (session)
        {


        }
        else
        {

        }
   
    }
    public string CurrentAnswer()
    {   string answer = answerList[selection];
         
        return answer;
    }
    public string QuestionList()
    {
     
        string question = questionList[selection];

       
        return question;
    }

   private void QuestionGenerator() {
        selection = Random.Range(0, questionList.Length);
       // curQuestion = QuestionList();
       // curAnswer = CurrentAnswer();

    }

    public void StartGame()
    {
      
        panel.ShowUI();
        session = true;
        _character.SetLockCameraInput(true);
        _character.SetLockUpdateMoveDirection(true);
        _character.SetLockBasicInput(true);
        QuestionGenerator();


    }

    public void Close()
    {
        panel.HideUI();
        session = false;
        _character.SetLockCameraInput(false);
        _character.SetLockUpdateMoveDirection(false);
        _character.SetLockBasicInput(false);
    }
    public void CheckAnswer()
    {
        //if answer is correct
        if (answerArea.text.ToLower() == curAnswer)
        {
            Correct();
        }

        //if wrong
        else
        {
            Wrong();
        }

        //this code is for when all questions are answered
        if (itemCount == questionList.Length)
        {

            scoreText.text = score.ToString();
            itemCount = 0;
        }
    }

    public void Correct()
    {
        answerArea.text = "";
        itemCount++;
        score++;
        

        if(itemCount != questionList.Length)
        {

            QuestionGenerator();
        }


        if (onRight != null)
        onRight.Invoke();
    }

    public void Wrong()
    {
        answerArea.text = "";
        itemCount++;

        if(onWrong != null)
        onWrong.Invoke();
    }
}
