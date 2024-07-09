using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class SalesCountData
    {
        public int TotalConfirmed { get; set; }
        public int TotalPending { get; set; }
        public int TodaysPending { get; set; }
        public float TotalPendingAmount { get; set; }
        public float TodaysPendingAmount { get; set; }

        public int NotifyOrderCount { get; set; }
    }
}
