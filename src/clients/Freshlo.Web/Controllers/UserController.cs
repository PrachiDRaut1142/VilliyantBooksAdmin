using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoDecodeURLParameters.Security;
using Freshlo.Common.Exceptions.Service;
using Freshlo.Common.Exceptions.Services;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Employee;
using Freshlo.DomainEntities.Hub;
using Freshlo.DomainEntities.Vendor;
using Freshlo.RI;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Freshlo.Web.Controllers
{
    public class UserController : Controller
    {
        private IEmployeeSI _employeeSI;
        private ISystemConfigSI _systemConfigService { get; }
        public ISettingSI _settingSI { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string hubId { get; set; }
        private readonly CustomIDataProtection protector;

        public UserController(IEmployeeSI employeeSI, CustomIDataProtection customIDataProtection, ISystemConfigSI systemConfigSI, ISettingSI settingSI, IHttpContextAccessor httpContextAccessor)
        {
            _employeeSI = employeeSI;
            _systemConfigService = systemConfigSI;
            _settingSI = settingSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
            protector = customIDataProtection;

        }

        // User Related Here...

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Task<List<Employee>> getVendorList = _employeeSI.GetVendorListByName();
            Task<List<Employee>> getUserroleList = _employeeSI.GetUserRole();
            Task<List<Hub>> getHublist = _employeeSI.GetHublist();
            await Task.WhenAll(getVendorList, getUserroleList, getHublist);
            var VM = new UserVM
            {
                getVendorList = getVendorList.Result,
                getUserRoleList = getUserroleList.Result,
                getHublist = getHublist.Result
            };

            if (TempData["ViewMessage"] != null)
                VM.ViewMessage = TempData["ViewMessage"] as string;

            if (TempData["ErrorMessage"] != null)
                VM.ErrorMessage = TempData["ErrorMessage"] as string;
            VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = VM.businessInfo.hotel_name;
            ViewBag.logoUrl = VM.businessInfo.logo_url;
            return View(VM);

        }

        [HttpPost]
        [ActionName("CreateorUpdateEmployee")]
        [Authorize]
        public async Task<IActionResult> CreateorUpdateEmployee(Employee userdata)
        {
            try
            {              
                userdata.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                userdata.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                userdata.UserType = "Internal Employee";
                userdata.PartnerType = "NA";
                if (userdata.Branch == "")
                {
                    userdata.Branch = Convert.ToString(User.FindFirst("branch").Value);
                }
                else
                {
                    userdata.Branch = userdata.Branch;
                }
                int result = _employeeSI.CreateorUpdateUser(userdata);
                return RedirectToAction("Manage");
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Employee userdata)
        {
            try
            {
                userdata.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                userdata.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                userdata.UserType = "Internal Employee";
                userdata.PartnerType = "NA";
                if (userdata.Branch == "")
                {
                    userdata.Branch = Convert.ToString(User.FindFirst("branch").Value);
                }
                else
                {
                    userdata.Branch = userdata.Branch;
                }
                userdata.id = _employeeSI.CreateEmployee(userdata);
                string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                await _employeeSI.InitiateEmailVerificationAsync(userdata, baseUrl);
                TempData["ViewMessage"] = "User created successfully.";
                return RedirectToAction("Manage", "User");
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Manage()
        {
            try
            {
                var uservm = new UserVM();
                var Branch = Convert.ToString(User.FindFirst("branch").Value);
                var role = Convert.ToString(User.FindFirst("userRole").Value);

                uservm.Employeelist = await _employeeSI.GetEmployeeList(Branch, role);

                var list = uservm.Employeelist.Select(p => new
                {
                    v = p.DecodeId = protector.Decode(p.id.ToString()),
                }).ToList();

                if (TempData["ViewMessage"] != null)
                    uservm.ViewMessage = TempData["ViewMessage"] as string;

                if (TempData["ErrorMessage"] != null)
                    uservm.ErrorMessage = TempData["ErrorMessage"] as string;

                uservm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = uservm.businessInfo.hotel_name;
                ViewBag.logoUrl = uservm.businessInfo.logo_url;
                return View(uservm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Detail(string id)
        {
            var Branch = Convert.ToString(User.FindFirst("branch").Value);
            var role = Convert.ToString(User.FindFirst("userRole").Value);
            try
            {
                id = protector.Encode(id);
                var uservm = new UserVM();
                uservm.Employeelistbyid = await _employeeSI.GetEmployeeListbyid(id);
               
                uservm.Employeelistuser = await _employeeSI.GetEmployeeListuser(Branch, role,id);
                var list = uservm.Employeelistuser.Select(p => new
                {
                    v = p.DecodeId = protector.Decode(p.id.ToString()),
                }).ToList();

                uservm.Getsecurity= await _systemConfigService.GetSecurityConfigAsync();
                uservm.getHublist = await _employeeSI.GetHublist();

                if (TempData["ViewMessage"] != null)
                    uservm.ViewMessage = TempData["ViewMessage"] as string;

                if (TempData["ErrorMessage"] != null)
                    uservm.ErrorMessage = TempData["ErrorMessage"] as string;

                uservm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = uservm.businessInfo.hotel_name;
                ViewBag.logoUrl = uservm.businessInfo.logo_url;
                return View(uservm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Detail(Employee userdata)
        {
            try
            {
                userdata.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                userdata.UserType = "Internal Employee";
                userdata.PartnerType = "NA";
                if (userdata.Branch == "")
                {
                    userdata.Branch = Convert.ToString(User.FindFirst("branch").Value);
                }
                else
                {
                    userdata.Branch = userdata.Branch;
                }
                if (userdata.OldStatus == "InComplete Registration" && userdata.Status != "InComplete Registration")
                {
                    TempData["ErrorMessage"] = "Email Not Verified, Cannot Change Status";
                    return RedirectToAction("Manage", "User", new { id = userdata.DecodeId });

                }
                else
                {
                    userdata.id =  _employeeSI.UpdateEmployee(userdata);
                    TempData["ViewMessage"] = "User Updated successfully.";
                    return RedirectToAction("Detail", "User", new { id = userdata.DecodeId });
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        // Access Related Here....

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Accesscontrol()
        {
            try
            {
              
                Task<List<WebAccessPermission>> getwebaccesslist = _employeeSI.GetEmployeeWebAccessList();
                Task<List<GlobalIPInfo>> getgloballist = _employeeSI.GetGlobalAccessList();
                Task<List<WebAccessInfoLog>> getwebaccessloglist = _employeeSI.GetWebAccessLogList();
                await Task.WhenAll(getwebaccesslist, getgloballist, getwebaccessloglist);
                var vm = new EmployeeAccessVM
                {
                    GetWebAccessList = getwebaccesslist.Result,
                    GetGlobalAccessList = getgloballist.Result,
                    GetWebAccessLogList = getwebaccessloglist.Result
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AccessControl(WebAccessPermission webaccessInfo)
        {
            try
            {
                if (webaccessInfo.Id == 0)
                {
                    webaccessInfo.Created_By = Convert.ToString(User.FindFirst("empId").Value);
                    webaccessInfo.Status = 1;
                    int result = await _employeeSI.EmployeeWebAccessPermissionCreate(webaccessInfo);
                }
                else
                {
                    webaccessInfo.Updated_By = Convert.ToString(User.FindFirst("empId").Value);
                    int result = await _employeeSI.EmployeeWebAccessPermissionUpdate(webaccessInfo);
                }
                return RedirectToAction("Accesscontrol");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> WhitelistAccesscontrol(WebAccessPermission webaccessInfo)
        {
            try
            {
                webaccessInfo.Created_By = Convert.ToString(User.FindFirst("empId").Value);
                int result = await _employeeSI.EmployeeWhitelistWebAccessCreate(webaccessInfo);
                return RedirectToAction("Accesscontrol");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> GlobalIpcontrol(GlobalIPInfo globalinfo)
        {
            try
            {
                if (globalinfo.Id == 0)
                {
                    globalinfo.Created_By = Convert.ToString(User.FindFirst("empId").Value);
                    globalinfo.Status = 1;
                    int result = await _employeeSI.GlobalPermissionCreate(globalinfo);
                }
                else
                {
                    globalinfo.Updated_By = Convert.ToString(User.FindFirst("empId").Value);
                    int result = await _employeeSI.GlobalPermissionUpdate(globalinfo);
                }
                return RedirectToAction("Accesscontrol");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [Authorize]
        public async Task<JsonResult> webAccessStatusChange(WebAccessPermission webaccessInfo, int Status)
        {
            try
            {
                webaccessInfo.Updated_By = Convert.ToString(User.FindFirst("empId").Value);
                //foreach (var checkId in webaccessInfo.Checkboxid)
                //{
                string result = await _employeeSI.EmployeeWebAccessPermissionStatusUpdate(Status, webaccessInfo);
                //}
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }

        }

        [Authorize]
        public async Task<JsonResult> globalAccessStatusChange(GlobalIPInfo globalaccessInfo, int Status)
        {
            try
            {
                globalaccessInfo.Updated_By = Convert.ToString(User.FindFirst("empId").Value);
                string result = await _employeeSI.GlobalWebAccessPermissionStatusUpdate(Status, globalaccessInfo);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }

        }

        public async Task<JsonResult> GetIpMappedEmployee(int WebAccressId)
        {
            try
            {
                Task<List<SelectListItem>> getemployeenamelist = _employeeSI.GetEmployeeMappedWebList(WebAccressId, 0);
                Task<List<SelectListItem>> getemployeenamelistById = _employeeSI.GetEmployeeMappedWebList(WebAccressId, 1);
                await Task.WhenAll(getemployeenamelist, getemployeenamelistById);
                var vm = new EmployeeAccessVM
                {
                    getEmployeeNameSL = getemployeenamelist.Result,
                    getEmployeeNameSLbyID = getemployeenamelistById.Result,
                };
                return Json(vm);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        public async Task<JsonResult> GetIpMappedWhitelistEmployee()
        {
            try
            {
                Task<List<SelectListItem>> getemployeenamelist = _employeeSI.GetWhitelistEmployeeMappedWebList(0);
                Task<List<SelectListItem>> getemployeenamelistById = _employeeSI.GetWhitelistEmployeeMappedWebList(1);
                await Task.WhenAll(getemployeenamelist, getemployeenamelistById);
                var vm = new EmployeeAccessVM
                {
                    getEmployeeNameSL = getemployeenamelist.Result,
                    getEmployeeNameSLbyID = getemployeenamelistById.Result,
                };
                return Json(vm);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        public async Task<JsonResult> GetAllEmployee()
        {
            try
            {
                Task<List<SelectListItem>> getemployeenamelist = _employeeSI.GetEmployeeName_SL();
                await Task.WhenAll(getemployeenamelist);
                var vm = new EmployeeAccessVM
                {
                    getEmployeeNameSL = getemployeenamelist.Result
                };
                return Json(vm);
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
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _employeeSI.DeleteUser(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> ValidateWebAccessIP(string WhoisIp)
        {
            try
            {
                string result = await _employeeSI.ValidateWebAccessIp(WhoisIp);
                if (result != "")
                {
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        public async Task<JsonResult> ValidateGlobalIp(string GlobalIP)
        {
            try
            {
                string result = await _employeeSI.ValidateWebAccessGlobalIp(GlobalIP);
                if (result != "")
                {
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        public  PartialViewResult _layout()
        {
            try
            {

                TempData["userType"]= Convert.ToString(User.FindFirst("userType").Value);
                TempData["branch"] = Convert.ToString(User.FindFirst("branch").Value);

                return PartialView();
            }
            catch (Exception e)
            {
                return PartialView();
            }

        }

        public async Task<JsonResult> CheckUniqueEmailId(string EmailId)
        {
            try
            {
                var empId = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _employeeSI.CheckUniqueEmailId(EmailId);
                return Json(new Message<string> { IsSuccess = true, ReturnMessage = "success", Data = Convert.ToString(result) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> CheckUniqueContactNo(string phoneNo)
        {
            try
            {
                var empId = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _employeeSI.CheckUniqueContactNo(phoneNo);
                return Json(new Message<string> { IsSuccess = true, ReturnMessage = "success", Data = Convert.ToString(result) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> CheckUniqueloginId(string loginId)
        {
            try
            {
                var empId = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _employeeSI.CheckUniqueloginId(loginId);
                return Json(new Message<string> { IsSuccess = true, ReturnMessage = "success", Data = Convert.ToString(result) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }


        [HttpGet]
        public async Task<JsonResult> ResendVerificationEmail(string empId)
        {
            try
            {
                Employee info = await _employeeSI.GetEmployeeListbyid(empId);
                if (info.Status != "InComplete Registration")
                    throw new SetupPasswordException(SetupPasswordException.EmailVerified, "User is already verified.");

                string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                await _employeeSI.ResendEmailVerificationAsync(info, baseUrl);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success.", Data = null });
            }
            catch (SetupPasswordException spe) when (spe.ErrorCode == SetupPasswordException.EmailVerified || spe.ErrorCode == SetupPasswordException.Emailconfiguration)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = spe.Message, Data = null });
            }
            catch (Exception /* ex */)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }

        public async Task<JsonResult> ResetPassword(string empId, string password)
        {
            try
            {
                var Modified_By = Convert.ToString(User.FindFirst("empId").Value);
                await _employeeSI.CompleteAccountRecoveryAsync(empId, password, Modified_By);
                return Json(new Message<List<SelectListItem>>() { IsSuccess = true, ReturnMessage = "success", Data = null });
            }
            catch (ResetPasswordException rpe) when (rpe.ErrorCode == ResetPasswordException.PasswordMatchesPreviousPasswords)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Password matches previous passwords.", Data = null });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }


        public async Task<JsonResult> CheckUniqueEmailIdTest(string EmailId, int Id)
        {
            try
            {
                var empId = Convert.ToString(User.FindFirst("empId").Value);
                var result = await _employeeSI.CheckUniqueEmailIdTest(EmailId, Id);
                return Json(new Message<string> { IsSuccess = true, ReturnMessage = "success", Data = Convert.ToString(result) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }


        public PartialViewResult _layout3()
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                BaseViewModel vm = new BaseViewModel();
                vm.GetHubList = _employeeSI.GetHublist().Result;
                return PartialView(vm);
            }
            catch
            {
                return PartialView("");
            }
        }




    }
}