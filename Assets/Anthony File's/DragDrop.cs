using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string name;

    private RectTransform rectTransform;
    public Canvas myCanvas;
    public int id;
    public CanvasGroup canvasGroup;
    private Vector2 initPos;
    public bool finish;
    public Image image;
    private ToolTipScreenSpaceUI tooltipScreen;
    public int alpha;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initPos = transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (finish == false)
        {
            rectTransform.anchoredPosition += eventData.delta / myCanvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        
    }
    public void ResetPosition()
    {
        transform.position = initPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        ToolTipScreenSpaceUI.ShowTooltip_Static(this.name);
        //tooltipScreen.tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        ToolTipScreenSpaceUI.HideTooltip_Static();
        //tooltipScreen.tooltip.SetActive(false);
    }
}
