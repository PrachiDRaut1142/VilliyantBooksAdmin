using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
   public interface SaleSummarySI
    {

        Task<List<SaleSummary>> GetCashSummary(string datefrom, string dateto,string id);
        Task<List<SaleSummary>> GetCardSummary(string datefrom, string dateto, string id);
        Task<List<SaleSummary>> GetUpiSummary(string datefrom, string dateto, string id);
        Task<List<SaleSummary>> GetPendingSummary(string datefrom, string dateto,string id);
        Task<List<SaleSummary>> GetDiscountSummary(string datefrom, string dateto,string id);

        Task<List<SaleSummary>> GetmonthCashSummary(string datefrom, string dateto,string id);
        Task<List<SaleSummary>> GetmonthCardSummary(string datefrom, string dateto,string id);
        Task<List<SaleSummary>> GetmonthUpiSummary(string datefrom, string dateto, string id);
        Task<List<SaleSummary>> GetmonthPendingSummary(string datefrom, string dateto, string id);

    }
}
