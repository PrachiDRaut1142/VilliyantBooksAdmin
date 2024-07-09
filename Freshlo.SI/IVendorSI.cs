using Freshlo.DomainEntities.Vendor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
   public interface IVendorSI
    {
        Task<List<Vendor>> GetVendorList(string hubId);
        Task<int> AddVendor(Vendor info);
        Task<List<SelectListItem>> GetMainCategoryList();
        Task<int> DeleteVendor(int Id);
        Vendor GetVendorDetails(int id);
        Task<List<int>> GetCategorieslist(int Id);
        Task<int> EditVendor(Vendor info);
        Task<List<Vendor>> GetVendorListByName();

    }
}
