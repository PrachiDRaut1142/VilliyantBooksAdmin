using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Freshlo.DomainEntities.Hub;

namespace Freshlo.RI
{
    public interface IHubRI
    {
        List<Hub> hublist();
        int CreateHub(Hub info);
        int UpdateHub(Hub info);
        Hub Hubdetails(string id);
        int DeleteHub(int id);
        int FacebookUpdate(bool isEnable, int branchId);
        int InstaUpdate(bool isEnable, int branchId);
        int TwitterUpdate(bool isEnable, int branchId);
        int SnapchatUpdate(bool isEnable, int branchId);
        int LinkedInUpdate(bool isEnable, int branchId);
        int GoogleMapUpdate(bool isEnable, int branchId);
        int PrinterestUpdate(bool isEnable, int branchId);
        int WhatsAppUpdate(bool isEnable, int branchId);
        int YoutubeUpdate(bool isEnable, int branchId);
        int GoogleReviewUpdate(bool isEnable, int branchId);

    }
}
