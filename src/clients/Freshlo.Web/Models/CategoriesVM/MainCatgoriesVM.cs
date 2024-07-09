using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.CategoriesVM
{
    public class MainCatgoriesVM : BaseViewModel
    {
        public List<MainCategory> Maincatlist { get; set; }
        public List<MainCategory> LanguageMaincatlist { get; set; }
        public List<ItemSubCategory> Languageitemcategorieslist { get; set; }

        public MainCategory GetmaincateDetails { get; set; }
        public ItemCategoreis  ItemCategoreisDetails { get; set; }
        public List<ItemCategoreis> Getitemcategorieslist { get; set; }
        public List<ItemSubCategory> GetitemSubCategoriesList  { get; set; }
        public BusinessInfo businessInfo { get; set; }
        public int hubMappedCountDetails { get; set; }
        public List<ItemMasters> getaddedlanguagelist { get; set; }
    }
}
