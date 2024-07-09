using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.CustomerVM
{
    public class CustomerManageVM : BaseViewModel
    {
        public List<CustomerSalesHistory> getCustomerHistoryList { get; set; }

       public List<CustomersAddress> AreawiseCustomer { get; set; }

       public CustomerSummaryCount GetallCustomerOrderSummaryCount { get; set; }

       public CustomerSummaryCount GetallnewCustomerSummaryCount { get; set; }

       public List<Customer> getCustomerList { get; set; }

        public int a { get; set; }

        public int b { get; set; }

        public List<Customer> GetCustomerRegOrderlist { get; set; }

        public List<Customer> GetCustomerUnregOrderlist { get; set; }

        public List<Hub> getHublist { get; set; }
        public List<CustomersAddress> getZiplist { get; set; }
        public List<Customer> CreateCustomer { get; set; }
        public List<CustomersAddress> addAddress { get; set; }


    }
}
