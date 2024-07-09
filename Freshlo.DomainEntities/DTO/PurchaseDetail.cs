using Freshlo.DomainEntities.Purchase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.DTO
{
    public class PurchaseDetail
    {
        public string Branch { get; set; }
        public string PurchaseId { get; set; }

        public DateTime DeliveryDate { get; set; }
        public float TotalQuantity { get; set; }
        public string PO_Source { get; set; }
        public float Plu_Count { get; set; }
        public float Total_Price { get; set; }
        public string Procurement_Type { get; set; }
        public List<PurchaseList> List { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Products { get; set; }
        public string Status { get; set; }
        public float OtherExpension { get; set; }
        public float Transportation_Charge { get; set; }
        public float Agent_commission { get; set; }
        public string Vendor { get; set; }







    }
}
