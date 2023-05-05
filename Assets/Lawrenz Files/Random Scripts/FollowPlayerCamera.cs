using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Camera _sequenceCamera;
    private GameObject _player;

    private void Awake()
    {
        _sequenceCamera = this.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if(_player)
        {
            _sequenceCamera.transform.SetParent(_player.transform);
        } 
    }
}
