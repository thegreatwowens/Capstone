using UnityEngine;
using PixelCrushers.DialogueSystem.SequencerCommands;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;
using DiZTools_AchievementsSystem;

namespace OwnCode
{
    public class Room1ComputerScript : SequencerCommandDelay
    {
        [Space]
        public bool session;
        [SerializeField]
        TextMeshProUGUI questionText;
        [SerializeField]
        PanelManager panel;
        GlobalInputLock inputplayer;
        Usable trigger;
        [SerializeField]
        List<Room1Questions> GivenQuestions;
        [SerializeField]
        List<Room1Questions> questions;
        [SerializeField]
        List<GameObject> choices;
        List<int> randomInts = new List<int>();

        public UnityEvent OnCorrect;
        public UnityEvent OnWrong;
        public UnityEvent TaskCompleted;
        int CurrentQuestion { get; set; }
        public string CurrentCorrectAnswer;
        public int AnsweredCount { get; set; }
        int CurrentValues;
        private void Awake()
        {
            inputplayer = GetComponent<GlobalInputLock>();
            trigger = GetComponent<Usable>();
        }
        public void SessionStart()
        {
            session = true;
            trigger.enabled = false;
            inputplayer.DisableAllPlayerInputMovement();
            panel.OpenPanel();
            QuestionPopulator();

        }

        private void GenerateValue()
        {
            CurrentValues = Random.Range(0, questions.Count);
        }
        public void EndSession()
        {
            sequencer.m_delayTimeLeft = 0f;
            inputplayer.EnableAllMovements();
            panel.HidePanel();
            session = false;
            trigger.enabled = true;
            AnsweredCount = 0;

        }
        public void ClosedSession()
        {
            sequencer.m_delayTimeLeft = 0f;
            inputplayer.EnableAllMovements();
            panel.HidePanel();
            session = false;
            trigger.enabled = true;
            questions.Clear();
            AnsweredCount = 0;
        }

        private void QuestionPopulator()
        {
            for (int i = 0; i <= GivenQuestions.Count-1; i++)
            {
                randomInts.Add(i);
            }
            randomInts.Shuffle();

            for (int i = 0; i <=GivenQuestions.Count-1; i++)
            {

                questions.Add(GivenQuestions[randomInts[i]]);
            }

     
        }
        
        public void UIValuesUpdate()
        {
            if(AnsweredCount == 5)
            {
                EndSession();
                AchievementsManager.Instance.AchievementsData.UpdateAchievementData(AchievementsDataGlossary.WindowsExpert,+5);
                if (TaskCompleted != null)
                {
                    TaskCompleted.Invoke();

                }
            }
            GenerateValue();
            questionText.text = questions[CurrentValues].question;
            CurrentCorrectAnswer = questions[CurrentValues].correctAnswer;

                for (int i = 0; i < choices.Count; i++)
                {
                    choices[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questions[CurrentValues].answers[i];

                }

            



        }

        public void Correct()
        {
            questions.RemoveAt(CurrentValues);
            AnsweredCount++;
            DialogueManager.ShowAlert("Correct Answer!");
            // GenerateValue();
            if (OnCorrect != null)
            {
                OnCorrect.Invoke();
            }
            
            Debug.Log(this.gameObject.GetComponent<Room1ComputerScript>().name.ToString()+" Correct is Called;");
            UIValuesUpdate();
        }
        public void Wrong() {
            DialogueManager.ShowAlert("Wrong Answer!");
            if (OnWrong != null)
            {
                OnWrong.Invoke();
            }
            Debug.Log(this.gameObject.GetComponent<Room1ComputerScript>().name.ToString()+" Wrong is Called;");
            EndSession();
        }

    }
   

}
