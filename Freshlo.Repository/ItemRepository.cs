using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Category;
using Freshlo.DomainEntities.DTO;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using static Freshlo.DomainEntities.ItemMasters;

namespace Freshlo.Repository
{
    public class ItemRepository : IItemRI
    {
        public ItemRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        private IDbConfig _dbConfig { get; }
        public List<ItemCategory> GetCategories(string id)
        {
            try
            {
                List<ItemCategory> list = new List<ItemCategory>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[GetCategoryList2]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            list.Add(new ItemCategory
                            {
                                CategoryId = rd[0] as string,
                                Name = rd[1] as string
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
        public string CreateItemMaster(ItemMasters itemMasters)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMaster_Create]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable odt = new DataTable();
                    odt.Columns.Add("AvailableDay", typeof(int));
                    if (itemMasters.AvailableDayS != null)
                    {
                        foreach (var o in itemMasters.AvailableDayS)
                        {
                            odt.Rows.Add(o);
                        }
                    }
                    cmd.Parameters.Add("@DayList", SqlDbType.Structured).Value = odt;
                    cmd.Parameters.Add("@PluName", SqlDbType.NVarChar, 200).Value = itemMasters.PluName;
                    cmd.Parameters.Add("@imagecdnpath", SqlDbType.NVarChar, -1).Value = itemMasters.imagecdnpath == null ? "NA" : itemMasters.imagecdnpath;
                    cmd.Parameters.Add("@PluCode", SqlDbType.VarChar, 100).Value = itemMasters.PluCode;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = itemMasters.Description == null ? "NA" : itemMasters.Description;
                    cmd.Parameters.Add("@Category", SqlDbType.VarChar, 100).Value = itemMasters.Category;
                    cmd.Parameters.Add("@SubCategory", SqlDbType.VarChar, 100).Value = itemMasters.SubCategory;
                    cmd.Parameters.Add("@Measurement", SqlDbType.VarChar, 50).Value = itemMasters.Measurement == null ? "Ut" : itemMasters.Measurement;
                    cmd.Parameters.Add("@Weight", SqlDbType.Float).Value = itemMasters.Weight == null ? "0" : itemMasters.Weight;
                    cmd.Parameters.Add("@Purchaseprice", SqlDbType.VarChar, 50).Value = itemMasters.Purchaseprice == null ? "0" : itemMasters.Purchaseprice;
                    cmd.Parameters.Add("@Wastage", SqlDbType.VarChar, 50).Value = itemMasters.Wastage == null ? "0" : itemMasters.Wastage;
                    cmd.Parameters.Add("@SellingPrice", SqlDbType.VarChar, 200).Value = itemMasters.SellingPrice == null ? "0" : itemMasters.SellingPrice;
                    cmd.Parameters.Add("@MarketPrice", SqlDbType.VarChar, 50).Value = itemMasters.MarketPrice == null ? "0" : itemMasters.MarketPrice;
                    cmd.Parameters.Add("@ProfitPrice", SqlDbType.VarChar, 20).Value = itemMasters.ProfitPrice == null ? "0" : itemMasters.ProfitPrice;
                    cmd.Parameters.Add("@ActualCost", SqlDbType.VarChar, 20).Value = itemMasters.ActualCost == null ? "0" : itemMasters.ActualCost;
                    cmd.Parameters.Add("@seasonSale", SqlDbType.VarChar, 10).Value = itemMasters.seasonSale == null ? "NA" : itemMasters.seasonSale;
                    cmd.Parameters.Add("@offer_type", SqlDbType.VarChar, 10).Value = itemMasters.offer_type == null ? "NA" : itemMasters.offer_type;
                    cmd.Parameters.Add("@foodSegment", SqlDbType.VarChar, 10).Value = itemMasters.foodSegment == null ? "NA" : itemMasters.foodSegment;
                    cmd.Parameters.Add("@ItemSellingType", SqlDbType.VarChar, 20).Value = itemMasters.ItemSellingType == null ? "Packed" : itemMasters.ItemSellingType;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100).Value = itemMasters.CreatedBy;
                    cmd.Parameters.Add("@LongDescription", SqlDbType.NVarChar).Value = itemMasters.LongDescription == null ? "NA" : itemMasters.LongDescription;
                    cmd.Parameters.Add("@Supplier", SqlDbType.VarChar, 100).Value = itemMasters.Supplier;
                    cmd.Parameters.Add("@MainCategory", SqlDbType.VarChar, 30).Value = itemMasters.MainCategory;
                    cmd.Parameters.Add("@ProfitMargin", SqlDbType.VarChar, 50).Value = itemMasters.ProfitMargin == null ? "0.00" : itemMasters.ProfitMargin;
                    cmd.Parameters.Add("@StockType", SqlDbType.VarChar, 15).Value = itemMasters.StockType == null ? "NA" : itemMasters.StockType;
                    cmd.Parameters.Add("@NetWeight", SqlDbType.Float).Value = itemMasters.NetWeight == null ? "01.00" : itemMasters.NetWeight;
                    cmd.Parameters.Add("@MaxQuantityAllowed", SqlDbType.Int).Value = itemMasters.MaxQuantityAllowed == 0 ? 1 : itemMasters.MaxQuantityAllowed;
                    cmd.Parameters.Add("@Brand", SqlDbType.VarChar, 30).Value = itemMasters.Brand;
                    cmd.Parameters.Add("@Tag", SqlDbType.VarChar, 30).Value = itemMasters.Tag == null ? "NA" : itemMasters.Tag;
                    cmd.Parameters.Add("@ItemType", SqlDbType.VarChar, 10).Value = itemMasters.ItemType == null ? "NA" : itemMasters.ItemType;
                    cmd.Parameters.Add("@Visibility", SqlDbType.VarChar).Value = itemMasters.Visibility;
                    cmd.Parameters.Add("@SellingVarience", SqlDbType.VarChar).Value = itemMasters.SellingVarience == null ? "NA" : itemMasters.SellingVarience;
                    cmd.Parameters.Add("@Relation", SqlDbType.Int).Value = itemMasters.Relationship == null ? 0 : itemMasters.Relationship;
                    cmd.Parameters.Add("@ParentId", SqlDbType.VarChar, 50).Value = itemMasters.ParentId == null ? "NA" : itemMasters.ParentId;
                    cmd.Parameters.Add("@ItemCost", SqlDbType.Float).Value = itemMasters.ProductCost == null ? 0 : itemMasters.ProductCost;
                    cmd.Parameters.Add("@VAT", SqlDbType.VarChar).Value = itemMasters.vat_per == null ? "NA" : itemMasters.vat_per;
                    cmd.Parameters.Add("@GST", SqlDbType.VarChar, 20).Value = itemMasters.gst_per == null ? "NA" : itemMasters.gst_per;
                    if (itemMasters.vat_per != null)
                    {
                        cmd.Parameters.Add("@CGST", SqlDbType.VarChar, 20).Value = itemMasters.cgst_per == null ? "NA" : itemMasters.vat_per;
                    }
                    else
                    {
                        cmd.Parameters.Add("@CGST", SqlDbType.VarChar, 20).Value = itemMasters.cgst_per == null ? "NA" : itemMasters.cgst_per;
                    }
                    cmd.Parameters.Add("@SGST", SqlDbType.VarChar, 20).Value = itemMasters.sgst_per == null ? "NA" : itemMasters.sgst_per;
                    cmd.Parameters.Add("@HSNCode", SqlDbType.VarChar, 200).Value = itemMasters.HSN_Code == null ? "NA" : itemMasters.HSN_Code;
                    cmd.Parameters.Add("@Tax", SqlDbType.VarChar, 10).Value = itemMasters.Tax == null ? "Yes" : itemMasters.Tax;
                    cmd.Parameters.Add("@KOT_Print", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@FoodType", SqlDbType.VarChar, 50).Value = itemMasters.FoodType == null ? "NA" : itemMasters.FoodType;
                    cmd.Parameters.Add("@FoodSubType", SqlDbType.VarChar, 50).Value = itemMasters.FoodSubType == null ? "NA" : itemMasters.FoodSubType;
                    cmd.Parameters.Add("@IsSpecialDay", SqlDbType.VarChar, 5).Value = itemMasters.IsSpecialDay == null ? "NA" : itemMasters.IsSpecialDay;
                    cmd.Parameters.Add("@MealTimeType", SqlDbType.VarChar, 50).Value = itemMasters.MealTimeType == null ? "NA" : itemMasters.MealTimeType;
                    cmd.Parameters.Add("@StartTime", SqlDbType.VarChar, 70).Value = itemMasters.StartTime == null ? "00:00" : itemMasters.StartTime;
                    cmd.Parameters.Add("@EndTime", SqlDbType.VarChar, 70).Value = itemMasters.EndTime == null ? "00:00" : itemMasters.EndTime;
                    cmd.Parameters.Add("@PreparationTime", SqlDbType.VarChar, 70).Value = 0;
                    cmd.Parameters.Add("@Spicy_Level", SqlDbType.Int).Value = 0;
                    con.Open();
                    return Convert.ToString(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<SelectListItem> GetCategoryNamebyMainCate(string mainCatId, string id)
        {
            List<SelectListItem> GetCategorySelectList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetCategoryListbyMainCat]", con))
            {
                cmd.Parameters.Add("@mainCatId", SqlDbType.VarChar, 100).Value = mainCatId;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["Name"]);
                        string Id = Convert.ToString(rd["CategoryId"]);
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
        public List<SelectListItem> GetMainCategoryList(string id)
        {
            try
            {
                List<SelectListItem> GetCategorySelectList = new List<SelectListItem>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[GetMainCategoryList3]", con))
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
        public List<SelectListItem> GetSubCategoryListbyCat(string CatId, string id)
        {
            List<SelectListItem> GetSubCategorySelectList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetSubCategoryListbyCat]", con))
            {
                cmd.Parameters.Add("@CategoryId", SqlDbType.VarChar, 100).Value = CatId;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["Name"]);
                        string Id = Convert.ToString(rd["SubCategoryId"]);
                        GetSubCategorySelectList.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return GetSubCategorySelectList;
                }
            }
        }
        public List<SelectListItem> GetSupplierNameList()
        {
            try
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
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<SelectListItem> GetBrandList()
        {
            try
            {
                List<SelectListItem> BrandList = new List<SelectListItem>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[GetBrandSL]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string text = Convert.ToString(rd["BrandName"]);
                            string Id = Convert.ToString(rd["BrandId"]);
                            BrandList.Add(new SelectListItem()
                            {
                                Text = text,
                                Value = Id,
                            });
                        }
                        return BrandList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int CreatePriceMaster(ItemMasters itemMasters)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMaster_CreatePriceList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemId", itemMasters.ItemId);
                cmd.Parameters.AddWithValue("@PluName", itemMasters.PluName);
                cmd.Parameters.AddWithValue("@Weight", itemMasters.Weight);
                cmd.Parameters.AddWithValue("@Measurement", itemMasters.Measurement);
                cmd.Parameters.AddWithValue("@WasteagePerc", itemMasters.Wastage == null ? "NA" : itemMasters.Wastage);
                cmd.Parameters.AddWithValue("@PurchasePrice", itemMasters.Purchaseprice == null ? "NA" : itemMasters.Purchaseprice);
                cmd.Parameters.AddWithValue("@SellingPrice", itemMasters.SellingPrice == null ? "NA" : itemMasters.SellingPrice);
                cmd.Parameters.AddWithValue("@MarketPrice", itemMasters.MarketPrice == null ? "NA" : itemMasters.MarketPrice);
                cmd.Parameters.AddWithValue("@seasonSale", itemMasters.seasonSale);
                cmd.Parameters.AddWithValue("@ProfitPrice", itemMasters.ProfitPrice == null ? "NA" : itemMasters.ProfitPrice);
                cmd.Parameters.AddWithValue("@ActualCost", itemMasters.ActualCost == null ? "NA" : itemMasters.ActualCost);
                cmd.Parameters.AddWithValue("@offer_type", itemMasters.offer_type);
                cmd.Parameters.AddWithValue("@foodSegment", itemMasters.foodSegment);
                cmd.Parameters.AddWithValue("@CreatedBy", itemMasters.CreatedBy);
                cmd.Parameters.AddWithValue("@ProfitMargin", itemMasters.ProfitMargin == null ? "NA" : itemMasters.ProfitMargin);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int CreateStockMaster(ItemMasters itemMasters)
        {
            var stock = itemMasters.ItemSellingType == "2" ? 0 : 1;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Stock_InsertStock]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 100).Value = itemMasters.ItemId;
                cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = stock;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 100).Value = itemMasters.CreatedBy;
                cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 30).Value = itemMasters.Branch;
                cmd.Parameters.Add("@Measurement", SqlDbType.VarChar, 100).Value = itemMasters.StartTime == null ? "Ut" : itemMasters.StartTime;
                cmd.Parameters.Add("@ItemPrice", SqlDbType.Int).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int CreateWastageMaster(ItemMasters itemMasters)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Wastage_CreateWastage]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = itemMasters.ItemId;
                cmd.Parameters.Add("@Wastage_Quan", SqlDbType.Float).Value = 0;
                cmd.Parameters.Add("@WastageType", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Wastage_Description", SqlDbType.VarChar, 200).Value = "NA";
                cmd.Parameters.Add("@WastageItemPrice", SqlDbType.Float).Value = 0;
                cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 30).Value = itemMasters.Branch;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = itemMasters.CreatedBy;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<ItemMasters> GetItemManageData(ItemMasters item)
        {
            try
            {
               
                List<ItemMasters> itemMasterdetail = new List<ItemMasters>();
                var itemstatusver = "";
                var itemstatusver1 = "";
                var maincate = item.MainCategory1 != null ? String.Join(",", item.MainCategory1) : "null";
                var maincate1 = item.MainCategory1 != null ? "0" : "null";
                var Cate = item.categoryId != null ? String.Join(",", item.categoryId) : "null";
                var cate1 = item.categoryId != null ? "0" : "null";
                var SubCategory = item.subcategory != null ? String.Join(",", item.subcategory) : "null";
                var SubCategory1 = item.subcategory != null ? "0" : "null";
                var Supplier = item.supplierid != null ? String.Join(",", item.supplierid) : "null";
                var Supplier1 = item.supplierid != null ? "0" : "null";
                var Featured = item.featured1 != null ? String.Join(",", item.featured1) : "null";
                var Featured1 = item.featured1 != null ? "0" : "null";
                var KotPrinted = item.KotPrintedId;
                var status = item.ItemStatus == 0 ? 1 : item.ItemStatus;
                if (status == 0 )
                {
                    itemstatusver = "Approved";
                    itemstatusver1 = "Pending";
                }
                if( status == 1)
                {
                    itemstatusver = "Approved";
                }
                if (status == 2)
                {
                    itemstatusver = "Pending";
                }
                var query = "";
                if (KotPrinted == 0)
                {
                    query = "SELECT Itm.Id,Itm.ItemId,Itm.PluName,m.Name,[dbo].[GetCategoryName](Itm.Category) as Category,[dbo].[GetSubCategoryName](Itm.SubCategory) as SubCategory,itm.Visibility,Itm.featured,[dbo].[Item_offer] (Itm.OfferId) as OfferId,Itm.seasonSale, " +
                           " Itm.ItemSellingType,Itm.MaxQuantityAllowed,Itm.Approval," +
                           "(select top 1  isnull(p.SellingPrice,0)  from PriceList p inner join ItemsMaster i on i.ItemId= p.ItemId where i.ItemId=Itm .ItemId)  as SellingPrice,Itm.Measurement,Itm.Coupen_Disc FROM[dbo].[ItemsMaster] Itm left join tblCategory c on c.CategoryId=Itm.Category left join MainCategory m on c.MainCategoryId=m.MainCategoryId where (" + maincate1 + " IS NULL OR  Itm.MainCategory IN (" + maincate + ")) AND (" + cate1 + " IS NULL OR Itm.Category IN (" + Cate + ")) AND " +
                           " (" + SubCategory1 + " IS NULL OR Itm.SubCategory IN(" + SubCategory + ")) AND (" + Supplier1 + " IS NULL OR Itm.Supplier IN(" + Supplier + ")) AND (" + Featured1 + " IS NULL OR Itm.featured IN(" + Featured + ")) AND (Itm.Approval In ('" + itemstatusver +  "','"+ itemstatusver1 + "'))";
                }
                else
                {
                    query = "SELECT Itm.Id,Itm.ItemId,Itm.PluName,m.Name,[dbo].[GetCategoryName](Itm.Category) as Category,[dbo].[GetSubCategoryName](Itm.SubCategory) as SubCategory,itm.Visibility,Itm.featured,[dbo].[Item_offer] (Itm.OfferId) as OfferId,Itm.seasonSale, " +
                           " Itm.ItemSellingType,Itm.MaxQuantityAllowed,Itm.Approval," +
                           "(select top 1  isnull(p.SellingPrice,0)  from PriceList p inner join ItemsMaster i on i.ItemId= p.ItemId where i.ItemId=Itm .ItemId)  as SellingPrice,Itm.Measurement,Itm.Coupen_Disc FROM[dbo].[ItemsMaster] Itm left join tblCategory c on c.CategoryId=Itm.Category left join MainCategory m on c.MainCategoryId=m.MainCategoryId where (" + maincate1 + " IS NULL OR  Itm.MainCategory IN (" + maincate + ")) AND (" + cate1 + " IS NULL OR Itm.Category IN (" + Cate + ")) AND " +
                           " (" + SubCategory1 + " IS NULL OR Itm.SubCategory IN(" + SubCategory + ")) AND (" + Supplier1 + " IS NULL OR Itm.Supplier IN(" + Supplier + ")) AND (" + Featured1 + " IS NULL OR Itm.featured IN(" + Featured + ")) AND (Itm.Approval In ('" + itemstatusver + "'))";
                }
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            itemMasterdetail.Add(new ItemMasters
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                ItemId = Convert.ToString(rd["ItemId"]),
                                PluName = Convert.ToString(rd["PluName"]),
                                MainCategory = Convert.ToString(rd["Name"]),
                                Category = Convert.ToString(rd["Category"]),
                                SubCategory = Convert.ToString(rd["SubCategory"]),
                                Visibility = Convert.ToString(rd["Visibility"]),
                                featured = Convert.ToString(rd["featured"]),
                                OfferId = rd["OfferId"] == DBNull.Value ? "N" : Convert.ToString(rd["OfferId"]),
                                seasonSale = Convert.ToString(rd["seasonSale"]),
                                ItemSellingType = Convert.ToString(rd["ItemSellingType"]),
                                MaxQuantityAllowed = rd["MaxQuantityAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(rd["MaxQuantityAllowed"]),
                                Approval = Convert.ToString(rd["Approval"]),
                                SellingPrice = Convert.ToString(rd["SellingPrice"] == DBNull.Value ? "0.00" : Convert.ToString(rd["SellingPrice"])),
                                Measurement = Convert.ToString(rd["Measurement"]),
                                Coupen = rd["Coupen_Disc"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Coupen_Disc"])
                            });
                        }
                        return itemMasterdetail;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public List<ItemMasters> GetItemManageData(ItemMasters item)
        //{
        //    List<ItemMasters> itemMasterdetail = new List<ItemMasters>();
        //    var itemstatusver = "";
        //    var maincate = item.MainCategory1 != null ? String.Join(",", item.MainCategory1) : "null";
        //    var maincate1 = item.MainCategory1 != null ? "0" : "null";
        //    var Cate = item.categoryId != null ? String.Join(",", item.categoryId) : "null";
        //    var cate1 = item.categoryId != null ? "0" : "null";
        //    var SubCategory = item.subcategory != null ? String.Join(",", item.subcategory) : "null";
        //    var SubCategory1 = item.subcategory != null ? "0" : "null";
        //    var Supplier = item.supplierid != null ? String.Join(",", item.supplierid) : "null";
        //    var Supplier1 = item.supplierid != null ? "0" : "null";
        //    var Featured = item.featured1 != null ? String.Join(",", item.featured1) : "null";
        //    var Featured1 = item.featured1 != null ? "0" : "null";
        //    var KotPrinted = item.KotPrintedId;
        //    var status = item.ItemStatus == 0 ? 1 : item.ItemStatus;
        //    if (status == 0 || status == 1)
        //    {
        //        itemstatusver = "Approved";
        //    }
        //    if (status == 2)
        //    {
        //        itemstatusver = "Pending";
        //    }
        //    var query = "";
        //    if (KotPrinted == 0)
        //    {
        //        query = "SELECT Itm.Id,Itm.ItemId,Itm.PluName,[dbo].[GetCategoryName](Itm.Category) as Category,[dbo].[GetSubCategoryName](Itm.SubCategory) as SubCategory,itm.Visibility,Itm.featured,[dbo].[Item_offer] (Itm.OfferId) as OfferId,Itm.seasonSale, " +
        //               " Itm.ItemSellingType,Itm.MaxQuantityAllowed,Itm.Approval," +
        //               "(select top 1  isnull(p.SellingPrice,0)  from PriceList p inner join ItemsMaster i on i.ItemId= p.ItemId where i.ItemId=Itm .ItemId)  as SellingPrice,Itm.Measurement,Itm.Coupen_Disc FROM[dbo].[ItemsMaster] Itm where (" + maincate1 + " IS NULL OR  Itm.MainCategory IN (" + maincate + ")) AND (" + cate1 + " IS NULL OR Itm.Category IN (" + Cate + ")) AND " +
        //               " (" + SubCategory1 + " IS NULL OR Itm.SubCategory IN(" + SubCategory + ")) AND (" + Supplier1 + " IS NULL OR Itm.Supplier IN(" + Supplier + ")) AND (" + Featured1 + " IS NULL OR Itm.featured IN(" + Featured + "))  AND (Itm.KOT_Print IN " + (1, 2) + " ) AND (Itm.Approval In ('" + itemstatusver + "'))";
        //    }
        //    else
        //    {
        //        query = "SELECT Itm.Id,Itm.ItemId,Itm.PluName,[dbo].[GetCategoryName](Itm.Category) as Category,[dbo].[GetSubCategoryName](Itm.SubCategory) as SubCategory,itm.Visibility,Itm.featured,[dbo].[Item_offer] (Itm.OfferId) as OfferId,Itm.seasonSale, " +
        //               " Itm.ItemSellingType,Itm.MaxQuantityAllowed,Itm.Approval," +
        //               "(select top 1  isnull(p.SellingPrice,0)  from PriceList p inner join ItemsMaster i on i.ItemId= p.ItemId where i.ItemId=Itm .ItemId)  as SellingPrice,Itm.Measurement,Itm.Coupen_Disc FROM[dbo].[ItemsMaster] Itm where (" + maincate1 + " IS NULL OR  Itm.MainCategory IN (" + maincate + ")) AND (" + cate1 + " IS NULL OR Itm.Category IN (" + Cate + ")) AND " +
        //               " (" + SubCategory1 + " IS NULL OR Itm.SubCategory IN(" + SubCategory + ")) AND (" + Supplier1 + " IS NULL OR Itm.Supplier IN(" + Supplier + ")) AND (" + Featured1 + " IS NULL OR Itm.featured IN(" + Featured + "))  AND (Itm.KOT_Print = " + KotPrinted + " ) AND (Itm.Approval In ('" + itemstatusver + "'))";
        //    }
        //    using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand(query, con))
        //    {
        //        con.Open();
        //        using (SqlDataReader rd = cmd.ExecuteReader())
        //        {
        //            while (rd.Read())
        //            {
        //                itemMasterdetail.Add(new ItemMasters
        //                {
        //                    Id = Convert.ToInt32(rd["Id"]),
        //                    ItemId = Convert.ToString(rd["ItemId"]),
        //                    PluName = Convert.ToString(rd["PluName"]),
        //                    //MainCategory = Convert.ToString(rd["MainCategory"]),
        //                    Category = Convert.ToString(rd["Category"]),
        //                    SubCategory = Convert.ToString(rd["SubCategory"]),
        //                    Visibility = Convert.ToString(rd["Visibility"]),
        //                    featured = Convert.ToString(rd["featured"]),
        //                    OfferId = rd["OfferId"] == DBNull.Value ? "N" : Convert.ToString(rd["OfferId"]),
        //                    seasonSale = Convert.ToString(rd["seasonSale"]),
        //                    ItemSellingType = Convert.ToString(rd["ItemSellingType"]),
        //                    MaxQuantityAllowed = rd["MaxQuantityAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(rd["MaxQuantityAllowed"]),
        //                    Approval = Convert.ToString(rd["Approval"]),
        //                    SellingPrice = Convert.ToString(rd["SellingPrice"] == DBNull.Value ? "0.00" : Convert.ToString(rd["SellingPrice"])),
        //                    Measurement = Convert.ToString(rd["Measurement"]),
        //                    Coupen = rd["Coupen_Disc"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Coupen_Disc"])
        //                });
        //            }
        //            return itemMasterdetail;
        //        }
        //    }
        //}

        //public List<ItemMasters> GetItemManageData(ItemMasters item)
        //{
        //    try
        //    {
        //        List<ItemMasters> itemMasterdetail = new List<ItemMasters>();
        //        var itemstatusver = "";
        //        var maincate = item.MainCategory1 != null ? String.Join(",", item.MainCategory1) : "";
        //        var maincate1 = item.MainCategory1 != null ? "0" : "";
        //        var Cate = item.categoryId != null ? String.Join(",", item.categoryId) : "";
        //        var cate1 = item.categoryId != null ? "0" : "";
        //        var SubCategory = item.subcategory != null ? String.Join(",", item.subcategory) : "";
        //        var SubCategory1 = item.subcategory != null ? "0" : "";
        //        var Supplier = item.supplierid != null ? String.Join(",", item.supplierid) : "";
        //        var Supplier1 = item.supplierid != null ? "0" : "";
        //        var Featured = item.featured1 != null ? String.Join(",", item.featured1) : "";
        //        var Featured1 = item.featured1 != null ? "0" : "";
        //        var KotPrinted = item.KotPrintedId;
        //        var status = item.ItemStatus == 0 ? 1 : item.ItemStatus;
        //        if (status == 0 || status == 1)
        //        {
        //            itemstatusver = "Approved";
        //        }
        //        if (status == 2)
        //        {
        //            itemstatusver = "Pending";
        //        }

        //        #region inlinequery
        //        var query = "";
        //        if (KotPrinted == 0)
        //        {
        //            query = "SELECT Itm.Id,Itm.ItemId,Itm.PluName,[dbo].[GetCategoryName](Itm.Category) as Category,[dbo].[GetSubCategoryName](Itm.SubCategory) as SubCategory,itm.Visibility,Itm.featured,[dbo].[Item_offer] (Itm.OfferId) as OfferId,Itm.seasonSale, " +
        //                   " Itm.ItemSellingType,Itm.MaxQuantityAllowed,Itm.Approval," +
        //                   "(select top 1  isnull(p.SellingPrice,0)  from PriceList p inner join ItemsMaster i on i.ItemId= p.ItemId where i.ItemId=Itm .ItemId)  as SellingPrice,Itm.Measurement,Itm.Coupen_Disc FROM[dbo].[ItemsMaster] Itm where (" + maincate1 + " IS NULL OR  Itm.MainCategory IN (" + maincate + ")) AND (" + cate1 + " IS NULL OR Itm.Category IN (" + Cate + ")) AND " +
        //                   " (" + SubCategory1 + " IS NULL OR Itm.SubCategory IN(" + SubCategory + ")) AND (" + Supplier1 + " IS NULL OR Itm.Supplier IN(" + Supplier + ")) AND (" + Featured1 + " IS NULL OR Itm.featured IN(" + Featured + ")) AND (Itm.Approval In ('" + itemstatusver + "'))";
        //        }
        //        else
        //        {
        //            query = "SELECT Itm.Id,Itm.ItemId,Itm.PluName,[dbo].[GetCategoryName](Itm.Category) as Category,[dbo].[GetSubCategoryName](Itm.SubCategory) as SubCategory,itm.Visibility,Itm.featured,[dbo].[Item_offer] (Itm.OfferId) as OfferId,Itm.seasonSale, " +
        //                   " Itm.ItemSellingType,Itm.MaxQuantityAllowed,Itm.Approval," +
        //                   "(select top 1  isnull(p.SellingPrice,0)  from PriceList p inner join ItemsMaster i on i.ItemId= p.ItemId where i.ItemId=Itm .ItemId)  as SellingPrice,Itm.Measurement,Itm.Coupen_Disc FROM[dbo].[ItemsMaster] Itm where (" + maincate1 + " IS NULL OR  Itm.MainCategory IN (" + maincate + ")) AND (" + cate1 + " IS NULL OR Itm.Category IN (" + Cate + ")) AND " +
        //                   " (" + SubCategory1 + " IS NULL OR Itm.SubCategory IN(" + SubCategory + ")) AND (" + Supplier1 + " IS NULL OR Itm.Supplier IN(" + Supplier + ")) AND (" + Featured1 + " IS NULL OR Itm.featured IN(" + Featured + "))  AND (Itm.KOT_Print = " + KotPrinted + " ) AND (Itm.Approval In ('" + itemstatusver + "'))";
        //        }

        //        #endregion

        //        using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
        //        //using (SqlCommand cmd = new SqlCommand("[dbo].[usp_getItemManageData]", con))
        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            //cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@itemstatusver", SqlDbType.VarChar, 20).Value = itemstatusver;
        //            cmd.Parameters.Add("@KotPrinted", SqlDbType.Int).Value = KotPrinted;
        //            if (SubCategory1.Length > 1)
        //                cmd.Parameters.Add("@SubCategory1", SqlDbType.VarChar, 20).Value = SubCategory1;
        //            if (SubCategory.Length > 1)
        //                cmd.Parameters.Add("@SubCategory", SqlDbType.VarChar, 20).Value = SubCategory;
        //            if (Supplier1.Length > 1)
        //                cmd.Parameters.Add("@Supplier1", SqlDbType.VarChar, 20).Value = Supplier1;
        //            if (Supplier.Length > 1)
        //                cmd.Parameters.Add("@Supplier", SqlDbType.VarChar, 20).Value = Supplier;
        //            if (Featured1.Length > 1)
        //                cmd.Parameters.Add("@Featured1", SqlDbType.VarChar, 20).Value = Featured1;
        //            if (Featured.Length > 1)
        //                cmd.Parameters.Add("@Featured", SqlDbType.VarChar, 20).Value = Featured;
        //            if (maincate1.Length > 1)
        //                cmd.Parameters.Add("@maincate1", SqlDbType.VarChar, 20).Value = maincate1;
        //            if (maincate.Length > 1)
        //                cmd.Parameters.Add("@maincate", SqlDbType.VarChar, 20).Value = maincate;
        //            if (cate1.Length > 1)
        //                cmd.Parameters.Add("@cate1", SqlDbType.VarChar, 20).Value = cate1;
        //            if (Cate.Length > 1)
        //                cmd.Parameters.Add("@Cate", SqlDbType.VarChar, 20).Value = Cate;
        //            con.Open();
        //            using (SqlDataReader rd = cmd.ExecuteReader())
        //            {
        //                while (rd.Read())
        //                {
        //                    itemMasterdetail.Add(new ItemMasters
        //                    {
        //                        Id = Convert.ToInt32(rd["Id"]),
        //                        ItemId = Convert.ToString(rd["ItemId"]),
        //                        PluName = Convert.ToString(rd["PluName"]),
        //                        ////Barcode = Convert.ToString(rd["Barcode"]),
        //                        ////MainCategory = Convert.ToString(rd["MainCategory"]),
        //                        Category = Convert.ToString(rd["Category"]),
        //                        SubCategory = Convert.ToString(rd["SubCategory"]),
        //                        Visibility = Convert.ToString(rd["Visibility"]),
        //                        featured = Convert.ToString(rd["featured"]),
        //                        OfferId = rd["OfferId"] == DBNull.Value ? "N" : Convert.ToString(rd["OfferId"]),
        //                        seasonSale = Convert.ToString(rd["seasonSale"]),
        //                        ItemSellingType = Convert.ToString(rd["ItemSellingType"]),
        //                        MaxQuantityAllowed = rd["MaxQuantityAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(rd["MaxQuantityAllowed"]),
        //                        Approval = Convert.ToString(rd["Approval"]),
        //                        SellingPrice = Convert.ToString(rd["SellingPrice"] == DBNull.Value ? "0.00" : Convert.ToString(rd["SellingPrice"])),
        //                        Measurement = Convert.ToString(rd["Measurement"]),
        //                        Coupen = rd["Coupen_Disc"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Coupen_Disc"])
        //                    });
        //                }
        //                return itemMasterdetail;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        public List<SelectListItem> GetSubCategoryList(string id)
        {
            try
            {
                List<SelectListItem> GetSubCategorySelectList = new List<SelectListItem>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[GetSubCategoryList1]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string text = Convert.ToString(rd["Name"]);
                            string Id = Convert.ToString(rd["SubCategoryId"]);
                            GetSubCategorySelectList.Add(new SelectListItem()
                            {
                                Text = text,
                                Value = Id,
                            });
                        }
                        return GetSubCategorySelectList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ItemMasters GetItemCountDetail()
        {
            ItemMasters ItemCountDetail = new ItemMasters();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Item_GetTotalItemCount]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        ItemCountDetail = new ItemMasters
                        {
                            Activeitem = Convert.ToInt32(rd["Activeitem"]),
                            PendingApproval = Convert.ToInt32(rd["PendingApproval"]),
                            Featureditem = Convert.ToInt32(rd["Featureditem"]),
                            coupenexcluded = Convert.ToInt32(rd["coupenexcluded"]),
                            Deleteditem = Convert.ToInt32(rd["DeletedItem"]),
                        };
                    }
                    return ItemCountDetail;
                }
            }
        }
        public ItemMasters GetItemMasterDetail(int Id)
        {
            try
            {
                ItemMasters ItemMasterDetail = new ItemMasters();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMaster_GetItemDetail]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            ItemMasterDetail = new ItemMasters
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                ItemId = Convert.ToString(rd["ItemId"]),
                                ItemSellingType = Convert.ToString(rd["ItemSellingType"]),
                                imagecdnpath = Convert.ToString(rd["ImagePath"]),
                                ItemType = Convert.ToString(rd["ItemType"]),
                                Visibility = Convert.ToString(rd["Visibility"]),
                                PluName = Convert.ToString(rd["PluName"]),
                                PluCode = Convert.ToString(rd["PluCode"]),
                                MainCategory = Convert.ToString(rd["MainCategory"]),
                                Category = Convert.ToString(rd["Category"]),
                                SubCategory = Convert.ToString(rd["SubCategory"]),
                                foodSegment = Convert.ToString(rd["foodSegment"]),
                                Tag = Convert.ToString(rd["Tag"]),
                                Weight = Convert.ToString(rd["Weight"]),
                                Measurement = Convert.ToString(rd["Measurement"]),
                                NetWeight = Convert.ToString(rd["NetWeight"]),
                                MaxQuantityAllowed = Convert.ToInt32(rd["MaxQuantityAllowed"]),
                                SellingVarience = Convert.ToString(rd["SellingVarience"]),
                                offer_type = Convert.ToString(rd["offer_type"]),
                                Supplier = Convert.ToString(rd["Supplier"]),
                                Brand = Convert.ToString(rd["Brand"]),
                                Purchaseprice = Convert.ToString(rd["Purchaseprice"]),
                                Wastage = Convert.ToString(rd["Wastage"]),
                                SellingPrice = Convert.ToString(rd["SellingPrice"]),
                                MarketPrice = Convert.ToString(rd["MarketPrice"]),
                                ProfitMargin = Convert.ToString(rd["ProfitMargin"]),
                                ProfitPrice = Convert.ToString(rd["ProfitPrice"]),
                                ActualCost = Convert.ToString(rd["ActualCost"]),
                                seasonSale = Convert.ToString(rd["seasonSale"]),
                                Description = Convert.ToString(rd["Description"]),
                                LongDescription = Convert.ToString(rd["LongDescription"]),
                                CreatedName = Convert.ToString(rd["CreatedName"]),
                                PromoVideoLink = Convert.ToString(rd["PromoVideoLink"]),
                                Approval = Convert.ToString(rd["Approval"]),
                                featured = Convert.ToString(rd["featured"]),
                                Relationship = rd["Relation"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Relation"]),
                                ParentId = rd["Parent_ItemId"] == DBNull.Value ? "" : Convert.ToString(rd["Parent_ItemId"]),
                                ParentName = rd["ParentName"] == DBNull.Value ? "" : Convert.ToString(rd["ParentName"]),
                                ProductCost = rd["Item_Cost"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Item_Cost"]),
                                vat_per = rd["vat_per"] as string,
                                gst_per = rd["gst_per"] as string,
                                sgst_per = rd["sgst_per"] as string,
                                cgst_per = rd["cgst_per"] as string,
                                HSN_Code = rd["HSN_Code"] == DBNull.Value ? "0" : Convert.ToString(rd["HSN_Code"]),
                                Tax = rd["Is_Tax_Free"] == DBNull.Value ? "Yes" : Convert.ToString(rd["Is_Tax_Free"]),
                                Coupen = rd["Coupen_Disc"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Coupen_Disc"]),
                                KOT_Print = Convert.ToInt32(rd["KOT_Print"] == DBNull.Value ? 0 : Convert.ToInt32(rd["KOT_Print"])),
                                FoodType = Convert.ToString(rd["Food_Type"] == DBNull.Value ? "OTH03" : Convert.ToString(rd["Food_Type"])),
                                FoodSubType = Convert.ToString(rd["Food_SubType"] == DBNull.Value ? "OTH034" : Convert.ToString(rd["Food_SubType"])),
                                Check_Speical = Convert.ToString(rd["Check_Specail"] == DBNull.Value ? "N" : Convert.ToString(rd["Check_Specail"])),
                                Spicy_Level = Convert.ToInt32(rd["Spicy_Level"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Spicy_Level"])),
                                IsSpecialDay = Convert.ToString(rd["IsSpecialDay"] == DBNull.Value ? "0" : Convert.ToString(rd["IsSpecialDay"])),
                                AvailableDay = Convert.ToInt32(rd["AvailableDay"] == DBNull.Value ? -1 : Convert.ToInt32(rd["AvailableDay"])),
                                MealTimeType = Convert.ToString(rd["MealTimeType"] == DBNull.Value ? "0" : Convert.ToString(rd["MealTimeType"])),
                                StartTime = Convert.ToString(rd["StartTime"] == DBNull.Value ? "00:00" : Convert.ToString(rd["StartTime"])),
                                EndTime = Convert.ToString(rd["EndTime"] == DBNull.Value ? "00:00" : Convert.ToString(rd["EndTime"])),
                                PreparationTime = Convert.ToString(rd["PreparationTime"] == DBNull.Value ? "00 Min - 00 Hour" : Convert.ToString(rd["PreparationTime"])),
                                MeasuredIn = Convert.ToString(rd["Item_MeasuredIn"] == DBNull.Value ? "0" : Convert.ToString(rd["Item_MeasuredIn"])),
                                ImagesCount = Convert.ToInt32(rd["ImagesCount"] == DBNull.Value ? 0 : Convert.ToInt32(rd["ImagesCount"])),
                                DisplayWithImg = Convert.ToBoolean(rd["displayWithImg"] == DBNull.Value ? false : Convert.ToBoolean(rd["displayWithImg"])),
                                //AvalStartDate = Convert.ToDateTime(rd["AvailabilityStartDate"] == DBNull.Value ? DateTime.Now : rd["AvailabilityStartDate"]),
                                //AvalEndDate = Convert.ToDateTime(rd["AvailabilityEndDate"] == DBNull.Value ? DateTime.Now : rd["AvailabilityEndDate"])
                            };
                        }
                        return ItemMasterDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ItemMasters HubGetItemMasterDetail(int Id)
        {
            ItemMasters ItemMasterDetail = new ItemMasters();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[hubItemMaster_GetItemDetail]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        ItemMasterDetail = new ItemMasters
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            ItemId = Convert.ToString(rd["ItemId"]),
                            ItemSellingType = Convert.ToString(rd["ItemSellingType"]),
                            ItemType = Convert.ToString(rd["ItemType"]),
                            Visibility = Convert.ToString(rd["Visibility"]),
                            PluName = Convert.ToString(rd["PluName"]),
                            PluCode = Convert.ToString(rd["PluCode"]),
                            MainCategory = Convert.ToString(rd["MainCategory"]),
                            Category = Convert.ToString(rd["Category"]),
                            SubCategory = Convert.ToString(rd["SubCategory"]),
                            foodSegment = Convert.ToString(rd["foodSegment"]),
                            Tag = Convert.ToString(rd["Tag"]),
                            Weight = Convert.ToString(rd["Weight"]),
                            Measurement = Convert.ToString(rd["Measurement"]),
                            NetWeight = Convert.ToString(rd["NetWeight"]),
                            MaxQuantityAllowed = Convert.ToInt32(rd["MaxQuantityAllowed"]),
                            SellingVarience = Convert.ToString(rd["SellingVarience"]),
                            offer_type = Convert.ToString(rd["offer_type"]),
                            Supplier = Convert.ToString(rd["Supplier"]),
                            Brand = Convert.ToString(rd["Brand"]),
                            Purchaseprice = Convert.ToString(rd["Purchaseprice"]),
                            Wastage = Convert.ToString(rd["Wastage"]),
                            SellingPrice = Convert.ToString(rd["SellingPrice"]),
                            MarketPrice = Convert.ToString(rd["MarketPrice"]),
                            ProfitMargin = Convert.ToString(rd["ProfitMargin"]),
                            ProfitPrice = Convert.ToString(rd["ProfitPrice"]),
                            ActualCost = Convert.ToString(rd["ActualCost"]),
                            seasonSale = Convert.ToString(rd["seasonSale"]),
                            Description = Convert.ToString(rd["Description"]),
                            LongDescription = Convert.ToString(rd["LongDescription"]),
                            CreatedName = Convert.ToString(rd["CreatedName"]),
                            PromoVideoLink = Convert.ToString(rd["PromoVideoLink"]),
                            Approval = Convert.ToString(rd["Approval"]),
                            featured = Convert.ToString(rd["featured"]),
                            Relationship = rd["Relation"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Relation"]),
                            ParentId = rd["Parent_ItemId"] == DBNull.Value ? "" : Convert.ToString(rd["Parent_ItemId"]),
                            ParentName = rd["ParentName"] == DBNull.Value ? "" : Convert.ToString(rd["ParentName"]),
                            ProductCost = rd["Item_Cost"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Item_Cost"]),
                            vat_per = rd["vat_per"] as string,
                            gst_per = rd["gst_per"] as string,
                            sgst_per = rd["sgst_per"] as string,
                            cgst_per = rd["cgst_per"] as string,
                            HSN_Code = rd["HSN_Code"] == DBNull.Value ? "0" : Convert.ToString(rd["HSN_Code"]),
                            Tax = rd["Is_Tax_Free"] == DBNull.Value ? "Yes" : Convert.ToString(rd["Is_Tax_Free"]),
                            Coupen = rd["Coupen_Disc"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Coupen_Disc"]),
                            KOT_Print = Convert.ToInt32(rd["KOT_Print"] == DBNull.Value ? 0 : Convert.ToInt32(rd["KOT_Print"])),
                            FoodType = Convert.ToString(rd["Food_Type"] == DBNull.Value ? "OTH03" : Convert.ToString(rd["Food_Type"])),
                            FoodSubType = Convert.ToString(rd["Food_SubType"] == DBNull.Value ? "OTH034" : Convert.ToString(rd["Food_SubType"])),
                            Check_Speical = Convert.ToString(rd["Checf_Specail"] == DBNull.Value ? "N" : Convert.ToString(rd["Checf_Specail"])),
                            Spicy_Level = Convert.ToInt32(rd["Spicy_Level"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Spicy_Level"])),
                            IsSpecialDay = Convert.ToString(rd["IsSpecialDay"] == DBNull.Value ? "0" : Convert.ToString(rd["IsSpecialDay"])),
                            AvailableDay = Convert.ToInt32(rd["AvailableDay"] == DBNull.Value ? -1 : Convert.ToInt32(rd["AvailableDay"])),
                            MealTimeType = Convert.ToString(rd["MealTimeType"] == DBNull.Value ? "0" : Convert.ToString(rd["MealTimeType"])),
                            StartTime = Convert.ToString(rd["StartTime"] == DBNull.Value ? "00:00" : Convert.ToString(rd["StartTime"])),
                            EndTime = Convert.ToString(rd["EndTime"] == DBNull.Value ? "00:00" : Convert.ToString(rd["EndTime"])),
                            PreparationTime = Convert.ToString(rd["PreparationTime"] == DBNull.Value ? "00 Min - 00 Hour" : Convert.ToString(rd["PreparationTime"])),
                            MeasuredIn = Convert.ToString(rd["Item_MeasuredIn"] == DBNull.Value ? "0" : Convert.ToString(rd["Item_MeasuredIn"])),
                        };
                    }
                    return ItemMasterDetail;
                }
            }
        }
        public string UpdateItemMaster(ItemMasters itemMasters)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMaster_Update]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable odt = new DataTable();
                    odt.Columns.Add("AvailableDay", typeof(int));
                    if (itemMasters.AvailableDayS != null)
                    {
                        foreach (var o in itemMasters.AvailableDayS)
                        {
                            odt.Rows.Add(o);
                        }
                    }
                    cmd.Parameters.Add("@DayList", SqlDbType.Structured).Value = odt;
                    cmd.Parameters.Add("@PluName", SqlDbType.NVarChar, 200).Value = itemMasters.PluName;
                    cmd.Parameters.Add("@imagecdnpath", SqlDbType.NVarChar, -1).Value = itemMasters.imagecdnpath;
                    cmd.Parameters.Add("@PluCode", SqlDbType.VarChar, 100).Value = itemMasters.PluCode;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = itemMasters.Description == null ? "NA" : itemMasters.Description;
                    cmd.Parameters.Add("@Category", SqlDbType.VarChar, 100).Value = itemMasters.Category;
                    cmd.Parameters.Add("@SubCategory", SqlDbType.VarChar, 100).Value = itemMasters.SubCategory;
                    cmd.Parameters.Add("@Measurement", SqlDbType.VarChar, 50).Value = itemMasters.Measurement;
                    cmd.Parameters.Add("@Weight", SqlDbType.Float).Value = itemMasters.Weight == null ? "0" : itemMasters.Weight;
                    cmd.Parameters.Add("@Purchaseprice", SqlDbType.VarChar, 50).Value = itemMasters.Purchaseprice == null ? "0.00" : itemMasters.Purchaseprice;
                    cmd.Parameters.Add("@Wastage", SqlDbType.VarChar, 50).Value = itemMasters.Wastage == null ? "0.00" : itemMasters.Wastage;
                    cmd.Parameters.Add("@SellingPrice", SqlDbType.VarChar, 200).Value = itemMasters.SellingPrice == null ? "0.00" : itemMasters.SellingPrice;
                    cmd.Parameters.Add("@MarketPrice", SqlDbType.VarChar, 50).Value = itemMasters.MarketPrice == null ? "0.00" : itemMasters.MarketPrice;
                    cmd.Parameters.Add("@ProfitPrice", SqlDbType.VarChar, 20).Value = itemMasters.ProfitPrice == null ? "0.00" : itemMasters.ProfitPrice;
                    cmd.Parameters.Add("@ActualCost", SqlDbType.VarChar, 20).Value = itemMasters.ActualCost == null ? "0.00" : itemMasters.ActualCost;
                    cmd.Parameters.Add("@seasonSale", SqlDbType.VarChar, 10).Value = itemMasters.seasonSale == null ? "Y" : itemMasters.seasonSale;
                    cmd.Parameters.Add("@offer_type", SqlDbType.VarChar, 10).Value = itemMasters.offer_type;
                    cmd.Parameters.Add("@foodSegment", SqlDbType.VarChar, 10).Value = itemMasters.foodSegment == null ? "NA" : itemMasters.foodSegment;
                    cmd.Parameters.Add("@ItemSellingType", SqlDbType.VarChar, 20).Value = itemMasters.ItemSellingType == null ? "NA" : itemMasters.ItemSellingType;
                    cmd.Parameters.Add("@LongDescription", SqlDbType.NVarChar).Value = itemMasters.LongDescription == null ? "NA" : itemMasters.LongDescription;
                    cmd.Parameters.Add("@Supplier", SqlDbType.VarChar, 100).Value = itemMasters.Supplier;
                    cmd.Parameters.Add("@MainCategory", SqlDbType.VarChar, 30).Value = itemMasters.MainCategory;
                    cmd.Parameters.Add("@ProfitMargin", SqlDbType.VarChar, 50).Value = itemMasters.ProfitMargin == null ? "0.00" : itemMasters.ProfitMargin;
                    cmd.Parameters.Add("@StockType", SqlDbType.VarChar, 15).Value = itemMasters.StockType == null ? "NA" : itemMasters.StockType;
                    cmd.Parameters.Add("@NetWeight", SqlDbType.Float).Value = itemMasters.NetWeight;
                    cmd.Parameters.Add("@Tag", SqlDbType.VarChar, 30).Value = itemMasters.Tag;
                    cmd.Parameters.Add("@ItemType", SqlDbType.VarChar, 10).Value = itemMasters.ItemType == null ? "NA" : itemMasters.ItemType;
                    cmd.Parameters.Add("@Visibility", SqlDbType.VarChar).Value = itemMasters.Visibility == null ? "0" : itemMasters.Visibility;
                    cmd.Parameters.Add("@SellingVarience", SqlDbType.VarChar).Value = itemMasters.SellingVarience == null ? "NA" : itemMasters.SellingVarience;
                    cmd.Parameters.Add("@Approval", SqlDbType.VarChar, 100).Value = itemMasters.Approval == null ? (object)DBNull.Value : itemMasters.Approval;
                    cmd.Parameters.Add("@PromoVideoLink", SqlDbType.VarChar, 100).Value = itemMasters.PromoVideoLink == null ? "NA" : itemMasters.PromoVideoLink;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 100).Value = itemMasters.ItemId;
                    cmd.Parameters.Add("@updatedby", SqlDbType.VarChar, 100).Value = itemMasters.LastUpdatedBy;
                    cmd.Parameters.Add("@Relation", SqlDbType.Int).Value = itemMasters.Relationship;
                    cmd.Parameters.Add("@ParentId", SqlDbType.VarChar, 50).Value = itemMasters.ParentId == null ? "NA" : itemMasters.ParentId;
                    cmd.Parameters.Add("@ItemCost", SqlDbType.Float).Value = itemMasters.ProductCost == null ? 0.00 : itemMasters.ProductCost;
                    cmd.Parameters.Add("@GST", SqlDbType.VarChar, 20).Value = itemMasters.gst_per == null ? "NA" : itemMasters.gst_per;
                    cmd.Parameters.Add("@SGST", SqlDbType.VarChar, 20).Value = itemMasters.sgst_per == null ? "NA" : itemMasters.sgst_per;
                    cmd.Parameters.Add("@CGST", SqlDbType.VarChar, 20).Value = itemMasters.cgst_per == null ? "NA" : itemMasters.cgst_per;
                    cmd.Parameters.Add("@HSNCode", SqlDbType.VarChar, 200).Value = itemMasters.HSN_Code == null ? "NA" : itemMasters.HSN_Code;
                    cmd.Parameters.Add("@Tax", SqlDbType.VarChar, 10).Value = itemMasters.Tax == null ? "Yes" : itemMasters.Tax;
                    cmd.Parameters.Add("@KOT_Print", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@FoodType", SqlDbType.VarChar, 50).Value = itemMasters.FoodType == null ? "NA" : itemMasters.FoodType;
                    cmd.Parameters.Add("@FoodSubType", SqlDbType.VarChar, 50).Value = itemMasters.FoodSubType == null ? "NA" : itemMasters.FoodSubType;
                    cmd.Parameters.Add("@IsSpecialDay", SqlDbType.VarChar, 5).Value = itemMasters.IsSpecialDay == null ? "No" : itemMasters.IsSpecialDay;
                    cmd.Parameters.Add("@MealTimeType", SqlDbType.VarChar, 50).Value = itemMasters.MealTimeType == null ? "NO TIMEZONE" : itemMasters.MealTimeType;
                    cmd.Parameters.Add("@StartTime", SqlDbType.VarChar, 20).Value = itemMasters.StartTime;
                    cmd.Parameters.Add("@EndTime", SqlDbType.VarChar, 20).Value = itemMasters.EndTime;
                    cmd.Parameters.Add("@PreparationTime", SqlDbType.VarChar, 70).Value = itemMasters.PreparationTime == null ? "10 Min - 20 Min" : itemMasters.PreparationTime;
                    cmd.Parameters.Add("@SpciyLevel", SqlDbType.Int).Value = 0 == 0 ? 0 : 0;
                    cmd.Parameters.Add("@ImagesCount", SqlDbType.Int).Value = itemMasters.ImagesCount;
                    con.Open();
                    return Convert.ToString(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int UpdatePriceMaster(ItemMasters itemMasters)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMaster_UpdatePriceList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemId", itemMasters.ItemId);
                cmd.Parameters.AddWithValue("@PluName", itemMasters.PluName);
                cmd.Parameters.AddWithValue("@Weight", itemMasters.Weight);
                cmd.Parameters.AddWithValue("@Measurement", itemMasters.Measurement);
                cmd.Parameters.AddWithValue("@WasteagePerc", itemMasters.Wastage);
                cmd.Parameters.AddWithValue("@PurchasePrice", itemMasters.Purchaseprice);
                cmd.Parameters.AddWithValue("@SellingPrice", itemMasters.SellingPrice);
                cmd.Parameters.AddWithValue("@MarketPrice", itemMasters.MarketPrice);
                cmd.Parameters.AddWithValue("@seasonSale", itemMasters.seasonSale);
                cmd.Parameters.AddWithValue("@ProfitPrice", itemMasters.ProfitPrice);
                cmd.Parameters.AddWithValue("@ActualCost", itemMasters.ActualCost);
                cmd.Parameters.AddWithValue("@offer_type", itemMasters.offer_type);
                cmd.Parameters.AddWithValue("@foodSegment", itemMasters.foodSegment);
                cmd.Parameters.AddWithValue("@updatedby", itemMasters.LastUpdatedBy);
                cmd.Parameters.AddWithValue("@ProfitMargin", itemMasters.ProfitMargin);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int UpdateItemFeatured(string feature, string itemId, int type, int Id)
        {
            feature = ((feature == "true") ? "Y" : "N");
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMaster_Featured1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 100).Value = Id;
                cmd.Parameters.Add("@featured", SqlDbType.VarChar, 100).Value = feature;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 100).Value = itemId;
                cmd.Parameters.Add("@type", SqlDbType.Int).Value = type;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int UpdateItemCheckSp(string checkSp, string itemId, int type, int Id)
        {
            checkSp = ((checkSp == "true") ? "Y" : "N");
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMaster_CheckSp1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 100).Value = Id;
                cmd.Parameters.Add("@checkSp", SqlDbType.VarChar, 100).Value = checkSp;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 100).Value = itemId;
                cmd.Parameters.Add("@type", SqlDbType.Int).Value = type;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int UpdateItemApproval(string itemId, int Id, string approval, string UpdatedBy, int type)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[UpdateItemApproval]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 200).Value = itemId;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@Approval", SqlDbType.VarChar, 100).Value = approval;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 100).Value = UpdatedBy;
                cmd.Parameters.Add("@type", SqlDbType.Int).Value = type;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public string CheckUniquePlucode(string PluCode)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMaster_CheckuniquePlucode]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PluCode", SqlDbType.VarChar, 100).Value = PluCode;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    var PluCodeName = "";
                    if (rd.Read())
                    {
                        PluCodeName = Convert.ToString(rd["PluCode"]);
                    }
                    return PluCodeName;
                }
            }
        }
        public void UpdateItemFields(ItemFields list, string updatedBy)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Item_UpdateField]", con))
            {
                con.Open();
                DataTable odt = new DataTable();
                odt.Columns.Add("ItemId", typeof(int));
                odt.Columns.Add("Featured", typeof(string));
                odt.Columns.Add("Coupen", typeof(int));
                odt.Columns.Add("Avaibility", typeof(string));
                odt.Columns.Add("Approval", typeof(string));
                if (list.ItemId != null)
                    for (int i = 0; i < list.ItemId.Count; i++)
                    {
                        var itemId = list.ItemId[i];
                        var featured = list.Featured[i];
                        var season = list.Avability[i];
                        var couopen = list.Coupen[i];
                        var approval = list.Status[i];
                        odt.Rows.Add(itemId, featured, couopen, season, approval);
                    }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tbllist", SqlDbType.Structured).Value = odt;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 50).Value = updatedBy;
                cmd.ExecuteNonQuery();
            }
        }
        //public List<SelectListItem> GetItemTypeList()
        //{
        //    List<SelectListItem> GetItemTypeList = new List<SelectListItem>();
        //    using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand("[dbo].[Pricelist_ItemTypeOffer]", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();
        //        using (SqlDataReader rd = cmd.ExecuteReader())
        //        {
        //            while (rd.Read())
        //            {
        //                string text = Convert.ToString(rd["Name"]);
        //                string Id = Convert.ToString(rd["Id"]);
        //                GetItemTypeList.Add(new SelectListItem()
        //                {
        //                    Text = text,
        //                    Value = Id,
        //                });
        //            }
        //            return GetItemTypeList;
        //        }
        //    }
        //}
        //public List<ItemMasters> GetPricelistData(ItemMasters item)
        //{
        //    List<ItemMasters> PriceItemListDetail = new List<ItemMasters>();
        //    var query =
        //            @"SELECT PriceId,p.ItemId,p.PluName,p.Measurement, p.OrderQty, p.WasteagePerc, p.PurchasePrice, p.SellingPrice, p.TotalPrice, p.SellingProfitPer,
        //                            p.ProfitMargin,p.MarketPrice,p.seasonSale,p.Id,i.ItemId,
        //                            i.Approval,[dbo].[GetItemPrice](p.ItemId,p.SellingPrice) as discountPrice,[dbo].[GetofferDiscount](p.ItemId) as OfferDiscount  FROM PriceList p
        //                            Inner Join ItemsMaster i ON p.ItemId = i.ItemId
        //                            Where i.Approval='Approved' ORDER BY p.Id desc";
        //    using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query, con))

        //            try
        //        {
        //            con.Open();
        //                using (SqlDataReader rd = cmd.ExecuteReader())
        //                {
        //                    while (rd.Read())
        //                    {
        //                        PriceItemListDetail.Add(new ItemMasters
        //                        {
        //                            PriceId = Convert.ToString(rd[0] == DBNull.Value ? "0" : rd[0]),
        //                            ItemId = Convert.ToString(rd[1] == DBNull.Value ? "0" : rd[1]),
        //                            PluName = Convert.ToString(rd[2] == DBNull.Value ? "0" : rd[2]),
        //                            Measurement = Convert.ToString(rd[3] == DBNull.Value ? "0" : rd[3]),
        //                            OrderQty = Convert.ToString(rd[4] == DBNull.Value ? "0" : rd[4]),
        //                            Wastage = Convert.ToString(rd[5] == DBNull.Value ? "0" : rd[5]),
        //                            Purchaseprice = Convert.ToString(rd[6] == DBNull.Value ? "0" : rd[6]),
        //                            SellingPrice = Convert.ToString(rd[7] == DBNull.Value ? "0" : rd[7]),
        //                            ActualCost = Convert.ToString(rd[8] == DBNull.Value ? "0" : rd[8]),
        //                            SellingProfitPer = Convert.ToString(rd[9] == DBNull.Value ? "0" : rd[9]),
        //                            ProfitMargin = Convert.ToString(rd[10] == DBNull.Value ? "0" : rd[10]),
        //                            MarketPrice = Convert.ToString(rd[11] == DBNull.Value ? "0" : rd[11]),
        //                            seasonSale = Convert.ToString(rd[12] == DBNull.Value ? "0" : rd[12]),
        //                            Id = Convert.ToInt32(rd[13] == DBNull.Value ? "0" : rd[13]),
        //                            DiscountPrice = Convert.ToDouble(rd["discountPrice"] == DBNull.Value ? 0 : rd["discountPrice"]),
        //                            OfferDiscount = Convert.ToDouble(rd["OfferDiscount"] == DBNull.Value ? 0 : rd["OfferDiscount"]),
        //                        });
        //                    }

        //                }
        //                sqlcon.Close();
        //        }
        //        catch (Exception e)
        //        {
        //            return topPriceList;
        //        }
        //    }
        //    return topPriceList;
        //}
        public int UpdateItemAvailability(string season, string itemId, int type, int Id)
        {
            season = ((season == "true") ? "Y" : "N");
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMaster_Availability1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 100).Value = Id;
                cmd.Parameters.Add("@Season", SqlDbType.VarChar, 10).Value = season;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 100).Value = itemId;
                cmd.Parameters.Add("@type", SqlDbType.Int).Value = type;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int UpdateItemCoupen(string coupen, string itemId, int type, int Id)
        {
            int value = ((coupen == "true") ? 1 : 0);
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMaster_Coupen1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 100).Value = Id;
                cmd.Parameters.Add("@Coupen", SqlDbType.Int).Value = value;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 100).Value = itemId;
                cmd.Parameters.Add("@type", SqlDbType.Int).Value = type;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public List<TaxPercentageMst> GetTaxPercentageList()
        {
            try
            {
                List<TaxPercentageMst> PercentageList = new List<TaxPercentageMst>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[Tax_GetPerList]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            PercentageList.Add(new TaxPercentageMst()
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                IGST_Per = Convert.ToDouble(rd["IGST_Per"]),
                                VAT_Per = Convert.ToDouble(rd["VAT_Per"]),
                                SGST_Per = Convert.ToDouble(rd["SGST_Per"]),
                                CGST_Per = Convert.ToDouble(rd["CGST_Per"]),
                            });
                        }
                        return PercentageList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int UpdateHubPrice(Item detail)
        {
            detail.featured = detail.featuredBool == true ? "Y" : "N";
            detail.seasonSale = detail.seasonBool == true ? "Y" : "N";
            detail.CoupenDisc = detail.coupenBool == true ? 1 : 0;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Hub_InsertPriceList1]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Feature", SqlDbType.VarChar, 10).Value = detail.featured;
                cmd.Parameters.Add("@CoupenDisc", SqlDbType.Int).Value = detail.CoupenDisc;
                cmd.Parameters.Add("@SeasonSale", SqlDbType.VarChar, 20).Value = detail.seasonSale;
                cmd.Parameters.Add("@HubId", SqlDbType.VarChar, 50).Value = detail.HubId;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = detail.ItemId;
                cmd.Parameters.Add("@PurchasePrice", SqlDbType.VarChar, 50).Value = detail.Purchaseprice;
                cmd.Parameters.Add("@SellingPrice", SqlDbType.VarChar, 50).Value = detail.SellingPrice;
                cmd.Parameters.Add("@ActualCost", SqlDbType.VarChar, 50).Value = detail.ActualCost;
                cmd.Parameters.Add("@ProfitMargin", SqlDbType.VarChar, 50).Value = detail.ProfitMargin;
                cmd.Parameters.Add("@ProfitPrice", SqlDbType.VarChar, 50).Value = detail.ProfitPrice;
                cmd.Parameters.Add("@MarketPrice", SqlDbType.VarChar, 50).Value = detail.MarketPrice;
                cmd.Parameters.Add("@Wastage", SqlDbType.VarChar, 50).Value = detail.Wastage;
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public List<ItemMasters> GetItemManagePriceData(ItemMasters item)
        {
            List<ItemMasters> itemMasterdetail = new List<ItemMasters>();
            var status = item.itemStatus != null ? String.Join(",", item.itemStatus) : "null";
            var status1 = item.itemStatus != null ? "0" : "null";
            var maincate = item.MainCategory1 != null ? String.Join(",", item.MainCategory1) : "null";
            var maincate1 = item.MainCategory1 != null ? "0" : "null";
            var Cate = item.categoryId != null ? String.Join(",", item.categoryId) : "null";
            var cate1 = item.categoryId != null ? "0" : "null";
            var SubCategory = item.subcategory != null ? String.Join(",", item.subcategory) : "null";
            var SubCategory1 = item.subcategory != null ? "0" : "null";
            var Supplier = item.supplierid != null ? String.Join(",", item.supplierid) : "null";
            var Supplier1 = item.supplierid != null ? "0" : "null";
            var Featured = item.featured1 != null ? String.Join(",", item.featured1) : "null";
            var Featured1 = item.featured1 != null ? "0" : "null";
            var query = "";
            query = "SELECT Itm.Id,Itm.ItemId,Itm.PluName,[dbo].[GetCategoryName](Itm.Category) as Category,[dbo].[GetSubCategoryName](Itm.SubCategory) as SubCategory,itm.Visibility,Itm.featured,[dbo].[Item_offer] (Itm.OfferId) as OfferId,Itm.seasonSale, " +
                   " Itm.ItemSellingType,Itm.MaxQuantityAllowed,Itm.Approval,Itm.SellingPrice,Itm.Measurement,Itm.Coupen_Disc,ISNULL(h.[ItemId],0) AS HubPriceId FROM [dbo].[ItemsMaster] Itm LEFT JOIN [dbo].[tblHub_PriceList] h on Itm.[Id]=h.[ItemId]  where (" + maincate1 + " IS NULL OR  Itm.MainCategory IN (" + maincate + ")) AND (" + cate1 + " IS NULL OR Itm.Category IN (" + Cate + ")) AND " +
                   " (" + SubCategory1 + " IS NULL OR Itm.SubCategory IN(" + SubCategory + ")) AND (" + Supplier1 + " IS NULL OR Itm.Supplier IN(" + Supplier + ")) AND (" + Featured1 + " IS NULL OR Itm.featured IN(" + Featured + "))  AND (" + status1 + " IS NULL OR Itm.Approval IN (" + status + ")) ";
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        itemMasterdetail.Add(new ItemMasters
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            ItemId = Convert.ToString(rd["ItemId"]),
                            PluName = Convert.ToString(rd["PluName"]),
                            Category = Convert.ToString(rd["Category"]),
                            SubCategory = Convert.ToString(rd["SubCategory"]),
                            Visibility = Convert.ToString(rd["Visibility"]),
                            featured = Convert.ToString(rd["featured"]),
                            OfferId = rd["OfferId"] == DBNull.Value ? "N" : Convert.ToString(rd["OfferId"]),
                            seasonSale = Convert.ToString(rd["seasonSale"]),
                            ItemSellingType = Convert.ToString(rd["ItemSellingType"]),
                            MaxQuantityAllowed = rd["MaxQuantityAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(rd["MaxQuantityAllowed"]),
                            Approval = Convert.ToString(rd["Approval"]),
                            SellingPrice = Convert.ToString(rd["SellingPrice"]),
                            Measurement = Convert.ToString(rd["Measurement"]),
                            Coupen = rd["Coupen_Disc"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Coupen_Disc"]),
                            HubPriceId = Convert.ToInt32(rd["HubPriceId"]),
                        });
                    }
                    return itemMasterdetail;
                }
            }
        }
        public List<ItemMasters> GetIncludedHubPrice(string id) //(ItemMasters item)
        {
            List<ItemMasters> itemMasterdetail = new List<ItemMasters>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Hub_IncludedItem", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@hubId", SqlDbType.VarChar).Value = id;
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            itemMasterdetail.Add(new ItemMasters
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                ItemId = Convert.ToString(rd["ItemId"]),
                                PluName = Convert.ToString(rd["PluName"]),
                                Category = Convert.ToString(rd["Category"]),
                                SubCategory = Convert.ToString(rd["SubCategory"]),
                                Visibility = Convert.ToString(rd["Visibility"]),
                                featured = Convert.ToString(rd["featured"]),
                                OfferId = rd["OfferId"] == DBNull.Value ? "N" : Convert.ToString(rd["OfferId"]),
                                seasonSale = Convert.ToString(rd["seasonSale"]),
                                ItemSellingType = Convert.ToString(rd["ItemSellingType"]),
                                MaxQuantityAllowed = rd["MaxQuantityAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(rd["MaxQuantityAllowed"]),
                                Approval = Convert.ToString(rd["Approval"]),
                                SellingPrice = Convert.ToString(rd["SellingPrice"]),
                                Measurement = Convert.ToString(rd["Measurement"]),
                                Coupen = rd["Coupen_Disc"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Coupen_Disc"]),
                                hubId = Convert.ToString(rd["hubId"]),
                            });
                        }
                        return itemMasterdetail;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public List<ProductPriceLog> GetProductPriceLog(string id, string hubId)
        {
            List<ProductPriceLog> GetProductPriceLog = new List<ProductPriceLog>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_PricelogList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 20).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        try
                        {
                            GetProductPriceLog.Add(new ProductPriceLog()
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                PrLogId = Convert.ToString(rd["PrLogId"]),
                                PurchasePrice = Convert.ToInt32(rd["PurchasePrice"]),
                                MarketPrice = Convert.ToInt32(rd["MarketPrice"]),
                                SellingPrice = Convert.ToInt32(rd["SellingPrice"]),
                                OldPurchasePrice = Convert.ToInt32(rd["OldPurchasePrice"]),
                                OldMarketPrice = Convert.ToInt32(rd["OldMarketPrice"]),
                                OldSellingPrice = Convert.ToInt32(rd["OldSellingPrice"]),
                                Size = Convert.ToString(rd["Size"]),
                                OldSize = Convert.ToString(rd["OldSize"]),
                                //Measurement = Convert.ToString(rd["Measurement"]),
                                CreatedOn = Convert.ToDateTime(rd["CreatedOn"]),
                                //EmpId = Convert.ToString(rd["EmpId"]),
                                CreatedBy = Convert.ToString(rd["CreatedBy"]),
                            });
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    return GetProductPriceLog;
                }
            }
        }
        public List<ItemMasters> GetExcludedHubPrice(string id)
        {
            List<ItemMasters> itemMasterdetail = new List<ItemMasters>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Hub_ExcludeItem", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        itemMasterdetail.Add(new ItemMasters
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            ItemId = Convert.ToString(rd["ItemId"]),
                            PluName = Convert.ToString(rd["PluName"]),
                            Category = Convert.ToString(rd["Category"]),
                            SubCategory = Convert.ToString(rd["SubCategory"]),
                            Visibility = Convert.ToString(rd["Visibility"]),
                            featured = Convert.ToString(rd["featured"]),
                            OfferId = rd["OfferId"] == DBNull.Value ? "N" : Convert.ToString(rd["OfferId"]),
                            seasonSale = Convert.ToString(rd["seasonSale"]),
                            ItemSellingType = Convert.ToString(rd["ItemSellingType"]),
                            MaxQuantityAllowed = rd["MaxQuantityAllowed"] == DBNull.Value ? 0 : Convert.ToInt32(rd["MaxQuantityAllowed"]),
                            Approval = Convert.ToString(rd["Approval"]),
                            SellingPrice = Convert.ToString(rd["SellingPrice"]),
                            Measurement = Convert.ToString(rd["Measurement"]),
                            Coupen = rd["Coupen_Disc"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Coupen_Disc"]),
                            HubPriceId = Convert.ToInt32(rd["HubPriceId"]),
                        });
                    }
                    return itemMasterdetail;
                }
            }
        }
        public int DeleteHubItem(string itemId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Hub_DeleteItemId]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = itemId;
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public int DeleteItem(ItemMasters info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[ItemMaster_DeleteItem]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = info.ItemId;
                    cmd.Parameters.Add("@Approval", SqlDbType.VarChar, 20).Value = info.Approval;
                    cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 20).Value = info.LastUpdatedBy;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public async Task<byte[]> FilterExcelofItems(ItemMasters item)
        {
            string fileName = Path.Combine(item.webRootPath, "ItemsOveriview.xlsx");
            FileInfo newFile = new FileInfo(fileName);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(fileName);
            }
            Stream stream = new System.IO.MemoryStream();
            using (var package = new ExcelPackage(newFile))
            {
                var worksheet1 = package.Workbook.Worksheets.Add("ItemsOveriview");
                List<ItemMasters> itemMasterList = new List<ItemMasters>();
                itemMasterList = GetItemManageData(item);
                int rowCount = 2;
                var rowdetail = rowCount;
                #region ExcelHeader
                worksheet1.Cells[rowdetail, 1].Value = "Item Name";
                worksheet1.Cells[rowdetail, 1].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(1).AutoFit();

                worksheet1.Cells[rowdetail, 2].Value = "Category";
                worksheet1.Cells[rowdetail, 2].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(2).AutoFit();

                worksheet1.Cells[rowdetail, 3].Value = "Visibilty";
                worksheet1.Cells[rowdetail, 3].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(3).AutoFit();

                worksheet1.Cells[rowdetail, 4].Value = "Featured";
                worksheet1.Cells[rowdetail, 4].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(4).AutoFit();

                worksheet1.Cells[rowdetail, 5].Value = "Coupen Disc.";
                worksheet1.Cells[rowdetail, 5].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(5).AutoFit();

                worksheet1.Cells[rowdetail, 6].Value = "Availability";
                worksheet1.Cells[rowdetail, 6].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(6).AutoFit();

                worksheet1.Cells[rowdetail, 7].Value = "Sold";
                worksheet1.Cells[rowdetail, 7].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(7).AutoFit();

                worksheet1.Cells[rowdetail, 8].Value = "Max Allowed";
                worksheet1.Cells[rowdetail, 8].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(8).AutoFit();

                worksheet1.Cells[rowdetail, 9].Value = "Status";
                worksheet1.Cells[rowdetail, 9].Style.Font.Bold = true;
                worksheet1.Cells[rowdetail, 9].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                worksheet1.Column(9).AutoFit();
                #endregion
                rowdetail++;
                foreach (var itemlist in itemMasterList)
                {
                    worksheet1.Cells[rowdetail, 1].Value = itemlist.PluName;
                    worksheet1.Cells[rowdetail, 2].Value = itemlist.Category;
                    if (itemlist.Visibility == "3")
                    {
                        worksheet1.Cells[rowdetail, 3].Value = itemlist.Visibility = "Both";
                    }
                    else
                    {
                        worksheet1.Cells[rowdetail, 3].Value = itemlist.Visibility = "Single";
                    }
                    worksheet1.Cells[rowdetail, 4].Value = itemlist.featured;
                    if (itemlist.Coupen == 1)
                    {
                        worksheet1.Cells[rowdetail, 5].Value = itemlist.Description = "Yes";
                    }
                    else
                    {
                        worksheet1.Cells[rowdetail, 5].Value = itemlist.Description = "No";
                    }
                    if (itemlist.Visibility == "3")
                    {
                        worksheet1.Cells[rowdetail, 6].Value = itemlist.Visibility = "Yes";
                    }
                    else
                    {
                        worksheet1.Cells[rowdetail, 6].Value = itemlist.Visibility = "No";
                    }
                    worksheet1.Cells[rowdetail, 7].Value = itemlist.SellingPrice;
                    worksheet1.Cells[rowdetail, 8].Value = itemlist.MaxQuantityAllowed;
                    worksheet1.Cells[rowdetail, 9].Value = itemlist.itemStatus;
                    rowdetail++;
                }
                package.SaveAs(newFile);
                byte[] files = File.ReadAllBytes(fileName);
                File.Delete(fileName);
                return files;
            }
        }
        public List<SelectListItem> GetfoodTypeList()
        {
            try
            {
                List<SelectListItem> GetfoodTypeList = new List<SelectListItem>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[GetfoodList]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string text = Convert.ToString(rd["foodname"]);
                            string Id = Convert.ToString(rd["id"]);
                            GetfoodTypeList.Add(new SelectListItem()
                            {
                                Text = text,
                                Value = Id,
                            });
                        }
                        return GetfoodTypeList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<SelectListItem> GetSubfoods()
        {
            try
            {
                List<SelectListItem> list = new List<SelectListItem>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[GetCategoryList]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            list.Add(new SelectListItem
                            {
                                Value = rd[0] as string,
                                Text = rd[1] as string
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
        public List<SelectListItem> GetSubfoodBymainFood(string foodType)
        {
            List<SelectListItem> GetfoodsubTypeList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetsubfoodListbyfoodType]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@foodType", SqlDbType.VarChar, 50).Value = foodType;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["foodsubName"]);
                        string Id = Convert.ToString(rd["foodsubName"]);
                        GetfoodsubTypeList.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return GetfoodsubTypeList;
                }
            }
        }
        public List<int> GetMappingDaywithItem(string id)
        {
            try
            {
                var result = new List<int>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[GetMappingDayShow]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar, 20).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(Convert.ToInt32(reader[0]));
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<ItemMasters> ProductSizeInfoDetails(string id, string HubId)
        {
            try
            {
                List<ItemMasters> productlist = new List<ItemMasters>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[HubItem_GetSizeDetailsTestForHub]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = id;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            productlist.Add(new ItemMasters
                            {
                                PId = Convert.ToInt32(rd["Id"]),
                                PriceId = Convert.ToString(rd["PriceId"]),
                                ItemId = Convert.ToString(rd["ItemId"]),
                                Weight = Convert.ToString(rd["OrderQty"]),
                                Size = Convert.ToString(rd["Size"]),
                                ItmNetWeight = Convert.ToSingle(rd["ItmNetWeight"]),
                                Measurement = Convert.ToString(rd["Measurement"]),
                                WasteagePerc = rd["WasteagePerc"] == DBNull.Value ? "N" : Convert.ToString(rd["WasteagePerc"]),
                                WastageQty = Convert.ToString(rd["WastageQty"]),
                                Purchaseprice = Convert.ToString(rd["Purchaseprice"]),
                                SellingProfitPer = rd["SellingProfitPer"] == DBNull.Value ? "0.00" : Convert.ToString(rd["SellingProfitPer"]),
                                TotalPrice = Convert.ToString(rd["TotalPrice"]),
                                ProfitMargin = Convert.ToString(rd["ProfitMargin"]),
                                SellingPrice = Convert.ToString(rd["SellingPrice"] == DBNull.Value ? "00.00" : Convert.ToString(rd["SellingPrice"])),
                                MarketPrice = rd["MarketPrice"] == DBNull.Value ? "00.00" : Convert.ToString(rd["MarketPrice"]),
                                seasonSale = Convert.ToString(rd["seasonSale"]),
                                MeasuredIn = Convert.ToString(rd["Item_MeasuredIn"]),
                                Barcode = Convert.ToString(rd["Barcode"])
                            });
                        }
                        return productlist;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<ItemMasters> UnMappedProductSizeInfoDetails(string id, string HubId)
        {
            List<ItemMasters> unmappedproductlist = new List<ItemMasters>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[HubItem_GetSizeDetailsUnMapped]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@HubId", SqlDbType.VarChar).Value = HubId;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        unmappedproductlist.Add(new ItemMasters
                        {
                            PId = Convert.ToInt32(rd["Id"]),
                            PriceId = Convert.ToString(rd["PriceId"]),
                            ItemId = Convert.ToString(rd["ItemId"]),
                            Weight = Convert.ToString(rd["OrderQty"]),
                            Size = Convert.ToString(rd["Size"]),
                            ItmNetWeight = Convert.ToSingle(rd["ItmNetWeight"]),
                            Measurement = Convert.ToString(rd["Measurement"]),
                            WasteagePerc = rd["WasteagePerc"] == DBNull.Value ? "N" : Convert.ToString(rd["WasteagePerc"]),
                            WastageQty = Convert.ToString(rd["WastageQty"]),
                            Purchaseprice = Convert.ToString(rd["Purchaseprice"]),
                            SellingProfitPer = rd["SellingProfitPer"] == DBNull.Value ? "0.00" : Convert.ToString(rd["SellingProfitPer"]),
                            TotalPrice = Convert.ToString(rd["TotalPrice"]),
                            ProfitMargin = Convert.ToString(rd["ProfitMargin"]),
                            SellingPrice = Convert.ToString(rd["SellingPrice"] == DBNull.Value ? "00.00" : Convert.ToString(rd["SellingPrice"])),
                            MarketPrice = rd["MarketPrice"] == DBNull.Value ? "00.00" : Convert.ToString(rd["MarketPrice"]),
                            seasonSale = Convert.ToString(rd["seasonSale"]),
                            MeasuredIn = Convert.ToString(rd["Item_MeasuredIn"]),
                        });
                    }
                    return unmappedproductlist;
                }
            }
        }
        public List<ItemMasters> ProductColorInfoDetails(string id)
        {
            try
            {
                List<ItemMasters> productlist = new List<ItemMasters>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("Item_GetColorDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = id;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            productlist.Add(new ItemMasters
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                ColorId = Convert.ToString(rd["ColorId"]),
                                ItemId = Convert.ToString(rd["ItemId"]),
                                Color = Convert.ToString(rd["Color_Name"]),
                            });
                        }
                        return productlist;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string ItemSizeInfo(ItemMasters variance)
        {
            var c = "";
            SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                sqlcon.Open();
                cmd.Connection = sqlcon;
                cmd.CommandText = "[dbo].[Item_CreteSizeInfo]";
                DataTable dt = new DataTable();
                dt.TableName = "Pricelist_VariantnewM6";
                dt.Columns.Add("ItemId", typeof(string));
                dt.Columns.Add("PluName", typeof(string));
                dt.Columns.Add("OrderQty", typeof(float));
                dt.Columns.Add("Size", typeof(string));
                dt.Columns.Add("ItmNetWeight", typeof(float));
                dt.Columns.Add("Measurement", typeof(string));
                dt.Columns.Add("WasteagePerc", typeof(float));
                dt.Columns.Add("WastageQty", typeof(float));
                dt.Columns.Add("PurchasePrice", typeof(decimal));
                dt.Columns.Add("SellingProfitPer", typeof(decimal));
                dt.Columns.Add("TotalPrice", typeof(decimal));
                dt.Columns.Add("ProfitMargin", typeof(decimal));
                dt.Columns.Add("SellingPrice", typeof(decimal));
                dt.Columns.Add("MarketPrice", typeof(string));
                dt.Columns.Add("CreatedBy", typeof(string));
                dt.Columns.Add("LastUpdatedBy", typeof(string));
                dt.Columns.Add("Hub", typeof(string));
                dt.Columns.Add("Barcode", typeof(string));
                dt.Columns.Add("ImagePath", typeof(string));
                if (variance != null)
                    foreach (var data in variance.ItemSizeInfo)
                    {
                        dt.Rows.Add(
                              data.ItemId
                            , data.PluName
                            , data.Weight
                            , data.Size
                            , data.ItmNetWeight
                            , data.ItmMeasurement
                            , data.WasteagePerc
                            , data.WastageQty
                            , data.Purchaseprice
                            , data.ProfitMargin
                            , data.TotalPrice
                            , data.ProfitPrice
                            , data.SellingPrice
                            , data.MarketPrice
                            , variance.CreatedBy
                            , variance.LastUpdatedBy
                            , variance.Hub
                            , data.Barcode
                            , data.imagecdnpathvariance
                            );
                    }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;
                cmd.Parameters.AddWithValue("@MeasuredIn", variance.MeasuredIn);
                cmd.Parameters.AddWithValue("@displayWithImg", variance.DisplayWithImg);
                cmd.Parameters.AddWithValue("@HubId", variance.Hub);
                cmd.Parameters.AddWithValue("@ItemId", variance.ItemSizeInfo[0].ItemId);
                cmd.Parameters.Add("@tblPricevariantList", SqlDbType.Structured).Value = dt;
                c = Convert.ToString(cmd.ExecuteScalar());
                c = "PRCID0" + c;
            }
            catch (Exception ex)
            {
                return c;
            }
            finally
            {
                if (sqlcon.State != ConnectionState.Closed)
                {
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            return c;
        }
        public bool hubItemSizeInfo(ItemMasters variance)
        {
            SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                sqlcon.Open();
                cmd.Connection = sqlcon;
                cmd.CommandText = "[dbo].[HubItem_CreteSizeInfo]";
                DataTable dt = new DataTable();
                dt.TableName = "Pricelist_Variantnew";
                dt.Columns.Add("ItemId", typeof(string));
                dt.Columns.Add("PluName", typeof(string));
                dt.Columns.Add("OrderQty", typeof(float));
                dt.Columns.Add("Size", typeof(string));
                dt.Columns.Add("ItmNetWeight", typeof(float));
                dt.Columns.Add("Measurement", typeof(string));
                dt.Columns.Add("WasteagePerc", typeof(float));
                dt.Columns.Add("WastageQty", typeof(float));
                dt.Columns.Add("PurchasePrice", typeof(decimal));
                dt.Columns.Add("SellingProfitPer", typeof(decimal));
                dt.Columns.Add("TotalPrice", typeof(decimal));
                dt.Columns.Add("ProfitMargin", typeof(decimal));
                dt.Columns.Add("SellingPrice", typeof(decimal));
                dt.Columns.Add("MarketPrice", typeof(string));
                dt.Columns.Add("CreatedBy", typeof(string));
                dt.Columns.Add("LastUpdatedBy", typeof(string));
                dt.Columns.Add("HubId", typeof(string));
                if (variance != null)
                    foreach (var data in variance.ItemSizeInfo)
                    {
                        dt.Rows.Add(
                              data.ItemId
                            , data.PluName
                            , data.Weight
                            , data.Size
                            , data.ItmNetWeight
                            , data.ItmMeasurement
                            , data.WasteagePerc
                            , data.WastageQty
                            , data.Purchaseprice
                            , data.ProfitMargin
                            , data.TotalPrice
                            , data.ProfitPrice
                            , data.SellingPrice
                            , data.MarketPrice
                            , variance.CreatedBy
                            , variance.LastUpdatedBy
                            , variance.hubId
                            );
                    }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;
                cmd.Parameters.AddWithValue("@MeasuredIn", variance.MeasuredIn);
                cmd.Parameters.AddWithValue("@ItemId", variance.ItemSizeInfo[0].ItemId);
                cmd.Parameters.Add("@tblPricevariantList", SqlDbType.Structured).Value = dt;
                var c = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                if (sqlcon.State != ConnectionState.Closed)
                {
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            return true;
        }
        public int ItemSizeDelete(string priceId, string Condition, string hubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Item_SizeDelete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PriceId", SqlDbType.VarChar, 50).Value = priceId;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = hubId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int ItemWeightQtyDelete(string priceId, string Condition, string hubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Item_WeightQtyDelete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PriceId", SqlDbType.VarChar, 50).Value = priceId;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = hubId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int CreateColor(ItemColorInfo info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Item_CreateColorVariance]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = info.ItemId;
                cmd.Parameters.Add("@Color", SqlDbType.VarChar, 100).Value = info.Color;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = info.CreatedBy;
                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 50).Value = info.LastUpdatedBy;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int ItemColorDelete(string ColorId, string Condition)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Item_ColorDelete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ColorId", SqlDbType.VarChar, 50).Value = ColorId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int InsertMappingValue(SizeColorData data)
        {
            var value = new List<ColorSizeMapping>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Item_InsertMapping]", con))
            {
                try
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("ItemId", typeof(string));
                    odt.Columns.Add("PriceId", typeof(string));
                    odt.Columns.Add("ColorId", typeof(string));
                    odt.Columns.Add("CreatedBy", typeof(string));
                    odt.Columns.Add("LastUpdatedBy", typeof(string));
                    if (data.MappingInfo != null)
                        foreach (var o in data.MappingInfo)
                            odt.Rows.Add(o.ItemId, o.PriceId, o.ColorId, data.CreatedBy, data.LastUpdatedBy);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tblMapping", SqlDbType.Structured).Value = odt;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    return 1;
                }
            }
        }
        public int EditMappingValue(SizeColorData data)
        {
            var value = new List<ColorSizeMapping>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Item_EditMapping]", con))
            {
                try
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("ItemId", typeof(string));
                    odt.Columns.Add("PriceId", typeof(string));
                    odt.Columns.Add("ColorId", typeof(string));
                    odt.Columns.Add("CreatedBy", typeof(string));
                    odt.Columns.Add("LastUpdatedBy", typeof(string));
                    if (data.MappingInfo != null)
                        foreach (var o in data.MappingInfo)
                            odt.Rows.Add(o.ItemId, o.PriceId, o.ColorId, data.CreatedBy, data.LastUpdatedBy);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tblMapping", SqlDbType.Structured).Value = odt;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = data.MappingInfo[0].ItemId;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    return 1;
                }
            }
        }
        public List<ColorSizeMapping> GetColorSizeMap(string ItemId)
        {
            List<ColorSizeMapping> list = new List<ColorSizeMapping>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Item_GetColorSizeMapping]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = ItemId;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {

                                list.Add(new ColorSizeMapping
                                {
                                    PriceId = Convert.ToString(rd["PriceId"]),
                                    MPriceId = rd["MPriceId"] == DBNull.Value ? "0" : Convert.ToString(rd["MPriceId"]),
                                    MColorId = rd["MColorId"] == DBNull.Value ? "0" : Convert.ToString(rd["MColorId"]),
                                    Size = rd["SizeDetail"] == DBNull.Value ? "" : Convert.ToString(rd["SizeDetail"]),
                                    ColorDetail = rd["ColorDetail"] == DBNull.Value ? "" : Convert.ToString(rd["ColorDetail"]),
                                    MappingId = rd["MappingId"] == DBNull.Value ? 0 : Convert.ToInt32(rd["MappingId"]),
                                });
                            }
                        }
                        return list;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
        }
        public ItemSizeInfo GetSizeInfoDetails(int id, string hubId)
        {
            ItemSizeInfo data = new ItemSizeInfo();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Size_GetSizeinfo]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.AddWithValue("@hubId", SqlDbType.Int).Value = hubId;
                        con.Open();
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                data = new ItemSizeInfo
                                {
                                    PId = Convert.ToInt32(rd["id"]),
                                    PriceId = Convert.ToString(rd["PriceId"]),
                                    ItemId = Convert.ToString(rd["ItemId"]),
                                    Size = Convert.ToString(rd["Size"]),
                                    Purchaseprice = Convert.ToDouble(rd["Purchaseprice"]),
                                    SellingPrice = Convert.ToDouble(rd["SellingPrice"]),
                                    MarketPrice = Convert.ToDouble(rd["MarketPrice"]),
                                    ItmNetWeight = Convert.ToSingle(rd["ItmNetWeight"]),
                                    Measurement = Convert.ToString(rd["Measurement"]),
                                    Barcode = Convert.ToString(rd["Barcode"]),
                                    imagecdnpathvariance = Convert.ToString(rd["ImagePath"]),
                                    colorCode = Convert.ToString(rd["color"])
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return data;
                    }
                    return data;
                }
            }
        }
        public int UpdateSizeDetail(ItemSizeInfo info)
        {
            {
                //string type = "Hub";
                //if (!info.PriceId.Contains('H'))
                //{
                //    type = "Main";
                //}
                int flag = 0;
                //    using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                //        try
                //        {
                //            using (SqlCommand cmd = new SqlCommand("[dbo].[Size_UpdateSize]", con))
                //            {
                //                cmd.CommandType = CommandType.StoredProcedure;
                //                con.Open();
                //                cmd.Parameters.Add("@priceid", SqlDbType.VarChar, 50).Value = info.PriceId;
                //                cmd.Parameters.Add("@size", SqlDbType.VarChar, 150).Value = info.Size;
                //                cmd.Parameters.Add("@purchaseprice", SqlDbType.Float).Value = info.Purchaseprice;
                //                cmd.Parameters.Add("@sellingprice", SqlDbType.Float).Value = info.SellingPrice;
                //                cmd.Parameters.Add("@marketprice", SqlDbType.Float).Value = info.MarketPrice;
                //                cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar, 150).Value = info.LastUpdatedBy;
                //                cmd.Parameters.Add("@type", SqlDbType.VarChar, 15).Value = type;
                //                flag = Convert.ToInt32(cmd.ExecuteNonQuery());
                //            }
                //            return flag;
                //        }
                //        catch (Exception ex)
                //        {
                //            throw;
                //        }
                //}
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("[dbo].[Size_UpdateSize]", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add("@size", SqlDbType.VarChar, 150).Value = info.Size;
                            cmd.Parameters.Add("@priceid", SqlDbType.VarChar, 50).Value = info.PriceId;
                            cmd.Parameters.Add("@purchaseprice", SqlDbType.Float).Value = info.Purchaseprice;
                            cmd.Parameters.Add("@sellingprice", SqlDbType.Float).Value = info.SellingPrice;
                            cmd.Parameters.Add("@barcode", SqlDbType.VarChar, 50).Value = info.Barcode;
                            cmd.Parameters.Add("@ItmNetWeight", SqlDbType.Float).Value = info.ItmNetWeight;
                            cmd.Parameters.Add("@marketprice", SqlDbType.Float).Value = info.MarketPrice;
                            cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar, 150).Value = info.LastUpdatedBy;
                            cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = info.Hub;
                            cmd.Parameters.Add("@ColorCode", SqlDbType.VarChar, 50).Value = info.colorCode;
                            // cmd.Parameters.Add("@type", SqlDbType.VarChar, 15).Value = "Main";
                            flag = Convert.ToInt32(cmd.ExecuteNonQuery());
                        }
                        return flag;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
            }

        }
        public int AddItem(ItemMasters info)
        {
            info.Coupen = ((info.CoupenDisc == "true") ? 1 : 0);
            info.featured = ((info.featured == "true") ? "Y" : "N");
            info.seasonSale = ((info.seasonSale == "true") ? "Y" : "N");
            info.Check_Speical = ((info.Check_Speical == "true") ? "Y" : "N");
            info.Approval = ((info.Approval == "true") ? "Approved" : "Pending");
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_InsertItemtoHub]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = info.ItemId;
                        cmd.Parameters.Add("@HubId", SqlDbType.VarChar).Value = info.hubId;
                        cmd.Parameters.Add("@Coupen", SqlDbType.VarChar).Value = info.Coupen;
                        cmd.Parameters.Add("@Feature", SqlDbType.VarChar).Value = info.featured;
                        cmd.Parameters.Add("@SeasonSale", SqlDbType.VarChar).Value = info.seasonSale;
                        cmd.Parameters.Add("@ChefSpecail", SqlDbType.VarChar).Value = info.Check_Speical;
                        cmd.Parameters.Add("@Approval", SqlDbType.VarChar).Value = info.Approval;
                        cmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = info.CreatedBy;
                        //cmd.Parameters.AddWithValue("@category", SqlDbType.VarChar).Value = info.Category;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }
        public int RemovalExitsVarainttoHub(ItemMasters info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_HubMaping]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PriceId", SqlDbType.VarChar).Value = info.PriceId;
                        cmd.Parameters.Add("@IsHubMap", SqlDbType.VarChar).Value = info.IsHubMap;
                        cmd.Parameters.AddWithValue("@LastUpdatedBy", SqlDbType.VarChar).Value = info.LastUpdatedBy;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
        }
        public List<ItemMasters> HubProductSizeInfoDetails(string id, string hubId)
        {
            List<ItemMasters> productlist = new List<ItemMasters>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Item_HubGetSizeDetailsInfo]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = hubId;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        productlist.Add(new ItemMasters
                        {
                            PId = Convert.ToInt32(rd["Id"]),
                            PriceId = Convert.ToString(rd["PriceId"]),
                            ItemId = Convert.ToString(rd["ItemId"]),
                            Weight = Convert.ToString(rd["OrderQty"]),
                            Size = Convert.ToString(rd["Size"]),
                            ItmNetWeight = Convert.ToSingle(rd["ItmNetWeight"]),
                            Measurement = Convert.ToString(rd["Measurement"]),
                            WasteagePerc = rd["WasteagePerc"] == DBNull.Value ? "N" : Convert.ToString(rd["WasteagePerc"]),
                            WastageQty = Convert.ToString(rd["WastageQty"]),
                            Purchaseprice = Convert.ToString(rd["Purchaseprice"]),
                            SellingProfitPer = rd["SellingProfitPer"] == DBNull.Value ? "0.00" : Convert.ToString(rd["SellingProfitPer"]),
                            TotalPrice = Convert.ToString(rd["TotalPrice"]),
                            ProfitMargin = Convert.ToString(rd["ProfitMargin"]),
                            SellingPrice = Convert.ToString(rd["SellingPrice"] == DBNull.Value ? "00.00" : Convert.ToString(rd["SellingPrice"])),
                            MarketPrice = rd["MarketPrice"] == DBNull.Value ? "00.00" : Convert.ToString(rd["MarketPrice"]),
                            seasonSale = Convert.ToString(rd["seasonSale"]),
                            MeasuredIn = Convert.ToString(rd["Item_MeasuredIn"])
                        });
                    }
                    return productlist;
                }
            }
        }
        public Task<List<ItemMasters>> HubProductColorInfoDetails(string id, string hubId)
        {
            throw new NotImplementedException();
        }
        public ItemMasters GEtItemCountDashboard(string hubId/*, string featured, string Approval, int coupenId*/)
        {
            try
            {
                ItemMasters ItemMasterlist1 = new ItemMasters();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_ItemCountDashboard]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@hubId", SqlDbType.VarChar).Value = hubId;
                    con.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            ItemMasterlist1 = new ItemMasters
                            {
                                ItemId = Convert.ToString(rd["tblItemcount"]),
                                featured = Convert.ToString(rd["tblfeaturedcount"]),
                                Approval = Convert.ToString(rd["tblApprovalcount"]),
                                CoupenDisc = Convert.ToString(rd["tblCoupencount"]),
                                FoodType = Convert.ToString(rd["tblNonVegCount"]),
                                foodtype = Convert.ToString(rd["tblVegCount"]),
                                Itemnever = Convert.ToString(rd["tblnevercount"]),
                            };
                        }
                        return ItemMasterlist1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public List<ItemMasters> GetItemDetails(string datefrom, string dateto, string id)
        //{
        //    using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
        //    {
        //        List<ItemMasters> ItemMasters1 = new List<ItemMasters>();
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandText = "[dbo].[usp_Item_details]";
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@startdate", datefrom);
        //            cmd.Parameters.AddWithValue("@enddate", dateto);
        //            cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
        //            con.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                try
        //                {
        //                    while (sdr.Read())
        //                    {
        //                        ItemMasters1.Add(new ItemMasters
        //                        {
        //                            ItemId = Convert.ToString(sdr["ItemId"]),
        //                            Weight = Convert.ToString(sdr["QuantityValue"]),
        //                            TotalPrice = Convert.ToString(sdr["TotalPrice"]),
        //                            PluName = Convert.ToString(sdr["PluName"]),
        //                        });
        //                    }
        //                    return ItemMasters1;
        //                }
        //                catch (Exception e)
        //                {
        //                    throw;
        //                }
        //            }
        //            con.Close();
        //        }
        //    }
        //}
        public List<ItemMasters> GetvegItemDetails(string datefrom, string dateto, string id, int type)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                List<ItemMasters> ItemMasters2 = new List<ItemMasters>();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_Veg_Item_details]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@startdate", datefrom);
                    cmd.Parameters.AddWithValue("@enddate", dateto);
                    cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                    cmd.Parameters.Add("@Type", SqlDbType.Int).Value = type;
                    cmd.CommandTimeout = 0;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (sdr.Read())
                            {
                                ItemMasters2.Add(new ItemMasters
                                {
                                    ItemId = Convert.ToString(sdr["ItemId"]),
                                    TotalPrice = Convert.ToString(sdr["TotalPrice"]),
                                    PluName = Convert.ToString(sdr["PluName"]),
                                    Variant = Convert.ToString(sdr["Varaint"]),
                                    totalnetweight = Convert.ToString(sdr["totalnetweight"] == DBNull.Value ? "01.00" : Convert.ToString(sdr["totalnetweight"])),
                                });
                            }
                            return ItemMasters2;
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        public List<ItemMasters> Itemvegnonvegcount(string datefrom, string dateto, string id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                List<ItemMasters> ItemMasters12 = new List<ItemMasters>();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_VegNonvegWeightCount]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@startdate", datefrom);
                    cmd.Parameters.AddWithValue("@enddate", dateto);
                    cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                    cmd.CommandTimeout = 0;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (sdr.Read())
                            {
                                ItemMasters12.Add(new ItemMasters
                                {
                                    QuantityValue = Convert.ToInt32(sdr["QuantityValue"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["QuantityValue"])),
                                    TotalPrice = Convert.ToString(sdr["TotalPrice"] == DBNull.Value ? "0" : Convert.ToString(sdr["TotalPrice"])),
                                    FoodType = Convert.ToString(sdr["Food_SubType"] == DBNull.Value ? "Name" : Convert.ToString(sdr["Food_SubType"])),
                                });
                            }
                            return ItemMasters12;
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        public int AddNonVegCategory(ItemMasters itemMasters)
        {
            SqlConnection con = new SqlConnection(_dbConfig.ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_InsertFoodSubType]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@foodsubName", SqlDbType.VarChar, 500).Value = itemMasters.FoodSubType;
                    cmd.Parameters.Add("@created_by", SqlDbType.VarChar, 100).Value = itemMasters.CreatedBy;
                    cmd.Parameters.Add("@lastupdated_by", SqlDbType.VarChar, 100).Value = itemMasters.LastUpdatedBy;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<ItemMasters> GetItemLanguage(ItemMasters item, string id, int Id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                List<ItemMasters> ItemMaster = new List<ItemMasters>();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_ItemMappedWithLang]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = id;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    cmd.CommandTimeout = 0;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (sdr.Read())
                            {
                                ItemMaster.Add(new ItemMasters
                                {
                                    Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["Id"])),
                                    ItemNameLanguage = Convert.ToString(sdr["ItemName"] == DBNull.Value ? "Na" : Convert.ToString(sdr["ItemName"])),
                                    ItemId = Convert.ToString(sdr["ItemId"] == DBNull.Value ? "Na" : Convert.ToString(sdr["ItemId"])),
                                    LanguageName = Convert.ToString(sdr["Languagecode"] == DBNull.Value ? "Na" : Convert.ToString(sdr["Languagecode"])),
                                    Descriptionlanguage = Convert.ToString(sdr["Itemdescription"] == DBNull.Value ? "Na" : Convert.ToString(sdr["Itemdescription"])),
                                    Itemused = Convert.ToString(sdr["ItemUsed"] == DBNull.Value ? "Na" : Convert.ToString(sdr["ItemUsed"])),
                                });
                            }
                            return ItemMaster;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        public int uploadimagecreateiteam(string id, string type)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_updateImageitemcreate]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = type;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }  
        public int uploadimageCount(string id, int count)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_UpdateImagecount]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@itemId", SqlDbType.VarChar).Value = id;
                    cmd.Parameters.Add("@count", SqlDbType.Int).Value = count;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public string CheckUniqueBarcode(string Barcode)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[barcode_Checkunique]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Barcode", SqlDbType.VarChar, 100).Value = Barcode;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    var barcode = "";
                    if (rd.Read())
                    {
                        barcode = Convert.ToString(rd["Barcode"]);
                    }
                    return barcode;
                }
            }
        }
        public LanguageMst GetItemLanguageById(int id)
        {
            LanguageMst data = new LanguageMst();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_GetItemLanguageById]", con))
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
                                data = new LanguageMst
                                {
                                    Id = Convert.ToInt32(rd["Id"]),
                                    Id1 = Convert.ToInt32(rd["Id1"]),
                                    ItemId = Convert.ToString(rd["ItemId"]),
                                    ItemNameLanguage = Convert.ToString(rd["ItemName"]),
                                    LanguageName = Convert.ToString(rd["Languagecode"]),
                                    Descriptionlanguage = Convert.ToString(rd["Itemdescription"]),
                                    Itemused = Convert.ToString(rd["ItemUsed"]),
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return data;
                    }
                    return data;
                }
            }
        }
        public int DeleteItemLanguageByitemId(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("usp_DeleteItemLanguageByitemId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<ProductType> GetProductList(string Id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                List<ProductType> ProductType = new List<ProductType>();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_GetProductTypeList]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BranchId", SqlDbType.VarChar).Value = Id;
                    cmd.CommandTimeout = 0;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (sdr.Read())
                            {
                                ProductType.Add(new ProductType
                                {
                                    Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["Id"])),
                                    ProductCategoryName = Convert.ToString(sdr["ProductCategoryName"] == DBNull.Value ? "Na" : Convert.ToString(sdr["ProductCategoryName"])),
                                    ProductCategoryId = Convert.ToString(sdr["ProductCategoryId"] == DBNull.Value ? "Na" : Convert.ToString(sdr["ProductCategoryId"])),
                                    BranchId = Convert.ToString(sdr["BranchId"] == DBNull.Value ? "Na" : Convert.ToString(sdr["BranchId"])),
                                 
                                });
                            }
                            return ProductType;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        public List<ProductVariance> GetProductVarianceById(string Id,string HubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                List<ProductVariance> ProductVarianceList = new List<ProductVariance>();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_GetProductListById]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProductCategoryId", SqlDbType.VarChar).Value = Id;
                    cmd.Parameters.Add("@BranchId", SqlDbType.VarChar).Value = HubId;
                    cmd.CommandTimeout = 0;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (sdr.Read())
                            {
                                ProductVarianceList.Add(new ProductVariance
                                {
                                    Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["Id"])),
                                    ProductVarainceName = Convert.ToString(sdr["ProductVarainceName"] == DBNull.Value ? "Na" : Convert.ToString(sdr["ProductVarainceName"])),
                                    ProductCategoryId = Convert.ToString(sdr["ProductCategoryId"] == DBNull.Value ? "Na" : Convert.ToString(sdr["ProductCategoryId"])),
                                    ProductVarainceId = Convert.ToString(sdr["ProductVarainceId"] == DBNull.Value ? "Na" : Convert.ToString(sdr["ProductVarainceId"])),
                                    ProductVarainceShortCode = Convert.ToString(sdr["ProductVarainceShortCode"] == DBNull.Value ? "Na" : Convert.ToString(sdr["ProductVarainceShortCode"])),
                                    BranchId = Convert.ToString(sdr["BranchId"] == DBNull.Value ? "Na" : Convert.ToString(sdr["BranchId"])),
                                 
                                });
                            }
                            return ProductVarianceList;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        public List<ItemMasters> GetMappedItemList(string Id, string HubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                List<ItemMasters> MappedItemList = new List<ItemMasters>();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_GetMappedItemListByItemId]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = Id;
                    cmd.Parameters.Add("@branchId", SqlDbType.VarChar).Value = HubId;
                    cmd.CommandTimeout = 0;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (sdr.Read())
                            {
                                MappedItemList.Add(new ItemMasters
                                {
                                    ItemId = Convert.ToString(sdr["ItemId"] == DBNull.Value ? "NA" : Convert.ToString(sdr["ItemId"])),
                                    MappingItemId = Convert.ToString(sdr["MappingItemId"] == DBNull.Value ? "NA" : Convert.ToString(sdr["MappingItemId"])),
                                    Itemname = Convert.ToString(sdr["PluName"] == DBNull.Value ? "Na" : Convert.ToString(sdr["PluName"])),
                                    MainCategory = Convert.ToString(sdr["MainCategoryName"] == DBNull.Value ? "Na" : Convert.ToString(sdr["MainCategoryName"])),
                                    Category = Convert.ToString(sdr["categoryName"] == DBNull.Value ? "Na" : Convert.ToString(sdr["categoryName"])),
                                    ItemImage = Convert.ToString(sdr["ImagePath"] == DBNull.Value ? "Na" : Convert.ToString(sdr["ImagePath"])),
                                    Branch = Convert.ToString(sdr["branchId"] == DBNull.Value ? "Na" : Convert.ToString(sdr["branchId"])),

                                });
                            }
                            return MappedItemList;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        public List<ItemMasters> GetMappedVarientItemList(string Id, string HubId,string maincategory)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                List<ItemMasters> MappedItemList = new List<ItemMasters>();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_GetMappeingItem]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = Id;
                    cmd.Parameters.Add("@BranchId", SqlDbType.VarChar).Value = HubId;
                    cmd.Parameters.Add("@maincategory", SqlDbType.VarChar).Value = maincategory;
                    cmd.CommandTimeout = 0;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (sdr.Read())
                            {
                                MappedItemList.Add(new ItemMasters
                                {
                                    ItemId = Convert.ToString(sdr["ItemId"] == DBNull.Value ? "NA" : Convert.ToString(sdr["ItemId"])),
                                    Itemname = Convert.ToString(sdr["PluName"] == DBNull.Value ? "Na" : Convert.ToString(sdr["PluName"]))
                                });
                            }
                            return MappedItemList;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        public int UpdateVarientItemList(string MappedItemId, string ItemId, string hubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_UpdateVariendToMapped", con)) 
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 50).Value = hubId;
                cmd.Parameters.Add("@ItemId", SqlDbType.VarChar, 50).Value = ItemId;
                cmd.Parameters.Add("@MappedItemId", SqlDbType.VarChar, 50).Value = MappedItemId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<ProductSpec> GetProductSpecById(string Id, string productId,string HubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                List<ProductSpec> ProductSpecList = new List<ProductSpec>();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_GetProductSpecWithData]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ItemId", SqlDbType.VarChar).Value = Id;
                    cmd.Parameters.Add("@ProductCatId", SqlDbType.VarChar).Value = productId;
                    cmd.CommandTimeout = 0;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (sdr.Read())
                            {
                                ProductSpecList.Add(new ProductSpec
                                {
                                    Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["Id"])),
                                    ProductSubCatId = Convert.ToString(sdr["ProductSubCatId"] == DBNull.Value ? "Na" : Convert.ToString(sdr["ProductSubCatId"])),
                                    DescType = Convert.ToString(sdr["DescType"] == DBNull.Value ? "Na" : Convert.ToString(sdr["DescType"])),
                                    DescValue = Convert.ToString(sdr["DescValue"] == DBNull.Value ? "Na" : Convert.ToString(sdr["DescValue"])),
                                    productCatId = Convert.ToString(sdr["productCatId"] == DBNull.Value ? "Na" : Convert.ToString(sdr["productCatId"])),
                                    branchId = Convert.ToString(sdr["BranchId"] == DBNull.Value ? "Na" : Convert.ToString(sdr["BranchId"])),
                                    itemId = Convert.ToString(sdr["itemId"] == DBNull.Value ? "Na" : Convert.ToString(sdr["itemId"])),

                                });
                            }
                            return ProductSpecList;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        public string ProductSpec(ProductSpec spec)
        {
            var c = "";
            SqlConnection sqlcon = new SqlConnection(_dbConfig.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = sqlcon;
                cmd.CommandText = "[dbo].[usp_AddProductSpec]";
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                dt.TableName = "AddProductSpecs";
                dt.Columns.Add("DescType", typeof(string));
                dt.Columns.Add("DescValue", typeof(string));
                dt.Columns.Add("productCatId", typeof(string));
                dt.Columns.Add("branchId", typeof(string));
                dt.Columns.Add("itemId", typeof(string));
                dt.Columns.Add("ProductSubCatId", typeof(string));
              
                if (spec != null)
                    foreach (var data in spec.productSpec)
                    {
                        dt.Rows.Add(
                              data.DescType
                            , data.DescValue
                            , data.productCatId
                            , data.branchId
                            , data.itemId
                            , data.ProductSubCatId
                            );
                    }
                cmd.Parameters.Add("@AddproductSpec", SqlDbType.Structured).Value = dt;
                sqlcon.Open();
                c = Convert.ToString(cmd.ExecuteScalar());
               
            }
            catch (Exception ex)
            {
                return c;
            }
            finally
            {
                if (sqlcon.State != ConnectionState.Closed)
                {
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
            return c;
        }


        public int DeleteMapItem(string itemId,string mappingItemId,string hubId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_DeleteMappedItemList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@itemId", SqlDbType.VarChar).Value = itemId;
                cmd.Parameters.Add("@MapitemId", SqlDbType.VarChar).Value = mappingItemId;
                cmd.Parameters.Add("@branchId", SqlDbType.VarChar).Value = hubId;
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
