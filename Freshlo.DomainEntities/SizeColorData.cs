using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class SizeColorData
    {
        public List<ItemSizeInfo> ProductSizeInfo { get; set; }
        public List<ItemColorInfo> ProductColorInfo { get; set; }
        public int Id { get; set; }
        public string ItemId { get; set; }
        public List<ColorSizeMapping> MappingInfo { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
