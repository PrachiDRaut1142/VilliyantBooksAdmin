using Freshlo.DomainEntities;
using Freshlo.DomainEntities.PriceList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface IPricelistSI
    {
        
        Task<List<PricelistCategory>> GetMainCategoryList();
        Task<List<PricelistCategory>> GetCategoryListext();
        List<PricelistCategory> GetCategories();
        Task<List<PricelistCategory>> GetCategoriesAsync(); 
        Task<List<SelectListItem>> GetItemTypeList();
        Task<List<PriceList>> GetPricelistData(PricelistFilter detail);
        Task<List<PriceList>> GetPricelistDataforCook(PricelistFilter detail);

        //Task<List<PriceList>> GetPricelistData();
        void UpdaetPricelist(PriceList list);
        Task<List<PricelistCategory>> GetBrandList();
        Task<List<PricelistCategory>> GetVendorList();
        Task<List<PriceList>> GetHubPricelist(PricelistFilter detail);
        void HubUpdatePrice(PriceList list);

    }
}
