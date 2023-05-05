using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEmptyWarningScript : MonoBehaviour
{
    [SerializeField]
    GameObject text;


    private void Awake()
    {
        text.SetActive(false);
    }
    public IEnumerator Notify()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(1);
        text.SetActive(false);
    }

    public void NotifyText()
    {
        StartCoroutine(Notify());

    }
}
