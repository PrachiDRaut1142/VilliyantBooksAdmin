using DemoDecodeURLParameters.Security;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Offer;
using Freshlo.DomainEntities.PriceList;
using Freshlo.SI;
using Freshlo.Web.Extensions;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models;
using Freshlo.Web.Models.LiveOfferVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Controllers
{
    public class OfferController : Controller
    {
        private IOfferlist _offerlist;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IPricelistSI _pricelistSI;
        public ISettingSI _settingSI { get; set; }
        private IStockSI _stockSI;
        private readonly CustomIDataProtection protector;
        public string hubId { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OfferController(IOfferlist offerlist, IStockSI stockSI, CustomIDataProtection customIDataProtection, IHostingEnvironment hostingEnvironment, IPricelistSI pricelist, ISettingSI settingSI, IHttpContextAccessor httpContextAccessor)
        {
            _stockSI = stockSI;
            _offerlist = offerlist;
            _hostingEnvironment = hostingEnvironment;
            _pricelistSI = pricelist;
            _settingSI = settingSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
            protector = customIDataProtection;
        }
        public IActionResult Index()
        {
            return View();
        }        
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
            //Task<List<PriceList>> getoffer = _offerlist.Getallitemlist(hubId);
            Task<List<SelectListItem>> getMainCattegorylist = _stockSI.GetMainCategoryList(hubId);
            Task<List<SelectListItem>> getOfferTypeList = _offerlist.GetOfferTypeList();

            await Task.WhenAll(getMainCattegorylist);
            var vm = new LiveOfferVM
            {                
                //getoffer = getoffer.Result,
                GetMainCategoryList1 = getMainCattegorylist.Result,
                GetOfferTypeList = getOfferTypeList.Result,
            };
           // LiveOfferVM vm = new LiveOfferVM();
            if (TempData["ViewMessage"] != null)
            {
                vm.ViewMessage = TempData["ViewMessage"] as string;
            }
            else if (TempData["ErrorMessage"] != null)
            {
                vm.ErrorMessage = TempData["ErrorMessage"] as string;
            }
            vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = vm.businessInfo.hotel_name;
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return View(vm);
        }
        // create offer 
        [HttpPost]
        public async Task<IActionResult> Create(Offer info, AliyunCredential credential)
        
        {
            try
            {
                var hubId = Convert.ToString(User.FindFirst("branch").Value);
                info.Hub = hubId;
                if (info.chooseOffer == "Bogo Offer") { 
                    if (info.FreeType != null)
                    {
                        var split = info.FreeType.Split(' ');
                        info.BuyQuantity = split[1];
                        info.GetQuantity = split[3];
                    }
                }
                //var aliyunfolder = info.Aliyunkey;
                var aliyunfolder = "HurTex";
                info.ItemId = info.items.Split(',');
                //info.ItemId = itemArray;
                if (info.GetItemId != null)
                {
                    info.GetItemIdss = info.GetItemId.Split(',');
                }
                if (info.OffStartdate != null && info.OffEndtdate != null)
                {
                    info.OfferStartDate = DateTime.ParseExact(info.OffStartdate, "dd/MM/yyyy - hh:mm tt", CultureInfo.InvariantCulture);
                    info.OfferEndDate = DateTime.ParseExact(info.OffEndtdate, "dd/MM/yyyy - hh:mm tt", CultureInfo.InvariantCulture);
                }
                else
                {
                   var date = "01/01/1900 - 12:00 PM";
                    info.OfferStartDate = DateTime.ParseExact(date, "dd/MM/yyyy - hh:mm tt", CultureInfo.InvariantCulture);
                    info.OfferEndDate = DateTime.ParseExact(date, "dd/MM/yyyy - hh:mm tt", CultureInfo.InvariantCulture);
                }

                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var addOffer = await _offerlist.AddOffer(info);
                info.OfferId = addOffer;
                if (info.imageFiles != null && addOffer != null)
                {
                    string fullPath = "";
                    string folderName = "Documents\\";
                    var rootFolder = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(rootFolder, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    fullPath = Path.Combine(newPath + info.imageFiles.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await info.imageFiles.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(info.imageFiles.FileName, info.OfferId, fullPath, credential, "Offer", aliyunfolder);
                    if (endResult.Equals("true"))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Image not saved";
                    return RedirectToAction("Manage");
                }
                TempData["ViewMessage"] = "Offer Created Successfully";
                return RedirectToAction("Manage");            
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        // offer list
        public async Task<IActionResult> Manage()
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<Offer>> getLiveOfferList = _offerlist.GetOfferlist(hubId);
                await Task.WhenAll(getLiveOfferList);
                var VM = new LiveOfferVM
                {
                    getLiveOfferList = getLiveOfferList.Result,
                };
                var list = VM.getLiveOfferList.Select(p => new
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
        // get offer
        [HttpGet]
        public async Task<IActionResult> Detail(string Id, PricelistFilter detail)
        {
            try
            {
                Id = protector.Encode(Id);
                Task<Offer> offersDetail= _offerlist.GetOfferDetail(Id);
                Task<List<PriceList>> getAllpricelist = _offerlist.GetallPricelist(detail);
                Task<List<PriceList>> getMappedPricelist = _offerlist.GetMappedPricelist(detail,Id);
                Task<List<PricelistCategory>> getMainCattegorylist = _pricelistSI.GetMainCategoryList();
                Task<List<PricelistCategory>> getCattegorylist = _pricelistSI.GetCategoryListext();
                Task<List<PricelistCategory>> getbrandList = _pricelistSI.GetBrandList();
                Task<List<PricelistCategory>> getVendorList = _pricelistSI.GetVendorList();
                await Task.WhenAll(offersDetail, getAllpricelist, getMappedPricelist, getMainCattegorylist, getCattegorylist, getbrandList, getVendorList);
                var VM = new LiveOfferVM
                {
                    OffersDetail = offersDetail.Result,
                    getAllpricelist = getAllpricelist.Result,
                    getMappedPricelist = getMappedPricelist.Result,
                    GetMainCategoryList = getMainCattegorylist.Result,
                    getCattegorylist = getCattegorylist.Result,
                    getBrandList = getbrandList.Result,
                    getVendorList = getVendorList.Result
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
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        // update offer
        [HttpPost]
        public async Task<IActionResult> Detail(Offer info)
        {
            try
            {
                //if (hubId == null)
                //{
                //    hubId = "HID01";
                //}
                //info.Hub = hubId;
                //string OffStartdate = info.OffStartdate;
                //string OffEndtdate = info.OffEndtdate;
                //var Format = "DD/MM/YYYY - HH:mm tt";
                //CultureInfo provider = CultureInfo.InvariantCulture;
                //info.OfferStartDate = DateTime.ParseExact(OffStartdate, Format, provider);
                //info.OfferEndDate = DateTime.ParseExact(OffEndtdate, Format, provider);
                string date = info.OffStartdate.Split(' ')[0].ToString();
                info.offerDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                info.OfferStartDate = DateTime.ParseExact(info.OffStartdate, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture);
                info.OfferEndDate = DateTime.ParseExact(info.OffEndtdate, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture);
                info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var addOffer = await _offerlist.EditOffer(info);
                TempData["ViewMessage"] = "Offer Updated Successfully";
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        // Add Item List 
        public async Task<PartialViewResult> _AddItemOfferModel(string MainCatName, string CatName, int type,  int selectedItem ,string vfs)
        {
            try
            {
                var hubId = Convert.ToString(User.FindFirst("branch").Value);
                Task<List<PriceList>> getoffer = _offerlist.Getallitemlist(MainCatName, CatName, type, hubId);
                //List<string> itemList = vfs.Split(',').ToList();
                List<string> itemList = vfs.Split(',').ToList();
                //item.items = itemList;
                await Task.WhenAll(getoffer);
                var vm = new LiveOfferVM
                {
                    getoffer = getoffer.Result,
                    items = itemList,
                    selectedItem = selectedItem,
                };
                return PartialView(vm);
            }
            catch (Exception ex)
            {
                return default(PartialViewResult);
            }
        }
        public async Task<PartialViewResult> _GetItemOfferModel(string MainCatName, string CatName, int type,  int selectedItem ,string vfs)
        {
            try
            {
                var hubId = Convert.ToString(User.FindFirst("branch").Value);
                Task<List<PriceList>> getoffer = _offerlist.Getallitemlist(MainCatName, CatName, type, hubId);
                List<string> itemList = vfs.Split(',').ToList();
                await Task.WhenAll(getoffer);
                var vm = new LiveOfferVM
                {
                    getoffer = getoffer.Result,
                    items = itemList,
                    selectedItem = selectedItem,
                };
                return PartialView(vm);
            }
            catch (Exception ex)
            {
                return default(PartialViewResult);
            }
        }
        // Available Item List 
        public async Task<JsonResult> _GetAllPricelist(PricelistFilter details)
        {
            try
            {
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Successs", Data = await this.RenderPartialViewAsync<List<PriceList>>("_GetAllPricelist", await _offerlist.GetallPricelist(details)) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Error", Data = null });
            }
        }
        // Mapped Item List with Offer
        public async Task<JsonResult> _GetOfferMapPricelist(PricelistFilter detail, string id)
        {
            try
            {
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Successs", Data = await this.RenderPartialViewAsync<List<PriceList>>("_GetOfferMapPricelist", await _offerlist.GetMappedPricelist(detail,id)) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Error", Data = null });
            }
        }
        // delete offer mapped item single
        public async Task<JsonResult> DeleteOfferItem(int Id)
        {
            try
            {
                var removeItem = _offerlist.DeleteOfferItem(Id);
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "Success", Data = await removeItem });
            }
            catch (Exception ex)
            {
                return Json(new Message<int>() { IsSuccess = false, ReturnMessage = "Error", Data = -1 });
            }
        }
        // add offer mapped item by single 
        public async Task<JsonResult> AddToItem(PriceList info, string OfferId, string startdate)
        {
            try
            {
                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(new Message<int>
                {
                    IsSuccess = true,
                    ReturnMessage = "success",
                    Data = await _offerlist.AddItem(info, OfferId, startdate)
                });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        // add offer mapped item by mulitple 
        public async Task<JsonResult> AddToItemList(Offer list)
        {
            try
            {
                list.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(new Message<int>
                {
                    IsSuccess = true,
                    ReturnMessage = "success",
                    Data = await _offerlist.AddItemList(list)
                });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        public async Task<JsonResult> InsertItemList(PriceList info, string OfferId)
        {
            try
            {
                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(new Message<int>
                {
                    IsSuccess = true,
                    ReturnMessage = "success",
                    Data = await _offerlist.AddItemList(info, OfferId)
                });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        // delete multiple mapped item 
        public async Task<JsonResult> DeleteMappingItem(string ids)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _offerlist.DeleteMappingItem(ids) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }
        // delete  offer
        [HttpGet]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _offerlist.DeleteOffer(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }
    }
}