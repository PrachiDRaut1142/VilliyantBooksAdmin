using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.RI
{
   public interface DashboardRI
    {
        // Actual Code Used    
        DashboardCount GetAllDashboardCount(string datefrom, string dateto,string hubId);

        
        
        // Later Resued Code
        DashboardCount GetAllDashboardCount(int today);
        DashboardCount GetAllDashboardCountHubwise(int today, string HubId);
        DashboardCount GetAllDashboardCountZipwise(int today, string ZipCode);



        // Taxation Code Used
        List<TaxPercentageMst> GetallGstTaxpyInfolist(string datefrom, string dateto);
        TaxPercentageMst GetGstCountDetailInfo(string datefrom, string dateto);
        List<TaxPercentageMst> GetallGstTaxpyInfolist();

        List<DashboardCount> Getexportlist(string id);
    }
}
