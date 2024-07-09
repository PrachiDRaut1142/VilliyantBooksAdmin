using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Stock;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Repository
{
    public class StockRepository : IStockRI
    {
        public StockRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        private IDbConfig _dbConfig { get; }
        public async Task<List<Stock>> GetStockList(string itemName, string hub)
        {
            var itemList = new List<Stock>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Stock_GetItemList]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ItemName", SqlDbType.VarChar, 50).Value = itemName;
                cmd.Parameters.Add("@hub", SqlDbType.VarChar, 30).Value = hub;

                con.Open();
                try
                {
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        while (rd.Read())
                        {
                            itemList.Add(new Stock
                            {
                                ItemId = Convert.ToString(rd[0]),
                                PluName = Convert.ToString(rd[1]),
                                StockValue = Convert.ToDouble(rd[2]),
                                Measurement = Convert.ToString(rd["Measurement"]),
                                Weight = Convert.ToDouble(rd["weight"]),
                                ItemPrice = Convert.ToDouble(rd["TotalPrice"]),
                            });
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
            return itemList;
        }
        public Stock GetStock(string hubId)
        {
            var Stock = new Stock();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Usp_Get_stockList]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = hubId;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                Stock.Itemcount = Convert.ToString(dr[0]);
                                Stock.Itemoutstock = Convert.ToString(dr[1]);
                                Stock.Itemnotlive = Convert.ToString(dr[2]);
                            }
                        }
                        return Stock;
                    }
                    catch (Exception e)
                    {
                        return Stock;
                    }
                }
            }
        }
        public List<Stock> GetStockItemlist(string maincategory, string Category, int type, string hubId)
        {
            List<Stock> Stocklist = new List<Stock>();

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Usp_Get_Item_In_stock]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MainCategory", SqlDbType.VarChar, 50).Value = maincategory;
                        cmd.Parameters.Add("@Category", SqlDbType.VarChar, 50).Value = Category;
                        cmd.Parameters.Add("@type", SqlDbType.Int).Value = type;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = hubId;
                        con.Open();

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                Stocklist.Add(new Stock
                                {
                                    PluName = Convert.ToString(rd["PluName"]),
                                    Id = Convert.ToInt32(rd["Id"]),
                                    Barcode = Convert.ToString(rd["Barcode"]),
                                    maincategory = Convert.ToString(rd["maincategory"]),
                                    category = Convert.ToString(rd["category"]),
                                    StockQty = Convert.ToString(rd["StockQty"]),
                                    SellingPrice = Convert.ToDecimal(rd["SellingPrice"]),
                                    Approval = Convert.ToString(rd["Approval"]),
                                    ItemId = Convert.ToString(rd["ItemId"]),
                                    priceId = Convert.ToString(rd["PriceId"]),
                                    size = Convert.ToString(rd["size"]),
                                    IsVisible = Convert.ToInt32(rd["IsVisible"]),
                                    ShortCode = Convert.ToString(rd["ShortCode"]),
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return Stocklist;
                }
            }
        }
        public List<SelectListItem> GetMainCategoryList(string id)
        {
            try
            {
                List<SelectListItem> GetCategorySelectList = new List<SelectListItem>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_MainCatlistforStock]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string text = Convert.ToString(rd["Name"]);
                            string Id = Convert.ToString(rd["MainCategoryId"]);
                            GetCategorySelectList.Add(new SelectListItem()
                            {
                                Text = text,
                                Value = Id,
                            });
                        }
                        return GetCategorySelectList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int UpdateItemIsVisiable(Stock st)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Usp_update_stock_Visiable]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = st.ItemId;
                        cmd.Parameters.Add("@priceId", SqlDbType.VarChar).Value = st.priceId;
                        cmd.Parameters.Add("@Isvisable", SqlDbType.Int).Value = st.IsVisible;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = st.Hub;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }

        public int UpdateStock(Stock st)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Usp_update_stock_stock]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = st.ItemId;
                        cmd.Parameters.Add("@priceId", SqlDbType.VarChar).Value = st.priceId;
                        cmd.Parameters.Add("@stock", SqlDbType.Int).Value = st.StockQty;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = st.Hub;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception e)
                    {
                        return 0;
                    }                   
                }
            }
        }

        public int UpdatePrice(Stock st)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Usp_update_stock_sellingprice]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = st.ItemId;
                        cmd.Parameters.Add("@priceId", SqlDbType.VarChar).Value = st.priceId;
                        cmd.Parameters.Add("@sellingprice", SqlDbType.Decimal).Value = st.SellingPrice;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = st.Hub;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception e)
                    {
                        return 0;
                    }
                   
                }
            }
        }

        public byte[] ExportExcelofStock(string hubId, string role, string webRootPath, string maincategory, string Category, int type)
        {
            string fileName = Path.Combine(webRootPath, "StockInfo.xlsx");
            FileInfo newFile = new FileInfo(fileName);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(fileName);
            }
            Stream stream = new System.IO.MemoryStream();
            using (var package = new ExcelPackage(newFile))
            {
                var worksheet1 = package.Workbook.Worksheets.Add("StockInfo");
                //IQueryable<Lead> leadList = null;
                List<Stock> Stock = new List<Stock>();
                Stock = GetStockItemlist(maincategory, Category, type, hubId);
                int rowCount = 1;
                var rowdetail = rowCount;
                #region ExcelHeader

                worksheet1.Cells[rowdetail, 1].Value = "ItemName";
                worksheet1.Cells[rowdetail, 1].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(1).AutoFit();

                worksheet1.Cells[rowdetail, 2].Value = "ItemBarcode";
                worksheet1.Cells[rowdetail, 2].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(2).AutoFit();

                worksheet1.Cells[rowdetail, 3].Value = "Category";
                worksheet1.Cells[rowdetail, 3].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(3).AutoFit();


                worksheet1.Cells[rowdetail, 4].Value = "SubCategory";
                worksheet1.Cells[rowdetail, 4].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(4).AutoFit();



                worksheet1.Cells[rowdetail, 5].Value = "StockCount";
                worksheet1.Cells[rowdetail, 5].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(5).AutoFit();


                worksheet1.Cells[rowdetail, 6].Value = "SellingPrice";
                worksheet1.Cells[rowdetail, 6].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(6).AutoFit();


                #endregion

                rowdetail++;
                foreach (var stocklist in Stock)
                {
                    worksheet1.Cells[rowdetail, 1].Value = stocklist.PluName;
                    worksheet1.Cells[rowdetail, 2].Value = stocklist.Barcode;
                    worksheet1.Cells[rowdetail, 3].Value = stocklist.maincategory;
                    worksheet1.Cells[rowdetail, 4].Value = stocklist.category;
                    worksheet1.Cells[rowdetail, 5].Value = stocklist.StockQty;
                    worksheet1.Cells[rowdetail, 6].Value = stocklist.SellingPrice;
                    rowdetail++;
                }

                package.SaveAs(newFile);
                byte[] files = File.ReadAllBytes(fileName);
                File.Delete(fileName);
                return files;
            }
        }

        public List<SelectListItem> GetHeaderList(string rootFolder, string fileName)
        {
            try
            {
                List<SelectListItem> headerList = new List<SelectListItem>();
                FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                    int ColCount = workSheet.Dimension.Columns;

                    for (int i = 1; i <= ColCount; i++)
                    {
                        var result = Convert.ToString(workSheet.Cells[1, i].Value);
                        if (string.IsNullOrEmpty(result))
                        {
                            break;
                        }
                        headerList.Add(new SelectListItem { Text = result, Value = i.ToString() });
                    }
                }
                return headerList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int InsertList(TblListcs list)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Usp_Stock_Logs]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = list.FileName;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = list.CreatedBy;
                        con.Open();

                        var rd = cmd.ExecuteReader();
                        var i = 0;
                        if (rd.Read())
                        {
                            i = Convert.ToInt32(rd["ListId"]);
                            if (i > 0)
                            {
                                return i;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        return Convert.ToInt32(i);

                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }
        
        public  DataTable ReadExcelData(TblListcs camp)
        {
            DataTable existingData = new DataTable();
            FileInfo file = new FileInfo(Path.Combine(camp.path, camp.FileName));
            using (var package = new ExcelPackage(file))
            {
                // Assuming that the data starts from the first row of the first worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                // Loop through rows and columns to read data
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    // Use the header text as the column name
                    string columnName = worksheet.Cells[1, col].Text;
                    existingData.Columns.Add(columnName);
                }

                // Start reading data from the second row since the first row contains headers
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    DataRow dataRow = existingData.NewRow();

                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        // Populate the data row with cell values
                        dataRow[col - 1] = worksheet.Cells[row, col].Text;
                    }

                    existingData.Rows.Add(dataRow);
                }
            }

            return existingData;
        }

        public string UploadStatgingListFromExcels(TblListcs camp)
        {
            try
            {
                List<int> cUserlist = new List<int>();
                FileInfo file = new FileInfo(Path.Combine(camp.path, camp.FileName));
                using (ExcelPackage package = new ExcelPackage(file))
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                {
                    con.Open(); // Open the database connection
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                    int totalRows = workSheet.Dimension.Rows;

                    using (SqlCommand cmd = new SqlCommand("[dbo].[UploadDataFromExcel_CreateStagingStock]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter Barcode = cmd.Parameters.Add("@Barcode", SqlDbType.VarChar, 80);
                        SqlParameter StockQty = cmd.Parameters.Add("@StockQty", SqlDbType.Int);
                        SqlParameter SellingPrice = cmd.Parameters.Add("@SellingPrice", SqlDbType.Decimal);
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = camp.CreatedBy;
                        cmd.Parameters.Add("@ListId", SqlDbType.Int).Value = camp.ListId;

                        for (int i = 2; i <= totalRows; i++)
                        {
                            if (camp.HeaderValues[0] != 0)
                            {
                                Barcode.Value = Convert.ToString(workSheet.Cells[i, camp.HeaderValues[0]].Value);
                            }
                            else
                            {
                                Barcode.Value = DBNull.Value;
                            }

                            if (camp.HeaderValues[1] != 0)
                            {
                                int stockQty;
                                if (int.TryParse(Convert.ToString(workSheet.Cells[i, camp.HeaderValues[1]].Value), out stockQty))
                                {
                                    StockQty.Value = stockQty;
                                }
                                else
                                {
                                    StockQty.Value = DBNull.Value;
                                }
                            }
                            else
                            {
                                StockQty.Value = DBNull.Value;
                            }

                            if (camp.HeaderValues[2] != 0)
                            {
                                decimal sellingPrice;
                                if (decimal.TryParse(Convert.ToString(workSheet.Cells[i, camp.HeaderValues[2]].Value), out sellingPrice))
                                {
                                    SellingPrice.Value = sellingPrice;
                                }
                                else
                                {
                                    SellingPrice.Value = DBNull.Value;
                                }
                            }
                            else
                            {
                                SellingPrice.Value = DBNull.Value;
                            }

                            cmd.ExecuteNonQuery(); // Execute the SQL command for each row
                        }
                    }

                    return "0";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "1";
            }
        }
        public int UploadStaggingToStock(TblListcs camp)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_BulkStockUpdate]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ListId", SqlDbType.Int).Value = camp.ListId;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = camp.hubId;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            var i = 0;
                            if (rd.Read())
                            {
                                i = Convert.ToInt32(rd["SuccessfulCount"]);
                                if (i > 0)
                                {
                                    return i;
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            return Convert.ToInt32(i);
                        }
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }
        public async Task<TblListcs> GetCountDetail(TblListcs camp)
        {
            var RejStock = new TblListcs();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_GetStockRejectedCount]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ListId", SqlDbType.VarChar).Value = camp.ListId;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                RejStock.RejectedCount = Convert.ToInt32(dr["RejectedCount"]);
                            }
                        }
                        return RejStock;
                    }
                    catch (Exception e)
                    {
                        return RejStock;
                    }
                }
            }            
        }
        public byte[] ExportRejectedData(string hubId, string role, string webRootPath, int ListId)
        {
            string fileName = Path.Combine(webRootPath, "StockInfo.xlsx");
            FileInfo newFile = new FileInfo(fileName);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(fileName);
            }
            Stream stream = new System.IO.MemoryStream();
            using (var package = new ExcelPackage(newFile))
            {
                var worksheet1 = package.Workbook.Worksheets.Add("StockInfo");
                //IQueryable<Lead> leadList = null;
                List<Stock> Stock = new List<Stock>();
                Stock = GetRejectedStockData(ListId);
                //var DeleteStagging = DeleteStockStaggingData(ListId);
                int rowCount = 1;
                var rowdetail = rowCount;
                #region ExcelHeader

                worksheet1.Cells[rowdetail, 1].Value = "ItemBarcode";
                worksheet1.Cells[rowdetail, 1].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(1).AutoFit();


                worksheet1.Cells[rowdetail, 2].Value = "StockCount";
                worksheet1.Cells[rowdetail, 2].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(2).AutoFit();


                worksheet1.Cells[rowdetail, 3].Value = "SellingPrice";
                worksheet1.Cells[rowdetail, 3].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(3).AutoFit();
                
                worksheet1.Cells[rowdetail, 4].Value = "RejectedReason";
                worksheet1.Cells[rowdetail, 4].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(4).AutoFit();


                #endregion

                rowdetail++;
                foreach (var stocklist in Stock)
                {
                    worksheet1.Cells[rowdetail, 1].Value = stocklist.Barcode;
                    worksheet1.Cells[rowdetail, 2].Value = stocklist.StockQty;
                    worksheet1.Cells[rowdetail, 3].Value = stocklist.SellingPrice;
                    worksheet1.Cells[rowdetail, 4].Value = stocklist.RejectedReason;
                    rowdetail++;
                }

                package.SaveAs(newFile);
                byte[] files = File.ReadAllBytes(fileName);
                File.Delete(fileName);
                return files;
            }
        }
        public List<Stock> GetRejectedStockData(int ListId)
        {
            List<Stock> Stocklist = new List<Stock>();

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_GetStockRejectData]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ListId", SqlDbType.Int).Value = ListId;
                        con.Open();

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                Stocklist.Add(new Stock
                                {
                                    Barcode = Convert.ToString(rd["Barcode"]),
                                    StockQty = Convert.ToString(rd["StockQut"]),
                                    SellingPrice = Convert.ToDecimal(rd["SellingPrrice"]),
                                    RejectedReason = Convert.ToString(rd["RejectedReason"]),
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return Stocklist;
                }
            }
        }

        public int DeleteStockStaggingData()
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_DeleteStockStaggingDataandRejectedData]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
        