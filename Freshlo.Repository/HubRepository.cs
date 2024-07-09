using Freshlo.DomainEntities.Hub;
using Freshlo.RI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Freshlo.Repository
{
    public class HubRepository : IHubRI
    {
        private IDbConfig _dbConfig { get; }
        public HubRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public List<Hub> hublist()
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

        public int CreateHub(Hub info)
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
                        cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = info.ContactNo;
                        cmd.Parameters.Add("@BrnachEmail", SqlDbType.VarChar).Value = info.BrnachEmail;
                        cmd.Parameters.Add("@BranchNotifyEmail", SqlDbType.VarChar).Value = info.BranchNotifyEmail;
                        cmd.Parameters.Add("@MapCode", SqlDbType.VarChar).Value = info.MapCode;
                        cmd.Parameters.Add("@ShortCode", SqlDbType.VarChar).Value = info.currency;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        int rowCount = 0;
                        while (dr.Read())
                        {
                            rowCount = Convert.ToInt32(dr[0]);
                        }
                        dr.Close();
                        return rowCount;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
        }

        public int UpdateHub(Hub info)
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
                        cmd.Parameters.Add("@ShortCode", SqlDbType.VarChar).Value = info.currency;
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
                        cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = info.ContactNo;
                        cmd.Parameters.Add("@BrnachEmail", SqlDbType.VarChar).Value = info.BrnachEmail;
                        cmd.Parameters.Add("@BranchNotifyEmail", SqlDbType.VarChar).Value = info.BranchNotifyEmail;
                        cmd.Parameters.Add("@MapCode", SqlDbType.VarChar).Value = info.MapCode;
                        cmd.Parameters.Add("@IsFacebookEnable", SqlDbType.Bit).Value = info.IsFacebookEnable;
                        cmd.Parameters.Add("@FacebookLink", SqlDbType.VarChar).Value = info.FacebookLink;
                        cmd.Parameters.Add("@IsInstaEnable", SqlDbType.Bit).Value = info.IsInstaEnable;
                        cmd.Parameters.Add("@InstaLink", SqlDbType.VarChar).Value = info.InstaLink;
                        cmd.Parameters.Add("@IsSnapchatEnable", SqlDbType.Bit).Value = info.IsSnapchatEnable;
                        cmd.Parameters.Add("@SnapChatLink", SqlDbType.VarChar).Value = info.SnapChatLink;
                        cmd.Parameters.Add("@IsTwitterEnable", SqlDbType.Bit).Value = info.IsTwitterEnable;
                        cmd.Parameters.Add("@TwitterLink", SqlDbType.VarChar).Value = info.TwitterLink;
                        cmd.Parameters.Add("@IsWhatsAppEnable", SqlDbType.Bit).Value = info.IsWhatsAppEnable;
                        cmd.Parameters.Add("@WhatsAppLink", SqlDbType.VarChar).Value = info.WhatsAppLink;
                        cmd.Parameters.Add("@IsGoogleMapEnable", SqlDbType.Bit).Value = info.IsGoogleMapEnable;
                        cmd.Parameters.Add("@GoogleMapLink", SqlDbType.VarChar).Value = info.GoogleMapLink;
                        cmd.Parameters.Add("@IsGoogleReviewEnable", SqlDbType.Bit).Value = info.IsGoogleReviewEnable;
                        cmd.Parameters.Add("@IsGoogleReviewLink", SqlDbType.VarChar).Value = info.IsGoogleReviewLink;
                        cmd.Parameters.Add("@IsPrinterestEnable", SqlDbType.Bit).Value = info.IsPrinterestEnable;
                        cmd.Parameters.Add("@PrinterestLink", SqlDbType.VarChar).Value = info.PrinterestLink;
                        cmd.Parameters.Add("@IsYoutubeEnable", SqlDbType.Bit).Value = info.IsYoutubeEnable;
                        cmd.Parameters.Add("@YoutubeLink", SqlDbType.VarChar).Value = info.YoutubeLink;
                        cmd.Parameters.Add("@IsLinkedInEnable", SqlDbType.Bit).Value = info.IsLinkedInEnable;
                        cmd.Parameters.Add("@LinkedInLink", SqlDbType.VarChar).Value = info.LinkedInLink;
                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
        }

        public Hub Hubdetails(string id)
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
                                HubId = MappingHelpers.GenericReferenceType<string>(rd["HubId"]),
                                currency = MappingHelpers.GenericReferenceType<string>(rd["ShortCode"]),
                                HubName = MappingHelpers.GenericReferenceType<string>(rd["HubName"]),
                                Area = MappingHelpers.GenericReferenceType<string>(rd["Area"]),
                                BuildingName = MappingHelpers.GenericReferenceType<string>(rd["BuildingName"]),
                                RoomNo = MappingHelpers.GenericReferenceType<string>(rd["RoomNo"]),
                                Sector = MappingHelpers.GenericReferenceType<string>(rd["Sector"]),
                                Landmark = MappingHelpers.GenericReferenceType<string>(rd["Landmark"]),
                                City = MappingHelpers.GenericReferenceType<string>(rd["City"]),
                                State = MappingHelpers.GenericReferenceType<string>(rd["State"]),
                                Country = MappingHelpers.GenericReferenceType<string>(rd["Country"]),
                                MapCode = MappingHelpers.GenericReferenceType<string>(rd["ContactNo"]),
                                BranchNotifyEmail = MappingHelpers.GenericReferenceType<string>(rd["BranchNotifyEmail"]),
                                BrnachEmail = MappingHelpers.GenericReferenceType<string>(rd["BranchEmail"]),
                                ContactNo = MappingHelpers.GenericReferenceType<string>(rd["MapCode"]),
                                IsFacebookEnable = MappingHelpers.GenericValueType<bool>(rd["IsFacebookEnable"]),
                                FacebookLink = MappingHelpers.GenericReferenceType<string>(rd["FacebookLink"]),
                                IsInstaEnable = MappingHelpers.GenericValueType<bool>(rd["IsInstaEnable"]),
                                InstaLink = MappingHelpers.GenericReferenceType<string>(rd["InstaLink"]),
                                IsTwitterEnable = MappingHelpers.GenericValueType<bool>(rd["IsTwitterEnable"]),
                                TwitterLink = MappingHelpers.GenericReferenceType<string>(rd["TwitterLink"]),
                                IsSnapchatEnable = MappingHelpers.GenericValueType<bool>(rd["IsSnapchatEnable"]),
                                SnapChatLink = MappingHelpers.GenericReferenceType<string>(rd["SnapChatLink"]),
                                IsLinkedInEnable = MappingHelpers.GenericValueType<bool>(rd["IsLinkedInEnable"]),
                                LinkedInLink = MappingHelpers.GenericReferenceType<string>(rd["LinkedInLink"]),
                                IsGoogleMapEnable = MappingHelpers.GenericValueType<bool>(rd["IsGoogleMapEnable"]),
                                GoogleMapLink = MappingHelpers.GenericReferenceType<string>(rd["GoogleMapLink"]),
                                IsPrinterestEnable = MappingHelpers.GenericValueType<bool>(rd["IsPrinterestEnable"]),
                                PrinterestLink = MappingHelpers.GenericReferenceType<string>(rd["PrinterestLink"]),
                                IsWhatsAppEnable = MappingHelpers.GenericValueType<bool>(rd["IsWhatsAppEnable"]),
                                WhatsAppLink = MappingHelpers.GenericReferenceType<string>(rd["WhatsAppLink"]),
                                IsYoutubeEnable = MappingHelpers.GenericValueType<bool>(rd["IsYoutubeEnable"]),
                                YoutubeLink = MappingHelpers.GenericReferenceType<string>(rd["YoutubeLink"]),
                                IsGoogleReviewEnable = MappingHelpers.GenericValueType<bool>(rd["IsGoogleReviewEnable"]),
                                IsGoogleReviewLink = MappingHelpers.GenericReferenceType<string>(rd["IsGoogleReviewLink"]),

                            };
                        }
                    }
                    return hubdetail;
                }
            }
        }

        public int DeleteHub(int id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Hub_DeleteHub]", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        return cmd.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
        }

        public int FacebookUpdate(bool isEnable, int branchId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_IsFacebookEnable]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(isEnable);
                cmd.Parameters.Add("@branchId", SqlDbType.VarChar, 100).Value = branchId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int InstaUpdate(bool isEnable, int branchId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_IsInstaEnable]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(isEnable);
                cmd.Parameters.Add("@branchId", SqlDbType.VarChar, 100).Value = branchId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int TwitterUpdate(bool isEnable, int branchId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_IsTwitterEnable]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(isEnable);
                cmd.Parameters.Add("@branchId", SqlDbType.VarChar, 100).Value = branchId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int SnapchatUpdate(bool isEnable, int branchId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_IsSnapchatEnable]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(isEnable);
                cmd.Parameters.Add("@branchId", SqlDbType.VarChar, 100).Value = branchId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int LinkedInUpdate(bool isEnable, int branchId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_IsLinkedInEnable]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(isEnable);
                cmd.Parameters.Add("@branchId", SqlDbType.VarChar, 100).Value = branchId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int GoogleMapUpdate(bool isEnable, int branchId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_IsGoogleMapEnable]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(isEnable);
                cmd.Parameters.Add("@branchId", SqlDbType.VarChar, 100).Value = branchId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int PrinterestUpdate(bool isEnable, int branchId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_IsPrinterestEnable]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(isEnable);
                cmd.Parameters.Add("@branchId", SqlDbType.VarChar, 100).Value = branchId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int WhatsAppUpdate(bool isEnable, int branchId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_IsWhatsAppEnable]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(isEnable);
                cmd.Parameters.Add("@branchId", SqlDbType.VarChar, 100).Value = branchId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int YoutubeUpdate(bool isEnable, int branchId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_IsYoutubeEnable]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(isEnable);
                cmd.Parameters.Add("@branchId", SqlDbType.VarChar, 100).Value = branchId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int GoogleReviewUpdate(bool isEnable, int branchId)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[usp_IsGoogleReviewEnable]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@isEnable", SqlDbType.Bit).Value = MappingHelpers.GenericValueType<bool>(isEnable);
                cmd.Parameters.Add("@branchId", SqlDbType.VarChar, 100).Value = branchId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
