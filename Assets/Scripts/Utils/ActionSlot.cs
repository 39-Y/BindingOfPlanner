using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Transform actionBlock = eventData.pointerDrag.transform;
        
        if (actionBlock != null)
        {
            string currentDate = actionBlock.parent.parent.Find("date").GetComponent<TextMeshProUGUI>().text;
            
            string changedDate = transform.Find("date").GetComponent<TextMeshProUGUI>().text;
            
            Debug.Log("current:" + currentDate+"/changed:"+ changedDate+"/isSame?" + changedDate.Equals(currentDate));
            
            if (!changedDate.Equals(currentDate))
            {
                actionBlock.SetParent(transform.Find("ActionBlocks"));
                
                Debug.Log("update");

            }
        }
    }
}
