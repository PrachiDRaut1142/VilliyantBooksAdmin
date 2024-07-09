using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.RI;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models;
using Freshlo.Web.Models.ItemMaster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace Freshlo.Web.Controllers
{
    public class ItemMasterController : Controller
    {
        private IItemSI _itemSI;
        private ISettingSI _settingSI;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IDbConfig _dbConfig { get; }
        private IConfiguration _Config { get; }
        public string hubId { get; set; }
        public string language { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ItemMasterController(IItemSI itemSI, IHostingEnvironment hostingEnvironment, IDbConfig dbConfig, IConfiguration config, ISettingSI settingSI, IHttpContextAccessor httpContextAccessor)
        {
            _itemSI = itemSI;
            _hostingEnvironment = hostingEnvironment;
            _dbConfig = dbConfig;
            _Config = config;
            _settingSI = settingSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
            language = new CookieHelper(_httpContextAccessor).GetCookiesValue("LanguageName");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Manage()
        {
            try
            {
                Task<List<SelectListItem>> getMainCattegorylist = _itemSI.GetMainCategoryList(hubId);
                Task<List<ItemCategory>> getCattegorylist = _itemSI.GetCategoriesAsync(hubId);
                Task<List<SelectListItem>> supplierNameList = _itemSI.GetSupplierNameList();
                Task<List<SelectListItem>> getsubCategoryList = _itemSI.GetSubCategoryList(hubId);
                Task<ItemMasters> Getitemcount = _itemSI.GetItemCountDetail();
                await Task.WhenAll(getMainCattegorylist, getCattegorylist, supplierNameList, getsubCategoryList, Getitemcount);
                var vm = new ItemMasterVM
                {
                    GetMainCategoryList = getMainCattegorylist.Result,
                    getCattegorylist = getCattegorylist.Result,
                    SupplierNameList = supplierNameList.Result,
                    GetSubCategory = getsubCategoryList.Result,
                    Getitemcount = Getitemcount.Result,
                };
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
        public async Task<PartialViewResult> _Manage(ItemMasters item)
        {
            try
            {
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.GetItemManageData(item);
                await Task.WhenAll(getItemmasterlist);
                var vm = new ItemMasterVM
                {
                    getItemMasterList = getItemmasterlist.Result,
                };
                vm.getccurrencylist = _settingSI.GetCurrencyList(hubId);
                ViewBag.hubId = hubId;
                if (hubId == "HID01")
                {
                    return PartialView("_Manage", vm);
                }
                else
                {
                    return PartialView("_Manage", vm);
                }
            }
            catch (Exception ex)
            {
                return PartialView("_Manage");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create([FromServices] DropDownRI _DropDownRI, AliyunCredential credential)
        {
            try
            {
                Task<List<SelectListItem>> getMainCattegorylist = _itemSI.GetMainCategoryList(hubId);
                List<SelectListItem> getSegmentList = _DropDownRI.Segment();
                List<SelectListItem> measurement = _DropDownRI.Measurement();
                Task<List<SelectListItem>> foodType = _itemSI.GetfoodTypeList();
                List<SelectListItem> offertype = _DropDownRI.offerType();
                Task<List<SelectListItem>> supplierNameList = _itemSI.GetSupplierNameList();
                Task<List<SelectListItem>> brandList = _itemSI.GetBrandList();
                Task<List<TaxPercentageMst>> taxList = _itemSI.GetTaxPercentageList();
                TaxationInfo taxInfo = _settingSI.GetTaxationInfo();
                await Task.WhenAll(getMainCattegorylist, supplierNameList, brandList, taxList);
                var vm = new ItemMasterVM
                {
                    GetMainCategoryList = getMainCattegorylist.Result,
                    GetSegmentList = getSegmentList,
                    GetMeasurementList = measurement,
                    offerType = offertype,
                    SupplierNameList = supplierNameList.Result,
                    BrandList = brandList.Result,
                    taxPercentageList = taxList.Result,
                    foodTypeList = foodType.Result,
                    taxInfoDetails = taxInfo,
                };
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

        [HttpPost]
        public void Upload(List<IFormFile> Files)
        {
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(fileName);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemMasters itemMasters, AliyunCredential credential, ItemMasters Item, List<IFormFile> files)
        {
            try
            {
                var aliyunfolder = itemMasters.Aliyunkey;
                itemMasters.Branch = Convert.ToString(User.FindFirst("branch").Value);
                itemMasters.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var itemMaster = await _itemSI.CreateItemMaster(itemMasters);
                var ItemId = itemMaster.Substring(4);
                var IId = Convert.ToInt32(ItemId);
                itemMasters.ItemId = Convert.ToString(itemMaster);
                var stockMaster = await _itemSI.CreateStockMaster(itemMasters);
                var wastageMaster = await _itemSI.CreateWastageMaster(itemMasters);
                if (itemMasters.ImagePath != null && itemMasters.ItemId != null)
                {
                    string fullPath = "";
                    var newfilepath = FilePath();
                    fullPath = Path.Combine(newfilepath + itemMasters.ImagePath.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await itemMasters.ImagePath.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(itemMasters.ImagePath.FileName, itemMasters.ItemId + (("_") + "0"), fullPath, credential, "Product-image", aliyunfolder);
                    if (endResult.Equals("true"))
                    {
                        _itemSI.uploadimagecreateiteam(itemMasters.ItemId, "update");
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }
                }
                if (files != null && itemMasters.ItemId != null)
                {
                    foreach (var img in files)
                    {
                        string fullPath = "";
                        var newfilepath = FilePath();
                        var endResult1 = "false";
                        itemMasters.ImagesCount = itemMasters.ImagesCount + 1;
                        fullPath = Path.Combine(newfilepath + img.FileName);
                        var filepath = Path.GetTempFileName();
                        List<string> image = BlAliyun.GetObjectFromFile(itemMasters.ItemId, credential);
                        List<string> iconImage = BlAliyun.GetObjectIcon(itemMasters.ItemId, credential);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await img.CopyToAsync(stream);
                        }
                        if (image.Count == 0 && iconImage.Count == 0)
                        {
                            endResult1 = BlAliyun.PutIconObjectFromFile(img.FileName, itemMasters.ItemId + ("_1"), fullPath, credential, "Product-image", aliyunfolder);
                        }
                        else
                        {
                            //itemMasters.ImagesCount = itemMasters.ImagesCount + 1;
                            endResult1 = BlAliyun.PutIconObjectFromFile(img.FileName, itemMasters.ItemId + (("_") + itemMasters.ImagesCount), fullPath, credential, "Product-image", aliyunfolder);
                        }
                        if (endResult1.Equals("true"))
                        {
                            _itemSI.uploadimagecreateiteam(itemMasters.ItemId, "update");
                            System.IO.File.Delete(fullPath);
                        }
                        else
                        {
                            TempData["Image"] = "Error while uploading Image.....";
                        }
                    }
                }
                return RedirectToAction("Detail", "ItemMaster", new { Id = IId });
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Detail(int Id, [FromServices] DropDownRI _DropDownRI, AliyunCredential credential, [FromServices] IPurchaseSI _purchaseSI, ItemMasters item)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<SelectListItem>> getMainCattegorylist = _itemSI.GetMainCategoryList(hubId);
                List<SelectListItem> getSegmentList = _DropDownRI.Segment();
                List<SelectListItem> measurement = _DropDownRI.Measurement();
                Task<List<SelectListItem>> foodTypeList = _itemSI.GetfoodTypeList();
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.GetItemManageData(item);
                List<SelectListItem> offertype = _DropDownRI.offerType();
                Task<List<SelectListItem>> supplierNameList = _itemSI.GetSupplierNameList();
                Task<List<SelectListItem>> brandList = _itemSI.GetBrandList();
                Task<ItemMasters> itemMastersDetail = _itemSI.GetItemMasterDetail(Id);
                var ItemId = itemMastersDetail.Result.ItemId;
                var main = itemMastersDetail.Result.MainCategory;
                Task<List<ItemCategory>> getCattegorylist = _itemSI.GetCategoriesAsync(hubId);
                Task<List<SelectListItem>> getSubfoods = _itemSI.GetsubFood();
                Task<List<SelectListItem>> getsubCategoryList = _itemSI.GetSubCategoryList(hubId);
                Task<List<ItemMasters>> getlanguagelist = _settingSI.selectlanguage(hubId);
                Task<List<ItemMasters>> getselectlanguagelist = _settingSI.languageselect(hubId);
                List<Item> itemList = await _purchaseSI.GetItemList(null);
                Task<List<ItemMasters>> getitemnamechage = _itemSI.GetItemLanguage(item, hubId, Id);
             
                Task<List<TaxPercentageMst>> taxList = _itemSI.GetTaxPercentageList();
                Task<List<ItemMasters>> getItemVariantDetails = _itemSI.ProductSizeInfoDetails(ItemId, hubId);
                Task<List<ItemMasters>> getItemColorDetails = _itemSI.ProductColorInfoDetails(ItemId);
                List<ColorSizeMapping> getSizeColorDetails = _itemSI.GetColorSizeMap(ItemId);
                Task<List<int>> getMappingDay = _itemSI.GetMappingDaywithItem(itemMastersDetail.Result.ItemId);
                Task<List<ProductPriceLog>> getProductPriceLog = _itemSI.GetProductPriceLog(ItemId, hubId);
                TaxationInfo taxInfo = _settingSI.GetTaxationInfo();
                await Task.WhenAll(getMainCattegorylist, supplierNameList, getselectlanguagelist, brandList, itemMastersDetail, getitemnamechage, getItemmasterlist, getlanguagelist, getCattegorylist, getsubCategoryList, taxList);
                var IconImages = "https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/Product-image/" + itemMastersDetail.Result.ItemId + ".png";
                var endResult1 = BlAliyun.GetNewObjectFromFile(itemMastersDetail.Result.ItemId, credential, itemMastersDetail.Result.ImagesCount);
                var galleryImages = "https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/Product-image/" + itemMastersDetail.Result.ItemId + ".png";
                List<CurrencyMST> getCurrencyList = _settingSI.GetCurrencyList(hubId);
                Task<List<ProductType>> getproductList = _itemSI.GetProductList(hubId);
                Task<List<ProductVariance>> getProductVarianceList = _itemSI.GetProductVarianceById("PCID01", hubId);
                Task<List<ItemMasters>> getMappedVarientItemList = _itemSI.GetMappedVarientItemList(ItemId, hubId, main);
                var vm = new ItemMasterVM
                {
                    GetProductPriceLog = getProductPriceLog.Result,
                    GetMainCategoryList = getMainCattegorylist.Result,
                    GetSegmentList = getSegmentList,
                    GetMeasurementList = measurement,
                    offerType = offertype,
                    SupplierNameList = supplierNameList.Result,
                    BrandList = brandList.Result,
                    ItemMastersDetail = itemMastersDetail.Result,
                    getCattegorylist = getCattegorylist.Result,
                    getsubFoodlist = getSubfoods.Result,
                    GetSubCategory = getsubCategoryList.Result,
                    GetGalleryImage = endResult1,
                    ItemList = itemList,
                    taxPercentageList = taxList.Result,
                    branch = Convert.ToString(User.FindFirst("branch").Value),
                    foodTypeList = foodTypeList.Result,
                    GetDayList = getMappingDay.Result,
                    ItemSizeInfo = getItemVariantDetails.Result,
                    ItemColorInfo = getItemColorDetails.Result,
                    MapInfo = getSizeColorDetails,
                    taxInfoDetails = taxInfo,
                    getccurrencylist = getCurrencyList,
                    getitemnamechage = getitemnamechage.Result,
                    getItemMasterList = getItemmasterlist.Result,
                    getlanguagelist = getlanguagelist.Result,
                    getaddedlanguagelist = getselectlanguagelist.Result,
                    getproductList = getproductList.Result,
                    getProductVarianceList = getProductVarianceList.Result,
                    getMappedVarientItemList = getMappedVarientItemList.Result
                };
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                ViewBag.Id = Id;
                ViewBag.hubId = hubId;
                ViewBag.ItemSizecount = vm.ItemSizeInfo.Count;
                ViewBag.VBMeasuredIn = vm.ItemMastersDetail.MeasuredIn;
                if (language != null)
                {
                    ViewBag.language = language;
                }
                else
                {
                    ViewBag.language = "No Set";
                }
                if (TempData["ViewMessage"] != null)
                    vm.ViewMessage = TempData["ViewMessage"] as string;

                if (TempData["ErrorMessage"] != null)
                    vm.ErrorMessage = TempData["ErrorMessage"] as string;
                return View(vm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }





        private List<String> GetObjecLinks(string itemId, int imagesCount)
        {
            List<string> images = new List<string>();
            var imageName = new List<string>();
            if (imagesCount > 0)
            {
                for (int i = 1; i < imagesCount; i++)
                {
                    images.Add("HurTex/Product-image/" + itemId + "_" + i + ".png");
                }
            }
            //else
            //{
            //   images.Add("HurTex/Product-image/" + itemId + "_" + 0 + ".png");
            //}
            return images;
        }

        [HttpPost]
        public async Task<IActionResult> Detail(ItemMasters Item, IFormFile[] files, AliyunCredential credential)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                var aliyunfolder = Item.Aliyunkey;
                if (Item.FoodType == "2")
                {
                    if (Item.FoodSubType == null)
                    {
                        Item.FoodSubType = "2";
                    }
                }
                var endResult = "true";
                var empId = Convert.ToString(User.FindFirst("empId").Value);
                Item.LastUpdatedBy = empId;
                Item.Hub = hubId;
                var ImagesCount = BlAliyun.GetLastObjectFromFile(Item.ItemId, credential, Item.ImagesCount);
                string result = await _itemSI.UpdateItemMaster(Item);
                //await _itemSI.UpdatePriceMaster(Item);
                if (files != null && Item.ItemId != null)
                {
                    foreach (var img in files)
                    {
                        string fullPath = "";
                        var newfilepath = FilePath();
                        fullPath = Path.Combine(newfilepath + img.FileName);
                        var filepath = Path.GetTempFileName();
                        List<string> image = BlAliyun.GetObjectFromFile(Item.ItemId, credential);
                        List<string> iconImage = BlAliyun.GetObjectIcon(Item.ItemId, credential);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await img.CopyToAsync(stream);
                        }
                        if (image.Count == 0 && iconImage.Count == 0)
                            endResult = BlAliyun.PutIconObjectFromFile(img.FileName, Item.ItemId + (("_") + "0"), fullPath, credential, "Product-image", aliyunfolder);
                        else
                            endResult = BlAliyun.PutIconObjectFromFile(img.FileName, Item.ItemId + (("_") + Item.ImagesCount), fullPath, credential, "Product-image", aliyunfolder);
                        if (endResult.Equals("true"))
                        {
                            _itemSI.uploadimagecreateiteam(Item.ItemId, "update");
                            System.IO.File.Delete(fullPath);
                        }
                        else
                        {
                            TempData["Image"] = "Error while uploading Image.....";
                        }
                    }
                }
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<JsonResult> GetCategorylist(string mainCatId, string id)
        {
            return Json(await _itemSI.GetCategoryNamebyMainCate(mainCatId, hubId));
        }
        public async Task<JsonResult> GetSubCategoryList(string CategoryId, string id)
        {
            return Json(await _itemSI.GetSubCategoryListbyCat(CategoryId, hubId));
        }
        public async Task<PartialViewResult> _ItemList(ItemMasters item)
        {
            try
            {
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.GetItemManageData(item);
                await Task.WhenAll(getItemmasterlist);
                var vm = new ItemMasterVM
                {
                    getItemMasterList = getItemmasterlist.Result
                };
                return PartialView("_ItemList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_ItemList");
            }
        }
        public async Task<JsonResult> FeaturedUpdate(string feature, string itemId, int type, int Id)
        {
            try
            {
                int result = await _itemSI.UpdateItemFeatured(feature, itemId, type, Id);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        public async Task<JsonResult> CheckSpUpadte(string checkSp, string itemId, int type, int Id)
        {
            try
            {
                int result = await _itemSI.UpdateItemCheckSp(checkSp, itemId, type, Id);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        public async Task<JsonResult> ApprovetheItem(string itemId, int Id, string approval, int type)
        {
            try
            {
                var empId = Convert.ToString(User.FindFirst("empId").Value);
                int result = await _itemSI.UpdateItemApproval(itemId, Id, approval, empId, type);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        public async Task<JsonResult> GetDowload(AliyunCredential credential, string name)
        {
            name = "HurTex/" + name;
            var itemId = name.Split('/');
            var deafultItemId = itemId[2].Split("_")[0] + "_0";
            var iconresult = "false";
            var temp = BlAliyun.GetDowloadObject(credential, name, itemId[2]);
            try
            {
                if (temp.Count > 1)
                {
                    var outcome = PutGalleryimage(temp[1], credential, itemId[2], itemId[2]);
                    if (outcome == "true")
                    {
                        var fullpath = "";
                        var newfilepath = FilePath();
                        fullpath = Path.Combine(newfilepath + itemId[2]);
                        System.IO.File.WriteAllBytes(fullpath, temp[0]);
                        iconresult = BlAliyun.PutIconObjectFromFile("", deafultItemId, fullpath, credential, "Product-image", "HurTex");
                    }
                }
                else
                {
                    var fullpath = "";
                    var newfilepath = FilePath();
                    fullpath = Path.Combine(newfilepath + itemId[3]);
                    System.IO.File.WriteAllBytes(fullpath, temp[0]);
                    iconresult = BlAliyun.PutIconObjectFromFile("", itemId[2], fullpath, credential, "Product-image", "HurTex");
                    BlAliyun.DeleteObjectFromFile("HurTex/Product-image/" + itemId[2] + "/" + itemId[3] + "", credential);
                }
            }
            catch (Exception e)
            {
                return Json("false");
            }
            return Json(iconresult);
        }
        public string PutGalleryimage(byte[] temp1, AliyunCredential credential, string name, string itemId)
        {
            try
            {
                string fullPath = "";
                var newfilepath = FilePath();
                fullPath = Path.Combine(newfilepath + name);
                System.IO.File.WriteAllBytes(fullPath, temp1);
                var result2 = BlAliyun.PutGallaryObjectFromFile(name, itemId, fullPath, credential, "Product-image/" + itemId);
                return result2;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string FilePath()
        {
            string folderName = "Documents\\";
            var rootFolder = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(rootFolder, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            return newPath;
        }
        //public JsonResult DeleteImageSrc(string key, AliyunCredential credential)
        //{
        //    return Json(BlAliyun.DeleteObjectFromFile(key, credential));
        //}

        [HttpPost]
        public JsonResult DeleteImageSrc(string key, AliyunCredential credential)
        {
            var result = BlAliyun.DeleteObjectFromFile(key, credential);
            if (result == true)
            {
                _itemSI.uploadimagecreateiteam(key.Split("/")[1].Split("_")[0], "delete");
            }
            return Json(true);
        }
        public async Task<JsonResult> CheckUniquePlucode(string PluCode)
        {
            try
            {
                var empId = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _itemSI.CheckUniquePlucode(PluCode);
                return Json(new Message<string> { IsSuccess = true, ReturnMessage = "success", Data = Convert.ToString(result) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Manage(ItemFields list, string updatedBy)
        {
            try
            {
                updatedBy = Convert.ToString(User.FindFirst("empId").Value);
                _itemSI.UpdateItemFields(list, updatedBy);
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<JsonResult> CoupenUpdate(string coupen, string itemId, int type, int Id)
        {
            try
            {
                int result = await _itemSI.UpdateItemCoupen(coupen, itemId, type, Id);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        public async Task<JsonResult> SeasonUpdate(string season, string itemId, int type, int Id)
        {
            try
            {
                int result = await _itemSI.UpdateItemAvailability(season, itemId, type, Id);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        [HttpPost]
        public async Task<IActionResult> updateItemDetail(Item item)
        {
            try
            {
                item.HubId = Convert.ToString(User.FindFirst("branch").Value);
                return View(await _itemSI.UpdateHubPrice(item));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ManagePrice()
        {
            try
            {
                Task<List<SelectListItem>> getMainCattegorylist = _itemSI.GetMainCategoryList(hubId);
                Task<List<ItemCategory>> getCattegorylist = _itemSI.GetCategoriesAsync(hubId);
                Task<List<SelectListItem>> supplierNameList = _itemSI.GetSupplierNameList();
                Task<List<SelectListItem>> getsubCategoryList = _itemSI.GetSubCategoryList(hubId);
                Task<List<SelectListItem>> getHublist = _settingSI.GetHubList();
                Task<ItemMasters> Getitemcount = _itemSI.GetItemCountDetail();
                await Task.WhenAll(getMainCattegorylist, getCattegorylist, supplierNameList, getsubCategoryList, Getitemcount);
                var vm = new ItemMasterVM
                {
                    GetMainCategoryList = getMainCattegorylist.Result,
                    getCattegorylist = getCattegorylist.Result,
                    SupplierNameList = supplierNameList.Result,
                    GetSubCategory = getsubCategoryList.Result,
                    Getitemcount = Getitemcount.Result,
                    gethubLiST = getHublist.Result,
                };
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
        public async Task<PartialViewResult> _MainList(ItemMasters item)
        {
            try
            {
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.GetItemManagePriceData(item);
                await Task.WhenAll(getItemmasterlist);
                var vm = new ItemMasterVM
                {
                    getItemMasterList = getItemmasterlist.Result,
                };
                return PartialView("_MainList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_MainList");
            }

        }
        public async Task<PartialViewResult> _includeItemList(ItemMasters item, int itemId)
        {
            try
            {
                item.Branch = Convert.ToString(User.FindFirst("branch").Value);
                item.Hub = hubId;
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.GetIncludedHubPrice(hubId);
                await Task.WhenAll(getItemmasterlist);
                var vm = new ItemMasterVM
                {
                    getItemMasterList = getItemmasterlist.Result,
                };
                return PartialView("_includeItemList", vm);
            }
            catch (Exception e){}
            return PartialView();
        }
        public async Task<PartialViewResult> _excludeItemList(ItemMasters item, int itemId)
        {
            try
            {
                item.Branch = Convert.ToString(User.FindFirst("branch").Value);
                item.Hub = hubId;
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.GetExcludedHubPrice(hubId);
                await Task.WhenAll(getItemmasterlist);
                var vm = new ItemMasterVM
                {
                    getItemMasterList = getItemmasterlist.Result,
                };
                return PartialView("_excludeItemList", vm);
            }
            catch (Exception e) { }
            return PartialView();
        }
        public async Task<JsonResult> GetItemById(int itemId, ItemMasters item)
        {
            try
            {
                item.Branch = Convert.ToString(User.FindFirst("branch").Value);
                item.Hub = hubId;
                //Task<List<ItemMasters>> getItemmasterlist =  _itemSI.GetExcludedHubPrice(itemId,item);
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.GetExcludedHubPrice(hubId);
                return Json(new Message<Task<List<ItemMasters>>> { IsSuccess = true, ReturnMessage = "success", Data = getItemmasterlist });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        public async Task<JsonResult> _updateHubPrice(Item item)
        {
            try
            {
                var salesvm = new SalesVM();
                //return View(await _itemSI.UpdateHubPrice(item));
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "Success", Data = await _itemSI.UpdateHubPrice(item) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });
            }
        }
        public async Task<JsonResult> DeleteHubItem(string itemId, ItemMasters item)
        {
            try
            {
                item.hubId = Convert.ToString(User.FindFirst("branch").Value);
                return Json(new Message<Task<int>> { IsSuccess = true, ReturnMessage = "success", Data = _itemSI.DeleteHubItem(itemId) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        public async Task<JsonResult> DeleteItem(ItemMasters info)
        {
            try
            {
                info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                int result = await _itemSI.DeleteItem(info);
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "Success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "Success", Data = -1 });
            }
        }
        public async Task<FileContentResult> GetFilterExcel(ItemMasters item)
        {
            string branch = Convert.ToString(User.FindFirst("branch").Value);
            string role = Convert.ToString(User.FindFirst("userRole").Value);
            item.webRootPath = _hostingEnvironment.WebRootPath;
            return File(await _itemSI.FilterExcelofItem(item),
                "application/ms-excel", "SalesOverview.xlsx");
        }
        [HttpPost]
        public IActionResult Index(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                //Create a Folder.
                string path = Path.Combine(this._hostingEnvironment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                //Read the connection string for the Excel file.
                string conString = this._Config.GetConnectionString("ExcelConString");
                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);
                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;
                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();
                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }
                //Insert the Data read from the Excel file to Database Table.
                conString = this._Config.GetConnectionString("DefaultConnection");
                try
                {
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            sqlBulkCopy.DestinationTableName = "dbo.ItemsMaster";

                            //[OPTIONAL]: Map the Excel columns with that of the database table.
                            //sqlBulkCopy.ColumnMappings.Add("Id", "Id");
                            //sqlBulkCopy.ColumnMappings.Add("ItemId", "ItemId");
                            sqlBulkCopy.ColumnMappings.Add("PluName", "PluName");
                            sqlBulkCopy.ColumnMappings.Add("PluCode", "PluCode");
                            sqlBulkCopy.ColumnMappings.Add("Category", "Category");
                            sqlBulkCopy.ColumnMappings.Add("SubCategory", "SubCategory");
                            sqlBulkCopy.ColumnMappings.Add("Measurement", "Measurement");
                            sqlBulkCopy.ColumnMappings.Add("Size", "Size");
                            sqlBulkCopy.ColumnMappings.Add("Weight", "Weight");
                            sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                            sqlBulkCopy.ColumnMappings.Add("HSN_Code", "HSN_Code");
                            sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                            sqlBulkCopy.ColumnMappings.Add("CreatedOn", "CreatedOn");
                            sqlBulkCopy.ColumnMappings.Add("LastUpdatedBy", "LastUpdatedBy");
                            sqlBulkCopy.ColumnMappings.Add("LastUpdatedOn", "LastUpdatedOn");
                            sqlBulkCopy.ColumnMappings.Add("Purchaseprice", "Purchaseprice");
                            sqlBulkCopy.ColumnMappings.Add("Wastage", "Wastage");
                            sqlBulkCopy.ColumnMappings.Add("ProfitMargin", "ProfitMargin");
                            sqlBulkCopy.ColumnMappings.Add("SellingPrice", "SellingPrice");
                            sqlBulkCopy.ColumnMappings.Add("MarketPrice", "MarketPrice");
                            sqlBulkCopy.ColumnMappings.Add("Title", "Title");
                            sqlBulkCopy.ColumnMappings.Add("ImagePath", "ImagePath");
                            sqlBulkCopy.ColumnMappings.Add("ProfitPrice", "ProfitPrice");
                            sqlBulkCopy.ColumnMappings.Add("ActualCost", "ActualCost");
                            sqlBulkCopy.ColumnMappings.Add("seasonSale", "seasonSale");
                            sqlBulkCopy.ColumnMappings.Add("Item_group", "Item_group");
                            sqlBulkCopy.ColumnMappings.Add("Mgf_code", "Mgf_code");
                            sqlBulkCopy.ColumnMappings.Add("gst_per", "gst_per");
                            sqlBulkCopy.ColumnMappings.Add("sgst_per", "sgst_per");
                            sqlBulkCopy.ColumnMappings.Add("cgst_per", "cgst_per");
                            sqlBulkCopy.ColumnMappings.Add("Manufracture", "Manufracture");
                            sqlBulkCopy.ColumnMappings.Add("offer_type", "offer_type");
                            sqlBulkCopy.ColumnMappings.Add("foodSegment", "foodSegment");
                            sqlBulkCopy.ColumnMappings.Add("TotalViews", "TotalViews");
                            sqlBulkCopy.ColumnMappings.Add("TotalFavs", "TotalFavs");
                            sqlBulkCopy.ColumnMappings.Add("SellingVarience", "SellingVarience");
                            sqlBulkCopy.ColumnMappings.Add("ItemSellingType", "ItemSellingType");
                            sqlBulkCopy.ColumnMappings.Add("Supplier", "Supplier");
                            sqlBulkCopy.ColumnMappings.Add("featured", "featured");
                            sqlBulkCopy.ColumnMappings.Add("PromoVideoLink", "PromoVideoLink");
                            sqlBulkCopy.ColumnMappings.Add("LongDescription", "LongDescription");
                            sqlBulkCopy.ColumnMappings.Add("MainCategory", "MainCategory");
                            sqlBulkCopy.ColumnMappings.Add("FeaturedStartDate", "FeaturedStartDate");
                            sqlBulkCopy.ColumnMappings.Add("Approval", "Approval");
                            sqlBulkCopy.ColumnMappings.Add("OfferId", "OfferId");
                            sqlBulkCopy.ColumnMappings.Add("StockType", "StockType");
                            sqlBulkCopy.ColumnMappings.Add("NetWeight", "NetWeight");
                            sqlBulkCopy.ColumnMappings.Add("TotalStock", "TotalStock");
                            sqlBulkCopy.ColumnMappings.Add("MaxQuantityAllowed", "MaxQuantityAllowed");
                            sqlBulkCopy.ColumnMappings.Add("Brand", "Brand");
                            sqlBulkCopy.ColumnMappings.Add("Tag", "Tag");
                            sqlBulkCopy.ColumnMappings.Add("ItemType", "ItemType");
                            sqlBulkCopy.ColumnMappings.Add("Visibility", "Visibility");
                            sqlBulkCopy.ColumnMappings.Add("Coupen_Disc", "Coupen_Disc");
                            sqlBulkCopy.ColumnMappings.Add("Relation", "Relation");
                            //sqlBulkCopy.ColumnMappings.Add("RelationValue", "RelationValue");
                            sqlBulkCopy.ColumnMappings.Add("Parent_ItemId", "Parent_ItemId");
                            sqlBulkCopy.ColumnMappings.Add("Tax_Value", "Tax_Value");
                            sqlBulkCopy.ColumnMappings.Add("Is_Tax_Free", "Is_Tax_Free");
                            sqlBulkCopy.ColumnMappings.Add("KOT_Print", "KOT_Print");
                            sqlBulkCopy.ColumnMappings.Add("KOT_PrintDesc", "KOT_PrintDesc");
                            sqlBulkCopy.ColumnMappings.Add("Check_Specail", "Check_Specail");
                            sqlBulkCopy.ColumnMappings.Add("Food_Type", "Food_Type");
                            sqlBulkCopy.ColumnMappings.Add("Food_SubType", "Food_SubType");
                            sqlBulkCopy.ColumnMappings.Add("Spicy_Level", "Spicy_Level");
                            con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    throw;
                }
            }
            return RedirectToAction("Manage", "ItemMaster");
        }
        public async Task<JsonResult> GetsubfoodList(string foodType)
        {
            return Json(await _itemSI.GetSubfoodBymainFood(foodType));
        }
        [HttpPost]
        public JsonResult ItemsVariance(ItemMasters variance, IFormFile[] files, AliyunCredential credential)
        {
            var aliyunfolder = "HurTex";
            variance.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
            variance.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
            variance.Hub = hubId;
            var result = _itemSI.SizeInfo(variance).Result;
            variance.PriceId = Convert.ToString(result);
            //if (variance.ImagePath != null && variance.PriceId != null)
            //{
            //    string fullPath = "";
            //    var newfilepath = FilePath();
            //    fullPath = Path.Combine(newfilepath + variance.ImagePath.FileName);
            //    var filepath = Path.GetTempFileName();
            //    using (var stream = new FileStream(fullPath, FileMode.Create))
            //    {
            //         variance.ImagePath.CopyToAsync(stream);
            //    }
            //    var endResult = BlAliyun.PutIconObjectFromFile(variance.ImagePath.FileName, variance.PriceId, fullPath, credential, "Product-image", aliyunfolder);

            //    if (endResult.Equals("true"))
            //    {
            //        _itemSI.uploadimagecreateiteam(variance.PriceId);
            //        System.IO.File.Delete(fullPath);
            //    }
            //    else
            //    {
            //        TempData["Image"] = "Error while uploading Image.....";
            //    }
            //}
            return Json(new { success = true, responseText = "Price created!", data = result });
            //return Json(0);
        }
        public async Task<IActionResult> UploadVaraintImages(ItemMasters variance, AliyunCredential credential, List<IFormFile> files)
        {
            var aliyunfolder = "HurTex";
            if (variance.ImagePath != null && variance.PriceId != null)
            {
                string fullPath = "";
                var newfilepath = FilePath();
                fullPath = Path.Combine(newfilepath + variance.ImagePath.FileName);
                var filepath = Path.GetTempFileName();
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    variance.ImagePath.CopyTo(stream);
                }
                var endResult = BlAliyun.PutIconObjectFromFile(variance.ImagePath.FileName, variance.PriceId, fullPath, credential, "Product-image", aliyunfolder);
                if (endResult.Equals("true"))
                {
                    _itemSI.uploadimagecreateiteam(variance.PriceId,"update");
                    _itemSI.uploadimageCount( variance.PriceId, files.Count);
                    System.IO.File.Delete(fullPath);
                }
                else
                {
                    TempData["Image"] = "Error while uploading Image.....";
                }
            }
            if (files != null && variance.PriceId != null)
            {
                var count = 0;
                foreach (var img in files)
                { 
                    string fullPath = "";
                    var newfilepath = FilePath();
                    variance.ImagesCount = variance.ImagesCount;
                    fullPath = Path.Combine(newfilepath + img.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }

                    var endResult1 = BlAliyun.PutIconObjectFromFile(img.FileName, variance.PriceId + (("_") + variance.ImagesCount), fullPath, credential, "Product-image", aliyunfolder);

                    if (endResult1.Equals("true"))
                    {
                        _itemSI.uploadimagecreateiteam(variance.PriceId, "update");
                        _itemSI.uploadimageCount(variance.PriceId, files.Count);
                        System.IO.File.Delete(fullPath);
                        variance.ImagesCount++;
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }
                }
            }
            return Json("True");
        }
        public async Task<JsonResult> CheckUniqueBarcode(string Barcode)
        {
            try
            {
                var empId = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _itemSI.CheckUniqueBarcode(Barcode);
                return Json(new Message<string> { IsSuccess = true, ReturnMessage = "success", Data = Convert.ToString(result) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        [HttpPost]
        public JsonResult HubItemsVariance(ItemMasters variance, IFormFile[] files)
        {
            variance.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
            variance.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
            variance.hubId = hubId;
            var result = _itemSI.HubSizeInfo(variance);
            return Json(true);
        }
        public JsonResult ItmDeleteSize(string PriceId, string Condition)
        {
            return Json(_itemSI.ItemSizeDelete(PriceId, Condition, hubId));
        }
        public JsonResult ItmDeleteWeightQty(string PriceId, string Condition)
        {
            return Json(_itemSI.ItemWeightQtyDelete(PriceId, Condition, hubId));
        }
        public JsonResult ItmDeleteColor(string ColorId, string Condition)
        {
            return Json(_itemSI.ItemColorDelete(ColorId, Condition));
        }
        public async Task<JsonResult> UploadMultiImageAsync(ItemColorInfo colorInfo, AliyunCredential credential)
        {
            var aliyunfolder = "HurTex";
            colorInfo.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
            colorInfo.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
            var result = _itemSI.CreateColor(colorInfo);
            colorInfo.ColorId = Convert.ToString("COLID0" + result);
            var endResult = "true";
            if (colorInfo.Image != null && result > 0)
            {
                string fullPath = "";
                string folderName = "Documents\\";
                var rootFolder = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(rootFolder, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                fullPath = Path.Combine(newPath + colorInfo.Image.FileName);
                var filepath = Path.GetTempFileName();
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await colorInfo.Image.CopyToAsync(stream);
                }
                endResult = BlAliyun.PutIconObjectFromFile(colorInfo.Image.FileName, colorInfo.ColorId, fullPath, credential, "color-variance", aliyunfolder);
                if (endResult.Equals("true"))
                {
                    System.IO.File.Delete(fullPath);
                }
                else
                {
                    TempData["Image"] = "Error while uploading Image.....";
                }
            }
            return Json(endResult);
        }
        public JsonResult MappingVariance(SizeColorData data)
        {
            data.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
            data.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
            return Json(_itemSI.InsertMappingValue(data));
        }
        public JsonResult EditMappingVariance(SizeColorData data)
        {
            data.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
            data.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
            return Json(_itemSI.EditMappingValue(data));
        }

        //AB0069
        //<!--Modal Detail for Size_Pricelist-->
        [HttpGet]
        public async Task<JsonResult> GetSizeInfoDetails(int id, [FromServices] DropDownRI _DropDownRI)
        {
            try
            {
                return Json(_itemSI.GetSizeInfoDetails(id, hubId));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        public async Task<IActionResult> EditSize(ItemSizeInfo info, AliyunCredential credential)
        {
            try
            {
                var aliyunfolder = info.Aliyunkey;
                info.LastUpdatedBy = Convert.ToString(User.FindFirst("id").Value);
                info.Hub = hubId;
                int result = await _itemSI.UpdateSizeDetail(info);
                if (info.ImagePath != null && info.PriceId != null)
                {
                    string fullPath = "";
                    var newfilepath = FilePath();
                    fullPath = Path.Combine(newfilepath + info.ImagePath.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await info.ImagePath.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(info.ImagePath.FileName, info.PriceId, fullPath, credential, "Product-image", aliyunfolder);
                    if (endResult.Equals("true"))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }
                }
                //return View();
                if (result != 0)
                {
                    if (info.PriceId.Contains('H'))
                    {
                        TempData["ViewData"] = "Size Updated...!";
                        return RedirectToAction("HubPriceDetails", "ItemMaster", new { id = info.ItemId });
                    }
                    else
                    {
                        TempData["ViewData"] = "Size Updated...!";
                        return RedirectToAction("Detail", "ItemMaster", new { id = info.ItemId });
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Something Went Wrong...!";
                }
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        public async Task<PartialViewResult> _GetSizeList(string Id)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.ProductSizeInfoDetails(Id, hubId);
                await Task.WhenAll(getItemmasterlist);
                var vm = new ItemMasterVM
                {
                    getItemMasterList = getItemmasterlist.Result,
                };
                vm.getccurrencylist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_GetSizeList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_GetSizeList");
            }
        }

        public async Task<PartialViewResult> _GetWeightQtyList(string Id)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.ProductSizeInfoDetails(Id, hubId);
                await Task.WhenAll(getItemmasterlist);
                var vm = new ItemMasterVM
                {
                    getItemMasterList = getItemmasterlist.Result,
                };
                vm.getccurrencylist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_GetWeightQtyList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_GetWeightQtyList");
            }
        }

        public async Task<PartialViewResult> _GetNoVarQtyList(string Id)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.ProductSizeInfoDetails(Id, hubId);
                await Task.WhenAll(getItemmasterlist);
                var vm = new ItemMasterVM
                {
                    getItemMasterList = getItemmasterlist.Result,
                };
                vm.getccurrencylist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_GetNoVarQtyList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_GetNoVarQtyList");
            }
        }

        public async Task<PartialViewResult> _GetColorList(string Id)
        {
            try
            {
                Task<List<ItemMasters>> getproductcolorlist = _itemSI.ProductColorInfoDetails(Id);
                await Task.WhenAll(getproductcolorlist);
                var vm = new ItemMasterVM
                {
                    ItemColorInfo = getproductcolorlist.Result,
                };
                return PartialView("_GetColorList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_GetColorList");
            }
        } 
        public async Task<PartialViewResult> _GetMappedItemsList(string Id)
        {
            try
            {
                Task<List<ItemMasters>> GetMappedItemList = _itemSI.GetMappedItemList(Id,hubId);
                await Task.WhenAll(GetMappedItemList);
                var vm = new ItemMasterVM
                {
                    MappedItemList = GetMappedItemList.Result,
                };
                return PartialView("_GetMappedItemsList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_GetMappedItemsList");
            }
        }
        public async Task<PartialViewResult> _GetColorSizeMapping(string Id)
        {
            try
            {
                Task<List<ItemMasters>> getItemSizeInfo = _itemSI.ProductSizeInfoDetails(Id, hubId);
                Task<List<ItemMasters>> getItemColorInfo = _itemSI.ProductColorInfoDetails(Id);
                List<ColorSizeMapping> getSizeColorDetails = _itemSI.GetColorSizeMap(Id);
                await Task.WhenAll(getItemSizeInfo, getItemColorInfo);
                var vm = new ItemMasterVM
                {
                    ItemSizeInfo = getItemSizeInfo.Result,
                    ItemColorInfo = getItemColorInfo.Result,
                    MapInfo = getSizeColorDetails,
                };
                return PartialView("_GetColorSizeMapping", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_GetColorSizeMapping", "");
            }
        }

        // add offer mapped item by single 
        public async Task<JsonResult> AddToItem(ItemMasters info)
        {
            try
            {
                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                info.hubId = hubId;
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = await _itemSI.AddItem(info) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> RemovalExitsVarainttoHub(ItemMasters info)
        {
            try
            {
                info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(new Message<int>
                {
                    IsSuccess = true,
                    ReturnMessage = "success",
                    Data = await _itemSI.RemovalExitsVarainttoHub(info)
                });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        [Authorize]
        public async Task<IActionResult> HubPriceDetails(int Id, [FromServices] DropDownRI _DropDownRI, AliyunCredential credential, [FromServices] IPurchaseSI _purchaseSI)
        {
            try
            {
                //var HubId = User.FindFirst("branch").Value.ToString();
                Task<List<SelectListItem>> getMainCattegorylist = _itemSI.GetMainCategoryList(hubId);
                List<SelectListItem> getSegmentList = _DropDownRI.Segment();
                List<SelectListItem> measurement = _DropDownRI.Measurement();
                Task<List<SelectListItem>> foodTypeList = _itemSI.GetfoodTypeList();
                List<SelectListItem> offertype = _DropDownRI.offerType();
                Task<List<SelectListItem>> supplierNameList = _itemSI.GetSupplierNameList();
                Task<List<SelectListItem>> brandList = _itemSI.GetBrandList();
                Task<ItemMasters> itemMastersDetail = _itemSI.HubGetItemMasterDetail(Id);
                var ItemId = itemMastersDetail.Result.ItemId;
                Task<List<ItemCategory>> getCattegorylist = _itemSI.GetCategoriesAsync(hubId);
                Task<List<SelectListItem>> getSubfoods = _itemSI.GetsubFood();
                Task<List<SelectListItem>> getsubCategoryList = _itemSI.GetSubCategoryList(hubId);
                List<Item> itemList = await _purchaseSI.GetItemList(null);
                Task<List<TaxPercentageMst>> taxList = _itemSI.GetTaxPercentageList();
                Task<List<ItemMasters>> getHubItemVariantDetails = _itemSI.HubProductSizeInfoDetails(ItemId, hubId);
                Task<List<ItemMasters>> getItemVariantDetails = _itemSI.ProductSizeInfoDetails(ItemId, hubId);
                Task<List<ItemMasters>> geUnMappedtItemVariantDetails = _itemSI.UnMappedProductSizeInfoDetails(ItemId, hubId);
                Task<List<ItemMasters>> getItemColorDetails = _itemSI.ProductColorInfoDetails(ItemId);
                List<ColorSizeMapping> getSizeColorDetails = _itemSI.GetColorSizeMap(ItemId);
                Task<List<int>> getMappingDay = _itemSI.GetMappingDaywithItem(itemMastersDetail.Result.ItemId);
                TaxationInfo taxInfo = _settingSI.GetTaxationInfo();
                await Task.WhenAll(getMainCattegorylist, supplierNameList, brandList, itemMastersDetail, getCattegorylist, getsubCategoryList, taxList);
                var IconImages = "https://freshlo.oss-ap-south-1.aliyuncs.com/freshpos/Product-image/" + itemMastersDetail.Result.ItemId + ".png";
                var endResult = BlAliyun.GetObjectFromFile(itemMastersDetail.Result.ItemId, credential);
                var galleryImages = "https://freshlo.oss-ap-south-1.aliyuncs.com/freshpos/Product-image/" + itemMastersDetail.Result.ItemId + ".png";
                List<CurrencyMST> getCurrencyList = _settingSI.GetCurrencyList(hubId);
                var vm = new ItemMasterVM
                {
                    GetMainCategoryList = getMainCattegorylist.Result,
                    GetSegmentList = getSegmentList,
                    GetMeasurementList = measurement,
                    offerType = offertype,
                    SupplierNameList = supplierNameList.Result,
                    BrandList = brandList.Result,
                    ItemMastersDetail = itemMastersDetail.Result,
                    getCattegorylist = getCattegorylist.Result,
                    getsubFoodlist = getSubfoods.Result,
                    GetSubCategory = getsubCategoryList.Result,
                    GetGalleryImage = endResult,
                    ItemList = itemList,
                    taxPercentageList = taxList.Result,
                    branch = Convert.ToString(User.FindFirst("branch").Value),
                    foodTypeList = foodTypeList.Result,
                    GetDayList = getMappingDay.Result,
                    HubItemSizeInfo = getHubItemVariantDetails.Result,
                    ItemSizeInfo = getItemVariantDetails.Result,
                    UnMappedItemSizeInfo = geUnMappedtItemVariantDetails.Result,
                    ItemColorInfo = getItemColorDetails.Result,
                    MapInfo = getSizeColorDetails,
                    taxInfoDetails = taxInfo,
                    getccurrencylist = getCurrencyList,
                };
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                ViewBag.hubId = hubId;
                return View(vm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<PartialViewResult> _HubGetSizeList(string Id)
        {
            try
            {
                //var HubId = User.FindFirst("branch").Value.ToString();
                Task<List<ItemMasters>> gethubItemmasterlist = _itemSI.HubProductSizeInfoDetails(Id, hubId);
                await Task.WhenAll(gethubItemmasterlist);
                var vm = new ItemMasterVM
                {
                    getHubItemMasterList = gethubItemmasterlist.Result,
                };
                vm.getccurrencylist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_HubGetSizeList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_HubGetSizeList");
            }
        }

        public async Task<PartialViewResult> _HubGetWeightQtyList(string Id)
        {
            try
            {
                // var HubId = User.FindFirst("branch").Value.ToString();
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.HubProductSizeInfoDetails(Id, hubId);
                await Task.WhenAll(getItemmasterlist);
                var vm = new ItemMasterVM
                {
                    getItemMasterList = getItemmasterlist.Result,
                };
                vm.getccurrencylist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_GetWeightQtyList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_GetWeightQtyList");
            }
        }

        public async Task<PartialViewResult> _HubGetNoVarQtyList(string Id)
        {
            try
            {
                // var HubId = User.FindFirst("branch").Value.ToString();
                Task<List<ItemMasters>> getItemmasterlist = _itemSI.HubProductSizeInfoDetails(Id, hubId);
                await Task.WhenAll(getItemmasterlist);
                var vm = new ItemMasterVM
                {
                    getItemMasterList = getItemmasterlist.Result,
                };
                vm.getccurrencylist = _settingSI.GetCurrencyList(hubId);
                return PartialView("_GetNoVarQtyList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_GetNoVarQtyList");
            }
        }

        public async Task<PartialViewResult> _HubGetColorList(string Id)
        {
            try
            {
                // var HubId = User.FindFirst("branch").Value.ToString();
                Task<List<ItemMasters>> getproductcolorlist = _itemSI.ProductColorInfoDetails(Id);
                await Task.WhenAll(getproductcolorlist);
                var vm = new ItemMasterVM
                {
                    ItemColorInfo = getproductcolorlist.Result,
                };
                return PartialView("_GetColorList", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_GetColorList");
            }
        }

        public async Task<PartialViewResult> _HubGetColorSizeMapping(string Id)
        {
            try
            {
                Task<List<ItemMasters>> getItemSizeInfo = _itemSI.ProductSizeInfoDetails(Id, hubId);
                Task<List<ItemMasters>> getItemColorInfo = _itemSI.ProductColorInfoDetails(Id);
                List<ColorSizeMapping> getSizeColorDetails = _itemSI.GetColorSizeMap(Id);
                await Task.WhenAll(getItemSizeInfo, getItemColorInfo);
                var vm = new ItemMasterVM
                {
                    ItemSizeInfo = getItemSizeInfo.Result,
                    ItemColorInfo = getItemColorInfo.Result,
                    MapInfo = getSizeColorDetails,
                };
                return PartialView("_GetColorSizeMapping", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_GetColorSizeMapping", "");
            }
        }

        public async Task<JsonResult> AddNonVegCategory(ItemMasters itemMasters)
        {
            try
            {
                itemMasters.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                itemMasters.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = await _itemSI.AddNonVegCategory(itemMasters) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        [HttpGet]
        public JsonResult GetItemLanguageById(int id)
        {
            var result = _itemSI.GetItemLanguageById(id);
            return Json(result);
        }
        public JsonResult DeleteItemLanguageByitemId(int id)
        {
            return Json(_itemSI.DeleteItemLanguageByitemId(id));
        }
        public async Task<PartialViewResult> _AddVarinace(string Id)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<ProductVariance>> getProductVarianceList = _itemSI.GetProductVarianceById(Id, hubId);
                await Task.WhenAll(getProductVarianceList);
                var vm = new ItemMasterVM
                {
                    getProductVarianceList = getProductVarianceList.Result,
                };
              
                return PartialView("_AddVarinace", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_AddVarinace");
            }
        }
       
        public async Task<JsonResult> UpdatemappedVarient(string mappedItemId, string itemId)
        {
            try
            {
                int result = await _itemSI.UpdateVarientItemList(mappedItemId, itemId, hubId);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<PartialViewResult> _GetProductVarianceSpec(string Id,string ProductId)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<ProductSpec>> getProductSpecList = _itemSI.GetProductSpecById(Id, ProductId, hubId);
                await Task.WhenAll(getProductSpecList);
                var vm = new ItemMasterVM
                {
                    getProductSpecList = getProductSpecList.Result,
                };

                return PartialView("_GetProductVarianceSpec", vm);
            }
            catch (Exception ex)
            {
                return PartialView("_GetProductVarianceSpec");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadPdf(string ItemId, IFormFile[] files, AliyunCredential credential)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                var aliyunfolder = "HurTex";
                var endResult = "false";
                if (files != null && ItemId != null)
                {
                    foreach (var img in files)
                    {
                        string fullPath = "";
                        var newfilepath = FilePath();
                        fullPath = Path.Combine(newfilepath + img.FileName);
                        var filepath = Path.GetTempFileName();
                        List<string> image = BlAliyun.GetObjectFromFile(ItemId, credential);
                        List<string> iconImage = BlAliyun.GetObjectIcon(ItemId, credential);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await img.CopyToAsync(stream);
                        }

                        try
                        {
                            endResult = BlAliyun.PutIconObjectFromFile2(img.FileName, ItemId, fullPath, credential, "UploadPdf", aliyunfolder);
                        }
                        catch (Exception ex)
                        {
                            // Handle the exception (e.g., logging)
                            Console.WriteLine("Error putting icon object to Aliyun: " + ex.Message);
                            // Optionally, rethrow the exception if you want it to propagate further
                            throw;
                        }
                    }
                }
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                Console.WriteLine("Error in UploadPdf method: " + ex.Message);
                // Return an appropriate status code or view indicating the error
                return new StatusCodeResult(500);
            }
        }


        public async Task<PartialViewResult> _Getpdfuploadedview(int Id)
        {
            try
            {
                Task<ItemMasters> itemMastersDetail = _itemSI.GetItemMasterDetail(Id);
                var vm = new ItemMasterVM
                {
                    ItemMastersDetail = itemMastersDetail.Result
                };
                return PartialView("_Getpdfuploadedview",vm);
            }
            catch (Exception ex)
            {
                return PartialView("_Getpdfuploadedview");
            }
        }


        [HttpPost]
        public JsonResult AddProductSpec(ProductSpec spec)
        {
            spec.branchId = hubId;
            var result = _itemSI.ProductSpec(spec).Result;
            spec.productCatId = Convert.ToString(result);
         
            return Json(new { success = true, responseText = "added ProductSpec", data = result });
           
        }


        public async Task<JsonResult> DeleteMapItem(string itemId, string mappingItemId)
        {
            try
            {
                hubId = Convert.ToString(User.FindFirst("branch").Value);
                return Json(new Message<Task<int>> { IsSuccess = true, ReturnMessage = "success", Data = _itemSI.DeleteMapItem(itemId, mappingItemId, hubId) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
    }
}
