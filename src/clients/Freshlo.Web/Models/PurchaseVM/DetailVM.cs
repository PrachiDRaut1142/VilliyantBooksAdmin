using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Purchase;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.PurchaseVM
{
    public class DetailVM
    {
        public Purchase Detail { get; set; }
        public List<PurchaseList> List { get; set; }
        public List<SelectListItem> SupplierList { get; set; }

        public List<Purchase> purchaselist { get; set; }

        public List<PurchaseList> pdfPurchslist { get; set; }

        public List<PricelistCategory> GetMainCategoryList { get; set; }
        public List<PricelistCategory> getCattegorylist { get; set; }

        public List<PricelistCategory> getBrandList { get; set; }
        public List<PricelistCategory> getVendorList { get; set; }
    }
}
