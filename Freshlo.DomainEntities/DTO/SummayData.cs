using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.DTO
{
   public class SummayData
    {
        public float TodaysInn { get; set; }
        public float TodaysOut { get; set; }
        public float WeeklyInn { get; set; }
        public float WeeklyOut { get; set; }
        public float MonthlyInn { get; set; }
        public float MonthlyOut { get; set; }

        ///Purchase Summary
        public float TodaysPO { get; set; }
        public float TodaysExpense { get; set; }
        public float WeeklyPO { get; set; }
        public float WeeklyExpense { get; set; }
        public float MonthlyPO { get; set; }
        public float MonthlyExpense { get; set; }
    }
}
