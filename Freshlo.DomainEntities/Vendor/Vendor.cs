using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Vendor
{
    public class Vendor
    {
        public int Id { get; set; }
        public string VendorId { get; set; }
        public string Business { get; set; }
        public string Person { get; set; }
        public string Role { get; set; }
        public string ProductType { get; set; }
        public string EmailId { get; set; }
        public string Ext { get; set; }
        public string ContactNo { get; set; }
        public string Area { get; set; }
        public string BuildingName { get; set; }
        public string RoomNo { get; set; }
        public string Sector { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string Password3 { get; set; }
        public int Attempts { get; set; }
        public string Status { get; set; }
        public string Otp { get; set; }
        public string LastLogin { get; set; }
        public DateTime PasswordChangeDate { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public string Hub { get; set; }
        public float Commission { get; set; }
        public string GSTNumber { get; set; }
        public string IFSCNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public List<int> productTypesValues { get; set; }
        public int ProductTypes { get; set; }

        //public int MainCatId { get; set; }
        //public string MainCatName { get; set; }

    }
}

