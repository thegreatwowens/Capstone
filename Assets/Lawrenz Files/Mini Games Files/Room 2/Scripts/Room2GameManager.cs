using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;
namespace OwnCode{
    public class Room2GameManager : MonoBehaviour {

        public TextMeshProUGUI timerUI;
        [SerializeField]
        private float settime = 0;
        [SerializeField]
        List<GameObject> Computers;
        [SerializeField]
        List<int> randomInts = new List<int>();
        bool startSession;
        private float timeMinutes;
        private float timeSeconds;
        [SerializeField]
        TextMeshProUGUI Computer1text;
        [SerializeField]
        TextMeshProUGUI Computer2text;
        [SerializeField]
        TextMeshProUGUI Computer3text;
        [SerializeField]
        TextMeshProUGUI Computer4text;
        [SerializeField]
        TextMeshProUGUI Computer5text;
        int ComputerCounts;
        public UnityEvent Completed;
        public UnityEvent OnCompleting;

        private void Start()
        {
            for (int j = 0; j <= Computers.Count - 1; j++)
            {
                randomInts.Add(j);
            }
            randomInts.Shuffle();
            Computer1text.text = "Computer#1 0/1";
            Computer2text.text = "Computer#2 0/1";
            Computer3text.text = "Computer#3 0/1";
            Computer4text.text = "Computer#4 0/1";
            Computer5text.text = "Computer#5 0/1";
            ComputerCounts = 0;
         
        }
        private void Update()
        {

       

            if (startSession)
            {
                settime -= Time.deltaTime;
                timeMinutes = Mathf.FloorToInt(settime / 60);
                timeSeconds = Mathf.FloorToInt(settime % 60);
            }
            timerUI.text = "Timer: " + timeMinutes.ToString("0") + ":" + timeSeconds.ToString("00").ToString() + "s";

            if(ComputerCounts >= 5 || settime <= 0)
            {
                if(OnCompleting != null)
                {
                    OnCompleting.Invoke();
                }
                StartCoroutine(ReturntoMap());
                if (settime >= 0)
                {
                    timerUI.text = "You will be return after: " + timeSeconds.ToString() + "s";
                }

            }
        }
     
        public void GameStart()
        {
            IntializedData();
            TimerStart();
        }
        IEnumerator ReturntoMap()
        {
            settime = 5;
            yield return new WaitForSeconds(5);
            if (Completed != null)
            {
                Completed.Invoke();
            }
        }

        private void IntializedData()
        {


            for (int i = 0; i < Computers.Count; i++)
            {

                Computers[i].transform.GetComponent<ComputerStarterRoom2>().QuestionIndex = randomInts[i];
                Computers[i].transform.GetComponent<ComputerStarterRoom2>().SetChoices();
            }



        }

        private void TimerStart()
        {
            startSession = true;
        }

        public  void UpdateUIVariables(int value)
        {
            switch (value)
            {
                case 1:
                    {
                        Computer1text.text = "Computer #1 Completed";
                        ComputerCounts++;
                    }
                    
                    break;
                case 2:
                    {
                        Computer2text.text = "Computer #2 Completed";
                        ComputerCounts++;
                    }
                   
                    break;

                case 3:
                    {
                        Computer3text.text = "Computer #3 Completed";
                        ComputerCounts++;
                    }
                    
                    break;

                case 4:
                    {
                        Computer4text.text = "Computer #4 Completed";
                        ComputerCounts++;
                    }
                   
                    break;

                case 5:
                    {
                        Computer5text.text = "Computer #5 Completed";
                        ComputerCounts++;
                    }
                    
                    break;

                case 6:
                    {
                        Computer1text.text = "Computer #1 Failed";
                        ComputerCounts++;
                    }
                  
                    break;
                case 7:
                    {
                        Computer2text.text = "Computer #2 Failed";
                        ComputerCounts++;
                    }
                    
                    break;
                case 8:
                    {
                        Computer3text.text = "Computer #3 Failed";
                        ComputerCounts++;
                    }
                    break;
                case 9:
                    {
                        Computer4text.text = "Computer #4 Failed";
                        ComputerCounts++;
                    }
                  
                    break;
                case 10:
                    {
                        Computer5text.text = "Computer #5 Failed";
                        ComputerCounts++;
                    }
                  
                    break;

                default:
                    break;


            }

        }
        
        public void UnPause()
        {
            Time.timeScale = 1;
            
        }
        public void Pause()
        {
            Time.timeScale =0 ;
        }
    }

}

