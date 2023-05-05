using UnityEngine;
using System.Collections.Generic;
using TMPro;
using PixelCrushers.DialogueSystem.SequencerCommands;
using PixelCrushers.DialogueSystem;
using System.Collections;
using UnityEngine.Events;

namespace OwnCode
{
    public class ComputerStarterRoom2 : SequencerCommand {
        [SerializeField]
        Room2GameManager manager;
        [SerializeField]
        PanelManager panel;
        [SerializeField]
        public int QuestionIndex;
        [SerializeField]
        private TextMeshProUGUI textQuestionValue;
        [SerializeField]
        List<Room2Questions> Questions;
        GlobalInputLock _lock;
        MouseSettingsInput _input;
        [SerializeField]
        List<GameObject> Choices;

        [Header("CANVAS Correct/Wrong")]
        [SerializeField]
        GameObject CorrectPanel;
        [SerializeField]
        GameObject WrongPanel;
        Usable trigger;
        public bool inSession;
        [SerializeField]
        PanelManager objectivePanel;
        float counter;
        public string CurrentAnswer;
        List<int> randomInts = new List<int>();

        [SerializeField]
        Camera _camera;

        public UnityEvent OnCorrect;
        public UnityEvent OnWrong;
        private void Start()
        {
            _input = GameObject.Find("GameManager").GetComponent<MouseSettingsInput>();
            _lock = GameObject.Find("GameManager").GetComponent<GlobalInputLock>();
            manager = GameObject.Find("GameManager").GetComponent<Room2GameManager>();
            trigger = transform.GetChild(0).GetComponent<Usable>();
            for (int j = 0; j <= Choices.Count - 1; j++)
            {
                randomInts.Add(j);
            }
            randomInts.Shuffle();
        }
        public void SetChoices()
        {
            textQuestionValue.text = Questions[QuestionIndex].question;

            for (int i = 0; i < Choices.Count; i++)
            {

                Choices[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Questions[QuestionIndex].answers[randomInts[i]];

            }
        }

        private void Update()
        {
            CurrentAnswer = Questions[QuestionIndex].correctAnswer;

            if (inSession)
            {
                trigger.enabled = false;
            }
        }
        IEnumerator cameraFocus()
        {
            yield return new WaitForSeconds(1.2f);
            _camera.depth = 1;
        }
        public void FlashScreen()
        {
            _lock.DisableAllPlayerInputMovement();
            inSession = true;
            panel.OpenPanel();
            _input.EnableMouseUI();
            objectivePanel.HidePanel();
            StartCoroutine(cameraFocus());

        }

        public void WrongAnswer()
        {
            sequencer.m_delayTimeLeft = 0;
            inSession = false;
            WrongPanel.transform.GetComponent<PanelManager>().OpenPanel();
            _lock.EnableAllMovements();
            _input.DiableMouseUI();
            objectivePanel.OpenPanel();
            DialogueManager.ShowAlert("Wrong Answer!");
            Debug.Log("Wrong Called at: " + this.gameObject.name);
            _camera.depth = -1;
            if (OnWrong != null)
            {
                OnWrong.Invoke();
            }
        }
        public void RightAnswer()
        {
            sequencer.m_delayTimeLeft = 0;
            inSession = false;
            CorrectPanel.transform.GetComponent<PanelManager>().OpenPanel();
            _lock.EnableAllMovements();
            _input.DiableMouseUI();
            objectivePanel.OpenPanel();
            DialogueManager.ShowAlert("Correct Answer!");
            Debug.Log("Correct Called at: " + this.gameObject.name);
            _camera.depth = -1;
            if(OnCorrect != null)
            {
                OnCorrect.Invoke();
            }
        }
    }

  

}

