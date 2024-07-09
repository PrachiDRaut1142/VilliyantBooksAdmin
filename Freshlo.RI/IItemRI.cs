using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Freshlo.DomainEntities.ItemMasters;

namespace Freshlo.RI
{
    public interface IItemRI
    {
        List<ItemCategory> GetCategories(string id);
        string CreateItemMaster(ItemMasters Item);
        List<SelectListItem> GetCategoryNamebyMainCate(string mainCatId,string id);
        List<SelectListItem> GetMainCategoryList(string id);
        List<SelectListItem> GetSubCategoryListbyCat(string CatId,string id);
        List<SelectListItem> GetSupplierNameList();
        List<SelectListItem> GetBrandList();
        int CreatePriceMaster(ItemMasters itemMasters);
        int CreateStockMaster(ItemMasters itemMasters);
        int CreateWastageMaster(ItemMasters itemMasters);
        List<ItemMasters> GetItemManageData(ItemMasters item);
        List<SelectListItem> GetSubCategoryList(string id);
        ItemMasters GetItemCountDetail();
        ItemMasters GetItemMasterDetail(int Id);
        ItemMasters HubGetItemMasterDetail(int Id);
        string UpdateItemMaster(ItemMasters itemMasters);
        int UpdatePriceMaster(ItemMasters itemMasters);
        int UpdateItemFeatured(string feature, string itemId, int type, int Id);
        int UpdateItemCheckSp(string checkSp, string itemId, int type, int Id);
        int UpdateItemApproval(string itemId, int Id, string approval, string UpdatedBy, int type);
        string CheckUniquePlucode(string PluCode);
        void UpdateItemFields(ItemFields list, string updatedBy);
        int UpdateItemAvailability(string season, string itemId, int type, int Id);
        int UpdateItemCoupen(string coupen, string itemId, int type, int Id);
        List<TaxPercentageMst> GetTaxPercentageList();
        int UpdateHubPrice(Item detail);
        List<ItemMasters> GetItemManagePriceData(ItemMasters item);
        List<ItemMasters> GetIncludedHubPrice(string id);
        //List<ItemMasters> GetExcludedHubPrice(int itemId, ItemMasters item);
        List<ItemMasters> GetExcludedHubPrice(string id);
        int DeleteHubItem(string itemId);
        int DeleteMapItem(string itemId, string mappingItemId, string hubId);
        int DeleteItem(ItemMasters info);
        //List<SelectListItem> GetItemTypeList();
        //List<ItemMasters> GetPricelistData(ItemMasters item);
        List<ProductPriceLog> GetProductPriceLog(string id, string hubId);


        Task<byte[]> FilterExcelofItems(ItemMasters item);
        List<SelectListItem> GetfoodTypeList();
        List<SelectListItem> GetSubfoods();
        List<SelectListItem> GetSubfoodBymainFood(string foodType);
        List<int> GetMappingDaywithItem(string id);
        List<ItemMasters> ProductSizeInfoDetails(string id, string hubId);
        List<ItemMasters> UnMappedProductSizeInfoDetails(string id, string hubId);
        List<ItemMasters> ProductColorInfoDetails(string id);
        //List<ProductPriceLog> GetProductPriceLog(string id, string hubId);

        string ItemSizeInfo(ItemMasters variance);
        bool hubItemSizeInfo(ItemMasters variance);
        int ItemSizeDelete(string priceId, string Condition,string hubId);
        int ItemWeightQtyDelete(string priceId, string Condition,string hubId);
        int CreateColor(ItemColorInfo info);
        int ItemColorDelete(string ColorId, string Condition);
       
        int InsertMappingValue(SizeColorData data);
        int EditMappingValue(SizeColorData data);
        List<ColorSizeMapping> GetColorSizeMap(string ItemId);

        ItemSizeInfo GetSizeInfoDetails(int id, string hubId);

        int UpdateSizeDetail(ItemSizeInfo info);
        int AddItem(ItemMasters info);
        int RemovalExitsVarainttoHub(ItemMasters info);

        List<ItemMasters> HubProductSizeInfoDetails(string id, string hubId);
        Task<List<ItemMasters>> HubProductColorInfoDetails(string id, string hubId);
        ItemMasters GEtItemCountDashboard(string hubId);
        // List<ItemMasters> GetItemDetails(string datefrom, string dateto, string id);
        List<ItemMasters> GetvegItemDetails(string datefrom, string dateto, string id, int type);

        List<ItemMasters> Itemvegnonvegcount(string datefrom, string dateto, string id);

        int AddNonVegCategory(ItemMasters Item);
        List<ItemMasters> GetItemLanguage(ItemMasters item, string id, int Id);
        int uploadimagecreateiteam(string id, string type);
        int uploadimageCount(string id, int count);

        string CheckUniqueBarcode(string Barcode);
        LanguageMst GetItemLanguageById(int id);
        int DeleteItemLanguageByitemId(int id);
        List<ProductType> GetProductList(string id);
        List<ProductVariance> GetProductVarianceById(string Id,string HubId);
        List<ProductSpec> GetProductSpecById(string Id,string productId,string HubId);
        List<ItemMasters> GetMappedItemList(string Id,string hubId);
        List<ItemMasters> GetMappedVarientItemList(string Id,string hubId,string maincategory);
        int UpdateVarientItemList(string MappedItemId, string ItemId, string hubId);

        string ProductSpec(ProductSpec spec);
    }
}
