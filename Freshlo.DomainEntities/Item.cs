using System;
using System.Collections.Generic;
using System.Text;
using Freshlo.DomainEntities.DTO;

namespace Freshlo.DomainEntities
{
    public class Item
    {
        public string categoryId { get; set; }
        public int SalesListTableCoumt { get; set; }
        public string SalesListId { get; set; }
        public int Id { get; set; }
        public int PId { get; set; }
        public string PriceId { get; set; }
        public float OrderQty { get; set; }

        public string ItemId { get; set; }
        public string PluName { get; set; }
        public string PluCode { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Measurement { get; set; }
        public string Size { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; }
        public string HSN_Code { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public double Purchaseprice { get; set; }
        public double Wastage { get; set; }
        public double ProfitMargin { get; set; }
        public double SellingPrice { get; set; }
        public double MarketPrice { get; set; }
        public string MainCategory { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public double ProfitPrice { get; set; }
        public double ActualCost { get; set; }
        public string seasonSale { get; set; }
        public string Item_group { get; set; }
        public string Mgf_code { get; set; }
        public string gst_per { get; set; }
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

        public string FeaturedStartDate { get; set; }
        public string Approval { get; set; }
        public string OfferId { get; set; }
        public string StockType { get; set; }
        public double NetWeight { get; set; }
        public double? TotalStock { get; set; }
        public int MaxQuantityAllowed { get; set; }
        public string Brand { get; set; }
        public string Tag { get; set; }
        public string ItemType { get; set; }
        public string CategoryName { get; set; }
        public byte[] Imagedata { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }        
        public string itemstock { get; set; }
        public string DiscountedPrice { get; set; }
        public string Branch { get; set; }
        public int CoupenDisc { get; set; }
        public double Tax_Value { get; set; }

        // Live Offer field
        public string OfferDescription { get; set; }
        public string OfferHeading { get; set; }
        public double? DiscountPerctg { get; set; }
        public DateTime? OfferEndDate { get; set; }
        public int stockId { get; set; }
        public string CustomerId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string SalesId { get; set; }
        public string BarcodeData { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCount { get; set; }
        public double AddedDiscount { get; set; }
        public string HubId { get; set; }
        public bool featuredBool { get; set; }
        public bool seasonBool { get; set; }
        public bool coupenBool { get; set; }
        public string Status { get; set; }
        public string ItemStatus { get; set; }
        public string Name { get; set; }

        public string MaincatId { get; set; }
        public string Remark { get; set; }
        public float Stock { get; set; }
        public string TableId { get; set; }
        public ItemSizeInfo ItemSizeInfo { get; set; }
        public float ItmNetWeight { get; set; }

    }

}
