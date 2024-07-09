using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.DTO
{
    public class PrintSalesList
    {
        public string ItemId { get; set; }
        public string PluName { get; set; }
        public string Measurement { get; set; }
        public string QuantityValue { get; set; }
        public string PricePerMeas { get; set; }
        public string TotalPrice { get; set; }
        public string Discount { get; set; }
        public string Weight { get; set; }
        public string Taxable_Amount { get; set; }
        public string gst_perId { get; set; }
        public string sgst_per { get; set; }
        public string cgst_Per { get; set; }
        public string CGST_Value { get; set; }
        public string SGST_Value { get; set; }
        public string IGST_Per { get; set; }
        public string HSNCode { get; set; }
        public string ItemType { get; set; }
        public int KOT_Print { get; set; }
        public DateTime LastUpdateOn { get; set; }
        public string Name { get; set; }
        public string SalesOrderId { get; set; }
        public string TableId { get; set; }
        public string tableName { get; set; }
        public string Size { get; set; }
        public string Remark { get; set; }
    }
}
