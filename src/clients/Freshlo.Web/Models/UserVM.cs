using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Employee;
using Freshlo.DomainEntities.Hub;
using Freshlo.DomainEntities.Vendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models
{
    public class UserVM : BaseViewModel
    {
        public List<Employee> Employeelist { get; set; }
        public List<Employee> Employeelistuser { get; set; }
        public Employee Employeelistbyid { get; set; }
        public List<Employee> getVendorList { get; set; }
        public List<Employee> getUserRoleList { get; set; }
        public List<Hub> getHublist { get; set; }
        public SecurityConfig Getsecurity { get; set; }

    }
}
