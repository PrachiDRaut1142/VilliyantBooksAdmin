using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Purchase
{
   public class SummaryFilter
    {
        public int filter { get; set; }
        public string category { get; set; }
        public string approval { get; set; }
        public string availability { get; set; }
        public string subCategory { get; set; }
        public string mainCategory { get; set; }
    }
}
