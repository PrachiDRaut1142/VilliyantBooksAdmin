using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class Emailconfig
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string EmailType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IncomingMainServer { get; set; }
        public string IncomingPort { get; set; }
        public string OutgoingMainServer { get; set; }
        public string OutgoingPort { get; set; }
        public bool IsSsl { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
