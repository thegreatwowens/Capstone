using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ResultPanelScript : MonoBehaviour
{
    [SerializeField]
    ConversionGamePanel panel;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI autoContinueText;
    [SerializeField]
    QuizManager quiz;
    [SerializeField]
    ConversionGamePanel MainPanel;
    [SerializeField]
    ConversionGamePanel difficultyPanel;
    [SerializeField]
    Button button;
    [SerializeField]
    KeyCode key;
    bool result;
    bool done;
    float timer;

    private void Awake()
    {
       
    }

    public void ShowResult()
    {
        panel.ShowUI();
        result = true;
        timer = 5f;
    }
    public void EndSession() { 
        MainPanel.HideUI();
        difficultyPanel.ShowUI();
        quiz.EndSession();
        panel.HideUI();
    }
    
    private void Update()
    {
        if (result)
        {
            if (Input.GetKeyDown(key))
            {

                button.onClick.Invoke();
            }

            scoreText.text ="Score: "+ quiz.returnScoreCount().ToString()+"/"+quiz.returnQuestionCount().ToString();
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                result = false;
                EndSession();
            }
        }
        autoContinueText.text = "auto continue in..." + timer.ToString("0");

    }

}
