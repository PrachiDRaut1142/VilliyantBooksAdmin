using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.PurchaseVM
{
    public class CreateVM
    {
        public List<SelectListItem> HubList { get; set; }
        public List<SelectListItem> SupplierList { get; set; }


    }
}
