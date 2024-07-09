using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
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
    public class SaleSummaryRepository : SaleSummaryRI
    {
        public SaleSummaryRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        private IDbConfig _dbConfig { get; }

        public List<SaleSummary> GetCardSummary(string datefrom, string dateto, string id)
        {
            var cardList = new List<SaleSummary>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[CardSummary_Barchart]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@startdate", datefrom);
                        cmd.Parameters.AddWithValue("@enddate", dateto);
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cardList.Add(new SaleSummary
                            {
                                CreatedDate = Convert.ToString(reader["CreatedDate"]),
                                TotalPrice = Convert.ToSingle(reader["TotalPrice"] == DBNull.Value ? 00 : Convert.ToSingle(reader["TotalPrice"])),
                            });
                        }
                        sqlcon.Close();
                        return cardList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public List<SaleSummary> GetCashSummary(string datefrom, string dateto, string id)
        {
            var cashList = new List<SaleSummary>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[CashSummary_Barchart]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@startdate", datefrom);
                        cmd.Parameters.AddWithValue("@enddate", dateto);
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cashList.Add(new SaleSummary
                            {
                                CreatedDate = Convert.ToString(reader["CreatedDate"]),
                                TotalPrice = Convert.ToSingle(reader["TotalPrice"] == DBNull.Value ? 00 : Convert.ToSingle(reader["TotalPrice"])),
                            });
                        }
                        sqlcon.Close();
                        return cashList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public List<SaleSummary> GetDiscountSummary(string datefrom, string dateto,string id)
        {
            var Discountlist = new List<SaleSummary>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[Discount_LineChart]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@startdate", datefrom);
                        cmd.Parameters.AddWithValue("@enddate", dateto);
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Discountlist.Add(new SaleSummary
                            {
                                CreatedDate = Convert.ToString(reader["CreatedDate"]),
                                Discount = Convert.ToSingle(reader["Discount"] == DBNull.Value ? 00 : Convert.ToSingle(reader["Discount"])),
                            });
                        }
                        sqlcon.Close();
                        return Discountlist;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public List<SaleSummary> GetmonthCardSummary(string datefrom, string dateto, string id)
        {
            var cardList = new List<SaleSummary>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[MonthlyCardRevenue]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@startdate", datefrom);
                        cmd.Parameters.AddWithValue("@enddate", dateto);
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cardList.Add(new SaleSummary
                            {
                                Month = Convert.ToString(reader["Month"]),
                                Year = Convert.ToInt32(reader["Year"]),
                                TotalPrice = Convert.ToSingle(reader["Totalrevenue"] == DBNull.Value ? 00 : Convert.ToSingle(reader["Totalrevenue"])),
                            });
                        }
                        sqlcon.Close();
                        return cardList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public List<SaleSummary> GetmonthCashSummary(string datefrom, string dateto, string id)
        {
            var cashlist = new List<SaleSummary>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[MonthlyCashRevenue]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@startdate", datefrom);
                        cmd.Parameters.AddWithValue("@enddate", dateto);
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cashlist.Add(new SaleSummary
                            {
                                Month = Convert.ToString(reader["Month"]),
                                Year = Convert.ToInt32(reader["Year"]),
                                TotalPrice = Convert.ToSingle(reader["Totalrevenue"] == DBNull.Value ? 00 : Convert.ToSingle(reader["Totalrevenue"])),
                            });
                        }
                        sqlcon.Close();
                        return cashlist;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

            public List<SaleSummary> GetmonthPendingSummary(string datefrom, string dateto, string id)
        {
            var Pendinglist = new List<SaleSummary>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[MonthlyPendingRevenue]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@startdate", datefrom);
                        cmd.Parameters.AddWithValue("@enddate", dateto);
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Pendinglist.Add(new SaleSummary
                            {
                                Month = Convert.ToString(reader["Month"]),
                                Year = Convert.ToInt32(reader["Year"]),
                                TotalPrice = Convert.ToSingle(reader["Totalrevenue"] == DBNull.Value ? 00 : Convert.ToSingle(reader["Totalrevenue"])),
                            });
                        }
                        sqlcon.Close();
                        return Pendinglist;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public List<SaleSummary> GetmonthUpiSummary(string datefrom, string dateto, string id)
        {
            var upilist = new List<SaleSummary>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[MonthlyUPIRevenue]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@startdate", datefrom);
                        cmd.Parameters.AddWithValue("@enddate", dateto);
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            upilist.Add(new SaleSummary
                            {
                                Month = Convert.ToString(reader["Month"]),
                                Year = Convert.ToInt32(reader["Year"]),
                                TotalPrice = Convert.ToSingle(reader["Totalrevenue"] == DBNull.Value ? 00 : Convert.ToSingle(reader["Totalrevenue"])),
                            });
                        }
                        sqlcon.Close();
                        return upilist;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public List<SaleSummary> GetPendingSummary(string datefrom, string dateto,string id)
        {
            var pendingList = new List<SaleSummary>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[PendingSummary_Barchart]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@startdate", datefrom);
                        cmd.Parameters.AddWithValue("@enddate", dateto);
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            pendingList.Add(new SaleSummary
                            {
                                CreatedDate = Convert.ToString(reader["CreatedDate"]),
                                TotalPrice = Convert.ToSingle(reader["TotalPrice"] == DBNull.Value ? 00 : Convert.ToSingle(reader["TotalPrice"])),
                            });
                        }
                        sqlcon.Close();
                        return pendingList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public List<SaleSummary> GetUpiSummary(string datefrom, string dateto,string id)
        {
            var upiList = new List<SaleSummary>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[UPISummary_Barchart]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@startdate", datefrom);
                        cmd.Parameters.AddWithValue("@enddate", dateto);
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            upiList.Add(new SaleSummary
                            {
                                CreatedDate = Convert.ToString(reader["CreatedDate"]),
                                TotalPrice = Convert.ToSingle(reader["TotalPrice"] == DBNull.Value ? 00 : Convert.ToSingle(reader["TotalPrice"])),
                            });
                        }
                        sqlcon.Close();
                        return upiList;
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

