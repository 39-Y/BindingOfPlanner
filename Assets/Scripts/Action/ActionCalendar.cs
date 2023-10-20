using System;
using System.Collections.Generic;
using UnityEngine;

namespace Action
{
    public class ActionCalendar
    {
        private DateTime currentDate;
        private int cellTotalCount;
        private int curMonthLastDay;
        private int curMonthFirstDayOfWeek;

        public ActionCalendar(int cellTotalCount)
        {
            this.cellTotalCount = cellTotalCount;
        }

        public List<DateTime> MakeDays(DateTime currentDate)
        {
            this.currentDate = currentDate;
            curMonthLastDay = DateTime.DaysInMonth(this.currentDate.Year, this.currentDate.Month);
            curMonthFirstDayOfWeek = GetCurMonthFirstDayOfWeek();
            List<DateTime> days = new List<DateTime>();
            
            for (int cell = 0; cell < cellTotalCount; cell++)
            {
                if (IsPreMonth(cell)) //저번 달 일자
                {
                    days.Add(CalcPreMonthDay(cell));
                }
                else if (IsCurMonth(cell)) //이번 달 일자
                {
                    days.Add(CalcCurMonthDay(cell));
                }
                else // 다음달 일자
                {
                    days.Add(CalcNextMonthDay(cell));
                }
            
            }
            return days;
        }

        private DateTime CalcPreMonthDay(int cell)
        {
            int day = GetPreMonthLastDay() - curMonthFirstDayOfWeek + cell + 1;
            DateTime preMonth = GetPreMonth();
            return new DateTime(preMonth.Year, preMonth.Month, day);
        }

        private DateTime CalcNextMonthDay(int cell)
        {
            int day = cell - (curMonthLastDay + curMonthFirstDayOfWeek) +1;
            DateTime nextMonth = GetNextMonth();
            return new DateTime(nextMonth.Year, nextMonth.Month, day);
        }

        private DateTime CalcCurMonthDay(int cell)
        {
            int day = cell - curMonthFirstDayOfWeek + 1;
            return new DateTime(currentDate.Year, currentDate.Month, day);
        }
        private bool IsPreMonth(int cell)
        {
            return cell < GetCurMonthFirstDayOfWeek();
        }

        private bool IsCurMonth(int cell)
        {
            return cell >= curMonthFirstDayOfWeek && cell < curMonthLastDay + curMonthFirstDayOfWeek;
        }

        private int GetCurMonthFirstDayOfWeek()
        {
            return (int) new DateTime(currentDate.Year, currentDate.Month, 1).DayOfWeek;
        }
        
        private int GetPreMonthLastDay()
        {
            DateTime preDateTime = GetPreMonth();
            return DateTime.DaysInMonth(preDateTime.Year, preDateTime.Month);   
        }
    
        private DateTime GetPreMonth()
        {
            if (currentDate.Month == 1)
            {
                return new DateTime(currentDate.Year - 1, 12, 1);
            }
            return currentDate.AddMonths(-1);
        }

        private DateTime GetNextMonth()
        {
            if (currentDate.Month == 12)
            {
                return new DateTime(currentDate.Year + 1, 1, 1);
            }

            return currentDate.AddMonths(1);
        }
    }
}