using System.Collections.Generic;
using System.Collections;
using UnityEngine;


[System.Serializable]
public class QuizItem
{
    [TextArea(5, 10)]
    public string question;
   public List<string> possibleAnswers;

}