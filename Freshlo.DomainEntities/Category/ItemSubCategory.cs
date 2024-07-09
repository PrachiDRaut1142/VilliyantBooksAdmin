using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Category
{
  public class ItemSubCategory
    {
        public int Id { get; set; }
        public string SubCategoryId { get; set; }
        public string hubId { get; set; }
        public string CategoryId { get; set; }
        public string LanguageName { get; set; }
        public string SubCategoryName { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public string SubCategoryCode { get; set; }
        public string CategoryLanguage { get; set; }
        public string CategoryDescription { get; set; }
        public string CreatedBy { get; set; }
        public IFormFile imageFiles { get; set; }

        public int CatId { get; set; }

    }
}
