using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Employee
{
    public class Employee
    {
        public int id { get; set; }
        public string EmpId { get; set; }
        public string DecodeId { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string UserRole { get; set; }
        public string AdhaarNo { get; set; }
        public string PanNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string Password { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string Password3 { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime PasswordChangeDate { get; set; }
        public int Attempt { get; set; }
        public string Status { get; set; }
        public string ResetOTP { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } 
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string PartnerType { get; set; }
        public string Branch { get; set; }
        public string usercreatedname { get; set; }
        public int vendorId { get; set; }
        public string Vendorname { get; set; }
        public int  RoleId { get; set; }
        public string RoleName { get; set; }
        public int IsfirstLogin { get; set; }
        public bool IsfirstLoginTime { get; set; }
        public string LoginId { get; set; }
        public string OldStatus { get; set; }
    }
    
}
