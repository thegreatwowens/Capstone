using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestionIndex",menuName ="Generate New QuestionIndex")]
public class QuestionGenerated : ScriptableObject
{
    [TextArea(5, 10)]
    public string question;
    [TextArea(2,2)]
    public string answer;
}
