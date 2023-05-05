using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OwnCode{

    [CreateAssetMenu(fileName = "Question", menuName = "Room2 Quiz")]
    public class Room2Questions : ScriptableObject
    {
        [TextArea(5,20)]
        public string question;
        public string correctAnswer;
        public List<string> answers;

        
    } 

   
}

