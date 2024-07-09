using DemoDecodeURLParameters.Security;
using Freshlo.DomainEntities.Coupen;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models;
using Freshlo.Web.Models.CoupenVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Controllers
{
    public class CoupenController : Controller
    {
        private ICoupenSI _coupenSI;
        public ISettingSI _settingSI { get; set; }
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly CustomIDataProtection protector;
        public string hubId { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CoupenController(ICoupenSI coupenSI, CustomIDataProtection customIDataProtection, IHostingEnvironment hostingEnvironment, ISettingSI settingSI, IHttpContextAccessor httpContextAccessor)
        {
            _coupenSI = coupenSI;
            _hostingEnvironment = hostingEnvironment;
            _settingSI = settingSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
            protector = customIDataProtection;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            CoupenVM VM = new CoupenVM();
            if (TempData["ViewMessage"] != null)
                VM.ViewMessage = TempData["ViewMessage"] as string;

            if (TempData["ErrorMessage"] != null)
                VM.ErrorMessage = TempData["ErrorMessage"] as string;
            VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = VM.businessInfo.hotel_name;
            ViewBag.logoUrl = VM.businessInfo.logo_url;
            return View("Create", VM);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Coupen coupen)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                coupen.Hub = hubId;
                CoupenVM VM = new CoupenVM();
                coupen.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var Coupencode = coupen.CoupenCode.ToUpper();
                var ExitsCode = _coupenSI.GetExistCoupenCode("");
                VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = VM.businessInfo.hotel_name;
                ViewBag.logoUrl = VM.businessInfo.logo_url;
                if (!ExitsCode.Contains(Coupencode))
                {
                    var result = await _coupenSI.CreateCoupen(coupen);
                    coupen.Id = Convert.ToInt32(result);
                    VM.ViewMessage = "Coupen Created Successfully";
                    return View("Create", VM);
                }
                else
                {
                    VM.ErrorMessage = "CoupenCode Already Exits Please Try Another";
                    return View("Create", VM);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Detail(string id)
        {
            id = protector.Encode(id);
            Task<Coupen> getcoupdetails = _coupenSI.GetCoupenDetails(id);
            var VM = new CoupenVM
            {
                GetCoupen = getcoupdetails.Result
            };         
            if (TempData["ViewMessage"] != null)
                VM.ViewMessage = TempData["ViewMessage"] as string;

            if (TempData["ErrorMessage"] != null)
                VM.ErrorMessage = TempData["ErrorMessage"] as string;
            VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = VM.businessInfo.hotel_name;
            ViewBag.logoUrl = VM.businessInfo.logo_url;
            return View("Detail", VM);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Detail(Coupen coupen)
        {
            try
            {
                //if (hubId == null)
                //{
                //    hubId = "HID01";
                //}
                //coupen.Hub = hubId;
                CoupenVM VM = new CoupenVM();
                coupen.ModifiedBy = Convert.ToString(User.FindFirst("empId").Value);
                var Coupencode = coupen.CoupenCode.ToUpper();
                var id = coupen.Id;
                var ExitsCode = _coupenSI.GetExistCoupenCode(Convert.ToString(id)).TrimEnd(',');
                if (!ExitsCode.Contains(Coupencode))
                {
                    var result = await _coupenSI.UpdateCoupen(coupen);
                    coupen.Id = Convert.ToInt32(result);
                    VM.ViewMessage = "Coupen Updated Successfully";
                    return RedirectToAction("Manage", "Coupen");
                }
                else
                {
                    VM.ErrorMessage = "CoupenCode Already Exits Please Try Another";
                    return RedirectToAction("Manage", "Coupen");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Manage()
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<Coupen>> getCoupenlist = _coupenSI.GetCoupenList(hubId);
                await Task.WhenAll(getCoupenlist);
                var VM = new CoupenVM
                {
                    GetCoupenList = getCoupenlist.Result,
                };
                var list = VM.GetCoupenList.Select(p => new
                {
                    v = p.DecodeId = protector.Decode(p.Id.ToString()),
                }).ToList();

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
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _coupenSI.DeleteCoupen(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }
        public async Task<JsonResult> CheckUniqueCouponcode(string CoupenCode)
        {
            try
            {
                var empId = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _coupenSI.CheckUniqueCouponcode(CoupenCode);
                return Json(new Message<string> { IsSuccess = true, ReturnMessage = "success", Data = Convert.ToString(result) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
    }
}