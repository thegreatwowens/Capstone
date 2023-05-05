using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    Transform SpawnPoint;
    [SerializeField]
    List<GameObject> PlayerObject;
    int player;
    GameObject _player;
    private void Start()
    {
        returnGender();
    }
    private void Awake()
    {
    }
    public GameObject ReturnGameObject()
    {
        GameObject _Player = PlayerObject[returnGender()];
        return _Player ;
    }
    private void Update()
    {
        returnGender();
    }

   private int returnGender()
    {
       int value = PlayerPrefs.GetInt("selectedCharacters", player);
        return value;
    }
 
}
