using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.SequencerCommands;

public enum Quizdificulty
{

    easy,
    medium,
    hard

}
public class QuizManager1 : SequencerCommandDelay
{
    [SerializeField]
    Usable usable;
    [SerializeField]
    UIQuizUpdater uIUpdater;
    public List<QuizItem> quizItems;
    public List<int> remainingQuestionIndices;
    private QuizItem currentQuizItem;

    int _questionsCount { get; set; }


    private Quizdificulty currentdifficulty;

    public void GameInitialized()
    {
        uIUpdater.GameInit();
        GlobalInputLock.Instance.DisableAllPlayerInputMovement();
        usable.enabled = false;

    }
    public void GameStart()
    {

        uIUpdater.StartGame();
        FirstSetQuestion();

    }
    public int Getdifficulty()
    {
        int value = 0;
        if (currentdifficulty == Quizdificulty.easy)
            value = 5;
        else if (currentdifficulty == Quizdificulty.medium)
            value = 10;
        else if (currentdifficulty == Quizdificulty.hard)
            value = 15;

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
    }
    private void MoveToNextQuestion()
    {
        // Check if there are remaining questions
        if (remainingQuestionIndices.Count > 0)
        {
            // Randomly select the next question index
            int randomIndex = Random.Range(0, remainingQuestionIndices.Count);
            int nextQuestionIndex = remainingQuestionIndices[randomIndex];
            remainingQuestionIndices.RemoveAt(randomIndex);

            // Get the quiz item for the selected question index
            currentQuizItem = quizItems[nextQuestionIndex];

            // Display the question
            uIUpdater.UpdateUI(currentQuizItem.question, remainingQuestionIndices.Count);
        }
        else
        {
            // The quiz is finished
            Debug.Log("Quiz completed!");
            GameFinished();
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
        }
        else
        {
            Debug.Log("Selected answer is incorrect!");
        }

        // Move to the next question
        MoveToNextQuestion();
    }
    public void GameFinished()
    {
        uIUpdater.EndGame();
        usable.enabled = true;
        GlobalInputLock.Instance.EnableAllMovements();
        sequencer.m_delayTimeLeft = 0;
    }

}
