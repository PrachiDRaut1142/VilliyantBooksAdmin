using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using Freshlo.DomainEntities.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Freshlo.RI
{
    public interface ISettingRI
    {
        // Ziplist Related Here...
        List<SelectListItem> GetHubList();
      
        List<SelectListItem> GettblPerferncelist();
        List<CustomersAddress> GetZipList(string id);
        List<CustomersAddress> Getpaymentsgatewaylist(string id);
        List<Recipe> GetRecipeList(string id);
        int AddZipCode(CustomersAddress info);
        int Addpaymentsgateway(CustomersAddress info);
        int AddRecipe(Recipe info);
        CustomersAddress GetZipCodeDetails(int id);
        CustomersAddress GetpaymentgatewayDetails(int id);
        Recipe GetRecipeDetails(int id);
        int EditZipCode(CustomersAddress info);
        int Editpayments(CustomersAddress info);
        int EditRecipe(Recipe info);
        bool DeleteZipcode(int id);
        bool Deletepayments(int id);
        bool DeleteRecipe(int id);

        // Slot Related Here...
        List<DeleiverySlot> GetSlotList();
        int AddSlot(DeleiverySlot info);
        DeleiverySlot GetSlotDetails(int id);
        int EditSlot(DeleiverySlot info);
        bool DeleteSlot(int id);
        // offer type Related Here...
        List<OfferType> GetofferList();
        OfferType GetOfferType(int id);
        int AddOfferType(OfferType info);
        int EditOfferType(OfferType info);
        // Brand Related Hree...
        List<BrandInfo> GetBrandList();
        List<SelectListItem> GetSupplierlist();
        List<string> GetSupplierlist(int id);
        string AddBrand(BrandInfo info);
        BrandInfo GetBrandDetails(int id);
        int EditBrand(BrandInfo info);
        bool DeleteBrand(int id);

        // Hub Related Heree....
        List<TableInfo> GetTableList(string id);
        string AddTable(TableInfo info);
        TableInfo GetTableDetails(int id);
        int EditTable(TableInfo info);
        bool DeleteTable(int id);

        // Hub Related Here...
        List<Hub> GetHubOrgList();
        int AddHub(Hub info);
        int EditHub(Hub info);
        bool DeleteHub(int id);
        Hub GetHubDetails(int id);

        // Password Policy Related Hree...
        bool SystemConfigUpdate(SecurityConfig securityConfig);

        // BusinessInfo Related Here...
        BusinessInfo GetBusinessInfoDetails(int id);
        int BusinessConfigUpdate(BusinessInfo businessConfig);

        // TaxationInfo Related Here...
        TaxationInfo GetTaxationInfo();
        bool TaxInfoUpdate(TaxationInfo taxConfig,string hubId);
        List<CurrencyMST> GetCurrencyList(string id);
        List<CurrencyMST> GetConfigCurrencyList(string id);
        CurrencyMST GetCurrencyDetails(int Id);
        int CurrencySymbolUpdate(string currency, string shortCode, string hubId, string country);
        bool DeleteCurrency(int id);
        string AddCurrency(CurrencyMST add);
        string AddProductSpecs(ProductType pro);
        string AddProductSubSpecs(ProductSpecSubCatgeory pro);
        string UpdateProductSubSpecs(ProductSpecSubCatgeory pro);
        string UpdateProductSpecs(ProductType pro);
        string DeleteProductSpecs(string Id, string hubId);
        string DeleteProductSUbSpecs(string Id, string hubId);
        int EditCurrency(CurrencyMST add);
        List<TimeZoneDetails> getTimezonelist();
        List<InventoryAsset> Get_Inventory_Assets_List(string id);
        //List<InventoryAsset> Create_Inventory_Asset();
        int Delete_Inventory_Asset(int id);
        InventoryAsset InventoryDetails(int Id, string id);
        int UpdateInventory(InventoryAsset Add);
        int CreateInventory(InventoryAsset Add);
        int Languagechange(LanguageMst Add);
        int Addlanguage(ItemMasters Add);
        List<ItemMasters> selectlanguage(string id);
        List<ItemMasters> languageselect(string id);
        int deleteLanguage (int id);
        BusinessInfo gethublist(string id);
        List<ProductType> GetProductMainList(string Id);
        List<ProductSpecSubCatgeory> GetProductSpecSucCategoryList(string Id, string ProductId);
    }
}
