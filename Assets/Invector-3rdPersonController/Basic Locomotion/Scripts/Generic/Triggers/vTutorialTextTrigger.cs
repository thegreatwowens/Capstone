using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Invector
{
    public class vTutorialTextTrigger : MonoBehaviour
    {
        enum TypeofNotification
        {
            none,
            Portal,
            NPC,
            Trivias,
            ComputerInteraction

        }
        [TextAreaAttribute(5, 3000), Multiline]
        public string text;
        public TextMeshProUGUI _textUI;
        public GameObject panel;
        public GameObject portalPanel;
        public GameObject triviaPanel;
        public GameObject nPCsPanel;
        [SerializeField]
        TypeofNotification type;

        private void Update()
        {
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                EnableTutorialPanel();
            }
        }

        public void EnableTutorialPanel()
        {
            if (type == TypeofNotification.Portal)
            {
                portalPanel.SetActive(true);
                _textUI.gameObject.SetActive(true);
                _textUI.text = text;
            }
           // Canvaspanel.SetActive(true);
           // _textUI.gameObject.SetActive(true);
            //_textUI.text = text;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                DisableTutorialPanel();
            }
        }

        public void DisableTutorialPanel()
        {
            if (type == TypeofNotification.Portal)
            {
                portalPanel.SetActive(false);
                _textUI.gameObject.SetActive(false) ;
                _textUI.text = " ";
            }
           // Canvaspanel.SetActive(false);
           //_textUI.gameObject.SetActive(false);
            //_textUI.text = " ";
        }
    }
}