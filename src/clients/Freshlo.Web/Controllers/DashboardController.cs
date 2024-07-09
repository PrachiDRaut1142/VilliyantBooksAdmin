using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.DomainEntities;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models.DashboardVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freshlo.Web.Controllers
{
    public class DashboardController : Controller
    {
        private DashboardSI _dashboardSI { get; }
        private ICustomerSI  _customerSI { get; }

        private IEmployeeSI _employeeSI;

        private ISettingSI _settingSI;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string hubId { get; set; }
        public DashboardController(DashboardSI  dashboardSI, ICustomerSI customerSI, IEmployeeSI employeeSI, ISettingSI settingSI,IHttpContextAccessor httpContextAccessor)
        {
            _dashboardSI = dashboardSI;
            _customerSI = customerSI;
            _employeeSI = employeeSI;
            _settingSI = settingSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Sales()
        {
            return View();
        }


      
        // Financial Code Used
        [HttpGet]
        [Authorize]
        //public async Task<IActionResult> Financial(int today, string HubId, string ZipCode)
        //{
        //    if (hubId == null)
        //    {
        //        hubId = "HID01";
        //    }
        //    FinancialDashboardVM vm = new FinancialDashboardVM();
        //    vm.getHublist = await _employeeSI.GetHublist();
        //    vm.getZiplist = await _settingSI.GetZiplist(hubId);
        //    //vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCount(today);
        //    vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
        //    ViewBag.businessName = vm.businessInfo.hotel_name;
        //    ViewBag.logoUrl = vm.businessInfo.logo_url;
        //    return View(vm);
        //}
        //public async Task<PartialViewResult> _Financial(string datefrom, string dateto)
        //{
        //    if (hubId == null)
        //    {
        //        hubId = "HID01";
        //    }
        //    FinancialDashboardVM vm = new FinancialDashboardVM();
        //    //vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCount(today);
        //    vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCount(datefrom, dateto, hubId);
        //    vm.getCurrencysybmollist =  _settingSI.GetCurrencyList(hubId);
        //    return PartialView("_Financial", vm);
        //}
        public async Task<IActionResult> Financial(int today, string HubId, string ZipCode)
        {
            if (hubId == null)
            {
                hubId = "HID04";
            }
            FinancialDashboardVM vm = new FinancialDashboardVM();
            vm.getHublist = await _employeeSI.GetHublist();
            vm.getZiplist = await _settingSI.GetZiplist(hubId);
            vm.dashboard = _dashboardSI.Getexportlist(hubId).Result;

            //vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCount(today);
            vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = vm.businessInfo.hotel_name;
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return View(vm);
        }
        public async Task<PartialViewResult> _Financial(string datefrom, string dateto)
        {
            if (hubId == null)
            {
                hubId = "HID04";
            }
            FinancialDashboardVM vm = new FinancialDashboardVM();
            //vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCount(today);
            vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCount(datefrom, dateto, hubId);
            vm.dashboard = _dashboardSI.Getexportlist(hubId).Result;
            vm.getCurrencysybmollist = _settingSI.GetCurrencyList(hubId);
            return PartialView("_Financial", vm);
        }


        // Resued Code Later On .....
        public async Task<PartialViewResult> _FinancialHub(int today, string HubId)
        {
            FinancialDashboardVM vm = new FinancialDashboardVM();
            vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCountHubwise(today, HubId);
            return PartialView("_FinancialHub", vm);
        }
        public async Task<PartialViewResult> _FinancialZipCode(int today, string ZipCode)
        {
            FinancialDashboardVM vm = new FinancialDashboardVM();
            vm.GetallCountDashbaord = await _dashboardSI.GetAllDashboardCountZipwise(today, ZipCode);
            return PartialView("_FinancialZipCode", vm);
        }




        // Customer Code Used
        public async Task<IActionResult> Customer(int filter, string Hub, string ZipCode)
        {
            CustomerDashboardVM vm = new CustomerDashboardVM();
            vm.AreawiseCustomer = await _customerSI.GetAreawiseCustomer();
            vm.GetallCustomerOrderSummaryCount = await _customerSI.GetAllCustomerOrderCount();
            //vm.GetallnewCustomerSummaryCount = await _customerSI.GetAllnewCustomerCount(filter);
            vm.getHublist = await _employeeSI.GetHublist();
            vm.getZiplist = await _settingSI.GetZiplist(hubId);
            vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = vm.businessInfo.hotel_name;
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return View(vm);
        }
        public async Task<PartialViewResult> _Customer(int filter)
        {
            CustomerDashboardVM vm = new CustomerDashboardVM();
            vm.AreawiseCustomer = await _customerSI.GetAreawiseCustomer();
            vm.GetallCustomerOrderSummaryCount = await _customerSI.GetAllCustomerOrderCount();
            vm.GetallnewCustomerSummaryCount = await _customerSI.GetAllnewCustomerCount(filter);
            return PartialView("_Customer", vm);
        }
        public async Task<PartialViewResult> _CustomerHubFilter(int filter, string Hub)
        {
            CustomerDashboardVM vm = new CustomerDashboardVM();
            vm.AreawiseCustomer = await _customerSI.GetAreawiseCustomer();
            vm.GetallCustomerOrderSummaryCount = await _customerSI.GetAllCustomerOrderCount();
            vm.GetallnewCustomerSummaryCount = await _customerSI.GetAllnewCustomerCountHubWise(filter, Hub);
            return PartialView("_Customer", vm);
        }
        public async Task<PartialViewResult> _CustomerZipCode(int filter, string ZipCode)
        {
            CustomerDashboardVM vm = new CustomerDashboardVM();
            vm.AreawiseCustomer = await _customerSI.GetAreawiseCustomer();
            vm.GetallCustomerOrderSummaryCount = await _customerSI.GetAllCustomerOrderCount();
            vm.GetallnewCustomerSummaryCount = await _customerSI.GetAllnewCustomerCountZipWise(filter, ZipCode);
            return PartialView("_Customer", vm);
        }


        // Taxation Code Used 
        public async Task<IActionResult> TaxPay(string datefrom, string dateto)
        {
            GSTDashbaordVM vm = new GSTDashbaordVM();
            vm.getHublist = await _employeeSI.GetHublist();
            //vm.getGstList = await _dashboardSI.GetallGstTaxpyInfolist(datefrom, dateto);
            //vm.Gstdashcount = await _dashboardSI.GstDashboardCountInfo(datefrom, dateto);
            //vm.getGstList = await _dashboardSI.GetallGstTaxpyInfolist();
            vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = vm.businessInfo.hotel_name;
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return View("TaxPay", vm);
        }
        public async Task<PartialViewResult> _TaxPayOnLoad()
        {
            GSTDashbaordVM vm = new GSTDashbaordVM();
            vm.getGstList = await _dashboardSI.GetallGstTaxpyInfolist();
            return PartialView("_TaxPayOnLoad", vm);
        }
        public async Task<PartialViewResult> _TaxPay(string datefrom, string dateto)
        {
            GSTDashbaordVM vm = new GSTDashbaordVM();
            vm.getGstList = await _dashboardSI.GetallGstTaxpyInfolist(datefrom, dateto);
            return PartialView("_TaxPay", vm);
        }
        public async Task<PartialViewResult> _TaxPayGSTCount(string datefrom, string dateto)
        {
            GSTDashbaordVM vm = new GSTDashbaordVM();
            vm.Gstdashcount = await _dashboardSI.GstDashboardCountInfo(datefrom, dateto);
            return PartialView("_TaxPayGSTCount", vm);
        }

    }
}