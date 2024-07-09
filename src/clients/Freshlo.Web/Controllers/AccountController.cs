using Freshlo.Common.Exceptions.Service;
using Freshlo.Common.Exceptions.Services;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Employee;
using Freshlo.SI;
using Freshlo.Web.Models;
using Freshlo.Web.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Freshlo.Web.Controllers
{
    public class AccountController : Controller
    {
        private ISystemConfigSI _systemConfigService { get; }
        public ISettingSI settingSI  { get; set; }
        private readonly IActionContextAccessor _accessor;
        public AccountController(IEmployeeSI employeeSI, ISystemConfigSI systemConfigService, IActionContextAccessor accessor, ISettingSI _settingSI)
        {
            _employeeSI = employeeSI;
            _systemConfigService = systemConfigService;
            _accessor = accessor;
            settingSI = _settingSI;
        }
        private IEmployeeSI _employeeSI { get; }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            try
            {
                ChangePasswordViewMoel vm = new ChangePasswordViewMoel();
                var empId = Convert.ToString(User.FindFirst("empId").Value);
                Employee info = await _employeeSI.GetSecurityInfoAsync(empId, null);
                vm.EmailAddress = info.EmailId;
                vm.OldPassword = info.Password;
                vm.SecurityConfig = await _systemConfigService.GetSecurityConfigAsync();
                if (TempData["ErrorMessage"] != null)
                    vm.ErrorMessage = TempData["ErrorMessage"] as string;
                vm.businessInfo = settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordPost(ChangePasswordViewMoel vm)
        {
            // Fetch user ID from authentication cookie
            var empId = Convert.ToString(User.FindFirst("empId").Value);
            SecurityConfig securityConfig = null;
            try
            {
                securityConfig = await _systemConfigService.GetSecurityConfigAsync();
                await _employeeSI.ChangePasswordAsync(empId, vm.OldPassword, vm.NewPassword);
                TempData["ViewMessage"] = " Change Password Successfully.";
                return RedirectToAction("Login", "Account");
            }
            catch (ChangePasswordException cpe) when (cpe.ErrorCode == ChangePasswordException.IncorrectPassword)
            {
                TempData["ErrorMessage"] = "Incorrect old password.";
                return RedirectToAction("ChangePassword", "Account");
            }
            catch (ChangePasswordException cpe) when (cpe.ErrorCode == ChangePasswordException.PasswordMatchesPreviousPasswords)
            {
                #region Populate View Model
                Employee info = await _employeeSI.GetSecurityInfoAsync(empId, null);
                vm.EmailAddress = info.EmailId;
                #endregion
                vm.ErrorMessage = cpe.Message;
                vm.SecurityConfig = securityConfig;
                return View(vm);
            }
            catch (Exception /* ex */)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ForgetPasswordViewMoel vm = new ForgetPasswordViewMoel();
            if (TempData["ViewMessage"] != null)
                vm.ViewMessage = TempData["ViewMessage"] as string;

            if (TempData["ErrorMessage"] != null)
                vm.ErrorMessage = TempData["ErrorMessage"] as string;
            vm.businessInfo = settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = vm.businessInfo.hotel_name;
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return View(vm);
        }

        [HttpPost]
        [ActionName("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string userID,string empid)
        {
            ForgetPasswordViewMoel vm = new ForgetPasswordViewMoel();
            try
            {
                string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                await _employeeSI.IntiateAccountRecoveryAsync(userID, baseUrl);
                TempData["ViewMessage"] = "Account recovery initiated. Please check email.";
                return RedirectToAction("ChangePassword", "Account");
            }
            catch (ForgotPasswordException fpe)
            {
                if (fpe.ErrorCode == ForgotPasswordException.EmailAddressNotRegistered)
                    vm.ErrorMessage = "Email Address Not Registered";
                else if (fpe.ErrorCode == ForgotPasswordException.AccountNotVerified)
                    vm.ErrorMessage = "Account Not Verified";
                else if (fpe.ErrorCode == ForgotPasswordException.AccountDeactivated)
                    vm.ErrorMessage = "Account Deactivated";
                return View(vm);
            }
            catch (Exception /* ex */)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Login(LoginViewModel vm)
        {
            /*
             * If already authenticated,
             * redirect to appropriate default page
             * for that user role.
             */
            if (User.Identities.Any(u => u.IsAuthenticated))
            {
                HttpContext.Response.Cookies.Append("BranchId", User.FindFirst("branch").Value);
                return RedirectToLocal(vm.ReturnUrl, vm.UserRole);
            }
            /*
             * Populate ViewModel's ViewMessage 
             * & ViewModel's ErrorMessage from TempData (if any)
             */
            if (TempData["ViewMessage"] != null)
                vm.ViewMessage = TempData["ViewMessage"] as string;

            if (TempData["ErrorMessage"] != null)
                vm.ErrorMessage = TempData["ErrorMessage"] as string;
            vm.businessInfo = settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = vm.businessInfo.hotel_name;
            ViewBag.logoUrl = vm.businessInfo.logo_url;
            return View(vm);
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel vm)
        {
            Employee employee = null;
            try
            {
                SecurityConfig securityConfig = await _systemConfigService.GetSecurityConfigAsync();
                employee = await _employeeSI.ValidateLoginAsync(vm.EmailId, vm.Password);
                List<Claim> claims = new List<Claim>
                {
                    new Claim("id",employee.id.ToString(),ClaimValueTypes.Integer32),
                    new Claim("empId",employee.EmpId,ClaimValueTypes.String),
                    new Claim("fullName",employee.FullName,ClaimValueTypes.String),
                    new Claim("emailId",employee.EmailId,ClaimValueTypes.String),
                    new Claim("loginId",employee.LoginId,ClaimValueTypes.String),
                    new Claim("userRole",employee.UserRole,ClaimValueTypes.String),
                    new Claim("branch",employee.Branch,ClaimValueTypes.String),
                    new Claim("userType",employee.UserType,ClaimValueTypes.String),
                    new Claim("isfirstLogin",employee.IsfirstLogin.ToString(),ClaimValueTypes.Integer32),
                    new Claim("LastLogin",employee.LastLogin.ToString(),ClaimValueTypes.DateTime),
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(securityConfig.Session_Expiry_Hours)
                };
                if(employee.id == 500)
                {
                    vm.businessInfo = settingSI.GetbusinessInfoDetails(0);
                    ViewBag.businessName = vm.businessInfo.hotel_name;
                    ViewBag.logoUrl = vm.businessInfo.logo_url;
                    vm.ErrorMessage = "User Access Declined Please Contact to Your Admin";
                    return View(vm);
                }
                if ( employee.Status == "Inactive")
                {
                    vm.ErrorMessage = "User not active";
                    return View(vm);
                }
                else
                {
                    if (claims[8].Value == "0" || claims[8].Value == "1")
                    {
                        return RedirectToAction("SetutpPassword", "Account", new { employeeId = employee.EmpId });
                    }
                    else
                    {
                        /*
                        * Task 1: Complete login process
                        * (add authentication cookie in response)
                        * Task 2: Log successful login attempt in database
                        */
                        await Task.WhenAll(
                            HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity),
                               authProperties),
                            _employeeSI.LogSuccessfulLoginAsync(employee.id));
                        vm.UserRole = employee.UserRole;
                        HttpContext.Response.Cookies.Append("BranchId", employee.Branch);
                        //return RedirectToLocal(vm.ReturnUrl, vm.UserRole);
                        var result = await GetWhoisIP(employee.EmpId);

                        if (result == "")
                        {
                            return View(vm);
                        }
                        else
                        {
                            return RedirectToLocal(vm.ReturnUrl, vm.UserRole);
                        }
                    }
                }

               
            }
            catch (ResourceNotFoundException)
            {
                vm.businessInfo = settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                vm.ErrorMessage = "User ID or password do not match";
                return View(vm);
            }
            catch (LoginException le)
            {
                vm.businessInfo = settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;        
                vm.ErrorMessage = le.Message;
                return View(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
                TempData["ViewMessage"] = "You have successfully logged out";
                RemoveCookie("BranchId");
                return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        public IActionResult LockedScreen()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string otp)
        {
            ResetPasswordViewModel vm = new ResetPasswordViewModel();
            try
            {
                Employee info = await _employeeSI.ProcessAccountRecoveryAsync(otp);
                vm.Otp = otp;
                vm.EmailAddress = info.EmailId;
                vm.UserId = info.EmpId;
                vm.SecurityConfig = await _systemConfigService.GetSecurityConfigAsync();
                vm.businessInfo = settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch (ResetPasswordException /* rpe */)
            {
                return StatusCode(400);
            }
            catch (Exception /* ex */)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel data)
        {
            var modifiedby = "";
            if (data != null)
            {
                modifiedby = data.UserId;   
            }
            else
            {
                modifiedby = Convert.ToString(User.FindFirst("empId").Value);
            }
            ResetPasswordViewModel vm = new ResetPasswordViewModel();
            Employee info = null;
            SecurityConfig securityConfig = null;
            try
            {
                securityConfig = await _systemConfigService.GetSecurityConfigAsync();
                info = await _employeeSI.ProcessAccountRecoveryAsync(data.Otp);
                await _employeeSI.CompleteAccountRecoveryAsync(info.EmpId, data.NewPassword, modifiedby);
                TempData["ViewMessage"] = "Password reset successfully. Please login using new password.";
                return RedirectToAction("ChangePassword", "Account");
            }
            catch (ResetPasswordException rpe) when (rpe.ErrorCode == ResetPasswordException.PasswordMatchesPreviousPasswords)
            {
                vm.ErrorMessage = rpe.Message;
                vm.Otp = data.Otp;
                vm.EmailAddress = info.EmailId;
                vm.UserId = info.EmpId;
                vm.SecurityConfig = securityConfig;
                return View(vm);
            }
            catch (ResetPasswordException /* rpe */)
            {
                return StatusCode(400);
            }
            catch (Exception /* ex */)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SetutpPassword(string employeeId)
        {
            try
            {
                SetupPasswordViewModel vm = new SetupPasswordViewModel();
                var empId = "";
                if (employeeId != null)
                {
                    empId = employeeId;
                }
                else
                {
                    empId = Convert.ToString(User.FindFirst("empId").Value);
                }
                Employee info = await _employeeSI.GetSecurityInfoAsync(empId, null);
                vm.EmailAddress = info.EmailId;
                vm.EmpId = info.EmpId;
                vm.SecurityConfig = await _systemConfigService.GetSecurityConfigAsync();
                if (TempData["ErrorMessage"] != null)
                    vm.ErrorMessage = TempData["ErrorMessage"] as string;
                vm.businessInfo = settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetupPassword(SetupPasswordViewModel data)
        {
            try
            {
                var empId = data.EmpId;
                await _employeeSI.SetPasswordAsync(empId, data.NewPassword);
                TempData["ViewMessage"] = "Password set successfully.";
                return RedirectToAction("Login", "Account");
            }
            catch (ResetPasswordException rpe) when (rpe.ErrorCode == ResetPasswordException.PasswordMatchesPreviousPasswords)
            {
                TempData["id"] = data.EmpId;
                TempData["ErrorMessage"] = rpe.Message;
                return RedirectToAction("SetupPassword", "Account");
            }
            catch (Exception /* ex */)
            {
                return StatusCode(500);
            }
        }
        #region Private methods


        private IActionResult RedirectToLocal(string returnUrl, string UserRole)
        {
            if (Url.IsLocalUrl(returnUrl) && (UserRole == "System Admin" || UserRole == "Management"))
            {
                return Redirect(returnUrl);
            }
            if (UserRole == "System Admin" /*|| UserRole == "Management" || UserRole == "Branch Manager"*/)
            {
                return RedirectToAction("ChooseBranch", "Account");
            }
            if (UserRole == "Branch Management")
            {
                return RedirectToAction("PendingOrders", "Sale");
            }
            if (UserRole == "Kitchen Manager" || UserRole == "Cook")
            {
                return RedirectToAction("kitchenOrderList", "Sale");
            }
            if (UserRole == "Steward" || UserRole == "Cashier")
            {
                return RedirectToAction("Financial", "Dashboard");
            }
            else
            {
                return RedirectToAction("PendingOrders", "Sale");
            }

        }
        #endregion
        #region web access
        public async Task<string> GetNetIP()
        {
            var ip = "";
            ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
            return ip;
        }
        #endregion
        #region web access
        //public async Task<string> GetWhoisIP(string employeeId)
        //{
        //    var returnmessage = "";
        //    var ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
        //    returnmessage = await _employeeSI.GetGlobalWebAccessIp(ip);
        //    if (returnmessage == "")
        //    {
        //        returnmessage = await _employeeSI.GetWebAccessIp(ip, employeeId);
        //        if (returnmessage == "")
        //        {
        //            returnmessage = await _employeeSI.GetWhitelistEmployee(employeeId);
        //        }
        //    }
        //    return returnmessage;
        //}
        #endregion
        private async Task<IActionResult> RedirectToLogin(string returnUrl)
        {
            await HttpContext.SignOutAsync(
               CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["ViewMessage"] = "Access Denied. Contact Your Admin.";
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string otp)
        {
            Employee info = null;
            try
            {
                info = await _employeeSI.ProcessAccountRecoveryAsync(otp);
                TempData["uaid"] = info.id;                
                TempData["Prompt"] = "First login, please setup password";
                return RedirectToAction("SetutpPassword", "Account", new { employeeId = info.EmpId });
            }
            catch (ResourceNotFoundException e)
            {
                return StatusCode(404);
            }
            catch (SetupPasswordException spe) when (spe.ErrorCode == SetupPasswordException.ProcessCompleted)
            {
                TempData["ViewMessage"] = spe.Message;
                return RedirectToAction("Login", "Account");
            }
            catch (SetupPasswordException spe) when (spe.ErrorCode == SetupPasswordException.EmailVerified)
            {
                TempData["ViewMessage"] = spe.Message;
                TempData["uaid"] = spe.info.id;
                TempData["Prompt"] = "First login, please set password";
                return RedirectToAction("SetupPassword", "Account");
            }
            catch (SetupPasswordException spe) when (spe.ErrorCode == SetupPasswordException.OtpExpired
                || spe.ErrorCode == SetupPasswordException.InvalidOtp)
            {
                //Redirect to error message page here
                TempData["ErrorMessage"] = spe.Message;
                return StatusCode(500);
            }
            catch (Exception /* ex */)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChooseBranch()
        {
            try
            {
                BaseViewModel vm = new BaseViewModel();
                if (TempData["ViewMessage"] != null)
                    vm.ViewMessage = TempData["ViewMessage"] as string;

                if (TempData["ErrorMessage"] != null)
                    vm.ErrorMessage = TempData["ErrorMessage"] as string;
                vm.businessInfo = settingSI.GetbusinessInfoDetails(0);
                vm.GetHubList = _employeeSI.GetHublist().Result;
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void RemoveCookie(string key)
        {
            //Erase the data in the cookie
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(-1);
            option.Secure = true;
            option.IsEssential = true;
            Response.Cookies.Append(key, string.Empty, option);
            //Then delete the cookie
            Response.Cookies.Delete(key);
        }
        public async Task<string> GetWhoisIP(string employeeId)
        {
            var returnmessage = "";
            WhosIpInfo ipInfo = new WhosIpInfo();
            var ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
            returnmessage = await _employeeSI.GetGlobalWebAccessIp(ip);
            if (returnmessage == "")
            {
                returnmessage = await _employeeSI.GetWebAccessIp(ip, employeeId);
                if (returnmessage == "")
                {
                    returnmessage = await _employeeSI.GetWhitelistUser(employeeId);
                }
            }
            if (returnmessage == "")
            {
                var insertnetip = await _employeeSI.InserNetwebIP(ip, employeeId, 0);
            }
            else
            {
                var insertnetip1 = await _employeeSI.InserNetwebIP(ip, employeeId, 1);
            }
            return returnmessage;
        }
    }
}