using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.PriceList
{
    public class PriceList
    {

        public int Id { get; set; }
        public string ItemId { get; set; }
        public string PriceId { get; set; }
        public string Barcode { get; set; }
        public string pluName { get; set; }
        public string Measurement { get; set; }
        public string OrderQty { get; set; }
        public double WasteagePerc { get; set; }
        public double WastageQty { get; set; }
        public double TotalQty { get; set; }
        public double PurchasePrice { get; set; }
        public double SellingPrice { get; set; }
        public double IncomingRevenue { get; set; }
        public double TotalPrice { get; set; }
        public double SellingProfitPer { get; set; }
        public double DiffMargin { get; set; }
        public double ProfitMargin { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string MarketPrice { get; set; }
        public string seasonSale { get; set; }
        public string Item_group { get; set; }
        public string Mgf_code { get; set; }
        public string gst_per { get; set; }
        public string sgst_per { get; set; }
        public string cgst_per { get; set; }
        public string offer_type { get; set; }
        public string foodSegment { get; set; }
        public double DiscountPrice { get; set; }
        public double OfferDiscount { get; set; }

        public double DiscountedValue { get; set; }
        public int selectedItem { get; set; }

        /* Get List Of Data */


        public List<int> Ids { get; set; }
        public List<string> ItemIds { get; set; }
        public List<string> PriceIds { get; set; }
        public List<string> pluNames { get; set; }
        public List<string> Measurements { get; set; }
        public List<string> OrderQtys { get; set; }
        public List<double> WasteagePercs { get; set; }
        public List<double> WastageQtys { get; set; }
        public List<double> TotalQtys { get; set; }
        public List<double> PurchasePrices { get; set; }
        public List<double> SellingPrices { get; set; }
        public List<double> TotalPrices { get; set; }
        public List<double> SellingProfitPers { get; set; }
        public List<double> ProfitMargins { get; set; }
        public string CreatedBys { get; set; }
        public DateTime CreatedOns { get; set; }
        public string LastUpdatedBys { get; set; }
        public DateTime LastUpdatedOns { get; set; }
        public List<string> MarketPrices { get; set; }
        public List<string> seasonSales { get; set; }
        public string Item_groups { get; set; }
        public string Mgf_codes { get; set; }
        public string gst_pers { get; set; }
        public string sgst_pers { get; set; }
        public string cgst_pers { get; set; }
        public string offer_types { get; set; }
        public string foodSegments { get; set; }
        public List<double> DiscountPrices { get; set; }
        public List<double> OfferDiscounts { get; set; }

        public string MainCatName { get; set; }
        public string CatName { get; set; }
        public string SubCatName { get; set; }
        public int type { get; set; }
        
        
    }
}
