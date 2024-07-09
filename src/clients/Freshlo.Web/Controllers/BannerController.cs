using DemoDecodeURLParameters.Security;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Banner;
using Freshlo.DomainEntities.Hub;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models;
using Freshlo.Web.Models.BannerVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Controllers
{
    public class BannerController : Controller
    {
        private BannerSI _bannerSI;
        public ISettingSI _settingSI { get; set; }
        public IEmployeeSI _employeeSI { get; set; }
        private readonly IHostingEnvironment _hostingEnvironment;
        public string hubId { get; set; }
        private readonly CustomIDataProtection protector;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BannerController(BannerSI bannerSI, CustomIDataProtection customIDataProtection, IHostingEnvironment hostingEnvironment, ISettingSI settingSI, IEmployeeSI employeeSI, IHttpContextAccessor httpContextAccessor)
        {
            _bannerSI = bannerSI;
            _hostingEnvironment = hostingEnvironment;
            _settingSI = settingSI;
            _employeeSI = employeeSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
            protector = customIDataProtection;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            Task<List<Banner>> getMainCattegorylist = _bannerSI.GetMancategoreylist();
            Task<List<Hub>> getHublist = _employeeSI.GetHublist();
            var VM = new BannerVM
            {             
                GetMainCategoryList = getMainCattegorylist.Result,    
                GetHubList= getHublist.Result,
            };
            if (TempData["ViewMessage"] != null)
                VM.ViewMessage = TempData["ViewMessage"] as string;

            if (TempData["ErrorMessage"] != null)
                VM.ErrorMessage = TempData["ErrorMessage"] as string;
            VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = VM.businessInfo.hotel_name;
            ViewBag.logoUrl = VM.businessInfo.logo_url;
            ViewBag.aliyunPath = VM.businessInfo.aliyunPath;
            return View("Create", VM);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Banner banner, AliyunCredential credential)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                banner.Branch = hubId;
                var aliyunfolder = banner.Aliyunkey;
                banner.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _bannerSI.CreateBanner(banner);
               banner.BannerId = Convert.ToString(result);
                if (banner.imageFiles != null && result > 0)
                {
                    string fullPath = "";
                    string folderName = "Documents\\";
                    var rootFolder = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(rootFolder, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    fullPath = Path.Combine(newPath + banner.imageFiles.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await banner.imageFiles.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(banner.imageFiles.FileName, (("BNNR0") + banner.BannerId), fullPath, credential, "top-banner", aliyunfolder);
                    if (endResult.Equals("true"))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }
                }
                TempData["ViewMessage"] = "Banner Created Successfully";
                return RedirectToAction("Manage", "Banner");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            id = protector.Encode(id);
            Task<List<Banner>> getMainCattegorylist = _bannerSI.GetMancategoreylist();
            Task<Banner> getbannerdetails = _bannerSI.GetBannerDetails(id);
            Task<List<Hub>> getHublist = _employeeSI.GetHublist();
            var VM = new BannerVM
            {
                GetMainCategoryList = getMainCattegorylist.Result,
                GetBanner = getbannerdetails.Result,
                GetHubList = getHublist.Result
            };
            VM.GetAcctiontriggerlist = await _bannerSI.GetActionTrigger(VM.GetBanner.ActionTrigger);
            if (TempData["ViewMessage"] != null)
                VM.ViewMessage = TempData["ViewMessage"] as string;

            if (TempData["ErrorMessage"] != null)
                VM.ErrorMessage = TempData["ErrorMessage"] as string;
            VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = VM.businessInfo.hotel_name;
            ViewBag.logoUrl = VM.businessInfo.logo_url;
            return View("Details", VM);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(Banner banner, AliyunCredential credential)
        {
            try
            {
                //if (hubId == null)
                //{
                //    hubId = "HID01";
                //}
                //banner.Branch = hubId;
                var aliyunfolder = banner.Aliyunkey;
                banner.UpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _bannerSI.UpdateBanner(banner);
                banner.BannerId =  Convert.ToString(banner.Id);
                if (banner.imageFiles != null && result > 0)
                {
                    string fullPath = "";
                    string folderName = "Documents\\";
                    var rootFolder = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(rootFolder, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    fullPath = Path.Combine(newPath + banner.imageFiles.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await banner.imageFiles.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(banner.imageFiles.FileName,(("BNNR0")+banner.BannerId), fullPath, credential, "top-banner",aliyunfolder);
                    if (endResult.Equals("true"))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }
                }
                TempData["ViewMessage"] = "Banner Updated Successfully";
                return RedirectToAction("Manage", "Banner");
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
                Task<List<Banner>> getBannerList = _bannerSI.GetbannerList(hubId);
                await Task.WhenAll(getBannerList);
                var VM = new BannerVM
                {
                    GetBannerList = getBannerList.Result,
                };
                var list = VM.GetBannerList.Select(p => new
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
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _bannerSI.DeleteBanner(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }

        // on change event trigger fired
        [HttpGet]
        public async Task<JsonResult> GetActionTriggerlist(string trigger)
        {
            try
            {
                return Json(await _bannerSI.GetActionTrigger(trigger));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult NitkhaCreate()
        {
            return View();
        }
    }
}