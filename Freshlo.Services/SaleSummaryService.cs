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
    public class SaleSummaryService : SaleSummarySI
    {
        private SaleSummaryRI _saleSummaryRI { get; }
        public SaleSummaryService(SaleSummaryRI SaleSummaryRI)
        {
            _saleSummaryRI = SaleSummaryRI;
        }

        public Task<List<SaleSummary>> GetCashSummary(string datefrom, string dateto, string id)
        {
            return Task.Run(() =>
            {
                return _saleSummaryRI.GetCashSummary(datefrom, dateto,id);
            });
        }

        public Task<List<SaleSummary>> GetCardSummary(string datefrom, string dateto, string id)
        {
            return Task.Run(() =>
            {
                return _saleSummaryRI.GetCardSummary(datefrom, dateto,id);
            });
        }

        public Task<List<SaleSummary>> GetUpiSummary(string datefrom, string dateto, string id)
        {
            return Task.Run(() =>
            {
                return _saleSummaryRI.GetUpiSummary(datefrom, dateto, id);
            });
        }

        public Task<List<SaleSummary>> GetPendingSummary(string datefrom, string dateto,string id)
        {
            return Task.Run(() =>
            {
                return _saleSummaryRI.GetPendingSummary(datefrom, dateto,id);
            });
        }

        public Task<List<SaleSummary>> GetDiscountSummary(string datefrom, string dateto,string id)
        {
            return Task.Run(() =>
            {
                return _saleSummaryRI.GetDiscountSummary(datefrom, dateto ,id);
            });
        }

        public Task<List<SaleSummary>> GetmonthCashSummary(string datefrom, string dateto, string id)
        {
            return Task.Run(() =>
            {
                return _saleSummaryRI.GetmonthCashSummary(datefrom, dateto,id);
            });
        }

        public Task<List<SaleSummary>> GetmonthCardSummary(string datefrom, string dateto, string id)
        {
            return Task.Run(() =>
            {
                return _saleSummaryRI.GetmonthCardSummary(datefrom, dateto,id);
            });
        }

        public Task<List<SaleSummary>> GetmonthUpiSummary(string datefrom, string dateto, string id)
        {
            return Task.Run(() =>
            {
                return _saleSummaryRI.GetmonthUpiSummary(datefrom, dateto,id);
            });
        }

        public Task<List<SaleSummary>> GetmonthPendingSummary(string datefrom, string dateto, string id)
        {
            return Task.Run(() =>
            {
                return _saleSummaryRI.GetmonthPendingSummary(datefrom, dateto,id);
            });
        }
    }
}