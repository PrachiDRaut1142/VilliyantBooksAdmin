using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class CurrencyMST
    {
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public int status { get; set; }
        public string symbol { get; set; }
        public string LastUpdatedBy { get; set; }
        public string CountryName { get; set; }
        public string CratedBy { get; set; } 
        public string hubId { get; set; } 
    }
}
