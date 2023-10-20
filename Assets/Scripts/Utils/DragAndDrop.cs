using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Image image;

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
        rectTransform.anchoredPosition += eventData.delta;
        //transform.position = Input.mousePosition;
        
        Ray ray = Camera.main.ScreenPointToRay(rectTransform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 충돌한 오브젝트 처리
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("충돌한 오브젝트 이름: " + hitObject.name);
        }
    }

    

    public void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color32(168, 197, 236, 255);

    }
}
