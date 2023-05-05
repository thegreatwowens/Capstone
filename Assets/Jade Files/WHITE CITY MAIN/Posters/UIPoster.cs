using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace OwnCode.Choi
{
    public class UIPoster : MonoBehaviour
    {
        [Header("NOTE:")]

        [Header("Scriptable Object here")]
        [Space]
        [SerializeField]
        private PostersTrigger poster;
        [Space]
        [Header("Poster Values")]
        [SerializeField]
        TextMeshProUGUI _title;
        [SerializeField]
        TextMeshProUGUI _content;
        [SerializeField]
        Image _image;
        [SerializeField]
        PanelManager panel;

        private void OnValidate()
        {
            this.gameObject.name = "Trivia";
            if (poster == null)
            {
                Debug.Log("MAG LAGAY KA NG SCRIPTABLE OBJECT!");

            }

        }
        private void Awake()
        {
            _title = GameObject.Find("Title_Poster").GetComponent<TextMeshProUGUI>();
            _content = GameObject.Find("Content_Poster").GetComponent<TextMeshProUGUI>();
            _image = GameObject.Find("Illustration_Poster").GetComponent<Image>();
            panel = GameObject.Find("TriviaCanvas").GetComponent<PanelManager>();
        }
        public void UIShow()
        {
            _title.text = poster._title;
            _content.text = poster._content;
            _image.sprite = poster._image;
            panel.OpenPanel();
        }
        public void UIHide()
        {
            panel.HidePanel();
        }
    }
}

