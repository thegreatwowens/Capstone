using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;

public class ToolTipScreenSpaceUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static ToolTipScreenSpaceUI instance;

    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] public GameObject tooltip;

    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;
    private void Awake()
    {
        instance = this;

        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();
        //SetText("Hello World");
        //tooltip.SetActive(false);
    }

    /*private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);

        backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }*/

    private void Update()
    {
        rectTransform.position = Mouse.current.position.ReadValue();
        //Input.mousePosition / canvasRectTransform.localScale.x;
    }

    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);

        textMeshPro.text = tooltipString;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowTooltip(tooltipString);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }   
    public void OnPointerEnter(PointerEventData eventData)
    {

        //tooltip.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //tooltip.enabled = false;
    }

    public void Show()
    {

    }
}
