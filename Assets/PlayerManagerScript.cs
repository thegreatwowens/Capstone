using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManagerScript : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> PlayerGender;

    private int genderSet;
    public GameObject currentPlayerObject;
    Transform SpawnLocation;

    public void FirstSpawn()
    {
        
        genderSet = PlayerPrefs.GetInt("selectedCharacters");

        Instantiate(PlayerGender[genderSet],SpawnLocation);
    }
    Scene currentScene;
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
            currentScene = SceneManager.GetActiveScene();
            if(currentScene == SceneManager.GetSceneByName("City")){
                SpawnLocation = GameObject.FindGameObjectWithTag("SpawnPointFromStart").transform;
            }

            


    }



    void Update()
    {
        currentPlayerObject = GameObject.FindGameObjectWithTag("Player");
    }
}
