using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.DashboardVM
{
    public class GSTDashbaordVM : BaseViewModel
    {
        public List<Hub> getHublist { get; set; }
        public List<TaxPercentageMst> getGstList { get; set; }
        public TaxPercentageMst Gstdashcount { get; set; }

    }
}
