using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Vendor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.VendorVM
{
    public class VendorVm : BaseViewModel
    {
        public List<Vendor> getAllVendorList { get; set; }
        public List<SelectListItem> GetMainCatlist { get; set; }
        public Vendor getvendordetails { get; set; }
        public List<int> GetCategorieslit { get; set; }
    }
}
