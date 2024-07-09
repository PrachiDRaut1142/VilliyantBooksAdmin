using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.RI
{
   public interface SaleSummaryRI
    {

       List<SaleSummary> GetCashSummary(string datefrom, string dateto, string id);
       List<SaleSummary> GetCardSummary(string datefrom, string dateto, string id);
       List<SaleSummary> GetUpiSummary(string datefrom, string dateto, string id);
       List<SaleSummary> GetPendingSummary(string datefrom, string dateto,string id);
       List<SaleSummary> GetDiscountSummary(string datefrom, string dateto,string id);

        List<SaleSummary> GetmonthCashSummary(string datefrom, string dateto, string id);
        List<SaleSummary> GetmonthCardSummary(string datefrom, string dateto, string id);
        List<SaleSummary> GetmonthUpiSummary(string datefrom, string dateto, string id);
        List<SaleSummary> GetmonthPendingSummary(string datefrom, string dateto, string id);


    }
}
