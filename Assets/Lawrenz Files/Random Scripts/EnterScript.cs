using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnterScript : MonoBehaviour
{
    [SerializeField]
    QuizManager manager;
    KeyCode key = KeyCode.Return;
    [SerializeField]
    TMP_InputField inputField;

 
    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
          SubmitAnswer();
            if(inputField != null)
            {
                inputField.Select();
                inputField.ActivateInputField();

            }
         

        }
        
    }
    public void SubmitAnswer(){
            manager.CheckAnswer(inputField.text);
    }
}
