using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameConversionRandomizer : MonoBehaviour
{
    private int selectorvalue;
    [SerializeField]
    private BinarytoDecimal binarytoDecimal;

    [SerializeField]
    private UnityEvent BinaryProblem;
    public void Start()
    {
       
    }
    public void RandomValue()
    {
        if (!binarytoDecimal.sessions)
        {
            selectorvalue = Random.Range(1,1);

            switch (selectorvalue)
            {
                case 1:
                    BinaryProblem.Invoke();

                    break;

                default:
                    return;
            }
        }
        else return;
        
    }
    void Update()
    {
       
    }

}
