using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.DomainEntities.PaymentSettlement;
using Freshlo.DomainEntities.Stock;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface ISalesSI
    {
        Task<List<Item>> GetallItemList(string mainCategory, string condition, string hubId, string ItemName);
        Task<List<ItemSizeInfo>> GetallItemVarianceList(string hubId);
        Task<List<Item>> GetallItemList_1(string ItemName, string CatogeryName);
        Task<List<SelectListItem>> GetMainCategoryList(string id);

        Task<Item> GetItemDetails(string itemId);
        Task<List<Item>> GetItemList(string searchTerm = null,string id=null);
        Task<List<PriceMap>> GetItemvaraintList(string ItemId,string id);
        string CreateorUpdateCustomerDetail(Customer customerdata);
        string CreateorUpdateCustomerAddress(Customer customersAddress);
        Task<List<SelectListItem>> GetCustomerContactDetail();
        Task<Customer> GetCustomerDataId(string Type, string custId);
        Task<Customer> GetCustomerDetails(string ContactNo);
        Customer ValidateContactNumber(string Ext, string ContactNo);
        Task<List<Customer>> GetCustomerMultipleAddId(int custId);
        int UpdateAddressNormal(string custermId, string addressId, string Type);
        int DeleteCustAddress(string custAdressId);
        string InsertSale(Sales dicData, string createdBy, string branch);
        bool InsertProductforSale(string insertValue);
        Task<List<SalesList>> GetSalesList(string id);
        Task<Sales> GetSalesOrderdetail(string id,string hubId);
        Task<List<Customer>> GetCustomerContactDetail(string searchTerm = null);
        Task<List<Item>> GetObjectFromFile(List<Item> item, AliyunCredential credential);

        Task<List<Sales>> GetAllSalesOrderList(string branch, string role, string date, string status, string source, string payment,string hubId);
        Task<List<SelectListItem>> GetSlotList();
        int UpdateStockData(Stock stockdata);
        Task<List<SelectListItem>> GetCoupenList(string CustomerId, string Status,string hubId);
        int InsertCoupenLog(List<SalesList> salesList, List<SalesList> kitchenList, int couponId, string saleId, string customerId, string LastUpdatedBy);
        int InsertCashDiscount(List<SalesList> salesList, List<SalesList> kitchenList, int couponId, string saleId, string customerId, float discount, float totalamount, string LastUpdatedBy);

        int InsertOldCoupenLog(List<SalesList> salesList, List<SalesList> kitchenList, int couponId, string saleId, string customerId, string LastUpdatedBy);

        void DeleteSalesOrder(string saleId);
        int AddStockData(Item detail);
        Task<List<Sales>> GetTodaySalesToPrint(Sales salesdata);
        Task<Sales> GetTodaySalesDetail(Sales salesdata);
        Task<byte[]> ExportExcelofSales(string webRootPath, Sales salesdata);
        Task<List<SelectListItem>> GetAllOrderStatus();
        Task<SalesDetail> GetSalesDetail(string Id,string hubId);
        Task<List<SalesList>> GetSalesProductListData(string Id,string hubId);
        Task<int> SalesList_InsertProductList(SalesList ProductList);
        Task<string> SalesList_CreateSalesOrderList(Item ProductList);
        Task<int> DeleteSalesList(string SalesListId, string SalesId);
        Task<int> RemoveSalesList(string SalesListId, string LastUpdatedBy, string SalesId);
        Task<List<SelectListItem>> Employee_NameSL(string UserRole);
        Task<int> AssignProductForDelivery(Sales SalesOrderData);
        Task<int> AssignShipped(Sales SalesOrderData);
        Task<int> UpdateDeliveryDate(Sales SalesOrderData);
        Task<int> UpdateDeliveryCharge(Sales SalesOrderData);
        Task<int> UpdateOrderdStatus(SalesList salesDetailList);
        Task<SalesDetail> GetSaleProductStatus(string SalesOrderId);
        Task<Item> GetItemDetailbyPlucode(string PluCode);
        Task<List<Sales>> GetPaymentSettlement(string Id, string PaymentSettled_By, string PaymentSettled_for, string Status);
        Task<string> updatepaymentsettlement(PaymentSettlement paymentData);
        Task<List<PaymentSettlement>> GetPaymentSettlementSummary();
        Task<int> UpdatePaymentStatus(Sales SalesOrderData);
        Task<int> CustomertblEdit(Sales CusttblEdit);
        Task<List<PendingData>> GetAllPendingOrders(PendingData info, string id);
        Task<List<SelectListItem>> GetAllZipcode();
        Task<byte[]> ExportExcelofPendingSales(string webRootPath, string status, string zipcode, string activeTab, string payment, string id);
        Task<string> UpdateSalesOtp(int id);
        Task<SalesCountData> GetSalesCount();
        //Task<byte[]> FilterExcelofSales(string branch, string role, string date, string status, string source, string payment, int a, int b, string startdate, string enddate, string webRootPath);
        Task<byte[]> FilterExcelofSales(string branch, string hubId, string role, string date, string status, string source, string payment, int a, int b, string startdate, string enddate, string webRootPath);

        int InsertStockSalesLog(Stock stockdata);
        Task<bool> UpdateOrderList(string ids, string updatedBy);
        Task<bool> DeductSalesStock(Sales dicData, string createdBy, string branch);

        Task<int> UpdateDeductedStock(SalesList ProductList, string createdBy, string branch);
        Task<SalesDetail> GetSalesGST(string Id);
        Task<TaxationInfo> GetTaxationDetails();

        Task<List<Sales>> GetFinancialSelOrderList(int a, int b, string c, string d,string hubId);
        Task<int> UpdateTaxesValue(string salesId,string hubId,int Isactive);
        Task<List<PrintSalesList>> GetSalesListForPrint(string SalesId,string hubId);
        Task<List<TaxPercentageMst>> GetGSTCount(string SalesId);
        Task<Sales> GetSummaryDetail(string id);
        Task<List<Item>> GetHubItemList(string hub, string searchTerm = null);
        Task<List<Sales>> GetFinancialSelOrderListHubWise(int a, int b, string c);

        Task<List<Sales>> GetFinancialSelOrderListZipWise(int a, int b, string c, string d);
        Task<List<TableInfo>> GetTableList(string id);
        Task<List<SelectListItem>> GettableList(string id);

        Task<List<SalesList>> GetKichenAllOrderList(int orderStatus, string id);
        Task<int> UpdateKitcheOrderStatus(Sales info);
        Task<List<SalesList>> GetKitchenReadyOrderList(string id);
        Task<List<SalesList>> GetTableStateList(string id);
        Task<List<SalesList>> GetTableStateListTKWY(string hubId);
        Task<List<SalesList>> GetTableStateListHOD(string hubId);


        Task<int> UpdateOrderdStatus(string id1, string LastUpdatedBy, string Orderstatus);
        Task<List<Item>> GetMainCatgList(string id);
        Task<List<Item>> GetCatgList(string id);

        Task<int> CloseOrder(SalesList Info);
        Task<int> UpdateAllKitcheOrderStatus(Sales info);
        Task<List<Item>> GetallItemListData(string mainCategory, string condition, string hubId, string ItemName);
        Task<TableInfo> GetTableDetails(int tableId, string hubId);

        Task<List<SalesList>> GetKotListView(string Id,string hubId);
        Task<int> UpdateAllKitchekotStatus(Sales info);

        Task<List<PrintSalesList>> GetSalesListKOTForPrint(string SalesId, string SalesKitchenId,string hubId);
        Task<List<PrintSalesList>> GetSalesListAllTblKOTForPrint(string SalesId, string SalesKitchenId,string hubId);
        Task<List<PrintSalesList>> GetSaleslistPrintDetails(string SalesId);

        Task<List<PrintSalesList>> GetSalesListConsolidateTblKOTForPrint(string SalesId, string SalesKitchenId);


        Task<int> RemoveSalesKitchenList(string SalesOrderId, string LastUpdatedBy, string SalesId, string KitchenListId);
        Task<List<SalesList>> GetPendingKotList();
        Task<List<SalesList>> GetItemListView(string Id,string hubId);

        Task<List<SalesList>> AllGetItemListView(string id);

        Task<int> ReadyOrder(SalesList Info);
        BusinessInfo GetBusinessInfoDetails(int id);
        Task<int> EditAnncouementMessage(BusinessInfo add);

        Task<List<Item>> GetCategorylist(string MainCatId,string id);

        int CreateCustomer(TableInfo info);
        int CreateBooking(TableInfo info);
        Task<List<TableInfo>> Getalltblbookinglist();

        Task<TableInfo> GetbookingCount();
        Task<List<SelectListItem>> Gettableperferenclist(string id);
        string InserttblBook(Sales dicData, string createdBy, string branch);
        Task<int> CancelBooking(Sales ids);
        Task<SalesCountData> GetOrderNotificationCount();
        Task<List<SalesDetail>> GetOrderdetails(string id);
        Task<int> UpdateNotificationCount(string SalesOrderId, string type);

        Task<List<KotLogs>> GetKotLogsView(string Id);
        Task<int> KotReprint(string KOTID, String SalesId);
        Task<List<PrintSalesList>> GetKOTLISTKOTForPrint(string kotId, string SalesId);
        Task<List<SalesList>> get_OrderTracking( string salesId);

    }
}
