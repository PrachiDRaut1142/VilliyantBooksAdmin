using Freshlo.DomainEntities.Banner;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
  public interface BannerSI
    {

        Task<List<Banner>> GetMancategoreylist();
        Task<List<SelectListItem>> GetActionTrigger(string trigger);
        Task<List<Banner>> GetbannerList(string hubId);
        Task<int> CreateBanner(Banner info);
        Task<int> UpdateBanner(Banner info);
        Task<Banner> GetBannerDetails(string id);
        Task<bool> DeleteBanner(int id);



    }
}
