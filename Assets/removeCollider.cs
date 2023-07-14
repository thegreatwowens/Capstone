using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeCollider : MonoBehaviour
{
   public BoxCollider collider;


    public void ColliderDisable(){
        collider.enabled = false;
    }
}
