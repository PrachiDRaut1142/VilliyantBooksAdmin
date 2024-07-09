using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
  public  class CustomerSummaryCount
    {

        public int TotalDownload { get; set; }
        public int TotalRegistered { get; set; }
        public int TotalUnRegistered { get; set; }
        public int TotalRegistederdButNotOrderToday { get; set; }
        public int TodayTotalCustomerRegistered { get; set; }
        public float TodayTotalOrderValueRegiCustomer { get; set; }
        public float TodayTotalOrderValueRegiCustomerAvg { get; set; }
        public int MorethanTwentyCustomerCount { get; set; }
        public int MorethanTenCustomerCount { get; set; }
        public int MorethanFiveCustomerCount { get; set; }
        public int LessthanFiveCustomerCount { get; set; }     
        public int MorethanOneCustomerCount { get; set; }
        public int ZeroCustomerCount { get; set; }

    }
}
