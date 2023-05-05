using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Invector.vCharacterController;
using UnityEngine.Events;
using System;
public class DecimalToOcta : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Value;
    [SerializeField]
    private TMP_InputField input;
    [SerializeField]
    private TextMeshProUGUI timerValue;
    private int random;
    string test;
    string toarray;
    private vThirdPersonInput _character;
    private void OnEnable()
    {
        _character = GameObject.FindGameObjectWithTag("Player").GetComponent<vThirdPersonInput>();
    }
    public void StarGame()
    {    random = UnityEngine.Random.Range(1, 255);
            
       toarray=""+  random.ToString().ToCharArray();
        test = " " + random.ToString().Length + "to Char" + toarray;
        Value.text = Convert.ToString(random, 8);
        Debug.Log("Random Value: "+random);
        Debug.Log("Converted Octa Value: " + Value.text);
        Debug.Log(test);
    }

    #region Events
    [SerializeField]
    private UnityEvent OnCompleteTask;
    [SerializeField]
    private UnityEvent OnClosedTask;
    [SerializeField]
    private UnityEvent OnTimesUpTask;

    #endregion
}
