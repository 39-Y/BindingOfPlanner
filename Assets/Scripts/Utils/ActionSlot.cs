using System;
using Action;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ActionSlot : MonoBehaviour, IDropHandler
{
    [Inject] private ActionService actionService;
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

                long id = Convert.ToInt64(actionBlock.Find("action_id").GetComponent<TextMeshProUGUI>().text);
                
                actionService.UpdateDoDateById(id, changedDate);
                
                Debug.Log("update");

            }
        }
    }
}
