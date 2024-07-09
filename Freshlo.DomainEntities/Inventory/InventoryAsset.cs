using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Inventory
{
    public class InventoryAsset
    {
        public int Id { get; set; }
        public string AssetsId { get; set; }
        public string AssetName { get; set; }
        public string AssetsUnitPrice { get; set; }
        public string AssetUnitPrice2 { get; set; }

        public string AssetUnitAd { get; set; }
        public string AssetDescriptions { get; set; }
        public string AssetsLifespan { get; set; }
        public int IsServicable { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Quantity { get; set; }
        public string Hub { get; set; }
        public int Quantity2 { get; set; }
        public int AuditQuantity { get; set; }
        public int  EntryType { get; set; }
        public string Remarks { get; set; }
        public string ItemId { get; set; }
        public string AuditId { get; set; }
        public int differance { get; set; }



    }
}
