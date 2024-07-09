using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.DTO
{
    public class PriceTagListItem
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string Measurement { get; set; }
    }
}
