using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OwnCode;
using TMPro;

public class Room1AnswerScript : MonoBehaviour
{
    public Room1ComputerScript script;
    private string text;
    private void Update()
    {
        text = this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }
    public void Checker()
    {
        if(text == script.CurrentCorrectAnswer)
        {
            script.Correct();

        }
        else
        {
            script.Wrong();
        }

    }
}
