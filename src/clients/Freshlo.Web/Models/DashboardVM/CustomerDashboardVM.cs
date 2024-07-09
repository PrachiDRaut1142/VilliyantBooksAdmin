using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.DashboardVM
{
    public class CustomerDashboardVM : BaseViewModel
    {
        public List<CustomersAddress> AreawiseCustomer { get; set; }

        public CustomerSummaryCount GetallCustomerOrderSummaryCount { get; set; }

        public CustomerSummaryCount GetallnewCustomerSummaryCount { get; set; }

        public List<Hub> getHublist { get; set; }

        public List<CustomersAddress> getZiplist { get; set; }
        public int a { get; set; }
        public int b { get; set; }

    }
}
