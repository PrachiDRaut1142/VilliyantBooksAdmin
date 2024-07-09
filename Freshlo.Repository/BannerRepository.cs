using Freshlo.DomainEntities.Banner;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Freshlo.Repository
{
    public class BannerRepository : BannerRI
    {
        private IDbConfig _dbConfig { get; }
        public BannerRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }


        public List<Banner> GetMancategorylist()
        {
            List<Banner> GetMainCategorySelectList = new List<Banner>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetMainCategoryList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                try
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {

                            GetMainCategorySelectList.Add(new Banner
                            {
                                MancategoryId = Convert.ToString(rd["MainCategoryId"]),
                                Mancategoryname = Convert.ToString(rd["Name"])

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

        public List<SelectListItem> GetAcctionTriggerlist(string trigger)
        {

            //if (trigger == "Offer")
            //{
            //    trigger = "Custom Item";
            //}
            List<SelectListItem> Getactiontrglist = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[GetActionTriggerId]", con))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = trigger;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["Name"]);
                        string Id = Convert.ToString(rd["Id"]);
                        Getactiontrglist.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return Getactiontrglist;
                }
            }


        }

        public List<Banner> GetBannerList(string hubId)
        {
            List<Banner> Getbannerlist = new List<Banner>();

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_NewBanner_Getbannerlist]", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@hubId",SqlDbType.VarChar,50).Value = hubId;
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Getbannerlist.Add(new Banner
                                {
                                    Id = Convert.ToInt32(dr[0]),
                                    BannerId = Convert.ToString(dr["BannerId"]),
                                    Name = Convert.ToString(dr["Banner_Name"] == DBNull.Value ? "NA" : dr["Banner_Name"]),
                                    MainCategory = Convert.ToString(dr["Banner_MainCategory_Name"] == DBNull.Value ? "NA" : dr["Banner_MainCategory_Name"]),
                                    Place = Convert.ToString(dr["Banner_Place"] == DBNull.Value ? "NA" : dr["Banner_Place"]),
                                    ActionTrigger = Convert.ToString(dr["Banner_ActionTrigger"] == DBNull.Value ? "NA" : dr["Banner_ActionTrigger"]),
                                    RefferTag = Convert.ToString(dr["RefferTag"] == DBNull.Value ? "NA" : dr["RefferTag"]),
                                    VideoLink = Convert.ToString(dr["VideoLink"] == DBNull.Value ? "NA" : dr["VideoLink"]),
                                    TextLink = Convert.ToString(dr["Link"] == DBNull.Value ? "NA" : dr["Link"]),
                                    Status = Convert.ToString(dr["Status"] == DBNull.Value ? "NA" : dr["Status"]),
                                    CreatedOn = Convert.ToDateTime(dr["CreatedOn"] == DBNull.Value ? "NA" : dr["CreatedOn"]),
                                    UpdatedOn = Convert.ToDateTime(dr["UpdatedOn"] == DBNull.Value ? "NA" : dr["UpdatedOn"]),
                                    CreatedBy = Convert.ToString(dr["CreatedBy"] == DBNull.Value ? "NA" : dr["CreatedBy"]),
                                    UpdatedBy = Convert.ToString(dr["UpdatedOn"] == DBNull.Value ? "NA" : dr["UpdatedOn"]),
                                    Size = Convert.ToString(dr["Banner_Size"] == DBNull.Value ? "NA": dr["Banner_Size"])
                                });
                            }
                        }

                    }

                    catch (Exception)
                    {

                        throw;
                    }
                    return Getbannerlist;
                }
            }
        }

        public int CreateBanner(Banner banner)
        {
            banner.VideoLink = string.IsNullOrEmpty(banner.VideoLink) ? "NA" : banner.VideoLink;
            banner.TextLink = string.IsNullOrEmpty(banner.TextLink) ? "NA" : banner.TextLink;
            banner.Name = string.IsNullOrEmpty(banner.Name) ? "NA" : banner.Name;
            banner.MainCategory = string.IsNullOrEmpty(banner.MainCategory) ? "NA" : banner.MainCategory;
            if (banner.ScrollBaner == "Y")
            {
                banner.Place = string.IsNullOrEmpty(banner.Place) ? "SB" : banner.Place;
                banner.RefferTag = "1";
            }
            else
            {
                banner.Place = string.IsNullOrEmpty(banner.Place) ? "Top" : banner.Place;
                banner.RefferTag = "0";
            }
            if (banner.VideoLink != "NA" && banner.VideoLink != null )
            {
                banner.TriggerId = banner.VideoLink;
            }
            if (banner.TextLink != "NA" && banner.TextLink !=  null)
            {
                banner.TriggerId = banner.TextLink;
            }
            if (banner.VideoLink == "NA"  && banner.TextLink == "NA")
            {
                banner.TriggerId = string.IsNullOrEmpty(banner.TriggerId) ? "NA" : banner.TriggerId;
            }
            if (banner.BranchMapped == true)
            {
                banner.BranchMapped1 = "No";
            }
            if (banner.BranchMapped == false)
            {
                banner.BranchMapped1 = "Yes";
            }
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[NewBanner_CreateBanner]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = banner.Id;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar,70).Value = banner.Name;
                        cmd.Parameters.Add("@MainCategory", SqlDbType.VarChar,50).Value = banner.MainCategory;
                        cmd.Parameters.Add("@Place", SqlDbType.VarChar,50).Value = banner.Place;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar,20).Value = banner.Status;
                        cmd.Parameters.Add("@VideoLink", SqlDbType.VarChar,100).Value = banner.VideoLink;
                        cmd.Parameters.Add("@TextLink", SqlDbType.VarChar,100).Value = banner.TextLink;
                        cmd.Parameters.Add("@Createdby", SqlDbType.VarChar,20).Value = banner.CreatedBy;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar,20).Value = banner.CreatedBy;
                        cmd.Parameters.Add("@RefferTag", SqlDbType.VarChar,5).Value = banner.RefferTag;
                        cmd.Parameters.Add("@Banner_ActionTrigger", SqlDbType.VarChar, 100).Value = banner.ActionTrigger;
                        cmd.Parameters.Add("@ActionTrigger", SqlDbType.VarChar,100).Value = banner.TriggerId;
                        cmd.Parameters.Add("@Size", SqlDbType.VarChar,100).Value = banner.Size;
                        cmd.Parameters.Add("@BranchMapped1", SqlDbType.VarChar,50).Value = banner.BranchMapped1;
                        cmd.Parameters.Add("@Branch", SqlDbType.VarChar,50).Value = banner.Branch;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception e)
                    {
                        return 0;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }

                }
            }
        }

        public int UpdaetBanner(Banner banner)
        {
            banner.VideoLink = string.IsNullOrEmpty(banner.VideoLink) ? "NA" : banner.VideoLink;
            banner.TextLink = string.IsNullOrEmpty(banner.TextLink) ? "NA" : banner.TextLink;
            banner.Name = string.IsNullOrEmpty(banner.Name) ? "NA" : banner.Name;
            banner.MainCategory = string.IsNullOrEmpty(banner.MainCategory) ? "NA" : banner.MainCategory;
            if (banner.ScrollBaner == "Y")
            {
                banner.Place = string.IsNullOrEmpty(banner.Place) ? "SB" : banner.Place;
                banner.RefferTag = "1";
            }
            else
            {
                banner.Place = string.IsNullOrEmpty(banner.Place) ? "Top" : banner.Place;
                banner.RefferTag = "0";
            }
            if (banner.ActionTrigger == "Video Link")
            {
                banner.TriggerId = banner.VideoLink;
                banner.TextLink = banner.TextLink;
            }
            if (banner.ActionTrigger == "Third Party Link")
            {
                banner.TriggerId = banner.TextLink;
            }
            if (banner.ActionTrigger == "Inner Web View")
            {
                banner.TriggerId = banner.TextLink;
            }
            if (banner.BranchMapped == true)
            {
                banner.BranchMapped1 = "No";
            }
            if (banner.BranchMapped == false)
            {
                banner.BranchMapped1 = "Yes";
            }
            //banner.TriggerId = string.IsNullOrEmpty(banner.TriggerId) ? "NA" : banner.TriggerId;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[NewBanner_UpdateBanner]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = banner.Id;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = banner.Name;
                        cmd.Parameters.Add("@MainCategory", SqlDbType.VarChar).Value = banner.MainCategory;
                        cmd.Parameters.Add("@Place", SqlDbType.VarChar).Value = banner.Place;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = banner.Status;
                        cmd.Parameters.Add("@VideoLink", SqlDbType.VarChar).Value = banner.VideoLink;
                        cmd.Parameters.Add("@TextLink", SqlDbType.VarChar).Value = banner.TextLink;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = banner.UpdatedBy;
                        cmd.Parameters.Add("@RefferTag", SqlDbType.VarChar).Value = banner.RefferTag; 
                        cmd.Parameters.Add("@ActionTrigger", SqlDbType.VarChar).Value = banner.TriggerId;
                        cmd.Parameters.Add("@Size", SqlDbType.VarChar).Value = banner.Size;
                        cmd.Parameters.Add("@Banner_ActionTrigger", SqlDbType.VarChar).Value = banner.ActionTrigger;
                        cmd.Parameters.Add("@T_Id", SqlDbType.Int).Value = banner.T_Id;
                        cmd.Parameters.Add("@BranchMapped1", SqlDbType.VarChar, 50).Value = banner.BranchMapped1;
                        cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 50).Value = banner.Branch;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                    catch (Exception e)
                    {
                        return 0;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }

                }
            }
        }

        public Banner GetbannerDetails(string id)
        {
            var banner = new Banner();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[NewBanner_GetDetailsbyId]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                banner.Id = Convert.ToInt32(dr[0]);
                                banner.BannerId = Convert.ToString(dr["BannerId"]);
                                banner.Name = Convert.ToString(dr["Banner_Name"]);
                                banner.MainCategory = Convert.ToString(dr["Banner_MainCategory_Name"]);
                                banner.Place = Convert.ToString(dr["Banner_Place"]);
                                banner.ActionTrigger = Convert.ToString(dr["Banner_ActionTrigger"]);
                                banner.RefferTag = Convert.ToString(dr["RefferTag"]);
                                banner.VideoLink = Convert.ToString(dr["VideoLink"]);
                                banner.TextLink = Convert.ToString(dr["Link"]);
                                banner.Status = Convert.ToString(dr["Status"]);
                                banner.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                                banner.UpdatedOn = Convert.ToDateTime(dr["UpdatedOn"]);
                                banner.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                                banner.UpdatedBy = Convert.ToString(dr["UpdatedOn"]);
                                banner.VideoLink = Convert.ToString(dr["VideoLink"]);
                                banner.TriggerId = Convert.ToString(dr["TriggerId"]);
                                banner.T_Id = Convert.ToInt32(dr["T_Id"]);
                                banner.Size = Convert.ToString(dr["Banner_Size"]);
                                banner.BranchMapped1 = Convert.ToString(dr["IsallBranch"]);
                                banner.Branch = Convert.ToString(dr["BranchId"]);
                              
                            }
                        }
                        return banner;
                    }
                    catch (Exception e)
                    {
                        return banner;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }

                }
            }
        }

        public bool DeleteBanner(int id)
        {
               using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                    try
                    {
                       using (SqlCommand cmd = new SqlCommand("[dbo].[NewBanner_DeleteBanner]", con))
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

      
    }
}