using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizEnabler : MonoBehaviour
{
    [SerializeField]
    QuizManager manager;
    [SerializeField]
    TextMeshProUGUI difficulty;
    [SerializeField]
    TextMeshProUGUI textInstruction;
    [SerializeField]
    ConversionGamePanel panel;
    [SerializeField]
    ConversionGamePanel difficultyPanel;
    [SerializeField]
    QuizManager quiz;
    [SerializeField]
    ConversionGamePanel contentPanel;

    private bool easy;
    private bool normal;
    private bool hard;
    private float owntimer;
    public string  choice{get; set;}

    private void Update()
    {
        if (easy)
        {
            difficulty.text = "Easy";
            textInstruction.text = "10 trivia questions<br>2 minutes to answer <br> Quiting can cause penalty";
        }
        if (normal)
        {
            owntimer = 3;
            owntimer -= Time.deltaTime;
            difficulty.text = "Normal";
            textInstruction.text = "20 trivia questions<br> 4 minutes to answer <br> Quiting can cause penalty";
        }
        if (hard)
        {
            owntimer = 3;
            owntimer -= Time.deltaTime;
            difficulty.text = "Hard";
            textInstruction.text = "30 trivia questions<br>5 minutes to answer <br> Quiting can cause penalty";
        }

    }

    public void EasyChoice()
    {
        easy = true;
        panel.ShowUI();
        difficultyPanel.HideUI();
        choice = "easy";
    }
    public void NormalChoice()
    {
        normal = true;
        panel.ShowUI();
        difficultyPanel.HideUI();
        choice = "normal";
    }
    public void HardChoice()
    {
        hard = true;
        panel.ShowUI();
        difficultyPanel.HideUI();
        choice = "hard";
    }
    public void ResetChoices()
    {
        textInstruction.text = "";
        difficulty.text = "";
        choice = "";
        panel.HideUI();
        easy = false;
        normal = false;
        hard = false;
        difficultyPanel.ShowUI();
        contentPanel.HideUI();
    }
    public void CallStartGame()
    {
        quiz.QuestionPopulator(choice);
        quiz.StartGame();
        panel.HideUI();

    }

}
