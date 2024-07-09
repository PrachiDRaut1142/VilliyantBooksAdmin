using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface IHubSI
    {
        Task<List<Hub>> hublist();
        Task<int> CreateHub(Hub info);
        Task<int> UpdateHub(Hub info);
        Task<Hub> Hubdetails(string id);
        Task<int> DeleteHub(int id);
        Task<int> FacebookUpdate(bool isEnable, int branchId);
        Task<int> InstaUpdate(bool isEnable, int branchId);
        Task<int> TwitterUpdate(bool isEnable, int branchId);
        Task<int> SnapchatUpdate(bool isEnable, int branchId);
        Task<int> LinkedInUpdate(bool isEnable, int branchId);
        Task<int> GoogleMapUpdate(bool isEnable, int branchId);
        Task<int> PrinterestUpdate(bool isEnable, int branchId);
        Task<int> WhatsAppUpdate(bool isEnable, int branchId);
        Task<int> YoutubeUpdate(bool isEnable, int branchId);
        Task<int> GoogleReviewUpdate(bool isEnable, int branchId);

    }
}
