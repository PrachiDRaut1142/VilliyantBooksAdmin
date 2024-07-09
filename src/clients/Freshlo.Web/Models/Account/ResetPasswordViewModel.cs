using Freshlo.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.Account
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Otp { get; set; }
        public string UserId { get; set; }


        public SecurityConfig SecurityConfig { get; set; }

        public string NewPassword { get; set; }
        public BusinessInfo businessInfo { get; set; }
    }
}
