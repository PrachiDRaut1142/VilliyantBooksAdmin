using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class WebAccessPermission
    {
        public int Id { get; set; }
        public string WhoisIp { get; set; }
        public string MacAddress { get; set; }
        public string[] EmployeeId { get; set; }
        public int Status { get; set; }
        public string Status_Desc { get; set; }
        public string CreatedName { get; set; }
        public string UserName { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_On { get; set; }
        public string Updated_By { get; set; }
        public DateTime Updated_On { get; set; }
        public string[] Checkboxids { get; set; }
        public string[] Checkboxid { get; set; }
        public int EmployeeCount { get; set; }
        public string MultipleEmployeeId { get; set; }
        public string[] WhitelistEmployee { get; set; }
    }
}
