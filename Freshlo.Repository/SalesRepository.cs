using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.DomainEntities.PaymentSettlement;
using Freshlo.DomainEntities.Stock;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Repository
{
    public class SalesRepository : ISalesRI
    {
        public SalesRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        private IDbConfig _dbConfig { get; }
        public async Task<List<Item>> GetallItemList(string mainCategory, string condition, string hubId = null, string ItemName = null)
        {
            var itemList = new List<Item>();
            ItemName = ItemName == null ? "0" : ItemName;
            mainCategory = mainCategory == null ? "0" : mainCategory;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                if (mainCategory == "0")
                {
                    cmd.CommandText = "[dbo].[GetSalesOrderItemList1222]";
                }
                else
                {
                    cmd.CommandText = "[dbo].[GetSalesOrderItemList]";
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@HubId", SqlDbType.VarChar).Value = hubId; 
                /*string.IsNullOrEmpty(hubId) ? (object)DBNull.Value : hubId*/;
                cmd.Parameters.Add("@Condition", SqlDbType.VarChar).Value = condition;
                if (!string.IsNullOrEmpty(ItemName))
                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = ItemName;
                if (!string.IsNullOrEmpty(mainCategory))
                {
                    cmd.Parameters.Add("@mainCategory", SqlDbType.VarChar).Value = mainCategory;
                }
                con.Open();
                try
                {
                    DataSet dt = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    DataTable dt1 = new DataTable();
                    dt1 = dt.Tables[0];
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        while (rd.Read())
                        {
                            itemList.Add(new Item
                            {
                                Id = Convert.ToInt32(rd["Id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Id"])),
                                PluCode = Convert.ToString(rd["PluCode"] == DBNull.Value ? null : Convert.ToString(rd["PluCode"])),
                                PluName = Convert.ToString(rd["PluName"] == DBNull.Value ? null : Convert.ToString(rd["PluName"])),
                                ItemId = Convert.ToString(rd["ItemId"] == DBNull.Value ? null : Convert.ToString(rd["ItemId"])),
                                Measurement = Convert.ToString(rd["Measurement"] == DBNull.Value ? null : Convert.ToString(rd["Measurement"])),
                                Weight = Convert.ToDouble(rd["Weight"] == DBNull.Value ? null : Convert.ToString(rd["Weight"])),
                                Category = Convert.ToString(rd["Category"] == DBNull.Value ? null : Convert.ToString(rd["Category"])),
                                Approval = Convert.ToString(rd["Approval"] == DBNull.Value ? null : Convert.ToString(rd["Approval"])),
                                seasonSale = Convert.ToString(rd["seasonSale"] == DBNull.Value ? null : Convert.ToString(rd["seasonSale"])),
                                ItemSellingType = Convert.ToString(rd["ItemSellingType"] == DBNull.Value ? null : Convert.ToString(rd["ItemSellingType"])),
                                TotalStock = rd["TotalStock"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["TotalStock"]),
                                MaxQuantityAllowed = Convert.ToInt32(rd["MaxQuantityAllowed"] == DBNull.Value ? null : Convert.ToString(rd["MaxQuantityAllowed"])),
                                SellingPrice = Convert.ToDouble(rd["SellingPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["SellingPrice"])),
                                MarketPrice = Convert.ToDouble(rd["MarketPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["MarketPrice"])),
                                MainCategory = Convert.ToString(rd["MainCategory"] == DBNull.Value ? "NA" : Convert.ToString(rd["MainCategory"])),
                                ActualCost = Convert.ToDouble(rd["ActualCost"] == DBNull.Value ? 0 : Convert.ToDouble(rd["ActualCost"])),
                                DiscountPerctg = rd["DiscountedPrice"] == DBNull.Value ? 00 : Convert.ToDouble(rd["DiscountedPrice"]),
                                ImagePath = "https://freshlo.oss-ap-south-1.aliyuncs.com/freshpos/icon/" + Convert.ToString(rd["ItemId"]) + ".png",
                                ItemType = Convert.ToString(rd["ItemType"] == DBNull.Value ? null : Convert.ToString(rd["ItemType"])),
                                stockId = Convert.ToInt32(rd["StockId"] == DBNull.Value ? 0 : Convert.ToInt32(rd["StockId"])),
                                Stock = Convert.ToSingle(rd["Stock"] == DBNull.Value ? 0 : Convert.ToSingle(rd["Stock"])),
                                CategoryName = Convert.ToString(rd["CategoryName"] == DBNull.Value ? null : Convert.ToString(rd["CategoryName"])),
                                Purchaseprice = Convert.ToDouble(rd["Purchaseprice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["Purchaseprice"]))
                            });
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return itemList;
        }

        public async Task<List<Item>> GetallItemList_1(string ItemName = null, string CatogeryName = null)
        {
            var itemList = new List<Item>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[GetSalesOrderItemList_Customer]";
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(ItemName))
                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = ItemName;

                if (!string.IsNullOrEmpty(CatogeryName))
                    cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = CatogeryName;


                con.Open();
                try
                {
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        while (rd.Read())
                        {
                            itemList.Add(new Item
                            {
                                PluCode = Convert.ToString(rd["PluCode"]),
                                PluName = Convert.ToString(rd["PluName"]),
                                ItemId = Convert.ToString(rd["ItemId"]),
                                Measurement = Convert.ToString(rd["Measurement"]),
                                Weight = Convert.ToDouble(rd["Weight"]),
                                Category = Convert.ToString(rd["Category"]),
                                seasonSale = Convert.ToString(rd["seasonSale"]),
                                Approval = Convert.ToString(rd["Approval"]),
                                ItemSellingType = Convert.ToString(rd["ItemSellingType"]),
                                MaxQuantityAllowed = Convert.ToInt32(rd["MaxQuantityAllowed"]),
                                SellingPrice = Convert.ToDouble(rd["SellingPrice"]),
                                MarketPrice = Convert.ToDouble(rd["MarketPrice"]),
                                OfferDescription = rd["OfferDescription"] == DBNull.Value ? null : Convert.ToString(rd["OfferDescription"]),
                                OfferHeading = rd["OfferHeading"] == DBNull.Value ? null : Convert.ToString(rd["OfferHeading"]),
                                DiscountPerctg = rd["DiscountPerctg"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["DiscountPerctg"]),
                                OfferEndDate = rd["OfferEndDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rd["OfferEndDate"]),
                                DiscountedPrice = rd["DiscountedPrice"] == DBNull.Value ? null : Convert.ToString(rd["DiscountedPrice"]),
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

        public async Task<Item> GetItemDetails(string itemId)
        {
            var itemDetail = new Item();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[GettheItemDetails]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = itemId;

                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    if (rd.Read())
                    {

                        itemDetail.ItemId = Convert.ToString(rd[0]);
                        itemDetail.PluName = Convert.ToString(rd[1]);
                        itemDetail.Measurement = Convert.ToString(rd[2]);
                        itemDetail.Weight = Convert.ToDouble(rd[3]);
                        itemDetail.Purchaseprice = Convert.ToDouble(rd[4]);
                        itemDetail.SellingPrice = Convert.ToDouble(rd[5]);
                        itemDetail.ItemType = Convert.ToString(rd[6]);
                        itemDetail.CategoryName = Convert.ToString(rd[7]);
                        itemDetail.Category = Convert.ToString(rd[8]);
                        itemDetail.MarketPrice = Convert.ToDouble(rd[9]);
                        itemDetail.TotalStock = rd["itemstock"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["itemstock"]);
                        itemDetail.Id = Convert.ToInt32(rd["StockId"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["StockId"]));
                        itemDetail.DiscountPerctg = rd["DiscountedPrice"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["DiscountedPrice"]);
                        itemDetail.PluCode = rd["PluCode"] == DBNull.Value ? null : Convert.ToString(rd["PluCode"]);
                    }
                    return itemDetail;
                }
            }
        }
        public string CreateorUpdateCustomerDetail(Customer customerdata)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Customer_CreateorUpdateCustomerDetail]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = customerdata.Id;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = customerdata.Name == null ? "NA" : customerdata.Name;
                cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = customerdata.EmailId == null ? (object)DBNull.Value : customerdata.EmailId;
                cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar, 30).Value = customerdata.ContactNo == null ? (object)DBNull.Value : customerdata.ContactNo;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 10).Value = customerdata.CreatedBy == null ? (object)DBNull.Value : customerdata.CreatedBy;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 10).Value = customerdata.LastUpdatedBy == null ? (object)DBNull.Value : customerdata.LastUpdatedBy;
                con.Open();
                return Convert.ToString(cmd.ExecuteScalar());
            }
        }
        public string CreateorUpdateCustomerAddress(Customer customersAddress)
        {
            //customersAddress.Type = customersAddress.CustomerId == null ? customersAddress.Type = "Default" : customersAddress.Type = "Normal";
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Customer_CreateorUpdateCustomerAddress]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = customersAddress.Addids;
                cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 50).Value = customersAddress.CustomerId;
                cmd.Parameters.Add("@BuildingName", SqlDbType.VarChar, 50).Value = customersAddress.BuildingName == null ? (object)DBNull.Value : customersAddress.BuildingName;
                cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar, 30).Value = customersAddress.RoomNo == null ? (object)DBNull.Value : customersAddress.RoomNo;
                cmd.Parameters.Add("@Sector", SqlDbType.VarChar, 20).Value = customersAddress.Sector == null ? (object)DBNull.Value : customersAddress.Sector;
                cmd.Parameters.Add("@Locality", SqlDbType.VarChar, 10).Value = customersAddress.Locality == null ? (object)DBNull.Value : customersAddress.Locality;
                cmd.Parameters.Add("@Landmark", SqlDbType.VarChar, 10).Value = customersAddress.Landmark == null ? (object)DBNull.Value : customersAddress.Landmark;
                cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 10).Value = customersAddress.ZipCode == null ? (object)DBNull.Value : customersAddress.ZipCode;
                cmd.Parameters.Add("@City", SqlDbType.VarChar, 10).Value = customersAddress.City == null ? (object)DBNull.Value : customersAddress.City;
                cmd.Parameters.Add("@State", SqlDbType.VarChar, 10).Value = customersAddress.State == null ? (object)DBNull.Value : customersAddress.State;
                cmd.Parameters.Add("@Country", SqlDbType.VarChar, 10).Value = customersAddress.Country == null ? (object)DBNull.Value : customersAddress.Country;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 10).Value = customersAddress.CreatedBy == null ? (object)DBNull.Value : customersAddress.CreatedBy;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 10).Value = customersAddress.LastUpdatedBy == null ? (object)DBNull.Value : customersAddress.LastUpdatedBy;
                cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 10).Value = customersAddress.Hub == null ? (object)DBNull.Value : customersAddress.Hub;
                cmd.Parameters.Add("@Type", SqlDbType.VarChar, 10).Value = customersAddress.Type == null ? (object)DBNull.Value : customersAddress.Type;
                cmd.Parameters.Add("@AddressType", SqlDbType.VarChar, 10).Value = customersAddress.AddressType == null ? (object)DBNull.Value : customersAddress.AddressType;

                con.Open();
                return Convert.ToString(cmd.ExecuteScalar());
            }
        }
        public async Task<List<SelectListItem>> GetCustomerContactDetail()
        {
            List<SelectListItem> user = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Sales_GetCustomerContactDetail_SL]";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (rd.Read())
                    {
                        string Id = Convert.ToString(rd["Id"]);
                        string temp = Convert.ToString(rd["CustomerContactNo"]);
                        user.Add(new SelectListItem
                        {
                            Text = temp,
                            Value = Id
                        });
                    }
                    return user;
                }
            }
        }
        public async Task<Customer> GetCustomerDataId(string Type, string custId)
        {
            Customer customerslist = null;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Customer_GetCustomerDetailbyId]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 200).Value = custId;
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
                            //Ext = rd["Ext"] == DBNull.Value ? null : Convert.ToString(rd["Ext"]),
                            ContactNo = rd["ContactNo"] == DBNull.Value ? null : Convert.ToString(rd["ContactNo"]),
                            BuildingName = rd["BuildingName"] == DBNull.Value ? null : Convert.ToString(rd["BuildingName"]),
                            RoomNo = rd["RoomNo"] == DBNull.Value ? null : Convert.ToString(rd["RoomNo"]),
                            Sector = rd["Sector"] == DBNull.Value ? null : Convert.ToString(rd["Sector"]),
                            Landmark = rd["Landmark"] == DBNull.Value ? null : Convert.ToString(rd["Landmark"]),
                            Locality = rd["Locality"] == DBNull.Value ? null : Convert.ToString(rd["Locality"]),
                            AddressType = rd["AddressType"] == DBNull.Value ? null : Convert.ToString(rd["AddressType"]),
                            ZipCode = rd["ZipCode"] == DBNull.Value ? null : Convert.ToString(rd["ZipCode"]),
                            City = rd["City"] == DBNull.Value ? null : Convert.ToString(rd["City"]),
                            State = rd["State"] == DBNull.Value ? null : Convert.ToString(rd["State"]),
                            Country = rd["Country"] == DBNull.Value ? null : Convert.ToString(rd["Country"]),
                            AddId = rd["AddId"] == DBNull.Value ? null : Convert.ToString(rd["AddId"]),
                            Type = rd["Type"] == DBNull.Value ? null : Convert.ToString(rd["Type"]),
                            computeAddIds = Convert.ToString(rd["computeaddid"]),
                            Address1 = rd["RoomNo"] + " " + rd["BuildingName"] + " " + rd["Sector"] + " " + rd["Locality"] + " " + rd["City"] + " " + rd["ZipCode"],
                        };
                    }
                    return customerslist;
                }
            }

        }

        public async Task<Customer> GetCustomerDetails(string ContactNo)
        {
            Customer customerslist = null;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Customer_GetCustomerDetailbyContactNo]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ContacNo", SqlDbType.VarChar, 50).Value = ContactNo;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    if (rd.Read())
                    {
                        customerslist = new Customer
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            CustomerId = Convert.ToString(rd["CustomerId"]),
                            Name = rd["Name"] == DBNull.Value ? null : Convert.ToString(rd["Name"]),
                            EmailId = rd["EmailId"] == DBNull.Value ? null : Convert.ToString(rd["EmailId"]),
                            ContactNo = rd["ContactNo"] == DBNull.Value ? null : Convert.ToString(rd["ContactNo"]),
                        };
                    }
                    return customerslist;
                }
            }

        }

        public Customer ValidateContactNumber(string Ext, string ContactNo)
        {
            Customer custcontactno = null;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Customer_GetallCustomerContactNumber]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar, 255).Value = ContactNo;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        custcontactno = new Customer
                        {
                            ContactNo = Convert.ToString(rd["ContactNo"])
                        };
                    }
                    return custcontactno;
                }
            }
        }
        public async Task<List<Customer>> GetCustomerMultipleAddId(int custId)
        {
            string customerId = "CI0" + Convert.ToString(custId);
            List<Customer> customeraddlist = new List<Customer>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Customer_GetCustomerAddressbyId]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar).Value = customerId;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (rd.Read())
                    {
                        customeraddlist.Add(new Customer
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            Type = rd["Type"] == DBNull.Value ? null : Convert.ToString(rd["Type"]),
                            AddId = rd["AddId"] == DBNull.Value ? null : Convert.ToString(rd["AddId"]),
                            CustomerId = rd["CustomerId"] == DBNull.Value ? null : Convert.ToString(rd["CustomerId"]),
                            Name = rd["FirstName"] == DBNull.Value ? null : Convert.ToString(rd["FirstName"]),
                            BuildingName = rd["BuildingName"] == DBNull.Value ? null : Convert.ToString(rd["BuildingName"]),
                            RoomNo = rd["RoomNo"] == DBNull.Value ? null : Convert.ToString(rd["RoomNo"]),
                            Sector = rd["Sector"] == DBNull.Value ? null : Convert.ToString(rd["Sector"]),
                            Locality = rd["Locality"] == DBNull.Value ? null : Convert.ToString(rd["Locality"]),
                            Landmark = rd["Landmark"] == DBNull.Value ? null : Convert.ToString(rd["Landmark"]),
                            ZipCode = rd["ZipCode"] == DBNull.Value ? null : Convert.ToString(rd["ZipCode"]),
                            City = rd["City"] == DBNull.Value ? null : Convert.ToString(rd["City"]),
                            State = rd["State"] == DBNull.Value ? null : Convert.ToString(rd["State"]),
                            Country = rd["Country"] == DBNull.Value ? null : Convert.ToString(rd["Country"]),

                        });
                    }
                    return customeraddlist;
                }
            }

        }
        public int UpdateAddressNormal(string custermId, string addressId, string Type)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Customer_UpdateAddresstype]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@custId", SqlDbType.VarChar, 50).Value = custermId;
                cmd.Parameters.Add("@Type", SqlDbType.VarChar, 50).Value = Type;
                cmd.Parameters.Add("@addId", SqlDbType.VarChar, 50).Value = addressId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int DeleteCustAddress(string custAdressId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Customer_DeletecustAdd]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@addId", SqlDbType.VarChar, 50).Value = custAdressId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public string InsertSale(Sales dicData, string createdBy, string branch)
        {
            var result = "";
            dicData.PaymentStatus = string.IsNullOrEmpty(dicData.PaymentStatus) ? "NA" : dicData.PaymentStatus;
            dicData.PaymentMode = string.IsNullOrEmpty(dicData.PaymentMode) ? "NA" : dicData.PaymentMode;
            dicData.SalesPerson = string.IsNullOrEmpty(dicData.SalesPerson) ? "NA" : dicData.SalesPerson;
            dicData.SalesPerson = dicData.OrderdStatus == "Delivered" ? createdBy : dicData.SalesPerson;
            dicData.status = "Pending";
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        //cmd.CommandText = "[dbo].[AddSaleKitchenOrderListOrderTypeWise]";
                        cmd.CommandText = "[dbo].[usp_POSsalesCreate]";
                        DataTable odt = new DataTable();
                        odt.Columns.Add("SalesId", typeof(string));
                        odt.Columns.Add("CustomerId", typeof(string));
                        odt.Columns.Add("Category", typeof(string));
                        odt.Columns.Add("ItemId", typeof(string));
                        odt.Columns.Add("PriceId", typeof(string));
                        odt.Columns.Add("Measurement", typeof(string));
                        odt.Columns.Add("QuantityValue", typeof(float));
                        odt.Columns.Add("Remark", typeof(string));
                        odt.Columns.Add("PricePerMeas", typeof(float));
                        odt.Columns.Add("TotalPrice", typeof(float));
                        odt.Columns.Add("Discount", typeof(float));
                        odt.Columns.Add("weight", typeof(float));
                        odt.Columns.Add("DeliveryDate", typeof(DateTime));
                        odt.Columns.Add("CreatedBy", typeof(string));
                        odt.Columns.Add("LastUpdatedBy", typeof(string));
                        if (dicData.MultipleItem != null)
                            foreach (var data in dicData.MultipleItem)
                                odt.Rows.Add("SO01"
                                      , dicData.CustomerId
                                    , data.Split('_')[5]//Category
                                    , data.Split('_')[0]//ItemId
                                    , data.Split('_')[1]//PriceId
                                    , data.Split('_')[8]//Measurement
                                    , data.Split('_')[3]//QuantValue
                                    , data.Split('_')[4]//Remark
                                    , data.Split('_')[2]//PricePerMeas
                                    , data.Split('_')[6]//TotalPrice
                                    , data.Split('_')[10]//Discount
                                    , data.Split('_')[7]//Weight
                                    , dicData.Deliverydt
                                    , createdBy
                                    , createdBy
                                    );                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@deliverydate", SqlDbType.NVarChar, 100).Value = dicData.DeliveryDate;
                        cmd.Parameters.AddWithValue("@customerId", dicData.CustomerId);
                        cmd.Parameters.AddWithValue("@salesPerson", dicData.SalesPerson);
                        cmd.Parameters.AddWithValue("@pulCount", dicData.PLU_Count);
                        cmd.Parameters.AddWithValue("@quantity", dicData.TotalQuantity);
                        cmd.Parameters.AddWithValue("@totalCost", dicData.TotalPrice);
                        cmd.Parameters.AddWithValue("@orderStatus", dicData.OrderdStatus);
                        cmd.Parameters.AddWithValue("@paymentStatus", dicData.PaymentStatus);
                        cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                        cmd.Parameters.AddWithValue("@PaymentMode", dicData.PaymentMode);
                        cmd.Parameters.AddWithValue("@SlotId", dicData.SlotId);
                        cmd.Parameters.AddWithValue("@addId", dicData.AddressId);
                        cmd.Parameters.AddWithValue("@hub", dicData.HubId);
                        cmd.Parameters.AddWithValue("@Branch", dicData.HubId);
                        cmd.Parameters.AddWithValue("@PurchaseId", "NA");
                        cmd.Parameters.Add("@DeliveryCharges", SqlDbType.VarChar, 100).Value = dicData.DeliveryCharges;
                        cmd.Parameters.Add("@Discount", SqlDbType.VarChar,50).Value = dicData.ActualDiscAmt;
                        cmd.Parameters.Add("@DeliveryType", SqlDbType.VarChar, 100).Value = dicData.DeliveryType == null ? "NA" : dicData.DeliveryType;
                        cmd.Parameters.Add("@Remaining_Amount", SqlDbType.Float).Value = dicData.PaymentStatus == "Partially" ? dicData.Remaining_Amount : 0;
                        cmd.Parameters.Add("@CoupenId", SqlDbType.Int).Value = dicData.coupenId;
                        cmd.Parameters.AddWithValue("@bags", dicData.tableId).Value = dicData.Bags == null ? "NA" : dicData.Bags;
                        cmd.Parameters.AddWithValue("@OrderType", dicData.OrderType).Value = dicData.OrderType == null ? "HOD" : dicData.OrderType;
                        cmd.Parameters.Add("@tblsalesList", SqlDbType.Structured).Value = odt;
                        //cmd.Parameters.Add("@totalCost", SqlDbType.Float).Value = dicData.TotaldisAmt;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = Convert.ToString(reader[0]);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        return result;
                    }
                    finally
                    {
                        if (sqlcon.State != ConnectionState.Closed)
                        {
                            sqlcon.Close();
                            sqlcon.Dispose();
                        }
                    }
                }
            }
            return result;
        }
        public bool InsertProductforSale(string insertValue)
        {
            var query =
                @"INSERT INTO [dbo].[SaleOrderssList] (ItemId,CustomerId,QuantityValue,PricePerMeas,TotalPrice,Category,SalesId,DeliveryDate,weight,Measurement,CreatedBy,Discount) VALUES " +
                insertValue + "";

            using (var sqlCon = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    sqlCon.Open();
                    var cmd = new SqlCommand(query, sqlCon);
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception e)
                {
                    return false;
                }
                finally
                {
                    if (sqlCon.State != ConnectionState.Closed)
                    {
                        sqlCon.Close();
                        sqlCon.Dispose();
                    }
                }

            }
        }
        public async Task<List<SalesList>> GetSalesList(string id)
        {
            List<SalesList> salesList = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[GetSalesList]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                con.Open();
                using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        salesList.Add(new SalesList
                        {
                            QuantityValue = Convert.ToDouble(dataReader[0]),
                            PricePerMeas = Convert.ToDouble(dataReader[1]),
                            TotalPrice = Convert.ToDouble(dataReader[2]),
                            ItemId = Convert.ToString(dataReader[3]),
                            PluName = Convert.ToString(dataReader[4]),
                            CName = Convert.ToString(dataReader[5]),
                            CategoryId = Convert.ToString(dataReader[6]),
                            Weight = Convert.ToDouble(dataReader[7]),
                            Measurement = Convert.ToString(dataReader[8]),
                            ItemSellingType = Convert.ToString(dataReader[9]),
                            Stock = dataReader[10] == DBNull.Value ? 0 : Convert.ToDouble(dataReader[10]),
                            SalesOrderId = Convert.ToString(dataReader[11]),
                            ItemType = Convert.ToString(dataReader[14]),
                            weight = Convert.ToDouble(dataReader[15]),
                            //MarketPrice= Convert.ToDouble(dataReader[16]),
                            //QuantityMarketPrice= Convert.ToDouble(dataReader[17]),
                            //WeightMarketPrice = Convert.ToDouble(dataReader[18]),

                        });
                    }
                    return salesList;
                }
            }

        }
        public async Task<Sales> GetSalesOrderdetail(string id,string hubId)
        {
            var salesdata = new Sales();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[GetSalesOrderDetails]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = hubId;
                con.Open();
                try
                {
                    using (SqlDataReader dataRead = await cmd.ExecuteReaderAsync())
                    {
                        if (dataRead.Read())
                        {
                            salesdata.Id = Convert.ToInt32(dataRead[0]);
                            salesdata.SalesOrderId = Convert.ToString(dataRead[1]);
                            salesdata.CustomerId = Convert.ToString(dataRead[2]);
                            salesdata.Name = Convert.ToString(dataRead[3]);
                            salesdata.SalesPerson = Convert.ToString(dataRead[4]);
                            salesdata.OrderdStatus = Convert.ToString(dataRead[5]);
                            salesdata.PaymentStatus = Convert.ToString(dataRead[6]);
                            salesdata.PluCount = Convert.ToInt32(dataRead[7]);
                            salesdata.TotalPrice = Convert.ToDouble(dataRead[8]);
                            salesdata.Discount = Convert.ToDouble(dataRead["Discount"]);
                            salesdata.SlotId = Convert.ToString(dataRead[9]);
                            salesdata.AddressId = Convert.ToString(dataRead[10]);
                            salesdata.PaymentMode = Convert.ToString(dataRead[11]);
                            salesdata.ContactNo = Convert.ToString(dataRead[12]);
                            salesdata.HubId = Convert.ToString(dataRead[13]);
                            salesdata.LastUpdatedOn = Convert.ToDateTime(dataRead[14]);
                            salesdata.CreatedBy = Convert.ToString(dataRead[15]);
                            salesdata.EmpName = Convert.ToString(dataRead[16]);
                            salesdata.TotalQuantity = Convert.ToDouble(dataRead[17]);
                            salesdata.DeliveryCharges = Convert.ToString(dataRead[18]);
                            salesdata.TotalAmount = Convert.ToDouble(dataRead["TotalCost"]);
                            salesdata.BuildingName = dataRead["BuildingName"] as string;
                            salesdata.RoomNo = dataRead["RoomNo"] as string;
                            salesdata.Sector = Convert.ToString(dataRead["Sector"]);
                            salesdata.Locality = Convert.ToString(dataRead["Locality"]);
                            salesdata.City = Convert.ToString(dataRead["City"]);
                            salesdata.ZipCode = Convert.ToString(dataRead["ZipCode"]);
                            salesdata.Wallet = Convert.ToDouble(dataRead["Wallet"]);
                            salesdata.Source = Convert.ToString(dataRead["Source"]);
                            //salesdata.tableName = Convert.ToString(dataRead["tableName"]);
                            salesdata.taxType = Convert.ToInt32(dataRead["taxType"]);
                            salesdata.taxbillType = Convert.ToInt32(dataRead["taxbillType"]);
                            salesdata.vatTax = Convert.ToSingle(dataRead["vatTax"] == DBNull.Value ? 0.0 : Convert.ToSingle(dataRead["vatTax"]));
                            salesdata.OrderType = Convert.ToString(dataRead["OrderType"]);
                            salesdata.gstTax = Convert.ToSingle(dataRead["gstTax"] == DBNull.Value ? 0 : dataRead["gstTax"]);
                        }
                        return salesdata;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public List<Customer> GetCustomerContactDetail(string searchTerm)
        {
            List<Customer> customerList = new List<Customer>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_GetCustomerContactDetail_SL]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@searchTerm", SqlDbType.VarChar, 100).Value = searchTerm;

                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        customerList.Add(new Customer
                        {
                            Id1 = Convert.ToString(rd["Id"]),
                            ContactNo = Convert.ToString(rd["PhoneNumber"]),
                            Name = Convert.ToString(rd["fullName"])
                        });
                    }
                    
                    return customerList;
                }
            }
        }
        public List<Item> GetItemList(string searchTerm,string id)
        {
            var insertValue = new StringBuilder();
            List<Item> itemList = new List<Item>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Item_GetList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(searchTerm))
                    cmd.Parameters.Add("@searchTerm", SqlDbType.VarChar, 100).Value = searchTerm;
                    //cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 30).Value = id;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            itemList.Add(new Item
                            {

                                ItemId = rd["ItemId"] as string,
                                PluName = rd["PluName"] as string,
                                Measurement = rd["Measurement"] as string,
                                //Size = rd["Size"] as string,
                                Weight = Convert.ToDouble(rd[3]),
                                SellingPrice = Convert.ToDouble(rd[4]),
                                Category = rd["Category"] as string,
                                ItemType = rd["ItemType"] as string,
                                CategoryName = Convert.ToString(rd[7]),
                                MarketPrice = Convert.ToDouble(rd[8]),
                                TotalStock = Convert.ToDouble(rd[10] == DBNull.Value ? 0 : Convert.ToDouble(rd[10])),
                                DiscountPerctg = rd["DiscountedPrice"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["DiscountedPrice"]),
                                stockId = Convert.ToInt32(rd["StockId"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["StockId"])),
                                Purchaseprice = Convert.ToDouble(rd["PurchasePrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["PurchasePrice"])),
                                ProfitMargin = Convert.ToDouble(rd["ProfitMargin"] == DBNull.Value ? 0 : Convert.ToDouble(rd["ProfitMargin"])),
                                CoupenDisc = Convert.ToInt32(rd["Coupen_Disc"] == DBNull.Value ? 0 : Convert.ToDouble(rd["Coupen_Disc"])),
                                Id = Convert.ToInt32(rd["Item_Id"]),
                                //PId = Convert.ToInt32(rd["Price_Id"]),
                                Tax_Value = Convert.ToDouble(rd["TaxValue"]),
                                MainCategory = Convert.ToString(rd["MainCategory"]),
                                Name = Convert.ToString(rd["Name"]),
                                MaincatId = Convert.ToString(rd["MainCategoryId"]),
                                ImagePath = "https://freshlo.oss-ap-south-1.aliyuncs.com/freshpos/icon/" + Convert.ToString(rd[2]) + ".png",

                            });
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return itemList;

        }
        public List<PriceMap> GetItemvaraintList(string ItemId,string id)
        {
            List<PriceMap> itemvList = new List<PriceMap>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetVarintListbyItemId]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@itemId", SqlDbType.VarChar, 50).Value = ItemId;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = id;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            itemvList.Add(new PriceMap
                            {
                                ItemId = rd["ItemId"] as string,
                                Size = rd["Size"] as string,
                                SellingPrice = Convert.ToDouble(rd["SellingPrice"]),
                                MarketPrice = Convert.ToDouble(rd["MarketPrice"]),
                                PurchasePrice = Convert.ToDouble(rd["PurchasePrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["PurchasePrice"])),
                                ProfitMargin = Convert.ToDouble(rd["ProfitMargin"] == DBNull.Value ? 0 : Convert.ToDouble(rd["ProfitMargin"])),
                                Id = Convert.ToInt32(rd["Item_Id"]),
                                PId = Convert.ToInt32(rd["Price_Id"]),
                                PriceId = Convert.ToString(rd["PriceId"]),

                            });
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return itemvList;

        }

        public async Task<List<Item>> GetObjectFromFile(List<Item> itemDetails, AliyunCredential credential)
        {
            var itemList = new List<Item>();
            //var client = new OssClient(credential.Endpoint, credential.AccessKeyId, credential.AccessKeySecret);

            try
            {

                foreach (var Item in itemDetails)
                {
                    var imagedata = GetImage(Item.ItemId);
                    Item.ImagePath = imagedata;
                    continue;
                }
            }
            catch (Exception ex)
            {
                return itemDetails;
            }
            return itemDetails;
        }
        public string GetImage(string itemid)
        {
            string Imgurl = "https://freshlo.oss-ap-south-1.aliyuncs.com/freshpos/icon/@name.png";
            var data = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] pic = client.DownloadData(Imgurl.Replace("@name", itemid));
                    string base64String = Convert.ToBase64String(pic);
                    data = base64String;
                }
                catch (Exception e)
                {
                    var dummyimg = "http://drawingzoro.com/wp-content/uploads/2018/03/fruit-and-veg-pencil-drawing-black-and-white-fruits-vegetables-clipart-basket-drawing-pencil-and-in-color.jpg";
                    byte[] pic = client.DownloadData(dummyimg);
                    string base64String = Convert.ToBase64String(pic);
                    data = base64String;

                    return data;
                }
                return data;
            }
        }
        public List<Sales> GetAllSalesOrderList(string branch, string hubId,string role, string date = null, string status = null, string source = null, string payment = null)
        {
            branch = role == "System Admin" ? null : branch;
            List<Sales> salesOrderList = new List<Sales>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetAllSalesOrderList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@sdate", SqlDbType.VarChar, 100).Value = date;
                cmd.Parameters.Add("@status", SqlDbType.VarChar, 20).Value = status;
                cmd.Parameters.Add("@source", SqlDbType.VarChar, 20).Value = source;
                cmd.Parameters.Add("@payment", SqlDbType.VarChar, 20).Value = payment;
                cmd.Parameters.Add("@branch", SqlDbType.VarChar, 20).Value = branch;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 20).Value = hubId;

                try
                {
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            salesOrderList.Add(new Sales
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                SalesOrderId = Convert.ToString(rd["SalesOrderId"] == DBNull.Value ? "NA" : rd["SalesOrderId"]),
                                CustomerId = Convert.ToString(rd["CustomerId"] == DBNull.Value ? "Not Registred" : rd["CustomerId"]),
                                Name = Convert.ToString(rd["Name"] == DBNull.Value ? "Guest" : rd["Name"]),
                                fullname = Convert.ToString(rd["fullname"] == DBNull.Value ? "Guest" : rd["fullname"]),
                                ContactNo = Convert.ToString(rd["ContactNo"] == DBNull.Value ? "7975073656" : rd["ContactNo"]),
                                phonenumber = Convert.ToString(rd["phonenumber"] == DBNull.Value ? "7975073656" : rd["phonenumber"]),
                                SalesPerson = Convert.ToString(rd["SalesPerson"] == DBNull.Value ? "NA" : rd["SalesPerson"]),
                                PluCount = Convert.ToInt32(rd["PLU_Count"] == DBNull.Value ? 0 : rd["PLU_Count"]),
                                TotalQuantity = Convert.ToDouble(rd["TotalQuantity"] == DBNull.Value ? null : rd["TotalQuantity"]),
                                TotalPrice = Convert.ToDouble(rd["TotalPrice"] == DBNull.Value ? null : rd["TotalPrice"]),
                                Discount = Convert.ToDouble(rd["Discount"] == DBNull.Value ? null : rd["Discount"]),
                                OrderdStatus = Convert.ToString(rd["OrderdStatus"] == DBNull.Value ? "NA" : rd["OrderdStatus"]),
                                PaymentStatus = Convert.ToString(rd["PaymentStatus"] == DBNull.Value ? "NA" : rd["PaymentStatus"]),
                                DeliveryDate1 = Convert.ToDateTime(rd["DeliveryDate"] == DBNull.Value ? "01-01-2021 12:00 AM" : rd["DeliveryDate"]),
                                CreatedBy = Convert.ToString(rd["CreatedBy"] == DBNull.Value ? "NA" : rd["CreatedBy"]),
                                CreatedOn = Convert.ToDateTime(rd["CreatedOn"] == DBNull.Value ? "01-01-2021 12:00 AM" : rd["CreatedOn"]),
                                Bags = Convert.ToString(rd["Bags"] == DBNull.Value ? "NA" : rd["Bags"]),
                                PaymentMode = Convert.ToString(rd["PaymentMode"] == DBNull.Value ? "NA" : rd["PaymentMode"]),
                                SlotId = Convert.ToString(rd["SlotId"] == DBNull.Value ? "NA" : rd["SlotId"]),
                                DeliveryType = Convert.ToString(rd["DeliveryType"] == DBNull.Value ? "NA" : rd["DeliveryType"]),
                                DeliveryCharges = Convert.ToString(rd["DeliveryCharges"] == DBNull.Value ? "NA" : rd["DeliveryCharges"]),
                                HubId = Convert.ToString(rd["HubId"] == DBNull.Value ? "NA" : rd["HubId"]),
                                Branch = Convert.ToString(rd["Branch"] == DBNull.Value ? "NA" : rd["Branch"]),
                                Source = Convert.ToString(rd["Source"] == DBNull.Value ? "NA" : rd["Source"]),
                                ItemName = Convert.ToString(rd["ItemName"] == DBNull.Value ? "NA" : rd["ItemName"]),
                                CreatedName = Convert.ToString(rd["CreatedName"] == DBNull.Value ? "NA" : rd["CreatedName"]),
                                currencytype = Convert.ToString(rd["Symbol"] == DBNull.Value ? "NA" : rd["Symbol"]),
                                UpdatedPersonName = Convert.ToString(rd["UpdatedPersonName"] == DBNull.Value ? "NA" : rd["UpdatedPersonName"]),
                                CoupenCalculation = Convert.ToDouble(rd["CoupenCalculation"] == DBNull.Value ? 0 : rd["CoupenCalculation"]),
                                Wallet = Convert.ToDouble(rd["Wallet"] == DBNull.Value ? 0 : rd["Wallet"]),
                                ModeFullForm = Convert.ToString(rd["Modes"] == DBNull.Value ? "NA" : rd["Modes"]),
                                CustId = Convert.ToString(rd["CustId"] == DBNull.Value ? "NA" : rd["CustId"]),
                                taxType = Convert.ToInt32(rd["taxType"] == DBNull.Value ? 0 : rd["taxType"]),
                                taxbillType = Convert.ToInt32(rd["taxbillType"] == DBNull.Value ? 0 : rd["taxbillType"]),
                                vatTax = Convert.ToSingle(rd["vatTax"] == DBNull.Value ? 0.0 : Convert.ToSingle(rd["vatTax"])),
                                gstTax = Convert.ToSingle(rd["gstTax"] == DBNull.Value ? 0 : rd["gstTax"]),
                                TaxableAmount = Convert.ToDouble(rd["TaxableAmount"] == DBNull.Value ? "00.00" : rd["TaxableAmount"]),

                            });
                        }
                        return salesOrderList;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
              
            }

        }
        public async Task<List<SelectListItem>> GetSlotList()
        {
            var slotList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[GetTimeSlot]";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        while (rd.Read())
                        {
                            string SlotId = Convert.ToString(rd["SlotId"]);
                            string Timing = Convert.ToString(rd["Timing"]);
                            slotList.Add(new SelectListItem
                            {
                                Text = Timing,
                                Value = SlotId
                            });
                        }

                    }
                }
                catch (Exception e)
                {

                }
            }
            return slotList;
        }
        public int UpdateStockData(Stock stockdata)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Stock_UpdateStock]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = stockdata.Id;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = stockdata.ItemId;
                cmd.Parameters.Add("@Stock", SqlDbType.Float).Value = stockdata.StockValue;
                cmd.Parameters.Add("@Measurement", SqlDbType.VarChar, 30).Value = stockdata.Measurement;
                cmd.Parameters.Add("@ItemPrice", SqlDbType.VarChar, 50).Value = stockdata.ItemPrice;
                cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 50).Value = stockdata.Hub;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = stockdata.UpdatedBy;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int InsertStockSalesLog(Stock stockdata)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Stock_CreateProdSalesLog]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@stockId", SqlDbType.Int).Value = stockdata.Id;
                cmd.Parameters.Add("@stock", SqlDbType.Float).Value = stockdata.salesStock;
                cmd.Parameters.Add("@ItemPrice", SqlDbType.Float).Value = stockdata.salesPrice;
                cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 50).Value = stockdata.Hub;
                cmd.Parameters.Add("@Createdby", SqlDbType.VarChar, 50).Value = stockdata.UpdatedBy;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //16-12-2021
        public int InsertCoupenLog(List<SalesList> salesList, List<SalesList> kitchenlist, int couponId, string saleId, string customerId, string LastUpdatedBy)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                DataTable odt = new DataTable();
                odt.Columns.Add("SalesId", typeof(string));
                odt.Columns.Add("CustomerId", typeof(string));
                odt.Columns.Add("Category", typeof(string));
                odt.Columns.Add("ItemId", typeof(string));
                odt.Columns.Add("PriceId", typeof(string));
                odt.Columns.Add("Measurement", typeof(string));
                odt.Columns.Add("QuantityValue", typeof(float));
                odt.Columns.Add("PricePerMeas", typeof(float));
                odt.Columns.Add("TotalPrice", typeof(float));
                odt.Columns.Add("Discount", typeof(float));
                odt.Columns.Add("weight", typeof(float));
                odt.Columns.Add("DeliveryDate", typeof(DateTime));
                odt.Columns.Add("CreatedBy", typeof(string));
                odt.Columns.Add("LastUpdatedBy", typeof(string));
                odt.Columns.Add("status", typeof(string));
                DataTable odt1 = new DataTable();
                odt1.Columns.Add("SalesOrderId", typeof(string));
                odt1.Columns.Add("CustomerId", typeof(string));
                odt1.Columns.Add("TableId", typeof(string));
                odt1.Columns.Add("ItemId", typeof(string));
                odt1.Columns.Add("PriceId", typeof(string));
                odt1.Columns.Add("TotalQty", typeof(float));
                odt1.Columns.Add("Status", typeof(string));
                odt1.Columns.Add("KOT_Status", typeof(int));
                odt1.Columns.Add("Remark", typeof(string));
                odt1.Columns.Add("CreatedBy", typeof(string));
                odt1.Columns.Add("LastUpdatedBy", typeof(string));
                if (salesList != null)
                    foreach (SalesList data in salesList)
                        odt.Rows.Add(data.SalesId,
                            data.CustomerId,
                            data.Category,
                            data.ItemId,
                            data.PriceId,
                            data.Measurement
                            , data.QuantityValue
                            , data.PricePerMeas
                            , data.TotalPrice
                            , data.Discount
                            , data.Weight
                            , DateTime.Today
                            , LastUpdatedBy
                            , LastUpdatedBy
                            , data.Status);
                if (kitchenlist != null)
                {
                    foreach (SalesList data in kitchenlist)
                        odt1.Rows.Add(data.SalesOrderId
                            , data.CustomerId
                            , data.tableId
                            , data.ItemId
                            , data.PriceId
                            , data.TotalQty
                            , data.Status
                            , data.KOT_Status
                            , data.Remark
                            , LastUpdatedBy
                            , LastUpdatedBy
                            );
                }
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Coupen_UpdateSales_CouponlogTESTfORfOODWAY]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 20).Value = saleId;
                cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 30).Value = customerId;
                cmd.Parameters.Add("@CoupenId", SqlDbType.Int).Value = couponId;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = LastUpdatedBy;
                cmd.Parameters.Add("@SalesOrderList", SqlDbType.Structured).Value = odt;
                cmd.Parameters.Add("@tblKitchenList", SqlDbType.Structured).Value = odt1;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        //16-12-2021
        public int InsertCashDiscount(List<SalesList> salesList, List<SalesList> kitchenlist, int couponId, string saleId, string customerId, float discount, float totalamount, string LastUpdatedBy)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    DataTable odt = new DataTable();
                    odt.Columns.Add("SalesId", typeof(string));
                    odt.Columns.Add("CustomerId", typeof(string));
                    odt.Columns.Add("Category", typeof(string));
                    odt.Columns.Add("ItemId", typeof(string));
                    odt.Columns.Add("Measurement", typeof(string));
                    odt.Columns.Add("QuantityValue", typeof(float));
                    odt.Columns.Add("PricePerMeas", typeof(float));
                    odt.Columns.Add("TotalPrice", typeof(float));
                    odt.Columns.Add("Discount", typeof(float));
                    odt.Columns.Add("weight", typeof(float));
                    odt.Columns.Add("DeliveryDate", typeof(DateTime));
                    odt.Columns.Add("CreatedBy", typeof(string));
                    odt.Columns.Add("LastUpdatedBy", typeof(string));
                    odt.Columns.Add("status", typeof(string));
                    DataTable odt1 = new DataTable();
                    odt1.Columns.Add("SalesOrderId", typeof(string));
                    odt1.Columns.Add("CustomerId", typeof(string));
                    odt1.Columns.Add("TableId", typeof(string));
                    odt1.Columns.Add("ItemId", typeof(string));
                    odt1.Columns.Add("TotalQty", typeof(float));
                    odt1.Columns.Add("Status", typeof(string));
                    odt1.Columns.Add("KOT_Status", typeof(int));
                    odt1.Columns.Add("Remark", typeof(string));
                    odt1.Columns.Add("CreatedBy", typeof(string));
                    odt1.Columns.Add("LastUpdatedBy", typeof(string));
                    if (salesList != null)
                        foreach (SalesList data in salesList)
                            odt.Rows.Add(data.SalesId,
                                data.CustomerId,
                                data.Category,
                                data.ItemId,
                                data.Measurement,
                                data.QuantityValue
                                , data.PricePerMeas
                                , data.TotalPrice
                                , data.Discount
                                , data.Weight
                                , DateTime.Today
                                , LastUpdatedBy
                                , LastUpdatedBy
                                , data.Status);
                    if (kitchenlist != null)
                    {
                        foreach (SalesList data in kitchenlist)
                            odt1.Rows.Add(data.SalesOrderId
                                , data.CustomerId
                                , data.tableId
                                , data.ItemId
                                , data.TotalQty
                                , data.Status
                                , data.KOT_Status
                                , data.Remark
                                , LastUpdatedBy
                                , LastUpdatedBy
                                );
                    }
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Coupen_CashDiscountTest]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 20).Value = saleId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 30).Value = customerId;
                    cmd.Parameters.Add("@CoupenId", SqlDbType.Int).Value = couponId;
                    cmd.Parameters.Add("@TotalDiscount", SqlDbType.Float).Value = discount;
                    cmd.Parameters.Add("@TotalAmount", SqlDbType.Float).Value = totalamount;
                    cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = LastUpdatedBy;
                    cmd.Parameters.Add("@SalesOrderList", SqlDbType.Structured).Value = odt;
                    cmd.Parameters.Add("@tblKitchenList", SqlDbType.Structured).Value = odt1;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public int InsertOldCoupenLog(List<SalesList> salesList, List<SalesList> kitchenlist, int couponId, string saleId, string customerId, string LastUpdatedBy)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    DataTable odt = new DataTable();
                    odt.Columns.Add("SalesId", typeof(string));
                    odt.Columns.Add("CustomerId", typeof(string));
                    odt.Columns.Add("Category", typeof(string));
                    odt.Columns.Add("ItemId", typeof(string));
                    odt.Columns.Add("Measurement", typeof(string));
                    odt.Columns.Add("QuantityValue", typeof(float));
                    odt.Columns.Add("PricePerMeas", typeof(float));
                    odt.Columns.Add("TotalPrice", typeof(float));
                    odt.Columns.Add("Discount", typeof(float));
                    odt.Columns.Add("weight", typeof(float));
                    odt.Columns.Add("DeliveryDate", typeof(DateTime));
                    odt.Columns.Add("CreatedBy", typeof(string));
                    odt.Columns.Add("LastUpdatedBy", typeof(string));
                    odt.Columns.Add("status", typeof(string));
                    DataTable odt1 = new DataTable();
                    odt1.Columns.Add("SalesOrderId", typeof(string));
                    odt1.Columns.Add("CustomerId", typeof(string));
                    odt1.Columns.Add("TableId", typeof(string));
                    odt1.Columns.Add("ItemId", typeof(string));
                    odt1.Columns.Add("TotalQty", typeof(float));
                    odt1.Columns.Add("Status", typeof(string));
                    odt1.Columns.Add("Remark", typeof(string));
                    odt1.Columns.Add("CreatedBy", typeof(string));
                    odt1.Columns.Add("LastUpdatedBy", typeof(string));
                    if (salesList != null)
                        foreach (SalesList data in salesList)
                            odt.Rows.Add(data.SalesId,
                                data.CustomerId,
                                data.Category,
                                data.ItemId,
                                data.Measurement,
                                data.QuantityValue
                                , data.PricePerMeas
                                , data.TotalPrice
                                , data.Discount
                                , data.Weight
                                , DateTime.Today
                                , LastUpdatedBy
                                , LastUpdatedBy
                                , data.Status);
                    if (kitchenlist != null)
                    {
                        foreach (SalesList data in kitchenlist)
                            odt1.Rows.Add(data.SalesOrderId
                                , data.CustomerId
                                , data.tableId
                                , data.ItemId
                                , data.TotalQty
                                , data.Status
                                , data.Remark
                                , LastUpdatedBy
                                , LastUpdatedBy
                                );
                    }
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[Coupen_UpdateSales_Couponlog]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 20).Value = saleId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 30).Value = customerId;
                    cmd.Parameters.Add("@CoupenId", SqlDbType.Int).Value = couponId;
                    cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = LastUpdatedBy;
                    cmd.Parameters.Add("@SalesOrderList", SqlDbType.Structured).Value = odt;
                    cmd.Parameters.Add("@tblKitchenList", SqlDbType.Structured).Value = odt1;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public async Task<List<SelectListItem>> GetCoupenList(string CustomerId, string Status,string hubId)
        {
            var coupenList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[GetCoupenList]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 50).Value = CustomerId == null ? "NA" : CustomerId;
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = "Active";
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 15).Value = hubId;
                con.Open();
                try
                {
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        while (rd.Read())
                        {
                            string coupenDiscount = Convert.ToString(rd["DiscountPercnt"]);
                            string coupenCode = Convert.ToString(rd["CoupenCode"]);
                            string Id = Convert.ToString(rd["Id"]);
                            string Maxdiscount = Convert.ToString(rd["MaxDiscount"]);
                            coupenList.Add(new SelectListItem
                            {
                                Text = coupenCode,
                                Value = coupenDiscount + "," + Id + "," + Maxdiscount
                            });
                        }

                    }
                }
                catch (Exception e)
                {

                }
            }
            return coupenList;
        }
        public void DeleteSalesOrder(string saleId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Sales_DeleteSalesOrder]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesId", SqlDbType.VarChar, 20).Value = saleId;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public int AddStockData(Item detail)
        {
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                sqlcon.Open();
                using (SqlCommand commandss = new SqlCommand())
                {
                    commandss.Connection = sqlcon;
                    commandss.CommandText = "[dbo].[Stock_InsertStock]";
                    //commandss.Parameters.Add("@Id", SqlDbType.Int).Value = 0;
                    commandss.Parameters.Add("@ItemId", SqlDbType.VarChar, 100).Value = Convert.ToString(detail.ItemId);
                    commandss.Parameters.Add("@Stock", SqlDbType.Int).Value = 0;
                    commandss.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 100).Value = detail.CreatedBy;
                    commandss.Parameters.Add("@Hub", SqlDbType.VarChar, 30).Value = detail.Branch;
                    commandss.Parameters.Add("@Measurement", SqlDbType.VarChar, 100).Value = detail.Measurement;
                    commandss.Parameters.Add("@ItemPrice", SqlDbType.Int).Value = 0;
                    commandss.CommandType = CommandType.StoredProcedure;
                    var StockId = commandss.ExecuteScalar();
                    sqlcon.Close();
                    sqlcon.Open();
                    using (SqlCommand cmmds = new SqlCommand())
                    {
                        cmmds.Connection = sqlcon;
                        cmmds.CommandText = "[dbo].[Wastage_CreateWastage]";
                        cmmds.Parameters.Add("@Id", SqlDbType.Int).Value = 0;
                        cmmds.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = Convert.ToString(detail.ItemId);
                        cmmds.Parameters.Add("@Wastage_Quan", SqlDbType.Float).Value = 0;
                        cmmds.Parameters.Add("@WastageType", SqlDbType.Int).Value = 0;
                        cmmds.Parameters.Add("@Wastage_Description", SqlDbType.VarChar, 200).Value = "NA";
                        cmmds.Parameters.Add("@WastageItemPrice", SqlDbType.Float).Value = 0;
                        cmmds.Parameters.Add("@Hub", SqlDbType.VarChar, 30).Value = detail.Branch;
                        cmmds.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = detail.CreatedBy;
                        // cmmds.Parameters.Add("@PurchaseId", SqlDbType.VarChar, 20).Value = "0";
                        cmmds.CommandType = CommandType.StoredProcedure;
                        cmmds.ExecuteNonQuery();
                        sqlcon.Close();
                    }
                    return Convert.ToInt32(StockId);

                }
            }

        }
        public List<Sales> GetTodaySalesToPrint(Sales salesdata)
        {
            List<Sales> getTodaySalesToPrint = new List<Sales>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetTodaySalesToPrint]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TodayDate", SqlDbType.VarChar, 100).Value = salesdata.TodayDate;
                cmd.Parameters.Add("@StartDate", SqlDbType.VarChar, 100).Value = salesdata.StartDate;
                cmd.Parameters.Add("@EndDate", SqlDbType.VarChar, 100).Value = salesdata.EndDate;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        getTodaySalesToPrint.Add(new Sales
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            SalesOrderId = Convert.ToString(rd["SalesOrderId"]),
                            PaymentMode = Convert.ToString(rd["PaymentMode"] == DBNull.Value ? "Not Found" : Convert.ToString(rd["PaymentMode"])),
                            TotalPrice = Convert.ToDouble(rd["TotalPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["TotalPrice"])),
                            PaymentStatus = Convert.ToString(rd["PaymentStatus"] == DBNull.Value ? "Not Found" : Convert.ToString(rd["PaymentStatus"])),
                            Name = Convert.ToString(rd["Name"]),
                            tableName = Convert.ToString(rd["tableName"] == DBNull.Value ? "Not Found" : Convert.ToString(rd["tableName"])),
                            OrderType = Convert.ToString(rd["OrderType"] == DBNull.Value ? "Not Found" : Convert.ToString(rd["OrderType"])),
                        });
                    }
                    return getTodaySalesToPrint;
                }
            }

        }
        public async Task<Sales> GetTodaySalesDetail(Sales salesdata)
        {
            Sales saleslist = null;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[GetTodaySalesDetail]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TodayDate", SqlDbType.VarChar, 100).Value = salesdata.TodayDate;
                cmd.Parameters.Add("@StartDate", SqlDbType.VarChar, 100).Value = salesdata.StartDate;
                cmd.Parameters.Add("@EndDate", SqlDbType.VarChar, 100).Value = salesdata.EndDate;
                con.Open();
                try
                {
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        if (rd.Read())
                        {
                            saleslist = new Sales
                            {
                                TotalCount = rd["TotalCount"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["TotalCount"]),
                                TotalAmt = rd["TotalAmount"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["TotalAmount"]),
                                AmountCollected = rd["AmountCollected"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["AmountCollected"]),
                                AmountCancelled = rd["AmountCancelled"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["AmountCancelled"]),
                                CashPaymentCollect = rd["CashPaymentCollect"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["CashPaymentCollect"]),
                                CardPaymentCollect = rd["CardPaymentCollect"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["CardPaymentCollect"]),
                                UPIPaymentCollect = rd["UPIPaymentCollect"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["UPIPaymentCollect"])

                            };
                        }
                        return saleslist;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }

        }
        public List<Sales> GetSalesOrderListExcel(Sales salesdata)
        {
            List<Sales> salesOrderList = new List<Sales>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[GetSalesOrderListExcel]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TodayDate", SqlDbType.VarChar, 100).Value = salesdata.TodayDate;
                        cmd.Parameters.Add("@StartDate", SqlDbType.VarChar, 100).Value = salesdata.StartDate;
                        cmd.Parameters.Add("@EndDate", SqlDbType.VarChar, 100).Value = salesdata.EndDate;

                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                salesOrderList.Add(new Sales
                                {
                                    Id = Convert.ToInt32(rd["Id"]),
                                    SalesOrderId = rd["SalesOrderId"] == DBNull.Value ? null : rd["SalesOrderId"] as string,
                                    CustomerId = rd["CustomerId"] == DBNull.Value ? null : rd["CustomerId"] as string,
                                    Name = rd["Name"] == DBNull.Value ? null : rd["Name"] as string,
                                    ContactNo = rd["ContactNo"] == DBNull.Value ? null : rd["ContactNo"] as string,
                                    SalesPerson = rd["SalesPerson"] == DBNull.Value ? null : rd["SalesPerson"] as string,
                                    PluCount = Convert.ToInt32(rd["PLU_Count"] == DBNull.Value ? 0 : rd["PLU_Count"]),
                                    TotalQuantity = Convert.ToDouble(rd["TotalQuantity"] == DBNull.Value ? null : rd["TotalQuantity"]),
                                    TotalPrice = Convert.ToDouble(rd["TotalPrice"] == DBNull.Value ? null : rd["TotalPrice"]),
                                    Discount = Convert.ToDouble(rd["Discount"] == DBNull.Value ? null : rd["Discount"]),
                                    OrderdStatus = rd["OrderdStatus"] == DBNull.Value ? null : rd["OrderdStatus"] as string,
                                    PaymentStatus = rd["PaymentStatus"] == DBNull.Value ? null : rd["PaymentStatus"] as string,
                                    DeliveryDate1 = Convert.ToDateTime(rd["DeliveryDate"] == DBNull.Value ? null : rd["DeliveryDate"]),
                                    CreatedBy = rd["CreatedBy"] == DBNull.Value ? null : rd["CreatedBy"] as string,
                                    CreatedOn1 = Convert.ToString(rd["CreatedOn"] == DBNull.Value ? null : rd["CreatedOn"]),
                                    Bags = rd["Bags"] == DBNull.Value ? null : rd["Bags"] as string,
                                    PaymentMode = rd["PaymentMode"] == DBNull.Value ? null : rd["PaymentMode"] as string,
                                    SlotId = rd["SlotId"] == DBNull.Value ? null : rd["SlotId"] as string,
                                    DeliveryType = rd["DeliveryType"] == DBNull.Value ? null : rd["DeliveryType"] as string,
                                    DeliveryCharges1 = Convert.ToDouble(rd["DeliveryCharges"] == DBNull.Value ? null : rd["DeliveryCharges"]),
                                    HubId = rd["HubId"] == DBNull.Value ? null : rd["HubId"] as string,
                                    Branch = rd["Branch"] == DBNull.Value ? null : rd["Branch"] as string,
                                    Source = rd["Source"] == DBNull.Value ? null : rd["Source"] as string,
                                    CreatedName = rd["CreatedName"] == DBNull.Value ? null : rd["CreatedName"] as string,
                                    UpdatedPersonName = rd["UpdatedPersonName"] == DBNull.Value ? null : rd["UpdatedPersonName"] as string,
                                    Coupencode = rd["Coupencode"] == DBNull.Value ? null : Convert.ToString(rd["Coupencode"]),
                                    TransactionId = rd["TransactionId"] == DBNull.Value ? null : rd["TransactionId"] as string,
                                    Wallet = Convert.ToDouble(rd["Wallet"] == DBNull.Value ? null : rd["Wallet"]),

                                });
                            }
                            return salesOrderList;
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }

        }
        public async Task<List<SelectListItem>> GetAllOrderStatus()
        {
            List<SelectListItem> OrderStatus = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Sales_GetOrderStatus]";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (rd.Read())
                    {

                        OrderStatus.Add(new SelectListItem
                        {
                            Text = Convert.ToString(rd["Status"]),
                            Value = Convert.ToString(rd["Status"]),
                        });
                    }
                    return OrderStatus;
                }
            }
        }
        public async Task<byte[]> ExportExcelofSales(string webRootPath, Sales salesdata)
        {

            string fileName = Path.Combine(webRootPath, "SalesOverview.xlsx");
            FileInfo newFile = new FileInfo(fileName);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(fileName);
            }
            Stream stream = new System.IO.MemoryStream();
            using (var package = new ExcelPackage(newFile))
            {

                var worksheet1 = package.Workbook.Worksheets.Add("SalesOverview");
                //IQueryable<Lead> leadList = null;
                var overviewList = GetSalesOrderListExcel(salesdata);
                var ovrviewDetail = await GetTodaySalesDetail(salesdata);

                int rowCount = 2;



                #region ExcelSalesDetail
                worksheet1.Cells[1, 1].Value = "TotalCount";
                worksheet1.Cells[1, 1].Style.Font.Bold = true;
                worksheet1.Cells[1, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(1).AutoFit();

                worksheet1.Cells[1, 2].Value = "Total Amount";
                worksheet1.Cells[1, 2].Style.Font.Bold = true;
                worksheet1.Cells[1, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(2).AutoFit();


                worksheet1.Cells[1, 3].Value = "Amount To Be Collected";
                worksheet1.Cells[1, 3].Style.Font.Bold = true;
                worksheet1.Cells[1, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(3).AutoFit();


                worksheet1.Cells[1, 4].Value = "Cancelled Order Amount";
                worksheet1.Cells[1, 4].Style.Font.Bold = true;
                worksheet1.Cells[1, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(4).AutoFit();


                worksheet1.Cells[1, 5].Value = "Total Turnover";
                worksheet1.Cells[1, 5].Style.Font.Bold = true;
                worksheet1.Cells[1, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(5).AutoFit();
                #endregion

                var TurnOver = ovrviewDetail.TotalAmt - ovrviewDetail.AmountCancelled;
                rowCount++;
                worksheet1.Cells[rowCount, 1].Value = ovrviewDetail.TotalCount;
                worksheet1.Cells[rowCount, 2].Value = ovrviewDetail.TotalAmt;
                worksheet1.Cells[rowCount, 3].Value = ovrviewDetail.AmountCollected;
                worksheet1.Cells[rowCount, 4].Value = ovrviewDetail.AmountCancelled;
                worksheet1.Cells[rowCount, 5].Value = TurnOver;



                var rowdetail = rowCount + 2;

                #region ExcelHeader
                worksheet1.Cells[rowdetail, 1].Value = "SalesOrderId";
                worksheet1.Cells[rowdetail, 1].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(1).AutoFit();

                worksheet1.Cells[rowdetail, 2].Value = "Cutomer Name";
                worksheet1.Cells[rowdetail, 2].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(2).AutoFit();

                worksheet1.Cells[rowdetail, 3].Value = "ContactNo";
                worksheet1.Cells[rowdetail, 3].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(3).AutoFit();

                worksheet1.Cells[rowdetail, 4].Value = "PluCount";
                worksheet1.Cells[rowdetail, 4].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(4).AutoFit();


                worksheet1.Cells[rowdetail, 5].Value = "TotalPrice";
                worksheet1.Cells[rowdetail, 5].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(5).AutoFit();



                worksheet1.Cells[rowdetail, 6].Value = "Discount";
                worksheet1.Cells[rowdetail, 6].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(6).AutoFit();


                worksheet1.Cells[rowdetail, 7].Value = "OrderdStatus";
                worksheet1.Cells[rowdetail, 7].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(7).AutoFit();


                worksheet1.Cells[rowdetail, 8].Value = "PaymentStatus";
                worksheet1.Cells[rowdetail, 8].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(8).AutoFit();

                worksheet1.Cells[rowdetail, 9].Value = "PaymentMode";
                worksheet1.Cells[rowdetail, 9].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 9].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(9).AutoFit();


                worksheet1.Cells[rowdetail, 10].Value = "DeliveryDate";
                worksheet1.Cells[rowdetail, 10].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 10].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(10).AutoFit();


                worksheet1.Cells[rowdetail, 11].Value = "DeliveryCharges";
                worksheet1.Cells[rowdetail, 11].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 11].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(11).AutoFit();


                worksheet1.Cells[rowdetail, 12].Value = "Source";
                worksheet1.Cells[rowdetail, 12].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 12].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(12).AutoFit();




                worksheet1.Cells[rowdetail, 13].Value = "CreatedBy";
                worksheet1.Cells[rowdetail, 13].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 13].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(13).AutoFit();

                worksheet1.Cells[rowdetail, 14].Value = "CreatedOn";
                worksheet1.Cells[rowdetail, 14].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 14].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(14).AutoFit();


                worksheet1.Cells[rowdetail, 15].Value = "LastUpdatedBy";
                worksheet1.Cells[rowdetail, 15].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 15].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(15).AutoFit();

                worksheet1.Cells[rowdetail, 16].Value = "LastUpdatedOn";
                worksheet1.Cells[rowdetail, 16].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 16].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(16).AutoFit();

                worksheet1.Cells[rowdetail, 17].Value = "Coupen Code";
                worksheet1.Cells[rowdetail, 17].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 17].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(17).AutoFit();

                worksheet1.Cells[rowdetail, 18].Value = "Transaction";
                worksheet1.Cells[rowdetail, 18].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 18].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(18).AutoFit();

                worksheet1.Cells[rowdetail, 19].Value = "Wallet Used";
                worksheet1.Cells[rowdetail, 19].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 19].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(18).AutoFit();


                #endregion

                rowdetail++;
                foreach (var Salesdetail in overviewList)
                {
                    worksheet1.Cells[rowdetail, 1].Value = Salesdetail.SalesOrderId;
                    worksheet1.Cells[rowdetail, 2].Value = Salesdetail.Name;
                    worksheet1.Cells[rowdetail, 3].Value = Salesdetail.ContactNo;
                    worksheet1.Cells[rowdetail, 4].Value = Salesdetail.PluCount;
                    worksheet1.Cells[rowdetail, 5].Value = "Rs. " + Salesdetail.TotalPrice;
                    worksheet1.Cells[rowdetail, 6].Value = "Rs. " + Salesdetail.Discount;
                    worksheet1.Cells[rowdetail, 7].Value = Salesdetail.OrderdStatus;
                    worksheet1.Cells[rowdetail, 8].Value = Salesdetail.PaymentStatus;
                    worksheet1.Cells[rowdetail, 9].Value = Salesdetail.PaymentMode;
                    worksheet1.Cells[rowdetail, 10].Value = Salesdetail.DeliveryDate;
                    worksheet1.Cells[rowdetail, 11].Value = Salesdetail.DeliveryCharges1;
                    worksheet1.Cells[rowdetail, 12].Value = Salesdetail.Source;
                    worksheet1.Cells[rowdetail, 13].Value = Salesdetail.CreatedName;
                    worksheet1.Cells[rowdetail, 14].Value = Salesdetail.CreatedOn1;
                    worksheet1.Cells[rowdetail, 15].Value = Salesdetail.UpdatedPersonName;
                    worksheet1.Cells[rowdetail, 16].Value = Salesdetail.LastUpdatedOn;
                    worksheet1.Cells[rowdetail, 17].Value = Salesdetail.Coupencode;
                    worksheet1.Cells[rowdetail, 18].Value = Salesdetail.TransactionId == "NA" ? "Offline" : "Online";
                    worksheet1.Cells[rowdetail, 19].Value = "Rs. " + Salesdetail.Wallet;



                    rowdetail++;
                }



                package.SaveAs(newFile);
                byte[] files = File.ReadAllBytes(fileName);
                File.Delete(fileName);
                return files;
            }

        }
        public SalesDetail GetSalesDetail(string Id,string hubId)
        {
            SalesDetail SalesDetail = new SalesDetail();
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[GetSalesData]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 100).Value = Id;
                    cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 100).Value = hubId;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            SalesDetail = new SalesDetail
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                SalesOrderId = Convert.ToString(rd["SalesOrderId"] == DBNull.Value ? "SO000" : rd["SalesOrderId"]),
                                Source = rd["Source"] == DBNull.Value ? "Web" : Convert.ToString(rd["Source"]),
                                OrderdStatus = Convert.ToString(rd["OrderdStatus"] == DBNull.Value ? "NA" : rd["OrderdStatus"]),
                                TransactionId = Convert.ToString(rd["TransactionId"] == DBNull.Value ? "TI0" : rd["TransactionId"]),
                                PaymentStatus = Convert.ToString(rd["PaymentStatus"] == DBNull.Value ? "NA" : rd["PaymentStatus"]),
                                PaymentMode = Convert.ToString(rd["PaymentMode"] == DBNull.Value ? "NA" : rd["PaymentMode"]),
                                Coupencode = rd["CoupenApplied"] == DBNull.Value ? "NA" : Convert.ToString(rd["CoupenApplied"]),
                                DeliveryDate = Convert.ToDateTime(rd["DeliveryDate"] == DBNull.Value ? "" : rd["DeliveryDate"]),
                                SlotId = rd["Slot"] == DBNull.Value ? "NA" : Convert.ToString(rd["Slot"]),
                                TotalPrice = Convert.ToDouble(rd["TotalPrice"] == DBNull.Value ? 0.00 : rd["TotalPrice"]),
                                Discount = Convert.ToDouble(rd["Discount"] == DBNull.Value ? 0.00 : rd["Discount"]),
                                DeliveryCharges = Convert.ToString(rd["DeliveryCharges"] == DBNull.Value ? "NA" : rd["DeliveryCharges"]),
                                TotalAmount = Convert.ToDouble(rd["TotalAmount"] == DBNull.Value ? 0.00 : rd["TotalAmount"]),
                                CustomerName = Convert.ToString(rd["CustomerName"] == DBNull.Value ? "Guest" : rd["CustomerName"]),
                                Name = Convert.ToString(rd["Name"] == DBNull.Value ? "Guest" : rd["Name"]),
                                EmailId = Convert.ToString(rd["EmailId"] == DBNull.Value ? "-" : rd["EmailId"]),
                                CustomerNo = Convert.ToString(rd["CustomerNumber"] == DBNull.Value ? "000000000" : rd["CustomerNumber"]),
                                PhoneNumber = Convert.ToString(rd["ContactNo"] == DBNull.Value ? "XXXXXXX" : rd["ContactNo"]),
                                PLU_Count = Convert.ToDouble(rd["PLU_Count"] == DBNull.Value ? 0.00 : rd["PLU_Count"]),
                                TotalQuantity = Convert.ToDouble(rd["TotalQuantity"] == DBNull.Value ? 0.00 : rd["TotalQuantity"]),
                                TotalWeight = Convert.ToDouble(rd["TotalWeight"] == DBNull.Value ? 0.00 : rd["TotalWeight"]),
                                BuildingName = Convert.ToString(rd["BuildingName"] == DBNull.Value ? "NA" : rd["BuildingName"]),
                                RoomNo = Convert.ToString(rd["RoomNo"] == DBNull.Value ? "000" : rd["RoomNo"]),
                                Sector = Convert.ToString(rd["Sector"] == DBNull.Value ? "01" : rd["Sector"]),
                                Locality = Convert.ToString(rd["Locality"] == DBNull.Value ? "AN" : rd["Locality"]),
                                Landmark = Convert.ToString(rd["Landmark"] == DBNull.Value ? "NA" : rd["Landmark"]),
                                ZipCode = Convert.ToString(rd["ZipCode"] == DBNull.Value ? "40000" : rd["ZipCode"]),
                                City = Convert.ToString(rd["City"] == DBNull.Value ? "Navi Mumbai" : rd["City"]),
                                state = Convert.ToString(rd["state"] == DBNull.Value ? "Maharashtra" : rd["state"]),
                                Country = Convert.ToString(rd["Country"] == DBNull.Value ? "India" : rd["Country"]),
                                DeliveryPerson = rd["DeliveryPerson"] == DBNull.Value ? "NA" : Convert.ToString(rd["DeliveryPerson"]),
                                CustomerId = rd["CustomerId"] == DBNull.Value ? "CI00" : Convert.ToString(rd["CustomerId"]),
                                CreatedBy = Convert.ToString(rd["CreatedBy"] == DBNull.Value ? "EMPID05" : rd["CreatedBy"]),
                                DeliveryNotes = rd["DeliveryNotes"] == DBNull.Value ? "" : Convert.ToString(rd["DeliveryNotes"]),
                                DiscountPer = rd["DiscountPer"] == DBNull.Value ? "0" : Convert.ToString(rd["DiscountPer"]),
                                DiscountApplied = rd["DiscountApplied"] == DBNull.Value ? "0" : Convert.ToString(rd["DiscountApplied"]),
                                CoupenId = Convert.ToInt32(rd["CoupenId"] == DBNull.Value ? 0 : rd["CoupenId"]),
                                Hub = Convert.ToString(rd["Hub"] == DBNull.Value ? "Arabian" : rd["Hub"]),
                                WalletAmount = Convert.ToDouble(rd["Wallet"] == DBNull.Value ? 0.00 : rd["Wallet"]),
                                MaxDiscount = Convert.ToDouble(rd["MaxDiscount"] == DBNull.Value ? 0 : rd["MaxDiscount"]),
                                ComputedCoupen = rd["DiscountPer"] == DBNull.Value ? "0" : Convert.ToString(rd["DiscountPer"]) + "," + Convert.ToInt32(rd["CoupenId"] == DBNull.Value ? 0 : rd["CoupenId"]) + "," + Convert.ToDouble(rd["MaxDiscount"] == DBNull.Value ? 0 : rd["MaxDiscount"]),
                                OrderedOn = Convert.ToDateTime(rd["CreatedOn"] == DBNull.Value ? "" : rd["CreatedOn"]),
                                //tableId = Convert.ToString(rd["tableId"] == DBNull.Value ? "T0" : rd["tableId"]),
                                //tableName = Convert.ToString(rd["tableName"] == DBNull.Value ? "TO" : rd["tableName"]),
                                userRole = Convert.ToString(rd["UserRole"] == DBNull.Value ? "NOT AVAILABLE" : rd["UserRole"]),
                                CustomerOnlyId = rd["CustomerOnlyId"] == DBNull.Value ? 0 : Convert.ToInt32(rd["CustomerOnlyId"]),
                                OrderType = Convert.ToString(rd["OrderType"] == DBNull.Value ? "Not Available" : Convert.ToString(rd["OrderType"])),
                                taxType = Convert.ToInt32(rd["taxType"] == DBNull.Value ? 0 : Convert.ToInt32(rd["taxType"])),
                                taxBillType = Convert.ToInt32(rd["taxBillType"] == DBNull.Value ? 0 : Convert.ToInt32(rd["taxBillType"])),
                                vatTax = Convert.ToSingle(rd["vatTax"] == DBNull.Value ? 0 : Convert.ToSingle(rd["vatTax"])),
                                gstTax = Convert.ToSingle(rd["gstTax"] == DBNull.Value ? 0 : Convert.ToSingle(rd["gstTax"])),
                                AWBNumber = Convert.ToString(rd["AWBNumber"] == DBNull.Value ? "NA" : Convert.ToString(rd["AWBNumber"])),
                                AWBShipLink = Convert.ToString(rd["AWBShipLink"] == DBNull.Value ? "NA" : Convert.ToString(rd["AWBShipLink"])),
                                TaxableAmount = Convert.ToSingle(rd["TaxableAmount"] == DBNull.Value ? 0 : Convert.ToSingle(rd["TaxableAmount"])),
                                
                            };
                        }
                        return SalesDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                return SalesDetail;
            }

        }
        public List<SalesList> GetSalesProductListData(string Id,string hubId)
        {
            List<SalesList> SalesProductListData = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_GetSalesProductDetailTest]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesId", SqlDbType.VarChar, 50).Value = Id;
              
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            SalesProductListData.Add(new SalesList
                            {
                                Id = Convert.ToInt32(rd["Id"] == DBNull.Value ? 0 : rd["Id"]),
                                SalesId = Convert.ToString(rd["SalesId"] == DBNull.Value ? "SO00" : rd["SalesId"]),
                                SalesListId = Convert.ToString(rd["SalesListId"] == DBNull.Value ? "SALE00" : rd["SalesListId"]),
                                CustomerId = Convert.ToString(rd["CustomerId"] == DBNull.Value ? "CI00" : rd["CustomerId"]),
                                KitchListId = Convert.ToString(rd["KitchListId"] == DBNull.Value ? "KIT00" : rd["KitchListId"]),
                                tableId = Convert.ToString(rd["TableId"] == DBNull.Value ? "T0" : rd["TableId"]),
                                ItemId = Convert.ToString(rd["ItemId"] == DBNull.Value ? "ITM00" : rd["ItemId"]),
                                PriceId = Convert.ToString(rd["PriceId"] == DBNull.Value ? "PRID00" : rd["PriceId"]),
                                Item_Id = Convert.ToInt32(rd["Item_Id"] == DBNull.Value ? 0 : rd["Item_Id"]),
                                Price_Id = Convert.ToInt32(rd["Price_Id"] == DBNull.Value ? 0 : rd["Price_Id"]),
                                PluName = Convert.ToString(rd["PluName"] == DBNull.Value ? "Not Found" : rd["PluName"]),
                                Category = Convert.ToString(rd["Category"] == DBNull.Value ? "Not Found" : rd["Category"]),
                                CName = Convert.ToString(rd["CategoryName"] == DBNull.Value ? "Not Found" : rd["CategoryName"]),
                                Measurement = Convert.ToString(rd["Measurement"] == DBNull.Value ? "Unit" : rd["Measurement"]),
                                Size = Convert.ToString(rd["Size"] == DBNull.Value ? "NA" : rd["Size"]),
                                ItmNetWeight = Convert.ToSingle(rd["ItmNetWeight"] == DBNull.Value ? 0 : rd["ItmNetWeight"]),
                                OrderQty = Convert.ToSingle(rd["OrderQty"] == DBNull.Value ? 0 : rd["OrderQty"]),
                                weight = Convert.ToSingle(rd["weight"] == DBNull.Value ? 0 : rd["weight"]),
                                ItemWeight = Convert.ToString(rd["ItemWeight"] == DBNull.Value ? "" : rd["ItemWeight"]),
                                ItemType = Convert.ToString(rd["ItemType"] == DBNull.Value ? "Not Found" : rd["ItemType"]),
                                Coupen_Disc = Convert.ToInt32(rd["Coupen_Disc"] == DBNull.Value ? 0 : rd["Coupen_Disc"]),
                                QuantityValue = Convert.ToSingle(rd["QuantityValue"] == DBNull.Value ? 0 : rd["QuantityValue"]),
                                PricePerMeas = Convert.ToDouble(rd["PricePerMeas"] == DBNull.Value ? 0 : rd["PricePerMeas"]),
                                TotalPrice = Convert.ToDouble(rd["TotalPrice"] == DBNull.Value ? 0 : rd["TotalPrice"]),
                                Discount = Convert.ToDouble(rd["Discount"] == DBNull.Value ? 0 : rd["Discount"]),
                                DeliveryDate = Convert.ToDateTime(rd["DeliveryDate"] == DBNull.Value ? "" : rd["DeliveryDate"]),
                                status = Convert.ToString(rd["Status"] == DBNull.Value ? "Pending" : rd["Status"]),
                                Item_Cost = Convert.ToDouble(rd["Item_Cost"] == DBNull.Value ? 0 : rd["Item_Cost"]),
                                AddedDiscount = Convert.ToDouble(rd["AddedDiscount"] == DBNull.Value ? 0 : rd["AddedDiscount"]),
                                Remark = Convert.ToString(rd["Remark"] == DBNull.Value ? "NA" : rd["Remark"]),
                                Barcode = Convert.ToString(rd["Barcode"] == DBNull.Value ? "NA" : rd["Barcode"]),
                                KOT_Status = Convert.ToInt32(rd["KOT_Status"] == DBNull.Value ? 0 : rd["KOT_Status"]) // 16-12-2021
                            });
                        }
                        return SalesProductListData;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public int SalesList_InsertProductList(SalesList ProductList)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_InsertSalesList]", con))
            {
                con.Open();
                DataTable odt = new DataTable();
                odt.Columns.Add("SalesId", typeof(string));
                odt.Columns.Add("CustomerId", typeof(string));
                odt.Columns.Add("Category", typeof(string));
                odt.Columns.Add("ItemId", typeof(string));
                odt.Columns.Add("Measurement", typeof(string));
                odt.Columns.Add("QuantityValue", typeof(float));
                odt.Columns.Add("PricePerMeas", typeof(float));
                odt.Columns.Add("TotalPrice", typeof(float));
                odt.Columns.Add("Discount", typeof(float));
                odt.Columns.Add("weight", typeof(float));
                odt.Columns.Add("DeliveryDate", typeof(DateTime));
                odt.Columns.Add("CreatedBy", typeof(string));
                odt.Columns.Add("LastUpdatedBy", typeof(string));
                odt.Columns.Add("status", typeof(string));

                if (ProductList != null)
                    for (var i = 0; i < ProductList.Measurements.Count; i++)
                        odt.Rows.Add(ProductList.SalesId,
                            ProductList.CustomerId,
                            ProductList.Categorys[i],
                            ProductList.ItemIds[i],
                            ProductList.Measurements[i],
                            ProductList.QuantityValues[i]
                            , ProductList.PricePerMeasList[i]
                            , ProductList.TotalPriceList[i]
                            , ProductList.DiscountList[i]
                            , ProductList.weightList[i]
                            , ProductList.ProductDeliveryDates
                            , ProductList.CreatedBy
                            , ProductList.LastUpdatedBy
                            , ProductList.statusList[i]);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesId", SqlDbType.VarChar, 100).Value = ProductList.SalesId;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = ProductList.LastUpdatedBy;
                cmd.Parameters.Add("@tblsalesList", SqlDbType.Structured).Value = odt;
                cmd.Parameters.Add("@CoupenId", SqlDbType.Int).Value = ProductList.CoupenId;
                cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 30).Value = ProductList.CustomerId;
                cmd.Parameters.Add("@TotalPrice", SqlDbType.Float).Value = ProductList.TotalsalesOrderCost;
                cmd.Parameters.Add("@Discount", SqlDbType.Float).Value = ProductList.Discount;
                cmd.Parameters.Add("@OrderStatus", SqlDbType.VarChar, 30).Value = ProductList.OrderStatus;
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public string SalesList_CreateSalesOrderList(Item ProductList)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_CreateSalesOrderListNewArabian]", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesId", SqlDbType.VarChar, 100).Value = ProductList.SalesId;
                cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 50).Value = ProductList.CustomerId;
                cmd.Parameters.Add("@Category", SqlDbType.VarChar, 30).Value = ProductList.Category;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 30).Value = ProductList.ItemId;
                cmd.Parameters.Add("@PriceId", SqlDbType.VarChar, 30).Value = ProductList.PriceId;
                cmd.Parameters.Add("@Measurement", SqlDbType.VarChar, 30).Value = ProductList.Measurement;
                cmd.Parameters.Add("@QuantityValue", SqlDbType.Float).Value = ProductList.Quantity;
                cmd.Parameters.Add("@PricePerMeas", SqlDbType.Float).Value = ProductList.SellingPrice;
                cmd.Parameters.Add("@TotalPrice", SqlDbType.Float).Value = ProductList.ActualCost;
                cmd.Parameters.Add("@Discount", SqlDbType.Float).Value = ProductList.Discount;
                cmd.Parameters.Add("@Weight", SqlDbType.Float).Value = ProductList.Weight;
                cmd.Parameters.Add("@DeliveryDate", SqlDbType.DateTime).Value = ProductList.DeliveryDate;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100).Value = ProductList.CreatedBy;
                cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = ProductList.CreatedOn;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = ProductList.LastUpdatedBy;
                cmd.Parameters.Add("@LastUpdatedOn", SqlDbType.DateTime).Value = ProductList.LastUpdatedOn;
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = ProductList.ItemStatus;
                cmd.Parameters.Add("@TaxableAmount", SqlDbType.Float).Value = ProductList.ActualCost;
                cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 100).Value = ProductList.Remark;
                //cmd.Parameters.Add("@TableId", SqlDbType.VarChar, 50).Value = ProductList.TableId;
                return Convert.ToString(cmd.ExecuteScalar());
            }
        }
        public int DeleteSalesList(string SalesListId, string SalesId)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_DeleteSalesList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesListId", SqlDbType.VarChar, 100).Value = SalesListId;
                cmd.Parameters.Add("@SalesId", SqlDbType.VarChar, 100).Value = SalesId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

        }
        public int RemoveSalesList(string SalesListId, string LastUpdatedBy, string SalesId)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_RemoveSalesOrderList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesListId", SqlDbType.VarChar, 100).Value = SalesListId;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = LastUpdatedBy;
                cmd.Parameters.Add("@SalesId", SqlDbType.VarChar, 8).Value = SalesId;

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

        }
        public List<SelectListItem> Employee_NameSL(string UserRole)
        {
            List<SelectListItem> employeeName = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Employee_FullNameSL]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 20).Value = UserRole;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        employeeName.Add(new SelectListItem
                        {
                            Text = Convert.ToString(rd["FullName"]),
                            Value = Convert.ToString(rd["EmpId"]),
                        });
                    }
                    return employeeName;
                }
            }
        }
        public int AssignProductForDelivery(Sales SalesOrderData)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_AssignDeliverytoEmp]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesPerson", SqlDbType.VarChar, 100).Value = SalesOrderData.SalesPerson;
                cmd.Parameters.Add("@OrderdStatus", SqlDbType.VarChar, 100).Value = SalesOrderData.OrderdStatus;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = SalesOrderData.LastUpdatedBy;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 100).Value = SalesOrderData.SalesOrderId;
                cmd.Parameters.Add("@DeliveryNotes", SqlDbType.VarChar).Value = SalesOrderData.DeliveryNotes;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

        }
        public int UpdateDeliveryDate(Sales SalesOrderData)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_UpdateDeliveryDate]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DeliveryDate", SqlDbType.VarChar, 100).Value = SalesOrderData.DeliveryDate;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = SalesOrderData.LastUpdatedBy;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 100).Value = SalesOrderData.SalesOrderId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

        }
        public int UpdateDeliveryCharge(Sales SalesOrderData)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_UpdateDeliveryCharge]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DeliveryCharges", SqlDbType.VarChar, 100).Value = SalesOrderData.DeliveryCharges;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = SalesOrderData.LastUpdatedBy;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 100).Value = SalesOrderData.SalesOrderId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int UpdateOrderdStatus(SalesList salesDetailList)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_UpdateOrderdStatus]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OrderdStatus", SqlDbType.VarChar, 100).Value = salesDetailList.OrderStatus;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = salesDetailList.LastUpdatedBy;
                cmd.Parameters.Add("@AWBNumber", SqlDbType.VarChar, 100).Value = salesDetailList.AWBNumber;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 100).Value = salesDetailList.SalesId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public SalesDetail GetSaleProductStatus(string SalesOrderId)
        {
            SalesDetail SalesDetail = new SalesDetail();

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_GetSaleProductStatus]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 100).Value = SalesOrderId;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        SalesDetail = new SalesDetail
                        {
                            OrderStatusDate = Convert.ToDateTime(rd["OrderStatus"] == DBNull.Value ? null : rd["OrderStatus"]),
                            ConfirmedStatusDate = Convert.ToDateTime(rd["ConfirmedStatus"] == DBNull.Value ? null : rd["ConfirmedStatus"]),
                            PackedStatusDate = Convert.ToDateTime(rd["PackedStatus"] == DBNull.Value ? null : rd["PackedStatus"]),
                            ConfirmedStatusPersonName = rd["ConfirmedStatusPersonName"] == DBNull.Value ? null : Convert.ToString(rd["ConfirmedStatusPersonName"]),
                            PackedStatusPersonName = rd["PackedStatusPersonName"] == DBNull.Value ? null : Convert.ToString(rd["PackedStatusPersonName"]),
                        };
                    }
                    return SalesDetail;
                }
            }
        }
        public Item GetItemDetailbyPlucode(string PluCode)
        {
            var itemDetail = new Item();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                //cmd.CommandText = "[dbo].[Item_GetItembyPlucode]";
                cmd.CommandText = "[dbo].[ItemVariant_GetItembyPlucode]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PluCode", SqlDbType.VarChar).Value = PluCode;

                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        #region MyRegion
                        //itemDetail.ItemId = Convert.ToString(rd[0]);
                        //itemDetail.PluName = Convert.ToString(rd[1]);
                        //itemDetail.Measurement = Convert.ToString(rd[2]);
                        //itemDetail.Weight = Convert.ToDouble(rd[3]);
                        //itemDetail.NetWeight = itemDetail.Weight;
                        //itemDetail.Purchaseprice = Convert.ToDouble(rd[4]);
                        //itemDetail.SellingPrice = Convert.ToDouble(rd[5]);
                        //itemDetail.ItemType = Convert.ToString(rd[6]);
                        //itemDetail.CategoryName = Convert.ToString(rd[7]);
                        //itemDetail.Category = Convert.ToString(rd[8]);
                        //itemDetail.MarketPrice = Convert.ToDouble(rd[9]);
                        //itemDetail.TotalStock = rd["itemstock"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["itemstock"]);
                        //itemDetail.stockId = Convert.ToInt32(rd["StockId"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["StockId"]));
                        //itemDetail.DiscountPerctg = rd["DiscountedPrice"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["DiscountedPrice"]);
                        //itemDetail.PluCode = Convert.ToString(rd["PluCode"]);
                        //itemDetail.Quantity = 1;
                        //itemDetail.itemstock = Convert.ToString(itemDetail.TotalStock);
                        #endregion
                        itemDetail.Id = Convert.ToInt32(rd["Item_Id"]);
                        itemDetail.PId = Convert.ToInt32(rd["Price_Id"]);
                        itemDetail.ItemId = Convert.ToString(rd["ItemId"]);
                        itemDetail.PriceId = Convert.ToString(rd["PriceId"]);
                        itemDetail.PluName = Convert.ToString(rd["PluName"]);
                        itemDetail.Measurement = Convert.ToString(rd["Measurement"]);
                        itemDetail.OrderQty = Convert.ToSingle(rd["OrderQty"]);
                        itemDetail.ItmNetWeight = Convert.ToSingle(rd["ItmNetWeight"]);
                        itemDetail.Purchaseprice = Convert.ToDouble(rd["Purchaseprice"]);
                        itemDetail.SellingPrice = Convert.ToDouble(rd["SellingPrice"]);
                        itemDetail.ItemType = Convert.ToString(rd["ItemType"]);
                        itemDetail.CategoryName = Convert.ToString(rd[10]);
                        itemDetail.Category = Convert.ToString(rd["Category"]);
                        itemDetail.ItemSizeInfo.MarketPrice = Convert.ToDouble(rd["MarketPrice"]);
                        itemDetail.TotalStock = rd["itemstock"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["itemstock"]);
                        itemDetail.stockId = Convert.ToInt32(rd["StockId"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["StockId"]));
                        itemDetail.DiscountPerctg = rd["DiscountedPrice"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["DiscountedPrice"]);
                        itemDetail.ItemSizeInfo.PluCode = Convert.ToString(rd["PluCode"]);
                        itemDetail.Quantity = 1;
                        itemDetail.itemstock = Convert.ToString(itemDetail.TotalStock);


                    }
                    return itemDetail;
                }
            }
        }
        public List<Sales> GetPaymentSettlement(string Id, string PaymentSettled_By, string PaymentSettled_for, string Status)
        {
            List<Sales> CustomerPaymentSettlement = new List<Sales>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetPaymentSettlementDetail]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 30).Value = Id;
                cmd.Parameters.Add("@PaymentStatus", SqlDbType.VarChar, 100).Value = Status;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CustomerPaymentSettlement.Add(new Sales
                        {
                            SalesOrderId = Convert.ToString(rd["SalesOrderId"]),
                            CustomerId = Convert.ToString(rd["CustomerId"]),
                            PaymentDeliveredDate = Convert.ToDateTime(rd["DeliveryDate"]),
                            SalesPerson = Convert.ToString(rd["SalesPerson"]),
                            DeliveredPerson = Convert.ToString(rd["DeliveredPerson"]),
                            TotalPrice = Convert.ToDouble(rd["TotalPrice"]),
                            DeliveryCharges = Convert.ToString(rd["DeliveryCharges"]),
                            CoupenCalculation = Convert.ToDouble(rd["CoupenCalculation"] == DBNull.Value ? null : rd["CoupenCalculation"]),
                            PaymentSettled_By = PaymentSettled_By,
                            PaymentSettled_for = PaymentSettled_for,
                            Settlement_Status = Convert.ToString(rd["Settlement_Status"] == DBNull.Value ? null : rd["Settlement_Status"]),
                            Description = Convert.ToString(rd["Description"] == DBNull.Value ? null : rd["Description"]),
                            Id = Convert.ToInt32(rd["Id"]),
                            CustomerName = Convert.ToString(rd["CustomerName"]),
                            Wallet = Convert.ToDouble(rd["Wallet"] == DBNull.Value ? null : rd["Wallet"]),
                            PaymentStatus = Convert.ToString(rd["PaymentStatus"] == DBNull.Value ? null : rd["PaymentStatus"]),
                            PaymentMode = Convert.ToString(rd["PaymentMode"] == DBNull.Value ? null : rd["PaymentMode"]),

                        });
                    }
                    return CustomerPaymentSettlement;
                }
            }
        }

        //public int updatepaymentsettlement(PaymentSettlement paymentData)
        //{
        //    using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand("[dbo].[PaymentSettlement_Update]", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 30).Value = paymentData.SalesOrderId;
        //        cmd.Parameters.Add("@settlementStatus", SqlDbType.VarChar, 30).Value = "Paid";
        //        cmd.Parameters.Add("@PaymentSettled_for", SqlDbType.VarChar, 100).Value = paymentData.PaymentSettled_for == null ? (object)DBNull.Value : paymentData.PaymentSettled_for;
        //        cmd.Parameters.Add("@SettlementNotes", SqlDbType.VarChar, 30).Value = paymentData.Description;
        //        cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 30).Value = paymentData.UpdatedBy;
        //        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = paymentData.Id;

        //        con.Open();
        //        return Convert.ToInt32(cmd.ExecuteScalar());
        //    }

        //}
        public List<PaymentSettlement> GetPaymentSettlementSummary()
        {
            List<PaymentSettlement> CustomerPaymentSettlement = new List<PaymentSettlement>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetPaymentToSettledSummary]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CustomerPaymentSettlement.Add(new PaymentSettlement
                        {
                            PaymentSettled_Name = Convert.ToString(rd["PaymentSettled_Name"] == DBNull.Value ? null : rd["PaymentSettled_Name"]),
                            PaymentSettled_for = Convert.ToString(rd["PaymentSettled_for"] == DBNull.Value ? null : rd["PaymentSettled_for"]),
                            Payment_settledof = Convert.ToString(rd["Payment_settledof"] == DBNull.Value ? null : rd["Payment_settledof"]),
                            Payment_settledPrice = Convert.ToString(rd["Payment_settledPrice"] == DBNull.Value ? null : rd["Payment_settledPrice"]),
                            PendingPaymentCount = Convert.ToInt32(rd["PendingPaymentCount"])


                        });
                    }
                    return CustomerPaymentSettlement;
                }
            }
        }
        public int UpdatePaymentStatus(Sales SalesOrderData)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_UpdatePaymentStatus]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PaymentStatus", SqlDbType.VarChar, 100).Value = SalesOrderData.PaymentStatus;
                cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 100).Value = SalesOrderData.CustomerId;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = SalesOrderData.LastUpdatedBy;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 100).Value = SalesOrderData.SalesOrderId;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar, 30).Value = SalesOrderData.PaymentMode;

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int CustomertblEdit(Sales CusttblEdit)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_UpdateCustomerTbl]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tableId", SqlDbType.VarChar, 100).Value = CusttblEdit.tableId;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = CusttblEdit.LastUpdatedBy;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 100).Value = CusttblEdit.SalesOrderId;
                cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 100).Value = CusttblEdit.CustomerId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public List<PendingData> GetAllPendingOrders(PendingData info, string id)

        {
            info.OrderStatus = info.activeTab != "pending" && info.OrderStatus == null ? info.activeTab : info.OrderStatus;
            info.Branch = info.Role == "System Admin" || info.Role == "9" || info.Role == "10" ? null : info.Branch;
            List<PendingData> salesOrderList = new List<PendingData>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_PendingOrders]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@status", SqlDbType.VarChar, 50).Value = info.OrderStatus;
                cmd.Parameters.Add("@zipCode", SqlDbType.VarChar, 50).Value = info.ZipCode;
                cmd.Parameters.Add("@branch", SqlDbType.VarChar, 50).Value = id;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = id;
                cmd.Parameters.Add("@role", SqlDbType.VarChar, 50).Value = info.Role;
                cmd.Parameters.Add("@payment", SqlDbType.VarChar, 50).Value = info.PaymentStatus;

                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            salesOrderList.Add(new PendingData
                            {
                                Id = Convert.ToInt32(rd["Id"] == DBNull.Value ? 000 : rd["Id"]),
                                SalesOrder = Convert.ToString(rd["SalesOrderId"] == DBNull.Value ? "SO0010" : rd["SalesOrderId"]),
                                CustomerId = Convert.ToString(rd["CustomerId"] == DBNull.Value ? "CI0010" : rd["CustomerId"]),
                                Name = Convert.ToString(rd["Name"] == DBNull.Value ? "NOT Available" : rd["Name"]),
                                fullname = Convert.ToString(rd["fullname"] == DBNull.Value ? "NOT Available" : rd["fullname"]),
                                BuildingName = Convert.ToString(rd["BuildingName"] == DBNull.Value ? "NOT Available" : rd["BuildingName"]),
                                RoomNo = Convert.ToString(rd["RoomNo"] == DBNull.Value ? "NOT Available" : rd["RoomNo"]),
                                Sector = Convert.ToString(rd["Sector"] == DBNull.Value ? "NOT Available" : rd["Sector"]),
                                Locality = Convert.ToString(rd["Locality"] == DBNull.Value ? "NOT Available" : rd["Locality"]),
                                City = Convert.ToString(rd["City"] == DBNull.Value ? "NOT Available" : rd["City"]),
                                ZipCode = Convert.ToString(rd["ZipCode"] == DBNull.Value ? "NOT Available" : rd["ZipCode"]),
                                TotalPrice = Convert.ToString(rd["TotalPrice"] == DBNull.Value ? "00.00" : rd["TotalPrice"]),
                                TaxableAmount = Convert.ToDouble(rd["TaxableAmount"] == DBNull.Value ? "00.00" : rd["TaxableAmount"]),
                                //SGSTValue = Convert.ToDouble(rd["SGST"] == DBNull.Value ? 0.0 : rd["SGST"]),
                                OrderStatus = Convert.ToString(rd["OrderdStatus"] == DBNull.Value ? "NOT Available" : rd["OrderdStatus"]),
                                CreatedOn = Convert.ToDateTime(rd["CreatedOn"] == DBNull.Value ? null : rd["CreatedOn"]),
                                DeliveryDate = Convert.ToDateTime(rd["DeliveryDate"] == DBNull.Value ? null : rd["DeliveryDate"]),
                                PaymentStatus = Convert.ToString(rd["PaymentStatus"] == DBNull.Value ? "NOT Available" : rd["PaymentStatus"]),
                                ContactNo = Convert.ToString(rd["ContactNo"] == DBNull.Value ? "NOT Available" : rd["ContactNo"]),
                                phonenumber = Convert.ToString(rd["phonenumber"] == DBNull.Value ? "NOT Available" : rd["phonenumber"]),
                                //Discount = Convert.ToDouble(rd["Discount"] == DBNull.Value ? "0.00" : rd["Discount"]),
                                DeliveryCharges = Convert.ToString(rd["DeliveryCharges"] == DBNull.Value ? "NOT Available" : rd["DeliveryCharges"]),
                                Wallet = Convert.ToDouble(rd["Wallet"] == DBNull.Value ? "NOT" : rd["Wallet"]),
                                CustId = Convert.ToString(rd["CustId"] == DBNull.Value ? "0" : rd["CustId"]),
                                //tableId = Convert.ToString(rd["tableId"] == DBNull.Value ? "NOT Available" : rd["tableId"]),
                                //tableName = Convert.ToString(rd["tableName"]),
                                PaymentMode = Convert.ToString(rd["PaymentMode"] == DBNull.Value ? "Not Found" : rd["PaymentMode"]),
                                OrderType = Convert.ToString(rd["OrderType"] == DBNull.Value ? "Not Available" : rd["OrderType"]),
                                taxType = Convert.ToInt32(rd["taxType"] == DBNull.Value ? 0 : rd["taxType"]),
                                taxbillType = Convert.ToInt32(rd["taxbillType"] == DBNull.Value ? 0 : rd["taxbillType"]),
                                vatTax = Convert.ToSingle(rd["vatTax"] == DBNull.Value ? 0.0 : Convert.ToSingle(rd["vatTax"])),
                                gstTax = Convert.ToSingle(rd["gstTax"] == DBNull.Value ? "0" : rd["gstTax"]),
                                currencttype = Convert.ToString(rd["Symbol"] == DBNull.Value ? "0" : rd["Symbol"])
                            });
                        }
                        return salesOrderList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }

        }
        public List<SelectListItem> GetAllZipcode()
        {
            List<SelectListItem> zipcode = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Sale_GetZipcode]";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        zipcode.Add(new SelectListItem
                        {
                            Text = Convert.ToString(rd["ZipCode"] == DBNull.Value ? "0" : Convert.ToString(rd["ZipCode"])),
                            Value = Convert.ToString(rd["ZipCode"] == DBNull.Value ? "0" : Convert.ToString(rd["ZipCode"])),
                        });
                    }
                    return zipcode;
                }
            }
        }
        public async Task<byte[]> ExportExcelofPendingSales(string webRootPath, string status, string zipcode, string activeTab, string payment, string id)
        {
            //To add data in DTO
            PendingData info = new PendingData();
            info.OrderStatus = status;
            info.ZipCode = zipcode;
            info.activeTab = activeTab;
            info.PaymentStatus = payment;
            //
            string fileName = Path.Combine(webRootPath, "Pending-Sales.xlsx");
            FileInfo newFile = new FileInfo(fileName);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(fileName);
            }
            Stream stream = new System.IO.MemoryStream();
            using (var package = new ExcelPackage(newFile))
            {

                var worksheet1 = package.Workbook.Worksheets.Add("PendingSalesOverview");
                //IQueryable<Lead> leadList = null;
                var overviewList = GetAllPendingOrders(info, id);
                //var ovrviewDetail = await GetTodaySalesDetail(salesdata);

                int rowCount = 1;

                var rowdetail = rowCount;

                #region ExcelHeader
                worksheet1.Cells[rowdetail, 1].Value = "Sales Id";
                worksheet1.Cells[rowdetail, 1].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(1).AutoFit();

                worksheet1.Cells[rowdetail, 2].Value = "Cutomer Name";
                worksheet1.Cells[rowdetail, 2].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(2).AutoFit();

                worksheet1.Cells[rowdetail, 3].Value = "Contact No";
                worksheet1.Cells[rowdetail, 3].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(3).AutoFit();

                worksheet1.Cells[rowdetail, 4].Value = "Address";
                worksheet1.Cells[rowdetail, 4].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(4).AutoFit();


                worksheet1.Cells[rowdetail, 5].Value = "Area";
                worksheet1.Cells[rowdetail, 5].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(5).AutoFit();

                worksheet1.Cells[rowdetail, 6].Value = "ZipCode";
                worksheet1.Cells[rowdetail, 6].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(6).AutoFit();


                worksheet1.Cells[rowdetail, 7].Value = "Total Price";
                worksheet1.Cells[rowdetail, 7].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(6).AutoFit();


                worksheet1.Cells[rowdetail, 8].Value = "Order Status";
                worksheet1.Cells[rowdetail, 8].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(7).AutoFit();


                worksheet1.Cells[rowdetail, 9].Value = "Payment";
                worksheet1.Cells[rowdetail, 9].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 9].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(8).AutoFit();

                worksheet1.Cells[rowdetail, 10].Value = "Order On";
                worksheet1.Cells[rowdetail, 10].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 10].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(9).AutoFit();
                #endregion

                rowdetail++;
                foreach (var Salesdetail in overviewList)
                {
                    worksheet1.Cells[rowdetail, 1].Value = Salesdetail.SalesOrder;
                    worksheet1.Cells[rowdetail, 2].Value = Salesdetail.Name;
                    worksheet1.Cells[rowdetail, 3].Value = Salesdetail.ContactNo;
                    worksheet1.Cells[rowdetail, 4].Value = Salesdetail.BuildingName + ',' + Salesdetail.RoomNo + ',' + Salesdetail.Sector;
                    worksheet1.Cells[rowdetail, 5].Value = Salesdetail.City;
                    worksheet1.Cells[rowdetail, 6].Value = Salesdetail.ZipCode;
                    worksheet1.Cells[rowdetail, 7].Value = Salesdetail.TotalPrice;
                    worksheet1.Cells[rowdetail, 8].Value = Salesdetail.OrderStatus;
                    worksheet1.Cells[rowdetail, 9].Value = Salesdetail.PaymentStatus;
                    worksheet1.Cells[rowdetail, 10].Value = Salesdetail.CreatedOn.ToString("yyyy-MM-dd hh:mm tt");



                    rowdetail++;
                }



                package.SaveAs(newFile);
                byte[] files = File.ReadAllBytes(fileName);
                File.Delete(fileName);
                return files;
            }

        }
        public void UpdateSalesOtp(string otp, int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sale_UpdateOTP]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@otp", SqlDbType.VarChar, 30).Value = otp;
                cmd.Parameters.Add("@salesId", SqlDbType.Int).Value = id;

                con.Open();
                cmd.ExecuteNonQuery();
            }

        }
        public string CheckSaleOTPExist(string otp)
        {
            string returnOtp = "";
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_OTPExist]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@otp", SqlDbType.VarChar, 100).Value = otp;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        returnOtp = Convert.ToString(rd[0]);
                    }
                }
            }
            return returnOtp;
        }
        public SalesCountData GetSalesCount()
        {
            var salesdata = new SalesCountData();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Sales_GetCount]";
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dataRead = cmd.ExecuteReader())
                {
                    if (dataRead.Read())
                    {
                        salesdata.TotalConfirmed = Convert.ToInt32(dataRead[0]);
                        salesdata.TotalPending = Convert.ToInt32(dataRead["TotalPending"]);
                        salesdata.TotalPendingAmount = Convert.ToInt32(dataRead["TotalPendingAmount"]);
                        salesdata.TodaysPending = Convert.ToInt32(dataRead["TodaysPending"]);
                        salesdata.TodaysPendingAmount = Convert.ToInt32(dataRead["PendingAmount"]);
                    }
                    return salesdata;
                }
            }
        }
        //public async Task<byte[]> FilterExcelofSales(string branch, string role, string date, string status, string source, string payment, int a, int b, string startdate, string enddate, string webRootPath)
        //{

        //    string fileName = Path.Combine(webRootPath, "SalesOverview.xlsx");
        //    FileInfo newFile = new FileInfo(fileName);
        //    if (newFile.Exists)
        //    {
        //        newFile.Delete();
        //        newFile = new FileInfo(fileName);
        //    }
        //    Stream stream = new System.IO.MemoryStream();
        //    using (var package = new ExcelPackage(newFile))
        //    {

        //        var worksheet1 = package.Workbook.Worksheets.Add("SalesOverview");
        //        //IQueryable<Lead> leadList = null;
        //        List<Sales> overviewList = new List<Sales>();
        //        if (startdate == null && enddate == null && a == 0 && b == 0)
        //        {
        //            overviewList = GetAllSalesOrderList(branch, role, date, status, source, payment);

        //        }
        //        else
        //        {
        //            overviewList = GetAllFinancialSaleOrderList(a, b, startdate, enddate);
        //        }
        //        int rowCount = 2;

        //        var rowdetail = rowCount;

        //        #region ExcelHeader
        //        worksheet1.Cells[rowdetail, 1].Value = "Sales Order Id";
        //        worksheet1.Cells[rowdetail, 1].Style.Font.Bold = true;
        //        worksheet1.Cells[rowdetail, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        //        worksheet1.Column(1).AutoFit();

        //        worksheet1.Cells[rowdetail, 2].Value = "Cutomer Name";
        //        worksheet1.Cells[rowdetail, 2].Style.Font.Bold = true;
        //        worksheet1.Cells[rowdetail, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        //        worksheet1.Column(2).AutoFit();

        //        worksheet1.Cells[rowdetail, 3].Value = "Order Date";
        //        worksheet1.Cells[rowdetail, 3].Style.Font.Bold = true;
        //        worksheet1.Cells[rowdetail, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        //        worksheet1.Column(3).AutoFit();

        //        worksheet1.Cells[rowdetail, 4].Value = "Total Item";
        //        worksheet1.Cells[rowdetail, 4].Style.Font.Bold = true;
        //        worksheet1.Cells[rowdetail, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        //        worksheet1.Column(4).AutoFit();


        //        worksheet1.Cells[rowdetail, 5].Value = "Delivery Status / Source";
        //        worksheet1.Cells[rowdetail, 5].Style.Font.Bold = true;
        //        worksheet1.Cells[rowdetail, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        //        worksheet1.Column(5).AutoFit();



        //        worksheet1.Cells[rowdetail, 6].Value = "Payment Status";
        //        worksheet1.Cells[rowdetail, 6].Style.Font.Bold = true;
        //        worksheet1.Cells[rowdetail, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        //        worksheet1.Column(6).AutoFit();


        //        worksheet1.Cells[rowdetail, 7].Value = "Payment Mode";
        //        worksheet1.Cells[rowdetail, 7].Style.Font.Bold = true;
        //        worksheet1.Cells[rowdetail, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        //        worksheet1.Column(7).AutoFit();


        //        worksheet1.Cells[rowdetail, 8].Value = "Bill Amount";
        //        worksheet1.Cells[rowdetail, 8].Style.Font.Bold = true;
        //        worksheet1.Cells[rowdetail, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        //        worksheet1.Column(8).AutoFit();

        //        worksheet1.Cells[rowdetail, 9].Value = "Discount Amount";
        //        worksheet1.Cells[rowdetail, 9].Style.Font.Bold = true;
        //        worksheet1.Cells[rowdetail, 9].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        //        worksheet1.Column(9).AutoFit();

        //        worksheet1.Cells[rowdetail, 10].Value = "Wallet Used";
        //        worksheet1.Cells[rowdetail, 10].Style.Font.Bold = true;
        //        worksheet1.Cells[rowdetail, 10].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        //        worksheet1.Column(10).AutoFit();

        //        #endregion

        //        rowdetail++;
        //        foreach (var Salesdetail in overviewList)
        //        {
        //            worksheet1.Cells[rowdetail, 1].Value = Salesdetail.SalesOrderId;
        //            worksheet1.Cells[rowdetail, 2].Value = Salesdetail.Name;
        //            worksheet1.Cells[rowdetail, 3].Value = Salesdetail.CreatedOn.ToString("yyyy-MM-dd hh:mm"); ;
        //            worksheet1.Cells[rowdetail, 4].Value = Salesdetail.PluCount;
        //            worksheet1.Cells[rowdetail, 5].Value = Salesdetail.OrderdStatus + " / " + Salesdetail.Source;
        //            worksheet1.Cells[rowdetail, 6].Value = Salesdetail.PaymentStatus;
        //            worksheet1.Cells[rowdetail, 7].Value = Salesdetail.ModeFullForm;
        //            worksheet1.Cells[rowdetail, 8].Value = "Rs. " + (Salesdetail.TotalPrice + Convert.ToDouble(Salesdetail.DeliveryCharges));
        //            worksheet1.Cells[rowdetail, 9].Value = "Rs. " + Salesdetail.Discount;
        //            worksheet1.Cells[rowdetail, 10].Value = "Rs. " + Salesdetail.Wallet;


        //            rowdetail++;
        //        }

        //        package.SaveAs(newFile);
        //        byte[] files = File.ReadAllBytes(fileName);
        //        File.Delete(fileName);
        //        return files;
        //    }

        //}
         public async Task<byte[]> FilterExcelofSales(string branch, string hubId, string role, string date, string status, string source, string payment, int a, int b, string startdate, string enddate, string webRootPath)
        {

            string fileName = Path.Combine(webRootPath, "SalesOverview.xlsx");
            FileInfo newFile = new FileInfo(fileName);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(fileName);
            }
            Stream stream = new System.IO.MemoryStream();
            using (var package = new ExcelPackage(newFile))
            {

                var worksheet1 = package.Workbook.Worksheets.Add("SalesOverview");
                //IQueryable<Lead> leadList = null;
                List<Sales> overviewList = new List<Sales>();
                if (startdate == null && enddate == null && a == 0 && b == 0)
                {
                    overviewList = GetAllSalesOrderList(branch, role, date, status, source, payment, hubId);

                }
                else
                {
                    overviewList = GetAllFinancialSaleOrderList(a, b, startdate, enddate, hubId);
                }
                int rowCount = 2;

                var rowdetail = rowCount;

                #region ExcelHeader
                worksheet1.Cells[rowdetail, 1].Value = "Order Id";
                worksheet1.Cells[rowdetail, 1].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(1).AutoFit();

                worksheet1.Cells[rowdetail, 2].Value = "Table Name";
                worksheet1.Cells[rowdetail, 2].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(2).AutoFit();

                worksheet1.Cells[rowdetail, 3].Value = "Cutomer Name";
                worksheet1.Cells[rowdetail, 3].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(3).AutoFit();

                worksheet1.Cells[rowdetail, 4].Value = "Order Date";
                worksheet1.Cells[rowdetail, 4].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(4).AutoFit();

                worksheet1.Cells[rowdetail, 5].Value = "Total Item";
                worksheet1.Cells[rowdetail, 5].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(5).AutoFit();


                worksheet1.Cells[rowdetail, 6].Value = "Delivery Status";
                worksheet1.Cells[rowdetail, 6].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(6).AutoFit();

                worksheet1.Cells[rowdetail, 7].Value = "Delivered On";
                worksheet1.Cells[rowdetail, 7].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(7).AutoFit();



                worksheet1.Cells[rowdetail, 8].Value = "Payment Status";
                worksheet1.Cells[rowdetail, 8].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(8).AutoFit();


                worksheet1.Cells[rowdetail, 9].Value = "Payment Mode";
                worksheet1.Cells[rowdetail, 9].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 9].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(9).AutoFit();


                worksheet1.Cells[rowdetail, 10].Value = "Sub Total";
                worksheet1.Cells[rowdetail, 10].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 10].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(10).AutoFit();

                worksheet1.Cells[rowdetail, 11].Value = "GST";
                worksheet1.Cells[rowdetail, 11].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 11].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(11).AutoFit();

                worksheet1.Cells[rowdetail, 12].Value = "Discount Amount";
                worksheet1.Cells[rowdetail, 12].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 12].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(12).AutoFit();

                worksheet1.Cells[rowdetail, 13].Value = "Bill Payable";
                worksheet1.Cells[rowdetail, 13].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 13].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(13).AutoFit();

                worksheet1.Cells[rowdetail, 14].Value = "Waiter Name";
                worksheet1.Cells[rowdetail, 14].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 14].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(14).AutoFit();

                //worksheet1.Cells[rowdetail, 13].Value = "Wallet Used";
                //worksheet1.Cells[rowdetail, 13].Style.Font.Bold = true;
                //worksheet1.Cells[rowdetail, 13].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                //worksheet1.Column(10).AutoFit();


                //Waiter Name
                //Table Name


                #endregion

                rowdetail++;
                foreach (var Salesdetail in overviewList)
                {
                    worksheet1.Cells[rowdetail, 1].Value = Salesdetail.SalesOrderId;
                   // worksheet1.Cells[rowdetail, 2].Value = Salesdetail.tableName;
                    worksheet1.Cells[rowdetail, 3].Value = Salesdetail.Name;
                    worksheet1.Cells[rowdetail, 4].Value = Salesdetail.CreatedOn.ToString("yyyy-MM-dd hh:mm tt"); ;
                    worksheet1.Cells[rowdetail, 5].Value = Salesdetail.PluCount;
                    worksheet1.Cells[rowdetail, 6].Value = Salesdetail.OrderdStatus;
                    worksheet1.Cells[rowdetail, 7].Value = Salesdetail.LastUpdatedOn.ToString("yyyy-MM-dd hh:mm tt"); ;
                    worksheet1.Cells[rowdetail, 8].Value = Salesdetail.PaymentStatus;
                    worksheet1.Cells[rowdetail, 9].Value = Salesdetail.PaymentMode;
                    worksheet1.Cells[rowdetail, 10].Value = Salesdetail.TotalPrice + Convert.ToDouble(Salesdetail.DeliveryCharges);
                    worksheet1.Cells[rowdetail, 11].Value = Salesdetail.gstTax;
                    worksheet1.Cells[rowdetail, 12].Value = Salesdetail.Discount;
                    worksheet1.Cells[rowdetail, 13].Value = Salesdetail.Taxable_Amount;
                   // worksheet1.Cells[rowdetail, 14].Value = Salesdetail.WaiterName;
                    //worksheet1.Cells[rowdetail, 13].Value = "Rs. " + Salesdetail.Wallet;


                    rowdetail++;
                }

                package.SaveAs(newFile);
                byte[] files = File.ReadAllBytes(fileName);
                File.Delete(fileName);
                return files;
            }

        }
        public string updatepaymentsettlement(PaymentSettlement payment)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_UpdatePaymentSettled]", con))
            {
                try
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("SalesOrderId", typeof(string));
                    odt.Columns.Add("SettlementNotes", typeof(string));
                    odt.Columns.Add("SettledId", typeof(int));
                    var count = 0;
                    if (payment.Checkboxid != null)
                        foreach (var o in payment.Checkboxid)
                        {
                            odt.Rows.Add(o.Split('_')[0], payment.multiDescription[count], o.Split('_')[1]);
                            count++;
                        }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PaymentSettled_for", SqlDbType.VarChar, 100).Value = payment.Checkboxid[0].Split('_')[3];
                    cmd.Parameters.Add("@tblList", SqlDbType.Structured).Value = odt;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 100).Value = payment.UpdatedBy;
                    return cmd.ExecuteScalar() as string;
                }
                catch (Exception ex)
                {
                    return "1";
                }

            }
        }
        public bool UpdateOrderList(string ids, string updatedBy)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_UpdateOrderList]", con))
            {
                con.Open();
                DataTable odt = new DataTable();
                odt.Columns.Add("ID", typeof(int));
                if (ids != null)
                    foreach (var o in ids.Split(','))
                        odt.Rows.Add(Convert.ToInt32(o));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@saleslist", SqlDbType.Structured).Value = odt;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = updatedBy;
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool DeductSalesStock(Sales dicData, string createdBy, string branch)
        {
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    sqlcon.Open();
                    cmd.Connection = sqlcon;
                    cmd.CommandText = "[dbo].[Sales_DeductStock]";
                    DataTable odt = new DataTable();
                    odt.Columns.Add("SalesId", typeof(string));
                    odt.Columns.Add("CustomerId", typeof(string));
                    odt.Columns.Add("Category", typeof(string));
                    odt.Columns.Add("ItemId", typeof(string));
                    odt.Columns.Add("Measurement", typeof(string));
                    odt.Columns.Add("QuantityValue", typeof(float));
                    odt.Columns.Add("PricePerMeas", typeof(float));
                    odt.Columns.Add("TotalPrice", typeof(float));
                    odt.Columns.Add("Discount", typeof(float));
                    odt.Columns.Add("weight", typeof(float));
                    odt.Columns.Add("DeliveryDate", typeof(DateTime));
                    odt.Columns.Add("CreatedBy", typeof(string));
                    odt.Columns.Add("LastUpdatedBy", typeof(string));

                    if (dicData.MultipleItem != null)
                        foreach (var data in dicData.MultipleItem)
                            odt.Rows.Add(data.Split('_')[10]//ItemType
                               , dicData.CustomerId
                                , data.Split('_')[3]//Category
                                , data.Split('_')[0]//ItemId
                                , data.Split('_')[6]//Measurement
                                , data.Split('_')[2]//QuantValue
                                , data.Split('_')[1]//PricePerMeas
                                , data.Split('_')[4]//TotalPrice
                                , data.Split('_')[8]//Discount
                                , data.Split('_')[5]//Weight
                                , dicData.Deliverydt
                                , createdBy
                                , createdBy
                                );
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                    cmd.Parameters.AddWithValue("@hub", branch);
                    cmd.Parameters.Add("@tblsalesList", SqlDbType.Structured).Value = odt;
                    return cmd.ExecuteNonQuery() > 0;

                }
            }
        }
        public int UpdateDeductedStock(SalesList ProductList, string createdBy, string branch)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_DeductStock]", con))
            {
                con.Open();
                DataTable odt = new DataTable();
                odt.Columns.Add("SalesId", typeof(string));
                odt.Columns.Add("CustomerId", typeof(string));
                odt.Columns.Add("Category", typeof(string));
                odt.Columns.Add("ItemId", typeof(string));
                odt.Columns.Add("Measurement", typeof(string));
                odt.Columns.Add("QuantityValue", typeof(float));
                odt.Columns.Add("PricePerMeas", typeof(float));
                odt.Columns.Add("TotalPrice", typeof(float));
                odt.Columns.Add("Discount", typeof(float));
                odt.Columns.Add("weight", typeof(float));
                odt.Columns.Add("DeliveryDate", typeof(DateTime));
                odt.Columns.Add("CreatedBy", typeof(string));
                odt.Columns.Add("LastUpdatedBy", typeof(string));

                if (ProductList != null)
                    for (var i = 0; i < ProductList.Measurements.Count; i++)
                        odt.Rows.Add(ProductList.SalesId,
                            ProductList.CustomerId,
                            ProductList.Categorys[i],
                            ProductList.ItemIds[i],
                            ProductList.Measurements[i],
                            ProductList.QuantityValues[i]
                            , ProductList.PricePerMeasList[i]
                            , ProductList.TotalPriceList[i]
                            , ProductList.DiscountList[i]
                            , ProductList.weightList[i]
                            , ProductList.ProductDeliveryDates
                            , ProductList.CreatedBy
                            , ProductList.LastUpdatedBy);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                cmd.Parameters.AddWithValue("@hub", branch);
                cmd.Parameters.Add("@tblsalesList", SqlDbType.Structured).Value = odt;

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public SalesDetail GetSalesGST(string Id)
        {
            SalesDetail GSTDetail = new SalesDetail();

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_GetGST]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesId", SqlDbType.VarChar, 100).Value = Id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        GSTDetail = new SalesDetail
                        {
                            GST_Sum = Convert.ToDouble(rd[0]),
                            TotalAmount = Convert.ToDouble(rd["TotalAmount"]),
                            CGSTValue = Convert.ToDouble(rd["CGST"]),
                            SGSTValue = Convert.ToDouble(rd["SGST"]),
                            TaxableAmount = Convert.ToDouble(rd["TaxableAmount"]),
                            TotalPrice = Convert.ToDouble(rd["MinusTaxable"]),

                        };
                    }
                    return GSTDetail;
                }
            }
        }

        public TaxationInfo GetTaxationDetails()
        {
            TaxationInfo taxInfo = new TaxationInfo();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_TaxationinfoDetails]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            taxInfo = new TaxationInfo
                            {
                                id = Convert.ToInt32(sdr["id"]),
                                taxType = Convert.ToInt32(sdr["taxType"]),
                                taxRegNo = Convert.ToString(sdr["taxRegNo"]),
                                vatRegNo = Convert.ToString(sdr["vatRegNo"]),
                                calcuationTaxtype = Convert.ToInt32(sdr["calcuationTaxtype"]),
                                taxPercentGST = Convert.ToSingle(sdr["gstTaxpercent"]),
                                taxPercentVAT = Convert.ToSingle(sdr["vatTaxpercent"]),
                                isActive = Convert.ToInt32(sdr["isActive"]),
                            };
                        }
                    }

                }
            }
            return taxInfo;
        }


        public List<Sales> GetAllFinancialSaleOrderList(int a, int b, string c, string d,string hubId)
        {
            List<Sales> salesOrderList = new List<Sales>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Dashboard_SalesOrderListFianaceWise]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@a", SqlDbType.Int).Value = a;
                cmd.Parameters.Add("@b", SqlDbType.Int).Value = b;
                cmd.Parameters.Add("@startdate", SqlDbType.VarChar, 30).Value = c;
                cmd.Parameters.Add("@enddate", SqlDbType.VarChar, 30).Value = d;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 30).Value = hubId;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            salesOrderList.Add(new Sales
                            {
                                Id = Convert.ToInt32(rd["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rd["ID"])),
                                SalesOrderId = Convert.ToString(rd["SalesOrderId"] == DBNull.Value ? "SO00" : Convert.ToString(rd["SalesOrderId"])),
                                CustomerId = Convert.ToString(rd["CustomerId"] == DBNull.Value ? "CI00" : Convert.ToString(rd["CustomerId"])),
                                Name = Convert.ToString(rd["Name"] == DBNull.Value ? "Not Known" : Convert.ToString(rd["Name"])),
                                currencytype = Convert.ToString(rd["Symbol"] == DBNull.Value ? "NA" : rd["Symbol"]),
                                ContactNo = Convert.ToString(rd["ContactNo"] == DBNull.Value ? "0000000000" : Convert.ToString(rd["ContactNo"])),
                                SalesPerson = Convert.ToString(rd["SalesPerson"] == DBNull.Value ? "Not Known" : Convert.ToString(rd["SalesPerson"])),
                                PluCount = Convert.ToInt32(rd["PLU_Count"] == DBNull.Value ? 0 : Convert.ToInt32(rd["PLU_Count"])),
                                TotalQuantity = Convert.ToDouble(rd["TotalQuantity"] == DBNull.Value ? 0.0 : Convert.ToDouble(rd["TotalQuantity"])),
                                TotalPrice = Convert.ToDouble(rd["TotalPrice"] == DBNull.Value ? 0.0 : Convert.ToDouble(rd["TotalPrice"])),
                                Discount = Convert.ToDouble(rd["Discount"] == DBNull.Value ? 0.0 : Convert.ToDouble(rd["Discount"])),
                                OrderdStatus = Convert.ToString(rd["OrderdStatus"] == DBNull.Value ? "Not Known" : Convert.ToString(rd["OrderdStatus"])),
                                PaymentStatus = Convert.ToString(rd["PaymentStatus"] == DBNull.Value ? "Not Known" : Convert.ToString(rd["PaymentStatus"])),
                                DeliveryDate1 = Convert.ToDateTime(rd["DeliveryDate"] == DBNull.Value ? null : rd["DeliveryDate"]),
                                CreatedBy = Convert.ToString(rd["CreatedBy"] == DBNull.Value ? "Not Known" : Convert.ToString(rd["CreatedBy"])),
                                CreatedOn = Convert.ToDateTime(rd["CreatedOn"] == DBNull.Value ? null : rd["CreatedOn"]),
                                PaymentMode = Convert.ToString(rd["PaymentMode"] == DBNull.Value ? "Not Known" : Convert.ToString(rd["PaymentMode"])),
                                taxType = Convert.ToInt32(rd["taxType"] == DBNull.Value ? 0 : rd["taxType"]),
                                taxbillType = Convert.ToInt32(rd["taxbillType"] == DBNull.Value ? 0 : rd["taxbillType"]),
                                vatTax = Convert.ToSingle(rd["vatTax"] == DBNull.Value ? 0.0 : Convert.ToSingle(rd["vatTax"])),
                                gstTax = Convert.ToSingle(rd["gstTax"] == DBNull.Value ? 0 : rd["gstTax"]),
                                TaxableAmount = Convert.ToDouble(rd["TaxableAmount"] == DBNull.Value ? "00.00" : rd["TaxableAmount"]),
                            });
                        }
                        return salesOrderList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public int UpdateTaxesValue(string salesId,string hubId,int Isactive)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "Sales_UpdateTaxes";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesId", SqlDbType.VarChar, 100).Value = salesId;
                cmd.Parameters.Add("@HubId", SqlDbType.VarChar, 100).Value = hubId;
               // cmd.Parameters.Add("@isActive", SqlDbType.VarChar, 100).Value = Isactive;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public async Task<List<PrintSalesList>> GetSalesListForPrint(string SalesId, string hubId)
        {
            List<PrintSalesList> salesList = new List<PrintSalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "Sales_GetPrintData";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SaleId", SqlDbType.VarChar).Value = SalesId;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = hubId;
                con.Open();
                using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        salesList.Add(new PrintSalesList
                        {
                            ItemId = Convert.ToString(dataReader["ItemId"]),
                            PluName = Convert.ToString(dataReader["Pluname"]),
                            Measurement = Convert.ToString(dataReader["Measurement"]),
                            QuantityValue = Convert.ToString(dataReader["QuantityValue"]),
                            PricePerMeas = Convert.ToString(dataReader["PricePerMeas"]),
                            TotalPrice = Convert.ToString(dataReader["TotalPrice"]),
                            Discount = Convert.ToString(dataReader["Discount"]),
                            Weight = Convert.ToString(dataReader["weight"]),
                            Taxable_Amount = Convert.ToString(dataReader["Taxable_Amount"]),
                            gst_perId = Convert.ToString(dataReader["gst_Id"]),
                            sgst_per = Convert.ToString(dataReader["sgst_per"]),
                            cgst_Per = Convert.ToString(dataReader["cgst_Per"]),
                            CGST_Value = Convert.ToString(dataReader["CGST"]),
                            SGST_Value = Convert.ToString(dataReader["SGST"]),
                            IGST_Per = Convert.ToString(dataReader["gst_Per"]),
                            ItemType = Convert.ToString(dataReader["ItemType"]),
                            HSNCode = Convert.ToString(dataReader["HSN_Code"]),
                            Size = Convert.ToString(dataReader["Size"]),
                        });
                    }
                    return salesList;
                }
            }

        }
        public async Task<List<TaxPercentageMst>> GetGSTCount(string SalesId)
        {
            List<TaxPercentageMst> tax = new List<TaxPercentageMst>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Sales_GetGSTCount]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesId", SqlDbType.VarChar, 50).Value = SalesId;
                con.Open();
                using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        tax.Add(new TaxPercentageMst
                        {
                            Id = Convert.ToInt32(dataReader["rowNumber"]),
                            IGST_Per = Convert.ToDouble(dataReader["IGST_Per"]),

                            SGST_Per = Convert.ToDouble(dataReader["SGST_Per"]),
                            CGST_Per = Convert.ToDouble(dataReader["CGST_Per"]),
                            TotalAmount = Convert.ToDouble(dataReader["TotalAmount"]),
                            CGSTValue = Convert.ToDouble(dataReader["CGST"]),
                            SGSTValue = Convert.ToDouble(dataReader["SGST"]),
                            TaxableAmount = Convert.ToDouble(dataReader["TaxableAmount"]),



                        });
                    }
                    return tax;
                }
            }

        }
        public async Task<Sales> GetSummaryDetail(string id)
        {
            var salesdata = new Sales();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Sales_GetSummaryDetail]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesId", SqlDbType.VarChar, 50).Value = id;

                con.Open();
                using (SqlDataReader dataRead = await cmd.ExecuteReaderAsync())
                {
                    if (dataRead.Read())
                    {
                        salesdata.PaymentStatus = Convert.ToString(dataRead["PaymentStatus"] == DBNull.Value ? "Not Mention" : dataRead["PaymentStatus"]);
                        salesdata.TotalPrice = Convert.ToDouble(dataRead["TotalPrice"] == DBNull.Value ? 0.0 : dataRead["TotalPrice"]);
                        salesdata.TotaldisAmt = Convert.ToDouble(dataRead["OrderDiscount"] == DBNull.Value ? 0.0 : dataRead["OrderDiscount"]);
                        salesdata.TotalSaleAmount = Convert.ToDouble(dataRead["OrderValue"] == DBNull.Value ? 0.0 : dataRead["OrderValue"]);
                        salesdata.DeliveryCharges = Convert.ToString(dataRead["DeliveryCharges"] == DBNull.Value ? "Not Mention" : dataRead["DeliveryCharges"]);
                        salesdata.Discount = Convert.ToDouble(dataRead["Discount"] == DBNull.Value ? 0.0 : dataRead["Discount"]);
                        salesdata.PaymentMode = Convert.ToString(dataRead["PaymentMode"] == DBNull.Value ? "Not Mention" : dataRead["PaymentMode"]);
                        salesdata.Wallet = Convert.ToDouble(dataRead["Wallet"] == DBNull.Value ? 0.0 : dataRead["Wallet"]);
                        salesdata.TotalAmt = Convert.ToDouble(dataRead["TotallistPrice"] == DBNull.Value ? 0.0 : dataRead["TotallistPrice"]);

                    }
                    return salesdata;
                }
            }
        }
        public async Task<List<Item>> GetHubItemList(string hub, string searchTerm = null)
        {
            var insertValue = new StringBuilder();
            List<Item> itemList = new List<Item>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Hub_GetItemList", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(searchTerm))
                    cmd.Parameters.Add("@searchTerm", SqlDbType.VarChar, 100).Value = searchTerm;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = hub;

                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
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
                            DiscountPerctg = rd["DiscountedPrice"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["DiscountedPrice"]),
                            Purchaseprice = Convert.ToDouble(rd["PurchasePrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["PurchasePrice"])),
                            ProfitMargin = Convert.ToDouble(rd["ProfitMargin"] == DBNull.Value ? 0 : Convert.ToDouble(rd["ProfitMargin"])),
                            CoupenDisc = Convert.ToInt32(rd["Coupen_Disc"] == DBNull.Value ? 0 : Convert.ToDouble(rd["Coupen_Disc"])),
                            Id = Convert.ToInt32(rd["Item_Id"])
                        });
                    }
                    return itemList;
                }
            }
        }

        public List<Sales> GetFinancialSelOrderListHubWise(int a, int b, string c)
        {
            List<Sales> salesOrderList = new List<Sales>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[DashboardFinancial_SalesOrderListHubWise]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@a", SqlDbType.Int).Value = a;
                cmd.Parameters.Add("@b", SqlDbType.Int).Value = b;
                cmd.Parameters.Add("@c", SqlDbType.VarChar).Value = c;

                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        salesOrderList.Add(new Sales
                        {
                            Id = Convert.ToInt32(rd["ID"]),
                            SalesOrderId = rd["SalesOrderId"] as string,
                            CustomerId = rd["CustomerId"] as string,
                            Name = rd["Name"] as string,
                            ContactNo = rd["ContactNo"] as string,
                            SalesPerson = rd["SalesPerson"] as string,
                            PluCount = Convert.ToInt32(rd["PLU_Count"]),
                            TotalQuantity = Convert.ToDouble(rd["TotalQuantity"]),
                            TotalPrice = Convert.ToDouble(rd["TotalPrice"]),
                            Discount = Convert.ToDouble(rd["Discount"]),
                            OrderdStatus = rd["OrderdStatus"] as string,
                            PaymentStatus = rd["PaymentStatus"] as string,
                            DeliveryDate1 = Convert.ToDateTime(rd["DeliveryDate"]),
                            CreatedBy = rd["CreatedBy"] as string,
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            PaymentMode = rd["PaymentMode"] as string,
                        });
                    }
                    return salesOrderList;
                }
            }
        }

        public List<Sales> GetFinancialSelOrderListZipWise(int a, int b, string c, string d)
        {
            List<Sales> salesOrderList = new List<Sales>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[DashboardFinancial_SalesOrderListZipWise]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@a", SqlDbType.Int).Value = a;
                cmd.Parameters.Add("@b", SqlDbType.Int).Value = b;
                cmd.Parameters.Add("@d", SqlDbType.VarChar).Value = d;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        salesOrderList.Add(new Sales
                        {
                            Id = Convert.ToInt32(rd["ID"]),
                            SalesOrderId = rd["SalesOrderId"] as string,
                            CustomerId = rd["CustomerId"] as string,
                            Name = rd["Name"] as string,
                            ContactNo = rd["ContactNo"] as string,
                            SalesPerson = rd["SalesPerson"] as string,
                            PluCount = Convert.ToInt32(rd["PLU_Count"]),
                            TotalQuantity = Convert.ToDouble(rd["TotalQuantity"]),
                            TotalPrice = Convert.ToDouble(rd["TotalPrice"]),
                            Discount = Convert.ToDouble(rd["Discount"]),
                            OrderdStatus = rd["OrderdStatus"] as string,
                            PaymentStatus = rd["PaymentStatus"] as string,
                            DeliveryDate1 = Convert.ToDateTime(rd["DeliveryDate"]),
                            CreatedBy = rd["CreatedBy"] as string,
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            PaymentMode = rd["PaymentMode"] as string,
                        });
                    }
                    return salesOrderList;
                }
            }
        }

        public List<TableInfo> GetTableList(string id)
        {
            List<TableInfo> list = new List<TableInfo>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sale_TableList1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hubId", SqlDbType.Int).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        list.Add(new TableInfo
                        {
                            tableId = Convert.ToString(rd["tableId"]),
                            tableName = Convert.ToString(rd["tableName"]),
                        });

                    }
                    return list;
                }
            }
        }

        public List<SelectListItem> GettableList(string id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sale_TableList1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hubId", SqlDbType.Int).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["tableName"]);
                        string Id = Convert.ToString(rd["tableId"]);
                        list.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return list;
                }
            }
        }


        public List<SalesList> GetKitchenOrderAllList(int orderStatus, string id)
        {
            List<SalesList> list = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Kitchen_PendingOrderList1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@orderStatus", SqlDbType.Int).Value = orderStatus;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;

                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        list.Add(new SalesList
                        {
                            ItemId = Convert.ToString(rd["ItemId"]),
                            PluName = Convert.ToString(rd["PluName"]),
                            Status = Convert.ToString(rd["Status"]),
                            //tableId = Convert.ToString(rd["tableId"]),
                            //tableName = Convert.ToString(rd["tableName"]),
                            SalesId = Convert.ToString(rd["SalesId"]),
                            QuantityValue = Convert.ToDouble(rd["QuantityValue"]),
                            SalesListId = Convert.ToString(rd["SalesListId"]),
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            Size = Convert.ToString(rd["Size"]),
                            Measurement = Convert.ToString(rd["Measurement"]),
                        });

                    }
                    return list;
                }
            }
        }

        public int UpdateKitcheOrderStatus(Sales info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Kitchen_OrderUpdated]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = info.status;
                    cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar).Value = info.LastUpdatedBy;
                    cmd.Parameters.Add("@salesOrderId", SqlDbType.VarChar).Value = info.SalesOrderId;
                    cmd.Parameters.Add("@SalesListId", SqlDbType.VarChar).Value = info.ItemId;
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public List<SalesList> GetKitchenReadyOrderList(string id)
        {
            List<SalesList> list = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Kitchen_ReadyOrderList1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        list.Add(new SalesList
                        {
                            ItemId = Convert.ToString(rd["ItemId"]),
                            PluName = Convert.ToString(rd["PluName"]),
                            Status = Convert.ToString(rd["Status"]),
                            //tableId = Convert.ToString(rd["tableId"]),
                            //tableName = Convert.ToString(rd["tableName"]),
                            SalesId = Convert.ToString(rd["SalesId"]),
                            QuantityValue = Convert.ToDouble(rd["QuantityValue"]),
                            SalesListId = Convert.ToString(rd["SalesListId"]),
                            Size = Convert.ToString(rd["Size"]),
                            Measurement = Convert.ToString(rd["Measurement"])
                        });

                    }
                    return list;
                }
            }
        }

        public List<SalesList> GetTableStateList(string id)
        {
            List<SalesList> list = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Table_StateListForAllOrders1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hubId", SqlDbType.VarChar).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new SalesList
                        {
                            Id = Convert.ToInt32(rd["id"]),
                            PLU_Count = Convert.ToString(rd["PLU_Count"] == DBNull.Value ? 0 : rd["PLU_Count"]),
                            tableId = Convert.ToString(rd["tableId"] == DBNull.Value ? "NA" : rd["tableId"]),
                            tableName = Convert.ToString(rd["tableName"] == DBNull.Value ? "NA" : rd["tableName"]),
                            SalesOrderId = Convert.ToString(rd["SalesOrderId"] == DBNull.Value ? "NA" : rd["SalesOrderId"]),
                            TotalPrice = Convert.ToDouble(rd["TotalPrice"] == DBNull.Value ? 0.00 : rd["TotalPrice"]),
                            OrderStatus = Convert.ToString(rd["OrderdStatus"] == DBNull.Value ? "Vacant" : rd["OrderdStatus"]),
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"] == DBNull.Value ? "1900-01-01 12:00:00.000" : rd["CreatedOn"]),
                            Duration = Convert.ToString(rd["Duration"] == DBNull.Value ? "00:00:00" : rd["Duration"]),
                            UpdateDuration = Convert.ToString(rd["UpdateDuration"] == DBNull.Value ? "00:00:00" : rd["UpdateDuration"]),
                            ComputedCoupen = rd["DiscountPer"] == DBNull.Value ? "0" : Convert.ToString(rd["DiscountPer"]) + "," + Convert.ToInt32(rd["CoupenId"] == DBNull.Value ? 0 : rd["CoupenId"]) + "," + Convert.ToDouble(rd["MaxDiscount"] == DBNull.Value ? 0 : rd["MaxDiscount"]),
                            CName = Convert.ToString(rd["Name"] == DBNull.Value ? "Guest" : rd["Name"]),
                            PaymentStatus = Convert.ToString(rd["PaymentStatus"] == DBNull.Value ? "NA" : rd["PaymentStatus"]),
                            CoupenId = Convert.ToInt32(rd["CoupenId"] == DBNull.Value ? null : rd["CoupenId"]),
                            KotPrint = Convert.ToInt32(rd["KotPrint"] == DBNull.Value ? 0 : rd["KotPrint"]),
                            BillingAmount = Convert.ToDouble(rd["TaxableAmount"] == DBNull.Value ? 0.0 : rd["TaxableAmount"]),
                        });

                    }
                    return list;
                }
            }
        }

        public List<SalesList> GetTableStateListTKWY(string hubId)
        {
            List<SalesList> list = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Table_StateListTKWY1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hubId", SqlDbType.VarChar).Value = hubId;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {

                            list.Add(new SalesList
                            {
                                Id = Convert.ToInt32(rd["id"]),
                                SalesOrderId = Convert.ToString(rd["SalesOrderId"] == DBNull.Value ? "NA" : rd["SalesOrderId"]),
                                OrderStatus = Convert.ToString(rd["OrderdStatus"] == DBNull.Value ? "Vacant" : rd["OrderdStatus"]),
                                CreatedOn = Convert.ToDateTime(rd["CreatedOn"] == DBNull.Value ? "1900-01-01 12:00:00.000" : rd["CreatedOn"]),
                                Duration = Convert.ToString(rd["Duration"] == DBNull.Value ? "00:00:00" : rd["Duration"]),
                                TotalPrice = Convert.ToDouble(rd["TotalPrice"] == DBNull.Value ? 0.00 : rd["TotalPrice"]),
                                UpdateDuration = Convert.ToString(rd["UpdateDuration"] == DBNull.Value ? "00:00:00" : rd["UpdateDuration"]),
                                CName = Convert.ToString(rd["Name"] == DBNull.Value ? "Guest" : rd["Name"]),
                                PaymentStatus = Convert.ToString(rd["PaymentStatus"] == DBNull.Value ? "NA" : rd["PaymentStatus"]),
                                // KotPrint = Convert.ToInt32(rd["KotPrint"] == DBNull.Value ? 0 : rd["KotPrint"]),
                                TokenNumberOrder = Convert.ToString(rd["TokenLabel"] == DBNull.Value ? "0" : rd["TokenLabel"])
                            });

                        }
                        return list;
                    }

                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        public List<SalesList> GetTableStateListHOD(string hubId)
        {
            List<SalesList> list = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Table_StateListHOD1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hubId", SqlDbType.VarChar).Value = hubId;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {

                            list.Add(new SalesList
                            {
                                Id = Convert.ToInt32(rd["id"]),
                                SalesOrderId = Convert.ToString(rd["SalesOrderId"] == DBNull.Value ? "NA" : rd["SalesOrderId"]),
                                OrderStatus = Convert.ToString(rd["OrderdStatus"] == DBNull.Value ? "Vacant" : rd["OrderdStatus"]),
                                CreatedOn = Convert.ToDateTime(rd["CreatedOn"] == DBNull.Value ? "1900-01-01 12:00:00.000" : rd["CreatedOn"]),
                                Duration = Convert.ToString(rd["Duration"] == DBNull.Value ? "00:00:00" : rd["Duration"]),
                                TotalPrice = Convert.ToDouble(rd["TotalPrice"] == DBNull.Value ? 0.00 : rd["TotalPrice"]),
                                UpdateDuration = Convert.ToString(rd["UpdateDuration"] == DBNull.Value ? "00:00:00" : rd["UpdateDuration"]),
                                CName = Convert.ToString(rd["Name"] == DBNull.Value ? "Guest" : rd["Name"]),
                                PaymentStatus = Convert.ToString(rd["PaymentStatus"] == DBNull.Value ? "NA" : rd["PaymentStatus"]),
                                KotPrint = Convert.ToInt32(rd["KotPrint"] == DBNull.Value ? 0 : rd["KotPrint"]),
                                TokenNumberOrder = Convert.ToString(rd["TokenLabel"] == DBNull.Value ? "0" : rd["TokenLabel"])
                            });

                        }
                        return list;
                    }

                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }



        public int UpdateOrderdStatus(string id1, string LastUpdatedBy, string Orderstatus)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_UpdateOrderdStatus]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 100).Value = id1;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = LastUpdatedBy;
                cmd.Parameters.Add("@OrderdStatus", SqlDbType.VarChar, 100).Value = Orderstatus;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<SelectListItem> GetMainCategoryList(string id)
        {
            List<SelectListItem> GetCategorySelectList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetMainCategoryListsales]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId",SqlDbType.VarChar, 30).Value = id;
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


        public List<Item> GetMainList(string id)
        {
            List<Item> list = new List<Item>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetMainCategoryListsalespage]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 30).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        list.Add(new Item
                        {
                            MaincatId = Convert.ToString(rd["MainCategoryId"]),
                            Name = Convert.ToString(rd["Name"]),
                        });

                    }
                    return list;
                }
            }
        }

        public List<Item> GetCatgList(string id)
        {
            List<Item> list1 = new List<Item>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetCategoryListItemsales]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId",SqlDbType.VarChar,30).Value=id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        list1.Add(new Item
                        {
                            categoryId = Convert.ToString(rd["CategoryId"]),
                            Category = Convert.ToString(rd["Name"]),
                            MainCategory = Convert.ToString(rd["MainCategoryId"]),
                        });

                    }
                    return list1;
                }
            }
        }



       public int CloseOrder(SalesList Info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_UpdateOrderdStatus]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OrderdStatus", SqlDbType.VarChar, 100).Value = Info.OrderdStatus;
                cmd.Parameters.Add("@CancellationReason", SqlDbType.VarChar, 100).Value = Info.CancellationReason;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = Info.LastUpdatedBy;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 100).Value = Info.SalesId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int UpdateAllKitcheOrderStatus(Sales info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_KitchenOrderUpdateList]", con))
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("SalesId", typeof(string));
                    odt.Columns.Add("SalesListId", typeof(string));
                    if (info.salesId != null)
                        for (int i = 0; i < info.salesId.Count; i++)
                        {
                            var salesId = info.salesId[i];
                            var salesListId = info.salesListId[i];
                            odt.Rows.Add(salesId, salesListId);
                        }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@saleslist", SqlDbType.Structured).Value = odt;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = info.status;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = info.LastUpdatedBy;
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public async Task<List<Item>> GetallItemListData(string mainCategory, string condition, string hubId = null, string ItemName = null)
        {
            var itemList = new List<Item>();
            ItemName = ItemName == null ? "0" : ItemName;
            mainCategory = mainCategory == null ? "0" : mainCategory;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[GetSalesOrderItemListData]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Hub", SqlDbType.VarChar).Value = string.IsNullOrEmpty(hubId) ? (object)DBNull.Value : hubId;
                cmd.Parameters.Add("@Condition", SqlDbType.VarChar).Value = condition;

                if (!string.IsNullOrEmpty(ItemName))
                    cmd.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = ItemName;
                if (!string.IsNullOrEmpty(mainCategory))
                {
                    cmd.Parameters.Add("@mainCategory", SqlDbType.VarChar).Value = mainCategory;
                }
                con.Open();
                try
                {
                    DataSet dt = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    DataTable dt1 = new DataTable();
                    dt1 = dt.Tables[0];

                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        while (rd.Read())
                        {

                            itemList.Add(new Item
                            {
                                PluCode = Convert.ToString(rd["PluCode"] == DBNull.Value ? null : Convert.ToString(rd["PluCode"])),
                                PluName = Convert.ToString(rd["PluName"] == DBNull.Value ? null : Convert.ToString(rd["PluName"])),
                                ItemId = Convert.ToString(rd["ItemId"] == DBNull.Value ? null : Convert.ToString(rd["ItemId"])),
                                Measurement = Convert.ToString(rd["Measurement"] == DBNull.Value ? null : Convert.ToString(rd["Measurement"])),
                                Weight = Convert.ToDouble(rd["Weight"] == DBNull.Value ? null : Convert.ToString(rd["Weight"])),
                                Category = Convert.ToString(rd["Category"] == DBNull.Value ? null : Convert.ToString(rd["Category"])),
                                Approval = Convert.ToString(rd["Approval"] == DBNull.Value ? null : Convert.ToString(rd["Approval"])),
                                seasonSale = Convert.ToString(rd["seasonSale"] == DBNull.Value ? null : Convert.ToString(rd["seasonSale"])),
                                ItemSellingType = Convert.ToString(rd["ItemSellingType"] == DBNull.Value ? null : Convert.ToString(rd["ItemSellingType"])),
                                TotalStock = rd["TotalStock"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["TotalStock"]),
                                MaxQuantityAllowed = Convert.ToInt32(rd["MaxQuantityAllowed"] == DBNull.Value ? null : Convert.ToString(rd["MaxQuantityAllowed"])),
                                SellingPrice = Convert.ToDouble(rd["SellingPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["SellingPrice"])),
                                MarketPrice = Convert.ToDouble(rd["MarketPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["MarketPrice"])),
                                MainCategory = Convert.ToString(rd["MainCategory"] == DBNull.Value ? "NA" : Convert.ToString(rd["MainCategory"])),
                                DiscountPerctg = rd["DiscountedPrice"] == DBNull.Value ? (double?)null : Convert.ToDouble(rd["DiscountedPrice"]),
                                ImagePath = "https://freshlo.oss-ap-south-1.aliyuncs.com/freshpos/icon/" + Convert.ToString(rd["ItemId"]) + ".png",
                            });
                        }

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return itemList;
        }

        public async Task<TableInfo> GetTableDetailsAsync(int tableId,string hubId)
        {
            TableInfo data = new TableInfo();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Table_GetTableDetails1]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = tableId;
                        cmd.Parameters.AddWithValue("@hubId", SqlDbType.Int).Value = hubId;
                        con.Open();
                        using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                        {
                            if (rd.Read())
                            {
                                data = new TableInfo
                                {
                                    id = Convert.ToInt32(rd["id"]),
                                    tableId = Convert.ToString(rd["tableId"]),
                                    tableName = Convert.ToString(rd["tableName"]),
                                    branch = Convert.ToString(rd["branch"])
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

        public List<SalesList> GetKotListView(string Id,string hubId)
        {
            List<SalesList> KotList = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_KOTListView1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 100).Value = Id;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 100).Value = hubId;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        KotList.Add(new SalesList
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            SalesOrderId = Convert.ToString(rd["SalesOrderId"]),
                            KitchListId = Convert.ToString(rd["KitchListId"]),
                            CustomerId = Convert.ToString(rd["CustomerId"]),
                            ItemId = Convert.ToString(rd["ItemId"]),
                            QuantityValue = Convert.ToDouble(rd["TotalQty"]),
                            PluName = Convert.ToString(rd["PluName"]),
                            status = Convert.ToString(rd["Status"] == DBNull.Value ? "Pending" : rd["Status"]),
                            CreatedBy = rd["CreatedBy"] as string,
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            KOT_Print = Convert.ToInt32(rd["KOT_Print"] == DBNull.Value ? 0 : rd["KOT_Print"]),
                            KOT_PrintDesc = Convert.ToString(rd["KOT_PrintDesc"] == DBNull.Value ? "NULL" : rd["KOT_PrintDesc"]),
                            KOT_Status = Convert.ToInt32(rd["KOT_Status"] == DBNull.Value ? 0 : rd["KOT_Status"]),
                            SalesListId = Convert.ToString(rd["SalesListId"] == DBNull.Value ? "" : rd["SalesListId"]),
                            Size = Convert.ToString(rd["Size"] == DBNull.Value ? "" : rd["Size"]),
                        });
                    }
                    return KotList;
                }
            }
        }

        public int UpdateAllKitcheOrderKOTPrintStatus(Sales info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_KitchenKOTPrintUpdateListReady]", con))
                    {
                        con.Open();
                        DataTable odt = new DataTable();
                        odt.Columns.Add("SalesOrderId", typeof(string));
                        odt.Columns.Add("SalesListId", typeof(string));
                        odt.Columns.Add("KitchListId", typeof(string));
                        if (info.salesId != null)
                            for (int i = 0; i < info.salesId.Count; i++)
                            {
                                var salesId = info.salesId[i];
                                var salesListId = info.salesListId[i];
                                var kitchenlistId = info.kitChenListId[i];
                                odt.Rows.Add(salesId, salesListId, kitchenlistId);
                            }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@saleslist", SqlDbType.Structured).Value = odt;
                        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = info.KOT_Status;
                        cmd.Parameters.Add("@ItemStatus", SqlDbType.VarChar).Value = info.status;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = info.LastUpdatedBy;
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        if (dataReader.Read())
                        {
                            return Convert.ToInt32(dataReader[0].ToString());
                        }
                        else
                        {
                            return 0;
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public async Task<List<PrintSalesList>> GetSalesListKOTForPrint(string SalesId, string SalesKitchenId,string hubId)
        {
            List<PrintSalesList> salesList = new List<PrintSalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Sales_GetKOTPrintData1]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SaleId", SqlDbType.VarChar).Value = SalesId;
                cmd.Parameters.Add("@SaleKitchenId", SqlDbType.VarChar).Value = SalesKitchenId;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = hubId;
                con.Open();
                try
                {
                    using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (dataReader.Read())
                        {
                            salesList.Add(new PrintSalesList
                            {
                                ItemId = Convert.ToString(dataReader["ItemId"]),
                                PluName = Convert.ToString(dataReader["PluName"]),
                                QuantityValue = Convert.ToString(dataReader["QuantityValue"]),
                                ItemType = Convert.ToString(dataReader["ItemType"]),
                                KOT_Print = Convert.ToInt32(dataReader["KOT_Print"] == DBNull.Value ? -1 : dataReader["KOT_Print"]),
                                Size = Convert.ToString(dataReader["Size"] == DBNull.Value ? "" : dataReader["Size"]),
                                Remark = Convert.ToString(dataReader["Remark"] == DBNull.Value ? "" : dataReader["Remark"]),
                            });
                        }
                        return salesList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }

        }
        public async Task<List<PrintSalesList>> GetSalesListAllTblKOTForPrint(string SalesId, string SalesKitchenId,string hubId)
        {
            List<PrintSalesList> salesList = new List<PrintSalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Sales_GetALLKOTPrintData1]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SaleId", SqlDbType.VarChar).Value = SalesId;
                cmd.Parameters.Add("@SaleKitchenId", SqlDbType.VarChar).Value = SalesKitchenId;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = hubId;
              
                con.Open();
                try
                {
                    using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (dataReader.Read())
                        {
                            salesList.Add(new PrintSalesList
                            {
                                ItemId = Convert.ToString(dataReader["ItemId"]),
                                PluName = Convert.ToString(dataReader["PluName"]),
                                QuantityValue = Convert.ToString(dataReader["QuantityValue"]),
                                ItemType = Convert.ToString(dataReader["ItemType"]),
                                KOT_Print = Convert.ToInt32(dataReader["KOT_Print"] == DBNull.Value ? -1 : dataReader["KOT_Print"]),
                                Name = Convert.ToString(dataReader["Name"] == DBNull.Value ? "Not Available" : dataReader["Name"]),
                                tableName = Convert.ToString(dataReader["tableName"] == DBNull.Value ? "Not Available" : dataReader["tableName"]),
                                SalesOrderId = Convert.ToString(dataReader["SalesOrderId"] == DBNull.Value ? "S001" : dataReader["SalesOrderId"])
                            });
                        }
                        return salesList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }

        }
        public async Task<List<PrintSalesList>> GetSaleslistPrintDetails(string SalesId)
        {
            List<PrintSalesList> salesList = new List<PrintSalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Sales_GetALLKOTPrintSalesDetailsData]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SaleId", SqlDbType.VarChar).Value = SalesId;
                con.Open();
                try
                {
                    using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (dataReader.Read())
                        {
                            salesList.Add(new PrintSalesList
                            {
                                Name = Convert.ToString(dataReader["Name"] == DBNull.Value ? "Not Available" : dataReader["Name"]),
                                tableName = Convert.ToString(dataReader["tableName"] == DBNull.Value ? "Not Available" : dataReader["tableName"]),
                                SalesOrderId = Convert.ToString(dataReader["SalesOrderId"] == DBNull.Value ? "S001" : dataReader["SalesOrderId"]),
                                LastUpdateOn = Convert.ToDateTime(dataReader["LastUpdatedOn"]),

                            });
                        }
                        return salesList;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }
        public async Task<List<PrintSalesList>> GetSalesListConsolidateTblKOTForPrint(string SalesId, string SalesKitchenId)
        {
            List<PrintSalesList> salesList = new List<PrintSalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Sales_GetConsolidateKOTPrintData]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SaleId", SqlDbType.VarChar).Value = SalesId;
                cmd.Parameters.Add("@SaleKitchenId", SqlDbType.VarChar).Value = SalesKitchenId;
                con.Open();
                try
                {
                    using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (dataReader.Read())
                        {
                            salesList.Add(new PrintSalesList
                            {
                                PluName = Convert.ToString(dataReader["PluName"]),
                                ItemType = Convert.ToString(dataReader["ItemType"]),
                                QuantityValue = Convert.ToString(dataReader["QuantityValue"]),
                                KOT_Print = Convert.ToInt32(dataReader["KOT_Print"] == DBNull.Value ? -1 : dataReader["KOT_Print"]),
                            });
                        }
                        return salesList;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public int RemoveSalesKitchenList(string SalesOrderId, string LastUpdatedBy, string SalesId, string KitchenListId)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_RemoveKitchenSalesOrderList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SalesId", SqlDbType.VarChar, 100).Value = SalesOrderId;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = LastUpdatedBy;
                cmd.Parameters.Add("@SalesListId", SqlDbType.VarChar, 100).Value = SalesId;
                cmd.Parameters.Add("@KitchenListId", SqlDbType.VarChar, 50).Value = KitchenListId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

        }

        public List<SalesList> GetPendingKotList()
        {
            List<SalesList> list = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_PendingKOTView]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        list.Add(new SalesList
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            SalesOrderId = Convert.ToString(rd["SalesOrderId"]),
                            KitchListId = Convert.ToString(rd["KitchListId"]),
                            CustomerId = Convert.ToString(rd["CustomerId"]),
                            ItemId = Convert.ToString(rd["ItemId"]),
                            QuantityValue = Convert.ToDouble(rd["TotalQty"]),
                            PluName = Convert.ToString(rd["PluName"]),
                            status = Convert.ToString(rd["Status"] == DBNull.Value ? "Pending" : rd["Status"]),
                            CreatedBy = rd["CreatedBy"] as string,
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            KOT_Print = Convert.ToInt32(rd["KOT_Print"] == DBNull.Value ? 0 : rd["KOT_Print"]),
                            KOT_PrintDesc = Convert.ToString(rd["KOT_PrintDesc"] == DBNull.Value ? "NULL" : rd["KOT_PrintDesc"]),
                            KOT_Status = Convert.ToInt32(rd["KOT_Status"] == DBNull.Value ? 0 : rd["KOT_Status"]),
                            SalesListId = Convert.ToString(rd["SalesListId"] == DBNull.Value ? "" : rd["SalesListId"])
                        });

                    }
                    return list;
                }
            }
        }

        public List<SalesList> GetItemListView(string Id,string hubId)
        {
            List<SalesList> KotList = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_GetItemsViews1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 100).Value = Id;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 100).Value = hubId;

                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        KotList.Add(new SalesList
                        {
                            KitchListId = Convert.ToString(rd["KitchListId"]),
                            QuantityValue = Convert.ToDouble(rd["TotalQty"]),
                            PluName = Convert.ToString(rd["PluName"]),
                            status = Convert.ToString(rd["Status"] == DBNull.Value ? "Pending" : rd["Status"]),
                            KOT_Status = Convert.ToInt32(rd["KOT_Status"] == DBNull.Value ? 0 : rd["KOT_Status"]),
                            Duration = Convert.ToString(rd["Duration"] == DBNull.Value ? "" : rd["Duration"]),
                            UpdateDuration = Convert.ToString(rd["UpdateDuration"] == DBNull.Value ? "" : rd["UpdateDuration"]),
                            Size = Convert.ToString(rd["Size"] == DBNull.Value ? "" : rd["Size"]),

                        });
                    }
                    return KotList;
                }
            }
        }
        public List<SalesList> AllGetItemListView(string id)
        {
            List<SalesList> KotList = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_AllTblGetItemsViews1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId",SqlDbType.VarChar ,30).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        KotList.Add(new SalesList
                        {
                            tableName = Convert.ToString(rd["TableName"]),
                            QuantityValue = Convert.ToDouble(rd["TotalQty"]),
                            PluName = Convert.ToString(rd["PluName"]),
                            Size = Convert.ToString(rd["Size"]),
                            Measurement = Convert.ToString(rd["Measurement"]),
                            status = Convert.ToString(rd["Status"] == DBNull.Value ? "Pending" : rd["Status"]),
                            KOT_Status = Convert.ToInt32(rd["KOT_Status"] == DBNull.Value ? 0 : rd["KOT_Status"]),
                            Duration = Convert.ToString(rd["Duration"] == DBNull.Value ? "" : rd["Duration"]),
                            UpdateDuration = Convert.ToString(rd["UpdateDuration"] == DBNull.Value ? "" : rd["UpdateDuration"]),
                            SalesId = Convert.ToString(rd["SalesOrderId"] == DBNull.Value ? "" : rd["SalesOrderId"]),
                            SalesListId = Convert.ToString(rd["SalesListId"] == DBNull.Value ? "" : rd["SalesListId"]),

                        });
                    }
                    return KotList;
                }
            }
        }

        public List<ItemSizeInfo> GetallItemVarianceList(string hubId)
        {
            List<ItemSizeInfo> ItemList = new List<ItemSizeInfo>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Item_ItemVariancePriceDetails]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@HubId", SqlDbType.VarChar, 30).Value = hubId;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        ItemList.Add(new ItemSizeInfo
                        {
                            PId = Convert.ToInt32(rd["Id"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Id"])),
                            PriceId = Convert.ToString(rd["PriceId"] == DBNull.Value ? null : Convert.ToString(rd["PriceId"])),
                            ItemId = Convert.ToString(rd["ItemId"] == DBNull.Value ? null : Convert.ToString(rd["ItemId"])),
                            PluName = Convert.ToString(rd["PluName"] == DBNull.Value ? null : Convert.ToString(rd["PluName"])),
                            Measurement = Convert.ToString(rd["Measurement"] == DBNull.Value ? null : Convert.ToString(rd["Measurement"])),
                            OrderQty = Convert.ToSingle(rd["OrderQty"] == DBNull.Value ? 1 : Convert.ToSingle(rd["OrderQty"])),
                            Size = Convert.ToString(rd["Size"] == DBNull.Value ? "Ut" : Convert.ToString(rd["Size"])),
                            ItmNetWeight = Convert.ToSingle(rd["ItmNetWeight"] == DBNull.Value ? 1 : Convert.ToSingle(rd["ItmNetWeight"])),
                            SellingPrice = Convert.ToDouble(rd["SellingPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["SellingPrice"])),
                            MarketPrice = Convert.ToDouble(rd["MarketPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["MarketPrice"])),
                            ActualCost = Convert.ToDouble(rd["TotalPrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["TotalPrice"])),
                            Purchaseprice = Convert.ToDouble(rd["PurchasePrice"] == DBNull.Value ? 0 : Convert.ToDouble(rd["PurchasePrice"]))
                        });
                    }
                    return ItemList;
                }
            }
        }

        public int ReadyOrder(SalesList Info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sales_UpdateOrderdStatus]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OrderdStatus", SqlDbType.VarChar, 100).Value = Info.OrderdStatus;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 100).Value = Info.LastUpdatedBy;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 100).Value = Info.SalesId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public BusinessInfo GetBusinessInfoDetails(int id)
        {
            BusinessInfo details = new BusinessInfo();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[freshPos_mst].[dbo].[usp_AnnouncmentTextHeader]", con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@type", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@hotel_id", SqlDbType.Int).Value = 3;
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            details = new BusinessInfo()
                            {
                                hotel_id = Convert.ToInt32(dr["hotel_id"]),
                                AnnouncemntMessage = Convert.ToString(dr["banner_text"])
                            };
                        }
                    }
                }
            }
            return details;
        }

        public int EditAnnouncementMessage(BusinessInfo add)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[freshPos_mst].[dbo].[usp_AnnouncmentTextHeader]", con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@type", SqlDbType.Int).Value = 2;
                    cmd.Parameters.Add("@hotel_id", SqlDbType.Int).Value = 3;
                    cmd.Parameters.Add("@banner_text", SqlDbType.VarChar, -1).Value = add.AnnouncemntMessage;
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Item> GetCategorylist(string MainCatId,string id)
        {
            List<Item> list1 = new List<Item>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetCategoryListItemFilterByMainCategory]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MainCategoryId", SqlDbType.VarChar, 50).Value = MainCatId;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        list1.Add(new Item
                        {
                            categoryId = Convert.ToString(rd["CategoryId"]),
                            Category = Convert.ToString(rd["Name"]),
                            MainCategory = Convert.ToString(rd["MainCategoryId"]),
                        });

                    }
                    return list1;
                }
            }

        }

        public int CreateCustomer(TableInfo info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_AddCustomerInfoForBooking]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = info.custName == null ? "NA" : info.custName;
                    cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = info.custEmail == null ? "Test@gmail.com" : info.custEmail;
                    cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar, 30).Value = info.custNumber == null ? (object)DBNull.Value : info.custNumber;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 10).Value = info.created_by == null ? (object)DBNull.Value : info.created_by;
                    cmd.Parameters.Add("@Source", SqlDbType.VarChar, 10).Value = info.source == null ? "Web" : info.source;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public int CreateBooking(TableInfo info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_AddBookingDetails]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@custId", SqlDbType.VarChar, 50).Value = info.custId == null ? "NA" : info.custId;
                    cmd.Parameters.Add("@peferences", SqlDbType.VarChar, 30).Value = info.perferencType == null ? (object)DBNull.Value : info.perferencType;
                    cmd.Parameters.Add("@totalGuest", SqlDbType.Int).Value = info.totalGuest == 0 ? 1 : info.totalGuest;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 10).Value = info.created_by == null ? (object)DBNull.Value : info.created_by;
                    cmd.Parameters.Add("@slotTime", SqlDbType.VarChar, 50).Value = info.slotTime == null ? "" : info.slotTime;
                    cmd.Parameters.Add("@timeType", SqlDbType.VarChar, 30).Value = info.timeType == null ? "" : info.timeType;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public List<TableInfo> Getalltblbookinglist()
        {
            List<TableInfo> bookinglist = new List<TableInfo>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_bookinglist]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            bookinglist.Add(new TableInfo
                            {
                                id = Convert.ToInt32(rd["Id"] == DBNull.Value ? 000 : rd["Id"]),
                                bookingId = Convert.ToString(rd["bookingId"] == DBNull.Value ? "" : rd["bookingId"]),
                                custId = Convert.ToString(rd["CustomerId"] == DBNull.Value ? "" : rd["CustomerId"]),
                                custName = Convert.ToString(rd["Name"] == DBNull.Value ? "" : rd["Name"]),
                                custNumber = Convert.ToString(rd["ContactNo"] == DBNull.Value ? "" : rd["ContactNo"]),
                                tblPerfernce = Convert.ToString(rd["Perferencetbl"] == DBNull.Value ? "" : rd["Perferencetbl"]),
                                totalGuest = Convert.ToInt32(rd["Guestno"] == DBNull.Value ? "NOT Available" : rd["Guestno"]),
                                created_on = Convert.ToDateTime(rd["CreatedOn"] == DBNull.Value ? null : rd["CreatedOn"]),
                                created_by = Convert.ToString(rd["CreatedBy"]),
                                status = Convert.ToString(rd["bookingStatus"] == DBNull.Value ? "" : rd["bookingStatus"]),
                                slotTime = Convert.ToString(rd["slotTime"] == DBNull.Value ? "" : rd["slotTime"]),
                                timeType = Convert.ToString(rd["timeType"] == DBNull.Value ? "" : rd["timeType"]),
                                perferencType = Convert.ToString(rd["perfernecs"] == DBNull.Value ? "" : rd["perfernecs"]),
                            });
                        }
                        return bookinglist;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public TableInfo GetbookingCount()
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                TableInfo bookingcount = new TableInfo();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_GetbookingCount]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (sdr.Read())
                            {

                                bookingcount = new TableInfo
                                {
                                    BookingCount = Convert.ToInt32(sdr["BookingCount"]),
                                    WaitingStatus = Convert.ToInt32(sdr["WaitingStatus"]),
                                    AdvanceStatus = Convert.ToInt32(sdr["AdvanceStatus"])
                                };
                            }
                            return bookingcount;
                        }
                        catch (Exception e)
                        {

                            throw;
                        }
                    }
                }
            }
        }

        public List<SelectListItem> Gettableperferenclist(string id)
        {
            List<SelectListItem> getTablelist = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_bookingtblPerferenceslit]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 5).Value = id;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            string text = Convert.ToString(sdr["tableName"]);
                            string Id = Convert.ToString(sdr["tableId"]);
                            getTablelist.Add(new SelectListItem()
                            {
                                Text = text,
                                Value = Id,
                            });
                        }
                        return getTablelist;
                    }
                }
            }
        }

        public string InserttblBook(Sales dicData, string createdBy, string branch)
        {
            var result = "";
            dicData.PaymentStatus = string.IsNullOrEmpty(dicData.PaymentStatus) ? "NA" : dicData.PaymentStatus;
            dicData.PaymentMode = string.IsNullOrEmpty(dicData.PaymentMode) ? "NA" : dicData.PaymentMode;
            dicData.SalesPerson = string.IsNullOrEmpty(dicData.SalesPerson) ? "NA" : dicData.SalesPerson;
            dicData.status = "Pending";
            using (SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        sqlcon.Open();
                        cmd.Connection = sqlcon;
                        cmd.CommandText = "[dbo].[AddtblBookingAssingment]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@deliverydate", dicData.DeliveryDate);
                        cmd.Parameters.AddWithValue("@customerId", dicData.CustomerId);
                        cmd.Parameters.AddWithValue("@salesPerson", dicData.SalesPerson);
                        cmd.Parameters.AddWithValue("@pulCount", dicData.PLU_Count);
                        cmd.Parameters.AddWithValue("@quantity", dicData.TotalQuantity);
                        cmd.Parameters.AddWithValue("@orderStatus", dicData.OrderdStatus);
                        cmd.Parameters.AddWithValue("@paymentStatus", dicData.PaymentStatus);
                        cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                        cmd.Parameters.AddWithValue("@PaymentMode", dicData.PaymentMode);
                        cmd.Parameters.AddWithValue("@SlotId", dicData.SlotId);
                        cmd.Parameters.AddWithValue("@addId", dicData.AddressId);
                        cmd.Parameters.AddWithValue("@hub", branch);
                        cmd.Parameters.AddWithValue("@Branch", branch);
                        cmd.Parameters.AddWithValue("@PurchaseId", "NA");
                        cmd.Parameters.Add("@DeliveryCharges", SqlDbType.VarChar, 100).Value = dicData.DeliveryCharges;
                        cmd.Parameters.Add("@Discount", SqlDbType.Float).Value = dicData.ActualDiscAmt;
                        cmd.Parameters.Add("@DeliveryType", SqlDbType.VarChar, 100).Value = dicData.DeliveryType == null ? "Waliking" : dicData.DeliveryType;
                        cmd.Parameters.Add("@totalCost", SqlDbType.Float).Value = dicData.TotaldisAmt;
                        cmd.Parameters.Add("@Remaining_Amount", SqlDbType.Float).Value = dicData.PaymentStatus == "Partially" ? dicData.Remaining_Amount : 0;
                        cmd.Parameters.Add("@CoupenId", SqlDbType.Int).Value = dicData.coupenId;
                        cmd.Parameters.Add("@bookingId", SqlDbType.VarChar, 50).Value = dicData.bookingIds;
                        cmd.Parameters.AddWithValue("@tableId", dicData.tableId).Value = dicData.tableId == null ? "NA" : dicData.tableId; ;
                        cmd.Parameters.AddWithValue("@bags", dicData.tableId).Value = dicData.Bags == null ? "NA" : dicData.Bags;
                        cmd.Parameters.AddWithValue("@OrderType", dicData.OrderType).Value = dicData.OrderType == null ? "HOD" : dicData.OrderType;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = Convert.ToString(reader[0]);
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        return result;
                    }
                    finally
                    {
                        if (sqlcon.State != ConnectionState.Closed)
                        {
                            sqlcon.Close();
                            sqlcon.Dispose();
                        }
                    }
                }
            }
            return result;
        }

        public int CancelBooking(Sales ids)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[usp_CancelBooking]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ids.Id;
                        cmd.Parameters.Add("@LastupdatedBy", SqlDbType.VarChar, 100).Value = ids.LastUpdatedBy;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }

        public SalesCountData GetOrderNotificationCount()
        {
            SalesCountData salesCountData = new SalesCountData();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) as TotalCount from SaleOrderss where OrderdStatus='Ordered' and Source='Mob' and Comment !='viewed'", con))
                {
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (sdr.Read())
                            {
                                salesCountData = new SalesCountData()
                                {
                                    NotifyOrderCount = Convert.ToInt32(sdr["TotalCount"]),
                                };
                            };
                            return salesCountData;
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                }
            }
        }

        public List<SalesDetail> GetOrderdetails(string id)
        {
            List<SalesDetail> Orderdtl = new List<SalesDetail>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                var query = "Select so.SalesOrderId,c.Name as CustomerName,c.ContactNo,so.TotalPrice from SaleOrderss so inner join tblCustomers c on so.CustomerId = c.CustomerId where  so.Source = 'Mob' and so.OrderdStatus = 'Ordered' and so.Comment != 'viewed'  order by so.Id desc";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
         
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Orderdtl.Add(new SalesDetail()
                            {
                                SalesOrderId = Convert.ToString(sdr["SalesOrderId"]),
                                CustomerName = Convert.ToString(sdr["CustomerName"]),
                                CustomerNo = Convert.ToString(sdr["ContactNo"]),
                                TotalAmount = Convert.ToDouble(sdr["TotalPrice"]),
                            });
                        }
                    }
                }
                return Orderdtl;
            }
        }

        public int UpdateNotificationCount(string SalesOrderId, string type)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_UpdateNotificationCount]", con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 50).Value = SalesOrderId;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = type;
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        //public int UpdateNotificationCount()
        //{
        //    using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
        //    {
        //        try
        //        {
        //            var query = "Update SaleOrderss Set Comment='viewed' Where Source='Mob' and OrderdStatus = 'Ordered' and CAST (CreatedOn as DATE) = CAST(GETDATE() AS DATE) ";
        //            using (SqlCommand cmd = new SqlCommand(query))
        //            {
        //                cmd.Connection = con;
        //                con.Open();
        //                return cmd.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}

        public List<KotLogs> GetKotLogsView(string Id)
        {
            try
            {
                List<KotLogs> KotLogs = new List<KotLogs>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_GetKotLogbyId]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 50).Value = Id;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            KotLogs.Add(new KotLogs
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                TotalQuantity = Convert.ToInt32(rd["Quantity"]),
                                KOTID = Convert.ToString(rd["KOTID"]),
                                CreatedOn = Convert.ToDateTime(rd["CreatedDate"]),
                                SalesOrderId = Convert.ToString(rd["SalesOrderId"]),
                                CounterKot = Convert.ToInt32(rd["CounterKot"]),
                            });
                        }
                    }
                }
                return KotLogs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int KotReprint(string KOTID, string SalesId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_KotReprint]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar, 50).Value = SalesId;
                    cmd.Parameters.Add("@KOTID", SqlDbType.VarChar, 50).Value = KOTID;
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {



                throw;
            }
        }
        public async Task<List<PrintSalesList>> GetKOTListKOTForPrint(string kotId, string SalesId)
        {
            List<PrintSalesList> salesList = new List<PrintSalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "usp_KotReprintList";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KOTID", SqlDbType.VarChar).Value = kotId;
                cmd.Parameters.Add("@SalesOrderId", SqlDbType.VarChar).Value = SalesId;
                con.Open();
                try
                {
                    using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (dataReader.Read())
                        {
                            salesList.Add(new PrintSalesList
                            {
                                ItemId = Convert.ToString(dataReader["ItemId"]),
                                PluName = Convert.ToString(dataReader["PluName"]),
                                QuantityValue = Convert.ToString(dataReader["QuantityValue"]),
                                ItemType = Convert.ToString(dataReader["ItemType"]),
                                KOT_Print = Convert.ToInt32(dataReader["KOT_Print"] == DBNull.Value ? -1 : dataReader["KOT_Print"]),
                                Size = Convert.ToString(dataReader["Size"] == DBNull.Value ? "" : dataReader["Size"]),
                                Remark = Convert.ToString(dataReader["Remark"] == DBNull.Value ? "" : dataReader["Remark"]),

                            });
                        }
                        return salesList;
                    }



                }
                catch (Exception ex)
                {



                    throw;
                }
            }
        }

        public List<SalesList> get_OrderTracking(string salesId)
        {
            List<SalesList> OrderTracking = new List<SalesList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_OrderStatus]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = salesId;
              
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            OrderTracking.Add(new SalesList
                            {
                                
                                SalesId = Convert.ToString(rd["soId"] == DBNull.Value ? "" : rd["soId"]),
                                Id = Convert.ToInt32(rd["id"] == DBNull.Value ? 0 : rd["id"]),
                                StatusId = Convert.ToInt32(rd["statusId"] == DBNull.Value ? 0 : rd["statusId"]),
                                CreatedOn = Convert.ToDateTime(rd["createdDate"] == DBNull.Value ? "" : rd["createdDate"]),
                                CreatedBy = Convert.ToString(rd["createdBy"] == DBNull.Value ? "NA" : rd["createdBy"]),
                                status = Convert.ToString(rd["stts_desc"] == DBNull.Value ? "Pending" : rd["stts_desc"]),
                                Remark = Convert.ToString(rd["transactionRemark"] == DBNull.Value ? "NA" : rd["transactionRemark"]),                                                                              
                            });
                        }
                        return OrderTracking;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public int AssignShipped(Sales SalesOrderData)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Usp_update_Track_order]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SaleId", SqlDbType.VarChar, 100).Value = SalesOrderData.SalesOrderId;
                cmd.Parameters.Add("@empid", SqlDbType.VarChar, 100).Value = SalesOrderData.CreatedBy;
                cmd.Parameters.Add("@AWB", SqlDbType.VarChar, 250).Value = SalesOrderData.AWBnumber;
                cmd.Parameters.Add("@awbship", SqlDbType.VarChar, -1).Value = SalesOrderData.AWBshippedlink;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}


