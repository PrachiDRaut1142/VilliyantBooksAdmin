using Freshlo.DomainEntities.Hub;
using Freshlo.RI;
using Freshlo.SI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class HubService : IHubSI
    {
        public IHubRI _hubRI { get; set; }
        public HubService(IHubRI hubRI)
        {
            _hubRI = hubRI;
        }

        public Task<List<Hub>> hublist()
        {
            return Task.Run(() =>
            {
                return _hubRI.hublist();
            });
        }

        public Task<int> CreateHub(Hub info)
        {
            return Task.Run(() =>
            {
                return _hubRI.CreateHub(info);
            });
        }

        public Task<int> UpdateHub(Hub info)
        {
            return Task.Run(() =>
            {
                return _hubRI.UpdateHub(info);
            });
        }

        public Task<Hub> Hubdetails(string id)
        {
            return Task.Run(() =>
            {
                return _hubRI.Hubdetails(id);
            });
        }

        public Task<int> DeleteHub(int id)
        {
            return Task.Run(() =>
            {
                return _hubRI.DeleteHub(id);
            });
        }

        public Task<int> FacebookUpdate(bool isEnable, int branchId)
        {
            return Task.Run(() =>
            {
                return _hubRI.FacebookUpdate(isEnable, branchId);
            });
        }

        public Task<int> InstaUpdate(bool isEnable, int branchId)
        {
            return Task.Run(() =>
            {
                return _hubRI.InstaUpdate(isEnable, branchId);
            });
        }

        public Task<int> TwitterUpdate(bool isEnable, int branchId)
        {
            return Task.Run(() =>
            {
                return _hubRI.TwitterUpdate(isEnable, branchId);
            });
        }

        public Task<int> SnapchatUpdate(bool isEnable, int branchId)
        {
            return Task.Run(() =>
            {
                return _hubRI.SnapchatUpdate(isEnable, branchId);
            });
        }

        public Task<int> LinkedInUpdate(bool isEnable, int branchId)
        {
            return Task.Run(() =>
            {
                return _hubRI.LinkedInUpdate(isEnable, branchId);
            });
        }

        public Task<int> GoogleMapUpdate(bool isEnable, int branchId)
        {
            return Task.Run(() =>
            {
                return _hubRI.GoogleMapUpdate(isEnable, branchId);
            });
        }

        public Task<int> PrinterestUpdate(bool isEnable, int branchId)
        {
            return Task.Run(() =>
            {
                return _hubRI.PrinterestUpdate(isEnable, branchId);
            });
        }

        public Task<int> WhatsAppUpdate(bool isEnable, int branchId)
        {
            return Task.Run(() =>
            {
                return _hubRI.WhatsAppUpdate(isEnable, branchId);
            });
        }

        public Task<int> YoutubeUpdate(bool isEnable, int branchId)
        {
            return Task.Run(() =>
            {

                return _hubRI.YoutubeUpdate(isEnable, branchId);
            });
        }

        public Task<int> GoogleReviewUpdate(bool isEnable, int branchId)
        {
            return Task.Run(() =>
            {

                return _hubRI.GoogleReviewUpdate(isEnable, branchId);
            });
        }
    }
}
