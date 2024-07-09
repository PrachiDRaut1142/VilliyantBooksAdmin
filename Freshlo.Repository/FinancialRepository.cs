using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.RI;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Repository
{
    public class FinancialRepository:IFinancialRI
    {
        public FinancialRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        private IDbConfig _dbConfig { get; }
        public int CreateFinance(Finance info)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Financial_Create]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Entry_Type", SqlDbType.Int).Value = info.Entry_Type;
                cmd.Parameters.Add("@Inward_Payment_Type", SqlDbType.Int).Value = info.Inward_Payment_Type;
                cmd.Parameters.Add("@Outward_Payment_Type", SqlDbType.Int).Value = info.Outward_Payment_Type;
                cmd.Parameters.Add("@Other_Payment", SqlDbType.VarChar, 100).Value =info.Other_Payment;
                cmd.Parameters.Add("@Payment_Status", SqlDbType.Int).Value = info.Payment_Status;
                cmd.Parameters.Add("@Reference_No", SqlDbType.VarChar, 100).Value = info.Reference_No;
                cmd.Parameters.Add("@Received_From", SqlDbType.VarChar, 100).Value = info.Received_From;
                cmd.Parameters.Add("@Paid_To", SqlDbType.VarChar, 100).Value = info.Paid_To;
                cmd.Parameters.Add("@Payment_Mode", SqlDbType.Int).Value = info.Payment_Mode;
                cmd.Parameters.Add("@Transaction_On", SqlDbType.DateTime).Value = info.Paid_On;
                cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 100).Value = info.Remark == null ? (object)DBNull.Value : info.Remark;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = info.Created_By;
                cmd.Parameters.Add("@Total_Amount", SqlDbType.Int).Value = info.Total_Amount;
                cmd.Parameters.Add("@Partial_Amount", SqlDbType.Int).Value = info.Partail_Amount;

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public Finance GetFinanceDetail(int id, int opt)
        {
            var detail = new Finance();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())

                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Financial_GetDetailById]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@opt", SqlDbType.Int).Value = opt;
                    con.Open();
                    if (opt == 1)
                    {
                        try
                        {
                            using (SqlDataReader rd = cmd.ExecuteReader())
                            {
                                if (rd.Read())
                                {
                                    detail.Id = Convert.ToInt32(rd["id"]);
                                    detail.Entry_Type = Convert.ToInt32(rd["Entry_Type"]);
                                    detail.Entry_Type_Desc = Convert.ToString(rd["Entry_Type_Desc"]);
                                    detail.Inward_Payment_Type = Convert.ToInt32(rd["Inward_Payment_Type"]);
                                    detail.Outward_Payment_Type = Convert.ToInt32(rd["Outward_Payment_Type"]);
                                    detail.Other_Payment = Convert.ToString(rd["Other_Payment"]);
                                    detail.Payment_Status = Convert.ToInt32(rd["Payment_Status"]);
                                    detail.Payment_Status_Desc = Convert.ToString(rd["Payment_Status_Desc"]);
                                    detail.Reference_No = Convert.ToString(rd["Reference_No"]);
                                    detail.Received_From = Convert.ToString(rd["Received_From"]);
                                    detail.Paid_To = Convert.ToString(rd["Paid_To"]);
                                    detail.Payment_Mode = Convert.ToInt32(rd["Payment_Mode"]);
                                    detail.Payment_Mode_Desc = Convert.ToString(rd["Payment_Mode_Desc"]);
                                    detail.Transaction_On = Convert.ToDateTime(rd["Transaction_On"]);
                                    detail.Remark = Convert.ToString(rd["Remark"]);
                                    detail.Created_On = Convert.ToDateTime(rd["Created_On"]);
                                    detail.Created_By = rd["Created_By"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Created_By"]);
                                    detail.Updated_On = rd["Updated_On"] == DBNull.Value ? Convert.ToDateTime("01-01-2020 12:20") : Convert.ToDateTime(rd["Updated_On"]);
                                    detail.Updated_By = rd["Updated_By"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Updated_By"]);
                                    detail.Total_Amount = Convert.ToSingle(rd["Total_Amount_Paid"]);
                                    detail.Partail_Amount = Convert.ToSingle(rd["Partial_Amount_Paid"]);
                                    detail.FullName = Convert.ToString(rd["FullName"]);

                            }
                                return detail;
                            }
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }

                    }
                    else
                    {
                        try
                        {
                            using (SqlDataReader rd = cmd.ExecuteReader())
                            {
                                if (rd.Read())
                                {
                                    detail.Id = Convert.ToInt32(rd["id"]);
                                    detail.Entry_Type = Convert.ToInt32(rd["Entry_Type"]);
                                    detail.Entry_Type_Desc = Convert.ToString(rd["Entry_Type_Desc"]);
                                    detail.Inward_Payment_Type = Convert.ToInt32(rd["Inward_Payment_Type"]);
                                    detail.Outward_Payment_Type = Convert.ToInt32(rd["Outward_Payment_Type"]);
                                    detail.Other_Payment = Convert.ToString(rd["Other_Payment"]);
                                    detail.Payment_Status = Convert.ToInt32(rd["Payment_Status"]);
                                    detail.Payment_Status_Desc = Convert.ToString(rd["Payment_Status_Desc"]);
                                    detail.Reference_No = Convert.ToString(rd["Reference_No"]);
                                    detail.Received_From = Convert.ToString(rd["Received_From"]);
                                    detail.Paid_To = Convert.ToString(rd["Paid_To"]);
                                    detail.Payment_Mode = Convert.ToInt32(rd["Payment_Mode"]);
                                    detail.Payment_Mode_Desc = Convert.ToString(rd["Payment_Mode_Desc"]);
                                    detail.Transaction_On = Convert.ToDateTime(rd["Transaction_On"]);
                                    detail.Remark = Convert.ToString(rd["Remark"]);
                                    detail.Created_On = Convert.ToDateTime(rd["Created_On"]);
                                    detail.Created_By = rd["Created_By"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Created_By"]);
                                    detail.Updated_On = rd["Updated_On"] == DBNull.Value ? Convert.ToDateTime("01-01-2020 12:20") : Convert.ToDateTime(rd["Updated_On"]);
                                    detail.Updated_By = rd["Updated_By"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Updated_By"]);
                                    detail.Total_Amount = Convert.ToSingle(rd["Total_Amount_Paid"]);
                                    detail.Partail_Amount = Convert.ToSingle(rd["Partial_Amount_Paid"]);
                                    detail.FullName = Convert.ToString(rd["FullName"]);

                                }
                                return detail;
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

        public List<Finance> GetManage(string paid_From ,string paid_Till)
        {
            List<Finance> list = new List<Finance>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Financial_GetManage]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Paid_From", SqlDbType.VarChar,50).Value = paid_From;
                cmd.Parameters.Add("@Paid_Till", SqlDbType.VarChar,50).Value = paid_Till;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Finance
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            Entry_Type = Convert.ToInt32(rd["Entry_Type"]),
                            Entry_Type_Desc = Convert.ToString(rd["Entry_Type_Desc"] == DBNull.Value ? null : rd["Entry_Type_Desc"]),
                            Inward_Payment_Type = Convert.ToInt32(rd["Inward_Payment_Type"]),
                            Outward_Payment_Type = Convert.ToInt32(rd["Outward_Payment_Type"]),
                            Paid_To = Convert.ToString(rd["Paid_To"] == DBNull.Value ? null : rd["Paid_To"]),
                            Payment_Status_Desc = Convert.ToString(rd["Payment_Status_Desc"] == DBNull.Value ? null : rd["Payment_Status_Desc"]),
                            Paid_On = Convert.ToDateTime(rd["Transaction_On"]),
                            Remark = Convert.ToString(rd["Remark"] == DBNull.Value ? null : rd["Remark"]),
                            Total_Amount = Convert.ToInt32(rd["Total_Amount_Paid"]),
                            Partail_Amount = Convert.ToInt32(rd["Partial_Amount_Paid"]),
                            Received_From = Convert.ToString(rd["Received_From"] == DBNull.Value ? null : rd["Received_From"]),
                            Payment_Status = Convert.ToInt32(rd["Payment_Status"]),
                            Created_On= Convert.ToDateTime(rd["Created_On"]),
                            
                        });
                    }
                    return list;
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
                cmd.CommandText = "[dbo].[Financial_GetSummary]";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    if (rd.Read())
                    {
                        about = new SummayData
                        {
                            TodaysInn = Convert.ToInt32(rd["TodaysInn"]),
                            TodaysOut = Convert.ToInt32(rd["TodaysOut"]),
                            WeeklyInn = Convert.ToInt32(rd["WeeklyInn"]),
                            WeeklyOut = Convert.ToInt32(rd["WeeklyOut"]),
                            MonthlyInn = Convert.ToInt32(rd["MonthlyInn"]),
                            MonthlyOut = Convert.ToInt32(rd["MonthlyOut"]),


                        };
                    }
                    return about;
                }
            }

        }
        public async Task<byte[]> ExportExcelofFinance(string webRootPath, string paid_From, string paid_Till)
        {

            string fileName = Path.Combine(webRootPath, "FinancialView.xlsx");
            FileInfo newFile = new FileInfo(fileName);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(fileName);
            }
            Stream stream = new System.IO.MemoryStream();
            using (var package = new ExcelPackage(newFile))
            {

                var worksheet1 = package.Workbook.Worksheets.Add("In_ward");
                var worksheet2 = package.Workbook.Worksheets.Add("Out_ward");
                //IQueryable<Lead> leadList = null;
                var FinanceData = GetManage(paid_From, paid_Till);
                IEnumerable<Finance> inwardList = FinanceData.Where(x => x.Entry_Type==1);
                IEnumerable<Finance> outwardList = FinanceData.Where(x => x.Entry_Type == 2);

                Task<SummayData> summaryDetail = GetSummaryData();

                int rowCount = 2;

                #region InwardDetail
                worksheet1.Cells[1, 1].Value = "Todays Inn";
                worksheet1.Cells[1, 1].Style.Font.Bold = true;
                worksheet1.Cells[1, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(1).AutoFit();

                worksheet1.Cells[1, 2].Value = "Weekly Inn";
                worksheet1.Cells[1, 2].Style.Font.Bold = true;
                worksheet1.Cells[1, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(2).AutoFit();

                worksheet1.Cells[1, 3].Value = "Monthly Inn";
                worksheet1.Cells[1, 3].Style.Font.Bold = true;
                worksheet1.Cells[1, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(3).AutoFit();

                rowCount++;
                worksheet1.Cells[rowCount, 1].Value ="Rs. "+summaryDetail.Result.TodaysInn;
                worksheet1.Cells[rowCount, 2].Value = "Rs. " + summaryDetail.Result.WeeklyInn;
                worksheet1.Cells[rowCount, 3].Value = "Rs. " + summaryDetail.Result.MonthlyInn;
                #endregion
                var rowdetail = rowCount + 2;

                #region InnwardList
                worksheet1.Cells[rowdetail, 1].Value = "Entry ID";
                worksheet1.Cells[rowdetail, 1].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(1).AutoFit();

                worksheet1.Cells[rowdetail, 2].Value = "Payment Person";
                worksheet1.Cells[rowdetail, 2].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(3).AutoFit();

                worksheet1.Cells[rowdetail, 3].Value = "Payment";
                worksheet1.Cells[rowdetail, 3].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(4).AutoFit();

                worksheet1.Cells[rowdetail, 4].Value = "Total Amount";
                worksheet1.Cells[rowdetail, 4].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(5).AutoFit();

                worksheet1.Cells[rowdetail, 5].Value = "Partial Amount";
                worksheet1.Cells[rowdetail, 5].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(5).AutoFit();


                worksheet1.Cells[rowdetail, 6].Value = "Payment Date";
                worksheet1.Cells[rowdetail, 6].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(5).AutoFit();



                worksheet1.Cells[rowdetail, 7].Value = "Comments";
                worksheet1.Cells[rowdetail, 7].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(6).AutoFit();


                worksheet1.Cells[rowdetail, 8].Value = "Created On";
                worksheet1.Cells[rowdetail, 8].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(7).AutoFit();
                #endregion

                rowdetail++;
                foreach (var inward in inwardList)
                {
                    worksheet1.Cells[rowdetail, 1].Value = "F"+inward.Id;
                    worksheet1.Cells[rowdetail, 2].Value = inward.Received_From;
                    worksheet1.Cells[rowdetail, 3].Value = "Rs. "+inward.Total_Amount;
                    worksheet1.Cells[rowdetail, 4].Value = "Rs. "+inward.Partail_Amount;
                    worksheet1.Cells[rowdetail, 5].Value = inward.Payment_Status_Desc;
                    worksheet1.Cells[rowdetail, 6].Value = inward.Paid_On.ToString("dd-MMM-yyyy"); ;
                    worksheet1.Cells[rowdetail, 7].Value = inward.Remark;
                    worksheet1.Cells[rowdetail, 8].Value = inward.Created_On.ToString("MM-dd-yyyy hh:mm");

                      rowdetail++;
                }
                #region sheet2
                int rowCount2 = 2;

                #region OutwardDetail
                worksheet2.Cells[1, 1].Value = "Todays Out";
                worksheet2.Cells[1, 1].Style.Font.Bold = true;
                worksheet2.Cells[1, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet2.Column(1).AutoFit();

                worksheet2.Cells[1, 2].Value = "Weekly Out";
                worksheet2.Cells[1, 2].Style.Font.Bold = true;
                worksheet2.Cells[1, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet2.Column(2).AutoFit();

                worksheet2.Cells[1, 3].Value = "Monthly Out";
                worksheet2.Cells[1, 3].Style.Font.Bold = true;
                worksheet2.Cells[1, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet2.Column(3).AutoFit();


                rowCount2++;
                worksheet2.Cells[rowCount2, 1].Value = "Rs. " + summaryDetail.Result.TodaysOut;
                worksheet2.Cells[rowCount2, 2].Value = "Rs. " + summaryDetail.Result.WeeklyOut;
                worksheet2.Cells[rowCount2, 3].Value = "Rs. " + summaryDetail.Result.MonthlyOut;

                #endregion
                var rowdetail2 = rowCount2 + 2;

                #region OutwardList
                worksheet2.Cells[rowdetail2, 1].Value = "Entry ID";
                worksheet2.Cells[rowdetail2, 1].Style.Font.Bold = true;
                worksheet2.Cells[rowdetail2, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet2.Column(1).AutoFit();

                worksheet2.Cells[rowdetail2, 2].Value = "Payment Person";
                worksheet2.Cells[rowdetail2, 2].Style.Font.Bold = true;
                worksheet2.Cells[rowdetail2, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet2.Column(3).AutoFit();

                worksheet2.Cells[rowdetail2, 3].Value = "Total Amount";
                worksheet2.Cells[rowdetail2, 3].Style.Font.Bold = true;
                worksheet2.Cells[rowdetail2, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet2.Column(5).AutoFit();

                worksheet2.Cells[rowdetail2, 4].Value = "Partial Amount";
                worksheet2.Cells[rowdetail2, 4].Style.Font.Bold = true;
                worksheet2.Cells[rowdetail2, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet2.Column(5).AutoFit();


                worksheet2.Cells[rowdetail2, 5].Value = "Payment";
                worksheet2.Cells[rowdetail2, 5].Style.Font.Bold = true;
                worksheet2.Cells[rowdetail2, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet2.Column(4).AutoFit();


                worksheet2.Cells[rowdetail2, 6].Value = "Payment Date";
                worksheet2.Cells[rowdetail2, 6].Style.Font.Bold = true;
                worksheet2.Cells[rowdetail2, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet2.Column(5).AutoFit();



                worksheet2.Cells[rowdetail2, 7].Value = "Comments";
                worksheet2.Cells[rowdetail2, 7].Style.Font.Bold = true;
                worksheet2.Cells[rowdetail2, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet2.Column(6).AutoFit();


                worksheet2.Cells[rowdetail2, 8].Value = "Created On";
                worksheet2.Cells[rowdetail2, 8].Style.Font.Bold = true;
                worksheet2.Cells[rowdetail2, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet2.Column(7).AutoFit();

                #endregion

                rowdetail2++;
                foreach (var outward in outwardList)
                {
                    worksheet2.Cells[rowdetail2, 1].Value ="F"+outward.Id;
                    worksheet2.Cells[rowdetail2, 2].Value = outward.Paid_To;
                    worksheet2.Cells[rowdetail2, 3].Value = "Rs. "+outward.Total_Amount;
                    worksheet2.Cells[rowdetail2, 4].Value = "Rs. "+outward.Partail_Amount;
                    worksheet2.Cells[rowdetail2, 5].Value = outward.Payment_Status_Desc;
                    worksheet2.Cells[rowdetail2, 6].Value = outward.Paid_On.ToString("dd-MMMM-yyyy");
                    worksheet2.Cells[rowdetail2, 7].Value = outward.Remark;
                    worksheet2.Cells[rowdetail2, 8].Value = outward.Created_On.ToString("MM-dd-yyyy hh:mm");

                    rowdetail2++;
                }
                #endregion sheet2


                package.SaveAs(newFile);
                byte[] files = File.ReadAllBytes(fileName);
                File.Delete(fileName);
                return files;
            }

        }

        public int UpdateFinancialDetail(Finance info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Financial_Update]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = info.Id;
                        cmd.Parameters.Add("@Entry_Type", SqlDbType.Int).Value = info.Entry_Type;
                        cmd.Parameters.Add("@Inward_Payment_Type", SqlDbType.Int).Value = info.Inward_Payment_Type;
                        cmd.Parameters.Add("@Outward_Payment_Type", SqlDbType.Int).Value = info.Outward_Payment_Type;
                        cmd.Parameters.Add("@Other_Payment", SqlDbType.VarChar, 100).Value = info.Other_Payment;
                        cmd.Parameters.Add("@Payment_Status", SqlDbType.Int).Value = info.Payment_Status;
                        cmd.Parameters.Add("@Reference_No", SqlDbType.VarChar, 100).Value = info.Reference_No;
                        cmd.Parameters.Add("@Received_From", SqlDbType.VarChar, 100).Value = info.Received_From;
                        cmd.Parameters.Add("@Paid_To", SqlDbType.VarChar, 100).Value = info.Paid_To;
                        //cmd.Parameters.Add("@Other_Payment", SqlDbType.VarChar, 100).Value = info.Other_Payment;
                        cmd.Parameters.Add("@Payment_Mode", SqlDbType.Int).Value = info.Payment_Mode;
                        cmd.Parameters.Add("@Transaction_On", SqlDbType.DateTime).Value = info.Transaction_On;
                        cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 100).Value = info.Remark == null ? (object)DBNull.Value : info.Remark;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = info.Updated_By;
                        cmd.Parameters.Add("@Total_Amount", SqlDbType.Int).Value = info.Total_Amount;
                        cmd.Parameters.Add("@Partial_Amount", SqlDbType.Int).Value = info.Partail_Amount;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());


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
