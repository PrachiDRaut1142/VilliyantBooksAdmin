using Freshlo.DomainEntities.Banner;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class BannerService : BannerSI
    {

        private BannerRI  _bannerRI;

        public BannerService(BannerRI bannerRI)
        {
            _bannerRI = bannerRI;
        }

        public Task<List<Banner>> GetMancategoreylist()
        {
            return Task.Run(() =>
            {

                return _bannerRI.GetMancategorylist();
            });
        }

        public Task<List<SelectListItem>> GetActionTrigger(string trigger)
        {
            return Task.Run(() =>
            {
                return _bannerRI.GetAcctionTriggerlist(trigger);
            });
        }


        public Task<List<Banner>> GetbannerList(string hubId)
        {
            return Task.Run(() =>
            {
                return _bannerRI.GetBannerList(hubId);
            });
        }


        public Task<int> CreateBanner(Banner info)
        {
            return Task.Run(() =>
            {
                return _bannerRI.CreateBanner(info);
            });
        }



        public Task<int> UpdateBanner(Banner info)
        {
            return Task.Run(() =>
            {
                return _bannerRI.UpdaetBanner(info);
            });
        }

        public Task<Banner> GetBannerDetails(string id)
        {
            return Task.Run(() =>
            {

                return _bannerRI.GetbannerDetails(id);
            });
        }


        public Task<bool> DeleteBanner(int id)
        {
            return Task.Run(() =>
            {
                return _bannerRI.DeleteBanner(id);
            });
        }

   
    }
}
