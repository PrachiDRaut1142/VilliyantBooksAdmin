using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Stock;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Freshlo.Web.Models.ItemMaster
{
    public class ItemMasterVM : BaseViewModel
    {
        public List<ProductPriceLog> GetProductPriceLog { get; set; }
        public List<SelectListItem> GetMainCategoryList { get; set; }
        public List<SelectListItem> GetSegmentList { get; set; }
        public List<SelectListItem> GetMeasurementList { get; set; }
        public List<SelectListItem> offerType { get; set; }
        public List<SelectListItem> SupplierNameList { get; set; }
        public List<SelectListItem> BrandList { get; set; }
        public List<ItemMasters> getItemMasterList { get; set; }
        public List<ItemMasters> getHubItemMasterList { get; set; }
        public List<ItemCategory> getCattegorylist { get; set; }
        public List<SelectListItem> GetSubCategory { get; set; }
        public ItemMasters Getitemcount { get; set; }
        public ItemMasters ItemMastersDetail { get; set; }
        public List<string> GetGalleryImage { get; set; }
        public List<Item> ItemList { get; set; }
        public List<SelectListItem> GetItemType { get; set; }
        public List<ItemMasters> getItemPricelist { get; set; }
        public List<TaxPercentageMst> taxPercentageList { get; set; }
        public string branch { get; set; }
        public List<SelectListItem> foodTypeList { get; set; }
        public List<SelectListItem> foodsubTypeList { get; set; }
        public List<SelectListItem> getsubFoodlist { get; set; }
        public List<int> GetDayList { get; set; }
        public List<ItemMasters> HubItemSizeInfo { get; set; }
        public List<ItemMasters> ItemSizeInfo { get; set; }
        public List<ItemMasters> UnMappedItemSizeInfo { get; set; }
        public List<ItemMasters> ItemColorInfo { get; set; }
        public List<ItemMasters> MappedItemList { get; set; }

        public List<ColorSizeMapping> MapInfo { get; set; }
        public TaxationInfo taxInfoDetails { get; set; }
        public List<CurrencyMST> getccurrencylist { get; set; }
        public List<SelectListItem> gethubLiST { get; set; }
        public ItemMasters GEtItemCountDashboard { get; set; }
        public List<ItemMasters> GetItemDetails { get; set; }
        public List<ItemMasters> GetvegItemDetails { get; set; }
        public List<ItemMasters> Itemvegnonvegcount { get; set; }
        public List<SelectListItem> foodSubTypeList { get; set; }
        public List<ItemMasters> getlanguagelist { get; set; }
        public List<ItemMasters> getitemnamechage { get; set; }
        public List<ItemMasters> getaddedlanguagelist { get; set; }
        public List<ProductType> getproductList { get; set; }
        public List<ProductVariance> getProductVarianceList { get; set; }
        public List<ProductSpec> getProductSpecList { get; set; }
        public List<ItemMasters> getMappedVarientItemList { get; set; }
        
        public Stock getstockcount { get; set; }
        public List<Stock> getstocklist { get; set; }
        public TblListcs GetListDetail { get; set; }
    }
}
