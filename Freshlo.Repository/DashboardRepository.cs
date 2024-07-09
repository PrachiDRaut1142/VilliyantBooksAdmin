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
    public class DashboardRepository : DashboardRI
    {
        public DashboardRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        private IDbConfig _dbConfig { get; }

        // Actual Code Used
        public DashboardCount GetAllDashboardCount(string datefrom, string dateto, string hubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                DashboardCount finacialStatistics = new DashboardCount();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    //cmd.CommandText = "[dbo].[Dashboard_NewFinancialData]";
                    cmd.CommandText = "[dbo].[Dashboard_NewFinancialDataHubSperation]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@startdate", datefrom);
                    cmd.Parameters.AddWithValue("@enddate", dateto);
                    cmd.Parameters.AddWithValue("@hubId", hubId);
                    con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {

                            try
                            {
                                if (sdr.Read())
                                {

                                    finacialStatistics = new DashboardCount
                                    {
                                        TotalOrderReceived = Convert.ToInt32(sdr["TotalOrderReceived"]),
                                        TotalOrderReceivedTotalAmountCount = Convert.ToInt32(sdr["TotalOrderReceivedTotalAmountCount"]),
                                        TotalOrderReceivedAmount = Convert.ToDouble(sdr["TotalOrderReceivedAmount1"]),
                                        TotalPendingOrder = Convert.ToInt32(sdr["TotalPendingOrder"]),
                                        TotalpendingPaymentCount = Convert.ToInt32(sdr["TotalpendingPaymentCount"]),
                                        TotalpendingPaymentAmount = Convert.ToDouble(sdr["TotalpendingPaymentAmount1"]),
                                        TotalOrderDelivered = Convert.ToInt32(sdr["TotalOrderDelivered"]),
                                        TotalOrderDeliveredTotalAmountCount = Convert.ToInt32(sdr["TotalOrderDeliveredTotalAmountCount"]),
                                        TotalOrderDeliveredAmount = Convert.ToDouble(sdr["TotalOrderDeliveredAmount1"]),
                                        TotalOrderCancelled = Convert.ToInt32(sdr["TotalOrderCancelled"]),
                                        TotalOrderCancelledTotalAmountCount = Convert.ToInt32(sdr["TotalOrderCancelledTotalAmountCount"]),
                                        TotalOrderCancelledAmount = Convert.ToDouble(sdr["TotalOrderCancelledAmount1"]),
                                        Todaypaymentcount = Convert.ToInt32(sdr["TotalTodaysPendingPaymentCount"]),
                                        Todaypaymentamount = Convert.ToInt32(sdr["TodayTotalPendingPaymentAmount"]),
                                        TotalItemPurchaseSum = Convert.ToInt32(sdr["TotalItemPurchaseSum"]),
                                        TotalItemPurchaseCount = Convert.ToInt32(sdr["TotalItemPurchaseCount"]),
                                        TotalAmountPurchaseSpent = Convert.ToInt32(sdr["TotalAmountPurchaseSpent"]),
                                        TotalAmountPurchaseSpentCount = Convert.ToInt32(sdr["TotalAmountPurchaseSpentCount"]),
                                        TotalExpenseOnPurchaseSpent = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpent"]),
                                        TotalExpenseOnPurchaseSpentCount = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpentCount"]),
                                        TotalDailyOperationExpensesAmount = Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"])),
                                        TotalDailyOperationExpensesCount = sdr["TotalDailyOperationExpensesCount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesCount"]),
                                        TotalCashSaleAmountCount = Convert.ToInt32(sdr["TotalCashSaleAmountCount"]),
                                        TotalCashSaleTotalAmount = Convert.ToInt32(sdr["TotalCashSaleTotalAmount1"]),
                                        TotalOnlineSaleAmountCount = Convert.ToInt32(sdr["TotalOnlineSaleAmountCount"]),
                                        TotalOnlineSaleTotalAmount = Convert.ToInt32(sdr["TotalOnlineSaleTotalAmount1"]),
                                        TotalCardSaleAmountCount = Convert.ToInt32(sdr["TotalCardSaleAmountCount"]),
                                        TotalCardSaleTotalAmount = Convert.ToDouble(sdr["TotalCardSaleTotalAmount1"]),
                                        TotalUPISaleAmountCount = Convert.ToInt32(sdr["TotalUPISaleAmountCount"]),
                                        TotalUPISaleTotalAmount = Convert.ToInt32(sdr["TotalUPISaleTotalAmount1"]),
                                        TotalGpaySaleAmountCount = Convert.ToInt32(sdr["TotalGpaySaleAmountCount"]),
                                        TotalGpaySaleTotalAmount = Convert.ToInt32(sdr["TotalGpaySaleTotalAmount1"]),
                                        TotalPhonePaySaleAmountCount = Convert.ToInt32(sdr["TotalPhonePaySaleAmountCount"]),
                                        TotalPhonePaySaleTotalAmount = Convert.ToInt32(sdr["TotalPhonePaySaleTotalAmount"]),
                                        TotalPaytmSaleAmountCount = Convert.ToInt32(sdr["TotalPaytmSaleAmountCount"]),
                                        TotalPaytmSaleTotalAmount = Convert.ToInt32(sdr["TotalPaytmSaleTotalAmount"]),
                                        TotalWalletSaleAmountCount = Convert.ToInt32(sdr["TotalWalletSaleAmountCount"]),
                                        TotalWalletSaleTotalAmount = Convert.ToSingle(sdr["TotalWalletSaleTotalAmount"]),
                                        TotalOverallPendingPaymentCount = Convert.ToInt32(sdr["TotalOverallPendingPaymentCount"]),
                                        TotalOverallPendingPaymenttotalAmount = Convert.ToInt32(sdr["TotalOverallPendingPaymenttotalAmount1"]),
                                        TotalCashDiscount = Convert.ToInt32(sdr["TotalCashDiscount"]),
                                        TotalDiscountAmountCount = Convert.ToInt32(sdr["TotalDiscountAmountCount"]),
                                        TotalDiscountAmount = Convert.ToDouble(sdr["TotalDiscountAmount"]),
                                        totalgstitemamount = Convert.ToDouble(sdr["totalgstitemamount"]),
                                        totalgstitemcount = Convert.ToInt32(sdr["totalgstitemcount"]),
                                        TOTAlGpayphoneCount = Convert.ToInt32(sdr["TotalGPAYphonepaycount"]),
                                        TOTAlGpayphoneAmount = Convert.ToInt32(sdr["TotalgpayphopepaySaleTotalAmount1"])
                                    };
                                }
                                return finacialStatistics;
                            }
                            catch (Exception e)
                            {

                                throw;
                            }

                        }
                    }
                  
                    }


                }





        // Later Resued Code
        public DashboardCount GetAllDashboardCount(int today)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                DashboardCount finacialStatistics = new DashboardCount();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Dashboard_FinancialStatictstics]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@today", SqlDbType.Int).Value = today;
                    con.Open();
                    if (today == 1)
                    {
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {

                            try
                            {
                                if (sdr.Read())
                                {

                                    finacialStatistics = new DashboardCount
                                    {
                                        TotalOrderReceived = Convert.ToInt32(sdr["TotalOrderReceived"]),
                                        TotalOrderReceivedTotalAmountCount = Convert.ToInt32(sdr["TotalOrderReceivedTotalAmountCount"]),
                                        TotalOrderReceivedAmount = Convert.ToInt32(sdr["TotalOrderReceivedAmount"]),
                                        TotalPendingOrder = Convert.ToInt32(sdr["TotalPendingOrder"]),
                                        TotalpendingPaymentCount = Convert.ToInt32(sdr["TotalpendingPaymentCount"]),
                                        TotalpendingPaymentAmount = Convert.ToInt32(sdr["TotalpendingPaymentAmount"]),
                                        TotalOrderDelivered = Convert.ToInt32(sdr["TotalOrderDelivered"]),
                                        TotalOrderDeliveredTotalAmountCount = Convert.ToInt32(sdr["TotalOrderDeliveredTotalAmountCount"]),
                                        TotalOrderDeliveredAmount = Convert.ToSingle(sdr["TotalOrderDeliveredAmount"]),
                                        TotalOrderCancelled = Convert.ToInt32(sdr["TotalOrderCancelled"]),
                                        TotalOrderCancelledTotalAmountCount = Convert.ToInt32(sdr["TotalOrderCancelledTotalAmountCount"]),
                                        TotalOrderCancelledAmount = Convert.ToInt32(sdr["TotalOrderCancelledAmount"]),
                                        Todaypaymentcount = Convert.ToInt32(sdr["TotalTodaysPendingPaymentCount"]),
                                        Todaypaymentamount = Convert.ToInt32(sdr["TodayTotalPendingPaymentAmount"]),
                                        TotalItemPurchaseSum = Convert.ToInt32(sdr["TotalItemPurchaseSum"]),
                                        TotalItemPurchaseCount = Convert.ToInt32(sdr["TotalItemPurchaseCount"]),
                                        TotalAmountPurchaseSpent = Convert.ToInt32(sdr["TotalAmountPurchaseSpent"]),
                                        TotalAmountPurchaseSpentCount = Convert.ToInt32(sdr["TotalAmountPurchaseSpentCount"]),
                                        TotalExpenseOnPurchaseSpent = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpent"]),
                                        TotalExpenseOnPurchaseSpentCount = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpentCount"]),
                                        TotalDailyOperationExpensesAmount = Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"])),
                                        TotalDailyOperationExpensesCount = sdr["TotalDailyOperationExpensesCount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesCount"]),
                                        TotalCashSaleAmountCount = Convert.ToInt32(sdr["TotalCashSaleAmountCount"]),
                                        TotalCashSaleTotalAmount = Convert.ToInt32(sdr["TotalCashSaleTotalAmount"]),
                                        TotalOnlineSaleAmountCount = Convert.ToInt32(sdr["TotalOnlineSaleAmountCount"]),
                                        TotalOnlineSaleTotalAmount = Convert.ToInt32(sdr["TotalOnlineSaleTotalAmount"]),
                                        TotalCardSaleAmountCount = Convert.ToInt32(sdr["TotalCardSaleAmountCount"]),
                                        TotalCardSaleTotalAmount = Convert.ToSingle(sdr["TotalCardSaleTotalAmount"]),
                                        TotalUPISaleAmountCount = Convert.ToInt32(sdr["TotalUPISaleAmountCount"]),
                                        TotalUPISaleTotalAmount = Convert.ToInt32(sdr["TotalUPISaleTotalAmount"]),
                                        TotalWalletSaleAmountCount = Convert.ToInt32(sdr["TotalWalletSaleAmountCount"]),
                                        TotalWalletSaleTotalAmount = Convert.ToSingle(sdr["TotalWalletSaleTotalAmount"]),
                                        TotalOverallPendingPaymentCount = Convert.ToInt32(sdr["TotalOverallPendingPaymentCount"]),
                                        TotalOverallPendingPaymenttotalAmount = Convert.ToInt32(sdr["TotalOverallPendingPaymenttotalAmount"]),

                                    };
                                }
                                return finacialStatistics;
                            }
                            catch (Exception e)
                            {

                                throw;
                            }

                        }
                    }
                    else
                    {
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {

                            try
                            {
                                if (sdr.Read())
                                {

                                    finacialStatistics = new DashboardCount
                                    {
                                        TotalOrderReceived = Convert.ToInt32(sdr["TotalOrderReceived"]),
                                        TotalOrderReceivedTotalAmountCount = Convert.ToInt32(sdr["TotalOrderReceivedTotalAmountCount"]),
                                        TotalOrderReceivedAmount = Convert.ToInt32(sdr["TotalOrderReceivedAmount"]),
                                        TotalPendingOrder = Convert.ToInt32(sdr["TotalPendingOrder"]),
                                        TotalpendingPaymentCount = Convert.ToInt32(sdr["TotalpendingPaymentCount"]),
                                        TotalpendingPaymentAmount = Convert.ToInt32(sdr["TotalpendingPaymentAmount"]),
                                        TotalOrderDelivered = Convert.ToInt32(sdr["TotalOrderDelivered"]),
                                        TotalOrderDeliveredTotalAmountCount = Convert.ToInt32(sdr["TotalOrderDeliveredTotalAmountCount"]),
                                        TotalOrderDeliveredAmount = Convert.ToSingle(sdr["TotalOrderDeliveredAmount"]),
                                        TotalOrderCancelled = Convert.ToInt32(sdr["TotalOrderCancelled"]),
                                        TotalOrderCancelledTotalAmountCount = Convert.ToInt32(sdr["TotalOrderCancelledTotalAmountCount"]),
                                        TotalOrderCancelledAmount = Convert.ToInt32(sdr["TotalOrderCancelledAmount"]),
                                        TotalItemPurchaseSum = Convert.ToInt32(sdr["TotalItemPurchaseSum"]),
                                        TotalItemPurchaseCount = Convert.ToInt32(sdr["TotalItemPurchaseCount"]),
                                        TotalAmountPurchaseSpent = Convert.ToInt32(sdr["TotalAmountPurchaseSpent"]),
                                        TotalAmountPurchaseSpentCount = Convert.ToInt32(sdr["TotalAmountPurchaseSpentCount"]),
                                        TotalExpenseOnPurchaseSpent = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpent"]),
                                        TotalExpenseOnPurchaseSpentCount = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpentCount"]),
                                        TotalDailyOperationExpensesAmount = Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"])),
                                        TotalDailyOperationExpensesCount = sdr["TotalDailyOperationExpensesCount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesCount"]),
                                        TotalCashSaleAmountCount = Convert.ToInt32(sdr["TotalCashSaleAmountCount"]),
                                        TotalCashSaleTotalAmount = Convert.ToInt32(sdr["TotalCashSaleTotalAmount"]),
                                        TotalOnlineSaleAmountCount = Convert.ToInt32(sdr["TotalOnlineSaleAmountCount"]),
                                        TotalOnlineSaleTotalAmount = Convert.ToInt32(sdr["TotalOnlineSaleTotalAmount"]),
                                        TotalCardSaleAmountCount = Convert.ToInt32(sdr["TotalCardSaleAmountCount"]),
                                        TotalCardSaleTotalAmount = Convert.ToSingle(sdr["TotalCardSaleTotalAmount"]),
                                        TotalUPISaleAmountCount = Convert.ToInt32(sdr["TotalUPISaleAmountCount"]),
                                        TotalUPISaleTotalAmount = Convert.ToInt32(sdr["TotalUPISaleTotalAmount"]),
                                        TotalWalletSaleAmountCount = Convert.ToInt32(sdr["TotalWalletSaleAmountCount"]),
                                        TotalWalletSaleTotalAmount = Convert.ToSingle(sdr["TotalWalletSaleTotalAmount"]),
                                        TotalOverallPendingPaymentCount = Convert.ToInt32(sdr["TotalOverallPendingPaymentCount"]),
                                        TotalOverallPendingPaymenttotalAmount = Convert.ToInt32(sdr["TotalOverallPendingPaymenttotalAmount"]),

                                    };
                                }
                                return finacialStatistics;
                            }
                            catch (Exception e)
                            {

                                throw;
                            }

                        }
                    }


                }

            }

        }
        public DashboardCount GetAllDashboardCountHubwise(int today, string HubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                DashboardCount finacialStatistics = new DashboardCount();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Dashboard_FinancialStatictsticsTesting]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@today", SqlDbType.Int).Value = today;
                    cmd.Parameters.Add("@HubId", SqlDbType.VarChar, 30).Value = HubId;
                    con.Open();
                    if (today == 1 && HubId == "HID01")
                    {
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {

                            try
                            {
                                if (sdr.Read())
                                {

                                    finacialStatistics = new DashboardCount
                                    {
                                        TotalOrderReceived = Convert.ToInt32(sdr["TotalOrderReceived"]),
                                        TotalOrderReceivedTotalAmountCount = Convert.ToInt32(sdr["TotalOrderReceivedTotalAmountCount"]),
                                        TotalOrderReceivedAmount = Convert.ToInt32(sdr["TotalOrderReceivedAmount"]),
                                        TotalPendingOrder = Convert.ToInt32(sdr["TotalPendingOrder"]),
                                        TotalpendingPaymentCount = Convert.ToInt32(sdr["TotalpendingPaymentCount"]),
                                        TotalpendingPaymentAmount = Convert.ToInt32(sdr["TotalpendingPaymentAmount"]),
                                        TotalOrderDelivered = Convert.ToInt32(sdr["TotalOrderDelivered"]),
                                        TotalOrderDeliveredTotalAmountCount = Convert.ToInt32(sdr["TotalOrderDeliveredTotalAmountCount"]),
                                        TotalOrderDeliveredAmount = Convert.ToSingle(sdr["TotalOrderDeliveredAmount"]),
                                        TotalOrderCancelled = Convert.ToInt32(sdr["TotalOrderCancelled"]),
                                        TotalOrderCancelledTotalAmountCount = Convert.ToInt32(sdr["TotalOrderCancelledTotalAmountCount"]),
                                        TotalOrderCancelledAmount = Convert.ToInt32(sdr["TotalOrderCancelledAmount"]),
                                        Todaypaymentcount = Convert.ToInt32(sdr["TotalTodaysPendingPaymentCount"]),
                                        Todaypaymentamount = Convert.ToInt32(sdr["TodayTotalPendingPaymentAmount"]),
                                        TotalItemPurchaseSum = Convert.ToInt32(sdr["TotalItemPurchaseSum"]),
                                        TotalItemPurchaseCount = Convert.ToInt32(sdr["TotalItemPurchaseCount"]),
                                        TotalAmountPurchaseSpent = Convert.ToInt32(sdr["TotalAmountPurchaseSpent"]),
                                        TotalAmountPurchaseSpentCount = Convert.ToInt32(sdr["TotalAmountPurchaseSpentCount"]),
                                        TotalExpenseOnPurchaseSpent = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpent"]),
                                        TotalExpenseOnPurchaseSpentCount = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpentCount"]),
                                        TotalDailyOperationExpensesAmount = Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"])),
                                        TotalDailyOperationExpensesCount = sdr["TotalDailyOperationExpensesCount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesCount"]),
                                        TotalCashSaleAmountCount = Convert.ToInt32(sdr["TotalCashSaleAmountCount"]),
                                        TotalCashSaleTotalAmount = Convert.ToInt32(sdr["TotalCashSaleTotalAmount"]),
                                        TotalOnlineSaleAmountCount = Convert.ToInt32(sdr["TotalOnlineSaleAmountCount"]),
                                        TotalOnlineSaleTotalAmount = Convert.ToInt32(sdr["TotalOnlineSaleTotalAmount"]),
                                        TotalCardSaleAmountCount = Convert.ToInt32(sdr["TotalCardSaleAmountCount"]),
                                        TotalCardSaleTotalAmount = Convert.ToSingle(sdr["TotalCardSaleTotalAmount"]),
                                        TotalUPISaleAmountCount = Convert.ToInt32(sdr["TotalUPISaleAmountCount"]),
                                        TotalUPISaleTotalAmount = Convert.ToInt32(sdr["TotalUPISaleTotalAmount"]),
                                        TotalWalletSaleAmountCount = Convert.ToInt32(sdr["TotalWalletSaleAmountCount"]),
                                        TotalWalletSaleTotalAmount = Convert.ToSingle(sdr["TotalWalletSaleTotalAmount"]),
                                        TotalOverallPendingPaymentCount = Convert.ToInt32(sdr["TotalOverallPendingPaymentCount"]),
                                        TotalOverallPendingPaymenttotalAmount = Convert.ToInt32(sdr["TotalOverallPendingPaymenttotalAmount"]),

                                    };
                                }
                                return finacialStatistics;
                            }
                            catch (Exception e)
                            {

                                throw;
                            }

                        }
                    }
                    else
                    {
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {

                            try
                            {
                                if (sdr.Read())
                                {

                                    finacialStatistics = new DashboardCount
                                    {
                                        TotalOrderReceived = Convert.ToInt32(sdr["TotalOrderReceived"]),
                                        TotalOrderReceivedTotalAmountCount = Convert.ToInt32(sdr["TotalOrderReceivedTotalAmountCount"]),
                                        TotalOrderReceivedAmount = Convert.ToInt32(sdr["TotalOrderReceivedAmount"]),
                                        TotalPendingOrder = Convert.ToInt32(sdr["TotalPendingOrder"]),
                                        TotalpendingPaymentCount = Convert.ToInt32(sdr["TotalpendingPaymentCount"]),
                                        TotalpendingPaymentAmount = Convert.ToInt32(sdr["TotalpendingPaymentAmount"]),
                                        TotalOrderDelivered = Convert.ToInt32(sdr["TotalOrderDelivered"]),
                                        TotalOrderDeliveredTotalAmountCount = Convert.ToInt32(sdr["TotalOrderDeliveredTotalAmountCount"]),
                                        TotalOrderDeliveredAmount = Convert.ToSingle(sdr["TotalOrderDeliveredAmount"]),
                                        TotalOrderCancelled = Convert.ToInt32(sdr["TotalOrderCancelled"]),
                                        TotalOrderCancelledTotalAmountCount = Convert.ToInt32(sdr["TotalOrderCancelledTotalAmountCount"]),
                                        TotalOrderCancelledAmount = Convert.ToInt32(sdr["TotalOrderCancelledAmount"]),
                                        TotalItemPurchaseSum = Convert.ToInt32(sdr["TotalItemPurchaseSum"]),
                                        TotalItemPurchaseCount = Convert.ToInt32(sdr["TotalItemPurchaseCount"]),
                                        TotalAmountPurchaseSpent = Convert.ToInt32(sdr["TotalAmountPurchaseSpent"]),
                                        TotalAmountPurchaseSpentCount = Convert.ToInt32(sdr["TotalAmountPurchaseSpentCount"]),
                                        TotalExpenseOnPurchaseSpent = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpent"]),
                                        TotalExpenseOnPurchaseSpentCount = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpentCount"]),
                                        TotalDailyOperationExpensesAmount = Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"])),
                                        TotalDailyOperationExpensesCount = sdr["TotalDailyOperationExpensesCount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesCount"]),
                                        TotalCashSaleAmountCount = Convert.ToInt32(sdr["TotalCashSaleAmountCount"]),
                                        TotalCashSaleTotalAmount = Convert.ToInt32(sdr["TotalCashSaleTotalAmount"]),
                                        TotalOnlineSaleAmountCount = Convert.ToInt32(sdr["TotalOnlineSaleAmountCount"]),
                                        TotalOnlineSaleTotalAmount = Convert.ToInt32(sdr["TotalOnlineSaleTotalAmount"]),
                                        TotalCardSaleAmountCount = Convert.ToInt32(sdr["TotalCardSaleAmountCount"]),
                                        TotalCardSaleTotalAmount = Convert.ToSingle(sdr["TotalCardSaleTotalAmount"]),
                                        TotalUPISaleAmountCount = Convert.ToInt32(sdr["TotalUPISaleAmountCount"]),
                                        TotalUPISaleTotalAmount = Convert.ToInt32(sdr["TotalUPISaleTotalAmount"]),
                                        TotalWalletSaleAmountCount = Convert.ToInt32(sdr["TotalWalletSaleAmountCount"]),
                                        TotalWalletSaleTotalAmount = Convert.ToSingle(sdr["TotalWalletSaleTotalAmount"]),
                                        TotalOverallPendingPaymentCount = Convert.ToInt32(sdr["TotalOverallPendingPaymentCount"]),
                                        TotalOverallPendingPaymenttotalAmount = Convert.ToInt32(sdr["TotalOverallPendingPaymenttotalAmount"]),

                                    };
                                }
                                return finacialStatistics;
                            }
                            catch (Exception e)
                            {

                                throw;
                            }

                        }
                    }


                }

            }
        }
        public DashboardCount GetAllDashboardCountZipwise(int today, string ZipCode)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                DashboardCount finacialStatistics = new DashboardCount();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Dashboard_FinancialStatictsticsZipCode]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@today", SqlDbType.Int).Value = today;
                    cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 30).Value = ZipCode;
                    con.Open();
                    if (today == 1 && ZipCode == "HID01")
                    {
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {

                            try
                            {
                                if (sdr.Read())
                                {

                                    finacialStatistics = new DashboardCount
                                    {
                                        TotalOrderReceived = Convert.ToInt32(sdr["TotalOrderReceived"]),
                                        TotalOrderReceivedTotalAmountCount = Convert.ToInt32(sdr["TotalOrderReceivedTotalAmountCount"]),
                                        TotalOrderReceivedAmount = Convert.ToInt32(sdr["TotalOrderReceivedAmount"]),
                                        TotalPendingOrder = Convert.ToInt32(sdr["TotalPendingOrder"]),
                                        TotalpendingPaymentCount = Convert.ToInt32(sdr["TotalpendingPaymentCount"]),
                                        TotalpendingPaymentAmount = Convert.ToInt32(sdr["TotalpendingPaymentAmount"]),
                                        TotalOrderDelivered = Convert.ToInt32(sdr["TotalOrderDelivered"]),
                                        TotalOrderDeliveredTotalAmountCount = Convert.ToInt32(sdr["TotalOrderDeliveredTotalAmountCount"]),
                                        TotalOrderDeliveredAmount = Convert.ToSingle(sdr["TotalOrderDeliveredAmount"]),
                                        TotalOrderCancelled = Convert.ToInt32(sdr["TotalOrderCancelled"]),
                                        TotalOrderCancelledTotalAmountCount = Convert.ToInt32(sdr["TotalOrderCancelledTotalAmountCount"]),
                                        TotalOrderCancelledAmount = Convert.ToInt32(sdr["TotalOrderCancelledAmount"]),
                                        Todaypaymentcount = Convert.ToInt32(sdr["TotalTodaysPendingPaymentCount"]),
                                        Todaypaymentamount = Convert.ToInt32(sdr["TodayTotalPendingPaymentAmount"]),
                                        TotalItemPurchaseSum = Convert.ToInt32(sdr["TotalItemPurchaseSum"]),
                                        TotalItemPurchaseCount = Convert.ToInt32(sdr["TotalItemPurchaseCount"]),
                                        TotalAmountPurchaseSpent = Convert.ToInt32(sdr["TotalAmountPurchaseSpent"]),
                                        TotalAmountPurchaseSpentCount = Convert.ToInt32(sdr["TotalAmountPurchaseSpentCount"]),
                                        TotalExpenseOnPurchaseSpent = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpent"]),
                                        TotalExpenseOnPurchaseSpentCount = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpentCount"]),
                                        TotalDailyOperationExpensesAmount = Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"])),
                                        TotalDailyOperationExpensesCount = sdr["TotalDailyOperationExpensesCount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesCount"]),
                                        TotalCashSaleAmountCount = Convert.ToInt32(sdr["TotalCashSaleAmountCount"]),
                                        TotalCashSaleTotalAmount = Convert.ToInt32(sdr["TotalCashSaleTotalAmount"]),
                                        TotalOnlineSaleAmountCount = Convert.ToInt32(sdr["TotalOnlineSaleAmountCount"]),
                                        TotalOnlineSaleTotalAmount = Convert.ToInt32(sdr["TotalOnlineSaleTotalAmount"]),
                                        TotalCardSaleAmountCount = Convert.ToInt32(sdr["TotalCardSaleAmountCount"]),
                                        TotalCardSaleTotalAmount = Convert.ToSingle(sdr["TotalCardSaleTotalAmount"]),
                                        TotalUPISaleAmountCount = Convert.ToInt32(sdr["TotalUPISaleAmountCount"]),
                                        TotalUPISaleTotalAmount = Convert.ToInt32(sdr["TotalUPISaleTotalAmount"]),
                                        TotalWalletSaleAmountCount = Convert.ToInt32(sdr["TotalWalletSaleAmountCount"]),
                                        TotalWalletSaleTotalAmount = Convert.ToSingle(sdr["TotalWalletSaleTotalAmount"]),
                                        TotalOverallPendingPaymentCount = Convert.ToInt32(sdr["TotalOverallPendingPaymentCount"]),
                                        TotalOverallPendingPaymenttotalAmount = Convert.ToInt32(sdr["TotalOverallPendingPaymenttotalAmount"]),

                                    };
                                }
                                return finacialStatistics;
                            }
                            catch (Exception e)
                            {

                                throw;
                            }

                        }
                    }
                    else
                    {
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {

                            try
                            {
                                if (sdr.Read())
                                {

                                    finacialStatistics = new DashboardCount
                                    {
                                        TotalOrderReceived = Convert.ToInt32(sdr["TotalOrderReceived"]),
                                        TotalOrderReceivedTotalAmountCount = Convert.ToInt32(sdr["TotalOrderReceivedTotalAmountCount"]),
                                        TotalOrderReceivedAmount = Convert.ToInt32(sdr["TotalOrderReceivedAmount"]),
                                        TotalPendingOrder = Convert.ToInt32(sdr["TotalPendingOrder"]),
                                        TotalpendingPaymentCount = Convert.ToInt32(sdr["TotalpendingPaymentCount"]),
                                        TotalpendingPaymentAmount = Convert.ToInt32(sdr["TotalpendingPaymentAmount"]),
                                        TotalOrderDelivered = Convert.ToInt32(sdr["TotalOrderDelivered"]),
                                        TotalOrderDeliveredTotalAmountCount = Convert.ToInt32(sdr["TotalOrderDeliveredTotalAmountCount"]),
                                        TotalOrderDeliveredAmount = Convert.ToSingle(sdr["TotalOrderDeliveredAmount"]),
                                        TotalOrderCancelled = Convert.ToInt32(sdr["TotalOrderCancelled"]),
                                        TotalOrderCancelledTotalAmountCount = Convert.ToInt32(sdr["TotalOrderCancelledTotalAmountCount"]),
                                        TotalOrderCancelledAmount = Convert.ToInt32(sdr["TotalOrderCancelledAmount"]),
                                        TotalItemPurchaseSum = Convert.ToInt32(sdr["TotalItemPurchaseSum"]),
                                        TotalItemPurchaseCount = Convert.ToInt32(sdr["TotalItemPurchaseCount"]),
                                        TotalAmountPurchaseSpent = Convert.ToInt32(sdr["TotalAmountPurchaseSpent"]),
                                        TotalAmountPurchaseSpentCount = Convert.ToInt32(sdr["TotalAmountPurchaseSpentCount"]),
                                        TotalExpenseOnPurchaseSpent = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpent"]),
                                        TotalExpenseOnPurchaseSpentCount = Convert.ToInt32(sdr["TotalExpenseOnPurchaseSpentCount"]),
                                        TotalDailyOperationExpensesAmount = Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesAmount"])),
                                        TotalDailyOperationExpensesCount = sdr["TotalDailyOperationExpensesCount"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["TotalDailyOperationExpensesCount"]),
                                        TotalCashSaleAmountCount = Convert.ToInt32(sdr["TotalCashSaleAmountCount"]),
                                        TotalCashSaleTotalAmount = Convert.ToInt32(sdr["TotalCashSaleTotalAmount"]),
                                        TotalOnlineSaleAmountCount = Convert.ToInt32(sdr["TotalOnlineSaleAmountCount"]),
                                        TotalOnlineSaleTotalAmount = Convert.ToInt32(sdr["TotalOnlineSaleTotalAmount"]),
                                        TotalCardSaleAmountCount = Convert.ToInt32(sdr["TotalCardSaleAmountCount"]),
                                        TotalCardSaleTotalAmount = Convert.ToSingle(sdr["TotalCardSaleTotalAmount"]),
                                        TotalUPISaleAmountCount = Convert.ToInt32(sdr["TotalUPISaleAmountCount"]),
                                        TotalUPISaleTotalAmount = Convert.ToInt32(sdr["TotalUPISaleTotalAmount"]),
                                        TotalWalletSaleAmountCount = Convert.ToInt32(sdr["TotalWalletSaleAmountCount"]),
                                        TotalWalletSaleTotalAmount = Convert.ToSingle(sdr["TotalWalletSaleTotalAmount"]),
                                        TotalOverallPendingPaymentCount = Convert.ToInt32(sdr["TotalOverallPendingPaymentCount"]),
                                        TotalOverallPendingPaymenttotalAmount = Convert.ToInt32(sdr["TotalOverallPendingPaymenttotalAmount"]),

                                    };
                                }
                                return finacialStatistics;
                            }
                            catch (Exception e)
                            {

                                throw;
                            }

                        }
                    }


                }

            }
        }




        // Taxation Code Used
        public List<TaxPercentageMst> GetallGstTaxpyInfolist(string datefrom, string dateto)
        {
            var taxlist = new List<TaxPercentageMst>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[Dashboard_GstTaxPayable]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@datefrom", datefrom);
                        cmd.Parameters.AddWithValue("@dateto", dateto);
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            taxlist.Add(new TaxPercentageMst
                            {
                                IGST_Per = Convert.ToDouble(reader["IGST_Per"]),
                                ItemCount = Convert.ToInt32(reader["TotalItems"]),
                                TotalCgstAmount = reader["CGST"] == DBNull.Value ? 00 : Convert.ToSingle(reader["CGST"]),
                                TotalSgstAmount = reader["SGST"] == DBNull.Value ? 00 : Convert.ToSingle(reader["SGST"]),
                                TotalIgstAmount = reader["IGST"] == DBNull.Value ? 00 : Convert.ToSingle(reader["IGST"]),

                            });
                        }
                        sqlcon.Close();
                        return taxlist;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public List<TaxPercentageMst> GetallGstTaxpyInfolist()
        {
            var taxlist = new List<TaxPercentageMst>();
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[Dashboard_GstTaxPayableAllCount]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            taxlist.Add(new TaxPercentageMst
                            {
                                IGST_Per = Convert.ToDouble(reader["IGST_Per"]),
                                ItemCount = Convert.ToInt32(reader["TotalItems"]),
                                TotalCgstAmount = reader["CGST"] == DBNull.Value ? 00 : Convert.ToSingle(reader["CGST"]),
                                TotalSgstAmount = reader["SGST"] == DBNull.Value ? 00 : Convert.ToSingle(reader["SGST"]),
                                TotalIgstAmount = reader["IGST"] == DBNull.Value ? 00 : Convert.ToSingle(reader["IGST"]),
                                
                            });
                        }
                        sqlcon.Close();
                        return taxlist;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public TaxPercentageMst GetGstCountDetailInfo(string datefrom, string dateto)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                TaxPercentageMst GstCount = new TaxPercentageMst();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Dashboard_GstTaxPayableBifurcation]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@datefrom", datefrom);
                    cmd.Parameters.AddWithValue("@dateto", dateto);
                    con.Open();           
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {

                            try
                            {
                                if (sdr.Read())
                                {

                                    GstCount = new TaxPercentageMst
                                    {
                                        TotalCgstAmount = Convert.ToSingle(sdr["TotalCgstTax"]),
                                        TotalSgstAmount = Convert.ToSingle(sdr["TotalSgstTax"]),
                                        TotaltaxAmount = Convert.ToSingle(sdr["TotalTax"]),
                                       

                                    };
                                }
                                return GstCount;
                            }
                            catch (Exception e)
                            {

                                throw;
                            }

                        }
                    }


                }

            }
        public List<DashboardCount> Getexportlist(string id)
        {
            List<DashboardCount> list = new List<DashboardCount>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Usp_GenerateExeclList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hubId", SqlDbType.Int).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        list.Add(new DashboardCount
                        {
                            Year = Convert.ToInt32(rd["Year"]),
                            Month = Convert.ToString(rd["Month1"]),
                            Startdate = Convert.ToDateTime(rd["Startdate"]),
                            Enddate = Convert.ToDateTime(rd["Enddate"]),


                        });

                    }
                    return list;
                }
            }
        }

    }
}

