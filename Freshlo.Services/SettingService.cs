using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using Freshlo.DomainEntities.Inventory;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Freshlo.Services
{
    public class SettingService : ISettingSI
    {
        private readonly ISettingRI _settingRI;
        public SettingService(ISettingRI settingRI)
        {
            _settingRI = settingRI;
        }
        // Zip Code Related Here
        public Task<List<SelectListItem>> GetHubList()
        {
            return Task.Run(() =>
            {
                return _settingRI.GetHubList();
            });
        } 
       
        public Task<List<SelectListItem>> GettblPeferencelist()
        {
            return Task.Run(() =>
            {
                return _settingRI.GettblPerferncelist();
            });
        }
        public Task<List<CustomersAddress>> GetZiplist(string id)
        {
            return Task.Run(() =>
            {
                return _settingRI.GetZipList(id);
            });
        }
        public Task<List<CustomersAddress>> Getpaymentsgatewaylist(string id)
        {
            return Task.Run(() =>
            {
                return _settingRI.Getpaymentsgatewaylist(id);
            });
        } 
        public Task<List<Recipe>> GetRecipeList(string id)
        {
            return Task.Run(() =>
            {
                return _settingRI.GetRecipeList(id);
            });
        }
        public Task<int> AddZipCode(CustomersAddress info)
        {
            return Task.Run(() =>
            {
                return _settingRI.AddZipCode(info);
            });
        }   
        public Task<int> Addpaymentsgateway(CustomersAddress info)
        {
            return Task.Run(() =>
            {
                return _settingRI.Addpaymentsgateway(info);
            });
        } 
        public Task<int> AddRecipe(Recipe info)
        {
            return Task.Run(() =>
            {
                return _settingRI.AddRecipe(info);
            });
        }
        public CustomersAddress GetZipCodeDetails(int id)
        {
            return _settingRI.GetZipCodeDetails(id);
        }
        public CustomersAddress GetpaymentgatewayDetails(int id)
        {
            return _settingRI.GetpaymentgatewayDetails(id);
        }
        public Recipe GetRecipeDetails(int id)
        {
            return _settingRI.GetRecipeDetails(id);
        }
        public Task<int> EditZipCode(CustomersAddress info)
        {
            return Task.Run(() =>
            {
                return _settingRI.EditZipCode(info);
            });
        }
        public Task<int> Editpayments(CustomersAddress info)
        {
            return Task.Run(() =>
            {
                return _settingRI.Editpayments(info);
            });
        } 
        public Task<int> EditRecipe(Recipe info)
        {
            return Task.Run(() =>
            {
                return _settingRI.EditRecipe(info);
            });
        }
        public Task<bool> DeleteZipcode(int id)
        {
            return Task.Run(() => {
                return _settingRI.DeleteZipcode(id);
            });
        }
        public Task<bool> Deletepayments(int id)
        {
            return Task.Run(() => {
                return _settingRI.Deletepayments(id);
            });
        }
        public Task<bool> DeleteRecipe(int id)
        {
            return Task.Run(() => {
                return _settingRI.DeleteRecipe(id);
            });
        }
        //Slot Related Here
        public Task<List<DeleiverySlot>> GetSlotList()
        {
            return Task.Run(() =>
            {
                return _settingRI.GetSlotList();
            });
        }
        public Task<int> AddSlot(DeleiverySlot info)
        {
            return Task.Run(() =>
            {
                return _settingRI.AddSlot(info);
            });
        }        
        public DeleiverySlot GetSlostDetail(int id)
        {
            return _settingRI.GetSlotDetails(id);
        }
        public Task<int> EditSlot(DeleiverySlot info)
        {
            return Task.Run(() =>
            {
                return _settingRI.EditSlot(info);
            });
        }        
        public Task<bool> DeleteSlot(int id)
        {
            return Task.Run(() =>
            {
                return _settingRI.DeleteSlot(id);
            });
        }
        //offer type Related Here
        public Task<List<OfferType>> GetofferList()
        {
            return Task.Run(() =>
            {
                return _settingRI.GetofferList();
            });
        }
        public OfferType GetOfferType(int id)
        {
            return _settingRI.GetOfferType(id);
        }
        public Task<int> AddOfferType(OfferType info)
        {
            return Task.Run(() =>
            {
                return _settingRI.AddOfferType(info);
            });
        }
        public Task<int> EditOfferType(OfferType info)
        {
            return Task.Run(() =>
            {
                return _settingRI.EditOfferType(info);
            });
        }
        // Brand Related Here
        public Task<List<BrandInfo>> GetBrandList()
        {
            return Task.Run(() =>
            {
                return _settingRI.GetBrandList();
            });
        }
        public Task<List<SelectListItem>> GetSupplierlist()
        {
            return Task.Run(() =>
            {
                return _settingRI.GetSupplierlist();
            });
        }
        public Task<List<string>> GetSupplierlist(int id)
        {
            return Task.Run(() =>
            {
                return _settingRI.GetSupplierlist(id);
            });
        }
        public Task<string> AddBrand(BrandInfo info)
        {
            return Task.Run(() =>
            {
                return _settingRI.AddBrand(info);
            });
        }
        public BrandInfo GetBrandDetails(int id)
        {
            return _settingRI.GetBrandDetails(id);
        }
        public Task<int> EditBrand(BrandInfo info)
        {
            return Task.Run(() =>
            {
                return _settingRI.EditBrand(info);
            });
        }
        public Task<bool> DeleteBrand(int id)
        {
            return Task.Run(() =>
            {
                return _settingRI.DeleteBrand(id);
            });
        }
        //Table Related Here
        public Task<List<TableInfo>> GetTableList(string id)
        {
            return Task.Run(() =>
            {
                return _settingRI.GetTableList(id);
            });
        }
        public Task<string> AddTable(TableInfo add)
        {
            return Task.Run(() =>
            {
                return _settingRI.AddTable(add);
            });
        }
        public TableInfo GetTableDetails(int id)
        {
            return _settingRI.GetTableDetails(id);
        }
        public Task<int> EditTable(TableInfo add)
        {
            return Task.Run(() =>
            {
                return _settingRI.EditTable(add);
            });
        }
        public Task<bool> DeleteTable(int id)
        {
            return Task.Run(() =>
            {
                return _settingRI.DeleteTable(id);
            });
        }
        //Hub Related Here
        public Task<List<Hub>> GetHubOrgList()
        {
            return Task.Run(() =>
            {
                return _settingRI.GetHubOrgList();
            });
        }
        public Task<int> AddHub(Hub info)
        {
            return Task.Run(() =>
            {
                return _settingRI.AddHub(info);
            });
        }
        public Task<int> EditHub(Hub info)
        {
            return Task.Run(() =>
            {
                return _settingRI.EditHub(info);
            });
        }
        public Hub GetHubDetails(int id)
        {
            return _settingRI.GetHubDetails(id);
        }
        public Task<Rootobject> geLongitude(string add)
        {
            return Task.Run(() =>
            {
                return getLongitude(add);
            });
        }
        public static Rootobject getLongitude(string add)
        {
            var Coordinate = new Rootobject();
            string address = "";
            address = "https://maps.google.com/maps/api/geocode/xml?address=" + add + "&sensor=false&key=AIzaSyBAN-dCYCJqreZNApUeS5LDXAcDym_2FGw";
            var result = new System.Net.WebClient().DownloadString(address);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            XmlNodeList parentNode = doc.GetElementsByTagName("location");
            foreach (XmlNode childrenNode in parentNode)
            {
                Coordinate.latitude = Convert.ToDouble(childrenNode.SelectSingleNode("lat").InnerText);
                Coordinate.longitude = Convert.ToDouble(childrenNode.SelectSingleNode("lng").InnerText);
            }
            return Coordinate;
        }
        public Task<bool> DeleteHub(int id)
        {
            return Task.Run(() => {
                return _settingRI.DeleteHub(id);
            });
        }
        // Password Policy 
        public Task<bool> SystemConfigUpdate(SecurityConfig securityConfig)
        {
            return Task.Run(() =>
            {
                return _settingRI.SystemConfigUpdate(securityConfig);
            });
        }
        // BusinessInfo Realted
        public BusinessInfo GetbusinessInfoDetails(int id)
        {
            return _settingRI.GetBusinessInfoDetails(id);
        }
        public Task<int> BusinessConfigUpdate(BusinessInfo businessConfig)
        {
            return Task.Run(() =>
            {
                return _settingRI.BusinessConfigUpdate(businessConfig);
            });
        }
        // TaxtionInfo Related
        public TaxationInfo GetTaxationInfo()
        {
            return _settingRI.GetTaxationInfo();
        }
        public Task<bool> TaxInfoUpdate(TaxationInfo taxConfig,string hubId)
        {
            return Task.Run(() =>
            {
                return _settingRI.TaxInfoUpdate(taxConfig,hubId);
            });
        }
        public List<CurrencyMST> GetConfigCurrencyList(string id)
        {
            return _settingRI.GetConfigCurrencyList(id);
        }
        public List<CurrencyMST> GetCurrencyList(string id)
        {
            return _settingRI.GetCurrencyList(id);
        }
        public CurrencyMST GetCurrencyDetails(int id)
        {
            return _settingRI.GetCurrencyDetails(id);
        }
        public Task<int> CurrencySymbolUpdate(string currency, string symbol, string hubId, string country)
        {
            return Task.Run(() =>
            {
                return _settingRI.CurrencySymbolUpdate(currency, symbol, hubId, country);
            });
        }
        public Task<bool> DeleteCurrency(int id)
        {
            return Task.Run(() =>
            {
                return _settingRI.DeleteCurrency(id);
            });
        }
        public Task<string> AddCurrency(CurrencyMST add)
        {
            return Task.Run(() =>
            {
                return _settingRI.AddCurrency(add);
            });
        }
        public Task<string> AddProductSpecs(ProductType pro)
        {
            return Task.Run(() =>
            {
                return _settingRI.AddProductSpecs(pro);
            });
        } 
        public Task<string> AddProductSubSpecs(ProductSpecSubCatgeory pro)
        {
            return Task.Run(() =>
            {
                return _settingRI.AddProductSubSpecs( pro);
            });
        }  
        public Task<string> UpdateProductSubSpecs(ProductSpecSubCatgeory pro)
        {
            return Task.Run(() =>
            {
                return _settingRI.UpdateProductSubSpecs( pro);
            });
        } 
        public Task<string> UpdateProductSpecs(ProductType pro)
        {
            return Task.Run(() =>
            {
                return _settingRI.UpdateProductSpecs(pro);
            });
        }
        public Task<string> DeleteProductSpecs(string Id, string hubId)
        {
            return Task.Run(() =>
            {
                return _settingRI.DeleteProductSpecs( Id,  hubId);
            });
        }  
        public Task<string> DeleteProductSUbSpecs(string Id, string hubId)
        {
            return Task.Run(() =>
            {
                return _settingRI.DeleteProductSUbSpecs( Id,  hubId);
            });
        }
        public Task<int> EditCurrency(CurrencyMST add)
        {
            return Task.Run(() =>
            {
                return _settingRI.EditCurrency(add);
            });
        }
        public List<TimeZoneDetails> getTimezonelist()
        {
            return _settingRI.getTimezonelist();
        }
        public Task<List<InventoryAsset>> Get_Inventory_Assets_List(string id)
        {
            return Task.Run(() =>
            {
                return _settingRI.Get_Inventory_Assets_List(id);
            });
        }
        public Task<int> Delete_Inventory_Asset(int id)
        {
            return Task.Run(() =>
            {
                return _settingRI.Delete_Inventory_Asset(id);
            });
        }
        public InventoryAsset InventoryDetails(int Id, string id)
        {
            return _settingRI.InventoryDetails(Id, id);
        }
        public Task<int> UpdateInventory(InventoryAsset Add)
        {
            return Task.Run(() =>
            {
                return _settingRI.UpdateInventory(Add);
            });
        }
        public Task<int> CreateInventory(InventoryAsset Add)
        {
            return Task.Run(() =>
            {
                return _settingRI.CreateInventory(Add);
            });
        }
        public int Languagechange(LanguageMst Add)
        {
                return _settingRI.Languagechange(Add);
        }
        public Task<int> Addlanguage(ItemMasters Add)
        {
            return Task.Run(() =>
            {
                return _settingRI.Addlanguage(Add);
            });
        }
        public Task<List<ItemMasters>> selectlanguage(string id)
        {
            return Task.Run(() =>
            {
                return _settingRI.selectlanguage(id);
            });
        }          
        public Task<List<ItemMasters>> languageselect(string id)
        {
            return Task.Run(() =>
            {
                return _settingRI.languageselect(id);
            });
        }
        public Task<int> deleteLanguage(int id)
        {
            return Task.Run(() =>
            {
                return _settingRI.deleteLanguage(id);
            });
        }
        public BusinessInfo gethublist(string id)
        {
            return _settingRI.gethublist(id);
        }
        public Task<List<ProductType>> GetProductMainList(string Id)
        {
            return Task.Run(() =>
            {
                return _settingRI.GetProductMainList(Id);
            });
        }
        public Task<List<ProductSpecSubCatgeory>> GetProductSpecSucCategoryList(string Id, string ProductId)
        {
            return Task.Run(() =>
            {
                return _settingRI.GetProductSpecSucCategoryList(Id, ProductId);
            });
        }
    }    
}
