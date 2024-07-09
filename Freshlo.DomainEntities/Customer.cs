using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class Customer
    {
        public int custId { get; set; }
        public int Id { get; set; }
        public string Id1 { get; set; }
        public string CustomerId { get; set; }
        public string DecodeId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string Password3 { get; set; }
        public int Attempts { get; set; }
        public string Status { get; set; }
        public string Otp { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime PasswordChangeDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public string EnrollNo { get; set; }
        public string ContactNo { get; set; }
        public string Ext { get; set; }
        public string UserType { get; set; }
        public string Source { get; set; }
        public string AddId { get; set; }
        public string BuildingName { get; set; }
        public string RoomNo { get; set; }
        public string Sector { get; set; }
        public string Locality { get; set; }
        public string Landmark { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public string AddressType { get; set; }
        public string Hub { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Addids { get; set; }
        public string computeAddIds { get; set; }
        public double TotalPrice { get; set; }
        public int Count { get; set; }
        public double wallet { get; set; }

        public int TotalOrder { get; set; }
    }
}
