using Freshlo.DomainEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.Sale
{
    public class SalesManageVM :BaseViewModel
    {
        public List<CurrencyMST> getCurrencysybmollist;

        public List<Sales> GetSalesList { get; set; }
        public List<Item> getcatlist { get; set; }

        public List<DropDown> OrderStatus { get; set; }
        public List<SelectListItem> Zipcode { get; set; }
        public SalesCountData SalesCount { get; set; }
        public int a { get; set; }
        public int b { get; set; }
        public string c { get; set; }
        public string d { get; set; }
        public List<Sales> GetFinacialSalesList { get; set; }
        public List<SalesList> GetKitcheOrderList { get; set; }
        public List<SalesList> GetTableStateList { get; set; }
        public List<SalesList> GetTableStateListTKWY { get; set; }
        public List<SalesList> GetTableStateListHOD { get; set; }

        public List<SelectListItem> GetCoupenList { get; set; }
        public List<SelectListItem> Getperferncelist { get; set; }
        public List<Item> getmainList { get; set; }

        public List<SalesList> GetPendingKOTList { get; set; }
        public List<SalesList> AllitemSalesList { get; set; }
        public Task<TableInfo> bookingCount { get; set; }
        public List<TableInfo> bookinglist { get; internal set; }
        public List<SelectListItem> GetHubList { get; set; }
    }
}
