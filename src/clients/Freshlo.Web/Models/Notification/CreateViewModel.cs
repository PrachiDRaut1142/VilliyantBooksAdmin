using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.Notification
{
    public class CreateViewModel : BaseViewModel
    {
        public List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> CustomerContactList { get; set; }
        public string ErrorMsg { get; set; }
        public string ImagePath { get; set; }

    }
}
