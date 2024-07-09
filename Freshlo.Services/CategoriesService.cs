using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Category;
using Freshlo.RI;
using Freshlo.SI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class CategoriesService : ICategoriesSI
    {
        public ICategoriesRI _categoriesRi { get; set; }
        public CategoriesService(ICategoriesRI categoriesRI)
        {
            _categoriesRi = categoriesRI;
        }

        // MainCateogry Related Here...
        public Task<List<MainCategory>> GetMainCategoriesList(string id)
        {
            return Task.Run(() =>
            {

                return _categoriesRi.GetMainCategoriesList(id);
            });
        }
        public string GetExistmainCategoryCode()
        {
            return _categoriesRi.GetExistmainCategoryCode();
        }
        public Task<string> InsertMainCategory(string maincategoryName, string userId, string maincategoryCode)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.InsertMainCategory(maincategoryName, userId, maincategoryCode);
            });
        }
        public Task<string> AddMainCategory(MainCategory info)
        {
            return Task.Run(() =>
            {

                return _categoriesRi.AddMainCategory(info);
            });
        }
        public Task<MainCategory> GetMainCategoryDetails(string id, string hubId)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.GetMainCategoryDetails(id,hubId);
            });
        }

        public Task<int> GetMainCategoryDetailsCount(string id, string hubId)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.GetMainCategoryDetailsCount(id, hubId);
            });
        }

        public Task<string> UpdateMainCategory(MainCategory info)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.UpdateMainCategory(info);
            });
        }
        public bool DeleteMaincategory(string id)
        {

            return _categoriesRi.DeleteMaincategory(id);
        }

        //Category Related Here...
        public string GetExistCategoryCode()
        {
            return _categoriesRi.GetExistCategoryCode();
        }
        public Task<List<ItemCategoreis>> GetItemCategoriesList(string id, string hubId)
        {
            return Task.Run(() =>
            {

                return _categoriesRi.GetItemCategoriesList(id, hubId);
            });
        }
        public Task<bool> DeleteReleatedCategories(int id)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.DeleteReleatedCategories(id);
            });
        }
        public Task<string> AddItemCategories(ItemCategoreis info)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.AddItemCategories(info);
            });
        }
        public Task<ItemCategoreis> GetCategoryDetails(string id, string hubId)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.GetCategoryDetails(id, hubId);
            });
        }
        public string GetExistCategoryId(string id,string hubId)
        {
            return _categoriesRi.GetExistCategoryId(id,hubId);
        }
        public bool DeleteFrmCatdata(string id)
        {

            return _categoriesRi.DeleteFrmCatdata(id);
        }
        public Task<string> UpdateItemCategory(ItemCategoreis info)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.UpdateItemCategory(info);
            });
        }

        // SubCateogries Related Here...
        public Task<bool> CheckSubCategories(ItemSubCategory info)
        {
            return Task.Run(() =>
            {

                return _categoriesRi.CheckSubCategories(info);
            });
        }
        public Task<string> AddSubItemCategories(ItemSubCategory info)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.AddSubCategoies(info);
            });
        }
        public Task<List<ItemSubCategory>> GetItemSubCategoriesList(string id,string hubId)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.GetItemSubCategoriesList(id,hubId);
            });
        }
        public Task<bool> DeleteSubcategories(string id)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.DeleteSubcategory(id);
            });
        }

        public Task<int> AddeIntoHub(string mainCatId, string hubId, string createdBy)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.AddeIntoHub(mainCatId, hubId, createdBy);
            });
        }

        Task<bool> ICategoriesSI.RemoveIntoHub(string mainCatId, string hubId, string createdBy)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.RemoveIntoHub(mainCatId, hubId, createdBy);
            });
        }
        public Task<int> GetLanguagecategory(ItemSubCategory cat)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.GetLanguagecategory(cat);
            });
        }

        public Task<int> GetLanguageMainCategory(LanguageMst maincat)
        {
            return Task.Run(() =>
            {
                return _categoriesRi.GetLanguageMainCategory(maincat);
            });
        }

        public Task<List<MainCategory>> GetLanguageMainCategoriesList(string id, string hubId)
        {
            return Task.Run(() =>
            {

                return _categoriesRi.GetLanguageMainCategoriesList(id, hubId);
            });
        }

        public Task<List<ItemSubCategory>> GetLanguageSubCategoriesList(string id, string hubId)
        {
            return Task.Run(() =>
            {

                return _categoriesRi.GetLanguageSubCategoriesList(id, hubId);
            });
        }

        public int uploadimage(string id)
        {
            return _categoriesRi.uploadimage(id);
        }

        public int uploadimagecategory(string id)
        {
            return _categoriesRi.uploadimagecategory(id);
        }
    }
}
