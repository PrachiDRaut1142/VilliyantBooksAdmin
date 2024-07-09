using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.DTO
{
   public class SaleSummary
    {
        public string CreatedDate { get; set; }
        public float TotalPrice { get; set; }
        public float Discount { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
    }
}
