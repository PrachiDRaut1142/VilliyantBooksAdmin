using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class CustomersAddress
    {
        public int id { get; set; }
        public string AddId { get; set; }
        public string CustomerId { get; set; }
        public string BuildingName { get; set; }
        public string RoomNo { get; set; }
        public string Sector { get; set; }
        public string Locality { get; set; }
        public string Landmark { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public string Type { get; set; }
        public string AddressType { get; set; }
        public string Hub { get; set; }
        public string CustomerCount { get; set; }
        public float StandradDeleiveryCharges { get; set; }
        public float ExpressDeleiveryCharges { get; set; }
        public float InternationalCharge { get; set; }
        public float MinOrderValue { get; set; }
        public string HubName { get; set; }
        public string Discription { get; set; }
        public string CODAVAL { get; set; }
        public string ExpressDaval { get; set; }
        public string ErrorMessage { get; set; }
        public string ChannelId { get; set; }
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }

    }
}
