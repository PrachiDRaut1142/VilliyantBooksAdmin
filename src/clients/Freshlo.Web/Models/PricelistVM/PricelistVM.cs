using Freshlo.DomainEntities;
using Freshlo.DomainEntities.PriceList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.PricelistVM
{
    public class PricelistVM :BaseViewModel
    {
        public List<PricelistCategory> GetMainCategoryList { get; set; }
        public List<PricelistCategory> getCattegorylist { get; set; }
        public List<PricelistCategory> getCattegorylistext { get; set; }
        public List<SelectListItem> GetitemType { get; set; }
        public List<PriceList> getItemPricelist { get; set; }
        public List<PriceList> getItemAvaliablePricelist { get; set; }


    }
}
