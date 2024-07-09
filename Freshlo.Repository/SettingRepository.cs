using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using Freshlo.DomainEntities.Inventory;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Freshlo.Repository
{
    public class SettingRepository : ISettingRI
    {
        public SettingRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        private IDbConfig _dbConfig { get; }
        // zip related here...
        public List<SelectListItem> GetHubList()
        {
            List<SelectListItem> getHublist = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Hub_GetDistinct]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["HubName"]);
                        string Id = Convert.ToString(rd["HubId"]);
                        getHublist.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return getHublist;
                }
            }
        }
        public List<SelectListItem> GettblPerferncelist()
        {
            List<SelectListItem> tblperferencelist = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Select top 2 * from tbl_Bookingtype ", con))
            {
                cmd.CommandType = CommandType.Text;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["name"]);
                        string Id = Convert.ToString(rd["Id"]);
                        tblperferencelist.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return tblperferencelist;
                }
            }
        }
        public List<CustomersAddress> GetZipList(string id)
        {
            List<CustomersAddress> list = new List<CustomersAddress>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetHub_Ziplist]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hubId", SqlDbType.Int).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new CustomersAddress
                        {
                            id = Convert.ToInt32(rd["Id"]),
                            HubName = Convert.ToString(rd["HubName"]),
                            Hub = Convert.ToString(rd["HubId"]),
                            ZipCode = Convert.ToString(rd["Zipcode"]),
                            StandradDeleiveryCharges = Convert.ToSingle(rd["DeliveryAmount"] == DBNull.Value ? 0 : rd["DeliveryAmount"]),
                            ExpressDeleiveryCharges = Convert.ToSingle(rd["ExpressDeliveryAmount"] == DBNull.Value ? 0 : rd["ExpressDeliveryAmount"]),
                            MinOrderValue = Convert.ToSingle(rd["AmountLimit"] == DBNull.Value ? 0 : rd["AmountLimit"]),
                            Discription = Convert.ToString(rd["Description"] == DBNull.Value ? "NA" : rd["Description"]),
                        });
                    }
                    return list;
                }
            }
        }
        public List<CustomersAddress> Getpaymentsgatewaylist(string id)
        {
            List<CustomersAddress> list = new List<CustomersAddress>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_Gettbl_PaymentGetway_list]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hubId", SqlDbType.Int).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new CustomersAddress
                        {
                            id = Convert.ToInt32(rd["Id"]),
                            ChannelId = Convert.ToString(rd["Channel_Id"]),
                            Hub = Convert.ToString(rd["hubId"]),
                            CreatedBy = Convert.ToString(rd["CreatedBy"]),
                            SecretKey = Convert.ToString(rd["Secret_Key"]),
                            PublicKey = Convert.ToString(rd["Public_Key"] == DBNull.Value ? "NA" : rd["Public_Key"]),
                        });
                    }
                    return list;
                }
            }
        }
        public List<Recipe> GetRecipeList(string id)
        {
            List<Recipe> list = new List<Recipe>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_Recipe_Management]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@type", SqlDbType.Int).Value = "getallRecipe";
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Recipe
                        {
                            R_Id = Convert.ToInt32(rd["Id"]),
                            RecipeId = Convert.ToString(rd["Recipe_Id"]),
                            Item_name = Convert.ToString(rd["Item_name"]),
                            Item_type = Convert.ToString(rd["Item_type"]),
                        });
                    }
                    return list;
                }
            }
        }
        public int AddRecipe(Recipe info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_Recipe_Management]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "addRecipe";
                        cmd.Parameters.Add("@Item_name", SqlDbType.VarChar, 100).Value = info.Item_name;
                        cmd.Parameters.Add("@Created_by", SqlDbType.VarChar, 100).Value = info.CreatedBy;
                        cmd.Parameters.Add("@Item_type", SqlDbType.VarChar, 100).Value = info.Item_type;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
        }
        public int AddZipCode(CustomersAddress info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[HubZipCodeamt_Create]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@HubId", SqlDbType.VarChar).Value = info.Hub;
                        cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar).Value = info.ZipCode;
                        cmd.Parameters.Add("@DeliveryAmount", SqlDbType.Float).Value = info.StandradDeleiveryCharges;
                        cmd.Parameters.Add("@ExpressDeliveryAmount", SqlDbType.Float, 100).Value = info.ExpressDeleiveryCharges;
                        cmd.Parameters.Add("@AmountLimit", SqlDbType.Float).Value = info.MinOrderValue;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = info.Discription;
                        cmd.Parameters.Add("@CODAVAL", SqlDbType.VarChar).Value = info.CODAVAL;
                        cmd.Parameters.Add("@ExpressDaval", SqlDbType.VarChar).Value = info.ExpressDaval;
                        cmd.Parameters.Add("@country", SqlDbType.VarChar).Value = info.Country;
                        cmd.Parameters.Add("@InternationalCharge", SqlDbType.Float).Value = info.InternationalCharge;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
        }
        public int Addpaymentsgateway(CustomersAddress info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_Insert_tbl_PaymentGetway]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = info.Hub;
                        cmd.Parameters.Add("@Channel_Id", SqlDbType.VarChar).Value = info.ChannelId;
                        cmd.Parameters.Add("@Secret_Key", SqlDbType.Float).Value = info.SecretKey;
                        cmd.Parameters.Add("@Public_Key", SqlDbType.Float, 100).Value = info.PublicKey;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = info.CreatedBy;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
        }
        public CustomersAddress GetZipCodeDetails(int id)
        {
            CustomersAddress data = new CustomersAddress();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[HubZip_GetZipDetail]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                data = new CustomersAddress
                                {
                                    id = Convert.ToInt32(rd["Id"]),
                                    Hub = Convert.ToString(rd["HubId"]),
                                    HubName = Convert.ToString(rd["HubName"]),
                                    ZipCode = Convert.ToString(rd["Zipcode"]),
                                    StandradDeleiveryCharges = Convert.ToSingle(rd["DeliveryAmount"]),
                                    ExpressDeleiveryCharges = Convert.ToSingle(rd["ExpressDeliveryAmount"]),
                                    MinOrderValue = Convert.ToSingle(rd["AmountLimit"]),
                                    Discription = Convert.ToString(rd["Description"]),
                                    CODAVAL = Convert.ToString(rd["Cod"]),
                                    ExpressDaval = Convert.ToString(rd["ExpressDeliveryAval"])

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
        public CustomersAddress GetpaymentgatewayDetails(int id)
        {
            CustomersAddress data = new CustomersAddress();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[HubPayments_GetPaymentgatewayDetail]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                data = new CustomersAddress
                                {
                                    id = Convert.ToInt32(rd["Id"]),
                                    SecretKey = Convert.ToString(rd["Secret_Key"]),
                                    PublicKey = Convert.ToString(rd["Public_Key"]),
                                    ChannelId = Convert.ToString(rd["Channel_Id"]),
                                    CreatedBy = Convert.ToString(rd["CreatedBy"])
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
        public Recipe GetRecipeDetails(int id)
        {
            Recipe data = new Recipe();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_Recipe_Management]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@type", SqlDbType.VarChar).Value = "GetRecipeById";
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                data = new Recipe
                                {
                                    R_Id = Convert.ToInt32(rd["Id"]),
                                    RecipeId = Convert.ToString(rd["Recipe_Id"]),
                                    Item_name = Convert.ToString(rd["Item_name"]),
                                    Item_type = Convert.ToString(rd["Item_type"]),
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return data;
                }
            }
        }
        public int EditZipCode(CustomersAddress info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[HubZipCode_Update]";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = info.id;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DeliveryAmount", SqlDbType.Float).Value = info.StandradDeleiveryCharges;
                    cmd.Parameters.Add("@ExpressDeliveryAmount", SqlDbType.Float, 100).Value = info.ExpressDeleiveryCharges;
                    cmd.Parameters.Add("@AmountLimit", SqlDbType.Float).Value = info.MinOrderValue;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = info.Discription;
                    cmd.Parameters.Add("@CODAVAL", SqlDbType.VarChar).Value = info.CODAVAL;
                    cmd.Parameters.Add("@ExpressDaval", SqlDbType.VarChar).Value = info.ExpressDaval;
                    cmd.Parameters.Add("@HubId", SqlDbType.VarChar).Value = info.Hub;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {
                return (0);
            }
        }
        public int Editpayments(CustomersAddress info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[HubPaymentsgateway_Update]";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = info.id;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Secret_Key", SqlDbType.VarChar).Value = info.SecretKey;
                    cmd.Parameters.Add("@Public_Key", SqlDbType.VarChar).Value = info.PublicKey;
                    cmd.Parameters.Add("@Channel_Id", SqlDbType.VarChar).Value = info.ChannelId;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {                 
                return (0);
            }
        }
        public bool DeleteZipcode(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[HubZipcode_Delete]", con))
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
        public bool Deletepayments(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[hubpayment_Delete]", con))
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
        public bool DeleteRecipe(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_Recipe_Management]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "DeleteRecipe";
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int EditRecipe(Recipe info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_Recipe_Management]";
                    cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = "updateRecipe";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = info.R_Id;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Item_name", SqlDbType.VarChar, 100).Value = info.Item_name;
                    cmd.Parameters.Add("@Item_type", SqlDbType.VarChar, 100).Value = info.Item_type;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {                 
                return (0);
            }
        }
        public List<DeleiverySlot> GetSlotList()
        {
            List<DeleiverySlot> list = new List<DeleiverySlot>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_DeleiverySlot]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        list.Add(new DeleiverySlot
                        {
                            Id = Convert.ToInt32(rd["id"]),
                            SlotId = Convert.ToString(rd["SlotId"]),
                            Timing = Convert.ToString(rd["Timing"]),
                            SlotType = Convert.ToInt32(rd["Slot_Type"]),
                            SlotTypeDesc = Convert.ToString(rd["Slot_TypeDesc"])
                        });
                    }
                    return list;
                }
            }
        }
        public List<OfferType> GetofferList()
        {
            List<OfferType> list = new List<OfferType>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_GetOfferTypeCRUD]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.Add("@type", SqlDbType.Int).Value = 2;
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        list.Add(new OfferType
                        {
                            Id = Convert.ToInt32(rd["id"]),
                            OfferName = Convert.ToString(rd["OfferName"]),
                            Status = Convert.ToString(rd["Status"]),
                            CreatedBy = Convert.ToString(rd["CreatedBy"])
                        });
                    }
                    return list;
                }
            }
        }
        public int AddSlot(DeleiverySlot info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[DeleiverySlot_Create]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Timing", SqlDbType.VarChar).Value = info.Timing;
                    cmd.Parameters.Add("@SlotType", SqlDbType.Int).Value = info.SlotType;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }        
        public DeleiverySlot GetSlotDetails(int id)
        {
            DeleiverySlot data = new DeleiverySlot();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[DeleiverySlotDetails]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                data = new DeleiverySlot
                                {
                                    Id = Convert.ToInt32(rd["id"]),
                                    SlotId = Convert.ToString(rd["SlotId"]),
                                    Timing = Convert.ToString(rd["Timing"]),
                                    SlotType = Convert.ToInt32(rd["Slot_Type"]),
                                    SlotTypeDesc = Convert.ToString(rd["Slot_TypeDesc"])

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
        public int EditSlot(DeleiverySlot info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[DeleiverySlot_Update]";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = info.Id;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Timing", SqlDbType.VarChar).Value = info.Timing;
                    cmd.Parameters.Add("@SlotType", SqlDbType.Int).Value = info.SlotType;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public bool DeleteSlot(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[DeleiverySlot_Delete]", con))
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
        // brand related here...
        public List<BrandInfo> GetBrandList()
        {
            List<BrandInfo> list = new List<BrandInfo>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Brand_Getbrandlist]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new BrandInfo
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            BrandId = Convert.ToString(rd["BrandId"]),
                            BrandName = Convert.ToString(rd["BrandName"]),
                            Logo = Convert.ToString(rd["Logo"]),
                            CreatedBy = Convert.ToString(rd["CreatedBy"]),
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                            ItemCount = Convert.ToInt32(rd["ItemCount"]),
                        });
                    }
                    return list;
                }
            }
        }
        public List<SelectListItem> GetSupplierlist()
        {
            List<SelectListItem> SupplierNameList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetSupplierNameList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["Business"]);
                        string Id = Convert.ToString(rd["VendorId"]);
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
        public List<string> GetSupplierlist(int id)
        {
            var result = new List<string>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetSupplierNameListByIdS]", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(Convert.ToString(reader[0]));
                    }
                }
                return result;
            }
        }
        public string AddBrand(BrandInfo info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Brand_CreateBrand]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        DataTable odt = new DataTable();
                        odt.Columns.Add("GetSupplier", typeof(string));
                        if (info.Supplierlist != null)
                        {
                            foreach (string o in info.Supplierlist)
                            {
                                odt.Rows.Add(o);
                            }
                        }
                        cmd.Parameters.Add("@Supplierlist", SqlDbType.Structured).Value = odt;
                        cmd.Parameters.Add("@BrandName", SqlDbType.VarChar, 50).Value = info.BrandName;
                        cmd.Parameters.Add("@Logo", SqlDbType.VarChar).Value = info.Logo == null ? (object)DBNull.Value : info.Logo;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = info.CreatedBy;
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    return "0";
                }
        }
        public BrandInfo GetBrandDetails(int id)
        {
            BrandInfo data = new BrandInfo();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Brand_GetBrandDetails]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                data = new BrandInfo
                                {
                                    Id = Convert.ToInt32(rd["Id"]),
                                    BrandId = Convert.ToString(rd["BrandId"]),
                                    BrandName = Convert.ToString(rd["BrandName"]),
                                    Logo = Convert.ToString(rd["Logo"]),
                                    Supplier = Convert.ToString(rd["Supplier"]),
                                    ItemCount = Convert.ToInt32(rd["ItemCount"]),
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
        public int EditBrand(BrandInfo info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Brand_Update]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = info.Id;
                        con.Open();
                        DataTable odt = new DataTable();
                        odt.Columns.Add("GetSupplier", typeof(string));
                        if (info.Supplierlist != null)
                        {
                            foreach (string o in info.Supplierlist)
                            {
                                odt.Rows.Add(o);
                            }
                        }
                        cmd.Parameters.Add("@Supplierlist", SqlDbType.Structured).Value = odt;
                        cmd.Parameters.Add("@BrandName", SqlDbType.VarChar, 50).Value = info.BrandName;
                        cmd.Parameters.Add("@Logo", SqlDbType.VarChar).Value = info.Logo == null ? (object)DBNull.Value : info.Logo;
                        cmd.Parameters.Add("@LastupdatedBy", SqlDbType.VarChar, 50).Value = info.LastupdatedBy;
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
        }
        public bool DeleteBrand(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Brand_DeleteBrand]", con))
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

        // table related here...
        public List<TableInfo> GetTableList(string id)
        {
            List<TableInfo> list = new List<TableInfo>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Table_GetTablelist]", con))
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
                            id = Convert.ToInt32(rd["id"]),
                            TableCode = Convert.ToString(rd["TableCode"]),
                            tableId = Convert.ToString(rd["tableId"]),
                            tableName = Convert.ToString(rd["tableName"]),
                            branch = Convert.ToString(rd["branch"]),
                            created_by = Convert.ToString(rd["created_by"]),
                            created_on = Convert.ToDateTime(rd["created_on"])
                        });
                    }
                    return list;
                }
            }
        }
        public string AddTable(TableInfo add)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Table_CreateTable]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add("@tableName", SqlDbType.VarChar, 50).Value = add.tableName;
                        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = add.branch;
                        cmd.Parameters.Add("@Hub", SqlDbType.VarChar).Value = add.Hub;
                        cmd.Parameters.Add("@tblperfernce", SqlDbType.VarChar).Value = add.tblPerfernce;
                        cmd.Parameters.Add("@created_by", SqlDbType.VarChar, 50).Value = add.created_by;
                        cmd.Parameters.Add("@status", SqlDbType.VarChar, 20).Value = "Vacant";
                        cmd.Parameters.Add("@tableCode", SqlDbType.VarChar, 50).Value = add.TableCode;
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    return "Not Found";
                }
        }
        public TableInfo GetTableDetails(int id)
        {
            TableInfo data = new TableInfo();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Table_GetTableDetails]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                data = new TableInfo
                                {
                                    id = Convert.ToInt32(rd["id"]),
                                    tableId = Convert.ToString(rd["tableId"]),
                                    tableName = Convert.ToString(rd["tableName"]),
                                    branch = Convert.ToString(rd["branch"]),
                                    TableCode = Convert.ToString(rd["TableCode"]),
                                    tblPerfernce = Convert.ToString(rd["Perfernces"]),
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
        public int EditTable(TableInfo add)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Table_Update]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = add.id;
                        con.Open();
                        cmd.Parameters.Add("@tableName", SqlDbType.VarChar, 50).Value = add.tableName;
                        cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = add.branch;
                        cmd.Parameters.Add("@updated_by", SqlDbType.VarChar, 50).Value = add.updated_by;
                        cmd.Parameters.Add("@TableCode", SqlDbType.VarChar, 50).Value = add.TableCode;
                        cmd.Parameters.Add("@tblperferece", SqlDbType.VarChar, 50).Value = add.tblPerfernce;
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }
        public bool DeleteTable(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Table_DeleteTable]", con))
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
        // hub related here...
        public List<Hub> GetHubOrgList()
        {
            List<Hub> list = new List<Hub>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Hub_GetHublist]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Hub
                        {
                            Id = Convert.ToInt32(reader[0]),
                            HubId = Convert.ToString(reader[1]),
                            HubName = Convert.ToString(reader[2]),
                            Area = Convert.ToString(reader[3]),
                            BuildingName = Convert.ToString(reader[4]),
                            RoomNo = Convert.ToString(reader[5]),
                            Sector = Convert.ToString(reader[6]),
                            Landmark = Convert.ToString(reader[7]),
                            City = Convert.ToString(reader[8]),
                            State = Convert.ToString(reader[9]),
                            Country = Convert.ToString(reader[10]),
                            CreatedBy = Convert.ToString(reader[11]),
                            LastUpdatedOn = Convert.ToDateTime(reader[12]),
                            Count = Convert.ToInt32(reader[13]),
                            HubCount = Convert.ToInt32(reader[14]),
                            HubDetails = Convert.ToString(reader[3]) + ',' + Convert.ToString(reader[8]),
                        });
                    }
                    return list;
                }
            }
        }
        public int AddHub(Hub info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Hub_CreateHub]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@HubName", SqlDbType.VarChar).Value = info.HubName;
                        cmd.Parameters.Add("@Area", SqlDbType.VarChar).Value = info.Area;
                        cmd.Parameters.Add("@BuildingName", SqlDbType.VarChar).Value = info.BuildingName;
                        cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar).Value = info.RoomNo;
                        cmd.Parameters.Add("@Sector", SqlDbType.VarChar).Value = info.Sector;
                        cmd.Parameters.Add("@Landmark", SqlDbType.VarChar).Value = info.Landmark;
                        cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = info.City;
                        cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = info.State;
                        cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = info.Country;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = info.CreatedBy;
                        cmd.Parameters.Add("@Latitude", SqlDbType.Float).Value = info.Latitude;
                        cmd.Parameters.Add("@Longitude", SqlDbType.Float).Value = info.Longitude;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }
        public int EditHub(Hub info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_HubUpdate]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = info.Id;
                        cmd.Parameters.Add("@HubName", SqlDbType.VarChar).Value = info.HubName;
                        cmd.Parameters.Add("@Area", SqlDbType.VarChar).Value = info.Area;
                        cmd.Parameters.Add("@BuildingName", SqlDbType.VarChar).Value = info.BuildingName;
                        cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar).Value = info.RoomNo;
                        cmd.Parameters.Add("@Sector", SqlDbType.VarChar).Value = info.Sector;
                        cmd.Parameters.Add("@Landmark", SqlDbType.VarChar).Value = info.Landmark;
                        cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = info.City;
                        cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = info.State;
                        cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = info.Country;
                        cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar).Value = info.LastUpdatedBy;
                        cmd.Parameters.Add("@Latitude", SqlDbType.Float).Value = info.Latitude;
                        cmd.Parameters.Add("@Longitude", SqlDbType.Float).Value = info.Longitude;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }
        public Hub GetHubDetails(int id)
        {
            Hub hubdetail = new Hub();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_Hub_GetDetailsById]", con))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            hubdetail = new Hub
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                HubId = Convert.ToString(rd["HubId"]),
                                HubName = Convert.ToString(rd["HubName"]),
                                Area = Convert.ToString(rd["Area"]),
                                BuildingName = Convert.ToString(rd["BuildingName"]),
                                RoomNo = Convert.ToString(rd["RoomNo"]),
                                Sector = Convert.ToString(rd["Sector"]),
                                Landmark = Convert.ToString(rd["Landmark"]),
                                City = Convert.ToString(rd["City"]),
                                State = Convert.ToString(rd["State"]),
                                Country = Convert.ToString(rd["Country"]),
                            };
                        }
                    }
                    return hubdetail;
                }
            }
        }
        public bool DeleteHub(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Hub_DeleteHub]", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }
        // password policy related heree
        public bool SystemConfigUpdate(SecurityConfig securityConfig)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SystemConfig_Update]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Unique_Password_Count", SqlDbType.Int).Value = MappingHelpers.GenericValueType<int>(securityConfig.Unique_Password_Count);
                    cmd.Parameters.Add("@Password_Length", SqlDbType.Int).Value = MappingHelpers.GenericValueType<int>(securityConfig.Password_Length);
                    cmd.Parameters.Add("@Password_Expiry_Day", SqlDbType.Int).Value = MappingHelpers.GenericValueType<int>(securityConfig.Password_Expiry_Day);
                    cmd.Parameters.Add("@Session_Expiry_Hours", SqlDbType.Int).Value = MappingHelpers.GenericValueType<int>(securityConfig.Session_Expiry_Hours);
                    cmd.Parameters.Add("@Remember_Password", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(securityConfig.Remember_Password);
                    cmd.Parameters.Add("@Allow_Special_Character", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(securityConfig.Allow_Special_Character);
                    cmd.Parameters.Add("@Alpha_Numeric", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(securityConfig.Alpha_Numeric);
                    cmd.Parameters.Add("@Check_Capital", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(securityConfig.Check_Capital);
                    cmd.Parameters.Add("@Modified_By", SqlDbType.Int).Value = MappingHelpers.GenericValueType<bool>(securityConfig.Modified_By);
                    cmd.Parameters.Add("@Login_Attempt", SqlDbType.Int).Value = MappingHelpers.GenericValueType<int>(securityConfig.Login_Attempt);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public BusinessInfo GetBusinessInfoDetails(int id)
        {
            BusinessInfo data = new BusinessInfo();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[freshPos_mst].[dbo].[usp_BusinessInfoDetails]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@hotel_id", SqlDbType.Int).Value = Convert.ToInt32(_dbConfig.BusinessInfo);
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                data = new BusinessInfo
                                {
                                    hotel_id = Convert.ToInt32(rd["hotel_id"]),
                                    hotel_name = Convert.ToString(rd["hotel_name"]),
                                    first_name = Convert.ToString(rd["first_name"]),
                                    last_name = Convert.ToString(rd["last_name"]),
                                    contact_number = Convert.ToString(rd["contact_number"]),
                                    alternate_contactnumber = Convert.ToString(rd["alternate_contactnumber"]),
                                    email = Convert.ToString(rd["email"]),
                                    business_emailaddress = Convert.ToString(rd["business_emailaddress"]),
                                    logo_url = Convert.ToString(rd["logo_url"]),
                                    secondarylogo_url = Convert.ToString(rd["Secondary_logourl"]),
                                    website = Convert.ToString(rd["website"]),
                                    bussiness_type = Convert.ToString(rd["bussiness_type"]),
                                    hotel_status = Convert.ToInt32(rd["hotel_status"]),
                                    is_multibranch = Convert.ToInt32(rd["is_multibranch"]),
                                    banner_text = Convert.ToString(rd["banner_text"]),
                                    created_by = Convert.ToInt32(rd["created_by"]),
                                    created_on = Convert.ToDateTime(rd["created_on"]),
                                    modified_by = Convert.ToInt32(rd["modified_by"]),
                                    modified_on = Convert.ToDateTime(rd["modified_on"]),
                                    bussiness_status = Convert.ToInt32(rd["bussiness_status"]),
                                    currency = Convert.ToString(rd["currency"]),
                                    Country = Convert.ToString(rd["Country"]),
                                    Language = Convert.ToString(rd["Language"]),
                                    address1 = Convert.ToString(rd["address_1"]),
                                    address2 = Convert.ToString(rd["address_2"]),
                                    city = Convert.ToString(rd["city"]),
                                    state = Convert.ToString(rd["state"]),
                                    postalCode = Convert.ToString(rd["zip"]),
                                    thankhyouMessage = Convert.ToString(rd["thankYouMessage"]),
                                    splashScreenMessage = Convert.ToString(rd["splashscreenMessage"]),
                                    TimeId = Convert.ToInt32(rd["TimeId"]),
                                    legalbusinessName = Convert.ToString(rd["businessLegalName"]),
                                    aliyunPath = Convert.ToString(rd["aliyunPath"]),
                                    dbName = Convert.ToString(rd["dbName"]),
                                    printlogo_url = Convert.ToString(rd["Print_logourl"]),
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
        public int BusinessConfigUpdate(BusinessInfo businessConfig)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[freshPos_mst].[dbo].[usp_BusinessInfoUpdate]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@hotel_id", SqlDbType.Int).Value = businessConfig.hotel_id;
                    cmd.Parameters.Add("@hotel_name", SqlDbType.VarChar).Value = businessConfig.hotel_name;
                    cmd.Parameters.Add("@logo_url", SqlDbType.VarChar).Value = businessConfig.logo_url;
                    cmd.Parameters.Add("@bussiness_type", SqlDbType.VarChar).Value = businessConfig.bussiness_type;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = businessConfig.Country;
                    cmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = businessConfig.first_name;
                    cmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value = businessConfig.last_name;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = businessConfig.email;
                    cmd.Parameters.Add("@contact_number", SqlDbType.VarChar).Value = businessConfig.contact_number;
                    cmd.Parameters.Add("@alternate_contactnumber", SqlDbType.VarChar).Value = businessConfig.alternate_contactnumber;
                    cmd.Parameters.Add("@website", SqlDbType.VarChar).Value = businessConfig.website;
                    cmd.Parameters.Add("@business_emailaddress", SqlDbType.VarChar).Value = businessConfig.business_emailaddress;
                    cmd.Parameters.Add("@secondarylogo_url", SqlDbType.VarChar).Value =
                    cmd.Parameters.Add("@printlogo_url", SqlDbType.VarChar).Value = businessConfig.printlogo_url == null ? "NA" : businessConfig.printlogo_url;
                    cmd.Parameters.Add("@thankhyouMessage", SqlDbType.VarChar).Value = businessConfig.thankhyouMessage;
                    cmd.Parameters.Add("@splashScreenMessage", SqlDbType.VarChar).Value = businessConfig.splashScreenMessage;
                    cmd.Parameters.Add("@timeId", SqlDbType.Int).Value = businessConfig.TimeId;
                    cmd.Parameters.Add("@legalbusinessName", SqlDbType.VarChar).Value = businessConfig.legalbusinessName;
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public TaxationInfo GetTaxationInfo()
        {
            try
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
                                    taxbillType = Convert.ToInt32(sdr["taxbillType"]),
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
            catch (Exception ex)
            {
                throw;
            }            
        }
        public bool TaxInfoUpdate(TaxationInfo taxConfig, string hubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[usp_TaxInfoUpdate]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = hubId;
                        cmd.Parameters.Add("@taxType", SqlDbType.Int).Value = MappingHelpers.GenericValueType<int>(taxConfig.taxType);
                        cmd.Parameters.Add("@calcuationTaxtype", SqlDbType.Int).Value = MappingHelpers.GenericValueType<int>(taxConfig.calcuationTaxtype);
                        cmd.Parameters.Add("@taxRegNo", SqlDbType.VarChar, 50).Value = taxConfig.taxRegNo;
                        cmd.Parameters.Add("@vatRegNo", SqlDbType.VarChar, 50).Value = taxConfig.vatRegNo;
                        cmd.Parameters.Add("@gstTaxpercent", SqlDbType.Float).Value = taxConfig.taxPercentGST == 0 ? 0 : taxConfig.taxPercentGST;
                        cmd.Parameters.Add("@vatTaxpercent", SqlDbType.Float).Value = taxConfig.taxPercentVAT == 0 ? 0 : taxConfig.taxPercentVAT;
                        cmd.Parameters.Add("@taxbillType", SqlDbType.Int).Value = taxConfig.taxbillType;
                        cmd.Parameters.Add("@modifiedBy", SqlDbType.VarChar).Value = taxConfig.modifiedBy;
                        cmd.Parameters.Add("@isActive", SqlDbType.Int).Value = taxConfig.isActive;
                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        // to show all place this methode used
        public List<CurrencyMST> GetCurrencyList(string id)
        {
            try
            {
                List<CurrencyMST> getcurrencylist = new List<CurrencyMST>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_getCurrencyList]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 30).Value = id;
                    con.Open();
                    try
                    {
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                getcurrencylist.Add(new CurrencyMST()
                                {
                                    Id = Convert.ToInt32(rd["Id"]),
                                    ShortCode = Convert.ToString(rd["ShortCode"]),
                                    status = Convert.ToInt32(rd["status"]),
                                    symbol = Convert.ToString(rd["Symbol"]),
                                });
                            }
                            return getcurrencylist;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }            
        }
        // Configuation Used there 
        public List<CurrencyMST> GetConfigCurrencyList(string id)
        {
            List<CurrencyMST> getcurrencylist = new List<CurrencyMST>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_CurrencyList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        getcurrencylist.Add(new CurrencyMST()
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            ShortCode = Convert.ToString(rd["ShortCode"]),
                            symbol = Convert.ToString(rd["CurrencySymbol"]),
                            CountryName = Convert.ToString(rd["CountryName"]),
                        });
                    }
                    return getcurrencylist;
                }
            }
        }
        public CurrencyMST GetCurrencyDetails(int Id)
        {
            CurrencyMST currencyMST = new CurrencyMST();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_getCurrencySymbol]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        currencyMST = new CurrencyMST()
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            CountryName = Convert.ToString(rd["CountryName"]),
                            ShortCode = Convert.ToString(rd["ShortCode"]),
                            symbol = Convert.ToString(rd["CurrencySymbol"]),
                        };
                    }
                    return currencyMST;
                }
            }
        }
        public int CurrencySymbolUpdate(string currency, string symbol, string hubId, string country)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_UpdateCurrencySymbol]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ShortCode", SqlDbType.VarChar).Value = currency;
                    cmd.Parameters.Add("@Symbol", SqlDbType.NVarChar).Value = symbol;
                    cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = hubId;
                    cmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = country;
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public bool DeleteCurrency(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[usp_CurrencyDelete]", con))
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
        public string AddCurrency(CurrencyMST add)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Currency_AddCurrency]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add("@CountryName", SqlDbType.VarChar, 50).Value = add.CountryName;
                        cmd.Parameters.Add("@ShortCode", SqlDbType.VarChar).Value = add.ShortCode;
                        cmd.Parameters.Add("@symbol", SqlDbType.NVarChar).Value = add.symbol;
                        cmd.Parameters.Add("@CratedBy", SqlDbType.VarChar, 50).Value = add.CratedBy;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = add.hubId;
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    return "Not Found";
                }
        } 
       
        public int EditCurrency(CurrencyMST add)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[currency_Update]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = add.Id;
                        con.Open();
                        cmd.Parameters.Add("@CountryName", SqlDbType.VarChar, 100).Value = add.CountryName;
                        cmd.Parameters.Add("@ShortCode", SqlDbType.VarChar).Value = add.ShortCode;
                        cmd.Parameters.Add("@symbol", SqlDbType.NVarChar, 50).Value = add.symbol;
                        cmd.Parameters.Add("@LastUpdatedby", SqlDbType.VarChar, 50).Value = add.LastUpdatedBy;
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }
        public List<TimeZoneDetails> getTimezonelist()
        {
            List<TimeZoneDetails> getTimezoneMap = new List<TimeZoneDetails>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[freshPos_mst].[dbo].[usp_TimezoneList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        getTimezoneMap.Add(new TimeZoneDetails()
                        {
                            Id = Convert.ToInt32(rd["TimeId"]),
                            TimezoneName = Convert.ToString(rd["TimeZoneName"]),
                        });
                    }
                    return getTimezoneMap;
                }
            }
        }
        public List<InventoryAsset> Get_Inventory_Assets_List(string id)
        {
            try
            {
                List<InventoryAsset> inventoryAssets = new List<InventoryAsset>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[Usp_Inventory_AssetList]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@hubId", SqlDbType.Int).Value = id;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            inventoryAssets.Add(new InventoryAsset()
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                AssetsId = Convert.ToString(rd["AssetsId"]),
                                AssetName = Convert.ToString(rd["AssetName"]),
                                AssetsUnitPrice = Convert.ToString(rd["AssetsUnitPrice"]),
                                AssetUnitPrice2 = Convert.ToString(rd["AssetUnitPrice2"]),
                                AssetsLifespan = Convert.ToString(rd["AssetsLifespan"]),
                                IsServicable = Convert.ToInt32(rd["IsServicable"]),
                                CreatedBy = Convert.ToString(rd["CreatedBy"]),
                                Quantity = Convert.ToInt32(rd["Quantity"]),
                                Hub = Convert.ToString(rd["Hub"]),
                                Quantity2 = Convert.ToInt32(rd["Quantity2"]),
                            });
                        }
                        return inventoryAssets;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int Delete_Inventory_Asset(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[usp_InventoryAssetDelete]", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }

        public InventoryAsset InventoryDetails(int Id, string id)
        {
            InventoryAsset inventoryAssetDetails = new InventoryAsset();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_InventoryAssetDetails]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        inventoryAssetDetails = new InventoryAsset()
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            AssetName = Convert.ToString(rd["AssetName"]),
                            AssetsUnitPrice = Convert.ToString(rd["AssetsUnitPrice"]),
                            AssetsLifespan = Convert.ToString(rd["AssetsLifespan"]),
                            Hub = Convert.ToString(rd["Hub"]),
                            IsServicable = Convert.ToInt32(rd["IsServicable"]),
                            AssetDescriptions = Convert.ToString(rd["AssetDescriptions"]),
                        };
                    }
                    return inventoryAssetDetails;
                }
            }
        }
        public int UpdateInventory(InventoryAsset Add)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_InventoryAssetUpdate]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Add.Id;
                        cmd.Parameters.Add("@AssetName", SqlDbType.VarChar, 100).Value = Add.AssetName;
                        cmd.Parameters.Add("@AssetsUnitPrice", SqlDbType.VarChar, 20).Value = Add.AssetsUnitPrice;
                        cmd.Parameters.Add("@AssetsLifespan", SqlDbType.VarChar, 20).Value = Add.AssetsLifespan;
                        cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Add.Quantity;
                        cmd.Parameters.Add("@IsServicable", SqlDbType.Int).Value = Add.IsServicable;
                        cmd.Parameters.Add("@AssetDescriptions", SqlDbType.VarChar).Value = Add.AssetDescriptions;
                        cmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar).Value = Add.ModifiedBy;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public int CreateInventory(InventoryAsset Add)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_InventoryAssetCreate]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AssetName", SqlDbType.VarChar, 100).Value = Add.AssetName;
                    cmd.Parameters.Add("@AssetsUnitPrice", SqlDbType.VarChar, 20).Value = Add.AssetsUnitPrice;
                    cmd.Parameters.Add("@AssetsLifespan", SqlDbType.VarChar, 20).Value = Add.AssetsLifespan;
                    cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 20).Value = Add.Hub;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Add.Quantity;
                    cmd.Parameters.Add("@IsServicable", SqlDbType.Int).Value = Add.IsServicable;
                    cmd.Parameters.Add("@AssetDescriptions", SqlDbType.VarChar, -1).Value = Add.AssetDescriptions;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = Add.CreatedBy;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
        }
        public int Languagechange(LanguageMst Add)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[SelectedLanguage_CreateLanguage]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ItemName", SqlDbType.NVarChar, 10).Value = Add.ItemNameLanguage;
                    cmd.Parameters.Add("@Id", SqlDbType.Int, 10).Value = Add.Id;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 100).Value = Add.Id1;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = Add.CreatedBy;
                    cmd.Parameters.Add("@Languagecode", SqlDbType.VarChar, 50).Value = Add.LanguageName;
                    cmd.Parameters.Add("@descriptionLanguage", SqlDbType.NVarChar, -1).Value = Add.Descriptionlanguage;
                    cmd.Parameters.Add("@ItemUsed", SqlDbType.NVarChar, -1).Value = Add.Itemused;
                    cmd.Parameters.Add("@Hub", SqlDbType.NVarChar, 50).Value = Add.Hub;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
        }
        public int Addlanguage(ItemMasters Add)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[USP_Addlanguageinsetting]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@LanguageName", SqlDbType.VarChar, 100).Value = Add.LanguageName;
                    cmd.Parameters.Add("@Languagecode", SqlDbType.VarChar, 20).Value = Add.LanguageName;
                    cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 20).Value = Add.Hub;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
        }
        public List<ItemMasters> selectlanguage(string id)
        {
            List<ItemMasters> list = new List<ItemMasters>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[USP_settingLanguage]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                list.Add(new ItemMasters
                                {
                                    Id = Convert.ToInt32(rd["Id"]),
                                    LanguageName = Convert.ToString(rd["LanguageName"]),
                                    Languagecode = Convert.ToString(rd["Languagecode"])
                                });
                            }
                            return list;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }
        public List<ItemMasters> languageselect(string id)
        {
            List<ItemMasters> list = new List<ItemMasters>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[USP_selectLanguage]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@hubId", SqlDbType.Int).Value = id;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                list.Add(new ItemMasters
                                {
                                    Id = Convert.ToInt32(rd["Id"]),
                                    LanguageName = Convert.ToString(rd["LanguageName"]),
                                    Languagecode = Convert.ToString(rd["Languagecode"])
                                });
                            }
                            return list;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }

        public int deleteLanguage(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[USP_deleteselectLanguage]", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }
        public BusinessInfo gethublist(string id)
        {
            BusinessInfo data = new BusinessInfo();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_gethubdetails]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        data = new BusinessInfo()
                        {
                            address1 = Convert.ToString(rd["address1"]),
                        };
                    }
                    return data;
                }
            }
        }
        public OfferType GetOfferType(int id)
        {
            OfferType data = new OfferType();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_GetOfferTypeCRUD]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                        cmd.Parameters.AddWithValue("@type", SqlDbType.Int).Value = 3;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                data = new OfferType
                                {
                                    Id = Convert.ToInt32(rd["id"]),
                                    OfferName = Convert.ToString(rd["OfferName"]),
                                    Status = Convert.ToString(rd["Status"])

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
        public int AddOfferType(OfferType info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_GetOfferTypeCRUD]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OfferName", SqlDbType.VarChar).Value = info.OfferName;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = info.Status;
                    cmd.Parameters.Add("@type", SqlDbType.Int).Value = 4;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int EditOfferType(OfferType info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_GetOfferTypeCRUD]";
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = info.Id;
                        cmd.Parameters.Add("@type", SqlDbType.Int).Value = 5;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@OfferName", SqlDbType.VarChar).Value = info.OfferName;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = info.Status;                        
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
        }

        public List<ProductType> GetProductMainList(string id)
        {
            List<ProductType> list = new List<ProductType>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_getProductSpec]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hubId", SqlDbType.VarChar).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ProductType
                        {
                            ProductCategoryId = Convert.ToString(rd["ProductCategoryId"]),
                            ProductCategoryName = Convert.ToString(rd["ProductCategoryName"]),
                            BranchId = Convert.ToString(rd["BranchId"]),
                            
                        });
                    }
                    return list;
                }
            }
        }
        public List<ProductSpecSubCatgeory> GetProductSpecSucCategoryList(string Id, string ProductId)
        {
            List<ProductSpecSubCatgeory> list = new List<ProductSpecSubCatgeory>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_getProductSpecwithId]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hubId", SqlDbType.VarChar).Value = Id;
                cmd.Parameters.AddWithValue("@ProductCategoryId", SqlDbType.VarChar).Value = ProductId;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ProductSpecSubCatgeory
                        {
                            productCatId = Convert.ToString(rd["ProductCategoryId"]),
                            ProductSubCatId = Convert.ToString(rd["ProductSubCatId"]),
                            ProductSubCatName = Convert.ToString(rd["ProductSubCatName"]),
                            BranchId = Convert.ToString(rd["BranchId"]),
                            
                        });
                    }
                    return list;
                }
            }
        }
        public string AddProductSpecs(ProductType pro)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_InsertProductMain]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add("@ProductCategoryName", SqlDbType.VarChar, 150).Value = pro.ProductCategoryName;
                        cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 150).Value = pro.BranchId;
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    return "Not Found";
                }
        }  
        public string AddProductSubSpecs(ProductSpecSubCatgeory pro)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Usp_InsertProductSub]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add("@ProductSubCatName", SqlDbType.VarChar, 150).Value = pro.ProductSubCatName;
                        cmd.Parameters.Add("@ProductCategoryId", SqlDbType.VarChar, 150).Value = pro.productCatId;
                        cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 150).Value = pro.BranchId;
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    return "Not Found";
                }
        }     
        
        public string UpdateProductSpecs (ProductType pro)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_UpdateProductMain]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add("@ProductCategoryName", SqlDbType.VarChar, 150).Value = pro.ProductCategoryName;
                        cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 150).Value = pro.BranchId;
                        cmd.Parameters.Add("@productCatId", SqlDbType.VarChar, 150).Value = pro.ProductCategoryId;
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    return "Not Found";
                }
        }
        public string UpdateProductSubSpecs(ProductSpecSubCatgeory pro)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Usp_UpdateProductSub]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add("@ProductSubCatName", SqlDbType.VarChar, 150).Value = pro.ProductSubCatName;
                        cmd.Parameters.Add("@ProductSubCatId", SqlDbType.VarChar, 150).Value = pro.ProductSubCatId;
                        cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 150).Value = pro.BranchId;
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    return "Not Found";
                }
        }
        public string DeleteProductSpecs(string Id, string hubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_DeletetProductMain]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add("@productCatId", SqlDbType.VarChar, 150).Value = Id;
                        cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 150).Value = hubId;
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    return "Not Found";
                }
        }
        public string DeleteProductSUbSpecs(string Id, string hubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[Usp_DeleteProductSub]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add("@ProductSubCatId", SqlDbType.VarChar, 150).Value = Id;
                        cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 150).Value = hubId;
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    return "Not Found";
                }
        }
    }
}
