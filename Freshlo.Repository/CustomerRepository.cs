using Freshlo.DomainEntities;
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
    public class CustomerRepository : ICustomerRI
    {
        private IDbConfig _dbConfig { get; }
        public CustomerRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        public List<CustomerSalesHistory> GetCustomerHistory(string hubId, string role)
        {
            hubId = role == "System Admin" ? null : hubId;

            List<CustomerSalesHistory> CustomerList = new List<CustomerSalesHistory>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Customer_SalesHistory]";
                cmd.Parameters.Add("@HubId", SqlDbType.VarChar, 15).Value = hubId;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            CustomerList.Add(new CustomerSalesHistory
                            {
                                //Id = Convert.ToString(rd["Id"]),
                                CustomerId = Convert.ToString(rd["CustomerId"] == DBNull.Value ? "NA" : rd["CustomerId"]),
                                Name = Convert.ToString(rd["Name"] == DBNull.Value ? "NA" : rd["Name"]),
                                Contact = Convert.ToString(rd["Contact"] == DBNull.Value ? "NA" : rd["Contact"]),
                                EmailId = Convert.ToString(rd["EmailId"] == DBNull.Value ? "NA" : rd["EmailId"]),
                                Status = Convert.ToString(rd["Status"] == DBNull.Value ? "NA" : rd["Status"]),
                                CreatedBy = Convert.ToString(rd["CreatedBy"] == DBNull.Value ? "NA" : rd["CreatedBy"]),
                                Source = Convert.ToString(rd["Source"] == DBNull.Value ? "NA" : rd["Source"]),
                                LastLogin = Convert.ToDateTime(rd["LastLogin"]),
                                CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                                //wallet = Convert.ToString(rd["wallet"] == DBNull.Value ? "0" : rd["wallet"]),
                                //PendingPaymentAmount = Convert.ToString(rd["PendingPaymentAmount"] == DBNull.Value ? "0" : rd["PendingPaymentAmount"]),
                                //OpenOrders = Convert.ToString(rd["OpenOrders"] == DBNull.Value ? "0" : rd["OpenOrders"]),
                                //ClosedOrders = Convert.ToString(rd["ClosedOrders"] == DBNull.Value ? "0" : rd["ClosedOrders"]),
                                //CancelledOrderCount = Convert.ToString(rd["CancelledOrderCount"] == DBNull.Value ? "0" : rd["CancelledOrderCount"]),
                                //CancelledOrderAmount = Convert.ToString(rd["CancelledOrderAmount"] == DBNull.Value ? "0" : rd["CancelledOrderAmount"]),
                                //PendingPaymentCount = Convert.ToString(rd["PendingPaymentCount"] == DBNull.Value ? "0" : rd["PendingPaymentCount"]),
                                //ClosedOrderAmount = Convert.ToString(rd["ClosedOrderAmount"] == DBNull.Value ? "0" : rd["ClosedOrderAmount"]),
                                //CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            });
                        }
                        return CustomerList;
                    }
                }
                catch (Exception ex) {
                    throw;
                }
            }

        }
        //public List<CustomerSalesHistory> GetCustomerHistory(string hubId, string role)
        //{
        //    hubId = role == "System Admin" ? null : hubId; List<CustomerSalesHistory> CustomerList = new List<CustomerSalesHistory>();
        //    using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.Connection = con;
        //        cmd.CommandText = "[dbo].[Customer_SalesHistory]";
        //        cmd.Parameters.Add("@HubId", SqlDbType.VarChar, 15).Value = hubId;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();
        //        using (SqlDataReader rd = cmd.ExecuteReader())
        //        {
        //            while (rd.Read())
        //            {
        //                CustomerList.Add(new CustomerSalesHistory
        //                {
        //                    Id = Convert.ToInt32(rd["Id"]),
        //                    CustomerId = Convert.ToString(rd["CustomerId"]),
        //                    Name = Convert.ToString(rd["Name"] == DBNull.Value ? "NA" : rd["Name"]),
        //                    Contact = Convert.ToString(rd["Contact"] == DBNull.Value ? "NA" : rd["Contact"]),
        //                    EmailId = Convert.ToString(rd["EmailId"] == DBNull.Value ? "NA" : rd["EmailId"]),
        //                    Status = Convert.ToString(rd["Status"] == DBNull.Value ? "NA" : rd["Status"]),
        //                    CreatedBy = Convert.ToString(rd["CreatedBy"] == DBNull.Value ? "NA" : rd["CreatedBy"]),
        //                    Source = Convert.ToString(rd["Source"] == DBNull.Value ? "NA" : rd["Source"]),
        //                    LastLogin = Convert.ToDateTime(rd["LastLogin"]),
        //                    CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
        //                    //wallet = Convert.ToString(rd["wallet"] == DBNull.Value ? "0" : rd["wallet"]),
        //                    //PendingPaymentAmount = Convert.ToString(rd["PendingPaymentAmount"] == DBNull.Value ? "0" : rd["PendingPaymentAmount"]),
        //                    //OpenOrders = Convert.ToString(rd["OpenOrders"] == DBNull.Value ? "0" : rd["OpenOrders"]),
        //                    //ClosedOrders = Convert.ToString(rd["ClosedOrders"] == DBNull.Value ? "0" : rd["ClosedOrders"]),
        //                    //CancelledOrderCount = Convert.ToString(rd["CancelledOrderCount"] == DBNull.Value ? "0" : rd["CancelledOrderCount"]),
        //                    //CancelledOrderAmount = Convert.ToString(rd["CancelledOrderAmount"] == DBNull.Value ? "0" : rd["CancelledOrderAmount"]),
        //                    //PendingPaymentCount = Convert.ToString(rd["PendingPaymentCount"] == DBNull.Value ? "0" : rd["PendingPaymentCount"]),
        //                    //ClosedOrderAmount = Convert.ToString(rd["ClosedOrderAmount"] == DBNull.Value ? "0" : rd["ClosedOrderAmount"]), });
        //                });
        //            return CustomerList;
        //            }
        //        }
        //    }
        //}

        public int AddToWallet(Wallet info)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Wallet_AddMoney]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 50).Value = info.CustomerId;
                cmd.Parameters.Add("@Amount", SqlDbType.Float).Value = info.Amount;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = info.Description;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = info.CreatedBy;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<CustomersAddress> GetAreawiseCustomer()
        {
            List<CustomersAddress> GetAreawiseCustomerList = new List<CustomersAddress>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Customer_AreawiseGetCount]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            GetAreawiseCustomerList.Add(new CustomersAddress()
                            {
                                ZipCode = Convert.ToString(rd["ZipCode"] == DBNull.Value ? "0" : rd["ZipCode"]),
                                CustomerCount = Convert.ToString(rd["CustomerId"]),
                                StandradDeleiveryCharges = Convert.ToSingle(rd["DeliveryAmount"] == DBNull.Value ? "0" : rd["DeliveryAmount"]),
                                ExpressDeleiveryCharges = Convert.ToSingle(rd["ExpressDeliveryAmount"] == DBNull.Value ? "0" : rd["ExpressDeliveryAmount"]),
                                MinOrderValue = Convert.ToSingle(rd["AmountLimit"] == DBNull.Value ? "0" : rd["AmountLimit"]),
                            });
                        }
                        return GetAreawiseCustomerList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public CustomerSummaryCount GetAllCustomerOrderCount()
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                CustomerSummaryCount customerOrderSummary = new CustomerSummaryCount();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Customer_Summary]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        try
                        {
                            if (sdr.Read())
                            {

                                customerOrderSummary = new CustomerSummaryCount
                                {

                                    MorethanTwentyCustomerCount = Convert.ToInt32(sdr["MorethanTwentyCustomerCount"] == DBNull.Value ? "0" : sdr["MorethanTwentyCustomerCount"]),
                                    MorethanTenCustomerCount = Convert.ToInt32(sdr["MorethanTenCustomerCount"] == DBNull.Value ? "0" : sdr["MorethanTenCustomerCount"]),
                                    MorethanFiveCustomerCount = Convert.ToInt32(sdr["MorethanFiveCustomerCount"] == DBNull.Value ? "0" : sdr["MorethanFiveCustomerCount"]),
                                    LessthanFiveCustomerCount = Convert.ToInt32(sdr["LessthanFiveCustomerCount"] == DBNull.Value ? "0" : sdr["LessthanFiveCustomerCount"]),
                                    MorethanOneCustomerCount = Convert.ToInt32(sdr["MorethanOneCustomerCount"] == DBNull.Value ? "0" : sdr["MorethanOneCustomerCount"]),
                                    ZeroCustomerCount = Convert.ToInt32(sdr["ZeroCustomerCount"] == DBNull.Value ? "0" : sdr["ZeroCustomerCount"]),

                                };
                            }
                            return customerOrderSummary;
                        }
                        catch (Exception e)
                        {

                            throw;
                        }

                    }
                }



            }

        }

        public CustomerSummaryCount GetAllnewCustomerCount(int filter)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                CustomerSummaryCount CustomerSummarynewCount = new CustomerSummaryCount();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Customer_NewOrderSummary]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Filter", SqlDbType.Int).Value = filter;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        try
                        {
                            if (sdr.Read())
                            {

                                CustomerSummarynewCount = new CustomerSummaryCount
                                {
                                    TotalDownload = Convert.ToInt32(sdr["TotalDownload"] == DBNull.Value ? "0" : sdr["TotalDownload"]),
                                    TotalRegistered = Convert.ToInt32(sdr["TotalRegistered"] == DBNull.Value ? "0" : sdr["TotalRegistered"]),
                                    TotalUnRegistered = Convert.ToInt32(sdr["TotalUnRegistered"] == DBNull.Value ? "0" : sdr["TotalUnRegistered"]),
                                    TotalRegistederdButNotOrderToday = Convert.ToInt32(sdr["TotalRegistederdButNotOrderToday"] == DBNull.Value ? "0" : sdr["TotalRegistederdButNotOrderToday"]),
                                    TodayTotalCustomerRegistered = Convert.ToInt32(sdr["TodayTotalCustomerRegistered"] == DBNull.Value ? "0" : sdr["TodayTotalCustomerRegistered"]),
                                    TodayTotalOrderValueRegiCustomer = Convert.ToInt32(sdr["TodayTotalOrderValueRegiCustomer"] == DBNull.Value ? "0" : sdr["TodayTotalOrderValueRegiCustomer"]),
                                    TodayTotalOrderValueRegiCustomerAvg = Convert.ToSingle(sdr["TodayTotalOrderValueRegiCustomerAvg"] == DBNull.Value ? "0" : sdr["TodayTotalOrderValueRegiCustomerAvg"]),
                                };
                            }
                            return CustomerSummarynewCount;
                        }
                        catch (Exception e)
                        {

                            throw;
                        }

                    }
                }
            }

        }
        public CustomerSalesHistory GetSalesSummary(string id)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                CustomerSalesHistory Summary = new CustomerSalesHistory();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Customer_OrderSummarytest]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar,200).Value = id;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        try
                        {
                            if (sdr.Read())
                            {

                                Summary = new CustomerSalesHistory
                                {
                                    CustomerId = Convert.ToString(sdr["CustomerId"] == DBNull.Value ? "" : sdr["CustomerId"]),
                                    Name = Convert.ToString(sdr["fullName"] == DBNull.Value ? "" : sdr["fullName"]),
                                    Contact = Convert.ToString(sdr["PhoneNumber"] == DBNull.Value ? "000000" : sdr["PhoneNumber"]),
                                    CreatedOn = Convert.ToDateTime(sdr["CreatedOn"] == DBNull.Value ? "" : sdr["CreatedOn"]),
                                    Symbol = Convert.ToString(sdr["Symbol"] == DBNull.Value ? "₹" : sdr["Symbol"]),
                                    OpenOrders = Convert.ToString(sdr["TotalOrder"] == DBNull.Value ? "0" : sdr["TotalOrder"]),
                                    CancelledOrderCount = Convert.ToString(sdr["CancelledOrder"] == DBNull.Value ? "0" : sdr["CancelledOrder"]),
                                    CancelledOrderAmount = Convert.ToString(sdr["CancelledAmount"] == DBNull.Value ? "0" : sdr["CancelledAmount"]),
                                    ClosedOrders = Convert.ToString(sdr["DeliveredOrder"] == DBNull.Value ? "0" : sdr["DeliveredOrder"]),
                                    ClosedOrderAmount = Convert.ToString(sdr["DeliveredAmount"] == DBNull.Value ? "0" : sdr["DeliveredAmount"]),
                                    InWallet = Convert.ToDecimal(sdr["WalletCash"] == DBNull.Value ? "0" : sdr["WalletCash"]),
                                    RewardPoint = Convert.ToDecimal(sdr["RewardPoints"] == DBNull.Value ? "0" : sdr["RewardPoints"]),
                                    RedeemedPoint = Convert.ToDecimal(sdr["ReedemPoints"] == DBNull.Value ? "0" : sdr["ReedemPoints"]),
                                    DaysLeft = Convert.ToInt32(sdr["DaysLeft"] == DBNull.Value ? "0" : sdr["DaysLeft"]),
                                 
                                };
                            }
                            return Summary;
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }

                    }
                }
            }
        }        
        public List<Sales> GetSalesOrderList(string CustId)
        {
            List<Sales> List = new List<Sales>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Customer_GetSalesList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CustId", SqlDbType.VarChar, 50).Value = CustId;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    try
                    {
                        while (rd.Read())
                        {
                            List.Add(new Sales()
                            {
                                SalesOrderId = Convert.ToString(rd["SalesOrderId"]),
                                CustomerId = Convert.ToString(rd["CustomerId"]),
                                PLU_Count = Convert.ToInt32(rd["PLU_Count"]),
                                TotalAmount = Convert.ToInt32(rd["TotalPrice"]),
                                DeliveryCharges1 = Convert.ToDouble(rd["DeliveryCharges"]),
                                OrderdStatus = Convert.ToString(rd["OrderdStatus"]),
                                PaymentStatus = Convert.ToString(rd["PaymentStatus"]),
                                Symbol = Convert.ToString(rd["Symbol"]),
                                CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                                DeliveryDate1 = Convert.ToDateTime(rd["DeliveryDate"]),

                            });
                        }
                    }
                    catch (Exception ex) { throw; }
                    return List;
                }
            }
        }
        public Customer GetCustomerDetail(string id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                Customer details = new Customer();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Customer_GetCustomerDetailbyIdtest]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar,50).Value = id;
                    cmd.Parameters.Add("@Type", SqlDbType.VarChar, 10).Value = "Default";

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            details = new Customer
                            {
                                CustomerId = Convert.ToString(sdr["CustomerId"] == DBNull.Value ? "NA" : Convert.ToString(sdr["CustomerId"])),
                                Name = Convert.ToString(sdr["Name"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Name"])),
                                EmailId = Convert.ToString(sdr["EmailId"] == DBNull.Value ? "NA" : Convert.ToString(sdr["EmailId"])),
                                ContactNo = Convert.ToString(sdr["ContactNo"] == DBNull.Value ? "NA" : Convert.ToString(sdr["ContactNo"])),
                                RoomNo = Convert.ToString(sdr["RoomNo"] == DBNull.Value ? "NA" : Convert.ToString(sdr["RoomNo"])),
                                BuildingName = Convert.ToString(sdr["BuildingName"] == DBNull.Value ? "NA" : Convert.ToString(sdr["BuildingName"])),
                                Sector = Convert.ToString(sdr["Sector"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Sector"])),
                                Landmark = Convert.ToString(sdr["Landmark"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Landmark"])),
                                Locality = Convert.ToString(sdr["Locality"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Locality"])),
                                AddressType = Convert.ToString(sdr["AddressType"] == DBNull.Value ? "NA" : Convert.ToString(sdr["AddressType"])),
                                Address1 = Convert.ToString(sdr["Address"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Address"])),
                                ZipCode = Convert.ToString(sdr["ZipCode"] == DBNull.Value ? "NA" : Convert.ToString(sdr["ZipCode"])),
                                Type = Convert.ToString(sdr["Type"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Type"])),
                                Source = Convert.ToString(sdr["Source"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Source"])),
                                Status = Convert.ToString(sdr["Status"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Status"])),
                                State = Convert.ToString(sdr["State"] == DBNull.Value ? "NA" : Convert.ToString(sdr["State"])),
                                Country = Convert.ToString(sdr["Country"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Country"])),
                                City = Convert.ToString(sdr["City"] == DBNull.Value ? "NA" : Convert.ToString(sdr["City"])),
                                AddId = Convert.ToString(sdr["AddId"] == DBNull.Value ? "NA" : Convert.ToString(sdr["AddId"])),

                            };
                        }
                        return details;


                    }
                }



            }

        }

        public List<Customer> GetCustomerList()
        {

            List<Customer> CustomerList = new List<Customer>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Customer_UnRegisteredCustomerList]";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CustomerList.Add(new Customer
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            CustomerId = Convert.ToString(rd["CustomerId"]),
                            Name = Convert.ToString(rd["Name"] == DBNull.Value ? "NA" : rd["Name"]),
                            EmailId = Convert.ToString(rd["EmailId"] == DBNull.Value ? "NA" : rd["EmailId"]),
                            ContactNo = Convert.ToString(rd["ContactNo"] == DBNull.Value ? "NA" : rd["ContactNo"]),
                            ZipCode = Convert.ToString(rd["ZipCode"] == DBNull.Value ? "NA" : rd["ZipCode"]),
                            Status = Convert.ToString(rd["Status"] == DBNull.Value ? "NA" : rd["Status"]),
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                        });
                    }
                    return CustomerList;
                }
            }
        }

        public List<Customer> GetCustomerOrderlist(int a, int b)
        {

            List<Customer> CustomerOrderList = new List<Customer>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[CustomerListView_DownloadSp]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@a", SqlDbType.Int).Value = a;
                cmd.Parameters.Add("@b", SqlDbType.Int).Value = b;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CustomerOrderList.Add(new Customer
                        {
                            Id = Convert.ToInt32(rd["ID"]),
                            CustomerId = rd["CustomerId"] as string,
                            Name = rd["Name"] as string,
                            ContactNo = rd["ContactNo"] as string,
                            EmailId = rd["EmailId"] as string,
                            Status = Convert.ToString(rd["Status"]),
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                        });
                    }
                    return CustomerOrderList;
                }
            }
        }

        public CustomerSummaryCount GetAllnewCustomerCountHub(int filter, string Hub)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                CustomerSummaryCount CustomerSummarynewCount = new CustomerSummaryCount();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Customer_NewOrderSummaryHubFilter]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Filter", SqlDbType.Int).Value = filter;
                    cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 30).Value = Hub;

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        try
                        {
                            if (sdr.Read())
                            {

                                CustomerSummarynewCount = new CustomerSummaryCount
                                {
                                    TotalDownload = Convert.ToInt32(sdr["TotalDownload"]),
                                    TotalRegistered = Convert.ToInt32(sdr["TotalRegistered"]),
                                    TotalUnRegistered = Convert.ToInt32(sdr["TotalUnRegistered"]),
                                    TotalRegistederdButNotOrderToday = Convert.ToInt32(sdr["TotalRegistederdButNotOrderToday"]),
                                    TodayTotalCustomerRegistered = Convert.ToInt32(sdr["TodayTotalCustomerRegistered"]),
                                    TodayTotalOrderValueRegiCustomer = Convert.ToInt32(sdr["TodayTotalOrderValueRegiCustomer"]),
                                    TodayTotalOrderValueRegiCustomerAvg = Convert.ToSingle(sdr["TodayTotalOrderValueRegiCustomerAvg"]),
                                };
                            }
                            return CustomerSummarynewCount;
                        }
                        catch (Exception e)
                        {

                            throw;
                        }

                    }
                }
            }

        }

        public CustomerSummaryCount GetAllnewCustomerCountZip(int filter, string ZipCode)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                CustomerSummaryCount CustomerSummarynewCount = new CustomerSummaryCount();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Customer_NewOrderSummaryZipCodewiseFilter]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Filter", SqlDbType.Int).Value = filter;
                    cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 30).Value = ZipCode;

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        try
                        {
                            if (sdr.Read())
                            {

                                CustomerSummarynewCount = new CustomerSummaryCount
                                {
                                    TotalDownload = Convert.ToInt32(sdr["TotalDownload"]),
                                    TotalRegistered = Convert.ToInt32(sdr["TotalRegistered"]),
                                    TotalUnRegistered = Convert.ToInt32(sdr["TotalUnRegistered"]),
                                    TotalRegistederdButNotOrderToday = Convert.ToInt32(sdr["TotalRegistederdButNotOrderToday"]),
                                    TodayTotalCustomerRegistered = Convert.ToInt32(sdr["TodayTotalCustomerRegistered"]),
                                    TodayTotalOrderValueRegiCustomer = Convert.ToInt32(sdr["TodayTotalOrderValueRegiCustomer"]),
                                    TodayTotalOrderValueRegiCustomerAvg = Convert.ToSingle(sdr["TodayTotalOrderValueRegiCustomerAvg"]),
                                };
                            }
                            return CustomerSummarynewCount;
                        }
                        catch (Exception e)
                        {

                            throw;
                        }

                    }
                }
            }

        }

        public byte[] FilterExcelofCustomer(string branch, string role, string webRootPath)
        {
            string fileName = Path.Combine(webRootPath, "CustomerInfo.xlsx");
            FileInfo newFile = new FileInfo(fileName);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(fileName);
            }
            Stream stream = new System.IO.MemoryStream();
            using (var package = new ExcelPackage(newFile))
            {

                var worksheet1 = package.Workbook.Worksheets.Add("CustomerInfo");
                //IQueryable<Lead> leadList = null;
                List<CustomerSalesHistory> CustomerList = new List<CustomerSalesHistory>();
                CustomerList = GetCustomerHistory(branch, role);


                int rowCount = 1;

                var rowdetail = rowCount;

                #region ExcelHeader
                worksheet1.Cells[rowdetail, 1].Value = "Customer Name";
                worksheet1.Cells[rowdetail, 1].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(1).AutoFit();

                worksheet1.Cells[rowdetail, 2].Value = "Contact Number";
                worksheet1.Cells[rowdetail, 2].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(2).AutoFit();

                worksheet1.Cells[rowdetail, 3].Value = "Created On";
                worksheet1.Cells[rowdetail, 3].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(3).AutoFit();

                worksheet1.Cells[rowdetail, 4].Value = "Wallet Balance";
                worksheet1.Cells[rowdetail, 4].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(4).AutoFit();


                worksheet1.Cells[rowdetail, 5].Value = "Pending Payment";
                worksheet1.Cells[rowdetail, 5].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(5).AutoFit();



                worksheet1.Cells[rowdetail, 6].Value = "Open Orders";
                worksheet1.Cells[rowdetail, 6].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(6).AutoFit();


                worksheet1.Cells[rowdetail, 7].Value = "Closed Order";
                worksheet1.Cells[rowdetail, 7].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(7).AutoFit();


                worksheet1.Cells[rowdetail, 8].Value = "Close Orders Amount";
                worksheet1.Cells[rowdetail, 8].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(8).AutoFit();


                #endregion

                rowdetail++;
                foreach (var custdetail in CustomerList)
                {
                    worksheet1.Cells[rowdetail, 1].Value = custdetail.Name;
                    worksheet1.Cells[rowdetail, 2].Value = custdetail.Contact;
                    worksheet1.Cells[rowdetail, 3].Value = custdetail.CreatedOn.ToString("yyyy-MM-dd hh:mm"); ;
                    worksheet1.Cells[rowdetail, 4].Value = custdetail.wallet;
                    worksheet1.Cells[rowdetail, 5].Value = "Rs." + custdetail.PendingPaymentAmount;
                    worksheet1.Cells[rowdetail, 6].Value = custdetail.OpenOrders;
                    worksheet1.Cells[rowdetail, 7].Value = custdetail.ClosedOrders;
                    worksheet1.Cells[rowdetail, 8].Value = "Rs. " + custdetail.ClosedOrderAmount;


                    rowdetail++;
                }

                package.SaveAs(newFile);
                byte[] files = File.ReadAllBytes(fileName);
                File.Delete(fileName);
                return files;
            }
        }

        public int CreateCustomer(Customer cust)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[usp_AddCustomer]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = cust.Name == null ? "Guest" : cust.Name;
                        cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = cust.EmailId == null ? (object)DBNull.Value : cust.EmailId;
                        cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar, 30).Value = cust.ContactNo == null ? (object)DBNull.Value : cust.ContactNo;
                        cmd.Parameters.Add("@UserType", SqlDbType.VarChar, 10).Value = cust.UserType == null ? "Default" : cust.UserType;
                        cmd.Parameters.Add("@Source", SqlDbType.VarChar, 10).Value = cust.Source == null ? (object)DBNull.Value : cust.Source;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 10).Value = cust.CreatedBy == null ? (object)DBNull.Value : cust.CreatedBy;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }

        }


        public int AddAddress(Customer cust)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_AddCustomerAddress]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 50).Value = cust.CustomerId == null ? (object)DBNull.Value : cust.CustomerId;
                    cmd.Parameters.Add("@BuildingName", SqlDbType.VarChar, 50).Value = cust.BuildingName == null ? (object)DBNull.Value : cust.BuildingName;
                    cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar, 30).Value = cust.RoomNo == null ? (object)DBNull.Value : cust.RoomNo;
                    cmd.Parameters.Add("@Sector", SqlDbType.VarChar, 10).Value = cust.Sector == null ? (object)DBNull.Value : cust.Sector;
                    cmd.Parameters.Add("@Locality", SqlDbType.VarChar, 10).Value = cust.Locality == null ? "NA" : cust.Locality;
                    cmd.Parameters.Add("@Landmark", SqlDbType.VarChar, 50).Value = cust.Landmark == null ? (object)DBNull.Value : cust.Landmark;
                    cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 50).Value = cust.ZipCode == null ? "0": cust.ZipCode;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 30).Value = cust.City == null ? (object)DBNull.Value : cust.City;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 10).Value = cust.State == null ? (object)DBNull.Value : cust.State;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar, 10).Value = cust.Country == null ? (object)DBNull.Value : cust.Country;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = cust.CreatedBy == null ? (object)DBNull.Value : cust.CreatedBy;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = cust.Address1 == null ? (object)DBNull.Value : cust.Address1;
                    cmd.Parameters.Add("@AddressType", SqlDbType.VarChar, 50).Value = cust.AddressType == null ? (object)DBNull.Value : cust.AddressType;
                    cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 30).Value = cust.Hub == null ? (object)DBNull.Value : cust.Hub;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
        }

        public List<Customer> GetCustomerDetaillist(string id)
        {
            List<Customer> CustomerList = new List<Customer>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "Customer_GetCustomerDetailbyIdlistest";
                cmd.Parameters.Add("@Id", SqlDbType.VarChar,50).Value = id;
                cmd.Parameters.Add("@Type", SqlDbType.VarChar, 15).Value = "Default";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CustomerList.Add(new Customer
                        {
                          
                            CustomerId = Convert.ToString(rd["Id"]),
                            Name = Convert.ToString(rd["Name"] == DBNull.Value ? "NA" : rd["Name"]),
                            EmailId = Convert.ToString(rd["EmailId"] == DBNull.Value ? "NA" : rd["EmailId"]),
                            ContactNo = Convert.ToString(rd["ContactNo"] == DBNull.Value ? "NA" : rd["ContactNo"]),
                            Address1 = Convert.ToString(rd["address1"] == DBNull.Value ? "NA" : rd["address1"]),
                            AddId = Convert.ToString(rd["AddId"] == DBNull.Value ? "NA" : rd["AddId"]),

                        });
                    }
                    return CustomerList;
                }
            }
        }

        public bool delcustomer(string Id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[delcustomerlist]", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@addid", SqlDbType.VarChar,30).Value = Id;
                        return cmd.ExecuteNonQuery() > 0;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
        }

        public int editaddress(Customer cust)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_updateCustomerAddress]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@addId", SqlDbType.VarChar,30).Value = cust.AddId;
                    cmd.Parameters.Add("@Sector", SqlDbType.VarChar, 10).Value = cust.Sector == null ? (object)DBNull.Value : cust.Sector;
                    cmd.Parameters.Add("@Locality", SqlDbType.VarChar, 10).Value = cust.Locality == null ? "NA" : cust.Locality;
                    cmd.Parameters.Add("@Landmark", SqlDbType.VarChar, 50).Value = cust.Landmark == null ? (object)DBNull.Value : cust.Landmark;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 30).Value = cust.City == null ? (object)DBNull.Value : cust.City;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 10).Value = cust.State == null ? (object)DBNull.Value : cust.State;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar, 10).Value = cust.Country == null ? (object)DBNull.Value : cust.Country;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = cust.CreatedBy == null ? (object)DBNull.Value : cust.CreatedBy;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = cust.Address2 == null ? (object)DBNull.Value : cust.Address2;
                    cmd.Parameters.Add("@AddressType", SqlDbType.VarChar, 50).Value = cust.AddressType == null ? (object)DBNull.Value : cust.AddressType;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
        }

        public async Task<Customer> GetCustomerDataId(string Type, string custId)
        {
            try
            {
                Customer customerslist = null;
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Customer_GetCustomerDetailbyIdadd]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar, 30).Value = custId;
                    cmd.Parameters.Add("@Type", SqlDbType.VarChar, 50).Value = Type;
                    con.Open();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        if (rd.Read())
                        {
                             customerslist = new Customer
                            {
                                Id1 = Convert.ToString(rd["Id"]),
                                CustomerId = Convert.ToString(rd["CustomerId"]),
                                Name = rd["Name"] == DBNull.Value ? null : Convert.ToString(rd["Name"]),
                                EmailId = rd["EmailId"] == DBNull.Value ? null : Convert.ToString(rd["EmailId"]),
                                ContactNo = rd["ContactNo"] == DBNull.Value ? null : Convert.ToString(rd["ContactNo"]),
                                BuildingName = rd["BuildingName"] == DBNull.Value ? null : Convert.ToString(rd["BuildingName"]),
                                RoomNo = rd["RoomNo"] == DBNull.Value ? null : Convert.ToString(rd["RoomNo"]),
                                Sector = rd["Sector"] == DBNull.Value ? null : Convert.ToString(rd["Sector"]),
                                Landmark = rd["Landmark"] == DBNull.Value ? null : Convert.ToString(rd["Landmark"]),
                                Locality = rd["Locality"] == DBNull.Value ? null : Convert.ToString(rd["Locality"]),
                                AddressType = rd["AddressType"] == DBNull.Value ? null : Convert.ToString(rd["AddressType"]),
                                Address2 = rd["Address"] == DBNull.Value ? null : Convert.ToString(rd["Address"]),
                                ZipCode = rd["ZipCode"] == DBNull.Value ? null : Convert.ToString(rd["ZipCode"]),
                                City = rd["City"] == DBNull.Value ? null : Convert.ToString(rd["City"]),
                                State = rd["State"] == DBNull.Value ? null : Convert.ToString(rd["State"]),
                                Country = rd["Country"] == DBNull.Value ? null : Convert.ToString(rd["Country"]),
                                AddId = rd["AddId"] == DBNull.Value ? null : Convert.ToString(rd["AddId"]),
                                Type = rd["Type"] == DBNull.Value ? null : Convert.ToString(rd["Type"]),
                                computeAddIds = Convert.ToString(rd["computeaddid"]),
                                Address1 = rd["RoomNo"] + " " + rd["BuildingName"] + "," + rd["Sector"] + "," + rd["Locality"] + "," + rd["City"] + "-" + rd["ZipCode"],
                            };
                        }
                        return customerslist;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
       
        public int AddsecAddress(Customer cust)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_AddCustomerAddresssec]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 50).Value = cust.CustomerId == null ? (object)DBNull.Value : cust.CustomerId;
                    cmd.Parameters.Add("@Sector", SqlDbType.VarChar, 10).Value = cust.Sector == null ? (object)DBNull.Value : cust.Sector;
                    cmd.Parameters.Add("@Locality", SqlDbType.VarChar, 10).Value = cust.Locality == null ? "NA" : cust.Locality;
                    cmd.Parameters.Add("@Landmark", SqlDbType.VarChar, 50).Value = cust.Landmark == null ? (object)DBNull.Value : cust.Landmark;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 30).Value = cust.City == null ? (object)DBNull.Value : cust.City;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 10).Value = cust.State == null ? (object)DBNull.Value : cust.State;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar, 10).Value = cust.Country == null ? (object)DBNull.Value : cust.Country;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = cust.CreatedBy == null ? (object)DBNull.Value : cust.CreatedBy;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = cust.Address1 == null ? (object)DBNull.Value : cust.Address1;
                    cmd.Parameters.Add("@AddressType", SqlDbType.VarChar, 50).Value = cust.AddressType == null ? (object)DBNull.Value : cust.AddressType;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
        }

        public int EditAddress(Customer cust)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_updateCustomerAddress]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@addId", SqlDbType.VarChar, 30).Value = cust.AddId;
                    cmd.Parameters.Add("@Sector", SqlDbType.VarChar, 10).Value = cust.Sector == null ? (object)DBNull.Value : cust.Sector;
                    cmd.Parameters.Add("@Locality", SqlDbType.VarChar, 10).Value = cust.Locality == null ? "NA" : cust.Locality;
                    cmd.Parameters.Add("@Landmark", SqlDbType.VarChar, 50).Value = cust.Landmark == null ? (object)DBNull.Value : cust.Landmark;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 30).Value = cust.City == null ? (object)DBNull.Value : cust.City;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 10).Value = cust.State == null ? (object)DBNull.Value : cust.State;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar, 10).Value = cust.Country == null ? (object)DBNull.Value : cust.Country;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = cust.CreatedBy == null ? (object)DBNull.Value : cust.CreatedBy;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = cust.Address2 == null ? (object)DBNull.Value : cust.Address2;
                    cmd.Parameters.Add("@AddressType", SqlDbType.VarChar, 50).Value = cust.AddressType == null ? (object)DBNull.Value : cust.AddressType;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
        }
    }
}

