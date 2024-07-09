using Freshlo.DomainEntities.Banner;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.RI
{
   public interface BannerRI
    {

        List<Banner> GetMancategorylist();
        List<SelectListItem> GetAcctionTriggerlist(string trigger);
        List<Banner> GetBannerList(string hubId);
        int CreateBanner(Banner info);
        int UpdaetBanner(Banner info);
        Banner GetbannerDetails(string id);
        bool DeleteBanner(int id);



    }
}
