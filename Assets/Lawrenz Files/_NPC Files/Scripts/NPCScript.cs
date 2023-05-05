using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using System.IO;
public class NPCScript : MonoBehaviour
{
    enum TypeofWalk
    {
        None,
        NormalWalk,
        CatWalk,
        WigglingWalk,
        WalkNormal,
        WalkHappy,
    }
    enum TypeOfIdle
    {
        None,
        Sitting,
        Talking,
        TalkingOnThePhone
    }
    [SerializeField]
    private Animator animator;
    [Space]
    [SerializeField]
    private bool StayPosition;
    [Header("Moving Variable")]
    [SerializeField]
    TypeofWalk WalkType;
    [Space]
    [Header("Idle Positions")]
    [SerializeField]
    TypeOfIdle IdleTypes;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (StayPosition) 
        {
            switch (IdleTypes)
            {
                case (TypeOfIdle)1:
                    animator.SetInteger("IdleType", 1);
                    break;
                case (TypeOfIdle)2:
                    animator.SetInteger("IdleType", 2);
                    break;
                case (TypeOfIdle)3:
                    animator.SetInteger("IdleType", 3);
                    break;
                default:
                    animator.SetBool("Idle", true);
                    break;
            }

        }
        else 
        {

                switch (WalkType)
                {
                    case (TypeofWalk)1:
                        animator.SetInteger("Walktype", 1);
                        break;
                    case (TypeofWalk)2:
                        animator.SetInteger("Walktype", 2);
                        break;
                    case (TypeofWalk)3:
                        animator.SetInteger("Walktype", 3);
                        break;

                    case (TypeofWalk)4:
                        animator.SetInteger("Walktype", 4);
                        break;
                    case (TypeofWalk)5:
                        animator.SetInteger("Walktype", 5);
                    break;
                default:
                        animator.SetBool("Idle", true);
                        break;

                }  

        }
        // IDLE CODE

    }

}
