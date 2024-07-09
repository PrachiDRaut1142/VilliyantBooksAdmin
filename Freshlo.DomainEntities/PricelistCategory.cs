using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class PricelistCategory
    {
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public string LastUpdatedBy { get; set; }
        public string LastUpdatedDate { get; set; }
        public bool MainCat_Visibility { get; set; }
        public string MainCategoryId { get; set; }
        public string MainName { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public string SubCategoryId { get; set; }
        public string SubName { get; set; }
        public bool Visibility { get; set; }
        public int MainCatId { get; set; }
        public string ManCatName { get; set; }
        public int CatId { get; set; }
        public string CatName { get; set; }

        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public int VendorId { get; set; }
        public string VendorName { get; set; }

        public int SubCatId { get; set; }
        public string SubCatName { get; set; }

    }
}
