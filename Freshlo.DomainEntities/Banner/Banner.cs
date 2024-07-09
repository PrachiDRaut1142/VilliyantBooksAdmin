using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Banner
{
   public class Banner
    {
        public int Id { get; set; }
        public string BannerId { get; set; }
        public string DecodeId { get; set; }
        public string Name { get; set; }
        public string MainCategory { get; set; }
        public string Place { get; set; }
        public string ActionTrigger { get; set; }
        public string RefferTag { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string VideoLink { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string TriggerId { get; set; }
        public IFormFile imageFiles { get; set; }
        public int T_Id { get; set; }
        public string TextLink { get; set; }
        public string Size { get; set; }
        public string MancategoryId { get; set; }
        public string Mancategoryname { get; set; }
        public string ScrollBaner { get; set; }
        public string Aliyunkey { get; set; }
        public bool BranchMapped { get; set; }
        public string BranchMapped1 { get; set; }
        public string Branch { get; set; }
    }
}
