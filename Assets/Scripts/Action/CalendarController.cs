using System;
using System.Collections.Generic;
using Action.VO;
using ModestTree;
using TMPro;
using UnityEngine;
using Zenject;

namespace Action
{
    public class CalendarController : MonoBehaviour
    {
        private DateTime currentDate;
        [Inject] private ActionService actionService;
        private ActionCalendar actionCalendar;
        private Transform days;
        public GameObject actionBlock;
        
        void Start()
        {
            currentDate = DateTime.Now;
            days = transform.Find("days");
            actionCalendar = new ActionCalendar(days.childCount);
            LoadCalendar();
        }
        
        public void UpdateNextMonth()
        {
            currentDate = GetNextMonth();
            LoadCalendar();
        }
        
        
        public void UpdatePreMonth()
        {
            currentDate = GetPreMonth();
            LoadCalendar();
        }
        
        private DateTime GetNextMonth()
        {
            if (currentDate.Month == 12)
            {
                return new DateTime(currentDate.Year + 1, 1, 1);
            }
            return currentDate.AddMonths(1);
        }
    
        private DateTime GetPreMonth()
        {
            if (currentDate.Month == 1)
            {
                return new DateTime(currentDate.Year - 1, 12, 1);
            }
            return currentDate.AddMonths(-1);
        }


        private void LoadCalendar()
        {
            List<DateTime> calendarDates = actionCalendar.MakeDays(currentDate);
            Queue<ActionVO> actionsQueue = GetActionQueue(calendarDates);

            SetYearMonthText();
            SetCalendarUI(calendarDates, actionsQueue);
        }

        private Queue<ActionVO> GetActionQueue(List<DateTime> calendarDates)
        {
            string firstDate = calendarDates[0].ToString("yyyy-MM-dd");
            string lastDate = calendarDates[^1].ToString("yyyy-MM-dd");
            List<ActionVO> actionVos = actionService.FindByPeriod(firstDate, lastDate);
            return new Queue<ActionVO>(actionVos);
        }
        
        private void SetYearMonthText()
        {
            transform.Find("year_month").GetComponent<TextMeshProUGUI>().text = currentDate.ToString("yyyy MM");
        }

        private void SetCalendarUI(List<DateTime> calendarDates, Queue<ActionVO> actionsQueue)
        {
            for(int cell = 0; cell < calendarDates.Count; cell++)
            {
                DateTime date = calendarDates[cell];
                Transform d = days.GetChild(cell);
                Transform actionBlocks = d.Find("ActionBlocks");
                
                SetDaysCell(date, d);
                ClearObject(actionBlocks);
                
                while (IsActionInDayAndQueueNotEmpty(actionsQueue, date)) //ToDoList: 추후 createDate -> dodate로 바꿀 것
                {
                    SetActionBlock(actionBlocks, actionsQueue.Dequeue());
                }
            }
        }

        private void SetDaysCell(DateTime date, Transform d)
        {
            TextMeshProUGUI dayText = d.Find("day").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI dateInfo = d.Find("date").GetComponent<TextMeshProUGUI>();
            SetDayText(date, dayText);
            SetDateInfo(date, dateInfo);
        }
        
        private void SetDayText(DateTime date, TextMeshProUGUI dayText)
        {
            if (IsCurrentMonth(date)) //이번 달 일자
            {
                dayText.color = Color.black;
                if (IsToday(date))
                {
                    dayText.color = Color.magenta;
                }
            }
            else //이번 달 아님
            {
                dayText.color = Color.gray;
            }
            
            dayText.text = date.Day.ToString();
        }

        private void SetDateInfo(DateTime date, TextMeshProUGUI dateInfo)
        {
            dateInfo.text = date.ToString("yyyy-MM-dd");
        }
        
        private void SetActionBlock(Transform actionBlocks, ActionVO actionVo)
        {
            Transform action= Instantiate(actionBlock, actionBlocks).transform;
            action.Find("action_text").GetComponent<TextMeshProUGUI>().text = actionVo.title;
            action.Find("action_id").GetComponent<TextMeshProUGUI>().text = actionVo.id.ToString();
                
        }
        
        
        private void ClearObject(Transform actionBlocks)
        {
            foreach (Transform actionBlock in actionBlocks)
            {
                Destroy(actionBlock.gameObject);
            }
        }

        private bool IsActionInDayAndQueueNotEmpty(Queue<ActionVO> actionsQueue, DateTime date)
        {
            return !actionsQueue.IsEmpty() && IsSameDay(actionsQueue.Peek().doDate, date);
        }
        
        private bool IsToday(DateTime date)
        {
            return IsSameDay(date, DateTime.Now);
        }

        private bool IsSameDay(DateTime date1, DateTime date2)
        {
            return IsSameYearAndMonth(date1, date2) &&
                   date1.Day == date2.Day;
        }

        private bool IsCurrentMonth(DateTime date)
        {
            return IsSameYearAndMonth(date, currentDate);
        }

        private bool IsSameYearAndMonth(DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year &&
                   date1.Month == date2.Month;
        }
        
        
        
    }
}
