using TMPro;
using UnityEngine;
using OwnCode;

public class Room2AnswerScript : MonoBehaviour
{
    public ComputerStarterRoom2 script;
    public string text;
    private void Update()
    {
        text = this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }
    public void Checker()
    {
        if (text == script.CurrentAnswer)
        {
            script.RightAnswer();
            Debug.Log(" Correct Called at: "+this.gameObject.name);

        }
        else
        {
            script.WrongAnswer();
            Debug.Log(" Wrong Called at: " + this.gameObject.name);
        }

    }

}
