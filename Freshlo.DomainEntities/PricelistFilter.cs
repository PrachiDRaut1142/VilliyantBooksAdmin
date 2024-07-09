using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class PricelistFilter
    {
        public int MainCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int ItemType { get; set; }
        public int BrandId { get; set; }
        public int VendorId { get; set; }
        public string ApproavalType { get; set; }
        public string HubId { get; set; }

    }
}
