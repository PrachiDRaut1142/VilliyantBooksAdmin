using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class ItemCategory
    {
        public int Id { get; set; }
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public string LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public string CategoryCode { get; set; }
        public string SubCategoryId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string MainCategoryId { get; set; }
        public string MainName { get; set; }
    }
}
