using Freshlo.DomainEntities;
using Freshlo.DomainEntities.PriceList;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Freshlo.Repository
{
    public class PricelistRepository : IPricelistRI
    {
        private IDbConfig _dbConfig { get; }

        public PricelistRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }


        //public List<PriceList> GetMainCategoryList()
        //{
        //    List<PriceList> GetMainCategorySelectList = new List<PriceList>();
        //    using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand("[dbo].[PriceList_MainCategory]", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();
        //        try
        //        {
        //            using (SqlDataReader rd = cmd.ExecuteReader())
        //            {
        //                while (rd.Read())
        //                {

        //                    GetMainCategorySelectList.Add(new PriceList
        //                    {
        //                        MainCatId = Convert.ToInt32(rd["Id"]),
        //                        ManCatName = Convert.ToString(rd["Name"])

        //                    });
        //                }
        //                return GetMainCategorySelectList;
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            throw;
        //        }
        //    }
        //}


        //public List<PriceList> GetCategories()
        //{
        //    List<PriceList> list = new List<PriceList>();
        //    using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand("[dbo].[PriceList_GetCategoryList]", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();
        //        try
        //        {
        //            using (SqlDataReader rd = cmd.ExecuteReader())
        //            {
        //                while (rd.Read())
        //                {
        //                    list.Add(new PriceList
        //                    {
        //                        CatId = Convert.ToInt32(rd["Id"]),
        //                        CatName = Convert.ToString(rd["Name"])
        //                    });
        //                }
        //                return list;
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            throw;
        //        }
        //    }
        //}



        public List<SelectListItem> GetItemTypeList()
        {
            List<SelectListItem> GetItemTypeList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Pricelist_ItemTypeOffer]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["Name"]);
                        string Id = Convert.ToString(rd["Id"]);
                        GetItemTypeList.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return GetItemTypeList;
                }
            }
        }


     
        public List<PriceList> GetPricelistData(PricelistFilter detail)
        {
            List<PriceList> PriceList = new List<PriceList>();
            
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[PriceList_GetNewItemList]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@MainCategory", SqlDbType.Int).Value = detail.MainCategoryId;
                        cmd.Parameters.AddWithValue("@Category", SqlDbType.Int).Value = detail.CategoryId;
                        cmd.Parameters.AddWithValue("@Itemtype", SqlDbType.Int).Value = detail.ItemType;
                        cmd.Parameters.Add("@ApproavalType", SqlDbType.VarChar, 10).Value = detail.ApproavalType; ;
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                PriceList.Add(new PriceList
                                {
                                    PriceId = Convert.ToString(rd[0] == DBNull.Value ? "0" : rd[0]),
                                    ItemId = Convert.ToString(rd[1] == DBNull.Value ? "0" : rd[1]),
                                    pluName = Convert.ToString(rd[2] == DBNull.Value ? "0" : rd[2]),
                                    Measurement = Convert.ToString(rd[3] == DBNull.Value ? "0" : rd[3]),
                                    OrderQty = Convert.ToString(rd[4] == DBNull.Value ? "0" : rd[4]),
                                    WasteagePerc = Convert.ToDouble(rd[5] == DBNull.Value ? "0" : rd[5]),
                                    PurchasePrice = Convert.ToDouble(rd[6] == DBNull.Value ? "0" : rd[6]),
                                    SellingPrice = Convert.ToDouble(rd[7] == DBNull.Value ? "0" : rd[7]),
                                    TotalPrice = Convert.ToDouble(rd[8] == DBNull.Value ? "0" : rd[8]),
                                    SellingProfitPer = Convert.ToDouble(rd[9] == DBNull.Value ? "0" : rd[9]),
                                    ProfitMargin = Convert.ToDouble(rd[10] == DBNull.Value ? "0" : rd[10]),
                                    MarketPrice = Convert.ToString(rd[11] == DBNull.Value ? "0" : rd[11]),
                                    seasonSale = Convert.ToString(rd[12] == DBNull.Value ? "0" : rd[12]),
                                    Id = Convert.ToInt32(rd[13] == DBNull.Value ? "0" : rd[13]),
                                    DiscountPrice = Convert.ToDouble(rd["discountPrice"] == DBNull.Value ? 0 : rd["discountPrice"]),
                                    OfferDiscount = Convert.ToDouble(rd["OfferDiscount"] == DBNull.Value ? 0 : rd["OfferDiscount"]),
                                });
                            }

                        }

                        

                    }

                    catch (Exception ex)
                    {
                        return PriceList;
                    }

                }

            }
            return PriceList;
        }


        public List<PriceList> GetPricelistDataforCook(PricelistFilter detail)
        {
            List<PriceList> PriceList = new List<PriceList>();

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[PriceList_GetNewItemListforCook]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@MainCategory", SqlDbType.Int).Value = detail.MainCategoryId;
                        cmd.Parameters.AddWithValue("@Category", SqlDbType.Int).Value = detail.CategoryId;
                        cmd.Parameters.AddWithValue("@Itemtype", SqlDbType.Int).Value = detail.ItemType;
                        cmd.Parameters.Add("@ApproavalType", SqlDbType.VarChar, 10).Value = detail.ApproavalType; ;
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                PriceList.Add(new PriceList
                                {
                                    PriceId = Convert.ToString(rd[0] == DBNull.Value ? "0" : rd[0]),
                                    ItemId = Convert.ToString(rd[1] == DBNull.Value ? "0" : rd[1]),
                                    pluName = Convert.ToString(rd[2] == DBNull.Value ? "0" : rd[2]),
                                    Measurement = Convert.ToString(rd[3] == DBNull.Value ? "0" : rd[3]),
                                    OrderQty = Convert.ToString(rd[4] == DBNull.Value ? "0" : rd[4]),
                                    WasteagePerc = Convert.ToDouble(rd[5] == DBNull.Value ? "0" : rd[5]),
                                    PurchasePrice = Convert.ToDouble(rd[6] == DBNull.Value ? "0" : rd[6]),
                                    SellingPrice = Convert.ToDouble(rd[7] == DBNull.Value ? "0" : rd[7]),
                                    TotalPrice = Convert.ToDouble(rd[8] == DBNull.Value ? "0" : rd[8]),
                                    SellingProfitPer = Convert.ToDouble(rd[9] == DBNull.Value ? "0" : rd[9]),
                                    ProfitMargin = Convert.ToDouble(rd[10] == DBNull.Value ? "0" : rd[10]),
                                    MarketPrice = Convert.ToString(rd[11] == DBNull.Value ? "0" : rd[11]),
                                    seasonSale = Convert.ToString(rd[12] == DBNull.Value ? "0" : rd[12]),
                                    Id = Convert.ToInt32(rd[13] == DBNull.Value ? "0" : rd[13]),
                                    DiscountPrice = Convert.ToDouble(rd["discountPrice"] == DBNull.Value ? 0 : rd["discountPrice"]),
                                    OfferDiscount = Convert.ToDouble(rd["OfferDiscount"] == DBNull.Value ? 0 : rd["OfferDiscount"]),
                                });
                            }

                        }



                    }

                    catch (Exception ex)
                    {
                        return PriceList;
                    }

                }

            }
            return PriceList;
        }


        List<PricelistCategory> IPricelistRI.GetMainCategoryList()
        {
            List<PricelistCategory> GetMainCategorySelectList = new List<PricelistCategory>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[PriceList_MainCategory]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {

                            GetMainCategorySelectList.Add(new PricelistCategory
                            {
                                MainCatId = Convert.ToInt32(rd["Id"]),
                                ManCatName = Convert.ToString(rd["Name"])

                            });
                        }
                        return GetMainCategorySelectList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        List<PricelistCategory> IPricelistRI.GetCategories()
        {
            List<PricelistCategory> list = new List<PricelistCategory>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[PriceList_GetCategoryList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            list.Add(new PricelistCategory
                            {
                                CategoryId = Convert.ToString(rd["CategoryId"]),
                                Name = Convert.ToString(rd["Name"])
                            });
                        }
                        return list;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }



        public void UpdatePricelist(PriceList list)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Pricelist_UpdateField]", con))
                    {
                        con.Open();
                        DataTable odt = new DataTable();
                        odt.Columns.Add("Ids", typeof(int));
                        odt.Columns.Add("WasteagePercs", typeof(double));
                        odt.Columns.Add("PurchasePrices", typeof(double));
                        odt.Columns.Add("SellingProfitPers", typeof(double));
                        odt.Columns.Add("TotalPrices", typeof(double));
                        odt.Columns.Add("ProfitMargins", typeof(double));
                        odt.Columns.Add("SellingPrices", typeof(double));
                        odt.Columns.Add("MarketPrices", typeof(double));
                        odt.Columns.Add("seasonSales", typeof(string));
                        if (list.Ids != null)
                            for (int i = 0; i < list.Ids.Count; i++)
                            {
                                var Ids = list.Ids[i];
                                var WasteagePercs = list.WasteagePercs[i];
                                var PurchasePrices = list.PurchasePrices[i];
                                var SellingProfitPers = list.SellingProfitPers[i];
                                var TotalPrices = list.TotalPrices[i];
                                var ProfitMargins = list.ProfitMargins[i];
                                var SellingPrices = list.SellingPrices[i];
                                var MarketPrices = list.MarketPrices[i];
                                var seasonSales = list.seasonSales[i];
                                odt.Rows.Add(Ids, WasteagePercs, PurchasePrices, SellingProfitPers, TotalPrices, ProfitMargins, SellingPrices, MarketPrices, seasonSales);
                            }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@pricelist", SqlDbType.Structured).Value = odt;
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.Read())
                        {
                            var purchase = Convert.ToString(rd[0]);
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
        }

        public List<PricelistCategory> GetCategoriesext()
        {
            List<PricelistCategory> list = new List<PricelistCategory>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Pricelist_GetCategoryListName]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            list.Add(new PricelistCategory
                            {
                                CatId = Convert.ToInt32(rd["Id"]),
                                Name = Convert.ToString(rd["Name"])
                            });
                        }
                        return list;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }


        //public List<PriceList> GetPricelistData()
        //{
        //    List<PriceList> PriceList = new List<PriceList>();

        //    using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[dbo].[PriceList_GetItemList] ", con))
        //        {
        //            try
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                con.Open();
        //                using (SqlDataReader rd = cmd.ExecuteReader())
        //                {
        //                    while (rd.Read())
        //                    {
        //                        PriceList.Add(new PriceList
        //                        {
        //                            PriceId = Convert.ToString(rd[0] == DBNull.Value ? "0" : rd[0]),
        //                            ItemId = Convert.ToString(rd[1] == DBNull.Value ? "0" : rd[1]),
        //                            pluName = Convert.ToString(rd[2] == DBNull.Value ? "0" : rd[2]),
        //                            Measurement = Convert.ToString(rd[3] == DBNull.Value ? "0" : rd[3]),
        //                            OrderQty = Convert.ToString(rd[4] == DBNull.Value ? "0" : rd[4]),
        //                            WasteagePerc = Convert.ToDouble(rd[5] == DBNull.Value ? "0" : rd[5]),
        //                            PurchasePrice = Convert.ToDouble(rd[6] == DBNull.Value ? "0" : rd[6]),
        //                            SellingPrice = Convert.ToDouble(rd[7] == DBNull.Value ? "0" : rd[7]),
        //                            TotalPrice = Convert.ToDouble(rd[8] == DBNull.Value ? "0" : rd[8]),
        //                            SellingProfitPer = Convert.ToDouble(rd[9] == DBNull.Value ? "0" : rd[9]),
        //                            ProfitMargin = Convert.ToDouble(rd[10] == DBNull.Value ? "0" : rd[10]),
        //                            MarketPrice = Convert.ToString(rd[11] == DBNull.Value ? "0" : rd[11]),
        //                            seasonSale = Convert.ToString(rd[12] == DBNull.Value ? "0" : rd[12]),
        //                            Id = Convert.ToInt32(rd[13] == DBNull.Value ? "0" : rd[13]),
        //                            DiscountPrice = Convert.ToDouble(rd["discountPrice"] == DBNull.Value ? 0 : rd["discountPrice"]),
        //                            OfferDiscount = Convert.ToDouble(rd["OfferDiscount"] == DBNull.Value ? 0 : rd["OfferDiscount"]),
        //                        });
        //                    }

        //                }



        //            }

        //            catch (Exception ex)
        //            {
        //                return PriceList;
        //            }

        //        }

        //    }
        //    return PriceList;
        //}



        List<PricelistCategory> IPricelistRI.GetBrandList()
        {
            List<PricelistCategory> GetBrandList = new List<PricelistCategory>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[PriceList_Brand]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {

                            GetBrandList.Add(new PricelistCategory
                            {
                                BrandId = Convert.ToInt32(rd["Id"]),
                                BrandName = Convert.ToString(rd["BrandName"])

                            });
                        }
                        return GetBrandList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }


        List<PricelistCategory> IPricelistRI.GetVendorList()
        {
            List<PricelistCategory> GetVendorList = new List<PricelistCategory>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[PriceList_Vendor]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {

                            GetVendorList.Add(new PricelistCategory
                            {
                                VendorId = Convert.ToInt32(rd["Id"]),
                                VendorName = Convert.ToString(rd["Business"])

                            });
                        }
                        return GetVendorList;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public List<PriceList> GetHubPricelist(PricelistFilter detail)
        {
            List<PriceList> PriceList = new List<PriceList>();

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Hub_GetPriceList", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@MainCategory", SqlDbType.Int).Value = detail.MainCategoryId;
                        cmd.Parameters.AddWithValue("@Category", SqlDbType.Int).Value = detail.CategoryId;
                        cmd.Parameters.AddWithValue("@Itemtype", SqlDbType.Int).Value = detail.ItemType;
                        cmd.Parameters.Add("@ApproavalType", SqlDbType.VarChar, 10).Value = detail.ApproavalType;
                        cmd.Parameters.Add("@HubId", SqlDbType.VarChar, 50).Value = detail.HubId;

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                PriceList.Add(new PriceList
                                {
                                    PriceId = Convert.ToString(rd["hPriceId"] == DBNull.Value ? "0" : rd["hPriceId"]),
                                    ItemId = Convert.ToString(rd["ItemId"] == DBNull.Value ? "0" : rd["ItemId"]),
                                    pluName = Convert.ToString(rd["PluName"] == DBNull.Value ? "0" : rd["PluName"]),
                                    Measurement = Convert.ToString(rd["Measurement"] == DBNull.Value ? "0" : rd["Measurement"]),
                                    OrderQty = Convert.ToString(rd["OrderQty"] == DBNull.Value ? "0" : rd["OrderQty"]),
                                    WasteagePerc = Convert.ToDouble(rd["Wastage"] == DBNull.Value ? "0" : rd["Wastage"]),
                                    PurchasePrice = Convert.ToDouble(rd["PurchasePrice"] == DBNull.Value ? "0" : rd["PurchasePrice"]),
                                    SellingPrice = Convert.ToDouble(rd["SellingPrice"] == DBNull.Value ? "0" : rd["SellingPrice"]),
                                    TotalPrice = Convert.ToDouble(rd["ActualCost"] == DBNull.Value ? "0" : rd["ActualCost"]),
                                    SellingProfitPer = Convert.ToDouble(rd["ProfitMargin"] == DBNull.Value ? "0" : rd["ProfitMargin"]),
                                    ProfitMargin = Convert.ToDouble(rd["ProfitPrice"] == DBNull.Value ? "0" : rd["ProfitPrice"]),
                                    MarketPrice = Convert.ToString(rd["MarketPrice"] == DBNull.Value ? "0" : rd["MarketPrice"]),
                                    seasonSale = Convert.ToString(rd["SeasonSale"] == DBNull.Value ? "N" : rd["SeasonSale"]),
                                    Id = Convert.ToInt32(rd[0] == DBNull.Value ? "0" : rd[0]),
                                });
                            }

                        }



                    }

                    catch (Exception ex)
                    {
                        return PriceList;
                    }

                }

            }
            return PriceList;
        }
        public void HubUpdatePrice(PriceList list)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Hub_UpdatePricelist]", con))
                    {
                        con.Open();
                        DataTable odt = new DataTable();
                        odt.Columns.Add("Ids", typeof(int));
                        odt.Columns.Add("WasteagePercs", typeof(double));
                        odt.Columns.Add("PurchasePrices", typeof(double));
                        odt.Columns.Add("SellingProfitPers", typeof(double));
                        odt.Columns.Add("TotalPrices", typeof(double));
                        odt.Columns.Add("ProfitMargins", typeof(double));
                        odt.Columns.Add("SellingPrices", typeof(double));
                        odt.Columns.Add("MarketPrices", typeof(double));
                        odt.Columns.Add("seasonSales", typeof(string));
                        if (list.Ids != null)
                            for (int i = 0; i < list.Ids.Count; i++)
                            {
                                var Ids = list.Ids[i];
                                var WasteagePercs = list.WasteagePercs[i];
                                var PurchasePrices = list.PurchasePrices[i];
                                var SellingProfitPers = list.SellingProfitPers[i];
                                var TotalPrices = list.TotalPrices[i];
                                var ProfitMargins = list.ProfitMargins[i];
                                var SellingPrices = list.SellingPrices[i];
                                var MarketPrices = list.MarketPrices[i];
                                var seasonSales = list.seasonSales[i];
                                odt.Rows.Add(Ids, WasteagePercs, PurchasePrices, SellingProfitPers, TotalPrices, ProfitMargins, SellingPrices, MarketPrices, seasonSales);
                            }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@pricelist", SqlDbType.Structured).Value = odt;
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.Read())
                        {
                            var purchase = Convert.ToString(rd[0]);
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
