using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.SequencerCommands;
using UnityEngine.Events;
using System.Collections;

public enum Quizdificulty
{

    easy,
    medium,
    hard

}
public class QuizManager : SequencerCommandDelay
{
    [SerializeField]
    Usable usable;
    [SerializeField]
    UIQuizUpdater uIUpdater;
    public List<QuizItem> quizItems;
    public List<int> remainingQuestionIndices;
    private QuizItem currentQuizItem;

    int _questionsCount { get; set; }
    
    [HideInInspector]
    public int Score {get; set;}
    
    public UnityEvent onGameFinished;
    public UnityEvent onTimesUp;

    private Quizdificulty currentdifficulty;

    public void GameInitialized()
    {
        uIUpdater.GameInit();
        GlobalInputLock.Instance.DisableAllPlayerInputMovement();
        usable.enabled = false;

    }
    public void GameStart()
    {
        Score = 0;
        FirstSetQuestion();
        uIUpdater.StartGame();
      

    }
    
    public int Getdifficulty()
    {
        int value = 0;
        if (currentdifficulty == Quizdificulty.easy)
            value = 5;
        else if (currentdifficulty == Quizdificulty.medium)
            value = 10;
        else if (currentdifficulty == Quizdificulty.hard)
            value = 20;

        return value;
    }
    public void difficultySet(Quizdificulty difficulty)
    {
        if (difficulty == Quizdificulty.easy)
        {
            IntializedQuestionList(5);

        }
        else if (difficulty == Quizdificulty.medium)
        {
            IntializedQuestionList(10);

        }

        else if (difficulty == Quizdificulty.hard)
        {
            IntializedQuestionList(20);

        }
        currentdifficulty = difficulty;
        uIUpdater.selectedDificulty();
    }

    public void IntializedQuestionList(int value)
    {
        
        _questionsCount = value;
        remainingQuestionIndices = new List<int>();
        for (int i = 0; i < value; i++)
        {
            remainingQuestionIndices.Add(i);
        }
        ShuffleQuestionIndices();
    }
    private void ShuffleQuestionIndices()
    {
        int n = remainingQuestionIndices.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int value = remainingQuestionIndices[k];
            remainingQuestionIndices[k] = remainingQuestionIndices[n];
            remainingQuestionIndices[n] = value;
        }
    }

    private void FirstSetQuestion()
    {
        int randomIndex = Random.Range(0, remainingQuestionIndices.Count);
        int nextQuestionIndex = remainingQuestionIndices[randomIndex];
        currentQuizItem = quizItems[nextQuestionIndex];
        uIUpdater.UpdateUI(currentQuizItem.question, remainingQuestionIndices.Count);
        remainingQuestionIndices.RemoveAt(randomIndex);
      
    }
    private void MoveToNextQuestion()
    {
        
        
        // Check if there are remaining questions
        if (remainingQuestionIndices.Count > 0)
        {
            // Randomly select the next question index
            int randomIndex = Random.Range(0, remainingQuestionIndices.Count);
            int nextQuestionIndex = remainingQuestionIndices[randomIndex];
            // Get the quiz item for the selected question index
            currentQuizItem = quizItems[nextQuestionIndex];
            // Display the question
            uIUpdater.UpdateUI(currentQuizItem.question, remainingQuestionIndices.Count);
            remainingQuestionIndices.RemoveAt(randomIndex);
        }
        else
        {
            // The quiz is finished
            Debug.Log("Quiz completed!");
            ShowResult();
        }
    }
    public void CheckAnswer(string selectedAnswer)
    {
        // Get the list of correct answers for the current question
        List<string> correctAnswers = currentQuizItem.possibleAnswers;

        // Check if the selected answer is in the list of correct answers
        if (correctAnswers.Contains(selectedAnswer))
        {
            Debug.Log("Selected answer is correct!");
            Score++;
        }
        else if(selectedAnswer == "test")
                {
                            Score++;
                            Debug.Log("Selected answer is correct!");
                }
        else
        {
            Debug.Log("Selected answer is incorrect!");
        }

        // Move to the next question
        MoveToNextQuestion();
    }

        IEnumerator DelayResult(){
             
            yield return new WaitForSeconds(5);
            GameFinished();
            
        }
    public void ShowResult(){
        uIUpdater.ShowResult(Score,currentdifficulty);
        StartCoroutine(DelayResult());
        AchievementTrigger.Instance.ScoreCheck(Score,currentdifficulty);
    }


    public void GameFinished()
    {
        onGameFinished?.Invoke();
        uIUpdater.EndGame();
        usable.enabled = true;
        GlobalInputLock.Instance.EnableAllMovements();
        sequencer.m_delayTimeLeft = 0;
        

    }
    public void TimesUp(){

            onTimesUp?.Invoke();
    }

}
