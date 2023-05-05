using UnityEngine;
using UnityEngine.Events;
namespace OwnCode
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PanelManager : MonoBehaviour
    {
        [HideInInspector]
       public  CanvasGroup Canvaspanel;
        [Range(1,5)]
        public float fadeInSpeed = 1;
        public UnityEvent OnFinishedOpened;
        [Range(1, 5)]
        public float fadeOutSpeed = 1;
        public UnityEvent OnFinishedFade;
        bool openPanel;
        bool closePanel;


        private void Awake()
        {
            Canvaspanel = this.gameObject.GetComponent<CanvasGroup>();
        }
        private void Start()
        {
            Canvaspanel.alpha = 0;
            Canvaspanel.interactable = false;
            Canvaspanel.blocksRaycasts = false;
        }
       public void OpenPanel()
        {
            openPanel = true;
        }
        public void HidePanel()
        {
            closePanel = true;
        }
        private void Update()
        {
            if(openPanel)
            {
                Canvaspanel.alpha += Time.deltaTime * fadeInSpeed;
                
                if (Canvaspanel.alpha >= 1)
                {
                    if (OnFinishedOpened != null) { OnFinishedOpened.Invoke(); }
                    Canvaspanel.interactable = true;
                    Canvaspanel.blocksRaycasts = true;
                    openPanel = false;
                }
            }
            if (closePanel)
            {
                Canvaspanel.interactable = false;
                Canvaspanel.blocksRaycasts = false;
                Canvaspanel.alpha -= Time.deltaTime * fadeOutSpeed;
           
                if (Canvaspanel.alpha <= 0)
                {
                    
                    closePanel = false;
                    if (OnFinishedFade != null) { OnFinishedFade.Invoke(); }
                }
            }
        }

    }
}
