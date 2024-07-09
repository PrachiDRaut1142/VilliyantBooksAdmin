using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Offer;
using Freshlo.DomainEntities.PriceList;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Freshlo.Repository
{
    public class LiveOfferRepository : IOfferRI
    {
        private IDbConfig _dbConfig { get; }
        public LiveOfferRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        // offer list, create, detail ,update,delete 
        public List<Offer> GetOfferlist(string hubId)
        {
            List<Offer> Offerlist = new List<Offer>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[LiveOffer_GetList]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@hubId", SqlDbType.VarChar, 50).Value = hubId;
                        con.Open();                 
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Offerlist.Add(new Offer
                                {
                                    Id = Convert.ToInt32(dr[0]),
                                    OfferId = Convert.ToString(dr[1]),
                                    OfferHeading = Convert.ToString(dr[2]),
                                    OfferDescription = Convert.ToString(dr[3]),
                                    OfferStartDate = Convert.ToDateTime(dr[4]),
                                    OfferEndDate = Convert.ToDateTime(dr[5]),
                                    DiscountPerctg = Convert.ToSingle(dr[6]),
                                    Status = Convert.ToString(dr[7]),
                                    LastUpdatedOn = Convert.ToDateTime(dr[8]),
                                    OfferTypes = Convert.ToString(dr[9]),
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return Offerlist;
                    }
                }
            }
            return Offerlist;
        }
        public string AddOffer(Offer info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[LiveOffer_Create]";
                        DataTable dt = new DataTable();
                        dt.TableName = "OfferMappedItemsId2";
                        dt.Columns.Add("PriceId", typeof(string));                        
                        dt.Columns.Add("Barcode", typeof(string));
                        dt.Columns.Add("ItemId", typeof(string));
                        dt.Columns.Add("ItemType", typeof(string));                      
                        
                        if (info.ItemId != null)
                        {
                            foreach (var data in info.ItemId)
                            {

                                dt.Rows.Add(
                                  //data.Split(" / ")[0], //priceid
                                  //data.Split(" / ")[2], // barcode
                                  //data.Split(" / ")[1], //ItemId
                                  //1
                                  data.ToString(),
                                  null,
                                  null,
                                  1
                                  );
                                
                            }
                        }
                        if (info.GetItemId != null) {
                            foreach (var data1 in info.GetItemIdss)
                            {
                                dt.Rows.Add(
                                //data1.Split(" / ")[0], //priceid
                                //  data1.Split(" / ")[2], // barcode
                                //  data1.Split(" / ")[1], //ItemId
                                //  2
                                data1.ToString(),
                                  null,
                                  null,
                                  2
                                  );
                            }
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@OfferHeading", SqlDbType.VarChar).Value = info.OfferHeading;
                            cmd.Parameters.Add("@OfferDescription", SqlDbType.VarChar).Value = info.OfferDescription;
                        cmd.Parameters.AddWithValue("@OfferStartDate", new DateTime()).Value = info.OfferStartDate;
                        cmd.Parameters.AddWithValue("@OfferEndDate", new DateTime()).Value = info.OfferEndDate;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = info.Status;
                            cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = info.CreatedBy;
                            cmd.Parameters.Add("@DiscountPerctg", SqlDbType.Float).Value = info.DiscountPerctg;
                            cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = info.Hub;
                            cmd.Parameters.Add("@OfferTypes", SqlDbType.VarChar).Value = info.chooseOffer;
                            cmd.Parameters.Add("@FreeType", SqlDbType.VarChar).Value = info.FreeType;
                            cmd.Parameters.Add("@BuyQuantity", SqlDbType.VarChar).Value = info.BuyQuantity;
                            cmd.Parameters.Add("@GetQuantity", SqlDbType.VarChar).Value = info.GetQuantity;
                            cmd.Parameters.Add("@MinQuantityValue", SqlDbType.VarChar).Value = info.MinQuantityValue;
                            cmd.Parameters.Add("@IsLimitedOffer", SqlDbType.VarChar).Value = info.IsLimitedOffer;
                            cmd.Parameters.Add("@MinOrderValue", SqlDbType.Int).Value = info.MinOrderValue;
                            cmd.Parameters.Add("@tblofferitems", SqlDbType.Structured).Value = dt;
                        con.Open();
                        var rd = cmd.ExecuteScalar();
                        return Convert.ToString(rd);
                        
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Offer GetOfferDetail(string Id)
        {
            Offer OfferDetail = new Offer();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[LiveOffer_GetDetail]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        OfferDetail = new Offer
                        {
                            Id = Convert.ToInt32(rd["id"]),
                            OfferId = Convert.ToString(rd["OfferId"]),
                            OfferHeading = Convert.ToString(rd["OfferHeading"]),
                            OfferDescription = Convert.ToString(rd["OfferDescription"]),
                            DiscountPerctg = Convert.ToSingle(rd["DiscountPerctg"]),
                            Status = Convert.ToString(rd["Status"]),
                            OfferStartDate = Convert.ToDateTime(rd["OfferStartDate"]),
                            OfferEndDate = Convert.ToDateTime(rd["OfferEndDate"]),
                            CreatedBy = Convert.ToString(rd["CreatedBy"]),
                            CreatedOn = Convert.ToDateTime(rd["CreatedOn"])
                        };
                    }
                    return OfferDetail;
                }
            }
        }
        public int EditOffer(Offer info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[LiveOffer_Update]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = info.Id;
                    cmd.Parameters.Add("@OfferHeading", SqlDbType.VarChar).Value = info.OfferHeading;
                    cmd.Parameters.Add("@OfferDescription", SqlDbType.VarChar).Value = info.OfferDescription;
                    cmd.Parameters.AddWithValue("@OfferStartDate", new DateTime()).Value = info.OfferStartDate;
                    cmd.Parameters.AddWithValue("@OfferEndDate", new DateTime()).Value = info.OfferEndDate;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = info.Status;
                    cmd.Parameters.Add("@UpdateBy", SqlDbType.VarChar).Value = info.LastUpdatedBy;
                    cmd.Parameters.Add("@DiscountPerctg", SqlDbType.Float).Value = info.DiscountPerctg;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool DeleteOffer(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[LiveOffer_DeleteOffer]", con))
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
        // mapping function offer 
        public List<PriceList> Getallpricelist(PricelistFilter details)
        {
            List<PriceList> allPricelist = new List<PriceList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[LiveOffer_GetAllPricelist]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MainCategory", SqlDbType.Int).Value = details.MainCategoryId;
                        cmd.Parameters.AddWithValue("@Category", SqlDbType.Int).Value = details.CategoryId;
                        cmd.Parameters.AddWithValue("@BrandId", SqlDbType.Int).Value = details.BrandId;
                        cmd.Parameters.AddWithValue("@VendorId", SqlDbType.Int).Value = details.VendorId;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                allPricelist.Add(new PriceList
                                {
                                    PriceId = Convert.ToString(dr["PriceId"]),
                                    ItemId = Convert.ToString(dr["ItemId"]),
                                    pluName = Convert.ToString(dr["PluName"]),
                                    PurchasePrice = Convert.ToDouble(dr["PurchasePrice"]),
                                    SellingPrice = Convert.ToDouble(dr["SellingPrice"]),
                                    TotalPrice = Convert.ToDouble(dr["TotalPrice"]),
                                    Id = Convert.ToInt32(dr["Id"]),
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return allPricelist;
                    }
                }
            }
            return allPricelist;
        }
        public List<PriceList> GetMappedPricelist(PricelistFilter details, string Id)
        {
            List<PriceList> MappedPricelist = new List<PriceList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[LiveOffer_MapPricelist]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                        cmd.Parameters.AddWithValue("@MainCategory", SqlDbType.Int).Value = details.MainCategoryId;
                        cmd.Parameters.AddWithValue("@Category", SqlDbType.Int).Value = details.CategoryId;
                        cmd.Parameters.AddWithValue("@BrandId", SqlDbType.Int).Value = details.BrandId;
                        cmd.Parameters.AddWithValue("@VendorId", SqlDbType.Int).Value = details.VendorId;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                MappedPricelist.Add(new PriceList
                                {
                                    ItemId = Convert.ToString(dr[0]),
                                    pluName = Convert.ToString(dr[1]),
                                    SellingPrice = Convert.ToSingle(dr[2]),
                                    DiscountPrice = Convert.ToSingle(dr[3]),
                                    DiscountedValue = Convert.ToSingle(dr[4]),
                                    Id = Convert.ToInt32(dr[5]),
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return MappedPricelist;
                    }
                }
            }
            return MappedPricelist;
        }
        public int DeleteOfferItem(int Id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[LiveOffer_DeleteItem]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public int AddItem(PriceList info, string offerid, string startdate)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[LiveOffer_InsertItemtoOffer]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OfferId", SqlDbType.VarChar).Value = offerid;
                    cmd.Parameters.Add("@itemId", SqlDbType.VarChar).Value = info.ItemId;
                    cmd.Parameters.AddWithValue("@DateAdded", new DateTime()).Value = startdate;
                    cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.VarChar).Value = info.CreatedBy;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int AddItemList(Offer list)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[LiveOffer_AddAavialableItem]", con))
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("ItemIds", typeof(string)); 
                    if (list.ItemIds != null)
                        for (int i = 0; i < list.ItemIds.Count; i++)
                        {
                            var ItemIds = list.ItemIds[i];
                            odt.Rows.Add(ItemIds);                        
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@LiveOfferlist", SqlDbType.Structured).Value = odt;
                    cmd.Parameters.Add("@OfferId", SqlDbType.VarChar, 50).Value = list.OfferId;
                    cmd.Parameters.Add("@AddedBy", SqlDbType.VarChar, 50).Value = list.CreatedBy;
                    return Convert.ToInt32(cmd.ExecuteNonQuery());                
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int AddItemList(PriceList info, string offerid)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Pricelist_UpdateField]", con))
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("ItemIds", typeof(string));
                    if (info.ItemIds != null)
                        for (int i = 0; i < info.ItemIds.Count; i++)
                        {
                            var ItemIds = info.ItemIds[i];
                            odt.Rows.Add(ItemIds);
                        }
                    for (int j  = 0; j  < info.ItemIds.Count; j ++)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@LiveOfferlist", SqlDbType.Structured).Value = odt;
                        cmd.Parameters.Add("@OfferId", SqlDbType.VarChar).Value = offerid;
                        cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.VarChar).Value = info.CreatedBy;
                    }
                    return Convert.ToInt32(cmd.ExecuteNonQuery());                
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool DeleteMappingItem(string ids)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            try
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[LiveOffer_AddItemDelete]", con))
                {
                    con.Open();
                    DataTable odt = new DataTable();
                    odt.Columns.Add("ID", typeof(int));
                    if (ids != null)
                        foreach (var o in ids.Split(','))
                            odt.Rows.Add(Convert.ToInt32(o));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@LiveOfferIdValues", SqlDbType.Structured).Value = odt;
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }        
        }

        public List<PriceList> Getallitemlist(string maincategory, string Category, int type, string hubId)
        {
            List<PriceList> allPricelist = new List<PriceList>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_GetOffercreate]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MainCategory", SqlDbType.VarChar, 50).Value = maincategory;
                        cmd.Parameters.Add("@Category", SqlDbType.VarChar, 50).Value = Category;
                        cmd.Parameters.Add("@type", SqlDbType.Int).Value = type;
                        cmd.Parameters.AddWithValue("@hubId", SqlDbType.Int).Value = hubId;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                allPricelist.Add(new PriceList
                                {
                                    Barcode = Convert.ToString(dr["Barcode"]),
                                    ItemId = Convert.ToString(dr["ItemId"]),
                                    pluName = Convert.ToString(dr["PluName"]),
                                    MainCatName = Convert.ToString(dr["MainCatName"]),
                                    CatName = Convert.ToString(dr["CatName"]),
                                    SubCatName = Convert.ToString(dr["SubCatName"]),                                   
                                    PriceId = Convert.ToString(dr["PriceId"]),                                   
                                    SellingPrice = (double)Convert.ToDecimal(dr["SellingPrice"]),                                  

                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return allPricelist;
                    }
                }
            }
            return allPricelist;
        }
        public List<SelectListItem> GetOfferTypeList()
        {
            try
            {
                List<SelectListItem> GetofferTypetList = new List<SelectListItem>();
                using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_GetOfferTypeCRUD]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add("@type", SqlDbType.Int).Value = 1;
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string text = Convert.ToString(rd["OfferName"]);
                            string Id = Convert.ToString(rd["Id"]);
                            GetofferTypetList.Add(new SelectListItem()
                            {
                                Text = text,
                                Value = Id,
                            });
                        }
                        return GetofferTypetList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

