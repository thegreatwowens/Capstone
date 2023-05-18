using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundObject", menuName = "ScriptableSound/SoundFx")]
public class ScriptableExternalSound : ScriptableObject
{
    SoundManager manager;

    public enum Typeofsound
    {
        Select,
        UISound,
        BackgroundMusic,
        SoundFx

    }
    [Header("Values")]
    public string audioName;
    [SerializeField]
    Typeofsound type;
    private void Update()
    {

    }
}
