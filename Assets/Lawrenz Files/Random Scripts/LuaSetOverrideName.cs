using UnityEngine;
using PixelCrushers.DialogueSystem;

public class LuaSetOverrideName : MonoBehaviour
{
    string name;


    private void Update()
    {
        name = PlayerPrefs.GetString("PlayerName");
        DialogueLua.SetActorField("Player", "Display Name", name);
    }
}