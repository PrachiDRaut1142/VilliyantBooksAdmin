using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Offer;
using Freshlo.DomainEntities.PriceList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.LiveOfferVM
{
    public class LiveOfferVM : BaseViewModel
    {
        public List<Offer> getLiveOfferList { get; set; }

        public List<PriceList> getAllpricelist { get; set; }
        public List<PriceList> getoffer { get; set; }

        public List<PriceList> getMappedPricelist { get; set; }

        public Offer OffersDetail { get; set; }

        public List<PricelistCategory> GetMainCategoryList { get; set; }
        public List<SelectListItem> GetMainCategoryList1 { get; set; }
        public List<SelectListItem> GetOfferTypeList { get; set; }
        public List<PricelistCategory> getCattegorylist { get; set; }

        public List<PricelistCategory> getBrandList { get; set; }
        public List<PricelistCategory> getVendorList { get; set; }
        public List<string> items { get; set; }
        public int selectedItem { get; set; }
    }
}
