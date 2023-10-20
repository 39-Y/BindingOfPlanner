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
    [Inject]public Canvas canvas;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.color = new Color32(168, 197, 236, 100);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        Debug.Log("canvas:"+canvas);
        rectTransform.anchoredPosition += eventData.delta;
        //transform.position = Input.mousePosition;
    }

    

    public void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color32(168, 197, 236, 255);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
