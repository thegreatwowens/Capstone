using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Invector.vCharacterController;
using PixelCrushers.DialogueSystem.SequencerCommands;
using UnityEngine.Events;
using PixelCrushers.DialogueSystem;
using OwnCode;

public class BinarytoDecimal : SequencerCommandDelay
{
    [SerializeField]
    PanelManager gamepanel;
    [SerializeField]
    private TextMeshProUGUI TargetValue;
    [SerializeField]
    private Calculator calculator;
    [SerializeField]
    private TextMeshProUGUI UItimer;
    [SerializeField]
    DoorNotification door;
    [SerializeField]
    private DialogueSystemTrigger systemTrigger;
    [SerializeField]
    private float timeLeft;
    [SerializeField]
    private vThirdPersonInput _character;
    public UnityEngine.UI.Toggle toggle1;
    public UnityEngine.UI.Toggle toggle2;
    public UnityEngine.UI.Toggle toggle3;
    public UnityEngine.UI.Toggle toggle4;
    public UnityEngine.UI.Toggle toggle5;
    public UnityEngine.UI.Toggle toggle6;
    public UnityEngine.UI.Toggle toggle7;
    public UnityEngine.UI.Toggle toggle8;
    #region private variables
    private int targetNumber = 0;
    private float timer = 3f;
    public bool sessions;
    #endregion
    #region Events
    [SerializeField]
    private UnityEvent OnCompleteTask;
    [SerializeField]
    private UnityEvent OnClosedTask;
    [SerializeField]
    private UnityEvent OnTimeIsUpTask;
    [SerializeField]
    private UnityEvent WeaponIsEquipped;
    #endregion
    int currentValue;


    private new void Start()
    {
    

    }
    private void OnEnable()
    {

    }
    private void ChooseNumber()
    {
        calculator._button.interactable = true;
        timer = 3;
        timeLeft = 15f;
        targetNumber = UnityEngine.Random.Range(0, 255);
        TargetValue.text = targetNumber.ToString();
        toggle1.isOn = false;
        toggle2.isOn = false;
        toggle3.isOn = false;
        toggle4.isOn = false;
        toggle5.isOn = false;
        toggle6.isOn = false;
        toggle7.isOn = false;
        toggle8.isOn = false;
        toggle1.interactable = true;
        toggle3.interactable = true;
        toggle4.interactable = true;
        toggle5.interactable = true;
        toggle6.interactable = true;
        toggle7.interactable = true;
        toggle8.interactable = true;

    }
    private new void Update()
    {
        _character = GameObject.FindObjectOfType<vThirdPersonInput>();

        if (calculator.Total == targetNumber)
        {
            timer -= DialogueTime.deltaTime;
            calculator._button.interactable = false;
            toggle1.interactable = false;
            toggle3.interactable = false;
            toggle4.interactable = false;
            toggle5.interactable = false;
            toggle6.interactable = false;
            toggle7.interactable = false;
            toggle8.interactable = false;
            if (timer < 2)
            {

                if (sessions && timer <= 0)
                {

                    CompleteTask();

                }
            }
            
            
        }
        if (sessions)
        {
            _character.SetLockCameraInput(true);
            if(calculator.Total != targetNumber) {
                timeLeft -= Time.deltaTime;
                UItimer.text = "Remaining time: " + ((int)timeLeft);
                if (timeLeft <= 0)
                {
                    FailedToComplete();
                }
            }
            
        }

    }
    public void startGame()
    {
        if (_character.cc.isStrafing)
        {
            door.EquippedNotification();
            return;
        }
        if (!_character.cc.isStrafing)
        {
            sessions = true;
            systemTrigger.OnUse();
            ChooseNumber();
            _character.SetLockUpdateMoveDirection(true);
            _character.SetLockCameraInput(true);
            _character.SetLockAllInput(true);
            gamepanel.OpenPanel();
        }

        currentValue = DialogueLua.GetVariable("BinaryScore").asInt;
        print ("before value:"+currentValue);


    }
    public void ExitGame()
    {
        sessions = false;
        sequencer.m_delayTimeLeft = 0;
        _character.UpdateCameraStates();
        _character.SetLockUpdateMoveDirection(false);
        _character.SetLockCameraInput(false);
        _character.SetLockAllInput(false);
        gamepanel.HidePanel();

        if (OnClosedTask != null)
        {
            OnClosedTask.Invoke();

        }

    }
    public void FailedToComplete()
    {
        sessions = false;
        sequencer.m_delayTimeLeft = 0;
        _character.UpdateCameraStates();
        _character.SetLockUpdateMoveDirection(false);
        _character.SetLockCameraInput(false);
        _character.SetLockAllInput(false);
        gamepanel.HidePanel();
        if (OnTimeIsUpTask != null)
        {
            OnTimeIsUpTask.Invoke();
        }
    
    }


    public void CompleteTask()
    {
        sessions = false;
        sequencer.m_delayTimeLeft = 0;
        _character.UpdateCameraStates();
        _character.SetLockUpdateMoveDirection(false);
        _character.SetLockCameraInput(false);
        _character.SetLockAllInput(false);

         //DialogueLua.SetVariable("BinaryScore",currentValue+=1);
         print("new value: "+ currentValue);
         if (OnCompleteTask != null)
        {
            OnCompleteTask.Invoke();
        }
        gamepanel.HidePanel();

    }
}
  


