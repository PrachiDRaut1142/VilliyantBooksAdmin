using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.DomainEntities.Purchase;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface IPurchaseSI
    {
        Task<List<SelectListItem>> GetHubList();
        Task<string> InsertPurchase(PurchaseDetail data);
        Task<bool> UpdatePriceList(PurchaseDetail data);
        Task<List<PurchaseDetail>> GetAPurchaseList();
        Task<Purchase> GetPurchaseDetail(string Id);
        Task<List<PurchaseList>> GetPurchaseItemList(string Id);
        void UpdatePurchaseDetail(Purchase data);
        Task<string> UpdatePurchase(PurchaseDetail data);
        Task<List<Item>> GetItemList(string purchaseId,string searchTerm = null);
        Task<List<SelectListItem>> GetSupplierNameList();
        Task<SummayData> GetSummaryData();
        Task<List<Pur_ItemSummary>> GetItemSummary(SummaryFilter Options);
        Task<List<Pur_ItemSummary>> GetCategorySummary(SummaryFilter Options);


        //   New Method Bind //


        // List PuchaseList and Append PurchaseList //
        Task<List<Purchase>> GetPendingPurchaseList();
        Task<List<Purchase>> GetAddPnedingPurchaseList(string datefrom, string dateto, string createFor);
        Task<List<PurchaseList>> DownloadPdfData(string id);



        // New Insert Purchase Create Methode //
        Task<string> InsertNewPurchase(PurchaseDetail data);



        // New Insert Purchase Details List Filter Methode //
        Task<List<PurchaseList>> GetallnewPurchaseListItem(string Id, PricelistFilter detail);



        // New Update Purchase Details & List  Methode //

        void NewUpdatePurchaseOrderDetail(Purchase data);
        Task<string> NewUpdatePurchaseOrder(PurchaseDetail data);
        Task<bool> UpdateNewPriceList(PurchaseDetail data);



        // New Insert Purchase Details List Delete Methode  //
        Task<bool> DeletePurchaseList(string id);

    }
}
