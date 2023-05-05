using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DoorNotification : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textNotification;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite[] spritelist;
    private CanvasGroup _canvas;
    private bool ShowUI;
    private bool HideUI;
    private bool closedWindow;
    private bool timesup;
    private bool completed;
    private bool equipped;
    private bool doorAlreadyOpened;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();



    }
    #region Methods
    public IEnumerator StartShow()
    {
        ShowUINotification();
        yield return new WaitForSeconds(3);
        HideUINotifcation();
        closedWindow = false;
        timesup = false;
        completed = false;
        equipped = false;
        doorAlreadyOpened = false;
    }
    public void ShowUINotification() { ShowUI = true; }

    public  void OnComplete()
    {
        completed = true;
        StartCoroutine(StartShow());

    }
    
    public void TaskTimeIsUp()
    {
        timesup = true;
        StartCoroutine(StartShow());

    }
    public void ClosedWindow()
    {
        closedWindow = true;
        StartCoroutine(StartShow());
    }
    public void HideUINotifcation() { HideUI = true; }
    public void EquippedNotification() { equipped = true; StartCoroutine(StartShow()); }
    public void DoorIsOpened() { doorAlreadyOpened = true; StartCoroutine(StartShow()); }
#endregion
    private void Update()
    {
        if (ShowUI)
        {
            _canvas.alpha += Time.deltaTime;
            _canvas.interactable = true;
            _canvas.blocksRaycasts = true;
            if (_canvas.alpha == 1)
            {
                _canvas.alpha = 1;
                ShowUI= false;
            }
        }

        if (HideUI)
        {
            _canvas.alpha -= Time.deltaTime;
            _canvas.interactable = false;
            _canvas.blocksRaycasts = false;
            if (_canvas.alpha <= 0)
            {
                _canvas.alpha = 0;
                HideUI = false;
            }
        }

        if (timesup)
        {
            _textNotification.text = "ERROR 402:Session Expired.";
            _textNotification.color = Color.white;
            image.color = Color.red;
            image.sprite = spritelist[0];
            
        }
        if (closedWindow)
        {
            _textNotification.text = "ERROR 403:Canceled Session.";
            _textNotification.color = Color.yellow;
            image.color = Color.yellow;
            image.sprite = spritelist[0];
        }
        if (completed)
        {
            _textNotification.text = "Congratulations! you earn +1 Binary Skill!";
            _textNotification.color = Color.green;
            image.sprite = spritelist[1];
            image.color = Color.green;
        }

        if (equipped) {

            _textNotification.text = "Can't Access with Equipped Weapon.";
            _textNotification.color = Color.white;
            image.color = Color.red;
            image.sprite = spritelist[2];
        }

        if (doorAlreadyOpened)
        {
            _textNotification.text = " Door is already opened!";
            _textNotification.color = Color.yellow;
            image.color = Color.yellow;
            image.sprite = spritelist[0];

        }
            

    }


}
