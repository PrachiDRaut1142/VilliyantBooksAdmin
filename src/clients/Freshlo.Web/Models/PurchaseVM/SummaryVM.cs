using Freshlo.DomainEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.PurchaseVM
{
    public class SummaryVM
    {
        public List<PricelistCategory> Categorylist { get; set; }
        public List<SelectListItem> SubCategorylist { get; set; }
        public List<SelectListItem> MainCategorylist { get; set; }


    }
}
