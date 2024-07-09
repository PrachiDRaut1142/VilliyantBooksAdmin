using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Freshlo.Web.Controllers
{
    public class PrintController : Controller
    {
        private ISalesSI _salesSI;
        private ISettingSI _settingSI;
        public string hubId { get; set; }
        private PrintDocument printDocument = new PrintDocument();
        private static String RECEIPT = Environment.CurrentDirectory + @"\comprovantes\comprovante.txt";
        private String stringToPrint = "";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PrintController(ISalesSI salesSI, ISettingSI settingSI, IHttpContextAccessor httpContextAccessor)
        {
            _salesSI = salesSI;
            _settingSI = settingSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");


        }
        public async Task<IActionResult>  Index(string  id1, string id2, string received,string remaining,string saved,string AmountTotal,string quant,string item,string DiscountPer,string TotalDiscountAmt,string ActualDiscountAmt)
        {
            try
            {
                var Id = 0;
                var salesvm = new SalesVM();
                salesvm.Saved = saved;
                salesvm.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                salesvm.Orderstatus = "Billing";
                if (id1 != null)
                {
                    salesvm.GetSalesPrintList = await _salesSI.GetSalesListForPrint(id1,hubId);
                    salesvm.GetSalesOrderdetail = await _salesSI.GetSalesOrderdetail(id1,hubId);    
                    salesvm.GetGSTCount = await _salesSI.GetGSTCount(id1);
                    salesvm.TotalGST = await _salesSI.GetSalesGST(id1);
                    salesvm.SummaryDetail = await _salesSI.GetSummaryDetail(id1);
                    salesvm.getBusinessdetails =  _settingSI.GetbusinessInfoDetails(Id);
                    salesvm.gethublist =  _settingSI.gethublist(hubId);
                    salesvm.getTaxationInfo = _settingSI.GetTaxationInfo();

                    //int updateorderStatus = await _salesSI.UpdateOrderdStatus(id1, salesvm.LastUpdatedBy, salesvm.Orderstatus);

                }
                //if (id2 == null && AmountTotal == null)
                //{
                //    //salesvm.GetSalesPrintList = await _salesSI.GetSalesListForPrint(id2);
                //    //salesvm.GetSalesOrderdetail = await _salesSI.GetSalesOrderdetail(id2);
                //    //salesvm.GetGSTCount = await _salesSI.GetGSTCount(id2);
                //    //salesvm.TotalGST = await _salesSI.GetSalesGST(id2);
                //    //salesvm.SummaryDetail = await _salesSI.GetSummaryDetail(id2);
                //    int updateorderStatus = await _salesSI.UpdateOrderdStatus(id1, salesvm.LastUpdatedBy, salesvm.Orderstatus);                  
                //}
                salesvm.currencyInfo = _settingSI.GetCurrencyList(hubId);
                //salesvm.CashRecevd = Math.Round(Convert.ToDouble(salesvm.SummaryDetail.TotalPrice) + Convert.ToDouble(salesvm.SummaryDetail.DeliveryCharges) - Convert.ToDouble(salesvm.SummaryDetail.Wallet), MidpointRounding.AwayFromZero);
                //salesvm.TotalSaleAmount = Math.Round(salesvm.SummaryDetail.TotalSaleAmount, MidpointRounding.AwayFromZero);
                salesvm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = salesvm.businessInfo.hotel_name;
                ViewBag.logoUrl = salesvm.businessInfo.logo_url;
                ViewBag.Print_logourl = salesvm.businessInfo.printlogo_url;
                return View(salesvm);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        public async Task<IActionResult> Delivery(string id, string saved)
        {
            var salesvm = new SalesVM();
          
                salesvm.GetSalesList = await _salesSI.GetSalesList(id);
                salesvm.GetSalesOrderdetail = await _salesSI.GetSalesOrderdetail(id,hubId);
                salesvm.Saved = saved;
            return View(salesvm);
        }
        [Authorize]
        public async Task<IActionResult> PrintPreview(string condition, string ItemName, AliyunCredential credential)
        {
            try
            {
                var salesvm = new SalesVM();
                //var hubId = Convert.ToString(User.FindFirst("branch").Value);
                salesvm.ItemList = await _salesSI.GetallItemList("", "", hubId, ItemName);
                //salesvm.GetObject = await _salesSI.GetObjectFromFile(ItemDetails, credential);
                return View(salesvm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [Authorize]
        public IActionResult PriceTag()
        {
            return View();
        }
        public async Task<JsonResult> ItemListTableData()
        {
            try
            {
               // string hubId = Convert.ToString(User.FindFirst("branch").Value);
                List<PriceTagListItem> list = (await _salesSI.GetallItemList("", "", hubId, null))
                    .Select(s => new PriceTagListItem {
                        ItemId = s.ItemId,
                        ItemName = s.PluName,
                        Quantity = s.Weight,
                        Price = s.SellingPrice,
                        Measurement = s.Measurement
                    }).ToList();
                return Json(new { data = list });
            }
            catch (Exception ex)
            {
                return Json(new { data = new List<PriceTagListItem>() });
            }
        }
        public IActionResult Test()
        {
            return View();
        }

        public async Task<IActionResult> Test1(string salesId, [FromServices] ISalesSI _salesSI)
        {
            var salesvm = new SalesVM();
            try
            {
                salesvm.GetSalesPrintList = await _salesSI.GetSalesListForPrint(salesId,hubId);
                salesvm.GetSalesOrderdetail = await _salesSI.GetSalesOrderdetail(salesId,hubId);
                salesvm.GetGSTCount = await _salesSI.GetGSTCount(salesId);
                salesvm.TotalGST = await _salesSI.GetSalesGST(salesId);
               salesvm.SummaryDetail= await _salesSI.GetSummaryDetail(salesId);
                salesvm.currencyInfo = _settingSI.GetCurrencyList(hubId);
                return View(salesvm);
            }
            catch (Exception ex)
            {
                return View();
            }
            
        }
        public IActionResult Invoice()
        {
            return View();
        }
        //public async Task<IActionResult> TodaySalesToPrint(string Date,string DateRange)
        //{
        //    try
        //    {
        //        Sales salesdata = new Sales();
        //        if (Date == null)
        //        {
        //            var date = DateRange.Split('/');
        //            salesdata.StartDate = date[0];
        //            salesdata.EndDate = date[1];
        //        }
        //        else
        //        {
        //            salesdata.TodayDate = Date;
        //            salesdata.StartDate = null;
        //            salesdata.EndDate = null;
        //        }

        //        var salesvm = new SalesVM();
        //        salesvm.GetTodaySalesList = await _salesSI.GetTodaySalesToPrint(salesdata);
        //        salesvm.GetTodaySalesDetail = await _salesSI.GetTodaySalesDetail(salesdata);

        //        return View(salesvm);
        //    }
        //    catch(Exception ex)
        //    {
        //        return View();
        //    }

        //}

        //[Authorize]
        //public IActionResult TodaySalesToPrint(string datefrom, string dateto)
        //{
        //    try
        //    {
        //        //Sales salesdata = new Sales();           
        //        //salesdata.StartDate = datefrom;
        //        //salesdata.EndDate = dateto;
        //        //var salesvm = new SalesVM();
        //        //salesvm.GetTodaySalesList = await _salesSI.GetTodaySalesToPrint(salesdata);
        //        //salesvm.GetTodaySalesDetail = await _salesSI.GetTodaySalesDetail(salesdata);
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }

        //}


        [Authorize]
        public async Task<IActionResult> TodaySalesToPrint(string Date, string DateRange)
        {
            try
            {
                var salesvm = new SalesVM();
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
                salesvm.GetTodaySalesList = await _salesSI.GetTodaySalesToPrint(salesdata);
                salesvm.GetTodaySalesDetail = await _salesSI.GetTodaySalesDetail(salesdata);
                salesvm.currencyInfo = _settingSI.GetCurrencyList(hubId);
                salesvm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = salesvm.businessInfo.hotel_name;
                ViewBag.logoUrl = salesvm.businessInfo.logo_url;
                ViewBag.Print_logourl = salesvm.businessInfo.printlogo_url;
                return View(salesvm);
            }
            catch (Exception ex)
            {
                return View();
            }

        }


        public async Task<PartialViewResult> _TodaySalesToPrint(string datefrom, string dateto)
        {
            Sales salesdata = new Sales();
            salesdata.StartDate = datefrom;
            salesdata.EndDate = dateto;
            var salesvm = new SalesVM();
            salesvm.GetTodaySalesList = await _salesSI.GetTodaySalesToPrint(salesdata);
            salesvm.GetTodaySalesDetail = await _salesSI.GetTodaySalesDetail(salesdata);
            salesvm.currencyInfo = _settingSI.GetCurrencyList(hubId);
            return PartialView("_PendingOrders", salesvm);
        }


        public async Task<IActionResult> PerviousPrintTest(string id1, string id2, string received, string remaining, string saved, string AmountTotal, string quant, string item, string DiscountPer, string TotalDiscountAmt, string ActualDiscountAmt)

        {
            var salesvm = new SalesVM();

            try
            {
                salesvm.CashReceieved = Convert.ToString(received);
                salesvm.CashRemaining = remaining;
                salesvm.Saved = saved;
                salesvm.Totalamt = AmountTotal;
                salesvm.DiscountPercentage = DiscountPer;
                salesvm.TotalDiscountAmount = Convert.ToDouble(TotalDiscountAmt);
                salesvm.Quantity = quant;
                salesvm.TotalItem = item;
                salesvm.ActualDiscountAmt = Convert.ToDouble(ActualDiscountAmt);
                if (id1 != null)
                {
                    salesvm.GetSalesList = await _salesSI.GetSalesList(id1);
                    salesvm.GetSalesOrderdetail = await _salesSI.GetSalesOrderdetail(id1,hubId);
                }
                if (id2 != null)
                {
                    salesvm.GetSalesList2 = await _salesSI.GetSalesList(id2);
                    salesvm.GetSalesOrderdetail2 = await _salesSI.GetSalesOrderdetail(id2,hubId);
                    salesvm.SubTotal = Convert.ToDouble(AmountTotal) + Convert.ToDouble(salesvm.GetSalesOrderdetail2.DeliveryCharges);
                    salesvm.TotalDiscountAmount = Convert.ToDouble(TotalDiscountAmt) + Convert.ToDouble(salesvm.GetSalesOrderdetail2.DeliveryCharges);

                }

                return View(salesvm);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }


        }

        public async Task<IActionResult> KOTPrint(string id1, string id2, string received, string remaining, string saved, string AmountTotal, string quant, string item, string DiscountPer, string TotalDiscountAmt, string ActualDiscountAmt)
        {
            try
            {
                var salesvm = new SalesVM();
                salesvm.Saved = saved;
                salesvm.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (id1 != null)
                {
                    salesvm.GetSalesPrintList = await _salesSI.GetSalesListKOTForPrint(id1, id2,hubId);
                    salesvm.GetSalesOrderdetail = await _salesSI.GetSalesOrderdetail(id1,hubId);
                    salesvm.GetGSTCount = await _salesSI.GetGSTCount(id1);
                    salesvm.TotalGST = await _salesSI.GetSalesGST(id1);
                    salesvm.SummaryDetail = await _salesSI.GetSummaryDetail(id1);
                }
                if (id2 == null && AmountTotal == null)
                {

                    int updateorderStatus = await _salesSI.UpdateOrderdStatus(id1, salesvm.LastUpdatedBy, salesvm.Orderstatus);

                }
                salesvm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = salesvm.businessInfo.hotel_name;
                ViewBag.logoUrl = salesvm.businessInfo.logo_url;
                return View(salesvm);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> KOTRePrint(string kotId, string salesId, string received, string remaining, string saved, string AmountTotal, string quant, string item, string DiscountPer, string TotalDiscountAmt, string ActualDiscountAmt)
        {
            try
            {
                var salesvm = new SalesVM();
                salesvm.Saved = saved;
                salesvm.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                salesvm.GetSalesPrintList = await _salesSI.GetKOTLISTKOTForPrint(kotId, salesId);
                salesvm.GetSalesOrderdetail = await _salesSI.GetSalesOrderdetail(salesId,hubId);
                //salesvm.GetGSTCount = await _salesSI.GetGSTCount(salesId);
                //salesvm.TotalGST = await _salesSI.GetSalesGST(salesId);
                //salesvm.SummaryDetail = await _salesSI.GetSummaryDetail(salesId);
                salesvm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = salesvm.businessInfo.hotel_name;
                ViewBag.logoUrl = salesvm.businessInfo.logo_url;
                return View(salesvm);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        public async Task<IActionResult> AllTblKOTPrint(string id1, string id2, string received, string remaining, string saved, string AmountTotal, string quant, string item, string DiscountPer, string TotalDiscountAmt, string ActualDiscountAmt,string Id)
        {
            try
            {
                
               var salesvm = new SalesVM();
               salesvm.Saved = saved;
               salesvm.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (id1 != null)
                {

                    salesvm.GetSalesPrintListDetails = await _salesSI.GetSaleslistPrintDetails(id1);
                    salesvm.GetSalesPrintList = await _salesSI.GetSalesListAllTblKOTForPrint(id1, id2,hubId);
                    //salesvm.GetPrintAllInfo = await _salesSI.GetAllTblKOTForPrint(id1, id2);
                }
                if (id2 == null && AmountTotal == null)
                {

                    int updateorderStatus = await _salesSI.UpdateOrderdStatus(id1, salesvm.LastUpdatedBy, salesvm.Orderstatus);

                }
                salesvm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = salesvm.businessInfo.hotel_name;
                ViewBag.logoUrl = salesvm.businessInfo.logo_url;
                return View(salesvm);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> ConsolidateTblKOTPrint(string id1, string id2, string received, string remaining, string saved, string AmountTotal, string quant, string item, string DiscountPer, string TotalDiscountAmt, string ActualDiscountAmt)
        {
            try
            {
                var salesvm = new SalesVM();
                salesvm.Saved = saved;
                salesvm.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (id1 != null)
                {
                    salesvm.GetSalesPrintList = await _salesSI.GetSalesListConsolidateTblKOTForPrint(id1, id2);
                }
                if (id2 == null && AmountTotal == null)
                {

                    int updateorderStatus = await _salesSI.UpdateOrderdStatus(id1, salesvm.LastUpdatedBy, salesvm.Orderstatus);

                }
                salesvm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = salesvm.businessInfo.hotel_name;
                ViewBag.logoUrl = salesvm.businessInfo.logo_url;
                return View(salesvm);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //public async Task<partialviewresult> _getpintlist()
        //{
        //    BaseViewModel vm = new BaseViewModel();
        //    try
        //    {
        //        foreach (var p in PrinterSettings.installedprinters)
        //        {
        //            cmbdefaultprinter.items.add(p);
        //        }
        //        task<list<selectlistitem>> printlist =
        //        await task.whenall(getstatus);
        //        return partialview("_saledetailstblstatelist", vm);
        //    }
        //    catch (exception e)
        //    {
        //        return partialview("_saledetailstblstatelist", "");
        //    }
        //}






    }
}