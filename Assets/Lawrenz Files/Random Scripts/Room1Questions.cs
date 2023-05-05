using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OwnCode{
    [CreateAssetMenu(fileName = "QuestionIndex", menuName = "Room1 Quiz")]
    public class Room1Questions : ScriptableObject
    {
        [TextArea(5,20)]
        public string question;
        public string correctAnswer;
        public List<string> answers;

        public int capacity { get; set; }
        
        public int capacityReturn()
        {
           capacity = answers.Capacity - 1;

            return capacity;
        }

        
    } 

   
}

