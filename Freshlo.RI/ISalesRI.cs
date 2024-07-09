using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.DomainEntities.PaymentSettlement;
using Freshlo.DomainEntities.Stock;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshlo.RI
{
    public interface ISalesRI
    {
        Task<List<Item>> GetallItemList(string mainCategory, string condition, string hubId, string ItemName);
        Task<List<Item>> GetallItemList_1(string ItemName, string CatogeryName);
        List<SelectListItem> GetMainCategoryList(string id);

        Task<Item> GetItemDetails(string itemId);
        string CreateorUpdateCustomerDetail(Customer customerdata);
        string CreateorUpdateCustomerAddress(Customer customersAddress);
        Task<List<SelectListItem>> GetCustomerContactDetail();
        Task<Customer> GetCustomerDataId(string Type, string custId);
        Task<Customer> GetCustomerDetails(string Contactno);
        Customer ValidateContactNumber(string Ext, string ContactNo);
        Task<List<Customer>> GetCustomerMultipleAddId(int custId);
        int UpdateAddressNormal(string custermId, string addressId, string Type);
        int DeleteCustAddress(string custAdressId);
        string InsertSale(Sales dicdata, string createdBy, string branch);
        bool InsertProductforSale(string insertValue);
        Task<List<SalesList>> GetSalesList(string id);
        Task<Sales> GetSalesOrderdetail(string itemId,string hubId);
        List<Customer> GetCustomerContactDetail(string searchTerm = null);
        List<Item> GetItemList(string searchTerm = null,string id = null);
        List<PriceMap> GetItemvaraintList(string ItemId,string id);

        Task<List<Item>> GetObjectFromFile(List<Item> itemDetails, AliyunCredential credential);
        Task<List<SelectListItem>> GetSlotList();
        List<Sales> GetAllSalesOrderList(string branch, string role, string date, string status, string source, string payment,string hubId);
        int UpdateStockData(Stock stockdata);
        Task<List<SelectListItem>> GetCoupenList(string CustomerId, string Status,string hubId);
        int InsertCoupenLog(List<SalesList> salesList, List<SalesList> kitchenList, int couponId, string saleId, string customerId, string LastUpdatedBy);
        int InsertCashDiscount(List<SalesList> salesList, List<SalesList> kitchenList, int couponId, string saleId, string customerId, float discount, float totalamount, string LastUpdatedBy);

        int InsertOldCoupenLog(List<SalesList> salesList, List<SalesList> kitchenList, int couponId, string saleId, string customerId, string LastUpdatedBy);

        void DeleteSalesOrder(string saleId);
        int AddStockData(Item detail);
        List<Sales> GetTodaySalesToPrint(Sales salesdata);
        Task<Sales> GetTodaySalesDetail(Sales salesdata);
        Task<byte[]> ExportExcelofSales(string webRootPath, Sales salesdata);
        Task<List<SelectListItem>> GetAllOrderStatus();
        SalesDetail GetSalesDetail(string Id,string hubId);
        List<SalesList> GetSalesProductListData(string Id,string hubId);
        int SalesList_InsertProductList(SalesList ProductList);
        string SalesList_CreateSalesOrderList(Item ProductList);
        int DeleteSalesList(string SalesListId, string SalesId);
        int RemoveSalesList(string SalesListId, string LastUpdatedBy, string SalesId);
        List<SelectListItem> Employee_NameSL(string UserRole);
        int AssignProductForDelivery(Sales SalesOrderData);
        int UpdateDeliveryDate(Sales SalesOrderData);
        int UpdateDeliveryCharge(Sales SalesOrderData);
        int UpdateOrderdStatus(SalesList salesDetailList);
        SalesDetail GetSaleProductStatus(string SalesOrderId);
        Item GetItemDetailbyPlucode(string PluCode);
        List<Sales> GetPaymentSettlement(string Id, string PaymentSettled_By, string PaymentSettled_for, string Status);
        string updatepaymentsettlement(PaymentSettlement paymentData);
        List<PaymentSettlement> GetPaymentSettlementSummary();
        int UpdatePaymentStatus(Sales SalesOrderData);
        int CustomertblEdit(Sales CusttblEdit);

        List<PendingData> GetAllPendingOrders(PendingData info, string hubId);
        List<SelectListItem> GetAllZipcode();
        Task<byte[]> ExportExcelofPendingSales(string webRootPath, string status, string zipcode, string activeTab, string payment, string hubId);
        void UpdateSalesOtp(string otp, int id);
        string CheckSaleOTPExist(string otp);
        SalesCountData GetSalesCount();
        //Task<byte[]> FilterExcelofSales(string branch, string role, string date, string status, string source, string payment, int a, int b, string startdate, string enddate, string webRootPath);
        Task<byte[]> FilterExcelofSales(string branch, string hubId, string role, string date, string status, string source, string payment, int a, int b, string startdate, string enddate, string webRootPath);

        int InsertStockSalesLog(Stock stockdata);
        bool UpdateOrderList(string ids, string updatedBy);
        bool DeductSalesStock(Sales dicData, string createdBy, string branch);
        int UpdateDeductedStock(SalesList ProductList, string createdBy, string branch);
        SalesDetail GetSalesGST(string Id);
        TaxationInfo GetTaxationDetails();
        List<Sales> GetAllFinancialSaleOrderList(int a, int b, string c, string d,string hubId);
        int UpdateTaxesValue(string salesId,string hubId,int Isactive);
        Task<List<PrintSalesList>> GetSalesListForPrint(string SalesId,string hubId);
        Task<List<TaxPercentageMst>> GetGSTCount(string SalesId);
        Task<Sales> GetSummaryDetail(string id);

        Task<List<Item>> GetHubItemList(string hub, string searchTerm = null);
        List<Sales> GetFinancialSelOrderListHubWise(int a, int b, string c);
        List<Sales> GetFinancialSelOrderListZipWise(int a, int b, string c, string d);
        List<TableInfo> GetTableList(string id);
        List<SelectListItem> GettableList(string id);

        List<SalesList> GetKitchenOrderAllList(int orderStatus, string hubId);

        int UpdateKitcheOrderStatus(Sales info);
        List<SalesList> GetKitchenReadyOrderList(string hubId);
        List<SalesList> GetTableStateList(string hubId);
        List<SalesList> GetTableStateListTKWY(string hubId);
        List<SalesList> GetTableStateListHOD(string hubId);

        int UpdateOrderdStatus(string id1, string LastUpdatedBy, string Orderstatus);
        List<Item> GetMainList(string id);
        List<Item> GetCatgList(string id);

        int CloseOrder(SalesList Info);

        int UpdateAllKitcheOrderStatus(Sales info);

        Task<List<Item>> GetallItemListData(string mainCategory, string condition, string hubId, string ItemName);
        Task<TableInfo> GetTableDetailsAsync(int tableId,string hubId);

        List<SalesList> GetKotListView(string Id,string hubId);
        int UpdateAllKitcheOrderKOTPrintStatus(Sales info);
        Task<List<PrintSalesList>> GetSalesListKOTForPrint(string SalesId, string SalesKitchenId,string hubId);
        Task<List<PrintSalesList>> GetSalesListAllTblKOTForPrint(string SalesId, string SalesKitchenId,string hubId);
        Task<List<PrintSalesList>> GetSaleslistPrintDetails(string SalesId);

        Task<List<PrintSalesList>> GetSalesListConsolidateTblKOTForPrint(string SalesId, string SalesKitchenId);

        int RemoveSalesKitchenList(string SalesOrderId, string LastUpdatedBy, string SalesId, string KitchenListId);
        List<SalesList> GetPendingKotList();
        List<SalesList> GetItemListView(string Id,string hubId);
        List<SalesList> AllGetItemListView(string id);
        List<ItemSizeInfo> GetallItemVarianceList(string hubId);
        int ReadyOrder(SalesList Info);

        BusinessInfo GetBusinessInfoDetails(int id);
        int EditAnnouncementMessage(BusinessInfo add);
        List<Item> GetCategorylist(string MainCatId,string id);
        int CreateCustomer(TableInfo info);
        int CreateBooking(TableInfo info);
        List<TableInfo> Getalltblbookinglist();
        TableInfo GetbookingCount();
        List<SelectListItem> Gettableperferenclist(string id);
        string InserttblBook(Sales dicdata, string createdBy, string branch);
        int CancelBooking(Sales ids);
        SalesCountData GetOrderNotificationCount();
        List<SalesDetail> GetOrderdetails(string id);
        int UpdateNotificationCount(string SalesOrderId, string type);
        List<KotLogs> GetKotLogsView(string Id);
        int KotReprint(string kOTID, string salesId);
        Task<List<PrintSalesList>> GetKOTListKOTForPrint(string kotId, string SalesId);

        List<SalesList> get_OrderTracking( string salesId);
        int AssignShipped(Sales SalesOrderData);
    }
}
