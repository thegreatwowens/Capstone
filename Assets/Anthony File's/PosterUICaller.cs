using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OwnCode.Choi;
public class PosterUICaller : MonoBehaviour
{
    [SerializeField]
    UIPoster poster;


    
    private void Awake()
    {
        poster = GameObject.Find("Trivia").GetComponent<UIPoster>();

    }
    private void Start()
    {

    }
    public void Clicked()
    {
        poster.UIHide();
    }
}
