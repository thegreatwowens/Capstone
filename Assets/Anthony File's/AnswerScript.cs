using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizPicManager quizPicManager;

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correct");
            quizPicManager.Correct();
        }
        else
        {
            Debug.Log("False");
            quizPicManager.Wrong();
        }
    }
}
