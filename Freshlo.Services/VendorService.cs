using Freshlo.DomainEntities.Vendor;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class VendorService : IVendorSI
    {

        public VendorService(IVendorRI vendorRI)
        {
            _vendorRI = vendorRI;
        }

        private IVendorRI _vendorRI;

        public Task<List<Vendor>> GetVendorList(string hubId)
        {
            return Task.Run(() =>
            {
                return _vendorRI.GetVendorList(hubId);
            });
        }

        public Task<int> AddVendor(Vendor info)
        {
            return Task.Run(() =>
            {
                return _vendorRI.AddVendor(info);
            });
        }

        public Task<List<SelectListItem>> GetMainCategoryList()
        {
            return Task.Run(() =>
            {
                return _vendorRI.GetMainCategoryList();
            });
        }

        public Task<int> DeleteVendor(int Id)
        {
            return Task.Run(() =>
            {
                return _vendorRI.DeleteVendor(Id);
            });
        }

        public Vendor GetVendorDetails(int id)
        {
            return _vendorRI.GetVendorDetails(id);
        }

        public Task<List<int>> GetCategorieslist(int Id)
        {
            return Task.Run(() =>
            {
                return _vendorRI.GetCategroiesList(Id);
            });
        }

        public Task<int> EditVendor(Vendor info)
        {
            return Task.Run(() =>
            {
                return _vendorRI.EditVendor(info);
            });
        }

        public Task<List<Vendor>> GetVendorListByName()
        {
            return Task.Run(() =>
            {
                return _vendorRI.GetVendorListByName();
            });
        }
    }
}
