using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.DomainEntities.Purchase;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Repository
{
    public class PurchaseRepository : IPurchaseRI
    {
        public PurchaseRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        private IDbConfig _dbConfig { get; }
        public List<SelectListItem> GetHubList()
        {
            List<SelectListItem> zipcode = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Hub_GetDistinct]";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        zipcode.Add(new SelectListItem
                        {
                            Text = Convert.ToString(rd["HubName"] == DBNull.Value ? "0" : Convert.ToString(rd["HubName"])),
                            Value = Convert.ToString(rd["HubId"] == DBNull.Value ? "0" : Convert.ToString(rd["HubId"])),
                        });
                    }
                    return zipcode;
                }
            }
        }
        public string InsertPurchase(PurchaseDetail data)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Purchase_CreateOrder]", con))
            {
                con.Open();
                DataTable odt = new DataTable();
                odt.Columns.Add("Id", typeof(string));
                odt.Columns.Add("Quantity", typeof(float));
                odt.Columns.Add("measure", typeof(string));
                odt.Columns.Add("Purchase_Price", typeof(float));
                odt.Columns.Add("Receive_Price", typeof(float));
                odt.Columns.Add("Selling_Price", typeof(float));
                odt.Columns.Add("Market_Price", typeof(float));
                odt.Columns.Add("Profit_Per", typeof(float));

                if (data.List != null)
                    foreach (var o in data.List)
                        odt.Rows.Add(o.ItemId, o.TotalQuantity, o.Measurement, o.Purchase_Price, o.Procured_POPrice, o.Selling_Price, o.Market_Price, o.Profit_Per);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@DeliveryDate", SqlDbType.Int).Value = data.DeliveryDate;
                cmd.Parameters.Add("@TotalQty", SqlDbType.Float).Value = data.TotalQuantity;
                cmd.Parameters.Add("@TotalPrice", SqlDbType.Float).Value = data.Total_Price;
                cmd.Parameters.Add("@PluCount", SqlDbType.Float).Value = data.Plu_Count;
                cmd.Parameters.Add("@PurchaseStatus", SqlDbType.VarChar, 15).Value = "Procured";
                cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 30).Value = data.Branch;
                cmd.Parameters.Add("@ProcurementType", SqlDbType.VarChar, 50).Value = data.Procurement_Type;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = data.CreatedBy;
                cmd.Parameters.Add("@VendorId", SqlDbType.VarChar, 10).Value = data.Vendor;
                cmd.Parameters.Add("@tblList", SqlDbType.Structured).Value = odt;

                return cmd.ExecuteScalar() as string;
            }

        }
        public bool UpdatePriceList(PurchaseDetail data)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[Purchase_UpdatePrice]", con))
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("Id", typeof(string));
                    odt.Columns.Add("Quantity", typeof(float));
                    odt.Columns.Add("measure", typeof(string));
                    odt.Columns.Add("Purchase_Price", typeof(float));
                    odt.Columns.Add("Receive_Price", typeof(float));
                    odt.Columns.Add("Selling_Price", typeof(float));
                    odt.Columns.Add("Market_Price", typeof(float));
                    odt.Columns.Add("Profit_Per", typeof(float));


                    if (data.List != null)
                        foreach (var o in data.List)
                            odt.Rows.Add(o.ItemId, o.TotalQuantity, o.Measurement, o.Purchase_Price, o.Procured_POPrice, o.Selling_Price, o.Market_Price, o.Profit_Per);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = data.CreatedBy;
                    cmd.Parameters.Add("@tblList", SqlDbType.Structured).Value = odt;
                    cmd.Parameters.Add("@PurchaseId", SqlDbType.VarChar, 50).Value = data.PurchaseId;


                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           

        }
        public List<PurchaseDetail> GetAPurchaseList()
        {
            List<PurchaseDetail> List = new List<PurchaseDetail>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Purchase_GetFreshloPO]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        List.Add(new PurchaseDetail
                        {
                            PurchaseId = Convert.ToString(rd["PurchaseId"]),
                            TotalQuantity = rd["TotalQuantity"] == DBNull.Value ? 0 : Convert.ToInt32(rd["TotalQuantity"]),
                            Status = rd["PurchaseStatus"] == DBNull.Value ? null : Convert.ToString(rd["PurchaseStatus"]),
                            Plu_Count = rd["Plu_Count"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Plu_Count"]),
                            Total_Price = rd["Total_Price"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Total_Price"]),
                            Procurement_Type = rd["Procurement_Type"] == DBNull.Value ? null : Convert.ToString(rd["Procurement_Type"]),
                            CreatedBy = rd["CreatedBy"] as string,
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            UpdatedBy = rd["UpdatedBy"] as string,
                            UpdatedOn = Convert.ToDateTime(rd["LastUpdatedOn"]),
                            Transportation_Charge = rd["Transportation_Charge"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Transportation_Charge"]),
                            OtherExpension = rd["OtherExpension"] == DBNull.Value ? 0 : Convert.ToInt32(rd["OtherExpension"]),
                            Agent_commission = rd["Agent_commission"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Agent_commission"]),

                        });
                    }
                    return List;
                }
            }

        }
        public Purchase GetPurchaseDetail(string Id)
        {
            Purchase details = new Purchase();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetPurchaseDetails]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PurchaseId", SqlDbType.VarChar, 50).Value = Id;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        details.PurchaseId = Convert.ToString(reader["PurchaseId"]);
                        details.DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"] == DBNull.Value ? null : reader["DeliveryDate"]);
                        details.TotalQuantity = Convert.ToInt32(reader["TotalQuantity"]);
                        details.Status = Convert.ToString(reader["PurchaseStatus"]);
                        details.Branch = Convert.ToString(reader["hubName"] == DBNull.Value ? "FreshLo" : reader["hubName"]);
                        details.Plu_Count = Convert.ToInt32(reader["Plu_Count"] == DBNull.Value ? 0 : reader["Plu_Count"]);
                        details.LastUpdatedOn = Convert.ToDateTime(reader["LastUpdatedOn"]);
                        details.LastUpdatedBy = Convert.ToString(reader["UpdatedBy"]);
                        details.Transportation_Charge = Convert.ToDouble(reader["Transportation_Charge"] == DBNull.Value ? 0 : reader["Transportation_Charge"]);
                        details.Taxes = Convert.ToDouble(reader["Taxes"] == DBNull.Value ? 0 : reader["Taxes"]);
                        details.OtherExpension = Convert.ToDouble(reader["OtherExpension"] == DBNull.Value ? 0 : reader["OtherExpension"]);
                        details.Total_Price = Convert.ToInt32(reader["Total_Price"] == DBNull.Value ? 0 : reader["Total_Price"]);
                        details.ReceivedAmountPrice = Convert.ToDouble(reader["ReceivedItem_Price"] == DBNull.Value ? 0 : reader["ReceivedItem_Price"]);
                        details.PurchaseStatus = Convert.ToString(reader["PurchaseStatus"] == DBNull.Value ? null : reader["PurchaseStatus"]);
                        details.AgentCommision = Convert.ToDouble(reader["Agent_Commission"] == DBNull.Value ? 0 : reader["Agent_Commission"]);
                        details.VenderId = Convert.ToString(reader["Vendor_Id"] == DBNull.Value ? "" : reader["Vendor_Id"]);
                        details.ReferenceNo = Convert.ToString(reader["Reference_No"] == DBNull.Value ? "" : reader["Reference_No"]);
                        details.GST = Convert.ToSingle(reader["GST"] == DBNull.Value ? 0 : reader["GST"]);
                        details.Procurement_Type = Convert.ToString(reader["Procurement_Type"] == DBNull.Value ? "" : reader["Procurement_Type"]);
                    }
                }
                return details;
            }
        }
        public List<PurchaseList> GetPurchaseItemList(string Id)
        {
            List<PurchaseList> List = new List<PurchaseList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetPurchaseOrderList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PurchaseId", SqlDbType.VarChar, 50).Value = Id;

                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        List.Add(new PurchaseList
                        {
                            PurchaseId = Convert.ToString(rd["PurchaseId"]),
                            ProductName = rd["PluName"] as string,
                            TotalQuantity = rd["TotalQuantity"] == DBNull.Value ? 0 : Convert.ToInt32(rd["TotalQuantity"]),
                            PurchaseStatus = rd["PurchaseStatus"] == DBNull.Value ? null : Convert.ToString(rd["PurchaseStatus"]),
                            Measurement = rd["Measurement"] as string,
                            Procured_POPrice = rd["Procured_POPrice"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Procured_POPrice"]),
                            Procured_Quantity = rd["Procured_Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Procured_Quantity"]),
                            Selling_Price = rd["Selling_Price"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Selling_Price"]),
                            Market_Price = rd["Market_Price"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Market_Price"]),
                            Purchase_Price = rd["Purchase_Price"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Purchase_Price"]),
                            ItemId = rd["ItemId"] == DBNull.Value ? "0" : Convert.ToString(rd["ItemId"]),
                            Profit_Per = rd["SellingProfitPer"] == DBNull.Value ? 0 : Convert.ToDouble(rd["SellingProfitPer"]),
                            Weight = Convert.ToDouble(rd["Weight"]),
                        });
                    }
                    return List;
                }
            }

        }
        public void UpdatePurchaseDetail(Purchase data)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[UpdateProcure]", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OrderStatus", SqlDbType.VarChar, 30).Value = data.Status;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = data.LastUpdatedBy;
                cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 30).Value = data.Branch;
                cmd.Parameters.Add("@ProcureId", SqlDbType.VarChar, 30).Value = data.PurchaseId;
                cmd.Parameters.Add("@PluCount", SqlDbType.Float).Value = data.Plu_Count;
                cmd.Parameters.Add("@Taxes", SqlDbType.Float).Value = data.AgentCommision;
                cmd.Parameters.Add("@OtherExpension", SqlDbType.VarChar, 50).Value = data.OtherExpension;
                cmd.Parameters.Add("@TotalItemPrice", SqlDbType.VarChar, 50).Value = data.Total_Price;
                cmd.Parameters.Add("@Total_Price", SqlDbType.VarChar, 50).Value = data.Total_Price;
                cmd.Parameters.Add("@Transportation", SqlDbType.Float).Value = data.Transportation_Charge;
                cmd.Parameters.Add("@VendorId", SqlDbType.VarChar, 10).Value = data.VenderId;


                cmd.ExecuteNonQuery();
            }

        }
        public string UpdatePurchase(PurchaseDetail data)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Purchase_UpdateOrder]", con))
            {
                con.Open();
                DataTable odt = new DataTable();
                odt.Columns.Add("Id", typeof(string));
                odt.Columns.Add("Quantity", typeof(float));
                odt.Columns.Add("measure", typeof(string));
                odt.Columns.Add("Purchase_Price", typeof(float));
                odt.Columns.Add("Receive_Price", typeof(float));
                odt.Columns.Add("Selling_Price", typeof(float));
                odt.Columns.Add("Market_Price", typeof(float));
                odt.Columns.Add("Profit_Per", typeof(float));


                if (data.List != null)
                    foreach (var o in data.List)
                        odt.Rows.Add(o.ItemId, o.TotalQuantity, o.Measurement, o.Purchase_Price, o.Procured_POPrice, o.Selling_Price, o.Market_Price, o.Profit_Per);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@DeliveryDate", SqlDbType.Int).Value = data.DeliveryDate;
                cmd.Parameters.Add("@TotalQty", SqlDbType.Float).Value = data.TotalQuantity;
                cmd.Parameters.Add("@TotalPrice", SqlDbType.Float).Value = data.Total_Price;
                cmd.Parameters.Add("@PluCount", SqlDbType.Float).Value = data.Plu_Count;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = data.CreatedBy;
                cmd.Parameters.Add("@tblList", SqlDbType.Structured).Value = odt;
                cmd.Parameters.Add("@PurchaseId", SqlDbType.VarChar, 30).Value = data.PurchaseId;
                return cmd.ExecuteScalar() as string;
            }

        }
        public List<Item> GetItemList(string purchaseId, string searchTerm = null)
        {
            var insertValue = new StringBuilder();
            List<Item> itemList = new List<Item>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Purchase_GetItemList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(searchTerm))
                    cmd.Parameters.Add("@searchTerm", SqlDbType.VarChar, 100).Value = searchTerm;
                cmd.Parameters.Add("@purchaseId", SqlDbType.VarChar, 100).Value = purchaseId;
                try
                {
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            itemList.Add(new Item
                            {
                                ItemId = rd["ItemId"] as string,
                                PluName = rd["PluName"] as string,
                                Measurement = rd["Measurement"] as string,
                                Weight = Convert.ToDouble(rd[3]),
                                SellingPrice = Convert.ToDouble(rd[4]),
                                Category = rd["Category"] as string,
                                ItemType = rd["ItemType"] as string,
                                CategoryName = Convert.ToString(rd[7]),
                                MarketPrice = Convert.ToDouble(rd[8]),
                                TotalStock = Convert.ToDouble(rd[10] == DBNull.Value ? 0 : Convert.ToDouble(rd[10])),
                                DiscountPerctg = rd["DiscountedPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["DiscountedPrice"]),
                                stockId = Convert.ToInt32(rd["StockId"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["StockId"])),
                                Purchaseprice = Convert.ToDouble(rd["PurchasePrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["PurchasePrice"])),
                                ProfitMargin = Convert.ToDouble(rd["ProfitMargin"] == DBNull.Value ? 0 : Convert.ToDouble(rd["ProfitMargin"])),
                                Approval = rd["Approval"] as string,
                            });
                        }
                        return itemList;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public List<SelectListItem> GetSupplierNameList()
        {
            List<SelectListItem> SupplierNameList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetSupplierList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["Business"]);
                        string Id = Convert.ToString(rd["Id"]);
                        SupplierNameList.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return SupplierNameList;
                }
            }
        }
        public async Task<SummayData> GetSummaryData()
        {
            SummayData about = null;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Purchase_GetSummaryData]";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    if (rd.Read())
                    {
                        about = new SummayData
                        {
                            TodaysPO = Convert.ToInt32(rd["TodaysPO"]),
                            TodaysExpense = Convert.ToInt32(rd["TodaysOther"]),
                            WeeklyPO = Convert.ToInt32(rd["WeeklyPO"]),
                            WeeklyExpense = Convert.ToInt32(rd["WeeklyOther"]),
                            MonthlyPO = Convert.ToInt32(rd["MonthlyPO"]),
                            MonthlyExpense = Convert.ToInt32(rd["MonthlyOther"]),


                        };
                    }
                    return about;
                }
            }

        }
        public List<Pur_ItemSummary> GetItemSummary(SummaryFilter Options)
        {
            List<Pur_ItemSummary> List = new List<Pur_ItemSummary>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Purchase_GetItemSummary]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@filter", SqlDbType.Int).Value = Options.filter;
                cmd.Parameters.Add("@category", SqlDbType.VarChar, 50).Value = Options.category == "All" ? null : Options.category;
                cmd.Parameters.Add("@approval", SqlDbType.VarChar, 15).Value = Options.approval;
                cmd.Parameters.Add("@availability", SqlDbType.VarChar, 10).Value = Options.availability;
                cmd.Parameters.Add("@subCategory", SqlDbType.VarChar, 50).Value = Options.subCategory == "All" ? null : Options.subCategory;


                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        List.Add(new Pur_ItemSummary
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            ItemId = Convert.ToString(rd["ItemId"]),
                            Pluname = Convert.ToString(rd["Pluname"]),
                            Measurement = rd["Measurement"] as string,
                            POQuantity = rd["POQuantity"] == DBNull.Value ? 0 : Convert.ToDouble(rd["POQuantity"]),
                            POPrice = rd["POPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["POPrice"]),
                            SalesQuantity = rd["SalesQuantity"] == DBNull.Value ? 0 : Convert.ToDouble(rd["SalesQuantity"]),
                            SalesPrice = rd["SalesPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["SalesPrice"]),
                            Stock = rd["Stock"] == DBNull.Value ? 0 : Convert.ToDouble(rd["Stock"]),
                            ItemPrice = rd["ItemPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["ItemPrice"]),
                            Wastage_Quan = rd["Wastage_Quan"] == DBNull.Value ? 0 : Convert.ToDouble(rd["Wastage_Quan"]),
                            Weight = rd["weight"] == DBNull.Value ? 0 : Convert.ToDouble(rd["weight"]),
                            Type = rd["ItemType"] as string,
                        });
                    }
                    return List;
                }
            }

        }
        public List<Pur_ItemSummary> GetCategorySummary(SummaryFilter Options)
        {
            List<Pur_ItemSummary> List = new List<Pur_ItemSummary>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Purchase_GetCategorySummary]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MainCategoryId", SqlDbType.VarChar, 50).Value = Options.mainCategory == "All" ? null : Options.mainCategory;

                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        List.Add(new Pur_ItemSummary
                        {

                            CategoryId = Convert.ToString(rd["Category"]),
                            CategoryName = Convert.ToString(rd["Name"]),
                            //Measurement = Convert.ToString(rd["Measurement"]),
                            Stock = rd["Stock"] == DBNull.Value ? 0 : Convert.ToDouble(rd["Stock"]),
                            ItemPrice = rd["ItemPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["ItemPrice"]),
                            ItemNames = rd["ItemName"] == DBNull.Value ? "" : Convert.ToString(rd["ItemName"]),
                        });
                    }
                    return List;
                }
            }

        }




        // New Methode Bind ///


        // List PuchaseList and Append PurchaseList //
        public List<Purchase> GetPendingPurchase()
        {
            var itemList = new List<Purchase>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[GetUnionPurchaseListForHub]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            itemList.Add(new Purchase
                            {
                                ItemId = Convert.ToString(reader[0]),
                                pluName = Convert.ToString(reader[1]),
                                Measurement = Convert.ToString(reader[2]),
                                TotalQuantity = Convert.ToSingle(reader[3]),
                                QuantityValue = Convert.ToDouble(reader[4]),
                                Category = Convert.ToString(reader[5]),
                                PurchasePrice = Convert.ToDouble(reader["PurchasePrice"]),
                                SellingPrice = Convert.ToDouble(reader["SellingPrice"]),
                                MarketPrice = Convert.ToDouble(reader["MarketPrice"]),
                                ProfitMargin = Convert.ToDouble(reader["ProfitMargin"]),
                                Profitper = Convert.ToDouble(reader["ProfitPrice"]),
                                categoryName = Convert.ToString(reader["Name"]),
                                currentsock = Convert.ToInt32(reader["Stock"])
                            });
                        }
                        sqlcon.Close();
                        return itemList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public List<Purchase> GetAddPendingPurchaseOrder(string datefrom, string dateto, string createFor)
        {
            var itemList = new List<Purchase>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[GetUnionPurchaseListForHub]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@datefrom", datefrom);
                        cmd.Parameters.AddWithValue("@dateto", dateto);
                        cmd.Parameters.AddWithValue("@createFor", createFor);
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            itemList.Add(new Purchase
                            {
                                ItemId = Convert.ToString(reader[0]),
                                pluName = Convert.ToString(reader[1]),
                                Measurement = Convert.ToString(reader[2]),
                                TotalQuantity = Convert.ToSingle(reader[3]),
                                //QuantityValue = reader[4] == DBNull.Value ? (double?)null : Convert.ToDouble(reader[4]),
                                Category = Convert.ToString(reader[5]),
                                PurchasePrice = reader["PurchasePrice"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["PurchasePrice"]),
                                SellingPrice = reader["SellingPrice"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["SellingPrice"]),
                                MarketPrice = reader["MarketPrice"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["MarketPrice"]),
                                ProfitMargin = reader["ProfitMargin"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["ProfitMargin"]),
                                Profitper = reader["ProfitPrice"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["ProfitPrice"]),
                                categoryName = reader["Name"] == DBNull.Value ? null : Convert.ToString(reader["Name"]),
                                currentsock = reader["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Stock"])


                            });
                        }
                        sqlcon.Close();
                        return itemList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public List<PurchaseList> DownloadPurchasePdfData(string id)
        {

            List<PurchaseList> List = new List<PurchaseList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[PdfPurchase_ProductlistbyId]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PurchaseId", SqlDbType.VarChar, 50).Value = id;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                List.Add(new PurchaseList
                                {
                                    PurchaseId = Convert.ToString(rd["PurchaseId"]),
                                    ProductName = rd["PluName"] as string,
                                    TotalQuantity = rd["TotalQuantity"] == DBNull.Value ? 0 : Convert.ToInt32(rd["TotalQuantity"]),
                                    Purchase_Price = rd["Purchase_Price"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Purchase_Price"]),
                                    ItemId = rd["ItemId"] == DBNull.Value ? "0" : Convert.ToString(rd["ItemId"]),
                                    currentsock = rd["CurrentStock"] == DBNull.Value ? 0 : Convert.ToInt32(rd["CurrentStock"]),
                                    categoryName = rd["Name"] == DBNull.Value ? "NA" : Convert.ToString(rd["Name"]),
                                    CreatedBy = rd["CreatedBy"] == DBNull.Value ? "NA" : Convert.ToString(rd["CreatedBy"]),
                                    CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                                });
                            }
                            return List;
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }


        }



        // New Insert Purchase Create Methode //
        public string InsertNewPurchase(PurchaseDetail data)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[NewPurchase_CreateOrder]", con))
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("Id", typeof(string));
                    odt.Columns.Add("Quantity", typeof(float));
                    odt.Columns.Add("Category", typeof(string));
                    odt.Columns.Add("Purchase_Price", typeof(float));
                    odt.Columns.Add("CurrentStock", typeof(float));
                    if (data.List != null)
                        foreach (var o in data.List)
                            odt.Rows.Add(o.ItemId, o.TotalQuantity, o.Category, o.PurchasePrice, o.currentsock);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@DeliveryDate", SqlDbType.Int).Value = data.DeliveryDate;
                    cmd.Parameters.Add("@TotalQty", SqlDbType.Float).Value = data.TotalQuantity;
                    cmd.Parameters.Add("@TotalPrice", SqlDbType.Float).Value = data.Total_Price;
                    cmd.Parameters.Add("@PluCount", SqlDbType.Float).Value = data.Plu_Count;
                    cmd.Parameters.Add("@PurchaseStatus", SqlDbType.VarChar, 15).Value = "Procured";
                    cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 30).Value = data.Branch;
                    cmd.Parameters.Add("@ProcurementType", SqlDbType.VarChar, 50).Value = data.Procurement_Type;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = data.CreatedBy;
                    cmd.Parameters.Add("@VendorId", SqlDbType.VarChar, 10).Value = data.Vendor;
                    cmd.Parameters.Add("@tblList", SqlDbType.Structured).Value = odt;

                    return cmd.ExecuteScalar() as string;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }


  
        // New Insert Purchase Details List Filter Methode //
        public List<PurchaseList> GetallnewPurchaseListItem(string Id, PricelistFilter detail)
        {
            List<PurchaseList> List = new List<PurchaseList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Purchase_ProductlistbyId]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PurchaseId", SqlDbType.VarChar, 50).Value = Id;
                        cmd.Parameters.AddWithValue("@MainCategory", SqlDbType.Int).Value = detail.MainCategoryId;
                        cmd.Parameters.AddWithValue("@Category", SqlDbType.Int).Value = detail.CategoryId;
                        cmd.Parameters.AddWithValue("@VendorId", SqlDbType.Int).Value = detail.VendorId;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                List.Add(new PurchaseList
                                {
                                    PurchaseId = Convert.ToString(rd["PurchaseId"]),
                                    ProductName = rd["PluName"] as string,
                                    TotalQuantity = rd["TotalQuantity"] == DBNull.Value ? 0 : Convert.ToInt32(rd["TotalQuantity"]),
                                    Purchase_Price = rd["Purchase_Price"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Purchase_Price"]),
                                    ItemId = rd["ItemId"] == DBNull.Value ? "0" : Convert.ToString(rd["ItemId"]),
                                    currentsock = rd["CurrentStock"] == DBNull.Value ? 0 : Convert.ToInt32(rd["CurrentStock"]),
                                    categoryName = rd["Name"] == DBNull.Value ? "NA" : Convert.ToString(rd["Name"]),
                                    Category = rd["Category"] == DBNull.Value ? "NA" : Convert.ToString(rd["Category"]),
                                    CreatedBy = rd["CreatedBy"] == DBNull.Value ? "NA" : Convert.ToString(rd["CreatedBy"]),
                                    CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                                });
                            }
                            return List;
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
        }




        // New Update Purchase Details & List  Methode //
        public void NewUpdatePurchaseDetail(Purchase data)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[UpdatePurcahseNewOrderProcure]", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OrderStatus", SqlDbType.VarChar, 30).Value = data.Status;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = data.LastUpdatedBy;
                cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 30).Value = data.Branch;
                cmd.Parameters.Add("@ProcureId", SqlDbType.VarChar, 30).Value = data.PurchaseId;
                cmd.Parameters.Add("@PluCount", SqlDbType.Float).Value = data.Plu_Count;
                cmd.Parameters.Add("@Taxes", SqlDbType.Float).Value = data.AgentCommision;
                cmd.Parameters.Add("@OtherExpension", SqlDbType.VarChar, 50).Value = data.OtherExpension;
                cmd.Parameters.Add("@TotalItemPrice", SqlDbType.VarChar, 50).Value = data.Total_Price;
                cmd.Parameters.Add("@Total_Price", SqlDbType.VarChar, 50).Value = data.Total_Price;
                cmd.Parameters.Add("@Transportation", SqlDbType.Float).Value = data.Transportation_Charge;
                cmd.Parameters.Add("@Gst", SqlDbType.Float).Value = data.GST;
                cmd.Parameters.Add("@VendorId", SqlDbType.VarChar, 10).Value = data.VenderId;


                cmd.ExecuteNonQuery();
            }
        }
        public string NewUpdatePurchaseOrder(PurchaseDetail data)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[NewPurchase_UpdateOrderDetails]", con))
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("Id", typeof(string));
                    odt.Columns.Add("Quantity", typeof(float));
                    odt.Columns.Add("Category", typeof(string));
                    odt.Columns.Add("Purchase_Price", typeof(float));
                    odt.Columns.Add("CurrentStock", typeof(float));
                    if (data.List != null)
                        foreach (var o in data.List)
                            odt.Rows.Add(o.ItemId, o.TotalQuantity, o.Category, o.PurchasePrice, o.currentsock);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PurchaseId", SqlDbType.VarChar).Value = data.PurchaseId;
                    cmd.Parameters.Add("@TotalQty", SqlDbType.Float).Value = data.TotalQuantity;
                    cmd.Parameters.Add("@TotalPrice", SqlDbType.Float).Value = data.Total_Price;
                    cmd.Parameters.Add("@PluCount", SqlDbType.Float).Value = data.Plu_Count;
                    //cmd.Parameters.Add("@PurchaseStatus", SqlDbType.VarChar, 15).Value = "Procured";
                    //cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 30).Value = data.Branch;
                    //cmd.Parameters.Add("@ProcurementType", SqlDbType.VarChar, 50).Value = data.Procurement_Type;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = data.CreatedBy;
                    //cmd.Parameters.Add("@VendorId", SqlDbType.VarChar, 10).Value = data.Vendor;
                    cmd.Parameters.Add("@tblList", SqlDbType.Structured).Value = odt;

                    return cmd.ExecuteScalar() as string;
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public bool UpdateNewPriceList(PurchaseDetail data)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Purchase_UpdatePrice]", con))
            {
                con.Open();
                DataTable odt = new DataTable();
                odt.Columns.Add("Id", typeof(string));
                odt.Columns.Add("Quantity", typeof(float));
                odt.Columns.Add("Category", typeof(string));
                odt.Columns.Add("Purchase_Price", typeof(float));
                odt.Columns.Add("CurrentStock", typeof(float));


                if (data.List != null)
                    foreach (var o in data.List)
                        odt.Rows.Add(o.ItemId, o.TotalQuantity, o.Category, o.Purchase_Price, o.currentsock);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = data.CreatedBy;
                cmd.Parameters.Add("@tblList", SqlDbType.Structured).Value = odt;
                cmd.Parameters.Add("@PurchaseId", SqlDbType.VarChar, 50).Value = data.PurchaseId;


                return cmd.ExecuteNonQuery() > 0;
            }
        }



        // New Insert Purchase Details List Delete Methode  //
        public bool DeletePurchaseList(string id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[ProductList_AddPurchaseItemDelete]", con))
                    {
                        con.Open();
                        DataTable odt = new DataTable();
                        odt.Columns.Add("ItemId", typeof(string));
                        if (id != null)
                            foreach (var o in id.Split(','))
                                odt.Rows.Add(Convert.ToString(o));
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ProductListIdValues", SqlDbType.Structured).Value = odt;

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

        }


    }
}