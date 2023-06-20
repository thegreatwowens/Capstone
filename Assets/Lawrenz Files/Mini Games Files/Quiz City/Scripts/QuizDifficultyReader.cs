using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizDifficultyReader : MonoBehaviour
{
    [SerializeField]
    QuizManager1 manager;
    public Quizdificulty difficulty;


    public void SetDifficulty(){
        manager.difficultySet(difficulty);
    }
    
}
