using Freshlo.DomainEntities.Inventory;
using Freshlo.RI;
using Freshlo.SI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class InventoryService : Inventory
    {
        private readonly InventoryRI _InventoryRI;
        public InventoryService(InventoryRI InventoryRI)
        {
            _InventoryRI = InventoryRI;
        }

        public Task<List<InventoryAsset>> Adhoc_Inventory( string id)
        {
            return Task.Run(() =>
            {
                return _InventoryRI.Adhoc_Inventory(id);
            });
        }

        public Task<int>Adhoc_Updates(InventoryAsset Id)
        {
            return Task.Run(() =>
            {
                return _InventoryRI.Adhoc_Updates(Id);
            });
        }

        public Task<List<InventoryAsset>> AuditLogs(string id)
        {
            return Task.Run(() =>
            {
                return _InventoryRI.AuditLogs(id);
            });
        }

        public int CreateAudit(InventoryAsset info)
        {
          
                return _InventoryRI.CreateAudit(info);
            
        }

        public InventoryAsset GetAuditlist(string id,string hubId)
        {
            return _InventoryRI.GetAuditlist(id,hubId);
        }

        public Task<List<InventoryAsset>> Inventory_Logs(string id)
        {
            return Task.Run(() =>
            {
                return _InventoryRI.Inventory_Logs(id);
            });
        }

        public Task<List<InventoryAsset>> New_AuditList(string id)
        {
            return Task.Run(() =>
            {
                return _InventoryRI.New_AuditList(id);
            });
        }
    }
}
