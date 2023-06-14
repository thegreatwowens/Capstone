using UnityEngine;
using DiZTools_AchievementsSystem;

public class AchievementExternalSender : MonoBehaviour
{
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
       
        #region QuizManager Settings 
            if(quizManager !=null)
        difficulty = quizManager.returnDifficulty();
         if(quizManager !=null)
        scoreCount = quizManager.returnScoreCount();
        if (quizManager != null) {

            if (!quizManager.session)
            {
                Savescores();
            }
            if (scoreCount == 10)
            {
                if (difficulty == "easy")
                        if(AchievementsManager.Instance !=null)
                    AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.EasyQuizComplete,true);
            }
            if (scoreCount == 20)
            {
                if (difficulty == "normal")
                               if(AchievementsManager.Instance !=null)
                     AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.NormalQuizComplete,true);

            }
            if (scoreCount == 30)
            {
                if (difficulty == "hard")
                               if(AchievementsManager.Instance !=null)
                     AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.HardQuizComplete,true);
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
            if(AchievementsManager.Instance !=null)
        AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.NetworkingExpert,+5);
    }
}
