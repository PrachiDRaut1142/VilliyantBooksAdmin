using Freshlo.DomainEntities;
using Freshlo.DomainEntities.PriceList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.RI
{
    public interface IPricelistRI
    {
        List<PricelistCategory> GetMainCategoryList();
        List<PricelistCategory> GetCategories();
        List<PricelistCategory> GetCategoriesext();
        List<SelectListItem> GetItemTypeList();
        List<PriceList> GetPricelistData(PricelistFilter detail);
        List<PriceList> GetPricelistDataforCook(PricelistFilter detail);

        void UpdatePricelist(PriceList list);
        //List<PriceList> GetPricelistData();

        List<PricelistCategory> GetBrandList();
        List<PricelistCategory> GetVendorList();
        List<PriceList> GetHubPricelist(PricelistFilter detail);
        void HubUpdatePrice(PriceList list);


    }
}
