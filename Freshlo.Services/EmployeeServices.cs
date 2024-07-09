using Freshlo.Common.Exceptions.Service;
using Freshlo.Common.Exceptions.Services;
using Freshlo.Common.Helpers.EmailHelper;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Employee;
using Freshlo.DomainEntities.Hub;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class EmployeeServices :IEmployeeSI
    {
        public EmployeeServices(IEmployeeRI employeeRI, ISystemConfigRI systemConfigRI)
        {
            _employeeRI = employeeRI;
            _systemConfigRepository = systemConfigRI;
        }

        // User Related Here...
        private IEmployeeRI _employeeRI { get; }
        private ISystemConfigRI _systemConfigRepository { get; }
        public Employee ValidateLogin(string emailaddress,string password)
        {            
            Employee employee = _employeeRI.GetEmployeeInfo(emailaddress);
            if (employee == null)
                throw new ResourceNotFoundException("User not found.");
            if (!employee.Password.Equals(password))
            {
                _employeeRI.IncrementLoginAttempts(employee.id);
                if (employee.Attempt >= 5)
                {
                    throw new LoginException(LoginException.ExceededLoginAttempts, "You have exceeded maximum no. of login attempts");
                }
                else
                {
                    throw new LoginException(LoginException.ExceededLoginAttempts, "Email Id or Password is incorrect");
                }
            }
            return employee;
        }
        public Task<Employee> ValidateLoginAsync(string EmailAddress,string password)
        {
            return Task.Run(() =>
            {
                return ValidateLogin(EmailAddress,password);
            });
        }
        public int LogSuccessfulLogin(int id)
        {
            return _employeeRI.LogSuccessfulLogin(id);
        }
        public Task<int> LogSuccessfulLoginAsync(int id)
        {
            return Task.Run(() =>
            {
                return LogSuccessfulLogin(id);
            });


        }
        public int CreateorUpdateUser(Employee userdata)
        {
            return _employeeRI.CreateorUpdateUser(userdata);
        }
        public int CreateEmployee(Employee userdata)
        {
            return _employeeRI.CreateEmployee(userdata);
        }
        public int UpdateEmployee(Employee userdata)
        {
            return _employeeRI.UpdateEmployee(userdata);
        }
        public async Task<List<Employee>> GetEmployeeList(string Branch,string role)
        {
            return await _employeeRI.GetEmployeeList(Branch,role);
        }
        public async Task<List<Employee>> GetEmployeeListuser(string Branch,string role,string Id)
        {
            return await _employeeRI.GetEmployeeListuser(Branch,role,Id);
        }    
        public async Task<Employee> GetEmployeeListbyid(string Empid)
        {
            return await _employeeRI.GetEmployeeListbyid(Empid);
        }
        public Task<List<Employee>> GetVendorListByName()
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetVendorListByName();
            });
        }
        public Task<List<Employee>> GetUserRole()
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetUserRoles();
            });
        }
        public Task<bool> DeleteUser(int id)
        {
            return Task.Run(() => {
                return _employeeRI.DeleteUser(id);
            });
        }
        public Task<List<SelectListItem>> GetEmployeeName_SL()
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetEmployeeName_SL();
            });
        }
        public Task<List<WebAccessPermission>> GetEmployeeWebAccessList()
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetEmployeeWebAccessList();
            });
        }
        public Task<int> EmployeeWebAccessPermissionCreate(WebAccessPermission webAccessinfo)
        {
            return Task.Run(() =>
            {
                return _employeeRI.EmployeeWebAccessPermissionCreate(webAccessinfo);
            });
        }
        public Task<int> EmployeeWebAccessPermissionUpdate(WebAccessPermission webAccessinfo)
        {
            return Task.Run(() =>
            {
                return _employeeRI.EmployeeWebAccessPermissionUpdate(webAccessinfo);
            });
        }
        public Task<string> EmployeeWebAccessPermissionStatusUpdate(int Status, WebAccessPermission webaccessInfo)
        {
            return Task.Run(() =>
            {
                return _employeeRI.EmployeeWebAccessPermissionStatusUpdate(Status, webaccessInfo);
            });
        }
        public Task<string> GetWebAccessIp(string WhoisIp, string EmployeeId)
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetWebAccessIp(WhoisIp, EmployeeId);
            });
        }
        public Task<int> InserNetwebIP(string Ip, string EmployeeId,int Status)
        {
            return Task.Run(() =>
            {
                return _employeeRI.InserNetwebIP(Ip, EmployeeId, Status);
            });
        }
        public Task<int> GlobalPermissionCreate(GlobalIPInfo globalipinfo)
        {
            return Task.Run(() =>
            {
                return _employeeRI.GlobalPermissionCreate(globalipinfo);
            });
        }
        public Task<int> GlobalPermissionUpdate(GlobalIPInfo globalipinfo)
        {
            return Task.Run(() =>
            {
                return _employeeRI.GlobalPermissionUpdate(globalipinfo);
            });
        }
        public Task<List<GlobalIPInfo>> GetGlobalAccessList()
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetGlobalAccessList();
            });
        }
        public Task<string> GlobalWebAccessPermissionStatusUpdate(int Status, GlobalIPInfo globalaccessInfo)
        {
            return Task.Run(() =>
            {
                return _employeeRI.GlobalWebAccessPermissionStatusUpdate(Status, globalaccessInfo);
            });
        }
        public Task<List<SelectListItem>> GetEmployeeMappedWebList(int WebAccessId,int Condition)
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetEmployeeMappedWebList(WebAccessId, Condition);
            });
        }
        public Task<string> GetGlobalWebAccessIp(string WhoisIp)
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetGlobalWebAccessIp(WhoisIp);
            });
        }
        public Task<List<WebAccessInfoLog>> GetWebAccessLogList()
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetWebAccessLogList();
            });
        }
        public Task<List<Hub>> GetHublist()
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetHublist();
            });
        }
        public Task<string> GetWhitelistEmployee(string EmployeeId)
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetWhitelistEmployee(EmployeeId);
            });
        }
        public Task<string> ValidateWebAccessIp(string WhoisIp)
        {
            return Task.Run(() =>
            {
                return _employeeRI.ValidateWebAccessIp(WhoisIp);
            });
        }
        public Task<string> ValidateWebAccessGlobalIp(string GlobalIp)
        {
            return Task.Run(() =>
            {
                return _employeeRI.ValidateWebAccessGlobalIp(GlobalIp);
            });
        }
        public Task<int> EmployeeWhitelistWebAccessCreate(WebAccessPermission webAccessinfo)
        {
            return Task.Run(() =>
            {
                return _employeeRI.EmployeeWhitelistWebAccessCreate(webAccessinfo);
            });
        }
        public Task<List<SelectListItem>> GetWhitelistEmployeeMappedWebList(int Condition)
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetWhitelistEmployeeMappedWebList(Condition);
            });
        }
        public Employee GetSecurityInfo(string empid, string EmpId)
        {
            Employee info = _employeeRI.GetSecurityInfo(empid, EmpId);
            if (info == null)
                return null;
            return info;
        }
        public Task<Employee> GetSecurityInfoAsync(string empid, string EmpId)
        {
            return Task.Run(() =>
            {
                return GetSecurityInfo(empid, EmpId);
            });
        }
        public void ChangePassword(string empid, string oldPassword, string newPassword)
        {
            Employee info = _employeeRI.GetSecurityInfo(empid, null);
            SecurityConfig config =  _systemConfigRepository.GetSecurityConfig();           
            _employeeRI.ChangePassword(empid, newPassword);
        }
        public Task ChangePasswordAsync(string empid, string oldPassword, string newPassword)
        {
            return Task.Run(() => ChangePassword(empid, oldPassword, newPassword));
        }
        public void IntiateAccountRecovery(string userID, string baseUrl)
        {
            Employee info = _employeeRI.GetSecurityInfo(null, userID);
            if (null == info)
                throw new ForgotPasswordException(ForgotPasswordException.EmailAddressNotRegistered);
           
            Emailconfig config = _systemConfigRepository.GetEmailConfig();
            EmailSetting emailSettings = new EmailSetting(
                config.UserName,
                config.Password,
                config.OutgoingMainServer,
                Convert.ToInt32(config.OutgoingPort));
            Email email = new Email();
            email.Sender = config.EmailAddress;
            email.Recipient = info.EmailId;
            email.Subject = "Password Recovery";
            string otp = GenerateOtp();
            string encryptedData = GenerateEncryptedData(info.EmpId.ToString(), otp, DateTime.UtcNow.ToString("yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            _employeeRI.UpdateOtp(info.id, otp);
            email.Body = GenerateEmailBody(baseUrl, info, encryptedData);
            email.IsBodyHtml = true;
            email.Send(emailSettings);
        }
        public Task IntiateAccountRecoveryAsync(string emailAddress, string baseUrl)
        {
            return Task.Run(() =>
            {
                IntiateAccountRecovery(emailAddress, baseUrl);
            });
        }
        private string GenerateOtp()
        {
            StringBuilder sbOtp = new StringBuilder();
            string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random rand = new Random();
            for (int i = 0; i < 8; i++)
            {
                sbOtp.Append(allowedChars[rand.Next(0, allowedChars.Length)]);
            }
            return sbOtp.ToString();
        }
        private string GenerateEncryptedData(params string[] data)
        {
            StringBuilder sbData = new StringBuilder();
            foreach (var param in data)
                sbData.Append(param).Append(";");
            sbData.Length--;
            return (sbData.ToString());
        }
        private string GenerateEmailBody(string baseUrl, Employee info, string encryptedData)
        {
            string regLink = new StringBuilder()
                .Append(baseUrl)
                .Append("/Account/ResetPassword?otp=")
                .Append(encryptedData)
                .ToString();
            #region Email Template
            StringBuilder body = new StringBuilder();
            body.Append(@"<html>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <title>Reset Password | Soul & Soul </title>

    <style type='text/css'>
        /* Take care of image borders and formatting, client hacks */
        img {
            max-width: 600px;
            outline: none;
            text-decoration: none;
            -ms-interpolation-mode: bicubic;
        }

        a img {
            border: none;
        }

        table {
            border-collapse: collapse !important;
        }

        #outlook a {
            padding: 0;
        }

        .ReadMsgBody {
            width: 100%;
        }

        .ExternalClass {
            width: 100%;
        }

        .backgroundTable {
            margin: 0 auto;
            padding: 0;
            width: 100% !important;
        }

        table td {
            border-collapse: collapse;
        }

        .ExternalClass * {
            line-height: 115%;
        }

        .container-for-gmail-android {
            min-width: 600px;
        }


        /* General styling */
        * {
            font-family: Helvetica, Arial, sans-serif;
        }

        body {
            -webkit-font-smoothing: antialiased;
            -webkit-text-size-adjust: none;
            width: 100% !important;
            margin: 0 !important;
            height: 100%;
            color: #676767;
        }

        td {
            font-family: Helvetica, Arial, sans-serif;
            font-size: 16px;
            color: #777777;
            text-align: center;
            line-height: 21px;
        }

        a {
            color: #676767;
            text-decoration: none !important;
        }

        .pull-left {
            text-align: left;
        }

        .pull-right {
            text-align: right;
        }


        .header-lg,
        .header-md,
        .header-sm {
            font-size: 32px;
            font-weight: 700;
            line-height: normal;
            padding: 35px 0 0;
            color: #4d4d4d;
        }

        .header-md {
            font-size: 22px;
            font-weight: 600;
        }

        .header-sm {
            padding: 5px 0;
            font-size: 18px;
            line-height: 1.3;
        }

        .content-padding {
            padding: 20px 0 30px;
        }

        .mobile-header-padding-right {
            width: 290px;
            text-align: right;
            padding-left: 10px;
        }

        .mobile-header-padding-left {
            width: 290px;
            text-align: left;
            padding-left: 10px;
        }

        .free-text {
            width: 100% !important;
            padding: 10px 60px 0px;
        }

        .block-rounded {
            border-radius: 5px;
            border: 1px solid #e5e5e5;
            vertical-align: top;
        }

        .button {
            padding: 30px 0;
        }

        .info-block {
            padding: 0 20px;
            width: 260px;
        }

        .block-rounded {
            width: 260px;
        }

        .info-img {
            width: 258px;
            border-radius: 5px 5px 0 0;
        }

        .force-width-gmail {
            min-width: 600px;
            height: 0px !important;
            line-height: 1px !important;
            font-size: 1px !important;
        }

        .button-width {
            width: 228px;
        }

        .pt-10 {
            padding-top: 10px
        }
    </style>

    <style type='text/css' media='screen'>
        @import url(http://fonts.googleapis.com/css?family=Oxygen:400,700);
    </style>

    <style type='text/css' media='screen'>
        @media screen {

            /* Thanks Outlook 2013! */
            * {
                font-family: 'Oxygen', 'Helvetica Neue', 'Arial', 'sans-serif' !important;
            }
        }
    </style>

    <style type='text/css' media='only screen and (max-width: 480px)'>
        /* Mobile styles */
        @media only screen and (max-width: 480px) {

            table[class*='container-for-gmail-android'] {
                min-width: 290px !important;
                width: 100% !important;
            }

            table[class='w320'] {
                width: 320px !important;
            }

            img[class='force-width-gmail'] {
                display: none !important;
                width: 0 !important;
                height: 0 !important;
            }

            a[class='button-width'],
            a[class='button-mobile'] {
                width: 248px !important;
            }

            td[class*='mobile-header-padding-left'] {
                width: 160px !important;
                padding-left: 0 !important;
            }

            td[class*='mobile-header-padding-right'] {
                width: 160px !important;
                padding-right: 0 !important;
            }

            td[class='header-lg'] {
                font-size: 24px !important;
                padding-bottom: 5px !important;
            }

            td[class='header-md'] {
                font-size: 18px !important;
                padding-bottom: 5px !important;
            }

            td[class='content-padding'] {
                padding: 5px 0 30px !important;
            }

            td[class='button'] {
                padding: 5px !important;
            }

            td[class*='free-text'] {
                padding: 10px 18px 30px !important;
            }

            td[class='info-block'] {
                display: block !important;
                width: 280px !important;
                padding-bottom: 40px !important;
            }

            td[class='info-img'],
            img[class='info-img'] {
                width: 278px !important;
            }
        }
    </style>
</head>

<body bgcolor='#f7f7f7'>
    <table align='center' cellpadding='0' cellspacing='0' class='container-for-gmail-android' width='100%'>
         <tr>
            <td align='center' valign='top' width='100%' style='background-color: #f7f7f7;' class='content-padding'>
                <center>
                    <table cellspacing='0' cellpadding='0' width='600' class='w320'>
                        <tr>
                            <td class='header-lg'>
                                Hi ");
            body.Append(info.FullName);
            body.Append(@"</td>
                        </tr>
                        <tr>
                            <td class='free-text'>
                                You received this email because Soul & Soul received a password reset request.
                            </td>
                        </tr>
                        <tr>
                            <td class='free-text'>
                                Click the button below to reset your password. 
                            </td>
                        </tr>
                        <tr>
                            <td class='button'>
                                <div>
                 <!--[if mso]>
                <v:roundrect xmlns:v='urn:schemas-microsoft-com:vml' xmlns:w='urn:schemas-microsoft-com:office:word' 
                href='");
            body.Append(regLink);
            body.Append(@"'style='height:45px;v-text-anchor:middle;width:155px;' arcsize='15%' strokecolor='#ffffff' fillcolor='#ff6f6f'>
                <w:anchorlock/><center style='color:#ffffff;font-family:Helvetica, Arial, sans-serif;font-size:14px;font-weight:bold;'>Reset Password</center>
                </v:roundrect>
              <![endif]-->                                    
              <a class='button-mobile' href='");
            body.Append(regLink);
            body.Append(@"'style='background-color:#ff6f6f;border-radius:5px;color:#ffffff;display:inline-block;font-weight:bold;line-height:45px;text-align:center;text-decoration:none;width:155px;-webkit-text-size-adjust:none;mso-hide:all;'>Reset Password</a></div>
                            </td>
                        </tr>
                        <tr>
                            <td class='free-text' style='padding-top: 0;padding-bottom: 20px;'>
                               If the above button did not work, please click on link below.
                            </td>
                        </tr>
                        <tr>
                            <td class='free-text' style='padding-top: 0;padding-bottom: 20px;'>
                               Or, copy and paste the link  in your browser 
                            </td>
                        </tr>
                        <tr>
                            <td class='free-text' style='padding-top: 0;padding-bottom: 20px;'>
                                <a href='#' style='font-size: 14px; text-decoration: :none; color:#202bb1;'>");
            body.Append(regLink);
            body.Append(@"</td>
                        </tr>
                       <tr>
                            <td class='free-text' style='padding-top: 0'>
                                If you did not  request to reset your password, no further action is required. 
                            </td>
                        </tr>
                        <tr>
                            <td class='free-text' style=' text-align: left;padding-top: 20px;'>
                                Thanks and Regards,
                                <span style='display: block'>Soul & Soul Team</span>
                            </td>
                        </tr>
                    </table>
                </center>
            </td>
        </tr>
    </table>
</body>
</html>");
            #endregion
             return body.ToString();
        }
        private string[] RetrieveData(string encryptedData)
        {
            return (encryptedData).Split(';');
        }   
        public Employee ProcessAccountRecovery(string otp)
        {
            string[] otpData = RetrieveData(otp);
            DateTime otpGenerateTime = DateTime.ParseExact(otpData[2], "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime currentTime = DateTime.UtcNow;
            TimeSpan timeDifference = currentTime - otpGenerateTime;
            if (timeDifference.TotalMinutes > 30)
                throw new ResetPasswordException(ResetPasswordException.OtpExpired, "Otp has expired. Please try again.");

            Employee info = _employeeRI.GetSecurityInfo((otpData[0]), null);
            if (info == null)
                throw new ResourceNotFoundException();
            if (!(info.Status == "InComplete Registration" || info.Password != null))
                throw new SetupPasswordException(SetupPasswordException.ProcessCompleted, "Email verification & Password setup already completed.");
            //if (info.Status != "InComplete Registration")
            //{
            //    SetupPasswordException spe = new SetupPasswordException(SetupPasswordException.EmailVerified, "Email already verified.");
            //    spe.info = info;
            //    throw spe;
            //}
            if (!info.ResetOTP.Equals(otpData[1]))
                throw new ResetPasswordException(ResetPasswordException.OtpDoesNotMatch, "Otp does not match. Please use latest generated OTP.");
            return info;
        }
        public Task<Employee> ProcessAccountRecoveryAsync(string otp)
        {
            return Task.Run(() =>
            {
                return ProcessAccountRecovery(otp);
            });
        }
        public void CompleteAccountRecovery(string empid, string newPassword, string Modifiedby)
        {
            Employee info = _employeeRI.GetSecurityInfo(empid, null);
            SecurityConfig config = _systemConfigRepository.GetSecurityConfig();        
            _employeeRI.CompleteAccountRecovery(empid, newPassword, Modifiedby);
        }
        public Task CompleteAccountRecoveryAsync(string empid, string newPassword, string Modifiedby)
        {
            return Task.Run(() =>
            {
                CompleteAccountRecovery(empid, newPassword, Modifiedby);
            });
        }
        public void SetPassword(string empid, string password)
        {
            SecurityConfig config = _systemConfigRepository.GetSecurityConfig();
            Employee info = GetSecurityInfo(empid, null);        
            _employeeRI.ChangePassword(info.EmpId, password);
        }
        public Task SetPasswordAsync(string empid, string password)
        {
            return Task.Run(() =>
            {
                SetPassword(empid, password);
            });
        }
        public Task<string> CheckUniqueEmailId(string EmailId)
        {
            return Task.Run(() =>
            {
                return _employeeRI.CheckUniqueEmailId(EmailId);
            });
        }
        public Task<string> CheckUniqueEmailIdTest(string EmailId, int Id)
        {
            return Task.Run(() =>
            {
                return _employeeRI.CheckUniqueEmailIdTest(EmailId, Id);
            });
        }
        public Task<string> CheckUniqueContactNo(string phoneNo)
        {
            return Task.Run(() =>
            {
                return _employeeRI.CheckUniqueContactNo(phoneNo);
            });
        }
        public Task<string> CheckUniqueloginId(string loginId)
        {
            return Task.Run(() =>
            {
                return _employeeRI.CheckUniqueloginId(loginId);
            });
        }
        public void InitiateEmailVerification(Employee info, string baseUrl)
        {
            Emailconfig config = _systemConfigRepository.GetEmailConfig();

            EmailSetting emailSettings = new EmailSetting(
                config.UserName,
                config.Password,
                config.OutgoingMainServer,
                Convert.ToInt32(config.OutgoingPort));

            Email email = new Email();
            email.Sender = config.EmailAddress;
            email.Recipient = info.EmailId;
            email.Subject = "Email Verification";

            string otp = GenerateOtp();
            string encryptedData = GenerateEncryptedData(info.id.ToString(), otp, DateTime.UtcNow.ToString("yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));

            _employeeRI.UpdateOtp(info.id, otp);

            email.Body = GenerateVerificationEmailBody(baseUrl, encryptedData, info);
            email.IsBodyHtml = true;
            try
            {
                email.Send(emailSettings);
            }
            catch
            {
            }
        }
        public Task InitiateEmailVerificationAsync(Employee info, string baseUrl)
        {
            return Task.Run(() =>
            {
                InitiateEmailVerification(info, baseUrl);
            });
        }
        private string GenerateVerificationEmailBody(string baseUrl, string encryptedData, Employee info)
        {

            #region Email's body
            StringBuilder body = new StringBuilder(
                @"<html>
    <head>
        <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
        <meta name='viewport' content='width=device-width, initial-scale=1' />
        <title>Account Verification Email |Soul & Soul </title>

        <style type='text/css'>
            /* Take care of image borders and formatting, client hacks */
            img {
                max-width: 600px;
                outline: none;
                text-decoration: none;
                -ms-interpolation-mode: bicubic;
            }

            a img {
                border: none;
            }

            table {
                border-collapse: collapse !important;
            }

            #outlook a {
                padding: 0;
            }

            .ReadMsgBody {
                width: 100%;
            }

            .ExternalClass {
                width: 100%;
            }

            .backgroundTable {
                margin: 0 auto;
                padding: 0;
                width: 100% !important;
            }

            table td {
                border-collapse: collapse;
            }

            .ExternalClass * {
                line-height: 115%;
            }

            .container-for-gmail-android {
                min-width: 600px;
            }


            /* General styling */
            * {
                font-family: Helvetica, Arial, sans-serif;
            }

            body {
                -webkit-font-smoothing: antialiased;
                -webkit-text-size-adjust: none;
                width: 100% !important;
                margin: 0 !important;
                height: 100%;
                color: #676767;
            }

            td {
                font-family: Helvetica, Arial, sans-serif;
                font-size: 16px;
                color: #777777;
                text-align: center;
                line-height: 21px;
            }

            a {
                color: #676767;
                text-decoration: none !important;
            }

            .pull-left {
                text-align: left;
            }

            .pull-right {
                text-align: right;
            }


            .header-lg,
            .header-md,
            .header-sm {
                font-size: 32px;
                font-weight: 700;
                line-height: normal;
                padding: 35px 0 0;
                color: #4d4d4d;
            }

            .header-md {
                font-size: 22px;
                font-weight: 600;
            }

            .header-sm {
                padding: 5px 0;
                font-size: 18px;
                line-height: 1.3;
            }

            .content-padding {
                padding: 20px 0 30px;
            }

            .mobile-header-padding-right {
                width: 290px;
                text-align: right;
                padding-left: 10px;
            }

            .mobile-header-padding-left {
                width: 290px;
                text-align: left;
                padding-left: 10px;
            }

            .free-text {
                width: 100% !important;
                padding: 10px 60px 0px;
            }

            .block-rounded {
                border-radius: 5px;
                border: 1px solid #e5e5e5;
                vertical-align: top;
            }

            .button {
                padding: 30px 0;
            }

            .info-block {
                padding: 0 20px;
                width: 260px;
            }

            .block-rounded {
                width: 260px;
            }

            .info-img {
                width: 258px;
                border-radius: 5px 5px 0 0;
            }

            .force-width-gmail {
                min-width: 600px;
                height: 0px !important;
                line-height: 1px !important;
                font-size: 1px !important;
            }

            .button-width {
                width: 228px;
            }

            .pt-10 {
                padding-top: 10px
            }
</style>

        <style type='text/css' media='screen'>
            @import url(http://fonts.googleapis.com/css?family=Oxygen:400,700);
</style>

        <style type='text/css' media='screen'>
            @media screen {

                /* Thanks Outlook 2013! */
                * {
                    font-family: 'Oxygen', 'Helvetica Neue', 'Arial', 'sans-serif' !important;
                }
            }
</style>

        <style type='text/css' media='only screen and (max-width: 480px)'>
            /* Mobile styles */
            @media only screen and (max-width: 480px) {

                table[class*='container-for-gmail-android'] {
                    min-width: 290px !important;
                    width: 100% !important;
                }

                table[class='w320'] {
                    width: 320px !important;
                }

                img[class='force-width-gmail'] {
                    display: none !important;
                    width: 0 !important;
                    height: 0 !important;
                }

                a[class='button-width'],
                a[class='button-mobile'] {
                    width: 248px !important;
                }

                td[class*='mobile-header-padding-left'] {
                    width: 160px !important;
                    padding-left: 0 !important;
                }

                td[class*='mobile-header-padding-right'] {
                    width: 160px !important;
                    padding-right: 0 !important;
                }

                td[class='header-lg'] {
                    font-size: 24px !important;
                    padding-bottom: 5px !important;
                }

                td[class='header-md'] {
                    font-size: 18px !important;
                    padding-bottom: 5px !important;
                }

                td[class='content-padding'] {
                    padding: 5px 0 30px !important;
                }

                td[class='button'] {
                    padding: 5px !important;
                }

                td[class*='free-text'] {
                    padding: 10px 18px 30px !important;
                }

                td[class='info-block'] {
                    display: block !important;
                    width: 280px !important;
                    padding-bottom: 40px !important;
                }

                td[class='info-img'],
                img[class='info-img'] {
                    width: 278px !important;
                }
            }
</style>
    </head>
    <body bgcolor='#f7f7f7'>
        <table align='center' cellpadding='0' cellspacing='0' class='container-for-gmail-android' width='100%'>            
            <tr>
                <td align='center' valign='top' width='100%' style='background-color: #f7f7f7;' class='content-padding'>
                    <center>
                        <table cellspacing='0' cellpadding='0' width='600' class='w320'>
                            <tr>
                                <td class='header-lg'>
                                    Welcome to Soul & Soul
                                </td>
                            </tr>
                            <tr>
                                <td class='header-md pt-10'>
");
            body.Append("Hi @Name, your Login ID is <span style='color: #ff6f6f;'>'".Replace("@Name", info.FullName));
            body.Append(info.LoginId);
            body.Append(@"'</span>
                                </td>
                            </tr>
                            <tr>
                                <td class='free-text'>
                                     First, please confirm your account by clicking the button below. 
                                </td>
                            </tr>
                            <tr>
                                <td class='button'>
                                    <div>
                                          <!--[if mso]>
                                          <v:roundrect xmlns:v='urn:schemas-microsoft-com:vml' xmlns:w='urn:schemas-microsoft-com:office:word' href='");
            body.Append(baseUrl).Append("/Account/VerifyEmail?otp=");
            body.Append(encryptedData);
            body.Append(@"'style='height:45px;v-text-anchor:middle;width:155px;' arcsize='15%' strokecolor='#040000' fillcolor='#ff6f6f'>
                                          <w:anchorlock/>
                                          <center style='color:#ffffff;font-family:Helvetica, Arial, sans-serif;font-size:14px;font-weight:bold;'>Confirm Account</center>
                                          <![endif]-->
                                          </v:roundrect>
                             <a class='button-mobile' href='");
            body.Append(baseUrl).Append("/Account/VerifyEmail?otp=");
            body.Append(encryptedData);
            body.Append(@"' style ='border-radius:5px;color:#ffffff;display:inline-block;font-size:14px;font-weight:bold;line-height:45px;text-align:center;text-decoration:none;width:155px;-webkit-text-size-adjust:none;mso-hide:all;background-color:#ff6f6f;'>
                       Confirm Account</a>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class='free-text' style='padding-top: 0;padding-bottom: 20px;'>
                                   If the above button didn't work, please click on this link or copy and paste it in your browser
                                </td>
                            </tr>
                            <tr>
                                <td class='free-text' style='padding-top: 0;padding-bottom: 20px;'>
                                    <a href='");
            body.Append(baseUrl).Append("/Account/VerifyEmail?otp=");
            body.Append(encryptedData);
            body.Append(@"'style ='font-size: 14px; text-decoration: :none; color:#202bb1;'>");
            body.Append(baseUrl).Append("/Account/VerifyEmail?otp="); body.Append(encryptedData);
            body.Append(@"</a>
                                </td>
                            </tr>
                            <tr>
                                <td class='free-text' style='padding-top: 0'>
                                    If you have any questions, do not reply to this email.
                                </td>
                            </tr>
                            <tr>
                                <td class='free-text' style='padding-top: 6'>
                                    Please send your questions to  <a href='#' style='color: #202bb1;'>noreply@automatebuddy.com</a>
                                </td>
                            </tr>
                            <tr>
                                <td class='free-text' style='padding-top: 6'>
                                    We are always happy to help.
                                </td>
                            </tr>
                            <tr>
                                <td class='free-text' style=' text-align: left;padding-top: 20px;'>
                                    Thanks and Regards,
                                    </br>
                                    <span style='display: block'>Soul & Soul Team</span>
                                </td>
                            </tr>
                        </table>
                    </center>
                </td>
            </tr>
        </table>        
    </body>
    </html>"
);
            #endregion
            return body.ToString();
        }
        public void ResendEmailVerification(Employee info, string baseUrl)
        {
            Emailconfig config = _systemConfigRepository.GetEmailConfig();

            EmailSetting emailSettings = new EmailSetting(
                config.UserName,
                config.Password,
                config.OutgoingMainServer,
                Convert.ToInt32(config.OutgoingPort));

            Email email = new Email();
            email.Sender = config.EmailAddress;
            email.Recipient = info.EmailId;
            email.Subject = "Email Verification";

            string otp = GenerateOtp();
            string encryptedData = GenerateEncryptedData(info.id.ToString(), otp, DateTime.UtcNow.ToString("yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));

            _employeeRI.UpdateOtp(info.id, otp);

            email.Body = GenerateVerificationEmailBody(baseUrl, encryptedData, info);
            email.IsBodyHtml = true;
            try
            {
                email.Send(emailSettings);
            }
            catch (Exception ex)
            {
                throw new SetupPasswordException(SetupPasswordException.Emailconfiguration, "Server Down Email Not Send.");
            }
        }
        public Task ResendEmailVerificationAsync(Employee info, string baseUrl)
        {
            return Task.Run(() =>
            {
                ResendEmailVerification(info, baseUrl);
            });
        }
        public Task<string> GetWhitelistUser(string employeeId)
        {
            return Task.Run(() =>
            {
                return _employeeRI.GetWhitelistUser(employeeId);
            });
        }
    }
}
