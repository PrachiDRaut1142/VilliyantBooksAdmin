using Freshlo.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.CustomerVM
{
    public class DetailViewModel
    {
        internal BusinessInfo businessInfo;

        public CustomerSalesHistory Summary { get; set; }
        public List<Sales> SalesList { get; set; }
        public List<Customer> CustomerDetailist { get; set; }
        public Customer CustomerDetail { get; set; }

    }
}
