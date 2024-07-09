using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models
{
    public class BaseViewModel
    {
        public string ErrorMessage { get; set; }

        public string ViewMessage { get; set; }
        public string acessError { get; set; }
        public string ip { get; set; }
        public BusinessInfo businessInfo { get; set; }
        public List<Hub> GetHubList { get; internal set; }
    }
}
