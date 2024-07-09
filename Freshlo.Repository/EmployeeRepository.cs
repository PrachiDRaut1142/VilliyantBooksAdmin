using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Employee;
using Freshlo.DomainEntities.Hub;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Repository
{
    public class EmployeeRepository : IEmployeeRI
    {
        public EmployeeRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        private IDbConfig _dbConfig { get; }


        // User Related Here...

        public Employee GetEmployeeInfo(string emailAddress)
        {
            Employee employee = null;

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_GetEmployeeByEmailAddress]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@emailAddress", SqlDbType.VarChar, 255).Value = emailAddress;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        employee = new Employee
                        {
                            id = rd["id"] == DBNull.Value ? 0: Convert.ToInt32(rd["id"]),
                            EmpId = rd["EmpId"] == DBNull.Value ? "na" : Convert.ToString(rd["EmpId"]),
                            UserType = rd["UserType"] == DBNull.Value ? "na" : Convert.ToString(rd["UserType"]),
                            FullName = rd["FullName"] == DBNull.Value ? "na" : Convert.ToString(rd["FullName"]),
                            ContactNo = rd["ContactNo"] == DBNull.Value ? "na" : Convert.ToString(rd["ContactNo"]),
                            EmailId = rd["EmailId"] == DBNull.Value ? "na" : Convert.ToString(rd["EmailId"]),
                            UserRole = rd["UserRole"] == DBNull.Value ? "na" : Convert.ToString(rd["UserRole"]),
                            AdhaarNo = rd["AdhaarNo"] == DBNull.Value ? "na" : Convert.ToString(rd["AdhaarNo"]),
                            PanNo =     rd["PanNo"] == DBNull.Value ? "na" : Convert.ToString(rd["PanNo"]),
                            Address1 = rd["Address1"] == DBNull.Value ? "na" : Convert.ToString(rd["Address1"]),
                            Address2 = rd["Address2"] == DBNull.Value ? "na" : Convert.ToString(rd["Address2"]),
                            city = rd["city"] == DBNull.Value ? "na" : Convert.ToString(rd["city"]),
                            state = rd["state"] == DBNull.Value ? "na" : Convert.ToString(rd["state"]),
                            country = rd["country"] == DBNull.Value ? "na" : Convert.ToString(rd["country"]),
                            Password = rd["Password"] == DBNull.Value ? null : Convert.ToString(rd["Password"]),
                            Password1 = rd["Password1"] == DBNull.Value ? null : Convert.ToString(rd["Password1"]),
                            Password2 = rd["Password2"] == DBNull.Value ? null : Convert.ToString(rd["Password2"]),
                            Password3 = rd["Password3"] == DBNull.Value ? null : Convert.ToString(rd["Password3"]),
                            LastLogin = Convert.ToDateTime(rd["LastLogin"] == DBNull.Value ? "01-01-2001": rd["LastLogin"]),
                            PasswordChangeDate = Convert.ToDateTime(rd["PasswordChangeDate"]),
                            Attempt =rd["Attempt"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Attempt"]),
                            Status = rd["Status"] == DBNull.Value ? "na" : Convert.ToString(rd["Status"]),
                            ResetOTP = rd["ResetOTP"] == DBNull.Value ? "na" : Convert.ToString(rd["ResetOTP"]),
                            CreatedBy = rd["CreatedBy"] == DBNull.Value ? "na" : Convert.ToString(rd["CreatedBy"]),
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            LastUpdatedBy = rd["LastUpdatedBy"] == DBNull.Value ? "na" : Convert.ToString(rd["LastUpdatedBy"]),
                            LastUpdatedOn = Convert.ToDateTime(rd["LastUpdatedOn"]),
                            PartnerType = rd["PartnerType"] == DBNull.Value ? "na" : Convert.ToString(rd["PartnerType"]),
                            Branch = rd["Branch"] == DBNull.Value ? "na" : Convert.ToString(rd["Branch"]),
                            IsfirstLogin = rd["IsfirstLogin"] == DBNull.Value ? 0 : Convert.ToInt32(rd["IsfirstLogin"]),
                            LoginId = rd["LoginId"] == DBNull.Value ? "na" : Convert.ToString(rd["LoginId"]),
                        };
                    }

                    return employee;
                }
            }
        }
        public int CreateorUpdateUser(Employee userdata)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Employee_CreateorUpdateEmployee]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = userdata.id;
                        cmd.Parameters.Add("@UserType", SqlDbType.VarChar, 100).Value = userdata.UserType == null ? (object)DBNull.Value : userdata.UserType;
                        cmd.Parameters.Add("@FullName", SqlDbType.VarChar, 100).Value = userdata.FullName == null ? (object)DBNull.Value : userdata.FullName;
                        cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar, 20).Value = userdata.ContactNo == null ? (object)DBNull.Value : userdata.ContactNo;
                        cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = userdata.EmailId == null ? (object)DBNull.Value : userdata.EmailId;
                        cmd.Parameters.Add("@UserRole", SqlDbType.VarChar, 50).Value = userdata.UserRole == null ? (object)DBNull.Value : userdata.UserRole;
                        cmd.Parameters.Add("@AdhaarNo", SqlDbType.VarChar, 50).Value = userdata.AdhaarNo == null ? (object)DBNull.Value : userdata.AdhaarNo;
                        cmd.Parameters.Add("@PanNo", SqlDbType.VarChar, 50).Value = userdata.PanNo == null ? (object)DBNull.Value : userdata.PanNo;
                        cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 200).Value = userdata.Address1 == null ? (object)DBNull.Value : userdata.Address1;
                        cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 200).Value = userdata.Address2 == null ? (object)DBNull.Value : userdata.Address2;
                        cmd.Parameters.Add("@city", SqlDbType.VarChar, 50).Value = userdata.city == null ? (object)DBNull.Value : userdata.city;
                        cmd.Parameters.Add("@state", SqlDbType.VarChar, 50).Value = userdata.state == null ? (object)DBNull.Value : userdata.state;
                        cmd.Parameters.Add("@country", SqlDbType.VarChar, 50).Value = userdata.country == null ? (object)DBNull.Value : userdata.country;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = userdata.Status == null ? (object)DBNull.Value : userdata.Status;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = userdata.CreatedBy == null ? (object)DBNull.Value : userdata.CreatedBy;
                        cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 50).Value = userdata.LastUpdatedBy == null ? (object)DBNull.Value : userdata.LastUpdatedBy;
                        cmd.Parameters.Add("@PartnerType", SqlDbType.VarChar, 50).Value = userdata.PartnerType == null ? (object)DBNull.Value : userdata.PartnerType;
                        cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 30).Value = userdata.Branch == null ? (object)DBNull.Value : userdata.Branch;
                        cmd.Parameters.Add("@IsfirstLogin", SqlDbType.Int).Value = userdata.IsfirstLogin;
                        cmd.Parameters.Add("@LoginId", SqlDbType.VarChar,50).Value = userdata.LoginId;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }

                }
                catch (Exception)
                {

                    throw;
                }
        }

        public int CreateEmployee(Employee userdata)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_EmployeeCreate]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserType", SqlDbType.VarChar, 100).Value = userdata.UserType == null ? (object)DBNull.Value : userdata.UserType;
                        cmd.Parameters.Add("@FullName", SqlDbType.VarChar, 100).Value = userdata.FullName == null ? (object)DBNull.Value : userdata.FullName;
                        cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar, 20).Value = userdata.ContactNo == null ? (object)DBNull.Value : userdata.ContactNo;
                        cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = userdata.EmailId == null ? (object)DBNull.Value : userdata.EmailId;
                        cmd.Parameters.Add("@UserRole", SqlDbType.VarChar, 50).Value = userdata.UserRole == null ? (object)DBNull.Value : userdata.UserRole;
                        cmd.Parameters.Add("@AdhaarNo", SqlDbType.VarChar, 50).Value = userdata.AdhaarNo == null ? (object)DBNull.Value : userdata.AdhaarNo;
                       // cmd.Parameters.Add("@PanNo", SqlDbType.VarChar, 50).Value = userdata.PanNo == null ? (object)DBNull.Value : userdata.PanNo;
                        cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 200).Value = userdata.Address1 == null ? (object)DBNull.Value : userdata.Address1;
                        cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 200).Value = userdata.Address2 == null ? (object)DBNull.Value : userdata.Address2;
                        cmd.Parameters.Add("@city", SqlDbType.VarChar, 50).Value = userdata.city == null ? (object)DBNull.Value : userdata.city;
                        //cmd.Parameters.Add("@state", SqlDbType.VarChar, 50).Value = userdata.state == null ? (object)DBNull.Value : userdata.state;
                        cmd.Parameters.Add("@country", SqlDbType.VarChar, 50).Value = userdata.country == null ? (object)DBNull.Value : userdata.country;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = userdata.Status == null ? (object)DBNull.Value : userdata.Status;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = userdata.CreatedBy == null ? (object)DBNull.Value : userdata.CreatedBy;
                        cmd.Parameters.Add("@PartnerType", SqlDbType.VarChar, 50).Value = userdata.PartnerType == null ? (object)DBNull.Value : userdata.PartnerType;
                        cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 30).Value = userdata.Branch == null ? (object)DBNull.Value : userdata.Branch;
                        cmd.Parameters.Add("@IsfirstLogin", SqlDbType.Int).Value = userdata.IsfirstLogin;
                        cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 50).Value = userdata.LoginId;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
        }

        public int UpdateEmployee(Employee userdata)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_EmployeeUpdate]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = userdata.id;
                        cmd.Parameters.Add("@UserType", SqlDbType.VarChar, 100).Value = userdata.UserType == null ? (object)DBNull.Value : userdata.UserType;
                        cmd.Parameters.Add("@FullName", SqlDbType.VarChar, 100).Value = userdata.FullName == null ? (object)DBNull.Value : userdata.FullName;
                        cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar, 20).Value = userdata.ContactNo == null ? (object)DBNull.Value : userdata.ContactNo;
                        cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = userdata.EmailId == null ? (object)DBNull.Value : userdata.EmailId;
                        cmd.Parameters.Add("@UserRole", SqlDbType.VarChar, 50).Value = userdata.UserRole == null ? (object)DBNull.Value : userdata.UserRole;
                        cmd.Parameters.Add("@AdhaarNo", SqlDbType.VarChar, 50).Value = userdata.AdhaarNo == null ? (object)DBNull.Value : userdata.AdhaarNo;
                        //cmd.Parameters.Add("@PanNo", SqlDbType.VarChar, 50).Value = userdata.PanNo == null ? (object)DBNull.Value : userdata.PanNo;
                        cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 200).Value = userdata.Address1 == null ? (object)DBNull.Value : userdata.Address1;
                        cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 200).Value = userdata.Address2 == null ? (object)DBNull.Value : userdata.Address2;
                        cmd.Parameters.Add("@city", SqlDbType.VarChar, 50).Value = userdata.city == null ? (object)DBNull.Value : userdata.city;
                        cmd.Parameters.Add("@state", SqlDbType.VarChar, 50).Value = userdata.state == null ? (object)DBNull.Value : userdata.state;
                        cmd.Parameters.Add("@country", SqlDbType.VarChar, 50).Value = userdata.country == null ? (object)DBNull.Value : userdata.country;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = userdata.Status == null ? (object)DBNull.Value : userdata.Status;
                        cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 50).Value = userdata.LastUpdatedBy == null ? (object)DBNull.Value : userdata.LastUpdatedBy;
                        cmd.Parameters.Add("@PartnerType", SqlDbType.VarChar, 50).Value = userdata.PartnerType == null ? (object)DBNull.Value : userdata.PartnerType;
                        cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 30).Value = userdata.Branch == null ? (object)DBNull.Value : userdata.Branch;
                        cmd.Parameters.Add("@IsfirstLogin", SqlDbType.Int).Value = userdata.IsfirstLogin;
                        cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 50).Value = userdata.LoginId;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
        }

        public int LogSuccessfulLogin(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_LogSuccessfulLogin]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public int IncrementLoginAttempts(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_IncrementLoginAttempts]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public async Task<List<Employee>> GetEmployeeList(string branch, string role)
        {
            branch = role == "System Admin" ? null : branch;
            var employeelist = new List<Employee>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Employee_GetEmployeeList]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 100).Value = branch;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (rd.Read())
                    {
                        employeelist.Add(new Employee
                        {
                            id = Convert.ToInt32(rd["id"]),
                            EmpId = Convert.ToString(rd["EmpId"]),
                            UserType = rd["UserType"] == DBNull.Value ? null : Convert.ToString(rd["UserType"]),
                            FullName = rd["FullName"] == DBNull.Value ? null : Convert.ToString(rd["FullName"]),
                            ContactNo = rd["ContactNo"] == DBNull.Value ? null : Convert.ToString(rd["ContactNo"]),
                            EmailId = rd["EmailId"] == DBNull.Value ? null : Convert.ToString(rd["EmailId"]),
                            UserRole = rd["UserRole"] == DBNull.Value ? null : Convert.ToString(rd["UserRole"]),
                            AdhaarNo = rd["AdhaarNo"] == DBNull.Value ? null : Convert.ToString(rd["AdhaarNo"]),
                            PanNo = rd["PanNo"] == DBNull.Value ? null : Convert.ToString(rd["PanNo"]),
                            Address1 = rd["Address1"] == DBNull.Value ? null : Convert.ToString(rd["Address1"]),
                            Address2 = rd["Address2"] == DBNull.Value ? null : Convert.ToString(rd["Address2"]),
                            city = rd["city"] == DBNull.Value ? null : Convert.ToString(rd["city"]),
                            state = rd["state"] == DBNull.Value ? null : Convert.ToString(rd["state"]),
                            country = rd["country"] == DBNull.Value ? null : Convert.ToString(rd["country"]),
                            Password = rd["Password"] == DBNull.Value ? null : Convert.ToString(rd["Password"]),
                            Password1 = rd["Password1"] == DBNull.Value ? null : Convert.ToString(rd["Password1"]),
                            Password2 = rd["Password2"] == DBNull.Value ? null : Convert.ToString(rd["Password2"]),
                            Password3 = rd["Password3"] == DBNull.Value ? null : Convert.ToString(rd["Password3"]),
                            LastLogin = Convert.ToDateTime(rd["LastLogin"]),
                            PasswordChangeDate = Convert.ToDateTime(rd["PasswordChangeDate"]),
                            Attempt = Convert.ToInt32(rd["Attempt"]),
                            Status = rd["Status"] == DBNull.Value ? null : Convert.ToString(rd["Status"]),
                            ResetOTP = Convert.ToString(rd["ResetOTP"]),
                            CreatedBy = rd["CreatedBy"] == DBNull.Value ? null : Convert.ToString(rd["CreatedBy"]),
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            LastUpdatedBy = rd["LastUpdatedBy"] == DBNull.Value ? null : Convert.ToString(rd["LastUpdatedBy"]),
                            LastUpdatedOn = Convert.ToDateTime(rd["LastUpdatedOn"]),
                            PartnerType = Convert.ToString(rd["PartnerType"]),
                            Branch = rd["Branch"] == DBNull.Value ? null : Convert.ToString(rd["Branch"]),
                            usercreatedname = rd[29] == DBNull.Value ? null : Convert.ToString(rd[29]),

                        });
                    }
                    return employeelist;
                }
            }
        } public async Task<List<Employee>> GetEmployeeListuser(string branch, string role,string Id)
        {
            branch = role == "System Admin" ? null : branch;
            var employeelist = new List<Employee>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Employee_GetEmployeeListUser]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 100).Value = branch;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 100).Value = Id;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (rd.Read())
                    {
                        employeelist.Add(new Employee
                        {
                            id = Convert.ToInt32(rd["id"]),
                            EmpId = Convert.ToString(rd["EmpId"]),
                            UserType = rd["UserType"] == DBNull.Value ? null : Convert.ToString(rd["UserType"]),
                            FullName = rd["FullName"] == DBNull.Value ? null : Convert.ToString(rd["FullName"]),
                            ContactNo = rd["ContactNo"] == DBNull.Value ? null : Convert.ToString(rd["ContactNo"]),
                            EmailId = rd["EmailId"] == DBNull.Value ? null : Convert.ToString(rd["EmailId"]),
                            UserRole = rd["UserRole"] == DBNull.Value ? null : Convert.ToString(rd["UserRole"]),
                            AdhaarNo = rd["AdhaarNo"] == DBNull.Value ? null : Convert.ToString(rd["AdhaarNo"]),
                            PanNo = rd["PanNo"] == DBNull.Value ? null : Convert.ToString(rd["PanNo"]),
                            Address1 = rd["Address1"] == DBNull.Value ? null : Convert.ToString(rd["Address1"]),
                            Address2 = rd["Address2"] == DBNull.Value ? null : Convert.ToString(rd["Address2"]),
                            city = rd["city"] == DBNull.Value ? null : Convert.ToString(rd["city"]),
                            state = rd["state"] == DBNull.Value ? null : Convert.ToString(rd["state"]),
                            country = rd["country"] == DBNull.Value ? null : Convert.ToString(rd["country"]),
                            Password = rd["Password"] == DBNull.Value ? null : Convert.ToString(rd["Password"]),
                            Password1 = rd["Password1"] == DBNull.Value ? null : Convert.ToString(rd["Password1"]),
                            Password2 = rd["Password2"] == DBNull.Value ? null : Convert.ToString(rd["Password2"]),
                            Password3 = rd["Password3"] == DBNull.Value ? null : Convert.ToString(rd["Password3"]),
                            LastLogin = Convert.ToDateTime(rd["LastLogin"]),
                            PasswordChangeDate = Convert.ToDateTime(rd["PasswordChangeDate"]),
                            Attempt = Convert.ToInt32(rd["Attempt"]),
                            Status = rd["Status"] == DBNull.Value ? null : Convert.ToString(rd["Status"]),
                            ResetOTP = Convert.ToString(rd["ResetOTP"]),
                            CreatedBy = rd["CreatedBy"] == DBNull.Value ? null : Convert.ToString(rd["CreatedBy"]),
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            LastUpdatedBy = rd["LastUpdatedBy"] == DBNull.Value ? null : Convert.ToString(rd["LastUpdatedBy"]),
                            LastUpdatedOn = Convert.ToDateTime(rd["LastUpdatedOn"]),
                            PartnerType = Convert.ToString(rd["PartnerType"]),
                            Branch = rd["Branch"] == DBNull.Value ? null : Convert.ToString(rd["Branch"]),
                            usercreatedname = rd[29] == DBNull.Value ? null : Convert.ToString(rd[29]),

                        });
                    }
                    return employeelist;
                }
            }
        }
        public async Task<Employee> GetEmployeeListbyid(string Empid)
        {
            Employee employeelist = null;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Employee_GetEmployeeListById]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Empid", SqlDbType.Int).Value = Empid;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    if (rd.Read())
                    {
                        employeelist = new Employee
                        {
                            id = Convert.ToInt32(rd["id"]),
                            EmpId = Convert.ToString(rd["EmpId"]),
                            UserType = rd["UserType"] == DBNull.Value ? null : Convert.ToString(rd["UserType"]),
                            FullName = rd["FullName"] == DBNull.Value ? null : Convert.ToString(rd["FullName"]),
                            ContactNo = rd["ContactNo"] == DBNull.Value ? null : Convert.ToString(rd["ContactNo"]),
                            EmailId = rd["EmailId"] == DBNull.Value ? null : Convert.ToString(rd["EmailId"]),
                            UserRole = rd["UserRole"] == DBNull.Value ? null : Convert.ToString(rd["UserRole"]),
                            AdhaarNo = rd["AdhaarNo"] == DBNull.Value ? null : Convert.ToString(rd["AdhaarNo"]),
                            PanNo = rd["PanNo"] == DBNull.Value ? null : Convert.ToString(rd["PanNo"]),
                            Address1 = rd["Address1"] == DBNull.Value ? null : Convert.ToString(rd["Address1"]),
                            Address2 = rd["Address2"] == DBNull.Value ? null : Convert.ToString(rd["Address2"]),
                            city = rd["city"] == DBNull.Value ? null : Convert.ToString(rd["city"]),
                            state = rd["state"] == DBNull.Value ? null : Convert.ToString(rd["state"]),
                            country = rd["country"] == DBNull.Value ? null : Convert.ToString(rd["country"]),
                            Password = rd["Password"] == DBNull.Value ? null : Convert.ToString(rd["Password"]),
                            Password1 = rd["Password1"] == DBNull.Value ? null : Convert.ToString(rd["Password1"]),
                            Password2 = rd["Password2"] == DBNull.Value ? null : Convert.ToString(rd["Password2"]),
                            Password3 = rd["Password3"] == DBNull.Value ? null : Convert.ToString(rd["Password3"]),
                            LastLogin = Convert.ToDateTime(rd["LastLogin"]),
                            PasswordChangeDate = Convert.ToDateTime(rd["PasswordChangeDate"]),
                            Attempt = Convert.ToInt32(rd["Attempt"]),
                            Status = rd["Status"] == DBNull.Value ? null : Convert.ToString(rd["Status"]),
                            ResetOTP = Convert.ToString(rd["ResetOTP"]),
                            CreatedBy = rd["CreatedBy"] == DBNull.Value ? null : Convert.ToString(rd["CreatedBy"]),
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            LastUpdatedBy = rd["LastUpdatedBy"] == DBNull.Value ? null : Convert.ToString(rd["LastUpdatedBy"]),
                            LastUpdatedOn = Convert.ToDateTime(rd["LastUpdatedOn"]),
                            PartnerType = Convert.ToString(rd["PartnerType"]),
                            Branch = rd["Branch"] == DBNull.Value ? null : Convert.ToString(rd["Branch"]),
                            usercreatedname = rd[29] == DBNull.Value ? null : Convert.ToString(rd[29]),
                            IsfirstLogin = Convert.ToInt32(rd["IsfirstLogin"]),
                            LoginId = rd["LoginId"] == DBNull.Value ? "" : Convert.ToString(rd["LoginId"])
                        };
                    }
                    return employeelist;
                }
            }
        }
        public List<Employee> GetVendorListByName()
        {
            List<Employee> GetVendorList = new List<Employee>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Vendor_VendorNameList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {

                            GetVendorList.Add(new Employee
                            {
                                vendorId = Convert.ToInt32(rd["Id"]),
                                Vendorname = Convert.ToString(rd["Person"])

                            });
                        }
                        return GetVendorList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public List<Employee> GetUserRoles()
        {
            List<Employee> GetUserroleList = new List<Employee>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_GetEmployeerolelist]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {

                            GetUserroleList.Add(new Employee
                            {
                                RoleId = Convert.ToInt32(rd["RoleId"]),
                                UserRole = Convert.ToString(rd["RoleName"])

                            });
                        }
                        return GetUserroleList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public bool DeleteUser(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_DeleteUser]", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        return cmd.ExecuteNonQuery() > 0;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
        }



        // Access Related Here...
        public List<SelectListItem> GetEmployeeName_SL()
        {
            List<SelectListItem> GetEmployeeNameSelectList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_GetNameSL]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["FullName"]) + " / " + Convert.ToString(rd["EmpId"]);
                        string Id = Convert.ToString(rd["FullName"]) + " / " + Convert.ToString(rd["EmpId"]);
                        GetEmployeeNameSelectList.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return GetEmployeeNameSelectList;
                }
            }
        }
        public List<WebAccessPermission> GetEmployeeWebAccessList()
        {
            List<WebAccessPermission> EmployeeWebAccessList = new List<WebAccessPermission>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[WebAccessPermission_List]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        EmployeeWebAccessList.Add(new WebAccessPermission
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            WhoisIp = Convert.ToString(rd["WhoisIp"] == DBNull.Value ? null : rd["WhoisIp"]),
                            MacAddress = Convert.ToString(rd["MacAddress"] == DBNull.Value ? null : rd["MacAddress"]),
                            Status = Convert.ToInt32(rd["Status"] == DBNull.Value ? null : rd["Status"]),
                            Status_Desc = Convert.ToString(rd["Status_Desc"] == DBNull.Value ? null : rd["Status_Desc"]),
                            Created_On = Convert.ToDateTime(rd["Created_On"] == DBNull.Value ? null : rd["Created_On"]),
                            EmployeeCount = Convert.ToInt32(rd["EmployeeCount"] == DBNull.Value ? null : rd["EmployeeCount"]),
                        });
                    }
                    return EmployeeWebAccessList;
                }
            }
        }
        public int EmployeeWebAccessPermissionCreate(WebAccessPermission webAccessinfo)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[AccessPermission_Create]", con))
            {
                con.Open();
                DataTable odt = new DataTable();
                odt.Columns.Add("Id", typeof(string));
                var count = 0;
                if (webAccessinfo.EmployeeId != null)
                    foreach (var o in webAccessinfo.EmployeeId)
                    {
                       odt.Rows.Add(o.Split("/")[1].Replace(" ", ""));
                        count++;
                    }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@WhoisIp", SqlDbType.VarChar, 100).Value = webAccessinfo.WhoisIp;
                cmd.Parameters.Add("@MacAddress", SqlDbType.VarChar, 100).Value = webAccessinfo.MacAddress;
                //cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar,100).Value = webAccessinfo.EmployeeId;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = webAccessinfo.Status;
                cmd.Parameters.Add("@Created_By", SqlDbType.VarChar, 100).Value = webAccessinfo.Created_By;
                cmd.Parameters.Add("@tblListId", SqlDbType.Structured).Value = odt;
                //con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int EmployeeWebAccessPermissionUpdate(WebAccessPermission webAccessinfo)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[WebAccessPermission_Update]", con))
            {
                con.Open();
                DataTable odt = new DataTable();
                odt.Columns.Add("Id", typeof(string));
                var count = 0;
                if (webAccessinfo.EmployeeId != null)
                    foreach (var o in webAccessinfo.EmployeeId)
                    {
                        odt.Rows.Add(o.Split("/")[1].Replace(" ", ""));
                        count++;
                    }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@WhoisIp", SqlDbType.VarChar, 100).Value = webAccessinfo.WhoisIp;
                cmd.Parameters.Add("@MacAddress", SqlDbType.VarChar, 100).Value = webAccessinfo.MacAddress;
                cmd.Parameters.Add("@Updated_By", SqlDbType.VarChar, 100).Value = webAccessinfo.Updated_By;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = webAccessinfo.Id;
                cmd.Parameters.Add("@tblListId", SqlDbType.Structured).Value = odt;
                //con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public string EmployeeWebAccessPermissionStatusUpdate(int Status, WebAccessPermission webaccessInfo)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[WebAccessPermission_StatusUpdate]", con))
            {
                try
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("Id", typeof(int));
                    var count = 0;
                    if (webaccessInfo.Checkboxid != null)
                        foreach (var o in webaccessInfo.Checkboxid)
                        {
                            odt.Rows.Add(o);
                            count++;
                        }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
                    cmd.Parameters.Add("@Updated_By", SqlDbType.VarChar, 100).Value = webaccessInfo.Updated_By;
                    cmd.Parameters.Add("@tblListId", SqlDbType.Structured).Value = odt;

                    return cmd.ExecuteScalar() as string;
                }
                catch (Exception ex)
                {
                    return "1";
                }

            }
        }
        public string GetWebAccessIp(string WhoisIp, string EmployeeId)
        {
            string GetWebAccessIpDetail = "";

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[WebAccess_GetIpList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@WhoisIp", SqlDbType.VarChar, 250).Value = MappingHelpers.SetNullableValue(WhoisIp);
                cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar, 250).Value = MappingHelpers.SetNullableValue(EmployeeId);

                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        GetWebAccessIpDetail = Convert.ToString(rd["WhoisIp"]);
                    }
                    return GetWebAccessIpDetail;
                }
            }
        }
        public string GetWhitelistEmployee(string EmployeeId)
        {
            string GetWhitelistEmployeeDetail = "";

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Web_GetWhitelistEmployee]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar, 250).Value = MappingHelpers.SetNullableValue(EmployeeId);

                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        GetWhitelistEmployeeDetail = Convert.ToString(rd["EmployeeId"]);
                    }
                    return GetWhitelistEmployeeDetail;
                }
            }
        }
        public string GetGlobalWebAccessIp(string WhoisIp)
        {
            string GetWebAccessIpDetail = "";

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GlobalWebAccess_GetIpList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GlobalIp", SqlDbType.VarChar, 250).Value = MappingHelpers.SetNullableValue(WhoisIp);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        GetWebAccessIpDetail = Convert.ToString(rd["GlobalIp"]);
                    }
                    return GetWebAccessIpDetail;
                }
            }
        }
        public int InserNetwebIP(string Ip, string EmployeeId, int Status)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[webnetIp_create]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ip", SqlDbType.VarChar).Value = Ip;
                cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar, 250).Value = EmployeeId;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public int GlobalPermissionCreate(GlobalIPInfo globalipinfo)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GlobalIP_Create]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GlobalIp", SqlDbType.VarChar, 100).Value = globalipinfo.GlobalIp;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = globalipinfo.Status;
                cmd.Parameters.Add("@Created_By", SqlDbType.VarChar, 100).Value = globalipinfo.Created_By;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int GlobalPermissionUpdate(GlobalIPInfo globalipinfo)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GlobalIP_Update]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GlobalIp", SqlDbType.VarChar, 100).Value = globalipinfo.GlobalIp;
                cmd.Parameters.Add("@Updated_By", SqlDbType.VarChar, 100).Value = globalipinfo.Updated_By;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = globalipinfo.Id;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public List<GlobalIPInfo> GetGlobalAccessList()
        {
            List<GlobalIPInfo> GlobalAccessList = new List<GlobalIPInfo>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GlobalIpPermission_List]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        GlobalAccessList.Add(new GlobalIPInfo
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            GlobalIp = Convert.ToString(rd["GlobalIp"] == DBNull.Value ? null : rd["GlobalIp"]),
                            Status = Convert.ToInt32(rd["Status"] == DBNull.Value ? null : rd["Status"]),
                            Status_Desc = Convert.ToString(rd["Status_Desc"] == DBNull.Value ? null : rd["Status_Desc"]),
                            CreatedName = Convert.ToString(rd["CreatedName"] == DBNull.Value ? null : rd["CreatedName"]),
                            UpdatedPersonName = Convert.ToString(rd["UpdatedPersonName"] == DBNull.Value ? null : rd["UpdatedPersonName"]),
                            Created_On = Convert.ToDateTime(rd["Created_On"] == DBNull.Value ? null : rd["Created_On"]),
                            Updated_On = Convert.ToDateTime(rd["Updated_On"] == DBNull.Value ? null : rd["Updated_On"])
                        });
                    }
                    return GlobalAccessList;
                }
            }
        }
        public string GlobalWebAccessPermissionStatusUpdate(int Status, GlobalIPInfo globalaccessInfo)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GlobalAccessPermission_StatusUpdate]", con))
            {
                try
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("Id", typeof(int));
                    var count = 0;
                    if (globalaccessInfo.Checkboxid != null)
                        foreach (var o in globalaccessInfo.Checkboxid)
                        {
                            odt.Rows.Add(o);
                            count++;
                        }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
                    cmd.Parameters.Add("@Updated_By", SqlDbType.VarChar, 100).Value = globalaccessInfo.Updated_By;
                    cmd.Parameters.Add("@tblListId", SqlDbType.Structured).Value = odt;

                    return cmd.ExecuteScalar() as string;
                }
                catch (Exception ex)
                {
                    return "1";
                }

            }
        }
        public List<SelectListItem> GetEmployeeMappedWebList(int WebAccessId, int Condition)
        {
            List<SelectListItem> GetEmployeeNameSelectList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[WebMappedEmployeeAccessPermission_Detail]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@WebAccessId", SqlDbType.Int).Value = WebAccessId;
                cmd.Parameters.Add("@Condition", SqlDbType.Int).Value = Condition;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = "";
                        string Id = "";
                        if (Condition == 0)
                        {
                            text = Convert.ToString(rd["FullName"]) + " / " + Convert.ToString(rd["EmpId"]);
                            Id = Convert.ToString(rd["FullName"]) + " / " + Convert.ToString(rd["EmpId"]);
                        }
                        else
                        {
                            text = Convert.ToString(rd["FullName"]) + " / " + Convert.ToString(rd["EmployeeId"]);
                            Id = Convert.ToString(rd["FullName"]) + " / " + Convert.ToString(rd["EmployeeId"]);
                        }

                        GetEmployeeNameSelectList.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return GetEmployeeNameSelectList;
                }
            }
        }
        public List<WebAccessInfoLog> GetWebAccessLogList()
        {
            List<WebAccessInfoLog> WebAccessLogList = new List<WebAccessInfoLog>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[web_netIpInfo]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        WebAccessLogList.Add(new WebAccessInfoLog
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            Ip = Convert.ToString(rd["Ip"] == DBNull.Value ? null : rd["Ip"]),
                            Status = Convert.ToBoolean(rd["Status"] == DBNull.Value ? null : rd["Status"]),
                            EmployeeId = Convert.ToString(rd["EmployeeId"] == DBNull.Value ? null : rd["EmployeeId"]),
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"] == DBNull.Value ? null : rd["CreatedOn"]),
                            EmployeeName = Convert.ToString(rd["EmployeeName"] == DBNull.Value ? null : rd["EmployeeName"]),
                        });
                    }
                    return WebAccessLogList;
                }
            }
        }
        public string ValidateWebAccessIp(string WhoisIp)
        {
            string GetWebAccessIpDetail = "";

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[WebAccess_GetWhoisIp]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@WhoisIp", SqlDbType.VarChar, 250).Value = MappingHelpers.SetNullableValue(WhoisIp);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        GetWebAccessIpDetail = Convert.ToString(rd["WhoisIp"]);
                    }
                    return GetWebAccessIpDetail;
                }
            }
        }
        public string ValidateWebAccessGlobalIp(string GlobalIp)
        {
            string GetWebAccessIpDetail = "";

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[WebAccess_GetGlobalIp]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GlobalIp", SqlDbType.VarChar, 250).Value = MappingHelpers.SetNullableValue(GlobalIp);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        GetWebAccessIpDetail = Convert.ToString(rd["GlobalIp"]);
                    }
                    return GetWebAccessIpDetail;
                }
            }
        }
        public int EmployeeWhitelistWebAccessCreate(WebAccessPermission webAccessinfo)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[AccessPermission_WhitelistEmployeeCreate]", con))
            {
                con.Open();
                DataTable odt = new DataTable();
                odt.Columns.Add("Id", typeof(string));
                var count = 0;
                if (webAccessinfo.EmployeeId != null)
                    foreach (var o in webAccessinfo.EmployeeId)
                    {
                        odt.Rows.Add(o.Split("/")[1].Replace(" ", ""));
                        count++;
                    }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Created_By", SqlDbType.VarChar, 100).Value = webAccessinfo.Created_By;
                cmd.Parameters.Add("@tblListId", SqlDbType.Structured).Value = odt;
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public List<SelectListItem> GetWhitelistEmployeeMappedWebList(int Condition)
        {
            List<SelectListItem> GetEmployeeNameSelectList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[WebMappedWhitelistEmployeePermission_Detail]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Condition", SqlDbType.Int).Value = Condition;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = "";
                        string Id = "";
                        if (Condition == 0)
                        {
                            text = Convert.ToString(rd["FullName"]) + " / " + Convert.ToString(rd["EmpId"]);
                            Id = Convert.ToString(rd["FullName"]) + " / " + Convert.ToString(rd["EmpId"]);
                        }
                        else
                        {
                            text = Convert.ToString(rd["FullName"]) + " / " + Convert.ToString(rd["EmployeeId"]);
                            Id = Convert.ToString(rd["FullName"]) + " / " + Convert.ToString(rd["EmployeeId"]);
                        }

                        GetEmployeeNameSelectList.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return GetEmployeeNameSelectList;
                }
            }
        }
        public List<Hub> GetHublist()
        {
            var hubDetailList = new List<Hub>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[User_HubList]", con))
                {
                    try
                    {
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            hubDetailList.Add(new Hub
                            {
                                Id = Convert.ToInt32(reader[0]),
                                HubId = Convert.ToString(reader[1]),
                                HubName = Convert.ToString(reader[2]),
                                Area = Convert.ToString(reader[3]),
                                BuildingName = Convert.ToString(reader[4]),
                                RoomNo = Convert.ToString(reader[5]),
                                Sector = Convert.ToString(reader[6]),
                                Landmark = Convert.ToString(reader[7]),
                                City = Convert.ToString(reader[8]),
                                State = Convert.ToString(reader[9]),
                                Country = Convert.ToString(reader[10]),
                                CreatedBy = Convert.ToString(reader[11]),
                                LastUpdatedOn = Convert.ToDateTime(reader[12]),
                                Count = Convert.ToInt32(reader[13]),
                                HubCount = Convert.ToInt32(reader[14]),
                                HubDetails = Convert.ToString(reader[3]) + ',' + Convert.ToString(reader[8]),
                            });
                        }
                    }
                    catch (Exception)
                    {
                        return hubDetailList;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                            con.Dispose();
                        }
                    }
                }
            }
            return hubDetailList;
        }


        // Account Setup and Password Related Here...
        public Employee GetSecurityInfo(string empid, string EmpId)
        {
            if (empid != null)
            {
                if (!empid.Contains("EMPID"))
                {
                    empid = "EMPID0" + empid;
                }
            }
            Employee emp = null;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_GetSecurityInfo]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 100).Value = MappingHelpers.SetNullableValue(empid);
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 100).Value = MappingHelpers.SetNullableValue(EmpId);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        emp = new Employee
                        {
                            id = Convert.ToInt32(rd["id"]),
                            EmpId = rd["EmpId"] as string,
                            FullName = rd["FullName"] as string,
                            EmailId = rd["EmailId"] as string,
                            ContactNo = rd["ContactNo"] as string,
                            UserRole = Convert.ToString(rd["UserRole"]),
                            Password =  Convert.ToString(rd["Password"]),
                            Password1 = Convert.ToString(rd["Password1"]),
                            Password2 = Convert.ToString(rd["Password2"]),
                            Password3 = Convert.ToString(rd["Password3"]),
                            PasswordChangeDate = Convert.ToDateTime(rd["PasswordChangeDate"]),
                            Status = Convert.ToString(rd["Status"]),
                            LastLogin = MappingHelpers.DateTimeGetValue(rd["LastLogin"]),
                            Attempt = Convert.ToInt32(rd["Attempt"]),
                            ResetOTP = rd["ResetOTP"] as string,
                        };
                    }
                    return emp;
                }
            }
        }
        public void ChangePassword(string empid, string newPassword)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_ChangePassword]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar,100).Value = empid;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 100).Value = newPassword;
                con.Open();
                cmd.ExecuteNonQuery();
                return;
            }
        }
        public void UpdateOtp(int empid, string otp)
        {
            
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_EmployeeUpdateOtp]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = empid;
                    cmd.Parameters.Add("@otp", SqlDbType.VarChar, 100).Value = otp;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return;
                }
            
        }

        public void UpdateOtp1(string empid, string otp)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_UpdateOtp]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 100).Value = empid;
                cmd.Parameters.Add("@otp", SqlDbType.VarChar, 100).Value = otp;
                con.Open();
                cmd.ExecuteNonQuery();
                return;
            }

        }
        public void CompleteAccountRecovery(string empid, string newPassword, string Modifiedby)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_CompleteAccountRecovery]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 100).Value = empid;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 100).Value = newPassword;
                cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar, 100).Value = Modifiedby;
                con.Open();
                cmd.ExecuteNonQuery();
                return;
            }
        }

        public string CheckUniqueEmailId(string EmailId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_CheckuniqueEmpEmailId]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 100).Value = EmailId;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    var emailId = "";
                    if (rd.Read())
                    {
                        emailId = Convert.ToString(rd["EmailId"]);
                    }
                    return emailId;
                }
            }
        }

        public string CheckUniqueEmailIdTest(string EmailId, int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_CheckuniqueEmpEmailIdDetail]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 100).Value = EmailId;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    var emailId = "";
                    if (rd.Read())
                    {
                        emailId = Convert.ToString(rd["EmailId"]);
                    }
                    return emailId;
                }
            }
        }

        public string CheckUniqueContactNo(string phoneNo)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_CheckuniqueEmpContactNo]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@phoneNo", SqlDbType.VarChar, 15).Value = phoneNo;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    var ContactNo = "";
                    if (rd.Read())
                    {
                        ContactNo = Convert.ToString(rd["ContactNo"]);
                    }
                    return ContactNo;
                }
            }
        }

        public string CheckUniqueloginId(string loginId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Employee_CheckuniqueEmpLoginId]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@loginId", SqlDbType.VarChar, 100).Value = loginId;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    var LoginId = "";
                    if (rd.Read())
                    {
                        LoginId = Convert.ToString(rd["LoginId"]);
                    }
                    return LoginId;
                }
            }
        }
        public string GetWhitelistUser(string employeeId)
        {
            string GetWhitelistUserDetail = "";

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Web_GetWhitelistUser]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 250).Value = MappingHelpers.SetNullableValue(employeeId);

                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        GetWhitelistUserDetail = Convert.ToString(rd["EmployeeId"]);
                    }
                    return GetWhitelistUserDetail;
                }
            }
        }

    }
}
