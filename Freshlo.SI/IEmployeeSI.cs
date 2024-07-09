using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Employee;
using Freshlo.DomainEntities.Hub;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface IEmployeeSI
    {
        // User Related Here...
        Task<Employee> ValidateLoginAsync(string emailAddress, string password);
        Task<int> LogSuccessfulLoginAsync(int id);
        int CreateorUpdateUser(Employee userdata);
        int CreateEmployee(Employee userdata);
        int UpdateEmployee(Employee userdata);
        Task<List<Employee>> GetEmployeeList(string Branch, string role);
        Task<List<Employee>> GetEmployeeListuser(string Branch, string role,string Id);
        Task<Employee> GetEmployeeListbyid(string Empid);
        Task<List<Employee>> GetVendorListByName();
        Task<List<Employee>> GetUserRole();
        Task<bool> DeleteUser(int id);

        // Access Level Related Here...
        Task<List<SelectListItem>> GetEmployeeName_SL();
        Task<List<WebAccessPermission>> GetEmployeeWebAccessList();
        Task<int> EmployeeWebAccessPermissionCreate(WebAccessPermission webAccessinfo);
        Task<int> EmployeeWebAccessPermissionUpdate(WebAccessPermission webAccessinfo);
        Task<string> EmployeeWebAccessPermissionStatusUpdate(int Status, WebAccessPermission webaccessInfo);
        Task<string> GetWebAccessIp(string WhoisIp, string EmployeeId);
        Task<int> InserNetwebIP(string Ip, string EmployeeId,int Status);
        Task<int> GlobalPermissionCreate(GlobalIPInfo globalipinfo);
        Task<int> GlobalPermissionUpdate(GlobalIPInfo globalipinfo);
        Task<List<GlobalIPInfo>> GetGlobalAccessList();
        Task<string> GlobalWebAccessPermissionStatusUpdate(int Status, GlobalIPInfo globalaccessInfo);
        Task<List<SelectListItem>> GetEmployeeMappedWebList(int WebAccessId,int Condition);
        Task<string> GetGlobalWebAccessIp(string WhoisIp);
        Task<List<WebAccessInfoLog>> GetWebAccessLogList();
        Task<List<Hub>> GetHublist();
        Task<string> GetWhitelistEmployee(string EmployeeId);
        Task<string> ValidateWebAccessIp(string WhoisIp);
        Task<string> ValidateWebAccessGlobalIp(string GlobalIp);
        Task<int> EmployeeWhitelistWebAccessCreate(WebAccessPermission webAccessinfo);
        Task<List<SelectListItem>> GetWhitelistEmployeeMappedWebList(int Condition);

        // User Account Setup and Password Related Here...
        Task<Employee> GetSecurityInfoAsync(string empid, string EmpId);
        Task ChangePasswordAsync(string empid, string oldPassword, string newPassword);
        Task IntiateAccountRecoveryAsync(string emailAddress, string baseUrl);
        Task<Employee> ProcessAccountRecoveryAsync(string otp);
        Task CompleteAccountRecoveryAsync(string empid, string newPassword, string Modifiedby);
        Task SetPasswordAsync(string empid, string password);
        Task<string> CheckUniqueEmailId(string EmailId);
        Task<string> CheckUniqueEmailIdTest(string EmailId, int Id);
        Task<string> CheckUniqueContactNo(string phoneNo);
        Task<string> CheckUniqueloginId(string loginId);
        Task InitiateEmailVerificationAsync(Employee info, string baseUrl);
        Task ResendEmailVerificationAsync(Employee info, string baseUrl);
        Task<string> GetWhitelistUser(string employeeId);
    }
}