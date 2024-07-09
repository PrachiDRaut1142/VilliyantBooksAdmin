using Freshlo.DomainEntities.Coupen;
using Freshlo.RI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Freshlo.Repository
{
    public class CoupenRepository : ICoupenRI
    {
        private IDbConfig _dbConfig { get; }
        public CoupenRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        public List<Coupen> GetCoupenList(string hubId)
        {
            List<Coupen> getCoupenlist = new List<Coupen>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Coupen_getallCoupenlist]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = hubId;
                    con.Open();
                    try
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                getCoupenlist.Add(new Coupen
                                {
                                    Id = Convert.ToInt32(dr[0]),
                                    Title = Convert.ToString(dr["Title"]),
                                    CoupenCode = Convert.ToString(dr["CoupenCode"] == DBNull.Value ? "NA" : dr["CoupenCode"]),
                                    ShortDescription = Convert.ToString(dr["ShortDescription"] == DBNull.Value ? "NA" : dr["ShortDescription"]),
                                    MinOrderValue = Convert.ToInt32(dr["MinOrderValue"] == DBNull.Value ? 0 : dr["MinOrderValue"]),
                                    MaxDiscount = Convert.ToInt32(dr["MaxDiscount"] == DBNull.Value ? 0 : dr["MaxDiscount"]),
                                    DiscountPercnt = Convert.ToInt32(dr["DiscountPercnt"] == DBNull.Value ? 0 : dr["DiscountPercnt"]),
                                    UsageAllowedperUser = Convert.ToInt32(dr["UsageAllowedperUser"] == DBNull.Value ? 0 : dr["UsageAllowedperUser"]),
                                    Status = Convert.ToString(dr["Status"] == DBNull.Value ? "NA" : dr["Status"]),
                                    CreatedOn = Convert.ToDateTime(dr["CreatedOn"] == DBNull.Value ? "NA" : dr["CreatedOn"]),
                                    ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"] == DBNull.Value ? "NA" : dr["ModifiedDate"]),
                                    CreatedBy = Convert.ToString(dr["CreatedBy"] == DBNull.Value ? "NA" : dr["CreatedBy"]),
                                    ModifiedBy = Convert.ToString(dr["ModifiedBy"] == DBNull.Value ? "NA" : dr["ModifiedBy"]),
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return getCoupenlist;
        }
        public int CreateCoupen(Coupen info)
        {
            info.CoupenCode = string.IsNullOrEmpty(info.CoupenCode.ToUpper()) ? "NA" : info.CoupenCode.ToUpper();
            info.ShortDescription = string.IsNullOrEmpty(info.ShortDescription) ? "NA" : info.ShortDescription;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Coupen_CreateCoupen]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = info.Id;
                        cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = info.Title;
                        cmd.Parameters.Add("@CoupenCode", SqlDbType.VarChar).Value = info.CoupenCode;
                        cmd.Parameters.Add("@ShortDescription", SqlDbType.VarChar).Value = info.ShortDescription;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = info.Status;
                        cmd.Parameters.Add("@MinOrderValue", SqlDbType.Int).Value = info.MinOrderValue;
                        cmd.Parameters.Add("@MaxDiscount", SqlDbType.Int).Value = info.MaxDiscount;
                        cmd.Parameters.Add("@UsageAllowedperUser", SqlDbType.Int).Value = info.UsageAllowedperUser;
                        cmd.Parameters.Add("@DiscountPercnt", SqlDbType.Int).Value = info.DiscountPercnt;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = info.CreatedBy;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 30).Value = info.Hub;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception e)
                    {
                        return 0;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        public string GetExistCoupenCode(string id)
        {
            var Coupencode = "";
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Coupen_ChkExitCoupenCode]", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Coupencode += Convert.ToString(sdr[0]) + ",";
                        }
                    }
                }
            }
            return Coupencode;            
        }
        public bool DeleteCoupen(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Coupen_DeleteCoupen]", con))
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
        public int UpdateCoupen(Coupen info)
        {
            info.CoupenCode = string.IsNullOrEmpty(info.CoupenCode.ToUpper()) ? "NA" : info.CoupenCode.ToUpper();
            info.ShortDescription = string.IsNullOrEmpty(info.ShortDescription) ? "NA" : info.ShortDescription;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Coupen_UpdateCoupen]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = info.Id;
                        cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = info.Title;
                        cmd.Parameters.Add("@CoupenCode", SqlDbType.NVarChar).Value = info.CoupenCode;
                        cmd.Parameters.Add("@ShortDescription", SqlDbType.NVarChar).Value = info.ShortDescription;
                        cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = info.Status;
                        cmd.Parameters.Add("@MinOrderValue", SqlDbType.Int).Value = info.MinOrderValue;
                        cmd.Parameters.Add("@MaxDiscount", SqlDbType.Int).Value = info.MaxDiscount;
                        cmd.Parameters.Add("@UsageAllowedperUser", SqlDbType.Int).Value = info.UsageAllowedperUser;
                        cmd.Parameters.Add("@DiscountPercnt", SqlDbType.Int).Value = info.DiscountPercnt;
                        cmd.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar).Value = info.ModifiedBy;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception e)
                    {
                        return 0;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        public Coupen GetCoupenDetails(string id)
        {
            var coupen = new Coupen();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Coupen_GetDetailsbyId]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                coupen.Id = Convert.ToInt32(dr[0]);
                                coupen.Title = Convert.ToString(dr["Title"]);
                                coupen.ShortDescription = Convert.ToString(dr["ShortDescription"]);
                                coupen.CoupenCode = Convert.ToString(dr["CoupenCode"]);
                                coupen.MinOrderValue = Convert.ToInt32(dr["MinOrderValue"]);
                                coupen.MaxDiscount = Convert.ToInt32(dr["MaxDiscount"]);
                                coupen.DiscountPercnt = Convert.ToInt32(dr["DiscountPercnt"]);
                                coupen.UsageAllowedperUser = Convert.ToInt32(dr["UsageAllowedperUser"]);
                                coupen.Status = Convert.ToString(dr["Status"]);
                                coupen.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                                coupen.ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"]);
                                coupen.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                                coupen.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                            }
                        }
                        return coupen;
                    }
                    catch (Exception e)
                    {
                        return coupen;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        public string CheckUniqueCouponcode(string CoupenCode)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Coupon_Checkunique]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CoupenCode", SqlDbType.VarChar, 100).Value = CoupenCode;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    var coupenCode = "";
                    if (rd.Read())
                    {
                        coupenCode = Convert.ToString(rd["CoupenCode"]);
                    }
                    return coupenCode;
                }
            }
        }
    }
}