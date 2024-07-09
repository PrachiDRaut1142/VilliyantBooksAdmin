using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class ItemMasters
    {
        public int Id { get; set; }
        public int PId { get; set; }
        public string ItemId { get; set; }
        public string MappingItemId { get; set; }
        public int salesId { get; set; }
        public int ImagesCount { get; set; }
       
        public string LanguageName { get; set; }
        public string measuremnetlanguage { get; set; }
        public string Languagecode { get; set; }
        public string Itemused { get; set; }
        public string Descriptionlanguage { get; set; }
        public string Itemname { get; set; }

        public string foodtype { get; set; }
        public string FoodType { get; set; }
        public string ItemId1 { get; set; }
        public string featured2 { get; set; }
        public string Approval1 { get; set; }
        public string CoupenDisc1 { get; set; }
        public string PriceId { get; set; }
        public string PluName { get; set; }
        public string totalnetweight { get; set; }
        public string PluCode { get; set; }
        public string Category { get; set; }
        public string ItemImage { get; set; }
        public string SubCategory { get; set; }
        public string Measurement { get; set; }
        public string colorCode { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string Description { get; set; }
        public string HSN_Code { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Purchaseprice { get; set; }
        public string Wastage { get; set; }
        public string ProfitMargin { get; set; }
        public string SellingPrice { get; set; }
        public string MarketPrice { get; set; }
        public string imagecdnpath { get; set; }
        public string Title { get; set; }
        public IFormFile ImagePath { get; set; }
        public List<IFormFile> ImagePaths { get; set; }

        public string ProfitPrice { get; set; }
        public string ActualCost { get; set; }
        public string seasonSale { get; set; }
        public string Item_group { get; set; }
        public string Mgf_code { get; set; }
        public string gst_per { get; set; }
        public string vat_per { get; set; }
        public string sgst_per { get; set; }
        public string cgst_per { get; set; }
        public string Manufracture { get; set; }
        public string offer_type { get; set; }
        public string foodSegment { get; set; }
        public string TotalViews { get; set; }
        public string TotalFavs { get; set; }
        public string SellingVarience { get; set; }
        public string ItemSellingType { get; set; }
        public string Supplier { get; set; }
        public string featured { get; set; }
        public string PromoVideoLink { get; set; }
        public string LongDescription { get; set; }
        public string MainCategory { get; set; }
        public string FeaturedStartDate { get; set; }
        public string Approval { get; set; }
        public string OfferId { get; set; }
        public string StockType { get; set; }
        public string NetWeight { get; set; }
        public string TotalStock { get; set; }
        public int MaxQuantityAllowed { get; set; }
        public string Brand { get; set; }
        public string Tag { get; set; }
        public string ItemType { get; set; }
        public string Visibility { get; set; }
        public string Branch { get; set; }
        public string CreatedName { get; set; }
        public int Coupen { get; set; }
        public int Relationship { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public double ProductCost { get; set; }
        public string Tax { get; set; }




        //Count of Item
        public int Activeitem { get; set; }
        public int Chicken { get; set; }
        public int Mutton { get; set; }
        public int SeaFood { get; set; }
        public int chickenTotalPrice { get; set; }
        public int MuttonTotalPrice { get; set; }
        public int seafoodTotalPrice { get; set; }
        public int PendingApproval { get; set; }
        public int Featureditem { get; set; }
        public int coupenexcluded { get; set; }
        public int Deleteditem { get; set; }


        //string List Data
        public List<string> MainCategory1 { get; set; }
        public List<string> categoryId { get; set; }
        public List<string> subcategory { get; set; }
        public List<string> supplierid { get; set; }
        public List<string> featured1 { get; set; }
        public List<string> coupen { get; set; }
        public List<string> itemStatus { get; set; }
        public int KotPrintedId { get; set; }
        public int ItemStatus { get; set; }
        public int HubPriceId { get; set; }
        public string hubId { get; set; }
        //public string PriceId { get; set; }
        //public string SellingProfitPer { get; set; }
        //public string discountPrice { get; set; }
        //public string OfferDiscount { get; set; }
        //public string OrderQty { get; set; }

        public int KOT_Print { get; set; }
        public string KOT_PrintDesc { get; set; }

        public string Check_Speical { get; set; }

        public string FoodSubType { get; set; }
        public string FoodType1 { get; set; }
        public string Itemnever { get; set; }

        public int Spicy_Level { get; set; }
        public string webRootPath { get; set; }
        public float ItmNetWeight { get; set; }
        public float TotalWeightchicken { get; set; }
        public float TotalWeightMutton { get; set; }
        public float TotalWeightSeaFood { get; set; }

        public DateTime StartDates { get; set; }
        public DateTime AvalStartDate { get; set; }
        public DateTime AvalEndDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string IsSpecialDay { get; set; }
        public int AvailableDay { get; set; }
        public string MealTimeType { get; set; }
        public string MealTimeZone { get; set; }
        public string PreparationTime { get; set; }


        public string FoodType2 { get; set; }
        public string FoodSubType2 { get; set; }
        public string FoodsubName { get; set; }


        public List<int> AvailableDayS { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public float OrderQty { get; set; }

        public string WasteagePerc { get; set; }
        public string WastageQty { get; set; }
        public string SellingProfitPer { get; set; }
        public string TotalPrice { get; set; }

        public List<ItemSizeInfo> ItemSizeInfo { get; set; }
        public List<ItemColorInfo> ItemColorInfo { get; set; }
        public string MeasuredIn { get; set; }
       
        public string Barcode { get; set; }
        public int ProductColorId { get; set; }

        public string Color { get; set; }
        public IFormFile Image { get; set; }
        public int Stock { get; set; }
        public int ColorStock { get; set; }
        public string ColorId { get; set; }
        public string Aliyunkey { get; set; }
        public string IsHubMap { get; set; }
        public string CoupenDisc { get; set; }
        public string Hub { get; set; }
        public string Variant { get; set; }
        public string ItemNameLanguage { get; set; }
        public int QuantityValue { get; set; }
        public string BarcodeImage { get; set; }

        public static implicit operator List<object>(ItemMasters v)
        {
            throw new NotImplementedException();
        }
        public int type { get; set; }
        public bool DisplayWithImg { get; set; }


    }
   
}
