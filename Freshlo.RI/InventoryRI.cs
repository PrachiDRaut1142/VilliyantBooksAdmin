using Freshlo.DomainEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.RI
{
    public interface InventoryRI 
    {
        List<InventoryAsset>Adhoc_Inventory(string id);
     
        int Adhoc_Updates(InventoryAsset Id);

        List<InventoryAsset> Inventory_Logs(string id);
        List<InventoryAsset> AuditLogs(string id);
        InventoryAsset GetAuditlist(string id,string hubId);
        List<InventoryAsset> New_AuditList(string id);
        int CreateAudit(InventoryAsset info);
    }
}

