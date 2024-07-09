using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Category
{
   public class MainCategory
    {
        public string hubId { get; set; }
        public string Hub { get; set; }

        public int Id { get; set; }
        public string MainCategoryId { get; set; }
        public string DecodeId { get; set; }
        public string MainCategoryLanguage { get; set; }
        public string MainCategoryDescription { get; set; }
        public string Name { get; set; }
        public string MainCategoryCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public int Visibility { get; set; }
        public int CategoryCount { get; set; }
        public string CategoryLanguage { get; set; }
        public string LanguageName { get; set; }
        public string CategoryDescription { get; set; }
        public IFormFile imageFiles { get; set; }
        public int imgIsUploaded { get; set; }
        public string Aliyunkey { get; set; }
        
        public int hubMapCount { get; set; }

    }
}
