using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.DomainEntities;
using Freshlo.SI;
using Freshlo.Web.Extensions;
using Freshlo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Freshlo.Web.Models.Sale;
using Freshlo.DomainEntities.Wastage;
using Freshlo.DomainEntities.Stock;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Permissions;
using Freshlo.DomainEntities.PaymentSettlement;
using Freshlo.DomainEntities.DTO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Freshlo.Web.Helpers;
using Microsoft.AspNetCore.Http;
using DemoDecodeURLParameters.Security;

namespace Freshlo.Web.Controllers
{
    public class SaleController : Controller
    {
        private ISalesSI _salesSI;
        private ISettingSI _settingSI;
        private readonly IHostingEnvironment _hostingEnvironment;
        public readonly IHubContext<SignalServer> _signalRHub;
        public readonly IHubContext<SignalRserver1> _signalRRHub;
        public readonly IHubContext<OrderNotification> _Order;
        public string hubId { get; set; }
        public string GstActive { get; set; }
        public string currencttype { get; private set; }
        public string SalesPerson { get; private set; }
        private readonly CustomIDataProtection protector;

        private readonly IHttpContextAccessor _httpContextAccessor;
        //CultureInfo trTR = new CultureInfo("en-US");


        public SaleController(ISalesSI salesSI, CustomIDataProtection customIDataProtection, IHostingEnvironment hostingEnvironment, IHubContext<SignalServer> signalRHub, IHubContext<SignalRserver1> signalRRHub, ISettingSI settingSI, IHubContext<OrderNotification> order, IHttpContextAccessor httpContextAccessor)
        {
            _salesSI = salesSI;
            _hostingEnvironment = hostingEnvironment;
            _signalRHub = signalRHub;
            _signalRRHub = signalRRHub;
            _settingSI = settingSI;
            _Order = order;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
            GstActive = new CookieHelper(_httpContextAccessor).GetCookiesValue("GstActive");
            protector = customIDataProtection;
            //trTR.NumberFormat.CurrencySymbol = "US";

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(string maincategory, string condition, string ItemName, AliyunCredential credential, string Status,TableInfo item)
        {
            try
            {
                if(hubId == null)
                {
                    hubId = "HID01";
                }
                
                var salesvm = new SalesVM();
              
                salesvm.ItemList = await _salesSI.GetallItemList(maincategory, "", hubId, ItemName);
                salesvm.SlotList = await _salesSI.GetSlotList();
                salesvm.GetCoupenList = await _salesSI.GetCoupenList(null, Status,hubId);
                salesvm.GetCoupenList = await _salesSI.GetCoupenList(null, Status,hubId);
                salesvm.tableList = await _salesSI.GetTableList(hubId);
                salesvm.getmainList = await _salesSI.GetMainCatgList(hubId);
                salesvm.getstats = _salesSI.GetAllOrderStatus();
                salesvm.currencyInfo = _settingSI.GetCurrencyList(hubId);
                //salesvm.GetObject = await _salesSI.GetObjectFromFile(ItemDetails, credential);
                salesvm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = salesvm.businessInfo.hotel_name;
                ViewBag.logoUrl = salesvm.businessInfo.logo_url;
                return View(salesvm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetCustomerContactNo(string q)
        {
            try
            {
                //var hubId = Convert.ToString(User.FindFirst("branch").Value);
                List<Customer> customerList = await _salesSI.GetCustomerContactDetail(q);
                return Json(new Message<List<Customer>> { IsSuccess = true, ReturnMessage = "success", Data = customerList });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
   

        [HttpGet]
        public async Task<JsonResult> ItemDetails(string itemId, AliyunCredential credential)
        {
            var salesvm = new SalesVM();
            salesvm.ItemDetails = await _salesSI.GetItemDetails(itemId);
            salesvm.currencyInfo = _settingSI.GetCurrencyList(hubId);

            return Json(salesvm);
        }

        [HttpGet]
        public async Task<JsonResult> ItemSelectList(string q)
        {
            try
            {
                List<Item> itemList = await _salesSI.GetItemList(q,hubId);
                return Json(new Message<List<Item>> { IsSuccess = true, ReturnMessage = "success", Data = itemList });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        [HttpGet]
        public async Task<JsonResult> ItemvariantList(string ItemId,string id)
        {
            try
            {
                return Json(await  _salesSI.GetItemvaraintList(ItemId,hubId));
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public async Task<JsonResult> CreateCustomers(Customer customerdata)
        {

            try
            {
                string resultcustomerAddress = "";
                string resultcustomerDetail = _salesSI.CreateorUpdateCustomerDetail(customerdata);
                if (resultcustomerDetail != "")
                {
                    var custid = resultcustomerDetail.Split('_');
                    customerdata.CustomerId = custid[1];
                    customerdata.Type = "Default";
                }
                else
                {
                    if (customerdata.Id != 0 && customerdata.Addids == 0)
                    {
                        resultcustomerDetail = customerdata.Id + "_" + customerdata.CustomerId;
                        customerdata.Type = "Normal";
                    }
                    else
                    {
                        resultcustomerDetail = customerdata.Id + "_" + customerdata.CustomerId;
                        resultcustomerAddress = Convert.ToString(customerdata.AddId);
                        customerdata.Type = "Default";
                    }

                }
                resultcustomerAddress = _salesSI.CreateorUpdateCustomerAddress(customerdata);
                Customer custdetail = new Customer();
                custdetail = new Customer
                {
                    CustomerId = Convert.ToString(resultcustomerDetail),
                    AddId = Convert.ToString(resultcustomerAddress)
                };
                return Json(custdetail);
            }
            catch (Exception ex)
            {
                return Json("NotSuccess");
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCustomerData(string Type, string Id)
        {
            var salesvm = new SalesVM();
            salesvm.GetCustomerDetail = await _salesSI.GetCustomerDataId(Type, Id);
            return Json(salesvm, new Newtonsoft.Json.JsonSerializerSettings());
        }




        [HttpGet]
        public async Task<JsonResult> GetCustomerDetailsData(string contactNo)
        {
            var salesvm = new SalesVM();
            salesvm.GetCustomerDetail = await _salesSI.GetCustomerDetails(contactNo);
            return Json(salesvm.GetCustomerDetail);
        }

        [HttpGet]
        public JsonResult ValidateContactNo(string Ext, string ContactNo)
        {
            return Json(_salesSI.ValidateContactNumber(Ext, ContactNo));
        }

        public async Task<PartialViewResult> _CustMultiAdd(int Id)
        {
            var salesvm = new SalesVM();
            salesvm.CustomerList = await _salesSI.GetCustomerMultipleAddId(Id);
            salesvm.currencyInfo = _settingSI.GetCurrencyList(hubId);
            return PartialView(salesvm);
        }

    
        public async Task<JsonResult> SetDefault(string custermId, string addressId, string Type)
        {
            int resultUpdateaddress = _salesSI.UpdateAddressNormal(custermId, addressId, Type);
            return Json(true);
        }
        [HttpPost]
        public JsonResult DeleteCustAddress(string custAdressId)
        {
            int Deleteresult = _salesSI.DeleteCustAddress(custAdressId);
            return Json(true);
        }

        public PartialViewResult _CartList(string conditions, string offset, string showid, string search)
        {
            TempData["offset"] = offset;
            return PartialView();
        }

        public async Task<JsonResult> _ItemList(string maincategory, string ItemName, AliyunCredential credential)
            {
            try
            {
                var salesvm = new SalesVM();
                //var hubId = Convert.ToString(User.FindFirst("branch").Value);
                salesvm.ItemList = await _salesSI.GetallItemList(maincategory, "", hubId, ItemName);
                salesvm.ItemVarainceList = await _salesSI.GetallItemVarianceList(hubId);
                salesvm.currencyInfo = _settingSI.GetCurrencyList(hubId);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<SalesVM>("_ItemList", salesvm) });
            }
            catch (Exception ex)
            {

                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }



        public async Task<PartialViewResult> _cancelledOrder(string zipcode)
        {
            PendingData info = new PendingData();
            string status = "";
            info.Branch = Convert.ToString(User.FindFirst("branch").Value);
            info.Role = Convert.ToString(User.FindFirst("userRole").Value);
            info.OrderStatus = "Cancelled";
            info.ZipCode = zipcode;
            info.activeTab = "cancelled";

            try
            {
                return PartialView("_cancelledOrder", await _salesSI.GetAllPendingOrders(info, hubId));
            }
            catch (Exception ex)
            {
                return PartialView("_cancelledOrder", "");
            }
        }



        //public JsonResult InsertSale(Sales dicData)
        //{
        //    var empId = Convert.ToString(User.FindFirst("empId").Value);
        //    var branch = Convert.ToString(User.FindFirst("branch").Value);
        //    var result = false;

        //    var saleId = _salesSI.InsertSale(dicData, empId, branch);

        //    try
        //    {
        //        if (saleId != "")
        //        {
        //            var insertValue = new StringBuilder();
        //            var UpdateValue = new StringBuilder();
        //            var insertstocklog = new StringBuilder();
        //            var tempinsert = new StringBuilder();
        //            if (dicData.MultipleItem != null)
        //            {
        //                foreach (var singleItemDetail in dicData.MultipleItem)
        //                {
        //                    Stock stockdetail = new Stock();
        //                    Item ItemDetail = new Item();
        //                    double stock = 0;
        //                    double stockpurchase;
        //                    var stockadded = "";
        //                    double stocksaleprice = 0;
        //                    var len = singleItemDetail.TrimEnd('|').Split('_').Length;
        //                    if (singleItemDetail.TrimEnd('|').Split('_').Length == 12)
        //                    {
        //                        var itemcode = singleItemDetail.Split('_')[0].TrimStart(',');
        //                        double itemQuantity = Convert.ToDouble(singleItemDetail.Split('_')[2]);
        //                        var itemPrice = singleItemDetail.Split('_')[1];
        //                        var itemtotalAmount = singleItemDetail.Split('_')[4];
        //                        var cate = singleItemDetail.Split('_')[3];
        //                        var weight = singleItemDetail.Split('_')[5];
        //                        var measure = singleItemDetail.Split('_')[6];
        //                        var discount = singleItemDetail.Split('_')[8];
        //                        var Itemtype = singleItemDetail.Split('_')[10];
        //                        double totalstock = Convert.ToDouble(singleItemDetail.Split('_')[7]);
        //                        int stockId = Convert.ToInt32(singleItemDetail.Split('_')[9]);
        //                        var Purchasepricr = Convert.ToString(singleItemDetail.Split('_')[11].TrimEnd('|'));
        //                        if (Itemtype == "Loose")
        //                        {
        //                            stock = Convert.ToDouble(totalstock) - Convert.ToDouble(weight);
        //                            stockpurchase = Convert.ToDouble(stock) * Convert.ToDouble(Purchasepricr);
        //                            stockadded = weight;
        //                            stocksaleprice = Convert.ToDouble(stockadded) * Convert.ToDouble(Purchasepricr);
        //                        }
        //                        else
        //                        {
        //                            stock = Convert.ToDouble(totalstock) - Convert.ToDouble(itemQuantity);
        //                            stockpurchase = Convert.ToDouble(stock) * Convert.ToDouble(Purchasepricr);
        //                            stockadded = Convert.ToString(itemQuantity);
        //                            stocksaleprice = Convert.ToDouble(stockadded) * Convert.ToDouble(Purchasepricr);
        //                        }
        //                        var addstock = Convert.ToDouble(totalstock) + Convert.ToDouble(itemQuantity);
        //                        insertValue.Append("('" + itemcode + "','" + dicData.CustomerId + "'," + itemQuantity + ",'" + itemPrice +
        //                                       "','" + itemtotalAmount + "','" + cate + "','" + saleId + "','" + dicData.DeliveryDate + "','" +
        //                                       weight + "','" + measure + "','" + empId + "'," + discount + "),");
        //                        //UpdateValue.Append(";update [dbo].[tblStock] set [Stock]=" + stock + ",ItemPrice=" + stockpurchase + ",[UpdatedBy]='" + empId + "' where ItemId='" + itemcode + "' and Hub='" + branch + "'");
        //                        //string[] stocklist = { stockadded + "," + stocksaleprice + "," + "Fresh", stock + "," + stockpurchase + "," + measure };
        //                        stockdetail.Id = stockId;
        //                        stockdetail.ItemId = itemcode;
        //                        stockdetail.StockValue = stock;
        //                        stockdetail.Measurement = measure;
        //                        stockdetail.ItemPrice = stockpurchase;
        //                        stockdetail.Hub = branch;
        //                        stockdetail.UpdatedBy = empId;
        //                        stockdetail.salesStock = Convert.ToDouble(stockadded);
        //                        stockdetail.salesPrice = Convert.ToDouble(stocksaleprice);
        //                        if (stockId == 0)
        //                        {
        //                            ItemDetail.ItemId = itemcode;
        //                            ItemDetail.CreatedBy = empId;
        //                            ItemDetail.Branch = branch;
        //                            ItemDetail.Measurement = measure;
        //                            stockId = _salesSI.AddStockData(ItemDetail);
        //                        }
        //                        if (dicData.OrderdStatus != "Confirmed")
        //                        {
        //                            if (stockId != 0)
        //                            {
        //                                _salesSI.UpdateStockData(stockdetail);
        //                                _salesSI.InsertStockSalesLog(stockdetail);

        //                            }
        //                        }
        //                    }

        //                }
        //            }
        //            tempinsert.Append(insertValue.ToString().TrimEnd(','));
        //            tempinsert.Append(UpdateValue.ToString());
        //            if (dicData.coupenId != 0)
        //            {
        //                var coupenlog = _salesSI.InsertCoupenLog(dicData, saleId);
        //            }
        //            result = _salesSI.InsertProductforSale(Convert.ToString(tempinsert));

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //_salesSI.DeleteSalesOrder(saleId);
        //        return Json(result);
        //    }
        //    if (result == true)
        //    {
        //        return Json(saleId);
        //    }
        //    return Json(result);
        //}
        public async Task<JsonResult> InsertSale(Sales dicData,int Isactive)
        {
            dicData.HubId = hubId;
            if (dicData.DeliveryDate != null)
                dicData.Deliverydt = DateTime.ParseExact(dicData.DeliveryDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            else
            {
                dicData.DeliveryDate = DateTime.Now.ToString("MM-dd-yyyy");
                dicData.Deliverydt = DateTime.ParseExact(dicData.DeliveryDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);
            }
            if (dicData.SlotId == "NA")
            {
                dicData.Bags = "0";
            }
            else
            {
                dicData.Bags = "1";
            }
            var empId = Convert.ToString(User.FindFirst("empId").Value);
            //var branch = Convert.ToString(User.FindFirst("branch").Value);
            var result = false;
            try
            {
                var saleId = _salesSI.InsertSale(dicData, empId, hubId);
                await _salesSI.UpdateTaxesValue(saleId, hubId, Isactive);
                if (saleId != "" && dicData.OrderdStatus == "Delivered")
                {
                    await _salesSI.DeductSalesStock(dicData, empId, hubId);
                }
                await _signalRHub.Clients.All.SendAsync("LoadKithcenOrderListData");               
                return Json(saleId);
            }
            catch (Exception e)
            {
                return Json(result);
            }
        }
        [Authorize]
        public async Task<IActionResult> Manage(int a, int b, string c, string d)
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {
                //var date= DateTime.ParseExact(Convert.ToString(DateTime.Now), "MM-dd-yyyy hh:mm:ss", CultureInfo.InvariantCulture);

                ViewBag.TodayDatetime = DateTime.Now.ToString("MM-dd-yyyy");
                var branch = Convert.ToString(User.FindFirst("branch").Value);
                var role = Convert.ToString(User.FindFirst("userRole").Value);

                vm.GetSalesList = await _salesSI.GetAllSalesOrderList(branch,hubId, role, null, null, null, null);

                //var list = vm.GetSalesList.Select(I => new
                //{
                //    v = I.DecodeId = protector.Decode(I.SalesOrderId.ToString()),
                //}).ToList();
                var list1 = vm.GetSalesList.Select(I => new
                {
                    v = I.DecodeId1 = protector.Decode(I.CustomerId.ToString()),
                }).ToList();

                //vm.GetFinacialSalesList = await _salesSI.GetFinancialSelOrderList(null, null, null, null, a, b, c);
                vm.a = a;
                vm.b = b;
                vm.c = c;
                vm.d = d;
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Detail(string Id)
        {
            SalesDetailVM vm = new SalesDetailVM();
            try
            {
                //ViewBag.TodayDatetime = DateTime.Now.ToString("MM-dd-yyyy");
                //var branch = Convert.ToString(User.FindFirst("branch").Value);
                //var role = Convert.ToString(User.FindFirst("userRole").Value);

               // Id = protector.Encode(Id);
              
                Task<List<SelectListItem>> getStatus = _salesSI.GetAllOrderStatus();
                Task<SalesDetail> salesDetail = _salesSI.GetSalesDetail(Id,hubId);
                Task<List<SalesList>> salesLists = _salesSI.GetSalesProductListData(Id,hubId);         
                Task<SalesDetail> salesProdstatus = _salesSI.GetSaleProductStatus(Id);
                Task<List<SelectListItem>> GetCoupenList = _salesSI.GetCoupenList(null, null,hubId);
                Task<SalesDetail> gstDetail = _salesSI.GetSalesGST(Id);
                Task<List<Item>> getMainList = _salesSI.GetMainCatgList(hubId);
                Task<TaxationInfo> getTaxationInfo = _salesSI.GetTaxationDetails();
                Task<List<SalesList>> ordertracking = _salesSI.get_OrderTracking(Id);
                await Task.WhenAll(getStatus, salesDetail, salesLists, salesProdstatus, GetCoupenList, gstDetail, getMainList, getTaxationInfo, ordertracking);
                vm.OrderStatus = getStatus.Result;
                vm.SaleData = salesDetail.Result;
            
                vm.productSalesList = salesLists.Result;
                vm.SaleProductStatus = salesProdstatus.Result;
                vm.GetCoupenList = GetCoupenList.Result;
                vm.salesListcount = vm.productSalesList.Count;
                vm.GSTDetail = gstDetail.Result;
                vm.getmainList = getMainList.Result;
                vm.taxdetails = getTaxationInfo.Result;
                vm.OrderTracking = ordertracking.Result;
                vm.Currencyinfo = _settingSI.GetCurrencyList(hubId);
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                ViewBag.GstActive = GstActive;
                return View(vm);
            }
            catch (Exception e)
            {
                return new StatusCodeResult(500);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Detail(SalesList salesDetailList)
        {
            var empId = Convert.ToString(User.FindFirst("empId").Value);
            var branch = Convert.ToString(User.FindFirst("branch").Value);
            if (branch == null)
            {
                branch = "HID01";
            }
            try
            {
                salesDetailList.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (salesDetailList.OrderStatus != "Billing" && salesDetailList.OrderStatus != null)
                {
                    int updateorderStatus = await _salesSI.UpdateOrderdStatus(salesDetailList);

                }
                if (salesDetailList.OrderStatus == "Billing")
                {
                    int updateorderStatus = await _salesSI.UpdateOrderdStatus(salesDetailList);

                }
                //int deleteresult = await _salesSI.DeleteSalesList(null, salesDetailList.SalesId);
                //int result = await _salesSI.SalesList_InsertProductList(salesDetailList);
                //await _salesSI.UpdateTaxesValue(salesDetailList.SalesId);
                await _signalRHub.Clients.All.SendAsync("LoadKithcenOrderListData");
                string[] split = salesDetailList.getprevurl.Split("/");
                var actionName = split[4];

                if (actionName == "tablestate")
                {
                    return RedirectToAction("tablestate", "Sale");
                }
                if (actionName == "PendingOrders")
                {
                    return RedirectToAction("PendingOrders", "Sale");

                }
                else
                {
                    return RedirectToAction("Detail", "Sale", new { Id = salesDetailList.SalesId });

                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Detail", "Sale", new { Id = salesDetailList.SalesId });
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetcustDetail(string Type, string custId)
        {
            try
            {
                Customer customerList = await _salesSI.GetCustomerDataId(Type, custId);
                return Json(new Message<Customer> { IsSuccess = true, ReturnMessage = "success", Data = customerList });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<PartialViewResult> _AddCart(Item itemDetails)
        {
            try
            {

                itemDetails.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                itemDetails.Branch = Convert.ToString(User.FindFirst("branch").Value);
                SalesVM vm = new SalesVM();
                if (itemDetails.BarcodeData != null)
                {
                    Task<Item> GetitemData = _salesSI.GetItemDetailbyPlucode(itemDetails.BarcodeData);
                    await Task.WhenAll(GetitemData);
                    GetitemData.Result.ActualCost = GetitemData.Result.SellingPrice;
                    itemDetails.ItemId = GetitemData.Result.ItemId;
                    itemDetails.Measurement = GetitemData.Result.Measurement;
                    itemDetails.stockId = GetitemData.Result.stockId;
                    if (GetitemData.Result.ActualCost == 0)
                    {
                        GetitemData.Result.ErrorMessage = "This Plucode is Not Exits";
                    }
                    vm.AddIteminCart = GetitemData.Result;
                    if (GetitemData.Result.ItemId != null)
                    {
                        if (GetitemData.Result.stockId == 0)
                        {
                            itemDetails.stockId = _salesSI.AddStockData(itemDetails);
                        }
                    }
                }
                else
                {
                    vm.AddIteminCart = itemDetails;
                    if (itemDetails.stockId == 0)
                        itemDetails.stockId = _salesSI.AddStockData(itemDetails);
                }
                vm.currencyInfo = _settingSI.GetCurrencyList(hubId);
                return PartialView("_AddCart", vm);
            }
            catch (Exception e)
            {
                return PartialView("_AddCart");

            }
        }
        public async Task<JsonResult> _AddSalesList(Item itemsaleList,int Isactive)
        {
            try
            {
                itemsaleList.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                itemsaleList.Branch = hubId;
                itemsaleList.CreatedOn = DateTime.Now;
                itemsaleList.LastUpdatedOn = DateTime.Now;
                itemsaleList.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                itemsaleList.DeliveryDate = DateTime.Today;
                if (itemsaleList.Remark == null)
                {
                    itemsaleList.Remark = "No Remark";
                }
                string result = await _salesSI.SalesList_CreateSalesOrderList(itemsaleList);
                await _salesSI.UpdateTaxesValue(itemsaleList.SalesId,hubId, Isactive);
                await _signalRHub.Clients.All.SendAsync("LoadKithcenOrderListData");
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public async Task<JsonResult> _RemoveSalesList(string SalesOrderId, string SalesId,int Isactive)
        {
            SalesDetailVM vm = new SalesDetailVM();
            try
            {
                //if (itemsaleList.stockId == 0)
                //    itemsaleList.stockId = _salesSI.AddStockData(itemsaleList);

                // salesDetailList.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                //if (salesDetailList.OrderStatus != null)
                //{
                //    int updateorderStatus = await _salesSI.UpdateOrderdStatus(salesDetailList);

                //}
                string LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                //int deleteresult = await _salesSI.DeleteSalesList(null, salesDetailList.SalesId);
                int result = await _salesSI.RemoveSalesList(SalesOrderId,LastUpdatedBy, SalesId);
                await _salesSI.UpdateTaxesValue(SalesId,hubId, Isactive);
                //Task<List<SalesList>> salesLists = _salesSI.GetSalesProductListData(SalesId);
                //await Task.WhenAll(salesLists);
                //vm.productSalesList = salesLists.Result;
                return Json(true);


            }
            catch (Exception e)
            {
                return Json(false);

            }
        }

        public async Task<JsonResult> _RemoveSalesKitchenList(string SalesOrderId, string SalesId,string KitchenListId ,int Isactive)
        {
            SalesDetailVM vm = new SalesDetailVM();
            try
            {
                //if (itemsaleList.stockId == 0)
                //    itemsaleList.stockId = _salesSI.AddStockData(itemsaleList);

                // salesDetailList.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                //if (salesDetailList.OrderStatus != null)
                //{
                //    int updateorderStatus = await _salesSI.UpdateOrderdStatus(salesDetailList);

                //}
                string LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                //int deleteresult = await _salesSI.DeleteSalesList(null, salesDetailList.SalesId);
                int result = await _salesSI.RemoveSalesKitchenList(SalesOrderId, LastUpdatedBy, SalesId, KitchenListId);
                await _salesSI.UpdateTaxesValue(SalesId,hubId, Isactive);
                //Task<List<SalesList>> salesLists = _salesSI.GetSalesProductListData(SalesId);
                //await Task.WhenAll(salesLists);
                //vm.productSalesList = salesLists.Result;
                return Json(true);


            }
            catch (Exception e)
            {
                return Json(false);

            }
        }


        public async Task<PartialViewResult> _HomeDelivery(Item itemDetails)
        {
            try
            {

                if (itemDetails.BarcodeData != null)
                {
                    Task<Item> GetitemData = _salesSI.GetItemDetailbyPlucode(itemDetails.BarcodeData);
                    await Task.WhenAll(GetitemData);
                    GetitemData.Result.ActualCost = GetitemData.Result.SellingPrice;
                    return PartialView(GetitemData.Result);

                }

                return PartialView(itemDetails);
            }
            catch (Exception ex)
            {
                return PartialView(itemDetails);
            }
        }

        public async Task<JsonResult> _salesOrderList(string date, string status, string source, string payment, int a, int b, string c, string d)
        {
            try
            {            
                var branch = Convert.ToString(User.FindFirst("branch").Value);
                var role = Convert.ToString(User.FindFirst("userRole").Value);
                SalesManageVM vm = new SalesManageVM();
                if (a != 0 && b != 0)
                {
                    vm.GetFinacialSalesList = await  _salesSI.GetFinancialSelOrderList(a, b, c,d,hubId);

                    //var list = vm.GetFinacialSalesList.Select(I => new
                    //{
                    //    v = I.DecodeId = protector.Decode(I.SalesOrderId.ToString()),
                    //}).ToList();
                    var list1 = vm.GetFinacialSalesList.Select(I => new
                    {
                        v = I.DecodeId1 = protector.Decode(I.CustomerId.ToString()),
                    }).ToList();


                    return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<SalesManageVM>("_salesOrderList", vm) });

                }
                //else if (c != 0 && d != 0 && c != "0" && d == "0")
                //{
                //    vm.GetFinacialSalesList = await _salesSI.GetFinancialSelOrderListHubWise(a, b, c);
                //    return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<SalesManageVM>("_salesOrderList", vm) });

                //}
                //else if (a != 0 && b != 0 && c == "0" && d != "0")
                //{
                //    vm.GetFinacialSalesList = await _salesSI.GetFinancialSelOrderListZipWise(a, b, c,d);
                //    return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<SalesManageVM>("_salesOrderList", vm) });

                //}
                else
                {
                    vm.GetSalesList = await _salesSI.GetAllSalesOrderList(branch, role, date, status, source, payment,hubId);

                    //var list = vm.GetSalesList.Select(I => new
                    //{
                    //    v = I.DecodeId = protector.Decode(I.SalesOrderId.ToString()),
                    //}).ToList();
                    var list1 = vm.GetSalesList.Select(I => new
                    {
                        v = I.DecodeId1 = protector.Decode(I.CustomerId.ToString()),
                    }).ToList();



                    return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<SalesManageVM>("_salesOrderList", vm) });

                }
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });
            }
        }
        public JsonResult getItemdata(string itemName)
        {
           // var hubId = Convert.ToString(User.FindFirst("branch").Value);
            if(hubId== null)
            {
                hubId = "HID01";
            }
            return Json(_salesSI.GetallItemList("","", hubId, itemName).Result);
        }
        //public async Task<JsonResult> CoupenList(string CustomerId, string Status)
        //{
        //    var result = await _salesSI.GetCoupenList(CustomerId, Status,hubId);
        //    return Json(result);
        //}

        public async Task<JsonResult> CoupenList(string CustomerId, string Status)
        {
            var result = await _salesSI.GetCoupenList(CustomerId, Status,hubId);
            return Json(result);
        }


        public async Task<FileContentResult> ExportSalesListtoExcel(string Date, string DateRange)
        {

            string webRootPath = _hostingEnvironment.WebRootPath;
            Sales salesdata = new Sales();
            if (Date == null)
            {
                var date = DateRange.Split('/');
                salesdata.StartDate = date[0];
                salesdata.EndDate = date[1];
            }
            else
            {
                salesdata.TodayDate = Date;
                salesdata.StartDate = null;
                salesdata.EndDate = null;
            }

            return File(await _salesSI.ExportExcelofSales(webRootPath, salesdata),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOverview" + DateTime.Now.ToString("MMddyyyyhhmm") + ".xlsx");

        }
        public async Task<JsonResult> AssignProductForDelivery(Sales SalesOrderArray)
        {
            try
            {
                SalesOrderArray.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                int updateResult = await _salesSI.AssignProductForDelivery(SalesOrderArray);
                //await SendOTP(SalesOrderArray.Id,SalesOrderArray.ContactNo, SalesOrderArray.CustomerName);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = "1" });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }
        public async Task<JsonResult> UpdateDeliveryDate(Sales ChangeDeliveryDateofProduct)
        {
            try
            {
                ChangeDeliveryDateofProduct.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                int updatedeliveryDate = await _salesSI.UpdateDeliveryDate(ChangeDeliveryDateofProduct);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = "1" });

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }
        public async Task<JsonResult> UpdateDeliveryCharge(Sales changeDeliveryCharge)
        {
            try
            {
                changeDeliveryCharge.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                int updatedeliveryDate = await _salesSI.UpdateDeliveryCharge(changeDeliveryCharge);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = "1" });

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }
        //public async Task<JsonResult> UpdateOrderdStatus(Sales changeOrderdStatus)
        //{
        //    try
        //    {
        //        changeOrderdStatus.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
        //        int updatedeliveryDate = await _salesSI.UpdateOrderdStatus(changeOrderdStatus);
        //        return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = "1" });

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

        //    }
        //}

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> PaymentSettlement(string Id, string SettledBy, string Settledfor, string Status)
        {
            try
            {
                Task<List<Sales>> getPaymentSettlementlist = _salesSI.GetPaymentSettlement(Id, SettledBy, Settledfor, Status);
                await Task.WhenAll(getPaymentSettlementlist);
                var vm = new PaymentSettlementVM
                {
                    GetPaymentListToSettled = getPaymentSettlementlist.Result
                };
                return View(vm);

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdatePaymentSettlement(PaymentSettlement payment)
        {
            try
            {
                payment.UpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var count = 0;
                string updatedeliveryDate = await _salesSI.updatepaymentsettlement(payment);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = "1" });

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }
        [Authorize]
        public async Task<IActionResult> PaymentSettlementSummary()
        {
            try
            {
                Task<List<PaymentSettlement>> getPaymentSettlementsummarylist = _salesSI.GetPaymentSettlementSummary();
                await Task.WhenAll(getPaymentSettlementsummarylist);
                var vm = new PaymentSettlementVM
                {
                    GetPaymentListToSummary = getPaymentSettlementsummarylist.Result
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<JsonResult> UpdatePaymentStatus(Sales ChangePaymentStatusofProduct)
        {
            try
            {
                ChangePaymentStatusofProduct.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                int updatedeliveryDate = await _salesSI.UpdatePaymentStatus(ChangePaymentStatusofProduct);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = "1" });

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }

        public async Task<JsonResult> CustomertblEdit(Sales CusttblEdit)
        {
            try
            {
                CusttblEdit.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                CusttblEdit.CustomerId = "CI0" + CusttblEdit.CustomerId;
                int CusttblEditData = await _salesSI.CustomertblEdit(CusttblEdit);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = "1" });

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }

        [Authorize]
        public async Task<IActionResult> PendingOrders()
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {
                ViewBag.TodayDatetime = DateTime.Now.ToString("MM-dd-yyyy");
                vm.Zipcode = await _salesSI.GetAllZipcode();
                vm.SalesCount = await _salesSI.GetSalesCount();

                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return View(vm);
        }
        public async Task<PartialViewResult> _pendingOrder(string status, string zipcode)
        {
            try
            {
                PendingData info = new PendingData();
                info.Branch = Convert.ToString(User.FindFirst("branch").Value);
                info.Role = Convert.ToString(User.FindFirst("userRole").Value);
                if (info.Branch == "na")
                {
                    info.Branch = "HID01";
                    hubId = "HID01";
                }
                
                var getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
                getCurrencysybmollist[0].symbol = currencttype;
                info.OrderStatus = status;
                info.ZipCode = zipcode;
                info.activeTab = "pending";
                return PartialView("_pendingOrder", await _salesSI.GetAllPendingOrders(info, hubId));
            }
            catch (Exception ex)
            {
                return PartialView("_pendingOrder", "");
            }

        }

        [Authorize]
        public IActionResult kitchenOrderList()
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {
                if(hubId == null)
                {
                    hubId = "HID01";
                }
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return View(vm);
        }
        public async Task<PartialViewResult> _PendingkitchenOrderList(int orderStatus)
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                vm.GetKitcheOrderList = await _salesSI.GetKichenAllOrderList(orderStatus,hubId);
                vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_PendingkitchenOrderList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_PendingkitchenOrderList", "");
            }
        }
        public async Task<PartialViewResult> _AllKitchOrderList(int orderStatus)
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                vm.GetKitcheOrderList = await _salesSI.GetKichenAllOrderList(orderStatus,hubId);
                vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_AllKitchOrderList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_AllKitchOrderList", "");
            }
        }
        public async Task<PartialViewResult> _AcceptedkitchenOrderList(int orderStatus)
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                vm.GetKitcheOrderList = await _salesSI.GetKichenAllOrderList(orderStatus,hubId);
                vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_AcceptedkitchenOrderList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_AcceptedkitchenOrderList", "");
            }
        }
        public async Task<PartialViewResult> _ReadykitchenOrderList(int orderStatus)
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                vm.GetKitcheOrderList = await _salesSI.GetKichenAllOrderList(orderStatus,hubId);
                vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_ReadykitchenOrderList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_ReadykitchenOrderList", "");
            }
        }
        public async Task<PartialViewResult> _ClosedkitchenOrderList(int orderStatus)
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                vm.GetKitcheOrderList = await _salesSI.GetKichenAllOrderList(orderStatus,hubId);
                vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_ClosedkitchenOrderList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_ClosedkitchenOrderList", "");
            }
        }
        public async Task<PartialViewResult> _CancelledkitchenOrderList(int orderStatus)
        {
            SalesManageVM vm = new SalesManageVM();
            try
            { 
                if(hubId== null)
                {
                    hubId = "HID01";
                }
                vm.GetKitcheOrderList = await _salesSI.GetKichenAllOrderList(orderStatus,hubId);
                vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_CancelledkitchenOrderList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_CancelledkitchenOrderList", vm);
            }
        }

        [Authorize]
        public IActionResult readyOrderList()
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return View(vm);
        }
        public async Task<PartialViewResult> _readyKitchOrderList(string id)
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {
                if (hubId == null){
                    hubId = "HID01";
                }
                vm.GetKitcheOrderList = await _salesSI.GetKitchenReadyOrderList(hubId);
                vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_readyKitchOrderList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_readyKitchOrderList", "");
            }
        }

        //Update KitcheOrder List and Ready OrderList

        [HttpPost]
        public async Task<JsonResult> ApprovalAllKitchenStatusUpdate(Sales info)
        {
            try
            {
                info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                await _salesSI.UpdateAllKitcheOrderStatus(info);
                TempData["ViewMessage"] = "Sales Approval Status Updated";
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "Success", Data = true });
            }
            catch (Exception ex)
            {

                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Danger", Data = null });
            }

        }
        [HttpGet]
        public async Task<JsonResult> ApprovalKitchenStatusUpdate(Sales info)
        {
            try
            {
                if (info.status == "Ready")
                {
                    info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                    await _salesSI.UpdateKitcheOrderStatus(info);
                    await _signalRRHub.Clients.All.SendAsync("LoadReadyKithcenOrderListData");
                    TempData["ViewMessage"] = "Sales Approval Status Updated";
                }
                else
                {
                    info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                    await _salesSI.UpdateKitcheOrderStatus(info);
                    TempData["ViewMessage"] = "Sales Approval Status Updated";
                }
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "Success", Data = true });
            }
            catch (Exception ex)
            {

                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Danger", Data = null });
            }

        }



        public async Task<FileContentResult> ExportSalesPendingListtoExcel(string status, string zipcode, string activeTab, string payment)
        {
            if (activeTab == "payment") { activeTab = "delivered"; payment = "Pending"; }

            string webRootPath = _hostingEnvironment.WebRootPath;

            return File(await _salesSI.ExportExcelofPendingSales(webRootPath, status, zipcode, activeTab, payment,hubId),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Pending-Sales" + DateTime.Now.ToString("MMddyyyyhhmm") + ".xlsx");

        }
        public async Task<JsonResult> GetDeliveryEmployee(string hubId, string orderStatus, string salesId, int id, string number)
        {
            Sales detail = new Sales();
            try
            {
                //await SendOTP(id,number);
                detail.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                detail.SalesOrderId = salesId;
                detail.OrderdStatus = orderStatus;
                detail.DeliveredPerson = SalesPerson;
                //detail.DeliveryNotes = "NA";
                //detail.SalesPerson = "NA";
                int updateResult = await _salesSI.AssignProductForDelivery(detail);
                Task<List<SelectListItem>> getemployeeName = _salesSI.Employee_NameSL(hubId);

                //int updatedeliveryDate = await _salesSI.UpdatePaymentStatus(ChangePaymentStatusofProduct);
                return Json(new Message<List<SelectListItem>>() { IsSuccess = true, ReturnMessage = "Success", Data = getemployeeName.Result });

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }

        [HttpGet]
        public async Task<IActionResult> SendOTP(int salesId, string number, string customerName)
        {
            try
            {
                var otp = await _salesSI.UpdateSalesOtp(salesId);
                // _notificationSI.Create(info);
                string url = "http://13.234.24.83:891/api/values/SendMessage?otp=" + otp + "&contactNo=" + number + "&Name=" + customerName + "";
                using (WebClient client = new WebClient())
                {
                    client.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                    //string myParameters = "{date:\"03-12-2019\",description:\"test\",title:\"test\",img:\"NA\",id:\"02\"}";

                    client.UploadString(url, "Get");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return View();
        }
        public async Task<PartialViewResult> _packedOrder(string zipcode)
        {
            PendingData info = new PendingData();
            info.Branch = Convert.ToString(User.FindFirst("branch").Value);
            info.hubId = hubId;
            info.Role = Convert.ToString(User.FindFirst("userRole").Value);
            info.OrderStatus = "Out for delivery";
            info.ZipCode = zipcode;
            info.activeTab = "Out for delivery";
            try
            {
                return PartialView("_packedOrder", await _salesSI.GetAllPendingOrders(info,hubId));
            }
            catch (Exception ex)
            {
                return PartialView("_packedOrder", "");
            }

        }
        public async Task<PartialViewResult> _dispatchedOrder(string zipcode)
        {
            PendingData info = new PendingData();
            info.Branch = Convert.ToString(User.FindFirst("branch").Value);
            info.Role = Convert.ToString(User.FindFirst("userRole").Value);
            info.OrderStatus = "Dispatched";
            info.ZipCode = zipcode;
            info.activeTab = "dispatched";
            try
            {
                return PartialView("_dispatchedOrder", await _salesSI.GetAllPendingOrders(info,hubId));
            }
            catch (Exception ex)
            {
                return PartialView("_dispatchedOrder", "");
            }
        }

        //public async Task<PartialViewResult> _cancelledOrder(string zipcode)
        //{
        //    PendingData info = new PendingData();
        //    string status = "";
        //    info.Branch = Convert.ToString(User.FindFirst("branch").Value);
        //    info.Role = Convert.ToString(User.FindFirst("userRole").Value);
        //    info.OrderStatus = "Cancelled";
        //    info.ZipCode = zipcode;
        //    info.activeTab = "cancelled";

        //    try
        //    {
        //        return PartialView("_cancelledOrder", await _salesSI.GetAllPendingOrders(info));
        //    }
        //    catch (Exception ex)
        //    {
        //        return PartialView("_cancelledOrder", "");
        //    }
        //}
        public async Task<PartialViewResult> _deliveredOrder(string zipcode)
        {
            PendingData info = new PendingData();
            info.Branch = Convert.ToString(User.FindFirst("branch").Value);
            info.Role = Convert.ToString(User.FindFirst("userRole").Value);
            info.OrderStatus = "Delivered";
            info.ZipCode = zipcode;
            info.activeTab = "delivered";

            try
            {
                return PartialView("_deliveredOrder", await _salesSI.GetAllPendingOrders(info,hubId));
            }
            catch (Exception ex)
            {
                return PartialView("_deliveredOrder", "");
            }
        }
        public async Task<PartialViewResult> _paymentOrder(string zipcode)
        {
            PendingData info = new PendingData();
            info.Branch = Convert.ToString(User.FindFirst("branch").Value);
            info.Role = Convert.ToString(User.FindFirst("userRole").Value);
            info.OrderStatus = "delivered";
            info.ZipCode = zipcode;
            info.activeTab = "payment";
            info.PaymentStatus = "Pending";

            try
            {
                return PartialView("_paymentOrder", await _salesSI.GetAllPendingOrders(info, hubId));
            }
            catch (Exception ex)
            {
                return PartialView("_paymentOrder", "");
            }
        }
        public async Task<JsonResult> GetDeliveryPerson(string hubId, string orderStatus, string salesId)
        {
            Sales detail = new Sales();
            try
            {
                Task<List<SelectListItem>> getemployeeName = _salesSI.Employee_NameSL(hubId);

                //int updatedeliveryDate = await _salesSI.UpdatePaymentStatus(ChangePaymentStatusofProduct);
                return Json(new Message<List<SelectListItem>>() { IsSuccess = true, ReturnMessage = "Success", Data = getemployeeName.Result });

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }
        public async Task<PartialViewResult> _partialOrder(string zipcode)
        {
            PendingData info = new PendingData();
            info.Branch = Convert.ToString(User.FindFirst("branch").Value);
            info.Role = Convert.ToString(User.FindFirst("userRole").Value);
            info.ZipCode = zipcode;
            info.activeTab = "Delivered";
            info.PaymentStatus = "Partially";

            try
            {
                return PartialView("_partialOrder", await _salesSI.GetAllPendingOrders(info, hubId));
            }
            catch (Exception ex)
            {
                return PartialView("_partialOrder", "");
            }
        }
        //public async Task<FileContentResult> GetFilterExcel(string date, string status, string source, string payment, int a, int b, string startdate, string enddate)
        //{
        //    string branch = Convert.ToString(User.FindFirst("branch").Value);
        //    string role = Convert.ToString(User.FindFirst("userRole").Value);
        //    string webRootPath = _hostingEnvironment.WebRootPath;
        //    return File(await _salesSI.FilterExcelofSales(branch, role, date, status, source, payment, a, b, startdate, enddate, webRootPath),
        //        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOverview" + DateTime.Now.ToString("MMddyyyyhhmm") + ".xlsx");

        //}

        public async Task<FileContentResult> GetFilterExcel(string date, string status, string source, string payment, int a, int b, string startdate, string enddate)
        {
            string branch = Convert.ToString(User.FindFirst("branch").Value);
            string role = Convert.ToString(User.FindFirst("userRole").Value);
            //enddate = DateTime.Now.ToString("MM-dd-yyyy");
            //startdate = DateTime.DaysInMonth.ToString("MM-dd-yyyy");
            string webRootPath = _hostingEnvironment.WebRootPath;
            return File(await _salesSI.FilterExcelofSales(branch, hubId, role, date, status, source, payment, a, b, startdate, enddate, webRootPath),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOverview" + DateTime.Now.ToString("MMddyyyyhhmm") + ".xlsx");

        }


        [HttpGet]
        public async Task<bool> GetApiAsync(string transactionId)
        {

            try
            {
                string url = "http://13.234.24.83:891/api/pub/" + transactionId;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return response.IsSuccessStatusCode;
                }
                else
                {
                    return response.IsSuccessStatusCode;

                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //[HttpGet]
        //public async Task<ActionResult<string>> GetApi(string transactionId)
        //{
        //    string url = "/api/pub?transaction_id=" + transactionId;
        //    using (HttpClient client = new HttpClient())
        //    {
        //        return await client.GetStringAsync(url);
        //    }
        //}
        public async Task<JsonResult> UpdateOrder(string ids)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _salesSI.UpdateOrderList(ids, Convert.ToString(User.FindFirst("empId").Value)) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }

        public IActionResult DeliveryOrderManage()
        {
            return View();
        }
        public IActionResult DeliveryOrderDetail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ApplyCoupen(List<SalesList> info,List<SalesList> data, int couponId, string SalesOrderId, string CustomerId,int Isactive)
        {
            try
            {

                if (info.Count != 0)
                {
                    if (couponId != 0)
                    {
                        string LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                        var coupenlog = _salesSI.InsertCoupenLog(info, data, couponId, SalesOrderId, CustomerId, LastUpdatedBy);
                        var data1 =  _salesSI.UpdateTaxesValue(info[0].SalesId,hubId, Isactive);
   
                    }
                    else if (couponId == -1)
                    {
                        string LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                        var coupenlog = _salesSI.InsertCoupenLog(info, data, couponId, SalesOrderId, CustomerId, LastUpdatedBy);
                        var data1 = _salesSI.UpdateTaxesValue(info[0].SalesId,hubId, Isactive);

                    }

                }
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "Success", Data = 1 });
            }
            catch (Exception ex)
            {
                return Json(new Message<int>() { IsSuccess = false, ReturnMessage = "Error", Data = 0 });
            }

        }



        [HttpPost]
        public ActionResult ApplyCashDiscount(List<SalesList> info, List<SalesList> data, int couponId, string SalesOrderId, string CustomerId,float discount, float totalamount)
        {
            try
            {

                if (info.Count != 0)
                {
                    if (couponId == -1)
                    {
                        string LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                        var coupenlog = _salesSI.InsertCashDiscount(info, data, couponId, SalesOrderId, CustomerId, discount, totalamount, LastUpdatedBy);
                    }
                }
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "Success", Data = 1 });
            }
            catch (Exception ex)
            {
                return Json(new Message<int>() { IsSuccess = false, ReturnMessage = "Error", Data = 0 });
            }

        }



        //[HttpPost]
        //public ActionResult ApplyOldCoupen(List<SalesList> info, List<SalesList> data, int couponId, string SalesOrderId, string CustomerId)
        //{
        //    try
        //    {

        //        if (info.Count != 0)
        //        {
        //            if (couponId != 0)
        //            {
        //                string LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
        //                var coupenlog = _salesSI.InsertOldCoupenLog(info,data, couponId, SalesOrderId, CustomerId, LastUpdatedBy);
        //            }
        //        }
        //        return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "Success", Data = 1 });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new Message<int>() { IsSuccess = false, ReturnMessage = "Error", Data = 0 });
        //    }

        //}




        [HttpGet]
        [Authorize]
        public async Task<IActionResult> HubIndex(string condition, string ItemName, AliyunCredential credential, string Status)
        {
            try
            {
                var salesvm = new SalesVM();
                //var hubId = Convert.ToString(User.FindFirst("branch").Value);
                salesvm.ItemList = await _salesSI.GetallItemList("","", hubId, ItemName);
                salesvm.SlotList = await _salesSI.GetSlotList();
                salesvm.GetCoupenList = await _salesSI.GetCoupenList(null, Status,hubId);
                //salesvm.GetObject = await _salesSI.GetObjectFromFile(ItemDetails, credential);
                return View(salesvm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }
        [HttpGet]
        public async Task<JsonResult> ItemSelectHubList(string q)
        {
            try
            {
               // var hubId = Convert.ToString(User.FindFirst("branch").Value);
                List<Item> itemList = await _salesSI.GetHubItemList(hubId,q);
                return Json(new Message<List<Item>> { IsSuccess = true, ReturnMessage = "success", Data = itemList });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        public async Task<IActionResult> tablestate(TableInfo item)
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {

                if(hubId == null)
                {
                    hubId = "HID01";
                }               
                if (User.Claims.Count() > 0)
                {

                    //vm.GetCoupenList = await _salesSI.GetCoupenList(null, null, hubId);
                    //vm.GetTableStateList = await _salesSI.GetTableStateList(hubId);
                    //vm.GetTableStateListTKWY = await _salesSI.GetTableStateListTKWY(hubId);
                    //vm.GetTableStateListHOD = await _salesSI.GetTableStateListHOD(hubId);
                    //vm.getmainList = await _salesSI.GetMainCatgList(hubId);
                    //vm.getcatlist = await _salesSI.GetCatgList(hubId);
                    //vm.AllitemSalesList = await _salesSI.AllGetItemListView(hubId);
                    //vm.Getperferncelist = await _settingSI.GettblPeferencelist();
                    //vm.bookingCount = _salesSI.GetbookingCount();
                    //vm.GetHubList = _settingSI.GetHubList().Result;
                    //vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);

                    //ViewBag.businessName = vm.businessInfo.hotel_name;
                    //ViewBag.logoUrl = vm.businessInfo.logo_url;
                    //ViewBag.TodayDatetime = DateTime.Now.ToString("MM-dd-yyyy");
                    return RedirectToAction("Index", "Sale");
                }
                else
                {
                    return RedirectToAction("Index","Sale");
                }
               
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            
        }

        [HttpGet]
        public async Task<JsonResult> CategoryListMainCatWise(string MainCatId)
        {
            try
            {
                return Json(await _salesSI.GetCategorylist(MainCatId,hubId));
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public async Task<PartialViewResult> _tablestate()
        {
            SalesManageVM vm = new SalesManageVM();
            try
            {
               
                Task<List<SalesList>> getItemlist = _salesSI.AllGetItemListView(hubId);
                await Task.WhenAll(getItemlist);
                vm.AllitemSalesList = getItemlist.Result;
                return PartialView("_tablestate", vm);
            }
            catch (Exception e)
            {
                return PartialView("_tablestate", "");
            }
        }



        [HttpPost]
        public async Task<JsonResult> CloseOrder(SalesList Info)
        {

            try
            {
                if (!Info.SalesId.Contains("SO"))
                {
                    Info.SalesId = "SO0" + Info.SalesId;
                }
                Info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                int result = await _salesSI.CloseOrder(Info);
                Info.currencyInfo = _settingSI.GetCurrencyList(hubId);
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "Success", Data = true });
            }
            catch (Exception ex)
            {
                return Json(new Message<bool>() { IsSuccess = false, ReturnMessage = "error", Data = false });
            }

        }


        [HttpGet]
        public async Task<JsonResult> GetTableStateListCoupenAsync(string Id)
        {
            SalesDetailVM vm = new SalesDetailVM();
            try
            {
                Task<SalesDetail> salesDetail = _salesSI.GetSalesDetail(Id,hubId);
                Task<List<SelectListItem>> GetCoupenList = _salesSI.GetCoupenList(null, null,hubId);
                await Task.WhenAll(salesDetail, GetCoupenList);
                vm.SaleData = salesDetail.Result;
                vm.GetCoupenList = GetCoupenList.Result;
                return Json(vm);
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });
            }
        }

        [HttpGet]
        public async Task<PartialViewResult> _SaleDetailsTblStateList(string Id)
        {
            SalesDetailVM vm = new SalesDetailVM();
            try
            {
                if(hubId== null)
                {
                    hubId = "HID01";
                }
                Task<List<SelectListItem>> getStatus = _salesSI.GetAllOrderStatus();
                Task<SalesDetail> salesDetail = _salesSI.GetSalesDetail(Id,hubId);
                Task<List<SalesList>> salesLists = _salesSI.GetSalesProductListData(Id,hubId);
                //Task<List<SelectListItem>> getemployeeName = _salesSI.Employee_NameSL(Convert.ToString(User.FindFirst("empId").Value));
                Task<SalesDetail> salesProdstatus = _salesSI.GetSaleProductStatus(Id);
                Task<List<SelectListItem>> GetCoupenList = _salesSI.GetCoupenList(null, null,hubId);
                Task<SalesDetail> gstDetail = _salesSI.GetSalesGST(Id);
                Task<List<Item>> getMainList = _salesSI.GetMainCatgList(hubId);
                Task<List<SelectListItem>> getTbllist =  _salesSI.GettableList(hubId);
                Task<List<SalesList>> getkotList = _salesSI.GetKotListView(Id,hubId);
                Task<List<SalesList>> getItemlist = _salesSI.GetItemListView(Id,hubId);
                Task<List<KotLogs>> getkotlogs = _salesSI.GetKotLogsView(Id);
                await Task.WhenAll(getStatus, salesDetail, salesLists, salesProdstatus, GetCoupenList, gstDetail, getMainList, getTbllist, getkotList,getItemlist,getkotlogs);
                vm.OrderStatus = getStatus.Result;
                vm.SaleData = salesDetail.Result;
                vm.productSalesList = salesLists.Result;
                //vm.EmployeeName = getemployeeName.Result;
                vm.SaleProductStatus = salesProdstatus.Result;
                vm.GetCoupenList = GetCoupenList.Result;
                vm.salesListcount = vm.productSalesList.Count;
                vm.GSTDetail = gstDetail.Result;
                vm.getmainList = getMainList.Result;
                vm.tableList = getTbllist.Result;
                vm.kotSaleList = getkotList.Result;
                vm.itemSalesList = getItemlist.Result;
                vm.kotLOGsList = getkotlogs.Result;
                return PartialView("_SaleDetailsTblStateList", vm);
            }
            catch (Exception e)
            {
                return PartialView("_SaleDetailsTblStateList", "");
            }
        }

        public async Task<PartialViewResult> _SaleAllTblKOT(string Id)
        {
            SalesDetailVM vm = new SalesDetailVM();
            try
            {
                Task<List<SalesList>> getTblkotList = _salesSI.GetKotListView(Id, hubId);
                await Task.WhenAll(getTblkotList);             
                vm.kotTblSaleList = getTblkotList.Result;
                return PartialView("_SaleAllTblKOT", vm);
            }
            catch (Exception e)
            {
                return PartialView("_SaleAllTblKOT", "");
            }
        }


        public async Task<PartialViewResult> _SaleConsolidateTblKOT(string Id)
        {
            SalesDetailVM vm = new SalesDetailVM();
            try
            {
                Task<List<SalesList>> getTblkotList = _salesSI.GetKotListView(Id, hubId);
                await Task.WhenAll(getTblkotList);
                vm.kotTblSaleList = getTblkotList.Result;
                return PartialView("_SaleConsolidateTblKOT", vm);
            }
            catch (Exception e)
            {
                return PartialView("_SaleConsolidateTblKOT", "");
            }
        }



        [HttpGet]
        public async Task<JsonResult> _ItemListData(string maincategory, string ItemName, AliyunCredential credential)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                var salesvm = new SalesVM();
                salesvm.ItemList = await _salesSI.GetallItemList(maincategory, "", hubId, ItemName);
                salesvm.ItemVarainceList = await _salesSI.GetallItemVarianceList(hubId);
                salesvm.currencyInfo = _settingSI.GetCurrencyList(hubId);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<SalesVM>("_ItemListData", salesvm) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }


        [HttpGet]
        public async Task<JsonResult> _ItemListUpdateData(string maincategory, string ItemName, AliyunCredential credential)
        {
            try
            {
                var salesvm = new SalesVM();
                //var hubId = Convert.ToString(User.FindFirst("branch").Value);
                salesvm.ItemList = await _salesSI.GetallItemList(maincategory, "", hubId, ItemName);
                salesvm.ItemVarainceList = await _salesSI.GetallItemVarianceList(hubId);
                salesvm.currencyInfo = _settingSI.GetCurrencyList(hubId);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<SalesVM>("_ItemListUpdateData", salesvm) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }


        [HttpGet]
        public async Task<PartialViewResult> _SaleDetailsTblStateListPanelData(int id, int Ordertype)
        {
            SalesVM vm = new SalesVM();
            try
            {
                     
                    
                    Task<TableInfo> gettabledetails = _salesSI.GetTableDetails(id,hubId);
                    await Task.WhenAll(gettabledetails);
                    vm.GetTableDetails = gettabledetails.Result;
                    vm.ViewMessage = Convert.ToString(Ordertype);
                    return PartialView("_SaleDetailsTblStateListPanelData", vm);
            }
            catch (Exception e)
            {
                return PartialView("_SaleDetailsTblStateListPanelData", "");
            }
        }


        [HttpGet]
        public async Task<PartialViewResult> _SaleDetailsTblStateListUpdateItemPanelData(string Id)
        {

            SalesDetailVM vm = new SalesDetailVM();
            try
            {
                Task<List<SelectListItem>> getStatus = _salesSI.GetAllOrderStatus();
                Task<SalesDetail> salesDetail = _salesSI.GetSalesDetail(Id,hubId);
                Task<List<SalesList>> salesLists = _salesSI.GetSalesProductListData(Id,hubId);
                //Task<List<SelectListItem>> getemployeeName = _salesSI.Employee_NameSL(Convert.ToString(User.FindFirst("empId").Value));
                Task<SalesDetail> salesProdstatus = _salesSI.GetSaleProductStatus(Id);
                Task<List<SelectListItem>> GetCoupenList = _salesSI.GetCoupenList(null, null,hubId);
                Task<SalesDetail> gstDetail = _salesSI.GetSalesGST(Id);
                Task<List<Item>> getMainList = _salesSI.GetMainCatgList(hubId);
                Task<List<SalesList>> getkotList = _salesSI.GetKotListView(Id, hubId);
                await Task.WhenAll(getStatus, salesDetail, salesLists, salesProdstatus, GetCoupenList, gstDetail, getMainList, getkotList);
                vm.OrderStatus = getStatus.Result;
                vm.SaleData = salesDetail.Result;
                vm.productSalesList = salesLists.Result;
                //vm.EmployeeName = getemployeeName.Result;
                vm.SaleProductStatus = salesProdstatus.Result;
                vm.GetCoupenList = GetCoupenList.Result;
                vm.salesListcount = vm.productSalesList.Count;
                vm.GSTDetail = gstDetail.Result;
                vm.getmainList = getMainList.Result;
                vm.kotSaleList = getkotList.Result;
                vm.Currencyinfo = _settingSI.GetCurrencyList(hubId);
                return PartialView("_SaleDetailsTblStateListUpdateItemPanelData", vm);
            }
            catch (Exception e)
            {
                return PartialView("_SaleDetailsTblStateListUpdateItemPanelData", "");
            }
        }


        [HttpGet]
        public async Task<PartialViewResult> _SalesDetailsUpdatePanelData(string Id)
        {

            SalesDetailVM vm = new SalesDetailVM();
          
            try
            {
                Task<SalesDetail> salesDetail = _salesSI.GetSalesDetail(Id,hubId);
                await Task.WhenAll(salesDetail);
                vm.SaleData = salesDetail.Result;
                return PartialView("_SalesDetailsUpdatePanelData", vm);
            }
            catch (Exception e)
            {
                return PartialView("_SalesDetailsUpdatePanelData", "");
            }
        }



        [HttpGet]
        public async Task<PartialViewResult> _SaleDetailsProductListUpdateItemPanelData(string Id)
        {

            SalesDetailVM vm = new SalesDetailVM();
            try
            {
                Task<List<SelectListItem>> getStatus = _salesSI.GetAllOrderStatus();
                Task<SalesDetail> salesDetail = _salesSI.GetSalesDetail(Id,hubId);
                Task<List<SalesList>> salesLists = _salesSI.GetSalesProductListData(Id,hubId);
                //Task<List<SelectListItem>> getemployeeName = _salesSI.Employee_NameSL(Convert.ToString(User.FindFirst("empId").Value));
                Task<SalesDetail> salesProdstatus = _salesSI.GetSaleProductStatus(Id);
                Task<List<SelectListItem>> GetCoupenList = _salesSI.GetCoupenList(null, null,hubId);
                Task<SalesDetail> gstDetail = _salesSI.GetSalesGST(Id);
                Task<List<Item>> getMainList = _salesSI.GetMainCatgList(hubId);
                Task<List<SalesList>> getkotList = _salesSI.GetKotListView(Id, hubId);
                await Task.WhenAll(getStatus, salesDetail, salesLists, salesProdstatus, GetCoupenList, gstDetail, getMainList, getkotList);
                vm.OrderStatus = getStatus.Result;
                vm.SaleData = salesDetail.Result;
                vm.productSalesList = salesLists.Result;
                //vm.EmployeeName = getemployeeName.Result;
                vm.SaleProductStatus = salesProdstatus.Result;
                vm.GetCoupenList = GetCoupenList.Result;
                vm.salesListcount = vm.productSalesList.Count;
                vm.GSTDetail = gstDetail.Result;
                vm.getmainList = getMainList.Result;
                vm.kotSaleList = getkotList.Result;
                vm.Currencyinfo = _settingSI.GetCurrencyList(hubId);
                return PartialView("_SaleDetailsProductListUpdateItemPanelData", vm);
            }
            catch (Exception e)
            {
                return PartialView("_SaleDetailsProductListUpdateItemPanelData", "");
            }
        }


        [HttpPost]
        public async Task<JsonResult> ApprovalAllKitchenKOTStatusUpdate(Sales info)
        {
            try
            {
                info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                int count= await _salesSI.UpdateAllKitchekotStatus(info);
                TempData["ViewMessage"] = "KOT List Update";
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "success", Data = count });
            }
            catch (Exception ex)
            {

                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Danger", Data = null });
            }

        }


        [HttpPost]
        public async Task<JsonResult> ReadyOrder(SalesList Info)
        {
            try
            {
                Info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                int result = await _salesSI.ReadyOrder(Info);
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "Success", Data = true });
            }
            catch (Exception ex)
            {
                return Json(new Message<bool>() { IsSuccess = false, ReturnMessage = "error", Data = false });
            }

        }


        [HttpGet]
        public JsonResult GetAnnounceMentMessage(int id)
        {
            BusinessInfoVM vm = new BusinessInfoVM();
            try
            {

                vm.getBusinessInfo = _salesSI.GetBusinessInfoDetails(id);
                return Json(vm);

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }

        public async Task<JsonResult> EditAnnoucement(BusinessInfo add)
        {
            try
            {
                var updateMessage = await _salesSI.EditAnncouementMessage(add);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = "1" });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = "0" });
            }
        }



        [HttpPost]
        [Authorize]
        public JsonResult CreateBooking(TableInfo info)
        {
            try
            {
                info.created_by = Convert.ToString(User.FindFirst("empId").Value);
                if(info.slotTime == null || info.slotTime == "")
                {
                    DateTime d = DateTime.Now;
                    info.slotTime = d.ToString("hh:mm:ss tt");
                }
                if (info.custId == null)
                {
                    info.customerId = _salesSI.CreateCustomer(info);
                    info.custId = "CI0" + info.customerId;
                    info.id = _salesSI.CreateBooking(info);
                }
                else
                {
                    info.id = _salesSI.CreateBooking(info);
                    SMSHelper.SendEmailSMSAsync(info);

                }
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "success", Data = info.id });
            }
            catch (Exception ex)
            {

                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "success", Data = -1 });
            }

        }

        public async Task<PartialViewResult> _getTblbookinglist()
        {
            try
            {
                SalesManageVM vm = new SalesManageVM();
                vm.bookingCount = _salesSI.GetbookingCount();
                vm.bookinglist = await _salesSI.Getalltblbookinglist();
                return PartialView("_getTblbookinglist", vm);

            }
            catch (Exception ex)
            {
                return PartialView("_getTblbookinglist", "1");
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetActionTriggerlist(string id)
        {
            try
            {
                return Json(await _salesSI.Gettableperferenclist(id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<JsonResult> InserttblBook(Sales dicData,int Isactive)
        {
            if (dicData.DeliveryDate != null)
                dicData.Deliverydt = DateTime.ParseExact(dicData.DeliveryDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            else
            {
                dicData.DeliveryDate = DateTime.Now.ToString("MM-dd-yyyy");
                dicData.Deliverydt = DateTime.ParseExact(dicData.DeliveryDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);
            }
            if (dicData.SlotId == "NA")
            {
                dicData.Bags = "0";
            }
            else
            {
                dicData.Bags = "1";
            }
            var empId = Convert.ToString(User.FindFirst("empId").Value);
            var branch = Convert.ToString(User.FindFirst("branch").Value);
            var result = false;
            try
            {
                var saleId = _salesSI.InserttblBook(dicData, empId, branch);
                await _salesSI.UpdateTaxesValue(saleId,hubId, Isactive);                
                await _signalRHub.Clients.All.SendAsync("LoadKithcenOrderListData");
                return Json(saleId);
            }
            catch (Exception e)
            {
                return Json(result);
            }
        }

 
        public async Task<JsonResult> CancelAdvancedBooking(Sales ids)
        {
            try
            {
                ids.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                int cancelBooking = await _salesSI.CancelBooking(ids);
                if(cancelBooking >= 0)
                    return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = "1" });
                else
                    return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "error", Data = null });

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }

        }

        public IActionResult GetNewOrderNotificationCount()
        {
            try
            {
                var count = _salesSI.GetOrderNotificationCount().Result;
                 _Order.Clients.All.SendAsync("updateNotificationCount");
                return Json(count);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            
        }

        [HttpGet]
        public async Task<JsonResult> GetNotificationOrderdtl(string id)
        {
            try
            {
                List<SalesDetail> saleList = await _salesSI.GetOrderdetails(hubId);
                return Json(saleList, new Newtonsoft.Json.JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateNotificationCount(string SalesOrderId, string type)
        {
            try
            {
                int count = await _salesSI.UpdateNotificationCount(SalesOrderId, type);
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "Success", Data = 0 });

            }
            catch (Exception ex)
            {
                return Json(new Message<int> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = 0 });
            }
        }

        public async Task<JsonResult> KotReprint(string KOTID, string salesid)
        {
            try
            {
                int result = await _salesSI.KotReprint(KOTID, salesid);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        public async Task<JsonResult> AssignShipped(Sales SalesOrderArray)
        {
            try
            {
                SalesOrderArray.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                int updateResult = await _salesSI.AssignShipped(SalesOrderArray);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = "1" });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }


    }
}
