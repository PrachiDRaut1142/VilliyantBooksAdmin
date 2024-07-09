using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class ProductSpec
    {
        public int Id { get; set; }
        public string ProductSubCatId { get; set; }
        public string DescType { get; set; }
        public string DescValue { get; set; }
        public string productCatId { get; set; }
        public string branchId { get; set; }
        public string itemId { get; set; }
        public List<ProductSpec> productSpec { get; set; }
    }
    public class ProductSpecSubCatgeory
    {
        public int Id { get; set; }
        public string ProductSubCatId { get; set; }
        public string ProductSubCatName { get; set; }
      
        public string productCatId { get; set; }
        public string BranchId { get; set; }

    }

}
