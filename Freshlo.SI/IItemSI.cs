using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Freshlo.SI
{
    public interface IItemSI
    {
        List<ItemCategory> GetCategories(string id);
        Task<List<ItemCategory>> GetCategoriesAsync(string id);
        Task<string> CreateItemMaster(ItemMasters Item);
        Task<List<SelectListItem>> GetCategoryNamebyMainCate(string mainCatId,string id);
        Task<List<SelectListItem>> GetMainCategoryList(string id);
        Task<List<SelectListItem>> GetSubCategoryListbyCat(string CatId,string id);
        Task<List<SelectListItem>> GetSupplierNameList();
        Task<List<SelectListItem>> GetBrandList();
        Task<int> CreatePriceMaster(ItemMasters itemMasters);
        Task<int> CreateStockMaster(ItemMasters itemMasters);
        Task<int> CreateWastageMaster(ItemMasters itemMasters);
        Task<List<ItemMasters>> GetItemManageData(ItemMasters item);
        Task<List<SelectListItem>> GetSubCategoryList(string id);
        Task<ItemMasters> GetItemCountDetail();
        Task<ItemMasters> GetItemMasterDetail(int Id);
        Task<ItemMasters> HubGetItemMasterDetail(int Id);
        Task<string> UpdateItemMaster(ItemMasters itemMasters);
        Task<int> UpdatePriceMaster(ItemMasters itemMasters);
        Task<int> UpdateItemFeatured(string feature, string itemId, int type, int Id);
        Task<int> UpdateItemCheckSp(string checkSp, string itemId, int type, int Id);
        Task<int> UpdateItemApproval(string itemId, int Id, string approval, string UpdatedBy, int type);
        Task<string> CheckUniquePlucode(string PluCode);
        void UpdateItemFields(ItemFields list, string updatedBy);
        //Task<List<SelectListItem>> GetItemTypeList();
        //Task<List<ItemMasters>> GetPricelistData(ItemMasters item);
        Task<int> UpdateItemAvailability(string season, string itemId, int type, int Id);
        Task<int> UpdateItemCoupen(string coupen, string itemId, int type, int Id);
        Task<List<TaxPercentageMst>> GetTaxPercentageList();
        Task<int> UpdateHubPrice(Item detail);
        Task<List<ItemMasters>> GetItemManagePriceData(ItemMasters item);
        Task<List<ItemMasters>> GetIncludedHubPrice(string id);
        //Task<List<ItemMasters>> GetExcludedHubPrice(int itemId,ItemMasters item);
        Task<List<ItemMasters>> GetExcludedHubPrice(string id);
        Task<int> DeleteHubItem(string ItemId);
        Task<int> DeleteMapItem(string itemId, string mappingItemId, string hubId);
        Task<int> DeleteItem(ItemMasters info);
        Task<byte[]> FilterExcelofItem(ItemMasters item);
        Task<List<ProductPriceLog>> GetProductPriceLog(string id, string hubId);
        Task<List<SelectListItem>> GetfoodTypeList();
        Task<List<SelectListItem>> GetsubFood();
        Task<List<SelectListItem>> GetSubfoodBymainFood(string foodType);
        Task<List<int>> GetMappingDaywithItem(string id);
        Task<List<ItemMasters>> ProductSizeInfoDetails(string Id, string hubId);
        Task<List<ItemMasters>> UnMappedProductSizeInfoDetails(string Id, string hubId);
        Task<List<ItemMasters>> ProductColorInfoDetails(string Id);
        //Task<List<ProductPriceLog>> GetProductPriceLog(string id, string hubId);
        //Task<bool> SizeInfo(ItemMasters variance);
        Task<string> SizeInfo(ItemMasters variance);
        bool HubSizeInfo(ItemMasters variance);
        //Task<bool> ColorInfo(ItemMasters variance);
        Task<int> ItemSizeDelete(string PriceId, string Conditoin,string hubId);        
        Task<int> ItemWeightQtyDelete(string PriceId, string Condition,string hubId);
        int CreateColor(ItemColorInfo info);
        Task<int> ItemColorDelete(string ColorId, string Conditoin);
        int InsertMappingValue(SizeColorData data);
        int EditMappingValue(SizeColorData data);
        List<ColorSizeMapping> GetColorSizeMap(string ItemId);
        Task<ItemSizeInfo> GetSizeInfoDetails(int id, string hubId);
        Task<int> UpdateSizeDetail(ItemSizeInfo info);
        Task<int> AddItem(ItemMasters info);
        Task<int> RemovalExitsVarainttoHub(ItemMasters info);
        Task<List<ItemMasters>> HubProductSizeInfoDetails(string Id, string hubId);
        Task<ItemMasters> GEtItemCountDashboard(string hubId);
        // Task<List<ItemMasters>> GetItemDetails(string datefrom, string dateto, string id);
        Task<List<ItemMasters>> GetvegItemDetails(string datefrom, string dateto, string id, int type);
        Task<List<ItemMasters>> Itemvegnonvegcount(string datefrom, string dateto, string id);
        Task<int> AddNonVegCategory(ItemMasters Item);
        Task<List<ItemMasters>> GetItemLanguage(ItemMasters item, string id, int Id);
        int uploadimagecreateiteam(string id, string type);
        int uploadimageCount(string id, int count);
        Task<string> CheckUniqueBarcode(string Barcode);
        LanguageMst GetItemLanguageById(int id);
        Task<int> DeleteItemLanguageByitemId(int id);

        Task<List<ProductType>> GetProductList(string id);
        Task<List<ProductVariance>> GetProductVarianceById(string Id ,string HubId);
     
        Task<List<ItemMasters>> GetMappedItemList(string Id, string HubId);
        Task<List<ItemMasters>> GetMappedVarientItemList(string Id, string HubId, string maincategory);
        Task<int> UpdateVarientItemList(string MappedItemId, string ItemId, string  hubId);
        Task<List<ProductSpec>> GetProductSpecById(string Id,string ProductId ,string HubId);
        Task<string> ProductSpec(ProductSpec spec);

    }
}
