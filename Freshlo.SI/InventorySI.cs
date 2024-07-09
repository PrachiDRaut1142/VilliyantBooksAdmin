using Freshlo.DomainEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface Inventory
    {
        Task<List<InventoryAsset>> Adhoc_Inventory(string id);

        Task<int> Adhoc_Updates(InventoryAsset Id);

        Task<List<InventoryAsset>> Inventory_Logs(string id);

          Task<List<InventoryAsset>> AuditLogs(string id);
        InventoryAsset GetAuditlist(string id,string hubId);
        Task<List<InventoryAsset>> New_AuditList(string id);

         int CreateAudit(InventoryAsset info);

    }
}
