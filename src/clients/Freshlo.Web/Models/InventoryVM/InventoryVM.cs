using Freshlo.DomainEntities.Inventory;
using Freshlo.SI;
using System.Collections.Generic;
namespace Freshlo.Web.Models.InventoryVM
{
    public class InventoryVM : BaseViewModel
    {
        public List<InventoryAsset> Adhoc_Inventory { get; set; }

        public List<InventoryAsset> Inventory_Logs { get; set; }
    }
}
