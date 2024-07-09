using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class SalesList
    {
        public List<CurrencyMST> currencyInfo;

        public int Id { get; set; }
        public string SalesListId { get; set; }
        public string SalesId { get; set; }
        public string DecodeId { get; set; }
        public double TotalsalesOrderCost { get; set; }
        public string CustomerId { get; set; }
        public string Category { get; set; }
        public string ItemId { get; set; }
        public string Measurement { get; set; }
        public double QuantityValue { get; set; }
        public string StatusValue { get; set; }

        public double PricePerMeas { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }
        public double weight { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public string PluName { get; set; }
        public float SalesTotalPrice { get; set; }
        public string CName { get; set; }
        public string CategoryId { get; set; }
        public double Weight { get; set; }
        public string ItemSellingType { get; set; }
        public double Stock { get; set; }
        public string SalesOrderId { get; set; }
        public string ItemType { get; set; }
        public double MarketPrice { get; set; }
        public double  QuantityMarketPrice { get; set; }
        public double WeightMarketPrice { get; set; }
        public int CoupenId { get; set; }
        public int Coupen_Disc { get; set; }
        public List<string> SalesIds { get; set; }
        public List<string> CustomerIds { get; set; }
        public double Item_Cost { get; set; }
        public double AddedDiscount { get; set; }


        public List<string> Categorys { get; set; }
        public List<string> ItemIds { get; set; }
        public List<string> PriceIds { get; set; }
        public List<string> Measurements { get; set; }
        public List<string> QuantityValues { get; set; }
        public List<string> StatusValues { get; set; }

        public List<string> PricePerMeasList { get; set; }
        public List<string> TotalPriceList { get; set; }
        public List<string> DiscountList { get; set; }
        public List<string> weightList { get; set; }
        public List<string> DeliveryDateList { get; set; }
        public string ProductDeliveryDates { get; set; }
        public List<string> CreatedByList { get; set; }
        public string ItemWeight { get; set; }
        public string OrderStatus { get; set; }
        public int TotalsalesListCount { get; set; }
        public int editsalesListCount { get; set; }
        public string tableId { get; set; }
        public string tableName { get; set; }
        //public string ItemStatus { get; set; }

        public string status { get; set; }

        public List<string> statusList { get; set; }
        public string Duration { get; set; }
        public string PLU_Count { get; set; }
        public string UpdateDuration { get; set; }
        public string ComputedCoupen { get; set; }
        
        public string OrderdStatus { get; set; }
        public string PaymentStatus { get; set; }

        public int KOT_Print { get; set; }
        public string KOT_PrintDesc { get; set; }

        public string KitchListId { get; set; }
        public int KOT_Status { get; set; }
        public List<string> SalesOrderIds { get; set; }
        public int KotPrint { get; set; }

        public object Split(char v)
        {
            throw new NotImplementedException();
        }


        public double TotalQty { get; set; }
        public string Remark { get; set; }

        public string getprevurl { get; set; }
        public string OrderType { get; set; }
        public string TokenNumberOrder { get; set; }

        public int Item_Id { get; set; }
        public int Price_Id { get; set; }
        public string PriceId { get; set; }
        public string AWBNumber { get; set; }
        public string Size { get; set; }
        public float OrderQty { get; set; }
        public float ItmNetWeight { get; set; }

        public double BillingAmount { get; set; }
        public string CancellationReason { get; set; }
        public string Barcode { get; set; }
    }

}
