using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using Freshlo.DomainEntities.Inventory;
using Freshlo.SI;
using Freshlo.Web.Extensions;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models;
using Freshlo.Web.Models.Setting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Freshlo.Web.Controllers
{
    public class SettingsController : Controller
    {
        private ISettingSI _settingSI;
        private readonly IHostingEnvironment _hostingEnvironment;
        public string hubId { get; set; }
        public string language { get; set; }
        public string GstActive { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public SettingsController(ISettingSI settingSI, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _settingSI = settingSI;
            _hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
            language = new CookieHelper(_httpContextAccessor).GetCookiesValue("LanguageName");
            GstActive = new CookieHelper(_httpContextAccessor).GetCookiesValue("GstActive");
        }


        // All List View Action Here 
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int id, [FromServices] ISystemConfigSI _systemConfig, string hubId)
        {
            try
            {
                Task<List<CustomersAddress>> getZiplist = _settingSI.GetZiplist(hubId);
                Task<List<CustomersAddress>> getpaymentsgatewaylist = _settingSI.Getpaymentsgatewaylist(hubId);
                Task<List<SelectListItem>> getHublist = _settingSI.GetHubList();
                Task<List<DeleiverySlot>> deleiverySlots = _settingSI.GetSlotList();
                Task<List<BrandInfo>> brandInfos = _settingSI.GetBrandList();
                Task<List<TableInfo>> tableInfos = _settingSI.GetTableList(hubId);

                Task<List<Hub>> HubInfoList = _settingSI.GetHubOrgList();
                Task<List<SelectListItem>> getSupplierlist = _settingSI.GetSupplierlist();
                Task<List<string>> getSelectedSuplist = _settingSI.GetSupplierlist(id);
                Task<SecurityConfig> getpasswordsecurity = _systemConfig.GetSecurityConfigAsync();
                await Task.WhenAll(getZiplist, getpaymentsgatewaylist, getHublist, deleiverySlots, brandInfos, tableInfos, getSupplierlist, getSelectedSuplist, getpasswordsecurity);
                await Task.WhenAll(getZiplist, getpaymentsgatewaylist, getHublist, deleiverySlots, brandInfos, tableInfos, HubInfoList, getSupplierlist, getSelectedSuplist);
                BrandInfo branddetails = _settingSI.GetBrandDetails(id);

                var vm = new SettingVM
                {
                    GetZipCodelist = getZiplist.Result,
                    GetPayment_gateway_List = getpaymentsgatewaylist.Result,
                    GetHubList = getHublist.Result,
                    GetSlotlist = deleiverySlots.Result,
                    Getbrandlist = brandInfos.Result,
                    GetOrgHubList = HubInfoList.Result,
                    GetSupplier = getSupplierlist.Result,
                    GetSupplierlist = getSelectedSuplist.Result,
                    getbrandetails = branddetails,
                    GetpasswordSecurityDetail = getpasswordsecurity.Result,
                    Gettablelist = tableInfos.Result,

                };
                return View(vm);

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Index(CustomersAddress add, DeleiverySlot info)
        {
            try
            {
                var data = add.Hub;
                if (data != null)
                {
                    var Zipcodelist = await _settingSI.AddZipCode(add);

                }
                var Slotadd = await _settingSI.AddSlot(info);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }


        [HttpGet]
        public JsonResult GetZipCodeDetails(int id)
        {
            return Json(_settingSI.GetZipCodeDetails(id));
        }
        public JsonResult UpdateZipCodeDetails(CustomersAddress info)
        {
            try
            {
                info.Hub = hubId;
                return Json(_settingSI.EditZipCode(info));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
         public JsonResult GetpaymentgatewayDetails(int id)
        {
            return Json(_settingSI.GetpaymentgatewayDetails(id));
        }
        public JsonResult UpdatepaymentsDetails(CustomersAddress info)
        {
            try
            {
                info.Hub = hubId;
                return Json(_settingSI.Editpayments(info));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        [HttpGet]
        public JsonResult GetRecipeDetails(int id)
        {
            return Json(_settingSI.GetRecipeDetails(id));
        }
        public JsonResult UpdateRecipeDetails(Recipe info)
        {
            try
            {
               
                return Json(_settingSI.EditRecipe(info));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        [HttpGet]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _settingSI.DeleteZipcode(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }
        public async Task<JsonResult> paymentgatewayDelete(int id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _settingSI.Deletepayments(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }
        [HttpGet]
        public async Task<JsonResult> DeleteRecipe(int id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _settingSI.DeleteRecipe(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }


        // Slot Related Action Here...
        [HttpGet]
        public JsonResult GetSlotDetails(int id)
        {
            return Json(_settingSI.GetSlostDetail(id));
        }
        public JsonResult UpdateSlotList(DeleiverySlot info)
        {
            try
            {
                return Json(_settingSI.EditSlot(info));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        public JsonResult UpdateOfferType(OfferType info)
        {
            try
            {
                return Json(_settingSI.EditOfferType(info));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetSlotDelete(int id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _settingSI.DeleteSlot(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }


        // Brand Related Action Here...

        public async Task<IActionResult> AddBrand(BrandInfo add, AliyunCredential credential)
        {
            try
            {
                var data = add.BrandName;
                add.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (data != null)
                {
                    var brandadd = await _settingSI.AddBrand(add);
                    add.BrandId = Convert.ToString(brandadd);
                }
                if (add.ImageLogo != null)
                {
                    string fullPath = "";
                    var newfilepath = FilePath();
                    fullPath = Path.Combine(newfilepath + add.ImageLogo.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await add.ImageLogo.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(add.ImageLogo.FileName,add.BrandId, fullPath, credential, "brand", "");

                    if (endResult.Equals("true"))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> EditBrand(BrandInfo add, AliyunCredential credential)
        {
            try
            {
                var data = add.BrandName;
                add.LastupdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (data != null)
                {
                    var brandadd = await _settingSI.EditBrand(add);
                }
                if (add.ImageLogo != null)
                {
                    string fullPath = "";
                    string folderName = "Documents\\";
                    var rootFolder = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(rootFolder, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    fullPath = Path.Combine(newPath + add.ImageLogo.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await add.ImageLogo.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(add.ImageLogo.FileName,add.BrandId, fullPath, credential, "brand", "");

                    if (endResult.Equals("true"))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
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

        [HttpGet]
        public async Task<JsonResult> GetBrandDetails(int id)
        {
            SettingVM vm = new SettingVM();
            try
            {

                vm.getbrandetails = _settingSI.GetBrandDetails(id);
                vm.GetSupplierlist = await _settingSI.GetSupplierlist(id);
                return Json(vm);

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }

        [HttpGet]
        public async Task<JsonResult> DeleteBrand(int id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _settingSI.DeleteBrand(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }



        // Table Related Action Here 

        public async Task<IActionResult> AddTable(TableInfo add)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                var data = add.tableName;
                add.created_by = Convert.ToString(User.FindFirst("empId").Value);
                if (data != null)
                {
                    var tableadd = await _settingSI.AddTable(add);
                    add.tableId = Convert.ToString(tableadd);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> EditTable(TableInfo add)
        {
            try
            {
                var data = add.tableName;
                add.updated_by = Convert.ToString(User.FindFirst("empId").Value);
                if (data != null)
                {
                    var brandadd = await _settingSI.EditTable(add);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        public JsonResult GetTableDetails(int id)
        {
            SettingVM vm = new SettingVM();
            try
            {

                vm.gettableetails = _settingSI.GetTableDetails(id);
                Task<List<SelectListItem>> getHublist = _settingSI.GetHubList();
                return Json(vm);

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }

        [HttpGet]
        public async Task<JsonResult> DeleteTable(int id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _settingSI.DeleteTable(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }




        // Hub Related Action Here
        public async Task<IActionResult> CreateHub(Hub add)
        {
            try
            {
                var data = add.HubName;
                var cordinates = new Rootobject();
                add.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (data != null)
                {
                    var address = add.City + "," + add.Area + "," + add.Country;
                    cordinates = await _settingSI.geLongitude(address);
                    add.Latitude = cordinates.latitude;
                    add.Longitude = cordinates.longitude;
                    var Hubadd = await _settingSI.AddHub(add);
                    add.HubId = Convert.ToString(Hubadd);
                }



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        public JsonResult GetHubDetails(int id)
        {
            return Json(_settingSI.GetHubDetails(id));
        }

        [HttpGet]
        public async Task<JsonResult> DeleteHub(int id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _settingSI.DeleteHub(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }

        public async Task<IActionResult> EditHub(Hub edit)
        {
            var cordinates = new Rootobject();
            edit.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
            if (edit.HubName != null)
            {
                var address = edit.City + "," + edit.Area + "," + edit.Country;
                cordinates = await _settingSI.geLongitude(address);
                edit.Latitude = cordinates.latitude;
                edit.Longitude = cordinates.longitude;
                var Hubedit = _settingSI.EditHub(edit);
            }
            return RedirectToAction("Index");
        }



        // Password Policy Here 

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> _passwordSecurityPost(SecurityConfig passwordconfig)
        {
            try
            {
                passwordconfig.Modified_By = Convert.ToInt32(User.FindFirst("id").Value);
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "Success", Data = await _settingSI.SystemConfigUpdate(passwordconfig) });
                //return Json(true);
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Something went wrong please try again later", Data = "error" });
            }
        }



        // New View Here...
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Info()
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<SelectListItem>> getHublist = _settingSI.GetHubList();
                Task<List<SelectListItem>> gettblperferncelist = _settingSI.GettblPeferencelist();
                Task<List<SelectListItem>> getSupplierlist = _settingSI.GetSupplierlist();
                Task<List<ItemMasters>> getlanguagelist = _settingSI.selectlanguage(hubId);
                Task<List<ItemMasters>> getselectlanguagelist = _settingSI.languageselect(hubId);
                await Task.WhenAll(getHublist, getSupplierlist, getlanguagelist);
                var vm = new SettingVM
                {
                    GetHubList = getHublist.Result,
                    GetSupplier = getSupplierlist.Result,
                    Gettblperferncelist = gettblperferncelist.Result,
                    getlanguagelist = getlanguagelist.Result,
                    getaddedlanguagelist = getselectlanguagelist.Result,
                  
                };
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                ViewBag.hubId = hubId;
                ViewBag.GstActive = GstActive;
                return View(vm);

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        public PartialViewResult _GetzipList(string id)
        {
            try

            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                var data = _settingSI.GetZiplist(hubId);
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }
        public PartialViewResult _GetPaymentsgatewayList(string id)
        {
            try

            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                var data = _settingSI.Getpaymentsgatewaylist(hubId);
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }

        public PartialViewResult _GetRecipeList(string id)
        {
            try

            {
                var data = _settingSI.GetRecipeList(id);
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }
        public PartialViewResult _GetSlotList()
        {
            try
            {
                var data = _settingSI.GetSlotList();
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }
        public PartialViewResult _GetofferList()
        {
            try
            {
                var data = _settingSI.GetofferList();
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }
        public PartialViewResult _GetBrandList()
        {
            try
            {
                var data = _settingSI.GetBrandList();
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }
        public PartialViewResult _GetTableList(TableInfo item)
        {
            try
            {
                item.Hub = hubId;
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                var data = _settingSI.GetTableList(hubId);
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }
        public PartialViewResult _GetPasswordSecurityInfo([FromServices] ISystemConfigSI _systemConfig)
        {
            try
            {
                var data = _systemConfig.GetSecurityConfigAsync();
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }
        public JsonResult GetZipDetails(int id)
        {
            try
            {
                return Json(_settingSI.GetZipCodeDetails(id));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        public JsonResult GetpaymentDetails(int id)
        {
            try
            {
                return Json(_settingSI.GetpaymentgatewayDetails(id));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        public JsonResult GetDeliverySlotDetails(int id)
        {
            try
            {
                return Json(_settingSI.GetSlostDetail(id));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        public JsonResult GetOfferTypeDetails(int id)
        {
            try
            {
                return Json(_settingSI.GetOfferType(id));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        public async Task<JsonResult> GetBrandInfoDetails(int id)
        {
            SettingVM vm = new SettingVM();
            try
            {

                vm.getbrandetails = _settingSI.GetBrandDetails(id);
                vm.GetSupplierlist = await _settingSI.GetSupplierlist(id);
                //return Json(data);
                return Json(new Message<SettingVM>() { IsSuccess = false, ReturnMessage = "Success", Data = vm });

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }

        }
        public JsonResult GetTableInfoDetails(int id)
        {
            try
            {
                return Json(_settingSI.GetTableDetails(id));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        public JsonResult CreateZipCode(CustomersAddress data)
        {
            try
            {
                data.Hub = hubId;
                data.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(_settingSI.AddZipCode(data));
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }
        public JsonResult Createpaymentsgateway(CustomersAddress data)
        {
            try
            {
                data.Hub = hubId;
                data.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(_settingSI.Addpaymentsgateway(data));
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }
        public JsonResult CreateRecipe(Recipe data)
        {
            try
            { 
           
                data.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(_settingSI.AddRecipe(data));
               
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }
        public JsonResult CreateDeliverySlot(DeleiverySlot data)
        {
            try
            {
                return Json(_settingSI.AddSlot(data));
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }
        public JsonResult CreateOfferType(OfferType data)
        {
            try
            {
                return Json(_settingSI.AddOfferType(data));
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }

        [HttpGet]
        public JsonResult CreateBrand(BrandInfo add, AliyunCredential credential)
        {
            try
            {
                var aliyunfolder = add.Aliyunkey;
                var data = add.BrandName;
                add.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (data != null)
                {
                    var brandadd = _settingSI.AddBrand(add);
                    if (add.ImageLogo != null)
                    {
                        string fullPath = "";
                        var newfilepath = FilePath();
                        fullPath = Path.Combine(newfilepath + add.ImageLogo.FileName);
                        var filepath = Path.GetTempFileName();
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            add.ImageLogo.CopyToAsync(stream);
                        }
                        var endResult = BlAliyun.PutIconObjectFromFile(add.ImageLogo.FileName,add.BrandId,  fullPath, credential, "brand", aliyunfolder);

                        if (endResult.Equals("true"))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        else
                        {
                            TempData["Image"] = "Error while uploading Image.....";
                        }
                    }

                    add.BrandId = Convert.ToString(brandadd);
                }
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }

        public JsonResult UpdateBrand(BrandInfo add, AliyunCredential credential)
        {
            try
            {
                var aliyunfolder = add.Aliyunkey;
                var data = add.BrandName;
                add.LastupdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (data != null)
                {
                    var brandadd = _settingSI.EditBrand(add);
                }
                if (add.ImageLogo != null)
                {
                    string fullPath = "";
                    var newfilepath = FilePath();
                    fullPath = Path.Combine(newfilepath + add.ImageLogo.FileName);
                    var filepath = Path.GetTempFileName();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        add.ImageLogo.CopyToAsync(stream);
                    }
                    var endResult = BlAliyun.PutIconObjectFromFile(add.ImageLogo.FileName,add.BrandId, fullPath, credential, "brand", aliyunfolder);

                    if (endResult.Equals("true"))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    else
                    {
                        TempData["Image"] = "Error while uploading Image.....";
                    }

                }
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }


        public JsonResult CreateTableCode(TableInfo add)
        {
            try
            {
                add.Hub = hubId;
                var data = add.tableName;
                add.created_by = Convert.ToString(User.FindFirst("empId").Value);
                if (data != null)
                {
                    var tableadd = _settingSI.AddTable(add);
                    add.tableId = Convert.ToString(tableadd);
                }
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json("Not Found");
            }
        }

        public JsonResult UpdateTableCode(TableInfo add)
        {
            var brandadd = new TableInfo();
            try
            {
                var data = add.tableName;
                add.updated_by = Convert.ToString(User.FindFirst("empId").Value);
                if (data != null)
                {
                    return Json(_settingSI.EditTable(add));

                }
                else
                {
                    return Json(0);

                }
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }


        public PartialViewResult _GetBusinessInfo(int id)
        {
            try
            {
                BusinessInfo data = new BusinessInfo();
                data = _settingSI.GetbusinessInfoDetails(id);
                data.getCurrencyList = _settingSI.GetConfigCurrencyList(hubId);
                data.getTimezoneList = _settingSI.getTimezonelist();
                return PartialView(data);
            }
            catch (Exception e)
            {
                return PartialView("");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> _businessInfoPost(BusinessInfo businessconfig, AliyunCredential credential)
        {
            try
            {
                businessconfig.secondarylogo_url = "NA";
                var businessId = _settingSI.BusinessConfigUpdate(businessconfig);
                int id = 1;
                var currency = businessconfig.currency;
                var symbol = businessconfig.symbol;
                //var data = _settingSI.CurrencySymbolUpdate(id, currency, symbol);
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "Success", Data = businessId.Result });

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Something went wrong please try again later", Data = "error" });
            }
        }

        [HttpPost]
        public IActionResult _businessLogUploader(IFormFile logoUpload, int hotel_id, AliyunCredential credential)
        {
            if (logoUpload != null)
            {
                var aliyunfolder = _settingSI.GetbusinessInfoDetails(0).aliyunPath;
                string fullPath = "";
                string folderName = "Documents\\";
                var rootFolder = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(rootFolder, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                fullPath = Path.Combine(newPath + logoUpload.FileName);
                var filepath = Path.GetTempFileName();
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    logoUpload.CopyTo(stream);
                }
                var endResult = BlAliyun.PutIconObjectFromFile(logoUpload.FileName, hotel_id.ToString(), fullPath, credential, "logo", aliyunfolder);
                if (endResult.Equals("true"))
                {
                    System.IO.File.Delete(fullPath);
                }
                else
                {
                    TempData["Image"] = "Error while uploading Image.....";
                }
                return new ObjectResult(new { status = "success" });
            }
            return new ObjectResult(new { status = "fail" });

        }


        [HttpPost]
        public IActionResult _businessSecondaryLogUploader(IFormFile SecondUploadImage, int hotel_id, AliyunCredential credential)
        {
            var aliyunfolder = _settingSI.GetbusinessInfoDetails(0).aliyunPath;
            var hoteldId = hotel_id + ".2";
            if (SecondUploadImage != null)
            {
                string fullPath = "";
                string folderName = "Documents\\";
                var rootFolder = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(rootFolder, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                fullPath = Path.Combine(newPath + SecondUploadImage.FileName);
                var filepath = Path.GetTempFileName();
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    SecondUploadImage.CopyTo(stream);
                }
                var endResult = BlAliyun.PutIconObjectFromFile(SecondUploadImage.FileName,hoteldId.ToString(), fullPath, credential, "logo", aliyunfolder);
                if (endResult.Equals("true"))
                {
                    System.IO.File.Delete(fullPath);
                }
                else
                {
                    TempData["Image"] = "Error while uploading Image.....";
                }
                return new ObjectResult(new { status = "success" });
            }
            return new ObjectResult(new { status = "fail" });

        }




        public PartialViewResult _GetTaxInfo()
        {
            try
            {
                var data = _settingSI.GetTaxationInfo();
                return PartialView(data);
            }
            catch
            {
                return PartialView("");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> _GetTaxationUpdate(TaxationInfo taxconfig)
        {
            try
            {
                taxconfig.modifiedBy = Convert.ToString(User.FindFirst("empId").Value);
                taxconfig.hubId = hubId;
                ViewBag.GstActive = GstActive;
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "Success", Data = await _settingSI.TaxInfoUpdate(taxconfig,hubId) });
               
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Something went wrong please try again later", Data = "error" });
            }
        }

        public PartialViewResult _GetCurrencyList()
        {
            try
            {
                var data = _settingSI.GetConfigCurrencyList(hubId);
                return PartialView(data);
            }
            catch
            {
                return PartialView("");
            }
        }
        [HttpGet]
        public async Task<JsonResult> DeleteCurrency(int id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _settingSI.DeleteCurrency(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }
        [HttpGet]
        public JsonResult GetCurrencyInfoDetails(int id)
        {
            SettingVM vm = new SettingVM();
            try
            {

                //vm.getcurrencyInfoDetails = 
                return Json(_settingSI.GetCurrencyDetails(id));

            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }
        public JsonResult AddCurrency(CurrencyMST add)
        {
            try
            {
                add.hubId = hubId;
                var data = add.CountryName;
                add.CratedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (data != null)
                {
                    var currency = _settingSI.AddCurrency(add);
                }
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json("Not Found");
            }
        }
        public JsonResult EditCurrency(CurrencyMST add)
        {
            var brandadd = new CurrencyMST();
            try
            {
                var data = add.CountryName;
                add.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (data != null)
                {
                    return Json(_settingSI.EditCurrency(add));
                }
                else
                {
                    return Json(0);
                }
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }

        public PartialViewResult _InventoryAssetList(string id)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                var data = _settingSI.Get_Inventory_Assets_List(hubId);
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }

        [HttpGet]
        public async Task<JsonResult> Delete_Inventory_Asset(int id)
        {
            try
            {
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "success", Data = await _settingSI.Delete_Inventory_Asset(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }

        [HttpGet]
        public JsonResult InventoryDetails(int Id, string id)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                return Json(_settingSI.InventoryDetails(Id, hubId));
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });

            }
        }

        [HttpGet]
        public JsonResult CreateInventory(InventoryAsset Add)
        {
            try
            {
                Add.Hub = hubId;
                Add.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(_settingSI.CreateInventory(Add));
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }

        [HttpGet]
        public JsonResult UpdateInventory(InventoryAsset Add)
        {
            try
            {
                Add.ModifiedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(_settingSI.UpdateInventory(Add));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        [HttpPost]
        public async Task<IActionResult> changelanguage(LanguageMst Add)
        {
            try
            {
                Add.Hub = hubId;
                //Add.LanguageName = language;
                Add.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                //return Json(_settingSI.Languagechange(Add));
                _settingSI.Languagechange(Add);
                return RedirectToAction("Detail", "ItemMaster", new { Id = Add.Id1 });
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }

        [HttpGet]
        public JsonResult Addlanguage(ItemMasters Add)
        {
            try
            {
                Add.Hub = hubId;
                return Json(_settingSI.Addlanguage(Add));
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }

        [HttpGet]
        public async Task<JsonResult> deleteLanguage(int id)
        {
            try
            {
                return Json(new Message<int>() { IsSuccess = true, ReturnMessage = "success", Data = await _settingSI.deleteLanguage(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }
        public async Task<PartialViewResult> languageselect(int id)
        {
            Task<List<ItemMasters>> getselectlanguagelist = _settingSI.languageselect(hubId);
            await Task.WhenAll(getselectlanguagelist);
            var vm = new SettingVM
            {
                getaddedlanguagelist = getselectlanguagelist.Result,
            };
            return PartialView("_AddlanguageList", vm);
        }


   
        public PartialViewResult _GetProductSpecMain(string id)
        {
            try
            {
                var data = _settingSI.GetProductMainList(hubId);
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }
        public PartialViewResult _GetProductSpecSucCategory(string productId)
        {
            try
            {
                var data = _settingSI.GetProductSpecSucCategoryList(hubId, productId);
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }
        public JsonResult AddProductSpecs(ProductType pro)
        {
            try
            {
                pro.BranchId = hubId;
                    var data = _settingSI.AddProductSpecs(pro);  
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json("Not Found");
            }
        }
        public JsonResult UpdateProductSpecs(ProductType pro)
        {
            try
            {
                pro.BranchId = hubId;
                    var data = _settingSI.UpdateProductSpecs(pro);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json("Not Found");
            }
        }
        public JsonResult DeleteProductSpecs(string Id)
        {
            try
            {
                var data = _settingSI.DeleteProductSpecs(Id,hubId);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json("Not Found");
            }
        }  
        public JsonResult DeleteProductSUbSpecs(string Id)
        {
            try
            {
                var data = _settingSI.DeleteProductSUbSpecs(Id,hubId);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json("Not Found");
            }
        }
        public JsonResult AddProductSubSpecs(ProductSpecSubCatgeory pro)
        {
            try
            {
                pro.BranchId = hubId;
                var data = _settingSI.AddProductSubSpecs(pro);

                return Json(data);
            }
            catch (Exception ex)
            {
                return Json("Not Found");
            }
        }
        public JsonResult UpdateProductSubSpecs(ProductSpecSubCatgeory pro)
        {
            try
            {
                pro.BranchId = hubId;
                var data = _settingSI.UpdateProductSubSpecs(pro);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json("Not Found");
            }
        }
    }
}