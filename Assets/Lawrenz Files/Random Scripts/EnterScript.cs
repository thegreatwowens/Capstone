using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnterScript : MonoBehaviour
{
    [SerializeField]
    Button button;
    [SerializeField]
    KeyCode key;
    [SerializeField]
    TMP_InputField inputField;

    private void Start()
    {

            button = GetComponent<Button>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            button.onClick.Invoke();
            if(inputField != null)
            {
                inputField.Select();
                inputField.ActivateInputField();

            }
         

        }
        
    }
}
