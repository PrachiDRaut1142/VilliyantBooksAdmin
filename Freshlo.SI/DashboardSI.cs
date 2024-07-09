using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
   public interface DashboardSI
    {

        // Actual Code
        Task<DashboardCount> GetAllDashboardCount(string datefrom, string dateto, string hubId);


        // Later Resued Code
        Task<DashboardCount> GetAllDashboardCount(int today);
        Task<DashboardCount> GetAllDashboardCountHubwise(int today, string HubId);
        Task<DashboardCount> GetAllDashboardCountZipwise(int today, string ZipCode);


        // Taxation Code Used
        Task<List<TaxPercentageMst>> GetallGstTaxpyInfolist(string datefrom, string dateto);
        Task<TaxPercentageMst> GstDashboardCountInfo(string datefrom, string dateto);
        Task<List<TaxPercentageMst>> GetallGstTaxpyInfolist();

        Task<List<DashboardCount>> Getexportlist(string id);
    }
}
