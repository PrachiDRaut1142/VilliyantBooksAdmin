using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Vendor;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models;
using Freshlo.Web.Models.VendorVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Freshlo.Web.Controllers
{
    public class VendorController : Controller
    {
        private IVendorSI _vendorSI;
        public ISettingSI _settingSI { get; set; }

        private readonly IHostingEnvironment _hostingEnvironment;

        public string hubId { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public VendorController(IVendorSI vendorSI, IHostingEnvironment hostingEnvironment, ISettingSI settingSI, IHttpContextAccessor httpContextAccessor)
        {
            _vendorSI = vendorSI;
            _hostingEnvironment = hostingEnvironment;
            _settingSI = settingSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
        }

        [HttpGet]
        public IActionResult Create()
        {

            Task<List<SelectListItem>> getMainCatlist =  _vendorSI.GetMainCategoryList();
            var VM = new VendorVm
            {
                GetMainCatlist = getMainCatlist.Result
            };
            if (TempData["ViewMessage"] != null)
            {
                VM.ViewMessage = TempData["ViewMessage"] as string;
            }
            else if (TempData["ErrorMessage"] != null)
            {
                VM.ErrorMessage = TempData["ErrorMessage"] as string;
            }
            VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = VM.businessInfo.hotel_name;
            ViewBag.logoUrl = VM.businessInfo.logo_url;
            return View(VM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vendor info)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                info.Hub = hubId;
                var addVendor = await _vendorSI.AddVendor(info);
                TempData["ViewMessage"] = "Vendor Created Successfully";
                return RedirectToAction("Manage");

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }


        public async Task<IActionResult> Manage()
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<Vendor>> getVendorlist = _vendorSI.GetVendorList(hubId);
                await Task.WhenAll(getVendorlist);
                var VM = new VendorVm
                {
                    getAllVendorList = getVendorlist.Result,
                };

                if (TempData["ViewMessage"] != null)
                    VM.ViewMessage = TempData["ViewMessage"] as string;

                if (TempData["ErrorMessage"] != null)
                    VM.ErrorMessage = TempData["ErrorMessage"] as string;

                VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = VM.businessInfo.hotel_name;
                ViewBag.logoUrl = VM.businessInfo.logo_url;
                return View("Manage", VM);
            }
            catch (Exception ex)
            {
                return View("Manage");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int Id)
        {

            try
            {
                VendorVm VM = new VendorVm();
                VM.getvendordetails = _vendorSI.GetVendorDetails(Id);
                VM.GetCategorieslit = await _vendorSI.GetCategorieslist(Id);
                VM.GetMainCatlist = await _vendorSI.GetMainCategoryList();
                if (TempData["ViewMessage"] != null)
                    VM.ViewMessage = TempData["ViewMessage"] as string;

                if (TempData["ErrorMessage"] != null)
                    VM.ErrorMessage = TempData["ErrorMessage"] as string;

                VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = VM.businessInfo.hotel_name;
                ViewBag.logoUrl = VM.businessInfo.logo_url;
                return View("Detail", VM);


            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Detail(Vendor info)
        {
            try
            {
                //if (hubId == null)
                //{
                //    hubId = "HID01";
                //}
                //info.Hub = hubId;
                VendorVm VM = new VendorVm();
                info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _vendorSI.EditVendor(info);                  
                TempData["ViewMessage"] = "Vendor Updated Successfully";
                return RedirectToAction("Manage");

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<JsonResult> DeleteVendor(int Id)
        {
            try
            {
                var removeVendor = _vendorSI.DeleteVendor(Id);
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "Success", Data = await removeVendor });
            }
            catch (Exception ex)
            {

                return Json(new Message<int>() { IsSuccess = false, ReturnMessage = "Error", Data = -1 });
            }
        }
    }
}
