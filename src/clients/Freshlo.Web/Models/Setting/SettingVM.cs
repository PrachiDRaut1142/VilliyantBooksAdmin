using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.Setting
{
    public class SettingVM : BaseViewModel
    {
        public List<SelectListItem> GetHubList { get; set; }
        public List<SelectListItem> Gettblperferncelist { get; set; }
        public List<SelectListItem> GetSupplier { get; set; }
        public List<CustomersAddress> GetZipCodelist { get; set; }
        public List<CustomersAddress> GetPayment_gateway_List { get; set; }
        public List<DeleiverySlot> GetSlotlist { get; set; }
        public List<BrandInfo> Getbrandlist { get; set; }
        public List<TableInfo> Gettablelist { get; set; }
        public List<Hub> GetOrgHubList { get; set; }
        public BrandInfo getbrandetails { get; set; }
        public TableInfo gettableetails { get; set; }
        public CurrencyMST getcurrencyInfoDetails { get; set; }

        public List<string> GetSupplierlist { get; set; }
        public SecurityConfig GetpasswordSecurityDetail { get; set; }
        public BusinessInfo getBusinessData { get; set; }
        public List<CurrencyMST> getCurrencyList { get; set; }
        public List<ItemMasters> getlanguagelist { get; set; }
        public List<ItemMasters> getaddedlanguagelist { get; set; }
        public List<ProductType> ProductMainList { get; set; }
    }
}
