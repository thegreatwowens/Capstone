using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionScript : MonoBehaviour
{
    [SerializeField]
    Animator animator;


     bool picked;
     bool unpicked;
    [Header("Check if Male/Uncheck for Female")]
    [SerializeField]
     bool gender;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (gender)
        {
            animator.SetBool("MenIdle", true);

        }
        else { animator.SetBool("FemaleIdle", true); }
    }

    // Update is called once per frame
    void Update()
    {
        if (picked)
        {
        
            animator.SetBool("FemaleStanding", true);
      
        }
        
        if(unpicked)
        {
            animator.SetBool("FemaleStanding", false);

        }
        
    }
    public void PlayPickAnimation()
    {
        picked = true;
    }
    public void PlayUnpickedAnimation()
    {
        unpicked = true;

    }
}
