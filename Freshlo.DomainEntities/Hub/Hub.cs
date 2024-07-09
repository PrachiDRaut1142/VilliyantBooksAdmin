using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Hub
{
   public class Hub
    {
        public int Id { get; set; }
        public string HubId { get; set; }
        public string DecodeId { get; set; }
        public string HubName { get; set; }
        public string Area { get; set; }
        public string BuildingName { get; set; }
        public string RoomNo { get; set; }
        public string Sector { get; set; }
        public string currency { get; set; }
        public string Landmark { get; set; }
        public List<string> ZipList { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int Count { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int HubCount { get; set; }
        public string HubDetails { get; set; }
        public int MyProperty { get; set; }
        public string ContactNo{ get; set; }
        public string BrnachEmail { get; set; }
        public string BranchNotifyEmail { get; set; }
        public string MapCode { get; set; }
       public bool IsFacebookEnable { get; set; }
       public string FacebookLink { get; set; }
       public bool IsInstaEnable   { get; set; }
       public string InstaLink { get; set; }
       public bool IsTwitterEnable  { get; set; }
       public string TwitterLink { get; set; }
       public bool IsSnapchatEnable   { get; set; }
       public string SnapChatLink { get; set; }
       public bool IsLinkedInEnable  { get; set; }
       public string LinkedInLink { get; set; }
       public bool IsGoogleMapEnable   { get; set; }
       public string GoogleMapLink { get; set; }
       public bool IsPrinterestEnable  { get; set; }
       public string PrinterestLink { get; set; }
       public bool IsWhatsAppEnable    { get; set; }
       public string WhatsAppLink { get; set; }
       public bool IsYoutubeEnable { get; set; }
       public string YoutubeLink { get; set; }
       public bool IsGoogleReviewEnable    { get; set; }
       public string IsGoogleReviewLink { get; set; }
    }
}
