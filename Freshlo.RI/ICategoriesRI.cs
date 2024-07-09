using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.RI
{
    public interface ICategoriesRI
    {

        // MainCategory Related Here
        List<MainCategory> GetMainCategoriesList(string id);
        string GetExistmainCategoryCode();
        string InsertMainCategory(string maincategoryName, string userId, string maincategoryCode);
        string AddMainCategory(MainCategory info);
        MainCategory GetMainCategoryDetails(string id,string hubId);
        int GetMainCategoryDetailsCount(string id, string hubId);
        string UpdateMainCategory(MainCategory info);
        bool DeleteMaincategory(string id);



        // Category Related Here...
        string GetExistCategoryCode();
        List<ItemCategoreis> GetItemCategoriesList(string id, string hubId);
        bool DeleteReleatedCategories(int id);
        string AddItemCategories(ItemCategoreis info);
        string UpdateItemCategory(ItemCategoreis info);
        ItemCategoreis GetCategoryDetails(string id, string hubId);
        string GetExistCategoryId(string id,string hubId);
        bool DeleteFrmCatdata(string id);


        // SubCategory Related Here....
        bool CheckSubCategories(ItemSubCategory info);
        string AddSubCategoies(ItemSubCategory info);
        List<ItemSubCategory> GetItemSubCategoriesList(string id,string hubId);
        bool DeleteSubcategory(string id);
        int AddeIntoHub(string mainCatId, string hubId, string createdBy);
        bool RemoveIntoHub(string mainCatId, string hubId, string createdBy);
        List<MainCategory> GetLanguageMainCategoriesList(string id, string hubId);
        List<ItemSubCategory> GetLanguageSubCategoriesList(string id, string hubId);
        int GetLanguagecategory(ItemSubCategory cat);
        int GetLanguageMainCategory(LanguageMst maincat);
        int uploadimage (string id);
        int uploadimagecategory(string id);
    }
}
