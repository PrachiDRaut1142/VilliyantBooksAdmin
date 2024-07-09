using Freshlo.SI;
using Freshlo.Web.Models.DashboardVM;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Freshlo.Web.Models.Sale;
using System.Collections.Generic;
using Freshlo.DomainEntities;
using System.Data.SqlClient;
using System.Data;
using System;
using Freshlo.Web.Models.SaleSummaryVM;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models.ItemMaster;

namespace Freshlo.Web.Controllers
{
    public class ManagementController : Controller
    {
        private ISalesSI _salesSI;
        private DashboardSI _dashboardSI { get; }
        private readonly IHostingEnvironment _hostingEnvironment;
        private ISettingSI _settingSI;
        private IItemSI _itemSI;
        private SaleSummarySI _saleSummarySI { get; }
        public string hubId { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ManagementController(DashboardSI dashboardSI, ISettingSI settingSI, ISalesSI salesSI, SaleSummarySI saleSummarySI, IItemSI itemSI, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _dashboardSI = dashboardSI;
            _settingSI = settingSI;
            _salesSI = salesSI;
            _saleSummarySI = saleSummarySI;
            _itemSI = itemSI;
            _hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
        }

        [HttpGet]
        [Authorize]



        public async Task<IActionResult> SaleSummary(string datefrom, string dateto, string id)
        {

            FinancialDashboardVM vm = new FinancialDashboardVM();
            if (hubId == null)
            {
                hubId = "HID01";
            }
            vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCount(datefrom, dateto,hubId);
            vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
            vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return View(vm);
        }



        public async Task<PartialViewResult> _SaleSummary(string datefrom, string dateto,string id)
        {

            FinancialDashboardVM vm = new FinancialDashboardVM();
            if (hubId == null)
            {
                hubId = "HID01";
            }
            vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCount(datefrom, dateto,hubId);
            vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
            vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return PartialView("_SaleSummary", vm);
        }
        public async Task<PartialViewResult> _discount(string datefrom, string dateto,string id)
        {

            FinancialDashboardVM vm = new FinancialDashboardVM();
            if (hubId == null)
            {
                hubId = "HID01";
            }
            vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCount(datefrom, dateto,hubId);

            vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
            return PartialView("_discount", vm);
        }
        public async Task<PartialViewResult> _SaleMonths(string datefrom, string dateto,string id)
        {
            FinancialDashboardVM vm = new FinancialDashboardVM();
            if (hubId == null)
            {
                hubId = "HID01";
            }
            vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCount(datefrom, dateto,hubId);

            vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
            return PartialView("_SaleMonths", vm);
        }


        public async Task<JsonResult> SaleCashSummary(string datefrom, string dateto,string id)
        {
            SalesSummary vm = new SalesSummary();
           if(hubId == null)
            {
                hubId = "HID01";
            }
            vm.cashSummary = await _saleSummarySI.GetCashSummary(datefrom, dateto,hubId);
            var cashlist = vm.cashSummary;
            cashlist = cashlist.GroupBy(x => x.CreatedDate).Select(x => x.Last()).ToList();
            vm.cashSummary = cashlist;



            vm.cardSummary = _saleSummarySI.GetCardSummary(datefrom, dateto,hubId).Result;
            var cardlist = vm.cardSummary;
            cardlist = cardlist.GroupBy(x => x.CreatedDate).Select(x => x.Last()).ToList();
            vm.cardSummary = cardlist;


            vm.upiSummary = _saleSummarySI.GetUpiSummary(datefrom, dateto,hubId).Result;
            var upilist = vm.upiSummary;
            upilist = upilist.GroupBy(x => x.CreatedDate).Select(x => x.Last()).ToList();
            vm.upiSummary = upilist;

            vm.pendingSummary = _saleSummarySI.GetPendingSummary(datefrom, dateto,hubId).Result;
            var pendinglist = vm.pendingSummary;
            pendinglist = pendinglist.GroupBy(x => x.CreatedDate).Select(x => x.Last()).ToList();
            vm.pendingSummary = pendinglist;



            return Json(vm);
        }
        public async Task<JsonResult> monthlyCashSummary(string datefrom, string dateto, string id)
        {
            SalesSummary vm = new SalesSummary();
            if (hubId == null)
            {
                hubId = "HID01";
            }
            vm.monthcashSummary = await _saleSummarySI.GetmonthCashSummary(datefrom, dateto,hubId);
            var cashlist = vm.monthcashSummary;
            cashlist = cashlist.GroupBy(x => x.Month).Select(x => x.Last()).ToList();
            vm.monthcashSummary = cashlist;



            vm.monthcardSummary = _saleSummarySI.GetmonthCardSummary(datefrom, dateto,hubId).Result;
            var cardlist = vm.monthcardSummary;
            cardlist = cardlist.GroupBy(x => x.Month).Select(x => x.Last()).ToList();
            vm.monthcardSummary = cardlist;


            vm.monthupiSummary = _saleSummarySI.GetmonthUpiSummary(datefrom, dateto,hubId).Result;
            var upilist = vm.monthupiSummary;
            upilist = upilist.GroupBy(x => x.Month).Select(x => x.Last()).ToList();
            vm.monthupiSummary = upilist;

            vm.monthpendingSummary = _saleSummarySI.GetmonthPendingSummary(datefrom, dateto,hubId).Result;
            var pendinglist = vm.monthpendingSummary;
            pendinglist = pendinglist.GroupBy(x => x.Month).Select(x => x.Last()).ToList();
            vm.monthpendingSummary = pendinglist;
            return Json(vm);
        }
        public async Task<JsonResult> DiscountSummary1(string datefrom, string dateto,string id)
        {
            SalesSummary vm = new SalesSummary();
            if (hubId == null)
            {
                hubId = "HID01";
            }
            vm.discountsummary = await _saleSummarySI.GetDiscountSummary(datefrom, dateto,hubId);
            var Discountlist = vm.discountsummary;
            Discountlist = Discountlist.GroupBy(x => x.CreatedDate).Select(x => x.Last()).ToList();
            vm.discountsummary = Discountlist;
            return Json(vm);
        }


       

        // Item Dashboard 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ItemDashboard( string id ,ItemMasters item)
        {   
            item.Hub = hubId;
            ItemMasterVM vm = new ItemMasterVM();
            vm.GEtItemCountDashboard = await _itemSI.GEtItemCountDashboard(hubId);
            vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return View(vm);
        }

        [HttpGet] 
        public async Task<IActionResult> ItemDetails(string datefrom, string dateto, string id, int type)
        {

            ItemMasterVM vm = new ItemMasterVM();
            if (hubId == null)
            {
                hubId = "HID01";
            }
            //vm.GetItemDetails = await _itemSI.GetItemDetails(datefrom, dateto, hubId);
            vm.GetvegItemDetails = await _itemSI.GetvegItemDetails(datefrom, dateto, hubId,type);
            vm.Itemvegnonvegcount = await _itemSI.Itemvegnonvegcount(datefrom, dateto, hubId);
            vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return View(vm);
        }

        [HttpGet]
        public async Task<PartialViewResult> _itemdetails(string datefrom, string dateto, string id,int type,ItemMasters item)
        {

            ItemMasterVM vm = new ItemMasterVM();
            if (hubId == null)
            {
                hubId = "HID01";
            }
          
            // vm.GetItemDetails = await _itemSI.GetItemDetails(datefrom, dateto, hubId);
            vm.GetvegItemDetails = await _itemSI.GetvegItemDetails(datefrom, dateto, hubId, type);
            vm.Itemvegnonvegcount = await _itemSI.Itemvegnonvegcount(datefrom, dateto, hubId);
                return PartialView("_itemdetails", vm);
            
            

        }



        // Later On Used View
        public IActionResult DiscountSummary()
        {
            return View();
        }

        public IActionResult TimeMotion()
        {
            return View();
        }

        public IActionResult ItemDetailsCount()
        {
            return View();
        }

    }
}
    
