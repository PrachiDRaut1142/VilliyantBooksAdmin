using Freshlo.Common.Exceptions.EncryptionHelper;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.DomainEntities.PaymentSettlement;
using Freshlo.DomainEntities.Stock;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class SalesServices : ISalesSI
    {
        private readonly ISalesRI _salesRI;

        public SalesServices(ISalesRI salesRI)
        {
            _salesRI = salesRI;
        }
        public async Task<List<Item>> GetallItemList(string mainCategory, string condition, string hubId, string ItemName)
        {
            return await _salesRI.GetallItemList(mainCategory, condition, hubId, ItemName);
        }

        public async Task<List<Item>> GetallItemList_1(string ItemName, string CatogeryName)
        {
            return await _salesRI.GetallItemList_1(ItemName, CatogeryName);
        }

        public async Task<Item> GetItemDetails(string itemId)
        {
            return await _salesRI.GetItemDetails(itemId);
        }
        public string CreateorUpdateCustomerDetail(Customer customerdata)
        {
            return _salesRI.CreateorUpdateCustomerDetail(customerdata);
        }
        public string CreateorUpdateCustomerAddress(Customer customersAddress)
        {
            return _salesRI.CreateorUpdateCustomerAddress(customersAddress);
        }
        public async Task<List<SelectListItem>> GetCustomerContactDetail()
        {
            return await _salesRI.GetCustomerContactDetail();
        }
        public async Task<Customer> GetCustomerDataId(string Type, string custId)
        {
            return await _salesRI.GetCustomerDataId(Type, custId);
        }
        public async Task<Customer> GetCustomerDetails(string ContactNo)
        {
            return await _salesRI.GetCustomerDetails(ContactNo);
        }
        public Customer ValidateContactNumber(string Ext, string ContactNo)
        {
            return _salesRI.ValidateContactNumber(Ext, ContactNo);
        }
        public async Task<List<Customer>> GetCustomerMultipleAddId(int custId)
        {
            return await _salesRI.GetCustomerMultipleAddId(custId);
        }
        public int UpdateAddressNormal(string custermId, string addressId, string Type)
        {
            return _salesRI.UpdateAddressNormal(custermId, addressId, Type);
        }
        public int DeleteCustAddress(string custAdressId)
        {
            return _salesRI.DeleteCustAddress(custAdressId);
        }
        public string InsertSale(Sales dicData, string createdBy, string branch)
        {
            return _salesRI.InsertSale(dicData, createdBy, branch);
        }
        public bool InsertProductforSale(string insertValue)
        {
            return _salesRI.InsertProductforSale(insertValue);
        }
        public async Task<List<SalesList>> GetSalesList(string id)
        {
            return await _salesRI.GetSalesList(id);
        }
        public async Task<Sales> GetSalesOrderdetail(string id,string hubId)
        {
            return await _salesRI.GetSalesOrderdetail(id,hubId);
        }



        public Task<List<Item>> GetItemList(string searchTerm,string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetItemList(searchTerm,id);
            });
        }

        public Task<List<PriceMap>> GetItemvaraintList(string ItemId,string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetItemvaraintList(ItemId,id);
            });
        }

        public Task<List<Customer>> GetCustomerContactDetail(string searchTerm)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetCustomerContactDetail(searchTerm);
            });
        }
        public async Task<List<Item>> GetObjectFromFile(List<Item> itemDetails, AliyunCredential credential)
        {
            return await _salesRI.GetObjectFromFile(itemDetails, credential);
        }

        public Task<List<Sales>> GetAllSalesOrderList(string branch, string role, string date, string status, string source, string payment,string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetAllSalesOrderList(branch, role, date, status, source, payment,hubId);
            });
        }
        public Task<List<Sales>> GetTodaySalesToPrint(Sales salesdata)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetTodaySalesToPrint(salesdata);
            });
        }
        public async Task<List<SelectListItem>> GetSlotList()
        {
            return await _salesRI.GetSlotList();
        }
        public int UpdateStockData(Stock stockdata)
        {
            return _salesRI.UpdateStockData(stockdata);
        }
        public int InsertStockSalesLog(Stock stockdata)
        {
            return _salesRI.InsertStockSalesLog(stockdata);
        }
        public async Task<List<SelectListItem>> GetCoupenList(string CustomerId, string Status,string hubId)
        {
            return await _salesRI.GetCoupenList(CustomerId, Status,hubId);
        }
        public int InsertCoupenLog(List<SalesList> salesList, List<SalesList> kitchenList, int couponId, string saleId, string customerId, string LastUpdatedBy)
        {
            return _salesRI.InsertCoupenLog(salesList, kitchenList, couponId, saleId, customerId, LastUpdatedBy);
        }

        public int InsertCashDiscount(List<SalesList> salesList, List<SalesList> kitchenList, int couponId, string saleId, string customerId, float discount, float totalamount, string LastUpdatedBy)
        {
            return _salesRI.InsertCashDiscount(salesList, kitchenList, couponId, saleId, customerId, discount, totalamount, LastUpdatedBy);
        }

        public int InsertOldCoupenLog(List<SalesList> salesList, List<SalesList> kitchenList, int couponId, string saleId, string customerId, string LastUpdatedBy)
        {
            return _salesRI.InsertOldCoupenLog(salesList, kitchenList, couponId, saleId, customerId, LastUpdatedBy);
        }

        public void DeleteSalesOrder(string saleId)
        {
            _salesRI.DeleteSalesOrder(saleId);
        }
        public int AddStockData(Item detail)
        {
            return _salesRI.AddStockData(detail);
        }
        public async Task<Sales> GetTodaySalesDetail(Sales salesdata)
        {
            return await _salesRI.GetTodaySalesDetail(salesdata);
        }
        public Task<byte[]> ExportExcelofSales(string webRootPath, Sales salesdata)
        {
            return _salesRI.ExportExcelofSales(webRootPath, salesdata);
        }
        public async Task<List<SelectListItem>> GetAllOrderStatus()
        {
            return await _salesRI.GetAllOrderStatus();
        }
        public Task<SalesDetail> GetSalesDetail(string Id,string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetSalesDetail(Id,hubId);
            });
        }
        public Task<List<SalesList>> GetSalesProductListData(string Id,string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetSalesProductListData(Id,hubId);
            });
        }
        public Task<int> SalesList_InsertProductList(SalesList ProductList)
        {
            return Task.Run(() =>
            {
                return _salesRI.SalesList_InsertProductList(ProductList);
            });
        }
        public Task<string> SalesList_CreateSalesOrderList(Item ProductList)
        {
            return Task.Run(() =>
            {
                return _salesRI.SalesList_CreateSalesOrderList(ProductList);
            });
        }
        public Task<int> DeleteSalesList(string SalesListId, string SalesId)
        {
            return Task.Run(() =>
            {
                return _salesRI.DeleteSalesList(SalesListId, SalesId);
            });
        }
        public Task<int> RemoveSalesList(string SalesListId, string LastUpdatedBy, string SalesId)
        {
            return Task.Run(() =>
            {
                return _salesRI.RemoveSalesList(SalesListId, LastUpdatedBy, SalesId);
            });
        }
        public Task<List<SelectListItem>> Employee_NameSL(string UserRole)
        {
            return Task.Run(() =>
            {
                return _salesRI.Employee_NameSL(UserRole);
            });
        }
        public Task<int> AssignProductForDelivery(Sales SalesOrderData)
        {
            return Task.Run(() =>
            {
                return _salesRI.AssignProductForDelivery(SalesOrderData);
            });
        }
        public Task<int> UpdateDeliveryDate(Sales SalesOrderData)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdateDeliveryDate(SalesOrderData);
            });
        }
        public Task<int> UpdateDeliveryCharge(Sales SalesOrderData)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdateDeliveryCharge(SalesOrderData);
            });
        }
        public Task<int> UpdateOrderdStatus(SalesList salesDetailList)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdateOrderdStatus(salesDetailList);
            });
        }
        public Task<SalesDetail> GetSaleProductStatus(string SalesOrderId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetSaleProductStatus(SalesOrderId);
            });
        }
        public Task<Item> GetItemDetailbyPlucode(string PluCode)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetItemDetailbyPlucode(PluCode);
            });
        }
        public Task<List<Sales>> GetPaymentSettlement(string Id, string PaymentSettled_By, string PaymentSettled_for, string Status)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetPaymentSettlement(Id, PaymentSettled_By, PaymentSettled_for, Status);
            });
        }
        public Task<string> updatepaymentsettlement(PaymentSettlement paymentData)
        {
            return Task.Run(() =>
            {
                return _salesRI.updatepaymentsettlement(paymentData);
            });
        }
        public Task<List<PaymentSettlement>> GetPaymentSettlementSummary()
        {
            return Task.Run(() =>
            {
                return _salesRI.GetPaymentSettlementSummary();
            });
        }
        public Task<int> UpdatePaymentStatus(Sales SalesOrderData)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdatePaymentStatus(SalesOrderData);
            });
        }

        public Task<int> CustomertblEdit(Sales CusttblEdit)
        {
            return Task.Run(() =>
            {
                return _salesRI.CustomertblEdit(CusttblEdit);
            });
        }

        public Task<List<PendingData>> GetAllPendingOrders(PendingData info, string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetAllPendingOrders(info, id);
            });
        }
        public Task<List<SelectListItem>> GetAllZipcode()
        {
            return Task.Run(() =>
            {
                return _salesRI.GetAllZipcode();
            });
        }
        public Task<byte[]> ExportExcelofPendingSales(string webRootPath, string status, string zipcode, string activeTab, string payment, string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.ExportExcelofPendingSales(webRootPath, status, zipcode, activeTab, payment, id);

            });
        }
        public Task<string> UpdateSalesOtp(int id)
        {
            return Task.Run(() =>
            {
                var otp = GenerateOtp(id);
                _salesRI.UpdateSalesOtp(otp, id);
                return otp;
            });
        }
        //ForOTP
        private string GenerateOtp(int id)
        {
            StringBuilder sbOtp = new StringBuilder();
            string allowedChars = "1234567890";
            Random rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                sbOtp.Append(allowedChars[rand.Next(0, allowedChars.Length)]);
            }
            if (_salesRI.CheckSaleOTPExist(sbOtp.ToString()) != "")
            {
                sbOtp.Clear();
                for (int i = 0; i < 6; i++)
                {
                    sbOtp.Append(allowedChars[rand.Next(0, allowedChars.Length)]);
                }
                if (_salesRI.CheckSaleOTPExist(sbOtp.ToString()) != "")
                {
                    sbOtp.Clear();
                    for (int i = 0; i < 6; i++)
                    {
                        sbOtp.Append(allowedChars[rand.Next(0, allowedChars.Length)]);
                    }
                    if (_salesRI.CheckSaleOTPExist(sbOtp.ToString()) != "")
                    {
                        sbOtp.Clear();
                        for (int i = 0; i < 4; i++)
                        {
                            sbOtp.Append(allowedChars[rand.Next(0, allowedChars.Length)]);
                        }
                        sbOtp.Append(id);
                        return sbOtp.ToString();
                    }

                }
            }

            return sbOtp.ToString();

        }
        //END
        public Task<SalesCountData> GetSalesCount()
        {
            return Task.Run(() =>
            {
                return _salesRI.GetSalesCount();
            });
        }
        //public Task<byte[]> FilterExcelofSales(string branch, string role, string date, string status, string source, string payment, int a, int b, string startdate, string enddate, string webRootPath)
        //{
        //    return Task.Run(() =>
        //    {
        //        return _salesRI.FilterExcelofSales(branch, role, date, status, source, payment, a, b, startdate, enddate, webRootPath);
        //    });
        //}
        public Task<byte[]> FilterExcelofSales(string branch, string hubId, string role, string date, string status, string source, string payment, int a, int b, string startdate, string enddate, string webRootPath)
        {
            return Task.Run(() =>
            {
                return _salesRI.FilterExcelofSales(branch, hubId, role, date, status, source, payment, a, b, startdate, enddate, webRootPath);
            });
        }
        public Task<bool> UpdateOrderList(string ids, string updatedBy)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdateOrderList(ids, updatedBy);
            });
        }
        public Task<bool> DeductSalesStock(Sales dicData, string createdBy, string branch)
        {
            return Task.Run(() =>
            {
                return _salesRI.DeductSalesStock(dicData, createdBy, branch);

            });
        }
        public Task<int> UpdateDeductedStock(SalesList ProductList, string createdBy, string branch)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdateDeductedStock(ProductList, createdBy, branch);

            });
        }
        public Task<SalesDetail> GetSalesGST(string Id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetSalesGST(Id);

            });
        }

        public Task<TaxationInfo> GetTaxationDetails()
        {
            return Task.Run(() =>
            {
                return _salesRI.GetTaxationDetails();

            });
        }

        public Task<List<Sales>> GetFinancialSlOrderList(int a, int b, string c, string d,string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetAllFinancialSaleOrderList(a, b, c, d,hubId);
            });
        }
        public Task<int> UpdateTaxesValue(string salesId,string hubId,int Isactive)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdateTaxesValue(salesId,hubId, Isactive);
            });
        }
        public Task<List<PrintSalesList>> GetSalesListForPrint(string SalesId,string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetSalesListForPrint(SalesId,hubId);
            });
        }
        public Task<List<TaxPercentageMst>> GetGSTCount(string SalesId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetGSTCount(SalesId);
            });
        }
        public Task<Sales> GetSummaryDetail(string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetSummaryDetail(id);
            });

        }
        public Task<List<Item>> GetHubItemList(string hub, string searchTerm = null)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetHubItemList(hub, searchTerm);
            });
        }

        public Task<List<Sales>> GetFinancialSelOrderList(int a, int b, string c, string d,string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetAllFinancialSaleOrderList(a, b, c, d,hubId);
            });
        }

        public Task<List<Sales>> GetFinancialSelOrderListHubWise(int a, int b, string c)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetFinancialSelOrderListHubWise(a, b, c);
            });
        }

        public Task<List<Sales>> GetFinancialSelOrderListZipWise(int a, int b, string c, string d)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetFinancialSelOrderListZipWise(a, b, c, d);
            });
        }

        public Task<List<TableInfo>> GetTableList(string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetTableList(id);
            });
        }

        public Task<List<SelectListItem>> GettableList(string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GettableList(id);
            });
        }

        public Task<List<SalesList>> GetKichenAllOrderList(int orderStatus, string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetKitchenOrderAllList(orderStatus, id);
            });
        }

        public Task<int> UpdateKitcheOrderStatus(Sales info)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdateKitcheOrderStatus(info);
            });
        }

        public Task<List<SalesList>> GetKitchenReadyOrderList(string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetKitchenReadyOrderList(id);
            });
        }

        public Task<List<SalesList>> GetTableStateList(string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetTableStateList(id);
            });
        }

        public Task<List<SalesList>> GetTableStateListTKWY(string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetTableStateListTKWY(hubId);
            });
        }

        public Task<List<SalesList>> GetTableStateListHOD(string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetTableStateListHOD(hubId);
            });
        }


        public Task<int> UpdateOrderdStatus(string id1, string LastUpdatedBy, string Orderstatus)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdateOrderdStatus(id1, LastUpdatedBy, Orderstatus);
            });
        }

        public Task<List<Item>> GetMainCatgList(string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetMainList(id);
            });
        }


        public Task<List<Item>> GetCatgList(string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetCatgList(id);
            });
        }

        public Task<List<SelectListItem>> GetMainCategoryList(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CloseOrder(SalesList Info)
        {
            return Task.Run(() =>
            {

                return _salesRI.CloseOrder(Info);
            });
        }

        public Task<int> UpdateAllKitcheOrderStatus(Sales info)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdateAllKitcheOrderStatus(info);
            });
        }

        public async Task<List<Item>> GetallItemListData(string mainCategory, string condition, string hubId, string ItemName)
        {
            return await _salesRI.GetallItemListData(mainCategory, condition, hubId, ItemName);
        }

        public Task<TableInfo> GetTableDetails(int tableId,string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetTableDetailsAsync(tableId,hubId);
            });
        }

        public Task<List<SalesList>> GetKotListView(string Id,string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetKotListView(Id,hubId);
            });
        }

        public Task<int> UpdateAllKitchekotStatus(Sales info)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdateAllKitcheOrderKOTPrintStatus(info);
            });
        }

        public Task<List<PrintSalesList>> GetSalesListKOTForPrint(string SalesId, string SalesKitchenId,string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetSalesListKOTForPrint(SalesId, SalesKitchenId,hubId);
            });
        }
        public Task<List<PrintSalesList>> GetSalesListAllTblKOTForPrint(string SalesId, string SalesKitchenId,string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetSalesListAllTblKOTForPrint(SalesId, SalesKitchenId,hubId);
            });
        }

        public Task<List<PrintSalesList>> GetSaleslistPrintDetails(string SalesId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetSaleslistPrintDetails(SalesId);
            });
        }

        public Task<List<PrintSalesList>> GetSalesListConsolidateTblKOTForPrint(string SalesId, string SalesKitchenId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetSalesListConsolidateTblKOTForPrint(SalesId, SalesKitchenId);
            });
        }

        public Task<int> RemoveSalesKitchenList(string SalesOrderId, string LastUpdatedBy, string SalesId, string KitchenListId)
        {
            return Task.Run(() =>
            {
                return _salesRI.RemoveSalesKitchenList(SalesOrderId, LastUpdatedBy, SalesId, KitchenListId);
            });
        }


        public Task<List<SalesList>> GetPendingKotList()
        {
            return Task.Run(() =>
            {
                return _salesRI.GetPendingKotList();
            });
        }

        public Task<List<SalesList>> GetItemListView(string Id,string hubId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetItemListView(Id,hubId);
            });
        }
        public Task<List<SalesList>> AllGetItemListView(string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.AllGetItemListView(id);
            });
        }

        public Task<List<ItemSizeInfo>> GetallItemVarianceList(string hubId)
        {
            return Task.Run(() => {
                return _salesRI.GetallItemVarianceList(hubId);
            });
        }

        public Task<int> ReadyOrder(SalesList Info)
        {
            return Task.Run(() =>
            {

                return _salesRI.ReadyOrder(Info);
            });
        }

        public BusinessInfo GetBusinessInfoDetails(int id)
        {
            return _salesRI.GetBusinessInfoDetails(id);
        }

        public Task<int> EditAnncouementMessage(BusinessInfo add)
        {
            return Task.Run(() =>
            {
                return _salesRI.EditAnnouncementMessage(add);
            });
        }

        public Task<List<Item>> GetCategorylist(string MainCatId,string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetCategorylist(MainCatId,id);
            });
        }

        public int CreateCustomer(TableInfo info)
        {
            return _salesRI.CreateCustomer(info);
        }

        public int CreateBooking(TableInfo info)
        {
            return _salesRI.CreateBooking(info);
        }

        public Task<List<TableInfo>> Getalltblbookinglist()
        {
            return Task.Run(() =>
            {
                return _salesRI.Getalltblbookinglist();
            });
        }

        public Task<TableInfo> GetbookingCount()
        {
            return Task.Run(() =>
            {
                return _salesRI.GetbookingCount();
            });
        }

        public Task<List<SelectListItem>> Gettableperferenclist(string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.Gettableperferenclist(id);
            });
        }

        public string InserttblBook(Sales dicData, string createdBy, string branch)
        {
            return _salesRI.InserttblBook(dicData, createdBy, branch);
        }

        public Task<int> CancelBooking(Sales ids)
        {
            return Task.Run(() =>
            {
                return _salesRI.CancelBooking(ids);
            });
        }

        public Task<SalesCountData> GetOrderNotificationCount()
        {
            return Task.Run(() =>
            {
                return _salesRI.GetOrderNotificationCount();
            });
        }

        public Task<List<SalesDetail>> GetOrderdetails(string id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetOrderdetails(id);
            });
        }

        public Task<int> UpdateNotificationCount(string SalesOrderId, string type)
        {
            return Task.Run(() =>
            {
                return _salesRI.UpdateNotificationCount(SalesOrderId, type);
            });
        }

        public Task<List<KotLogs>> GetKotLogsView(string Id)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetKotLogsView(Id);
            });
        }

        public Task<int> KotReprint(string KOTID, string SalesId)
        {
            return Task.Run(() =>
            {
                return _salesRI.KotReprint(KOTID, SalesId);
            });
        }

        Task<List<PrintSalesList>> ISalesSI.GetKOTLISTKOTForPrint(string kotId, string SalesId)
        {
            return Task.Run(() =>
            {
                return _salesRI.GetKOTListKOTForPrint(kotId, SalesId);
            });
        }

        public Task<List<SalesList>> get_OrderTracking(string salesId)
        {
            return Task.Run(() =>
            {
                return _salesRI.get_OrderTracking(salesId);
            });
        }

        public Task<int> AssignShipped(Sales SalesOrderData)
        {
            return Task.Run(() =>
            {
                return _salesRI.AssignShipped(SalesOrderData);
            });
        }
    }
}
