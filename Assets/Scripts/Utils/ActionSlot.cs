using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("On Drop"+transform.Find("date").GetComponent<TextMeshProUGUI>().text); 
            eventData.pointerDrag.transform.SetParent(transform.Find("ActionBlocks"));
        }
    }
}
