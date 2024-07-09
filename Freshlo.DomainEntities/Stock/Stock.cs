using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Stock
{
    public class Stock
    {
        public int Id { get; set; }
        public string ItemId { get; set; }
        public double StockValue { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string Hub { get; set; }
        public string PluName { get; set; }
        public string size { get; set; }
        public string priceId { get; set; }
        public string Measurement { get; set; }
        public double ItemPrice { get; set; }
        public double ItemPriceSelling { get; set; }
        public double Totalstock { get; set; }
        public double salesStock { get; set; }
        public double salesPrice { get; set; }
        public double Weight { get; set; }
        public string Itemcount { get; set; }
        public string Itemoutstock { get; set; }
        public string Itemnotlive { get; set; }
        public string Barcode { get; set; }
        public string maincategory { get; set; }
        public string category { get; set; }
        public string StockQty { get; set; }
        public decimal SellingPrice { get; set; }
        public string Approval { get; set; }
        public int IsVisible { get; set; }
        public string ShortCode { get; set; }
        public string RejectedReason { get; set; }

    }
}
