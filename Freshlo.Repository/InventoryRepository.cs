using Freshlo.DomainEntities.Inventory;
using Freshlo.RI;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Freshlo.Repository
{
    public class InventoryRepository : InventoryRI
    {
        public InventoryRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }    
        private IDbConfig _dbConfig { get; }
        public List<InventoryAsset> Adhoc_Inventory(string id)
        {
            List<InventoryAsset> list = new List<InventoryAsset>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_tblInventoryListGet]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                con.Open();
                using(SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new InventoryAsset()
                        {
                            AssetsId = Convert.ToString(rd["AssetsId"]),
                            AssetName = Convert.ToString(rd["AssetName"]),
                            AssetsUnitPrice = Convert.ToString(rd["AssetsUnitPrice"]),
                            Quantity = Convert.ToInt32(rd["Quantity"]),
                        });
                    }
                    return list;
                }
            }       
        }

        public int Adhoc_Updates(InventoryAsset Id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[AdhocInOutInsert]",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 100).Value = Id.ItemId;
                    cmd.Parameters.Add("@UnitPrice", SqlDbType.VarChar, 20).Value = Id.AssetsUnitPrice;
                    cmd.Parameters.Add("@TotalPrice", SqlDbType.VarChar, 20).Value = Id.AssetUnitAd;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Id.Quantity;
                    cmd.Parameters.Add("@EntryType", SqlDbType.Int).Value = Id.EntryType;
                    cmd.Parameters.Add("@Hub", SqlDbType.VarChar).Value = Id.Hub;
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar, -1).Value = Id.Remarks;
                    cmd.Parameters.Add("@EmpId", SqlDbType.VarChar, 50).Value = Id.CreatedBy;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
        }

       
        public int CreateAudit(InventoryAsset info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[usp_InventoryAuditInsert]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AssetId", SqlDbType.VarChar,100).Value = info.AssetsId;
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = info.Quantity;
                cmd.Parameters.Add("@AuditQuantity", SqlDbType.Int).Value = info.AuditQuantity;
                cmd.Parameters.Add("@Differance", SqlDbType.Int).Value = info.differance;
                cmd.Parameters.Add("@AuditedBy", SqlDbType.VarChar,100).Value = info.CreatedBy;
                cmd.Parameters.Add("@Hub", SqlDbType.VarChar,100).Value = info.Hub;
                cmd.Parameters.Add("@Remark", SqlDbType.VarChar, -1).Value = info.Remarks;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<InventoryAsset> Inventory_Logs(string id)
        {
            List<InventoryAsset> list1 = new List<InventoryAsset>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_GetInventoryLogs]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list1.Add(new InventoryAsset()
                        {
                            ItemId = Convert.ToString(rd["ItemId"]),
                            AssetName = Convert.ToString(rd["AssetName"]),
                            Quantity = Convert.ToInt32(rd["Quantity"]),
                            EntryType = Convert.ToInt32(rd["EntryType"]),
                            Remarks = Convert.ToString(rd["Remarks"]),
                            CreatedBy = Convert.ToString(rd["Createdby"]),
                            CreatedDate = Convert.ToDateTime(rd["CreatedDate"]),
                        });
                    }
                    return list1;
                }
            }
        }

        public List<InventoryAsset> New_AuditList(string id)
        {
            List<InventoryAsset> list = new List<InventoryAsset>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_tblInventoryListGet]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new InventoryAsset()
                        {
                            AssetName = Convert.ToString(rd["AssetName"]),          
                            Quantity = Convert.ToInt32(rd["Quantity"]),
                            AssetsId = Convert.ToString(rd["AssetsId"]),
                        });
                    }
                    return list;
                }
            }
        }
        public List<InventoryAsset> AuditLogs(string id)
        {
            List<InventoryAsset> Auditlist = new List<InventoryAsset>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_GetAuditLogs]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        Auditlist.Add(new InventoryAsset()
                        {
                            AuditId = Convert.ToString(rd["AuditId"]),
                            CreatedDate = Convert.ToDateTime(rd["AuditDate"]),
                            AssetsId = Convert.ToString(rd["Assetcount"]),
                            CreatedBy = Convert.ToString(rd["AuditedBy"]),
                            Remarks = Convert.ToString(rd["Remark"]),
                        });
                    }
                    return Auditlist;
                }
            }
        }

        public InventoryAsset GetAuditlist(string id,string hubId)
        {

            InventoryAsset data = new InventoryAsset();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_GetAuditDetails]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AuditId", SqlDbType.VarChar).Value = id;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = hubId;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                data = new InventoryAsset
                                {
                                    AssetName = Convert.ToString(rd["AssetName"]),
                                    AuditId = Convert.ToString(rd["AuditId"]),
                                    CreatedDate = Convert.ToDateTime(rd["AuditDate"]),
                                    AuditQuantity = Convert.ToInt32(rd["AuditQuantity"]),
                                    Quantity = Convert.ToInt32(rd["Quantity"]),
                                    CreatedBy = Convert.ToString(rd["AuditedBy"]),
                                    Remarks = Convert.ToString(rd["Remark"]),
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
    }
}
