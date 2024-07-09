using Microsoft.AspNetCore.Http;
using System;

namespace Freshlo.DomainEntities
{
    public class ItemSizeInfo
    {

        public int Id { get; set; }
        public int PId { get; set; }
        public string Aliyunkey { get; set; }
        public string ItemId { get; set; }
        public string PriceId { get; set; }
        public string PluName { get; set; }
        public string PluCode { get; set; }
        public string Measurement { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public double Purchaseprice { get; set; }
        public string Wastage { get; set; }
        public string ProfitMargin { get; set; }
        public double SellingPrice { get; set; }
        public double MarketPrice { get; set; }
        public string Title { get; set; }
        public IFormFile ImagePath { get; set; }
        public IFormFile ImagePathurl { get; set; }

        public string ProfitPrice { get; set; }
        public double ActualCost { get; set; }
        public string seasonSale { get; set; }
        public string Mgf_code { get; set; }
        public string gst_per { get; set; }
        public string sgst_per { get; set; }
        public string cgst_per { get; set; }
        public string Manufracture { get; set; }
        public string offer_type { get; set; }
        public string foodSegment { get; set; }
        public string OfferId { get; set; }
        public string StockType { get; set; }
        public string NetWeight { get; set; }
        public string TotalStock { get; set; }
        public int MaxQuantityAllowed { get; set; }
        public double ProductCost { get; set; }
        public string webRootPath { get; set; }
        public float ItmNetWeight { get; set; }
        public float OrderQty { get; set; }
        public string WasteagePerc { get; set; }
        public string WastageQty { get; set; }
        public string SellingProfitPer { get; set; }
        public string TotalPrice { get; set; }
        public string ItmMeasurement { get; set; }
        public string Hub { get; set; }
        public string Barcode { get; set; }
        public string imagecdnpathvariance { get; set; }
        public string BarcodeImage { get; set; }
        public string colorCode { get; set; }
    }
}