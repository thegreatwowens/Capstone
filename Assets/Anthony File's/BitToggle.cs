using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BitToggle : MonoBehaviour
{
    [SerializeField]
    private int number;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TextMeshProUGUI _BinaryEquivalent;
    [SerializeField]
    private TextMeshProUGUI _BinaryEquivalentUI;
    public TextMeshProUGUI Total;
    public event Action<int, bool> OnToggleChanged = delegate { };

    private void Awake()
    {
        GetComponent<Toggle>().isOn = false;
        GetComponent<Toggle>().onValueChanged.AddListener(HandleToggleChanged);
    }

    private void HandleToggleChanged(bool enabled)
    {
        if(enabled == true)
        {
            OnToggleChanged(number, enabled);
            _BinaryEquivalent.text = 1.ToString();
            _BinaryEquivalentUI.text = _BinaryEquivalent.text;
        }
        else
        {
            OnToggleChanged(number, false);
            _BinaryEquivalent.text = 0.ToString();
            _BinaryEquivalentUI.text = _BinaryEquivalent.text;
        }
          

    }

    private void OnValidate()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();

        _text.text = number.ToString();
        _BinaryEquivalent.text = 0.ToString();


        this.gameObject.name = "Toggle " + number;
    }
    private void Update()
    {
        
    }
}
