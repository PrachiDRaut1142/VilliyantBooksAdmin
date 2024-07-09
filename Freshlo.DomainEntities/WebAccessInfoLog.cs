using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class WebAccessInfoLog
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string EmployeeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Status { get; set; }
        public string EmployeeName { get; set; }
      
    }
}
