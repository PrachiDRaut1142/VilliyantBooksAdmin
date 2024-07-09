using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Employee;
using Freshlo.DomainEntities.Hub;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.RI
{
    public interface IEmployeeRI
    {
        // User Level Here...
        Employee GetEmployeeInfo(string emailaddress);
        int LogSuccessfulLogin(int id);
        int IncrementLoginAttempts(int uaid);
        int CreateorUpdateUser(Employee userdata);
        int CreateEmployee(Employee userdata);
        int UpdateEmployee(Employee userdata);
        Task<List<Employee>> GetEmployeeList(string branch,string role);
        Task<List<Employee>> GetEmployeeListuser(string branch,string role,string Id);
        Task<Employee> GetEmployeeListbyid(string Empid);
        List<Employee> GetVendorListByName();
        List<Employee> GetUserRoles();
        bool DeleteUser(int id);

        // Access Level Here...
        List<SelectListItem> GetEmployeeName_SL();
        List<WebAccessPermission> GetEmployeeWebAccessList();
        int EmployeeWebAccessPermissionCreate(WebAccessPermission webAccessinfo);
        int EmployeeWebAccessPermissionUpdate(WebAccessPermission webAccessinfo);
        string EmployeeWebAccessPermissionStatusUpdate(int Status, WebAccessPermission webaccessInfo);
        string GetWebAccessIp(string WhoisIp, string EmployeeId);
        int InserNetwebIP(string Ip, string EmployeeId,int Status);
        int GlobalPermissionCreate(GlobalIPInfo globalipinfo);
        int GlobalPermissionUpdate(GlobalIPInfo globalipinfo);
        List<GlobalIPInfo> GetGlobalAccessList();
        string GlobalWebAccessPermissionStatusUpdate(int Status, GlobalIPInfo globalaccessInfo);
        List<SelectListItem> GetEmployeeMappedWebList(int WebAccessId,int Condition);
        string GetGlobalWebAccessIp(string WhoisIp);
        List<WebAccessInfoLog> GetWebAccessLogList();
    
        List<Hub> GetHublist();
        string GetWhitelistEmployee(string EmployeeId);
        string ValidateWebAccessIp(string WhoisIp);
        string ValidateWebAccessGlobalIp(string GlobalIp);
        int EmployeeWhitelistWebAccessCreate(WebAccessPermission webAccessinfo);
        List<SelectListItem> GetWhitelistEmployeeMappedWebList(int Condition);


        // Account setup and psswrd here...

        Employee GetSecurityInfo(string empid, string EmpId);
        void ChangePassword(string empid, string newPassword);
        void UpdateOtp1(string empid, string otp);

        void UpdateOtp(int empid, string otp);

        void CompleteAccountRecovery(string empid, string newPassword, string Modifiedby);
        string CheckUniqueEmailId(string EmailId);
        string CheckUniqueEmailIdTest(string EmailId,int id);
        string CheckUniqueContactNo(string phoneNo);
        string CheckUniqueloginId(string loginId);
        string GetWhitelistUser(string UserIemployeeIdd);

    }
}
