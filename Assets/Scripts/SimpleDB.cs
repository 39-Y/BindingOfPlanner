using System.Collections;
using System.Collections.Generic;
using System.Data;
using Action;
using Action.VO;
using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;
using Zenject;

public class SimpleDB : MonoBehaviour
{
    [Inject] private ActionService actionService;
    
    public GameObject actionPrefab;
    public GameObject canvas;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertAction()
    {
        actionService.save();
    }

    public void SelectAllAction()
    {
        List<ActionVO> actionVos = actionService.FindAll();

        foreach (ActionVO actionVo in actionVos)
        {
            Debug.Log($"id:{actionVo.id}, title:{actionVo.content}, date:{actionVo.createDate}");
            GameObject action = Instantiate(actionPrefab, canvas.transform);
            Transform header = action.transform.Find("header");
            Transform body = action.transform.Find("body");
            Transform footer = action.transform.Find("footer");

            header.Find("title").GetComponent<TextMeshProUGUI >().text = actionVo.title;
            header.Find("id").GetComponent<TextMeshProUGUI>().text = $"id: {actionVo.id}";
            body.Find("content").GetComponent<TextMeshProUGUI >().text = actionVo.content;
            footer.Find("isCompleted").GetComponent<TextMeshProUGUI >().text = actionVo.isCompleted.ToString();
            footer.Find("createDate").GetComponent<TextMeshProUGUI >().text = actionVo.createDate.ToString("yyyy-MM-dd");
        }

    }
}
