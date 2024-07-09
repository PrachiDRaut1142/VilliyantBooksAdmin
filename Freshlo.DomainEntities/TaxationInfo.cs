using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class TaxationInfo
    {
        public int id { get; set; }
        public int taxType { get; set; }
        public string taxRegNo { get; set; }
        public string vatRegNo { get; set; }
        public int calcuationTaxtype { get; set; }
        public float taxPercentGST { get; set; }
        public float taxPercentVAT { get; set; }
        public DateTime modifiedOn { get; set; }
        public string modifiedBy { get; set; }
        public string createdOn { get; set; }
        public string hubId { get; set; }
        public int isActive { get; set; }
        public int taxbillType { get; set; }
    }
}
