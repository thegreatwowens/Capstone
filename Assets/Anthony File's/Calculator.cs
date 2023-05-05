using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PixelCrushers.DialogueSystem;


public class Calculator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI totalText;
 
    public Button _button;
    [SerializeField]
    BinarytoDecimal binaryToDecimal;


    public int Total { get; set; }

    private void Awake()
    {
    }
    private void Start()
    {
        
        var allToggles = FindObjectsOfType<BitToggle>();
        foreach (var toggle in allToggles)
            toggle.OnToggleChanged += Toggle_OnToggleChanged;
        _button.onClick.AddListener(ButtonOnlickUI);


    }

    private void Toggle_OnToggleChanged(int number, bool enabled)
    {
        if (enabled)
            Total += number;
        else
            Total -= number;

        totalText.text = Total.ToString();
    }
    private void Update()
    {

    }

    void ButtonOnlickUI()
    {
        binaryToDecimal.SendMessage("EndSesssion");

    }
}

