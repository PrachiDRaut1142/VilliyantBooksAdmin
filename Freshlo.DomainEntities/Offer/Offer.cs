using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Offer
{
    public class Offer
    {
        public int Id { get; set; }
        public int Id1 { get; set; }
        public string OfferId { get; set; }
        public string[] ItemId { get; set; }
        public string[] BuyItemId { get; set; }
        public string GetItemId { get; set; }
        public string[] GetItemIdss { get; set; }
        public string chooseOffer { get; set; }
        public string OfferTypes { get; set; }
        public string FreeType { get; set; }
        public string DecodeId { get; set; }
        public string OfferHeading { get; set; }
        public string OfferDescription { get; set; }
        public DateTime offerDate { get; set; }
        public DateTime OfferStartDate { get; set; }
        public DateTime OfferEndDate { get; set; }
        public string OffStartdate { get; set; }
        public string Aliyunkey { get; set; }
        public string OffEndtdate { get; set; }
        public string Status { get; set; }
        public string CategoryId { get; set; }
        public string CreatedBy { get; set; }
        public IFormFile imageFiles { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public float DiscountPerctg { get; set; }
        public string[] OfferIds { get; set; }
        public decimal MinOrderValue { get; set; }
        public string MinQuantityValue { get; set; }
        public string IsLimitedOffer { get; set; }


        /* List Of Offertbl */

        public List<int> Ids { get; set; }

        public List<string> ItemIds { get; set; }

        //public List<string> OfferIds { get; set; }

        public List<string> CreatedBys { get; set; }

        public List<DateTime> DateAdded { get; set; }
        public Offerlist offervariance { get; set; }
        public DateTime StartDate { get; set; }
        public string Hub { get; set; }
        public string items { get; set; }
        public string GetQuantity { get; set; }
        public string BuyQuantity { get; set; }
        public int ItemType { get; set; }
    }
     public class Offerlist
    {
        public string[] ItemId { get; set; }
        public string[] BuyItemId { get; set; }
        public string[] GetItemId { get; set; }
    }
}
