using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.DomainEntities.Purchase;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.RI
{
    public interface IPurchaseRI
    {
        List<SelectListItem> GetHubList();
        string InsertPurchase(PurchaseDetail data);
        bool UpdatePriceList(PurchaseDetail data);
        List<PurchaseDetail> GetAPurchaseList();
        Purchase GetPurchaseDetail(string Id);
        List<PurchaseList> GetPurchaseItemList(string Id);
        void UpdatePurchaseDetail(Purchase data);
        string UpdatePurchase(PurchaseDetail data);
        List<Item> GetItemList(string purchaseId, string searchTerm = null);
        List<SelectListItem> GetSupplierNameList();
        Task<SummayData> GetSummaryData();
        List<Pur_ItemSummary> GetItemSummary(SummaryFilter Options);
        List<Pur_ItemSummary> GetCategorySummary(SummaryFilter Options);


        // New Methode Bind ///


        // List PuchaseList and Append PurchaseList //
        List<Purchase> GetPendingPurchase();
        List<Purchase> GetAddPendingPurchaseOrder(string datefrom, string dateto, string createFor);
        List<PurchaseList> DownloadPurchasePdfData(string id);



        // New Insert Purchase Create Methode //
        string InsertNewPurchase(PurchaseDetail data);



        // New Insert Purchase Details List Filter Methode //
        List<PurchaseList> GetallnewPurchaseListItem(string Id, PricelistFilter detail);



        // New Update Purchase Details & List  Methode //
        void NewUpdatePurchaseDetail(Purchase data);
        string NewUpdatePurchaseOrder(PurchaseDetail data);
        bool UpdateNewPriceList(PurchaseDetail data);


        
        // New Insert Purchase Details List Delete Methode  //
        bool DeletePurchaseList(string id);


    }
}