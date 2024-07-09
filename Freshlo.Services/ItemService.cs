using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Freshlo.Services
{
    public class ItemService : IItemSI
    {
        public ItemService(IItemRI itemRI)
        {
            _itemRI = itemRI;
        }
        private IItemRI _itemRI;
        public List<ItemCategory> GetCategories(string id)
        {
            return _itemRI.GetCategories(id);
        }
        public Task<List<ItemCategory>> GetCategoriesAsync(string id)
        {
            return Task.Run(() =>
            {
                return GetCategories(id);
            });
        }
        public Task<string> CreateItemMaster(ItemMasters Item)
        {
            return Task.Run(() =>
            {
                return _itemRI.CreateItemMaster(Item);
            });
        }
        public Task<List<SelectListItem>> GetCategoryNamebyMainCate(string mainCatId,string id)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetCategoryNamebyMainCate(mainCatId,id);
            });
        }
        public Task<List<SelectListItem>> GetMainCategoryList(string id)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetMainCategoryList(id);
            });
        }
        public Task<List<SelectListItem>> GetSubCategoryListbyCat(string CatId,string id)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetSubCategoryListbyCat(CatId,id);
            });
        }
        public Task<List<SelectListItem>> GetSupplierNameList()
        {
            return Task.Run(() =>
            {
                return _itemRI.GetSupplierNameList();
            });
        }
        public Task<List<SelectListItem>> GetBrandList()
        {
            return Task.Run(() =>
            {
                return _itemRI.GetBrandList();
            });
        }
        public Task<int> CreatePriceMaster(ItemMasters itemMasters)
        {
            return Task.Run(() =>
            {
                return _itemRI.CreatePriceMaster(itemMasters);
            });
        }
        public Task<int> CreateStockMaster(ItemMasters itemMasters)
        {
            return Task.Run(() =>
            {
                return _itemRI.CreateStockMaster(itemMasters);
            });
        }
        public Task<int> CreateWastageMaster(ItemMasters itemMasters)
        {
            return Task.Run(() =>
            {
                return _itemRI.CreateWastageMaster(itemMasters);
            });
        }
        public Task<List<ItemMasters>> GetItemManageData(ItemMasters item)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetItemManageData(item);
            });
        }
        public Task<List<SelectListItem>> GetSubCategoryList(string id)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetSubCategoryList(id);
            });
        }
        public Task<ItemMasters> GetItemCountDetail()
        {
            return Task.Run(() =>
            {
                return _itemRI.GetItemCountDetail();
            });
        }
        public Task<ItemMasters> GetItemMasterDetail(int Id)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetItemMasterDetail(Id);
            });
        }
        public Task<ItemMasters> HubGetItemMasterDetail(int Id)
        {
            return Task.Run(() =>
            {
                return _itemRI.HubGetItemMasterDetail(Id);
            });
        }
        public Task<string> UpdateItemMaster(ItemMasters itemMasters)
        {
            return Task.Run(() =>
            {
                return _itemRI.UpdateItemMaster(itemMasters);
            });
        }
        public Task<int> UpdatePriceMaster(ItemMasters itemMasters)
        {
            return Task.Run(() =>
            {
                return _itemRI.UpdatePriceMaster(itemMasters);
            });
        }
        public Task<int> UpdateItemFeatured(string feature, string itemId, int type, int Id)
        {
            return Task.Run(() =>
            {
                return _itemRI.UpdateItemFeatured(feature, itemId, type, Id);
            });
        }
        public Task<int> UpdateItemCheckSp(string checkSp, string itemId, int type, int Id)
        {
            return Task.Run(() =>
            {
                return _itemRI.UpdateItemCheckSp(checkSp, itemId, type, Id);
            });
        }
        public Task<int> UpdateItemApproval(string itemId, int Id, string approval, string UpdatedBy, int type)
        {
            return Task.Run(() =>
            {
                return _itemRI.UpdateItemApproval(itemId, Id, approval, UpdatedBy, type);
            });
        }
        public Task<string> CheckUniquePlucode(string PluCode)
        {
            return Task.Run(() =>
            {
                return _itemRI.CheckUniquePlucode(PluCode);
            });
        }
        public void UpdateItemFields(ItemFields list, string updatedBy)
        {
            Task.Run(() =>
            {
                _itemRI.UpdateItemFields(list, updatedBy);
            });
        }
        //public Task<List<SelectListItem>> GetItemTypeList()
        //{
        //    return Task.Run(() =>
        //    {
        //        return _itemRI.GetItemTypeList();
        //    });
        //}
        //public Task<List<ItemMasters>> GetPricelistData(ItemMasters item)
        //{
        //    return Task.Run(() =>
        //    {
        //        return _itemRI.GetPricelistData(item);
        //    });
        //}
        public Task<int> UpdateItemAvailability(string season, string itemId, int type, int Id)
        {
            return Task.Run(() =>
            {
                return _itemRI.UpdateItemAvailability(season, itemId, type, Id);
            });
        }
        public Task<int> UpdateItemCoupen(string coupen, string itemId, int type, int Id)
        {
            return Task.Run(() =>
            {
                return _itemRI.UpdateItemCoupen(coupen, itemId, type, Id);
            });
        }
        public Task<List<TaxPercentageMst>> GetTaxPercentageList()
        {
            return Task.Run(() =>
            {
                return _itemRI.GetTaxPercentageList();
            });
        }
        public Task<int> UpdateHubPrice(Item detail)
        {
            return Task.Run(() =>
            {
                return _itemRI.UpdateHubPrice(detail);
            });
        }
        public Task<List<ItemMasters>> GetItemManagePriceData(ItemMasters item)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetItemManagePriceData(item);
            });
        }
        public Task<List<ItemMasters>> GetIncludedHubPrice(string id)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetIncludedHubPrice(id);
            });
        }
        //public Task<List<ItemMasters>> GetExcludedHubPrice(int itemId, ItemMasters item )
        //{
        //    return Task.Run(() =>
        //    {
        //        return _itemRI.GetExcludedHubPrice(itemId,item);
        //    });
        //}
        public Task<List<ItemMasters>> GetExcludedHubPrice(string id)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetExcludedHubPrice(id);
            });
        }
        public Task<int> DeleteHubItem(string itemId)
        {
            return Task.Run(() =>
            {
                return _itemRI.DeleteHubItem(itemId);
            });
        } 
        public Task<int> DeleteMapItem(string itemId, string mappingItemId, string hubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.DeleteMapItem( itemId,  mappingItemId,  hubId);
            });
        }
        public Task<int> DeleteItem(ItemMasters info)
        {
            return Task.Run(() =>
            {
                return _itemRI.DeleteItem(info);
            });
        }
        public Task<byte[]> FilterExcelofItem(ItemMasters item)
        {
            return Task.Run(() =>
            {
                return _itemRI.FilterExcelofItems(item);
            });
        }
        public Task<List<ProductPriceLog>> GetProductPriceLog(string id, string hubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetProductPriceLog(id, hubId);
            });
        }
        public Task<List<SelectListItem>> GetfoodTypeList()
        {
            return Task.Run(() =>
            {
                return _itemRI.GetfoodTypeList();
            });
        }
        public Task<List<SelectListItem>> GetSubfoodBymainFood(string foodType)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetSubfoodBymainFood(foodType);
            });
        }
        public Task<List<SelectListItem>> GetsubFood()
        {
            return Task.Run(() =>
            {
                return _itemRI.GetSubfoods();
            });
        }
        public Task<List<int>> GetMappingDaywithItem(string id)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetMappingDaywithItem(id);
            });
        }
        public Task<List<ItemMasters>> ProductSizeInfoDetails(string Id, string HubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.ProductSizeInfoDetails(Id, HubId);
            });
        }
        public Task<List<ItemMasters>> UnMappedProductSizeInfoDetails(string Id, string HubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.UnMappedProductSizeInfoDetails(Id, HubId);
            });
        }
        public Task<List<ItemMasters>> ProductColorInfoDetails(string Id)
        {
            return Task.Run(() =>
            {
                return _itemRI.ProductColorInfoDetails(Id);
            });
        }
        //public Task<List<ProductPriceLog>> GetProductPriceLog(string id, string hubId)
        //{
        //    return Task.Run(() =>
        //    {
        //        return _itemRI.GetProductPriceLog(id, hubId);
        //    });
        //}
        //public Task<bool> SizeInfo(ItemMasters variance)
        //{
        //    return Task.Run(() =>
        //    {
        //        return _itemRI.ItemSizeInfo(variance);
        //    });
        //}
        public Task<string> SizeInfo(ItemMasters variance)
        {
            return Task.Run(() =>
            {
                return _itemRI.ItemSizeInfo(variance);
            });            
        }
        public bool HubSizeInfo(ItemMasters variance)
        {
            return _itemRI.hubItemSizeInfo(variance);
        }
        //public Task<bool> ColorInfo(ItemMasters variance)
        //{
        //    return Task.Run(() =>
        //    {
        //        return _itemRI.ItemColorInfo(variance);
        //    });
        //}
        public Task<int> ItemSizeDelete(string PriceId, string Condition,string hubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.ItemSizeDelete(PriceId, Condition,hubId);
            });
        }
        public Task<int> ItemWeightQtyDelete(string PriceId, string Condition,string hubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.ItemWeightQtyDelete(PriceId, Condition,hubId);
            });
        }
        public int CreateColor(ItemColorInfo info)
        {
            return _itemRI.CreateColor(info);
        }
        public Task<int> ItemColorDelete(string ColorId, string Condition)
        {
            return Task.Run(() =>
            {
                return _itemRI.ItemColorDelete(ColorId, Condition);
            });
        } 
       
        public int InsertMappingValue(SizeColorData data)
        {
            return _itemRI.InsertMappingValue(data);
        }
        public int EditMappingValue(SizeColorData data)
        {
            return _itemRI.EditMappingValue(data);
        }
        public List<ColorSizeMapping> GetColorSizeMap(string ItemId)
        {
            return _itemRI.GetColorSizeMap(ItemId);
        }
        public Task<ItemSizeInfo> GetSizeInfoDetails(int id, string hubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetSizeInfoDetails(id, hubId);
            });
        }
        public Task<int> UpdateSizeDetail(ItemSizeInfo info)
        {
            return Task.Run(() =>
            {
                return _itemRI.UpdateSizeDetail(info);
            });
        }
        public Task<int> AddItem(ItemMasters info)
        {
            return Task.Run(() =>
            {
                return _itemRI.AddItem(info);
            });
        }
        public Task<int> RemovalExitsVarainttoHub(ItemMasters info)
        {
            return Task.Run(() =>
            {
                return _itemRI.RemovalExitsVarainttoHub(info);
            });
        }
        public Task<List<ItemMasters>> HubProductSizeInfoDetails(string Id, string hubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.HubProductSizeInfoDetails(Id, hubId);
            });
        }
        public Task<List<ItemMasters>> HubProductColorInfoDetails(string Id, string hubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.HubProductColorInfoDetails(Id, hubId);
            });
        }
        public Task<ItemMasters> GEtItemCountDashboard(string hubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.GEtItemCountDashboard(hubId);
            });
        }
        //public Task<List<ItemMasters>> GetItemDetails(string datefrom, string dateto, string id)
        //{
        //    return Task.Run(() =>
        //    {
        //        return _itemRI.GetItemDetails(datefrom, dateto, id);
        //    });
        //}
        public Task<List<ItemMasters>> GetvegItemDetails(string datefrom, string dateto, string id, int type)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetvegItemDetails(datefrom, dateto, id, type);
            });
        }
        public Task<List<ItemMasters>> Itemvegnonvegcount(string datefrom, string dateto, string id)
        {
            return Task.Run(() =>
            {
                return _itemRI.Itemvegnonvegcount(datefrom, dateto, id);
            });
        }
        public Task<int> AddNonVegCategory(ItemMasters Item)
        {
            return Task.Run(() =>
            {
                return _itemRI.AddNonVegCategory(Item);
            });
        }
        public Task<List<ItemMasters>> GetItemLanguage(ItemMasters item, string id, int Id)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetItemLanguage(item, id, Id);
            });
        }
        public int uploadimagecreateiteam(string id, string type)
        {
            return _itemRI.uploadimagecreateiteam(id, type);
        } 
        public int uploadimageCount(string id, int count)
        {
            return _itemRI.uploadimageCount( id,  count);
        }
        public Task<string> CheckUniqueBarcode(string Barcode)
        {
            return Task.Run(() =>
            {
                return _itemRI.CheckUniqueBarcode(Barcode);
            });
        }
        public LanguageMst GetItemLanguageById(int id)
        {
                return _itemRI.GetItemLanguageById(id);
        }
        public Task<int> DeleteItemLanguageByitemId(int id)
        {
            return Task.Run(() =>
            {
                return _itemRI.DeleteItemLanguageByitemId(id);
            });
        }
        public Task<List<ProductType>> GetProductList(string id)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetProductList(id);
            });
        }
        public Task<List<ProductVariance>> GetProductVarianceById(string Id,string HubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetProductVarianceById(Id,HubId);
            });
        }
        public Task<List<ItemMasters>> GetMappedItemList(string Id,string HubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetMappedItemList(Id, HubId);
            });
        } 
        public Task<List<ItemMasters>> GetMappedVarientItemList(string Id,string HubId, string maincategory)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetMappedVarientItemList(Id, HubId, maincategory);
            });
        }
        public Task<int> UpdateVarientItemList(string MappedItemId, string ItemId, string hubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.UpdateVarientItemList(MappedItemId, ItemId, hubId);
            });
        }

        public Task<List<ProductSpec>> GetProductSpecById(string Id, string ProductId, string HubId)
        {
            return Task.Run(() =>
            {
                return _itemRI.GetProductSpecById(Id, ProductId, HubId);
            });
        }

        public Task<string> ProductSpec(ProductSpec spec)
        {
            return Task.Run(() =>
            {
                return _itemRI.ProductSpec(spec);
            });
        }
    }
}
