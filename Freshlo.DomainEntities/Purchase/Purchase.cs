using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Purchase
{
    public class Purchase
    {
        public int Id { get; set; }
        public string PurchaseId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public float Wastage { get; set; }
        public float TotalQuantity { get; set; }
        public string VenderId { get; set; }
        public float SystemPurchase_Price { get; set; }
        public float ActualPurchase_Price { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string PurchaseStatus { get; set; }
        public string Comment { get; set; }
        public string Branch { get; set; }
        public string FreshloPurchaseId { get; set; }
        public string PO_Source { get; set; }
        public float Plu_Count { get; set; }
        public float Total_Price { get; set; }
        public double Transportation_Charge { get; set; }
        public double Taxes { get; set; }
        public double OtherExpension { get; set; }
        public float ReceivedItem_Price { get; set; }
        public string Procurement_Type { get; set; }
        public double ReceivedAmountPrice { get; set; }
        public double AgentCommision { get; set; }
        public string ReferenceNo { get; set; }




        public DateTime Proc_date { get; set; }
        public float GST { get; set; }
        public string ItemId { get; set; }
        public string pluName { get; set; }
        public double Quantity { get; set; }
        public string Measurement { get; set; }
        public double? QuantityValue { get; set; }
        public string Category { get; set; }
        public double? PurchasePrice { get; set; }

        public double?  MarketPrice { get; set; }
        public double? SellingPrice { get; set; }
        public double? Profitper { get; set; }
        public double? ProfitMargin { get; set; }
        public string categoryName { get; set; }
        public int currentsock { get; set; }



    }
}
