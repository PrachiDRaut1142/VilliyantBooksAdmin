using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface ICategoriesSI
    {

        // MainCategory Related Here...
        Task<List<MainCategory>> GetMainCategoriesList(string id);
        Task<List<MainCategory>> GetLanguageMainCategoriesList(string id, string hubId);
        Task<List<ItemSubCategory>> GetLanguageSubCategoriesList(string id, string hubId);
        string GetExistmainCategoryCode();
        Task<string> InsertMainCategory(string maincategoryName, string userId, string maincategoryCode);
        Task<string> AddMainCategory(MainCategory info);
        Task<MainCategory> GetMainCategoryDetails(string id, string hubId);
        Task<int> GetMainCategoryDetailsCount(string id, string hubId);
        Task<string> UpdateMainCategory(MainCategory info);
        bool DeleteMaincategory(string id);


        //Cateogry Related Here...
        string GetExistCategoryCode();
        Task<List<ItemCategoreis>> GetItemCategoriesList(string id, string hubId);
        Task<bool> DeleteReleatedCategories(int id);
        Task<string> AddItemCategories(ItemCategoreis info);
        Task<string> UpdateItemCategory(ItemCategoreis info);
        Task<ItemCategoreis> GetCategoryDetails(string id, string hubId);
        bool DeleteFrmCatdata(string id);
        string GetExistCategoryId(string id,string hubId);


        // SubCateogory Related Here...
        Task<bool> CheckSubCategories(ItemSubCategory info);
        Task<string> AddSubItemCategories(ItemSubCategory info);
        Task<List<ItemSubCategory>> GetItemSubCategoriesList(string id, string hubId);
        Task<bool> DeleteSubcategories(string id);
        Task<bool> RemoveIntoHub(string mainCatId, string hubId, string createdBy);
        Task<int> AddeIntoHub(string mainCatId, string hubId, string createdBy);
        Task<int> GetLanguagecategory(ItemSubCategory cat);
        Task<int> GetLanguageMainCategory(LanguageMst maincat);

        int uploadimage(string id);
        int uploadimagecategory(string id);
    }
}
