using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using Freshlo.DomainEntities.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface ISettingSI
    {
        // Zip Related Action Here..
        Task<List<SelectListItem>> GetHubList();
       
        Task<List<SelectListItem>> GettblPeferencelist();
        Task<List<CustomersAddress>> GetZiplist(string id);
        Task<List<CustomersAddress>> Getpaymentsgatewaylist(string id);
        Task<List<Recipe>> GetRecipeList(string id);
        Task<int> AddZipCode(CustomersAddress info);
        Task<int> Addpaymentsgateway(CustomersAddress info);
        Task<int> AddRecipe(Recipe info);
        CustomersAddress GetZipCodeDetails(int id);
        CustomersAddress GetpaymentgatewayDetails(int id);
        Recipe GetRecipeDetails(int id);
        Task<int> EditZipCode(CustomersAddress info);
        Task<int> Editpayments(CustomersAddress info);
        Task<int> EditRecipe(Recipe info);
        Task<bool> DeleteZipcode(int id);
        Task<bool> Deletepayments(int id);
        Task<bool> DeleteRecipe(int id);

        // Slot Related Action Here...
        Task<List<DeleiverySlot>> GetSlotList();
        Task<int> AddSlot(DeleiverySlot info);
        DeleiverySlot GetSlostDetail(int id);
        Task<int> EditSlot(DeleiverySlot info);        
        Task<bool> DeleteSlot(int id);
        // offer type Related Action Here...
        Task<List<OfferType>> GetofferList();
        OfferType GetOfferType(int id);
        Task<int> AddOfferType(OfferType info);
        Task<int> EditOfferType(OfferType info);
        //Brand Related Action Hree..
        Task<List<BrandInfo>> GetBrandList();
        Task<List<SelectListItem>> GetSupplierlist();
        Task<List<string>> GetSupplierlist(int id);
        Task<string> AddBrand(BrandInfo info);
        BrandInfo GetBrandDetails(int id);
        Task<int> EditBrand(BrandInfo info);
        Task<bool> DeleteBrand(int id);

        // Table Related Acton here..
        Task<string> AddTable(TableInfo add);
        TableInfo GetTableDetails(int id);
        Task<int> EditTable(TableInfo add);
        Task<bool> DeleteTable(int id);
        Task<List<TableInfo>> GetTableList(string id);

        // Hub Related Action Hrere...
        Task<List<Hub>> GetHubOrgList();
        Task<int> AddHub(Hub info);
        Task<int> EditHub(Hub info);
        Hub GetHubDetails(int id);
        Task<Rootobject> geLongitude(string add);
        Task<bool> DeleteHub(int id);

        // Password Poicy Related Hree...
        Task<bool> SystemConfigUpdate(SecurityConfig securityConfig);

        // BusinessInfo Details Realted Here...
        BusinessInfo GetbusinessInfoDetails(int id);
        Task<int> BusinessConfigUpdate(BusinessInfo businessConfig);

        // TaxationInfo Details Realted Here...
        TaxationInfo GetTaxationInfo();
        Task<bool> TaxInfoUpdate(TaxationInfo taxConfig,string hubId);

        // Currency Configuation Details Here...
        List<CurrencyMST> GetConfigCurrencyList(string id);
        List<CurrencyMST> GetCurrencyList(string id);
        CurrencyMST GetCurrencyDetails(int Id);
        Task<int> CurrencySymbolUpdate(string currency, string symbol, string hubId, string country);
        Task<bool> DeleteCurrency(int id);
        Task<string> AddCurrency(CurrencyMST add);
        Task<string> AddProductSpecs(ProductType pro);
        Task<string> AddProductSubSpecs(ProductSpecSubCatgeory pro);
        Task<string> UpdateProductSubSpecs(ProductSpecSubCatgeory pro);
        Task<string> UpdateProductSpecs(ProductType pro);
        Task<string> DeleteProductSpecs(string Id, string hubId);
        Task<string> DeleteProductSUbSpecs(string Id, string hubId);
        Task<int> EditCurrency(CurrencyMST add);
        List<TimeZoneDetails> getTimezonelist();
        Task<List<InventoryAsset>> Get_Inventory_Assets_List(string id);
        Task<int> Delete_Inventory_Asset(int id);
        InventoryAsset InventoryDetails(int Id, string id);
        Task<int> UpdateInventory(InventoryAsset Add);
        Task<int> CreateInventory(InventoryAsset Add);
        int Languagechange(LanguageMst Add);
        Task<int> Addlanguage(ItemMasters Add);
        Task<List<ItemMasters>> selectlanguage(string id);
        Task<List<ItemMasters>> languageselect(string id);
        Task<int> deleteLanguage(int id);
        BusinessInfo gethublist(string id);
        Task<List<ProductType>> GetProductMainList(string Id);
        Task<List<ProductSpecSubCatgeory>> GetProductSpecSucCategoryList(string Id, string ProductId);
    }
}
