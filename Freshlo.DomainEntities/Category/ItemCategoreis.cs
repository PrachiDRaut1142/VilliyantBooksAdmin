using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Category
{
   public class ItemCategoreis
    {
        public int Id { get; set; }
        public string CategoryId { get; set; }
        public string DecodeId { get; set; }
        public string CategoriesName { get; set; }
        public string MainCategoryCode { get; set; }
        public string SubName { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public string CategoryCode { get; set; }
        public string SubCategoryId { get; set; }
        public string CategDescription { get; set; }
        public string Image { get; set; }
        public IFormFile ImagePath { get; set; }
        public string MainCategoryId { get; set; }
        public string MainName { get; set; }
        public bool CategVisibility { get; set; }
        public bool MainCat_Visibility { get; set; }
        public int CategorySequence { get; set; }
        public int SubCategoryCount { get; set; }
        public int MainCateId { get; set; }
        public string Aliyunkey { get; set; }
        public string Hub { get; set; }
    }
}
