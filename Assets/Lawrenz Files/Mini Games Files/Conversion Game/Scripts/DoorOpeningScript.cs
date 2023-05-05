using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
public class DoorOpeningScript : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    Transform leftDoor;
    [SerializeField]
    Transform rightDoor;
    [SerializeField]
    bool doorOpen;
    [SerializeField]
    bool doorClose;
    [SerializeField]
    float slideAmount = 1.51f;
    [SerializeField]
    float Speed= 1;
    [SerializeField]
    bool reverse;
    public bool doorisOpen;
    Vector3 left = Vector3.left;
    Vector3 right = Vector3.right;
    [Header("Sound Effects Door")]
    [SerializeField]
    AudioSource openFx;
    [SerializeField]
    AudioSource closeFx;
    [Header("HaveFx?")]
    [SerializeField]
    bool HaveFx;
    [SerializeField]
    ParticleSystem[] effects;

    Vector3 leftStartPosition;
   Vector3 rightStartPosition;
    [SerializeField]
    UnityEvent CallTimer;
    public bool waitforCollision;
    private void OnValidate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   float timer = 3f;
            if (waitforCollision)
            {
                
                CloseDoorCaller();

            }
        }
    }


    private void Start()
    {
        leftStartPosition = leftDoor.position;
        rightStartPosition = rightDoor.position;
   
    }
   
    IEnumerator OpenDoor()
    {
        Vector3 leftEndPosition = leftStartPosition + slideAmount * left;
        Vector3 rightEndPosition = leftStartPosition + slideAmount* right;
        leftStartPosition = leftDoor.position;
        rightStartPosition = rightDoor.position;
        float time = 0;

       while(time < 1)
        {
            leftDoor.position = Vector3.Lerp(leftStartPosition, leftEndPosition, time);
            rightDoor.position = Vector3.Lerp(rightStartPosition, rightEndPosition, time);
      
            yield return doorOpen = false;
            time += Time.deltaTime * Speed;
            
        }

    }

    IEnumerator CloseDoor()
    {
        Vector3 leftEndPosition = leftStartPosition;
        Vector3 leftStartPosition2 = leftDoor.position;
        Vector3 rightEndPosition = rightStartPosition;
        Vector3 rightStartPosition2 = rightDoor.position;
        float time = 0;
        waitforCollision = false;
        while (time < 1)
        {
            leftDoor.position = Vector3.Lerp(leftStartPosition2, leftEndPosition, time);
            rightDoor.position = Vector3.Lerp(rightStartPosition2, leftEndPosition, time);
         
            yield return doorClose = false;
            time += Time.deltaTime * Speed;
        }


    }

    private void Update()
    {
        if (doorOpen)
        {
            openFx.Play();
            StartCoroutine(OpenDoor());
            waitforCollision = true;
            if (HaveFx)
            {


            }
        }
 
        if (doorClose)
        {
          
            closeFx.Play();
            StartCoroutine(CloseDoor());
        }
        
        if (reverse)
        {
            left = Vector3.right;
            right = Vector3.left;
        }
    }
     
    public void RelockDoor()
    {

        CloseDoorCaller();
    }

    public void OpenDoorCaller() { doorOpen = true; doorisOpen = true; }

    public void CloseDoorCaller() { doorClose = true;
        doorisOpen = false;
    }
}
