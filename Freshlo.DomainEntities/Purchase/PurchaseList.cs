using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Purchase
{
    public class PurchaseList
    {
        public int Id { get; set; }
        public string ProductListId { get; set; }
        public string ItemId { get; set; }
        public string Category { get; set; }
        public float TotalQuantity { get; set; }
        public string PurchaseId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public float OrderQuantity { get; set; }
        public string PurchaseStatus { get; set; }
        public float Wastage { get; set; }
        public string Measurement { get; set; }
        public string Comment { get; set; }
        public float ReceivedQuantity { get; set; }
        public float Procured_POPrice { get; set; }
        public float Procured_Quantity { get; set; }
        public float Received_POPrice { get; set; }
        public float Purchase_Price { get; set; }
        public float Selling_Price { get; set; }
        public float Market_Price { get; set; }
        public Double Profit_Per { get; set; }

        public string ProductName { get; set; }
        public double Weight { get; set; }






        // Bind this data


        public DateTime Proc_date { get; set; }
        public float GST { get; set; }
        public string pluName { get; set; }
        public double Quantity { get; set; }
        public double? QuantityValue { get; set; }
        public double? PurchasePrice { get; set; }

        public double? MarketPrice { get; set; }
        public double? SellingPrice { get; set; }
        public double? Profitper { get; set; }
        public double? ProfitMargin { get; set; }
        public string categoryName { get; set; }
        public int currentsock { get; set; }







    }
}
