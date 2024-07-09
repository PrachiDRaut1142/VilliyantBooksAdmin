using Freshlo.DomainEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models
{
    public class EmployeeAccessVM
    {
        public List<SelectListItem> getEmployeeNameSL { get; set; }
        public List<SelectListItem> getEmployeeNameSLbyID { get; set; }
        public List<WebAccessPermission> GetWebAccessList { get; set; }
        public List<GlobalIPInfo> GetGlobalAccessList { get; set; }
        public List<WebAccessInfoLog> GetWebAccessLogList { get; set; }
    }
}
