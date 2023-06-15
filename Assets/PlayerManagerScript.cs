using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class PlayerManagerScript : MonoBehaviour
{

    public static PlayerManagerScript playerManager;
    [SerializeField]
    private List<GameObject> PlayerGender;

    private int genderSet;
    public GameObject currentPlayerObject;
    Vector3 SpawnLocation;
    
    public void FirstSpawn()
    {
        
        genderSet = PlayerPrefs.GetInt("selectedCharacters");

        Instantiate(PlayerGender[genderSet],SpawnLocation,new Quaternion());
    }
    Scene currentScene;
    Scene previousScene;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
            
            currentScene = SceneManager.GetActiveScene();
            
            if(currentScene == SceneManager.GetSceneByName("City")){
                  DialogueManager.ShowAlert("Detroit City");
                SpawnLocation = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
                FirstSpawn();
                
            }

            


    }

 

    void Update()
    {

        currentPlayerObject = GameObject.FindGameObjectWithTag("Player");
    }
}
