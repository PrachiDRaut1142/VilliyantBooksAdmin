using Freshlo.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.Account
{
    public class ChangePasswordViewMoel :BaseViewModel
    {
        public string EmpId { get; set; }
        public string EmailAddress { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public SecurityConfig SecurityConfig { get; set; }
        public BusinessInfo businessInfo { get; set; }
    }
}
