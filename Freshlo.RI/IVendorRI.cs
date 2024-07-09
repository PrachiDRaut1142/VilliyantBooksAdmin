using Freshlo.DomainEntities.Vendor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.RI
{
   public interface IVendorRI
    {
        List<Vendor> GetVendorList(string hubId);
        int AddVendor(Vendor info);
        List<SelectListItem> GetMainCategoryList();
        int DeleteVendor(int Id);
        Vendor GetVendorDetails(int id);
        List<int> GetCategroiesList(int Id);
        int EditVendor(Vendor info);
        List<Vendor> GetVendorListByName();


    }
}
