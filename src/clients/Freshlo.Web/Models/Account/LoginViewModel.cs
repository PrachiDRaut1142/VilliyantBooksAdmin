using Freshlo.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.Account
{
    public class LoginViewModel :BaseViewModel
    {
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public string EmailId { get; set; }
        public string UserId { get; set; }

        public string UserRole { get; set; }
        public BusinessInfo businessInfo { get; set; }

    }
}
