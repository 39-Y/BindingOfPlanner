using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Image image;
    [SerializeField]private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.color = new Color32(168, 197, 236, 100);
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //transform.position = Input.mousePosition;
    }
    

    

    public void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color32(168, 197, 236, 255);
        canvasGroup.alpha = 1f;

        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
