using UnityEngine;
using DiZTools_AchievementsSystem;

public class AchievementExternalSender : MonoBehaviour
{
    AchievementTrigger achievementTrigger;
    QuizManager quizManager;
    #region variables
    // scoreQuiz
    int scoreCount;
    string difficulty;
    #endregion

    private void Awake()
    {
        quizManager = GetComponent<QuizManager>();
    }
    private void Update()
    {
        achievementTrigger = GameObject.FindObjectOfType<AchievementTrigger>();
        #region QuizManager Settings 
        difficulty = quizManager.returnDifficulty();
        scoreCount = quizManager.returnScoreCount();
        if (quizManager != null) {

            if (!quizManager.session)
            {
                Savescores();
            }
            if (scoreCount == 10)
            {
                if (difficulty == "easy")
                    achievementTrigger.EasyQuizCaller();
            }
            if (scoreCount == 20)
            {
                if (difficulty == "normal")
                    achievementTrigger.NormalQuizCaller();
            }
            if (scoreCount == 30)
            {
                if (difficulty == "hard")
                    achievementTrigger.HardQuizCaller();
            }
        }
        #endregion
    }


    private void Savescores()
    {
        PlayerPrefs.SetInt("scoreCount",scoreCount);
        PlayerPrefs.Save();
        if(difficulty == "easy")
            PlayerPrefs.SetInt("easyScore", scoreCount);
        if (difficulty == "normal")
            PlayerPrefs.SetInt("normalScore", scoreCount);
        if (difficulty == "hard")
            PlayerPrefs.SetInt("hardScore", scoreCount);

    }
    private void ResetScores()
    {
            PlayerPrefs.DeleteKey("scoreCount");
        PlayerPrefs.DeleteKey("easyScore");
        PlayerPrefs.DeleteKey("normalScore");
        PlayerPrefs.DeleteKey("hardScore");

    }
    

    public void NetworkingMaster()
    {
        achievementTrigger.NetworkAchievement();
    }
}
