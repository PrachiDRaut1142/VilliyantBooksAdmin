using Freshlo.DomainEntities.Vendor;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Freshlo.Repository
{
    public class VendorRepository : IVendorRI
    {

        private IDbConfig _dbConfig { get; }

        public VendorRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }


        public List<Vendor> GetVendorList(string hubId)
        {
            List<Vendor> Vendorlist = new List<Vendor>();

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Vendor_GetVendorList]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = hubId;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Vendorlist.Add(new Vendor
                                {
                                    Id = Convert.ToInt32(dr[0]),
                                    Person = Convert.ToString(dr[1]),
                                    Role = Convert.ToString(dr[2]),
                                    ProductType = Convert.ToString(dr[3]),
                                    EmailId = Convert.ToString(dr[4]),
                                    ContactNo = Convert.ToString(dr[5]),
                                    City = Convert.ToString(dr[6]),
                                    Status = Convert.ToString(dr[7]),
                                    LastUpdatedOn = Convert.ToDateTime(dr[8]),
                                    Business = Convert.ToString(dr[9]),
                                });
                            }

                        }



                    }

                    catch (Exception ex)
                    {
                        return Vendorlist;
                    }

                }

            }
            return Vendorlist;
        }

        public int AddVendor(Vendor info)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[Vendor_CreateVendor]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("GetMainCatlist", typeof(int));
                    if (info.productTypesValues != null)
                        foreach (int o in info.productTypesValues)
                            odt.Rows.Add(o);
                    cmd.Parameters.Add("@GetMainCatlist", SqlDbType.Structured).Value = odt;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = info.Status == null ? (object)DBNull.Value : info.Status;
                    cmd.Parameters.Add("@Business", SqlDbType.VarChar, 50).Value = info.Business == null ? (object)DBNull.Value : info.Business;
                    cmd.Parameters.Add("@Person", SqlDbType.VarChar, 50).Value = info.Person == null ? (object)DBNull.Value : info.Person;
                    cmd.Parameters.Add("@Role", SqlDbType.VarChar, 30).Value = info.Role == null ? "Supplier" : info.Role;
                    cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = info.EmailId == null ? (object)DBNull.Value : info.EmailId;
                    cmd.Parameters.Add("@Ext", SqlDbType.VarChar, 5).Value = info.Ext == null ? "+91" : info.Ext;
                    cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar, 50).Value = info.ContactNo == null ? (object)DBNull.Value : info.ContactNo;
                    cmd.Parameters.Add("@Area", SqlDbType.VarChar, 100).Value = info.Area == null ? (object)DBNull.Value : info.Area;
                    cmd.Parameters.Add("@BuildingName", SqlDbType.VarChar, 100).Value = info.BuildingName == null ? (object)DBNull.Value : info.BuildingName;
                    cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar, 50).Value = info.RoomNo == null ? (object)DBNull.Value : info.RoomNo;
                    cmd.Parameters.Add("@Sector", SqlDbType.VarChar, 70).Value = info.Sector == null ? (object)DBNull.Value : info.Sector;
                    cmd.Parameters.Add("@Landmark", SqlDbType.VarChar, 100).Value = info.Landmark == null ? (object)DBNull.Value : info.Landmark;
                    cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 10).Value = info.ZipCode == null ? (object)DBNull.Value : info.ZipCode;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 20).Value = info.City == null ? (object)DBNull.Value : info.City;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 20).Value = info.State == null ? "Maharashtra" : info.State;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar, 20).Value = info.Country == null ? "India" : info.Country;
                    cmd.Parameters.Add("@Commission", SqlDbType.Float).Value = info.Commission;
                    cmd.Parameters.Add("@GstNumber", SqlDbType.VarChar, 100).Value = info.GSTNumber == null ? (object)DBNull.Value : info.GSTNumber;
                    cmd.Parameters.Add("@IFSCnumber", SqlDbType.VarChar, 100).Value = info.IFSCNumber == null ? (object)DBNull.Value : info.IFSCNumber;
                    cmd.Parameters.Add("@BankAcNumber", SqlDbType.VarChar, 100).Value = info.BankAccountNumber == null ? (object)DBNull.Value : info.BankAccountNumber;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 10).Value = info.CreatedBy;
                    cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 30).Value = info.Hub == null ? (object)DBNull.Value : info.Hub;
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<SelectListItem> GetMainCategoryList()
        {
            List<SelectListItem> GetMainCategorySelectList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[PriceList_MainCategory]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string text = Convert.ToString(rd["Name"]);
                            string Id = Convert.ToString(rd["Id"]);
                            GetMainCategorySelectList.Add(new SelectListItem()
                            {
                                Text = text,
                                Value = Id,
                            });
                        }
                        return GetMainCategorySelectList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public int DeleteVendor(int Id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Vendor_DeleteVendor]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public Vendor GetVendorDetails(int id)
        {
            Vendor data = new Vendor();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Vendor_GetVendorDetails]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                data = new Vendor
                                {
                                    Id = Convert.ToInt32(rd["Id"]),
                                    VendorId = Convert.ToString(rd["VendorId"] == DBNull.Value ? null : Convert.ToString(rd["VendorId"])),
                                    Status = Convert.ToString(rd["Status"] == DBNull.Value ? null : Convert.ToString(rd["Status"])),
                                    Business = Convert.ToString(rd["Business"] == DBNull.Value ? null : Convert.ToString(rd["Business"])),
                                    Person = Convert.ToString(rd["Person"] == DBNull.Value ? null : Convert.ToString(rd["Person"])),
                                    ContactNo = Convert.ToString(rd["ContactNo"] == DBNull.Value ? null : Convert.ToString(rd["ContactNo"])),
                                    EmailId = Convert.ToString(rd["EmailId"] == DBNull.Value ? null : Convert.ToString(rd["EmailId"])),
                                    Commission = Convert.ToInt32(rd["Commission"] == DBNull.Value ? "0" : rd["Commission"]),
                                    GSTNumber = Convert.ToString(rd["GstNumber"] == DBNull.Value ? null : Convert.ToString(rd["GstNumber"])),
                                    IFSCNumber = Convert.ToString(rd["IFSCnumber"] == DBNull.Value ? null : Convert.ToString(rd["IFSCnumber"])),
                                    BankAccountNumber = Convert.ToString(rd["BankAcNumber"] == DBNull.Value ? null : Convert.ToString(rd["BankAcNumber"])),


                                };
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return data;
                    }
                    return data;
                }
            }
        }

        public List<int> GetCategroiesList(int Id)
        {
            var result = new List<int>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Vendor_GetMainCategerolistByVendorId]", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(Convert.ToInt32(reader[0]));
                    }
                }
                return result;
            }
        }

        public int EditVendor(Vendor info)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[Vendor_UpdateVendor]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = info.Id;
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("GetMainCatlist", typeof(int));
                    if (info.productTypesValues != null)
                        foreach (int o in info.productTypesValues)
                            odt.Rows.Add(o);
                    cmd.Parameters.Add("@GetMainCatlist", SqlDbType.Structured).Value = odt;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = info.Status == null ? (object)DBNull.Value : info.Status;
                    cmd.Parameters.Add("@Business", SqlDbType.VarChar, 50).Value = info.Business == null ? (object)DBNull.Value : info.Business;
                    cmd.Parameters.Add("@Person", SqlDbType.VarChar, 50).Value = info.Person == null ? (object)DBNull.Value : info.Person;
                    cmd.Parameters.Add("@Role", SqlDbType.VarChar, 30).Value = info.Role == null ? "Supplier" : info.Role;
                    cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = info.EmailId == null ? (object)DBNull.Value : info.EmailId;
                    cmd.Parameters.Add("@Ext", SqlDbType.VarChar, 5).Value = info.Ext == null ? "+91" : info.Ext;
                    cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar, 50).Value = info.ContactNo == null ? (object)DBNull.Value : info.ContactNo;
                    cmd.Parameters.Add("@Area", SqlDbType.VarChar, 100).Value = info.Area == null ? (object)DBNull.Value : info.Area;
                    cmd.Parameters.Add("@BuildingName", SqlDbType.VarChar, 100).Value = info.BuildingName == null ? (object)DBNull.Value : info.BuildingName;
                    cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar, 50).Value = info.RoomNo == null ? (object)DBNull.Value : info.RoomNo;
                    cmd.Parameters.Add("@Sector", SqlDbType.VarChar, 70).Value = info.Sector == null ? (object)DBNull.Value : info.Sector;
                    cmd.Parameters.Add("@Landmark", SqlDbType.VarChar, 100).Value = info.Landmark == null ? (object)DBNull.Value : info.Landmark;
                    cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 10).Value = info.ZipCode == null ? (object)DBNull.Value : info.ZipCode;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 20).Value = info.City == null ? (object)DBNull.Value : info.City;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 20).Value = info.State == null ? "Maharashtra" : info.State;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar, 20).Value = info.Country == null ? "India" : info.Country;
                    cmd.Parameters.Add("@Commission", SqlDbType.Float).Value = info.Commission;
                    cmd.Parameters.Add("@GstNumber", SqlDbType.VarChar, 100).Value = info.GSTNumber == null ? (object)DBNull.Value : info.GSTNumber;
                    cmd.Parameters.Add("@IFSCnumber", SqlDbType.VarChar, 100).Value = info.IFSCNumber == null ? (object)DBNull.Value : info.IFSCNumber;
                    cmd.Parameters.Add("@BankAcNumber", SqlDbType.VarChar, 100).Value = info.BankAccountNumber == null ? (object)DBNull.Value : info.BankAccountNumber;
                    cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 10).Value = info.LastUpdatedBy;
                    cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 30).Value = info.Hub == null ? (object)DBNull.Value : info.Hub;
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        List<Vendor> IVendorRI.GetVendorListByName()
        {
            List<Vendor> GetVendorList = new List<Vendor>();
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

                            GetVendorList.Add(new Vendor
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                Person = Convert.ToString(rd["Person"])

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
    }
}