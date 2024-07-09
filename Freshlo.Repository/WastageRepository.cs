using Freshlo.DomainEntities.Wastage;
using Freshlo.RI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Repository
{
   public class WastageRepository:IWastageRI
    {
        private IDbConfig _dbConfig { get; }
        public WastageRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        public async Task<List<Wastage>> GetallItemList()
        {
            List<Wastage> ItemList = new List<Wastage>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Wastage_GetWastageProductDetail]";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (rd.Read())
                    {
                        ItemList.Add(new Wastage
                        {
                            ItemId = Convert.ToString(rd["ItemId"] == DBNull.Value ? "ITM00" : rd["ItemId"]),
                            PluName = Convert.ToString(rd["PluName"] == DBNull.Value ? "NA" : rd["PluName"]),
                            TotalStock = Convert.ToDouble(rd["Stock"] == DBNull.Value ? 0 : rd["Stock"]),
                            Measurement = Convert.ToString(rd["Measurement"] == DBNull.Value ? "Mt" : rd["Measurement"]),
                            WastageItemPrice = rd["ItemPrice"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["ItemPrice"]),
                            PurchasePrice = Convert.ToDouble(rd["PurchasePrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["ItemPrice"])),
                            Id=Convert.ToInt32(rd["Id"] == DBNull.Value ? 0 : rd["Id"]),
                            wastageid=Convert.ToInt32(rd["wastageid"] == DBNull.Value ? 0 : rd["wastageid"]),
                            Wastage_Quan=Convert.ToDouble(rd["Wastage_Quan"] == DBNull.Value ?  0  : rd["Wastage_Quan"]),
                            ItemwastagePrice = Convert.ToDouble(rd["WastageItemPrice"] == DBNull.Value ? 0 : rd["WastageItemPrice"])
                        });
                    }
                    return ItemList;
                }
            }

        }
        public int CreateorUpdateWastageDetail(Wastage wastagedata)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Wastage_UpdateWastage]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = wastagedata.wastageid;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar,50).Value = wastagedata.ItemId;
                cmd.Parameters.Add("@Wastage_Quan", SqlDbType.Float).Value =wastagedata.TotalWastageQuan;
                cmd.Parameters.Add("@WastageType", SqlDbType.Int).Value = wastagedata.WastageType;
                cmd.Parameters.Add("@Wastage_Description", SqlDbType.VarChar,200).Value = wastagedata.Wastage_Description;
                cmd.Parameters.Add("@WastageItemPrice", SqlDbType.Float).Value = wastagedata.ItemwastagePrice;
                cmd.Parameters.Add("@Hub", SqlDbType.VarChar,30).Value = wastagedata.Hub;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar,50).Value = wastagedata.CreatedBy;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int CreateWastageLog(Wastage wastagedata)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Wastage_CreateWastageLog]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@WastageId", SqlDbType.Int).Value = wastagedata.wastageid;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = wastagedata.ItemId;
                cmd.Parameters.Add("@Wastage", SqlDbType.Float).Value = wastagedata.Wastage_Quan;
                cmd.Parameters.Add("@WastageType", SqlDbType.Int).Value = wastagedata.WastageType;
                cmd.Parameters.Add("@WastageDescription", SqlDbType.VarChar, 200).Value = wastagedata.Wastage_Description;
                cmd.Parameters.Add("@WastagePrice", SqlDbType.Float).Value = wastagedata.WastageItemPrice;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = wastagedata.CreatedBy;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int UpdateStockDetail(Wastage wastagedata)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Stock_UpdateStock]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = wastagedata.stockId;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = wastagedata.ItemId;
                cmd.Parameters.Add("@Stock", SqlDbType.Float).Value = wastagedata.Wastagestock;
                cmd.Parameters.Add("@Measurement", SqlDbType.VarChar, 30).Value = wastagedata.Measurement;
                cmd.Parameters.Add("@ItemPrice", SqlDbType.VarChar, 50).Value = wastagedata.stockPo;
                cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 50).Value = wastagedata.Hub;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = wastagedata.CreatedBy;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
