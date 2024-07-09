using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DemoDecodeURLParameters.Security;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Category;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models;
using Freshlo.Web.Models.BannerVM;
using Freshlo.Web.Models.CategoriesVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freshlo.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoriesSI _categoriesSI;
        public ISettingSI _settingSI { get; set; }
        private readonly IHostingEnvironment _hostingEnvironment;
        public string hubId { get; set; }
        public string language { get; set; }

        private readonly CustomIDataProtection protector;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryController(ICategoriesSI categoriesSI, IHostingEnvironment hostingEnvironment, ISettingSI settingSI, IHttpContextAccessor httpContextAccessor, CustomIDataProtection customIDataProtection)
        {
            _categoriesSI = categoriesSI;
            _hostingEnvironment = hostingEnvironment;
            _settingSI = settingSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
            language = new CookieHelper(_httpContextAccessor).GetCookiesValue("LanguageName");
            protector = customIDataProtection;
        }

        // MainCateogry Related Here....
        public async Task<IActionResult> Manage(string id)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<MainCategory>> getMaincatlist = _categoriesSI.GetMainCategoriesList(hubId);
                Task<List<ItemMasters>> getselectlanguagelist = _settingSI.languageselect(hubId);
                await Task.WhenAll(getMaincatlist, getselectlanguagelist);
                var VM = new MainCatgoriesVM
                {
                    Maincatlist = getMaincatlist.Result,
                    getaddedlanguagelist = getselectlanguagelist.Result,
                };
                var list = VM.Maincatlist.Select(p => new
                {
                    v = p.DecodeId = protector.Decode(p.MainCategoryId.ToString()),
                }).ToList();

                if (TempData["ViewMessage"] != null)
                    VM.ViewMessage = TempData["ViewMessage"] as string;

                if (TempData["ErrorMessage"] != null)
                    VM.ErrorMessage = TempData["ErrorMessage"] as string;

                VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = VM.businessInfo.hotel_name;
                ViewBag.logoUrl = VM.businessInfo.logo_url;
            
                if (language != null)
                {
                    ViewBag.language = language;

                }
                else
                {
                    ViewBag.language = "No Set";

                }
                return View("Manage", VM);
            }
            catch (Exception ex)
            {
                return View("Manage");
            }

        }
        public async Task<IActionResult> AddedMainCategory(MainCategory add, AliyunCredential credential)
        {
            try
            {
                add.hubId = hubId;
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                var aliyunfolder = add.Aliyunkey;
                var existCode = _categoriesSI.GetExistmainCategoryCode().TrimEnd(',');
                var existcategoryCode = _categoriesSI.GetExistCategoryCode().TrimEnd(',');
                string[] word = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                var charString = add.Name.ToUpper().Select(x => new string(x, 1)).ToArray();
                var possibleComb = charString.SelectMany(x => charString.Select(y => charString.Select(z => x + y + z)));
                var maincategoryCode = charString.First() + charString[charString.Length - 2] + charString.Last();
                add.AddedBy = Convert.ToString(User.FindFirst("empId").Value);
                var MaincategoryId = "";
                if (!existCode.Contains(maincategoryCode) && !existcategoryCode.Contains(maincategoryCode))
                {
                    add.hubId = hubId;
                    add.MainCategoryCode = maincategoryCode;
                    MaincategoryId = await _categoriesSI.AddMainCategory(add);

                }
                else
                {
                    
                    add.MainCategoryCode = "NA";
                    MaincategoryId = await _categoriesSI.AddMainCategory(add);

                }
                add.MainCategoryId = Convert.ToString(MaincategoryId);
                if (add.imageFiles != null && add.MainCategoryId != null)
                {
                    string fullPath = "";
                    string folderName = "Documents\\";
                    var rootFolder = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(rootFolder, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    fullPath = Path.Combine(newPath + add.imageFiles.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await add.imageFiles.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(add.imageFiles.FileName,add.MainCategoryId, fullPath, credential, "category-images", aliyunfolder);
                    if (endResult.Equals("true"))
                    {
                        _categoriesSI.uploadimage(MaincategoryId);
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }

                }
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<IActionResult> Detail(string id, MainCategory info)
        {
            //id = protector.Encode(id);
            //var hubId = Convert.ToString(User.FindFirst("branch").Value);
            if (hubId == null)
            {
                hubId = "HID01";
            }
            Task<List<ItemCategoreis>> itemCategoreislist = _categoriesSI.GetItemCategoriesList(id, hubId);
            Task<List<ItemSubCategory>> itemSubCategorieslist = _categoriesSI.GetItemSubCategoriesList(id,hubId);
            Task<List<MainCategory>> getMaincatlist = _categoriesSI.GetMainCategoriesList(hubId);
            Task<MainCategory> mainCategorydetail = _categoriesSI.GetMainCategoryDetails(id,hubId);
            Task<List<ItemMasters>> getselectlanguagelist = _settingSI.languageselect(hubId);
            Task<int> mainHubcatDetail = _categoriesSI.GetMainCategoryDetailsCount(id, hubId);
            Task<ItemCategoreis> itemCategoreis = _categoriesSI.GetCategoryDetails(id, hubId);
            await Task.WhenAll(itemCategoreislist, mainCategorydetail);
            ViewBag.CatId = id;
            var VM = new MainCatgoriesVM
            {               
                Getitemcategorieslist = itemCategoreislist.Result,
                GetitemSubCategoriesList = itemSubCategorieslist.Result,
                Maincatlist= getMaincatlist.Result,
                GetmaincateDetails = mainCategorydetail.Result,
                ItemCategoreisDetails = itemCategoreis.Result,
                hubMappedCountDetails = mainHubcatDetail.Result,
                getaddedlanguagelist = getselectlanguagelist.Result,

            };

            if (TempData["ViewMessage"] != null)
                VM.ViewMessage = TempData["ViewMessage"] as string;

            if (TempData["ErrorMessage"] != null)
                VM.ErrorMessage = TempData["ErrorMessage"] as string;
            VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = VM.businessInfo.hotel_name;
            ViewBag.logoUrl = VM.businessInfo.logo_url;
            ViewBag.maincatId = id;
            ViewBag.id = VM.GetmaincateDetails.Id;
            if (language != null)
            {
                ViewBag.language = language;

            }
            else
            {
                ViewBag.language = "No Set";

            }
           
            return View("Detail", VM);
        }
        public async Task<IActionResult> EditMainCategory(MainCategory add, AliyunCredential credential)
        {
            try
            {

                var aliyunfolder = add.Aliyunkey;
                add.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var MaincategoryId = await _categoriesSI.UpdateMainCategory(add);
                add.MainCategoryId = Convert.ToString(add.MainCategoryId);
                if (add.imageFiles != null && add.MainCategoryId != null)
                {
                    string fullPath = "";
                    string folderName = "Documents\\";
                    var rootFolder = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(rootFolder, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    fullPath = Path.Combine(newPath + add.imageFiles.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await add.imageFiles.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(add.imageFiles.FileName,add.MainCategoryId,fullPath, credential, "category-images", aliyunfolder);

                    if (endResult.Equals("true"))
                    {
                        _categoriesSI.uploadimage(add.MainCategoryId);
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }

                }
                return RedirectToAction("Detail", "Category", new { id = add.MainCategoryId });

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public bool DeleteMainCategory(string maincategoryId)
        {
            if (_categoriesSI.DeleteMaincategory(maincategoryId).Equals(true))
            {
                if(hubId == null)
                {
                    hubId = "HID01";
                }
                var existCategoryId = _categoriesSI.GetExistCategoryId(maincategoryId,hubId);
                foreach (var id in existCategoryId.Split(','))
                {
                    if (id != "")
                    {
                        _categoriesSI.DeleteFrmCatdata(id);
                    }

                }
                return true;
            }
            else
            {
                return false;

            }
        }


        // Cateogry Related Here...
        public async Task<IActionResult> AddCategories(ItemCategoreis add, AliyunCredential credential)
        {
            try
            {
                if(hubId == null)
                {
                    hubId = "HID01";
                }
                var aliyunfolder = add.Aliyunkey;
                var existCode = _categoriesSI.GetExistmainCategoryCode().TrimEnd(',');
                var existcategoryCode = _categoriesSI.GetExistCategoryCode().TrimEnd(',');
                string[] word = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                var charString = add.CategoriesName.ToUpper().Select(x => new string(x, 1)).ToArray();
                var possibleComb = charString.SelectMany(x => charString.Select(y => charString.Select(z => x + y + z)));
                var Categorycode = charString.First() + charString[charString.Length - 2] + charString.Last();
                add.AddedBy = Convert.ToString(User.FindFirst("empId").Value);
                var CategoryId = "";
                if (!existCode.Contains(Categorycode) && !existcategoryCode.Contains(Categorycode))
                {
                    add.Hub = hubId;
                    add.CategoryCode = Categorycode;
                    CategoryId = await _categoriesSI.AddItemCategories(add);


                }
                else
                {
                    add.Hub = hubId;
                    add.CategoryCode = Categorycode;
                    CategoryId = await _categoriesSI.AddItemCategories(add);
                }
                add.Hub = hubId;
                add.CategoryId = Convert.ToString(CategoryId);
                if (add.ImagePath != null && add.CategoryId != null)
                {
                    string fullPath = "";
                    string folderName = "Documents\\";
                    var rootFolder = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(rootFolder, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    fullPath = Path.Combine(newPath + add.ImagePath.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await add.ImagePath.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(add.ImagePath.FileName,add.CategoryId, fullPath, credential, "category-images", aliyunfolder);

                    if (endResult.Equals("true"))
                    {
                        _categoriesSI.uploadimagecategory(CategoryId);
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }

                }
                return RedirectToAction("Detail", "Category", new { id = add.MainCategoryId });
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<IActionResult> EditCateogry(ItemCategoreis add, AliyunCredential credential)
        {
            try
            {
                var aliyunfolder = add.Aliyunkey;
                add.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var CategoryId = await _categoriesSI.UpdateItemCategory(add);
                add.CategoryId = Convert.ToString(add.CategoryId);
                if (add.ImagePath != null && add.CategoryId != null)
                {
                    string fullPath = "";
                    string folderName = "Documents\\";
                    var rootFolder = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(rootFolder, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    fullPath = Path.Combine(newPath + add.ImagePath.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await add.ImagePath.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(add.ImagePath.FileName,add.CategoryId,fullPath, credential, "category-images", aliyunfolder);

                    if (endResult.Equals("true"))
                    {
                        _categoriesSI.uploadimagecategory(add.CategoryId);
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }

                }
                return RedirectToAction("Detail", "Category", new { id = add.MainCategoryId });
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetCategoryDetails(string id)
        {
            MainCatgoriesVM vm = new MainCatgoriesVM();
            try
            {

                vm.ItemCategoreisDetails = await _categoriesSI.GetCategoryDetails(id, hubId);
                return Json(vm, new Newtonsoft.Json.JsonSerializerSettings());

                //return Json(vm.ItemCategoreisDetails);

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }
        public bool DeleteCategory(string categoryId)
        {
            if (categoryId != "")
            {
                _categoriesSI.DeleteFrmCatdata(categoryId);
                return true;

            }
            else
            {
                return false;
            }

        }


        // SubCateogry Related Here..
        public async Task<JsonResult> AddSubCategoriesItem(ItemSubCategory add)
        {
            try
            {
                add.hubId = hubId;
                add.AddedBy = Convert.ToString(User.FindFirst("empId").Value);
                add.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (await _categoriesSI.CheckSubCategories(add))
                {
                    //return Json(new Message<string>
                    //{
                    //    IsSuccess = true,
                    //    ReturnMessage = "success",
                    //    Data = null
                    //});
                    return Json(new Message<string>
                    {
                       
                        IsSuccess = true,
                        ReturnMessage = "success",
                        Data = await _categoriesSI.AddSubItemCategories(add)
                    });
                }
                else
                {
                    return Json(new Message<string>
                    {
                        IsSuccess = true,
                        ReturnMessage = "success",
                        Data = await _categoriesSI.AddSubItemCategories(add)
                    });
                }

            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });

            }
        }
        public async Task<PartialViewResult> _GetItemSubcategorieslist(string id)
        {
            var VM = new MainCatgoriesVM();
            Task<List<ItemSubCategory>> itemSubCategories = _categoriesSI.GetItemSubCategoriesList(id,hubId);
            await Task.WhenAll(itemSubCategories);
            VM = new MainCatgoriesVM
            {
                GetitemSubCategoriesList = itemSubCategories.Result,
            };
            return PartialView("_GetItemSubcategorieslist", VM);
        }
        public bool DeleteSubCategory(string subCategoryId)
        {
            if (subCategoryId != "")
            {
                _categoriesSI.DeleteSubcategories(subCategoryId);
                return true;

            }
            else
            {
                return false;
            }

        }




        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SubCategory()
        {
            return View();
        }
        public IActionResult Backup()
        {
            return View();
        }

        //public async Task<IActionResult> AddSubCategories(ItemSubCategory add)
        //{
        //    try
        //    {
        //        if (await _categoriesSI.CheckSubCategories(add))
        //        {
        //            return RedirectToAction("Detail", "Cateogry");
        //        }
        //        else
        //     {
        //            var addSubcategory = _categoriesSI.AddSubItemCategories(add);
        //            return RedirectToAction("Detail", "Category");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new StatusCodeResult(500);
        //    }
        //}


        public async Task<JsonResult> AddeIntoHub(string MainCatId)
        {
            try
            {
                var CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var HubId = Convert.ToString(User.FindFirst("branch").Value);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = await _categoriesSI.AddeIntoHub(MainCatId, HubId, CreatedBy) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });

            }
        }

        public async Task<JsonResult> RemoveIntoHub(string MainCatId)
        {
            try
            {
                var CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var HubId = Convert.ToString(User.FindFirst("branch").Value);
                return Json(new Message<bool> { IsSuccess = true, ReturnMessage = "success", Data = await _categoriesSI.RemoveIntoHub(MainCatId, HubId, CreatedBy) });
            }
            catch (Exception ex)
            {
                return Json(new Message<bool> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = false });

            }
        }
        [HttpGet]
        public JsonResult GetCategoryLanguage(ItemSubCategory cat)
        {
            try
            {
                cat.hubId = hubId;
                cat.LanguageName = language;
                cat.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(_categoriesSI.GetLanguagecategory(cat));
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }

        [HttpPost]
        public IActionResult GetLanguageMainCategory(LanguageMst maincat)
        {
            try
            {
                maincat.Hub = hubId;
                //maincat.LanguageName = language;
                maincat.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                var result = _categoriesSI.GetLanguageMainCategory(maincat);
                return RedirectToAction("Manage", "Category");
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }

        [HttpGet]
        public async Task<PartialViewResult> GetLanguageMainCategoriesList(string id)
        {
            var VM = new MainCatgoriesVM();
            Task<List<MainCategory>> getlanguageMaincatlist = _categoriesSI.GetLanguageMainCategoriesList(id, hubId);
            await Task.WhenAll(getlanguageMaincatlist);
            VM = new MainCatgoriesVM
            {
                LanguageMaincatlist = getlanguageMaincatlist.Result,
            };
            return PartialView("_maincategory", VM);
        }


        [HttpGet]
        public async Task<PartialViewResult> GetLanguageSubCategoriesList(string id)
        {

            var VM = new MainCatgoriesVM();
            Task<List<ItemSubCategory>> getlanguageSubcatlist = _categoriesSI.GetLanguageSubCategoriesList(id, hubId);
            await Task.WhenAll(getlanguageSubcatlist);
            VM = new MainCatgoriesVM
            {
                Languageitemcategorieslist = getlanguageSubcatlist.Result,
            };
            return PartialView("_categorylanguage", VM);
        }
    }
}