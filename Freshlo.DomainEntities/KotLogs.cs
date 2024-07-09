using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class KotLogs 
    {
        public int Id { get; set; }
        public string KOTID { get; set; }
        public int TotalQuantity { get; set; }
        public string SalesOrderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int KotPrint { get; set; }
        public int CounterKot { get; set; }
    }
}
