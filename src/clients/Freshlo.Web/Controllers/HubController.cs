using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DemoDecodeURLParameters.Security;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using Freshlo.RI;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models;
using Freshlo.Web.Models.HubVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Freshlo.Web.Controllers
{
    public class HubController : Controller
    {
        private IHubSI _hubSI;
        private IDbConfig _dbConfig { get; }
        private IConfiguration _Config { get; }

        public ISettingSI _settingSI { get; set; }
        private readonly CustomIDataProtection protector;

        public string hubId { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HubController(IHubSI hubSI, ISettingSI settingSI, IHttpContextAccessor httpContextAccessor, IDbConfig dbConfig, IConfiguration config, CustomIDataProtection customIDataProtection)
        {
            _hubSI = hubSI;
            _settingSI = settingSI;
            _dbConfig = dbConfig;
            _Config = config;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
            protector = customIDataProtection;

        }

        [HttpGet]
        public IActionResult Create()
        {
            HubVM vm = new HubVM();
            if (TempData["ViewMessage"] != null)
                vm.ViewMessage = TempData["ViewMessage"] as string;

            if (TempData["ErrorMessage"] != null)
                vm.ErrorMessage = TempData["ErrorMessage"] as string;
            vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            vm.getCurrencyList = _settingSI.GetConfigCurrencyList(hubId);
            ViewBag.businessName = vm.businessInfo.hotel_name;
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return View("Create", vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Hub info, BusinessInfo businessconfig, AliyunCredential credential, string Country)
        {
            try
            {


                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _hubSI.CreateHub(info);
                var hubId = "HID0" + result;
                //int id = 1;
                var symbol = getShortCodeDetails(info.currency);
                var currency = businessconfig.currency;
                info.HubId = hubId;
                var data = _settingSI.CurrencySymbolUpdate(currency, symbol, hubId, info.Country);
                TempData["ViewMessage"] = "Hub Created Successfully";
                return RedirectToAction("Manage", "Hub");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            id = protector.Encode(id);
            Task<Hub> gethubdetails = _hubSI.Hubdetails(id);
            var vm = new HubVM
            {
                getHubdetails = gethubdetails.Result
            };

            if (TempData["ViewMessage"] != null)
                vm.ViewMessage = TempData["ViewMessage"] as string;

            if (TempData["ErrorMessage"] != null)
                vm.ErrorMessage = TempData["ErrorMessage"] as string;
            vm.getCurrencyList = _settingSI.GetConfigCurrencyList(hubId);
            vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = vm.businessInfo.hotel_name;
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return View("Detail", vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Detail(Hub info)
        {
            try
            {
                HubVM vm = new HubVM();
                info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _hubSI.UpdateHub(info);
                TempData["ViewMessage"] = "Hub Updated Successfully";
                return RedirectToAction("Manage", "Hub");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            try
            {

                Task<List<Hub>> getHublist = _hubSI.hublist();
                await Task.WhenAll(getHublist);
                var vm = new HubVM
                {
                    getHublist = getHublist.Result,
                };

                var list = vm.getHublist.Select(p => new
                {
                    v = p.DecodeId = protector.Decode(p.Id.ToString()),
                }).ToList();

                if (TempData["ViewMessage"] != null)
                    vm.ViewMessage = TempData["ViewMessage"] as string;

                if (TempData["ErrorMessage"] != null)
                    vm.ErrorMessage = TempData["ErrorMessage"] as string;
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;

                return View(vm);
            }
            catch (Exception ex)
            {
                return View("Manage");
            }
        }

        [HttpGet]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "success", Data = await _hubSI.DeleteHub(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<int>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = -1 });
            }
        }

        public async Task<JsonResult> IsFacebookEnable(bool isEnable, int branchId)
        {
            try
            {
                int result = await _hubSI.FacebookUpdate(isEnable, branchId);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> IsInstaEnable(bool isEnable, int branchId)
        {
            try
            {
                int result = await _hubSI.InstaUpdate(isEnable, branchId);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> IsTwitterEnable(bool isEnable, int branchId)
        {
            try
            {
                int result = await _hubSI.TwitterUpdate(isEnable, branchId);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> IsSnapchatEnable(bool isEnable, int branchId)
        {
            try
            {
                int result = await _hubSI.SnapchatUpdate(isEnable, branchId);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> IsLinkedInEnable(bool isEnable, int branchId)
        {
            try
            {
                int result = await _hubSI.LinkedInUpdate(isEnable, branchId);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> IsGoogleMapEnable(bool isEnable, int branchId)
        {
            try
            {
                int result = await _hubSI.GoogleMapUpdate(isEnable, branchId);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> IsPrinterestEnable(bool isEnable, int branchId)
        {
            try
            {
                int result = await _hubSI.PrinterestUpdate(isEnable, branchId);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> IsWhatsAppEnable(bool isEnable, int branchId)
        {
            try
            {
                int result = await _hubSI.WhatsAppUpdate(isEnable, branchId);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> IsYoutubeEnable(bool isEnable, int branchId)
        {
            try
            {
                int result = await _hubSI.YoutubeUpdate(isEnable, branchId);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> IsGoogleReviewEnable(bool isEnable, int branchId)
        {
            try
            {
                int result = await _hubSI.GoogleReviewUpdate(isEnable, branchId);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public string getShortCodeDetails(string ShortCode)
        {
            string CurrencySymbol = "Na";
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select CurrencySymbol from tbl_CurrencyType WHERE ShortCode='" + ShortCode + "'", con))
                {
                    con.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            CurrencySymbol = Convert.ToString(r["CurrencySymbol"]);
                        }
                    }
                }
            }
            return CurrencySymbol;
        }
    }
}