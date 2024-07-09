using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Category;
using Freshlo.RI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Repository
{
    public class CategoriesRepository : ICategoriesRI
    {
        private IDbConfig _dbConfig { get; }
        public CategoriesRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        // MainCateogy Related Here...
        public List<MainCategory> GetMainCategoriesList(string id)
        {
            List<MainCategory> mainCategoreisList = new List<MainCategory>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[MainCategoryList_CategoryCount]", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                    con.Open();
                    try
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                mainCategoreisList.Add(new MainCategory
                                {

                                    Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"])),
                                    MainCategoryId = Convert.ToString(dr["MainCategoryId"] == DBNull.Value ? "NA" : Convert.ToString(dr["MainCategoryId"])),
                                    Name = Convert.ToString(dr["Name"] == DBNull.Value ? "NA" : Convert.ToString(dr["Name"])),
                                    Sequence = Convert.ToInt32(dr["Sequence"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Sequence"])),
                                    Visibility = Convert.ToInt32(dr["IsVisibile"] == DBNull.Value ? -1 : Convert.ToInt32(dr["IsVisibile"])),
                                    AddedBy = Convert.ToString(dr["AddedBy"]),
                                    AddedOn = Convert.ToDateTime(dr["AddedOn"]),
                                    Description = Convert.ToString(dr["Description"] == DBNull.Value ? "NA" : Convert.ToString(dr["Description"])),
                                    CategoryCount = Convert.ToInt32(dr["CategoryCount"]),
                                });

                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
            return mainCategoreisList;
        }
        public string GetExistmainCategoryCode()
        {
            var mainCategoryCode = "";
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Maincategory_GetExistmainCategoryCode]", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            mainCategoryCode += Convert.ToString(sdr[0]) + ",";
                        }
                    }
                }
            }
            return mainCategoryCode;
        }
        public string InsertMainCategory(string maincategoryName, string userId, string maincategoryCode)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[MainCategory_CreatMaincatg]", con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@maincategoryName", maincategoryName);
                    cmd.Parameters.AddWithValue("@AddedBy", userId);
                    cmd.Parameters.AddWithValue("@LastUpdatedBy", userId);
                    cmd.Parameters.AddWithValue("@maincategoryCode", maincategoryCode);
                    return Convert.ToString(cmd.ExecuteScalar());
                }
            }
        }
        public string AddMainCategory(MainCategory info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[MainCategory_CreatMaincatg]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@maincategoryName", info.Name);
                        cmd.Parameters.AddWithValue("@AddedBy", info.AddedBy);
                        cmd.Parameters.AddWithValue("@LastUpdatedBy", info.AddedBy);
                        cmd.Parameters.AddWithValue("@maincategoryCode", info.MainCategoryCode);
                        cmd.Parameters.AddWithValue("@Sequence", info.Sequence);
                        cmd.Parameters.AddWithValue("@Description", info.Description);
                        cmd.Parameters.AddWithValue("@IsVisibile", info.Visibility);
                        //cmd.Parameters.AddWithValue("@hubId", info.hubId);
                        
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
        }
        public MainCategory GetMainCategoryDetails(string id,string hubId)
        {
            var maincat = new MainCategory();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[MainCategory_GetDetailsById]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                       
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                maincat.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"]));
                                maincat.MainCategoryId = Convert.ToString(dr["MainCategoryId"] == DBNull.Value ? "NA" : Convert.ToString(dr["MainCategoryId"]));
                                maincat.Name = Convert.ToString(dr["Name"] == DBNull.Value ? "NA" : Convert.ToString(dr["Name"]));
                                maincat.MainCategoryCode = Convert.ToString(dr["MainCategoryCode"] == DBNull.Value ? "NA" : Convert.ToString(dr["MainCategoryCode"]));
                                maincat.Sequence = Convert.ToInt32(dr["Sequence"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Sequence"]));
                                maincat.Visibility = Convert.ToInt32(dr["IsVisibile"] == DBNull.Value ? -1 : Convert.ToInt32(dr["IsVisibile"]));
                                maincat.AddedBy = Convert.ToString(dr["AddedBy"]);
                                maincat.AddedOn = Convert.ToDateTime(dr["AddedOn"]);
                                maincat.LastUpdatedBy = Convert.ToString(dr["LastUpdatedBy"]);
                                maincat.LastUpdatedOn = Convert.ToDateTime(dr["LastUpdatedOn"]);
                                maincat.Description = Convert.ToString(dr["Description"] == DBNull.Value ? "NA" : Convert.ToString(dr["Description"]));

                            }
                            else
                            {
                                maincat.MainCategoryId = id;
                            }
                        }
                        return maincat;
                    }
                    catch (Exception e)
                    {
                        return maincat;
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

        public int GetMainCategoryDetailsCount(string id, string hubId)
        {
            int IshubMap = 0;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[usp_hubMappedMaincategoryCoun]";
                        cmd.CommandType = CommandType.StoredProcedure;
                     
                        cmd.Parameters.Add("@mainCatId", SqlDbType.VarChar).Value = id;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                IshubMap = Convert.ToInt32(dr["IsHubMapped"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IsHubMapped"]));
                            }
                        }
                        return IshubMap;
                    }
                    catch (Exception e)
                    {
                        return IshubMap;
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


        public string UpdateMainCategory(MainCategory info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[MainCategory_EditMaincatg]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = info.Id;
                        con.Open();
                        cmd.Parameters.AddWithValue("@maincategoryName", info.Name);
                        cmd.Parameters.AddWithValue("@LastUpdatedBy", info.LastUpdatedBy);
                        cmd.Parameters.AddWithValue("@Sequence", info.Sequence);
                        cmd.Parameters.AddWithValue("@Description", info.Description);
                        cmd.Parameters.AddWithValue("@IsVisibile", info.Visibility);
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
        }
        public bool DeleteMaincategory(string id)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[MainCategory_DeleteRelatedCategories]", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                        cmd.ExecuteNonQuery();
                        return true;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }


        }


        // Cateogry Related Here
        public string GetExistCategoryCode()
        {
            var categoryCode = "";
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[tblCategory_GetExistCategoryCode]", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            categoryCode += Convert.ToString(sdr[0]) + ",";
                        }
                    }
                }
            }
            return categoryCode;
        }
        public List<ItemCategoreis> GetItemCategoriesList(string id, string hubId)
        {
            List<ItemCategoreis> ItemCategoreis = new List<ItemCategoreis>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[tblCategoryList_GetListByMaincategoryId]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                  
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ItemCategoreis.Add(new ItemCategoreis
                            {
                                MainCategoryId = Convert.ToString(sdr["MainCategoryId"] == DBNull.Value ? "NA" : Convert.ToString(sdr["MainCategoryId"])),
                                CategoriesName = Convert.ToString(sdr["Name"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Name"])),
                                AddedDate = Convert.ToDateTime(sdr["AddedDate"]),
                                AddedBy = Convert.ToString(sdr["AddedBy"] == DBNull.Value ? "NA" : Convert.ToString(sdr["AddedBy"])),
                                CategDescription = Convert.ToString(sdr["Description"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Description"])),
                                CategorySequence = Convert.ToInt32(sdr["Sequence"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["Sequence"])),
                                CategVisibility = Convert.ToBoolean(sdr["IsVisible"]),
                                Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["Id"])),
                                SubCategoryCount = Convert.ToInt32(sdr["SubCategoryCount"]),
                                CategoryId = Convert.ToString(sdr["CategoryId"] == DBNull.Value ? "NA" : Convert.ToString(sdr["CategoryId"]))

                            });

                        }
                    }
                }
            }
            return ItemCategoreis;
        }
        public bool DeleteReleatedCategories(int id)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[MainCategory_DeleteRelatedCategories]", con))
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
        public string AddItemCategories(ItemCategoreis info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[tblCategory_CreateItemCategories]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@categoryName", info.CategoriesName);
                        cmd.Parameters.AddWithValue("@AddedBy", info.AddedBy);
                        cmd.Parameters.AddWithValue("@LastUpdatedBy", info.AddedBy);
                        cmd.Parameters.AddWithValue("@MainCategoryId", info.MainCategoryId);
                        cmd.Parameters.AddWithValue("@categoryCode", info.CategoryCode);
                        cmd.Parameters.AddWithValue("@Description", info.CategDescription);
                        cmd.Parameters.AddWithValue("@Visibility", info.CategVisibility);
                        cmd.Parameters.AddWithValue("@Sequence", info.CategorySequence);
                      
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
        }
        public ItemCategoreis GetCategoryDetails(string id, string hubId)
        {
            var maincat = new ItemCategoreis();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[tblCategories_GetDetailsById]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                        
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                maincat.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"]));
                                maincat.MainCategoryId = Convert.ToString(dr["MainCategoryId"] == DBNull.Value ? "NA" : Convert.ToString(dr["MainCategoryId"]));
                                maincat.CategoriesName = Convert.ToString(dr["Name"] == DBNull.Value ? "NA" : Convert.ToString(dr["Name"]));
                                maincat.CategoryCode = Convert.ToString(dr["CategoryCode"] == DBNull.Value ? "NA" : Convert.ToString(dr["CategoryCode"]));
                                maincat.CategorySequence = Convert.ToInt32(dr["Sequence"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Sequence"]));
                                maincat.CategVisibility = Convert.ToBoolean(dr["IsVisible"]);
                                maincat.AddedBy = Convert.ToString(dr["AddedBy"]);
                                maincat.AddedDate = Convert.ToDateTime(dr["AddedDate"]);
                                maincat.LastUpdatedBy = Convert.ToString(dr["LastUpdatedBy"]);
                                maincat.LastUpdatedDate = Convert.ToDateTime(dr["LastUpdatedDate"]);
                                maincat.CategDescription = Convert.ToString(dr["Description"] == DBNull.Value ? "NA" : Convert.ToString(dr["Description"]));
                                maincat.CategoryId = Convert.ToString(dr["CategoryId"] == DBNull.Value ? "NA" : Convert.ToString(dr["CategoryId"]));
                            }
                        }
                        return maincat;
                    }
                    catch (Exception e)
                    {
                        return maincat;
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
        public string UpdateItemCategory(ItemCategoreis info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[dbo].[tblCategory_EditItemcatgories]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = info.Id;
                        con.Open();
                        cmd.Parameters.AddWithValue("@maincategoryName", info.CategoriesName);
                        cmd.Parameters.AddWithValue("@LastUpdatedBy", info.LastUpdatedBy);
                        cmd.Parameters.AddWithValue("@Sequence", info.CategorySequence);
                        cmd.Parameters.AddWithValue("@Description", info.CategDescription);
                        cmd.Parameters.AddWithValue("@IsVisibile", info.CategVisibility);
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
        }
        public string GetExistCategoryId(string id,string hubId)
        {
            var categoryCode = "";
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[tblCategory_GetExistingCategoieslist]", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            categoryCode += Convert.ToString(sdr[0]) + ",";
                        }
                    }
                }
            }
            return categoryCode;
        }
        public bool DeleteFrmCatdata(string id)
        {
            SqlTransaction transaction = null;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                try
                {
                    con.Open();
                    transaction = con.BeginTransaction();
                    using (SqlCommand cmd = new SqlCommand("[dbo].[tblCategory_Gettblcatgorydeletealldata]", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd1 = new SqlCommand("[dbo].[tblCategory_Gettblsubcatgorydeletealldata]", con, transaction))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@Id", id);
                        cmd1.ExecuteNonQuery();

                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    if (transaction != null) transaction.Rollback();
                    return false;
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                        if (transaction != null) transaction.Dispose();
                    }
                }
            }
        }

        //SubCateogry Related Here....
        public bool CheckSubCategories(ItemSubCategory info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[tblSubcategory_Isexits]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryId", info.CategoryId);
                    cmd.Parameters.AddWithValue("@subcategoryName", info.SubCategoryName);
                    try
                    {
                        con.Open();
                        if (null == cmd.ExecuteScalar())
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Do nothing.
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return true;
        }
        
        public string AddSubCategoies(ItemSubCategory info)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {        
                        using (SqlCommand cmd = new SqlCommand("[dbo].[tblSubCategory_CreateSubItemCategories]", con))
                        {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CategoryId", info.CategoryId);
                        cmd.Parameters.AddWithValue("@Name", info.SubCategoryName);
                        cmd.Parameters.AddWithValue("@AddedBy", info.AddedBy);
                        cmd.Parameters.AddWithValue("@LastUpdatedBy", info.LastUpdatedBy);
                        
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                catch(Exception ex)
                {
                    throw;
                }
        }
        public List<ItemSubCategory> GetItemSubCategoriesList(string id,string hubId)
        {
            List<ItemSubCategory> ItemsubCategoreis = new List<ItemSubCategory>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[ItemSubCategories_GetListbYiD]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                   
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ItemsubCategoreis.Add(new ItemSubCategory
                            {
                                SubCategoryId = Convert.ToString(sdr["SubCategoryId"] == DBNull.Value ? "NA" : Convert.ToString(sdr["SubCategoryId"])),
                                SubCategoryName = Convert.ToString(sdr["Name"] == DBNull.Value ? "NA" : Convert.ToString(sdr["Name"])),
                                AddedDate = Convert.ToDateTime(sdr["AddedDate"]),
                                AddedBy = Convert.ToString(sdr["AddedBy"] == DBNull.Value ? "NA" : Convert.ToString(sdr["AddedBy"])),
                                Id = Convert.ToInt32(sdr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["Id"])),
                                CategoryId = Convert.ToString(sdr["CategoryId"] == DBNull.Value ? "NA" : Convert.ToString(sdr["CategoryId"]))

                            });

                        }
                    }
                }
            }
            return ItemsubCategoreis;
        }
        public bool DeleteSubcategory(string id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[tblSubCategory_DeleteSubcategory]", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                        return cmd.ExecuteNonQuery() > 0;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
        }

        public int AddeIntoHub(string mainCatId, string hubId, string createdBy)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_InsertMainCategorytoHub]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mainCatId", mainCatId);
                    cmd.Parameters.AddWithValue("@hubId", hubId);
                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }

            }
        }

        public bool RemoveIntoHub(string mainCatId, string hubId, string createdBy)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_RemovalMainCategorytoHub]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mainCatId", mainCatId);
                    cmd.Parameters.AddWithValue("@hubId", hubId);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public int GetLanguagecategory(ItemSubCategory cat)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[categoryLanguage_CreateLanguage]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.VarChar, 100).Value = cat.CategoryId;
                    cmd.Parameters.Add("@CategoryLanguage", SqlDbType.NVarChar, 100).Value = cat.CategoryLanguage;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = cat.CreatedBy;
                    cmd.Parameters.Add("@Languagecode", SqlDbType.VarChar, 50).Value = cat.LanguageName;
                    cmd.Parameters.Add("@CategoryDescription", SqlDbType.NVarChar, 50).Value = cat.CategoryDescription;
                    cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 50).Value = cat.hubId;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
        }

        public int GetLanguageMainCategory(LanguageMst maincat)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[maincategoryLanguage_CreateLanguage]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@mainCategoryId", SqlDbType.VarChar, 100).Value = maincat.MainCategoryId;
                    cmd.Parameters.Add("@mainCategoryLanguage", SqlDbType.NVarChar, -1).Value = maincat.MainCatNameLanguage;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = maincat.CreatedBy;
                    cmd.Parameters.Add("@Languagecode", SqlDbType.VarChar, 50).Value = maincat.LanguageName;
                    cmd.Parameters.Add("@mainCategoryDescription", SqlDbType.NVarChar, -1).Value = maincat.Descriptionlanguage;
                    cmd.Parameters.Add("@Hub", SqlDbType.VarChar, 50).Value = maincat.Hub;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
        }

        public List<MainCategory> GetLanguageMainCategoriesList(string id, string hubId)
        {
            List<MainCategory> mainlanguageCategoreisList = new List<MainCategory>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[USP_LanguageMainCategoryList]", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = hubId;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                    con.Open();
                    try
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                mainlanguageCategoreisList.Add(new MainCategory
                                {
                                    MainCategoryId = Convert.ToString(dr["MainCategoryId"] == DBNull.Value ? "NA" : Convert.ToString(dr["MainCategoryId"])),
                                    MainCategoryLanguage = Convert.ToString(dr["MainCategoryLanguage"] == DBNull.Value ? "NA" : Convert.ToString(dr["MainCategoryLanguage"])),
                                    MainCategoryDescription = Convert.ToString(dr["MainCategoryDescription"] == DBNull.Value ? "NA" : Convert.ToString(dr["MainCategoryDescription"])),
                                });

                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
            return mainlanguageCategoreisList;
        }

        public List<ItemSubCategory> GetLanguageSubCategoriesList(string id, string hubId)
        {
            List<ItemSubCategory> SublanguageCategoreisList = new List<ItemSubCategory>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[USP_LanguageSubCategoryList]", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@hubId", SqlDbType.VarChar).Value = hubId;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                    con.Open();
                    try
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                SublanguageCategoreisList.Add(new ItemSubCategory
                                {
                                    CategoryId = Convert.ToString(dr["CategoryId"] == DBNull.Value ? "NA" : Convert.ToString(dr["CategoryId"])),
                                    CategoryLanguage = Convert.ToString(dr["CategoryLanguage"] == DBNull.Value ? "NA" : Convert.ToString(dr["CategoryLanguage"])),
                                    CategoryDescription = Convert.ToString(dr["CategoryDescription"] == DBNull.Value ? "NA" : Convert.ToString(dr["CategoryDescription"])),
                                });

                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
            return SublanguageCategoreisList;
        }

        public int uploadimage(string id)
        {
            using(SqlConnection con =new SqlConnection(_dbConfig.ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_updateImage]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());    
                }
            }
        }

        public int uploadimagecategory(string id)
        {
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "[dbo].[usp_updateImagecategory]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
