using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Banner;
using Freshlo.DomainEntities.Hub;
using Freshlo.DomainEntities.Offer;
using Freshlo.DomainEntities.Vendor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.BannerVM
{
    public class BannerVM : BaseViewModel
    {
        public List<Banner> GetMainCategoryList { get; set; }
        public List<SelectListItem> GetAcctiontriggerlist { get; set; }
        public List<Banner> GetBannerList { get; set; }
        public int CreateBanner { get; set; }
        public Banner GetBanner { get; set; }
        public BusinessInfo businessInfo { get; set; }
        public List<Hub> GetHubList { get; set; }
    }
}
