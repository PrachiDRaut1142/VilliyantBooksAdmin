using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Coupen
{
  public class Coupen
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DecodeId { get; set; }
        public string ShortDescription { get; set; }
        public string CoupenCode { get; set; }
        public int MinOrderValue { get; set; }
        public int MaxDiscount { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int DiscountPercnt { get; set; }
        public int UsageAllowedperUser { get; set; }
        public string Hub { get; set; }
    }
}
