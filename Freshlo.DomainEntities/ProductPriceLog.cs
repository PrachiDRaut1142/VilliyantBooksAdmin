using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class ProductPriceLog
    {
       public int Id { get; set; }
       public string ItemId { get; set; }
       public string EmpId { get; set; }
       public string PluName { get; set; }
       public float PurchasePrice { get; set; }
       public float MarketPrice { get; set; }
       public float SellingPrice { get; set; }
       public DateTime CreatedOn { get; set; }
       public string CreatedBy { get; set; }
       public string HubId { get; set; }
       public float OldPurchasePrice { get; set; }
       public float OldMarketPrice { get; set; }
       public float OldSellingPrice { get; set; }
       public string OldSize { get; set; }
       public string Size { get; set; }
       public string PrLogId { get; set; }
       public string Measurement { get; set; }
    }
}
