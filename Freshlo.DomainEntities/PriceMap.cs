using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class PriceMap
    {
        public int Id { get; set; }
        public int PId { get; set; }
        public string ItemId { get; set; }
        public string PriceId { get; set; }
        public string Size { get; set; }
        public double SellingPrice { get; set; }
        public double MarketPrice { get; set; }
        public double PurchasePrice { get; set; }
        public double ProfitMargin { get; set; }
    }
}
