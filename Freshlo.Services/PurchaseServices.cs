using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.DomainEntities.Purchase;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
   public class PurchaseServices:IPurchaseSI
    {
        private readonly IPurchaseRI _purchaseRI;
        public PurchaseServices(IPurchaseRI purchaseRI)
        {
            _purchaseRI = purchaseRI;
        }
        public Task<List<SelectListItem>> GetHubList()
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetHubList();
            });
        }
        public Task<string> InsertPurchase(PurchaseDetail data)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.InsertPurchase(data);
            });
        }
        public Task<bool> UpdatePriceList(PurchaseDetail data)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.UpdatePriceList(data);
            });
        }
        public Task<List<PurchaseDetail>> GetAPurchaseList()
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetAPurchaseList();
            });
        }
        public Task<Purchase> GetPurchaseDetail(string Id)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetPurchaseDetail(Id);
            });
        }
        public Task<List<PurchaseList>> GetPurchaseItemList(string Id)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetPurchaseItemList(Id);
            });
        }
        public void UpdatePurchaseDetail(Purchase data)
        {
            _purchaseRI.UpdatePurchaseDetail(data);
        }
        public Task<string> UpdatePurchase(PurchaseDetail data)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.UpdatePurchase(data);
            });
        }
        public Task<List<Item>> GetItemList(string purchaseId,string searchTerm = null)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetItemList(searchTerm, purchaseId);
            });
        }
        public Task<List<SelectListItem>> GetSupplierNameList()
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetSupplierNameList();
            });
        }
        public Task<SummayData> GetSummaryData()
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetSummaryData();
            });
        }
        public Task<List<Pur_ItemSummary>> GetItemSummary(SummaryFilter Options)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetItemSummary( Options);
            });
        }
        public Task<List<Pur_ItemSummary>> GetCategorySummary(SummaryFilter Options)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetCategorySummary(Options);
            });
        }




        //   New Method Bind //



        // List PuchaseList and Append PurchaseList //
        public Task<List<Purchase>> GetPendingPurchaseList()
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetPendingPurchase();
            });
        }
        public Task<List<Purchase>> GetAddPnedingPurchaseList(string datefrom, string dateto, string createFor)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetAddPendingPurchaseOrder(datefrom, dateto, createFor);

            });
        }
        public Task<List<PurchaseList>> DownloadPdfData(string id)
        {
            return Task.Run(() =>
            {

                return _purchaseRI.DownloadPurchasePdfData(id);
            });
        }



        // New Insert Purchase Create Methode //
        public Task<string> InsertNewPurchase(PurchaseDetail data)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.InsertNewPurchase(data);
            });
        }



        // New Insert Purchase Details List Filter Methode //
        public Task<List<PurchaseList>> GetallnewPurchaseListItem(string Id, PricelistFilter detail)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.GetallnewPurchaseListItem(Id, detail);
            });
        }




        // New Update Purchase Details & List  Methode //
        public void NewUpdatePurchaseOrderDetail(Purchase data)
        {
            _purchaseRI.NewUpdatePurchaseDetail(data);
        }
        public Task<string> NewUpdatePurchaseOrder(PurchaseDetail data)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.NewUpdatePurchaseOrder(data);
            });
        }
        public Task<bool> UpdateNewPriceList(PurchaseDetail data)
        {
            return Task.Run(() =>
            {
                return _purchaseRI.UpdateNewPriceList(data);
            });
        }


  
        
        // New Insert Purchase Details List Delete Methode  //
        public Task<bool> DeletePurchaseList(string id)
        {
            return Task.Run(() => {

                return _purchaseRI.DeletePurchaseList(id);

            });
        }

    }
}
