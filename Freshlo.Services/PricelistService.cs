using Freshlo.DomainEntities;
using Freshlo.DomainEntities.PriceList;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class PricelistService : IPricelistSI
    {
        private IPricelistRI  _pricelistRI;
        public PricelistService(IPricelistRI pricelistRI)
        {
            _pricelistRI = pricelistRI;
        }

        //public Task<List<SelectListItem>> GetMainCategoryList()
        //{
        //    return Task.Run(() =>
        //    {
        //        return _pricelistRI.GetMainCategoryList();
        //    });
        //}


        public List<PricelistCategory> GetCategories()
        {
            return _pricelistRI.GetCategories();
        }

        public Task<List<PricelistCategory>> GetCategoriesAsync()
        {
            return Task.Run(() =>
            {
                return GetCategories();
            });
        }

        public Task<List<PricelistCategory>> GetCategoryListext()
        {
            return Task.Run(() =>
            {
                return _pricelistRI.GetCategoriesext();
            });
        }

        public Task<List<SelectListItem>> GetItemTypeList()
        {
            return Task.Run(() =>
            {
                return _pricelistRI.GetItemTypeList();
            });
        }

        public Task<List<PricelistCategory>> GetMainCategoryList()
        {
            return Task.Run(() =>
          {
                return _pricelistRI.GetMainCategoryList();
           });
        }


        public Task<List<PricelistCategory>> GetMainCategoryListE()
        {
            return Task.Run(() =>
            {
                return _pricelistRI.GetMainCategoryList();
            });
        }

        public Task<List<PriceList>> GetPricelistData(PricelistFilter detail)
        {
            return Task.Run(() =>
            {
                return _pricelistRI.GetPricelistData(detail);
            });
        }

        public Task<List<PriceList>> GetPricelistDataforCook(PricelistFilter detail)
        {
            return Task.Run(() =>
            {
                return _pricelistRI.GetPricelistDataforCook(detail);
            });
        }

        public void UpdaetPricelist(PriceList list)
        {
            Task.Run(() =>
            {
                _pricelistRI.UpdatePricelist(list);
            });
        }

        //public Task<List<PriceList>> GetPricelistData()
        //{
        //    return Task.Run(() =>
        //    {
        //        return _pricelistRI.GetPricelistData();
        //    });
        //}


        public Task<List<PricelistCategory>> GetBrandList()
        {
            return Task.Run(() =>
            {
                return _pricelistRI.GetBrandList();
            });
        }

        public Task<List<PricelistCategory>> GetVendorList()
        {
            return Task.Run(() =>
            {
                return _pricelistRI.GetVendorList();
            });
        }
        public Task<List<PriceList>> GetHubPricelist(PricelistFilter detail)
        {
            return Task.Run(() =>
            {
                return _pricelistRI.GetHubPricelist(detail);
            });
        }
        public void HubUpdatePrice(PriceList list)
        {
            _pricelistRI.HubUpdatePrice(list);
        }
    }
}
