using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class BrandInfo 
    {
        public int Id { get; set; }
        public string BrandId { get; set; }
        public string BrandName { get; set; }
        public string Logo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastupdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Supplier { get; set; }
        public List<string> Supplierlist { get; set; }
        public IFormFile ImageLogo { get; set; }
        public int ItemCount { get; set; }
        public int BrandCount { get; set; }
        public string SupType { get; set; }

        public string Vendornames { get; set; }
        public string Aliyunkey { get; set; }
    }
}
