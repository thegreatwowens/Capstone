using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OwnCode.Choi;
using OwnCode;
public class PosterUICaller : MonoBehaviour
{
    [SerializeField]
    UIPoster poster;

   
    void Update()
    {
        poster = GameObject.FindObjectOfType<UIPoster>();
    }
    public void Clicked()
    {
        poster.UIHide();
    }
}
