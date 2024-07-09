using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.DomainEntities.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.DashboardVM
{
    public class FinancialDashboardVM : BaseViewModel
    {
        public List<DashboardFinacialStatistics> GetListViewCountDasboardData { get; set; }
        public DashboardCount GetallCountDashbaord { get; set; }
        public List<Hub> getHublist { get; set; }
        public List<DashboardCount> dashboard { get; set; }
        public List<CustomersAddress> getZiplist { get; set; }
        public List<CurrencyMST> getCurrencysybmollist { get; set; }

    }
}
