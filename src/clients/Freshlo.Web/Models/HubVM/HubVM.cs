using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.HubVM
{
    public class HubVM : BaseViewModel
    {
        public List<Hub> getHublist { get; set; }
        public Hub getHubdetails { get; set; }
        public List<CurrencyMST> getCurrencyList { get; set; }
    }
}
