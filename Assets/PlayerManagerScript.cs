using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
using PixelCrushers;

public class PlayerManagerScript : MonoBehaviour
{

    public bool fixedSpawnPoint;
    string spawnTag = "SpawnPoint";
    public static PlayerManagerScript playerManager;
    [SerializeField]
    private List<GameObject> PlayerGender;
    private int genderSet;

    GameObject existingPlayer;
  public Vector3 _savedPlayerPosition;
 void Start() => genderSet = PlayerPrefs.GetInt("selectedCharacters");
    public void SpawnPlayer()
    {
        if(existingPlayer !=null){
            return;
        }else{
            Instantiate(PlayerGender[genderSet],_savedPlayerPosition,Quaternion.identity);
        }
    }
    Scene currentScene;
    Scene previousScene;
    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
   
    IEnumerator SpawnDelayFrame(){
        
        yield return new WaitForSecondsRealtime(.001f);
         SpawnPlayer();
        
        }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        existingPlayer = GameObject.FindGameObjectWithTag("Player");
        SaveSystem.saveCurrentScene = true;
        currentScene = SceneManager.GetActiveScene();
         if(fixedSpawnPoint){
            _savedPlayerPosition = GameObject.FindGameObjectWithTag(spawnTag).transform.position;
         }else
            _savedPlayerPosition = SaveSystem.playerSpawnpoint.transform.position;
        StartCoroutine(SpawnDelayFrame());
    }


}
