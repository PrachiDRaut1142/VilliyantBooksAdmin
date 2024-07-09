using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class DashboardService : DashboardSI
    {
        private DashboardRI _DashboardRI { get; }
        public DashboardService(DashboardRI DashboardRI)
        {
            _DashboardRI = DashboardRI;
        }


        // Actual Code Used
        public Task<DashboardCount> GetAllDashboardCount(string datefrom, string dateto, string hubId)
        {
            return Task.Run(() =>
            {
                return _DashboardRI.GetAllDashboardCount(datefrom, dateto,hubId);
            });
        }


    
        
        // Later Resued Code
        Task<DashboardCount> DashboardSI.GetAllDashboardCount(int today)
        {
            return Task.Run(() =>
            {
                return _DashboardRI.GetAllDashboardCount(today);
            });
        }
        public Task<DashboardCount> GetAllDashboardCountHubwise(int today, string HubId)
        {
            return Task.Run(() =>
            {
                return _DashboardRI.GetAllDashboardCountHubwise(today, HubId);
            });
        }
        public Task<DashboardCount> GetAllDashboardCountZipwise(int today, string ZipCode)
        {
            return Task.Run(() =>
            {
                return _DashboardRI.GetAllDashboardCountZipwise(today, ZipCode);
            });
        }




        // Taxation Code Used
        public Task<List<TaxPercentageMst>> GetallGstTaxpyInfolist(string datefrom, string dateto)
        {
            return Task.Run(() =>
            {
                return _DashboardRI.GetallGstTaxpyInfolist(datefrom, dateto);
            });
        }
        public Task<TaxPercentageMst> GstDashboardCountInfo(string datefrom, string dateto)
        {
            return Task.Run(() =>
            {
                return _DashboardRI.GetGstCountDetailInfo(datefrom, dateto);
            }); ;
        }
        public Task<List<TaxPercentageMst>> GetallGstTaxpyInfolist()
        {
            return Task.Run(() =>
            {
                return _DashboardRI.GetallGstTaxpyInfolist();
            });
        }

        public Task<List<DashboardCount>> Getexportlist(string id)
        {
            return Task.Run(() =>
            {
                return _DashboardRI.Getexportlist(id);
            });
        }

    }
}