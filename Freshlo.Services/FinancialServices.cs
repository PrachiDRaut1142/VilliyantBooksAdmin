using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.RI;
using Freshlo.SI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class FinancialServices : IFinancialSI
    {
        private readonly IFinancialRI _financialRI;
        public FinancialServices(IFinancialRI financialRI)
        {
            _financialRI = financialRI;
        }
        public Task<int> CreateFinance(Finance info)
        {
            return Task.Run(() =>
            {
                return _financialRI.CreateFinance(info);
            });
        }

        public Task<Finance> GetFinanceDetail(int id, int opt)
        {
            return Task.Run(() =>
            {
                return _financialRI.GetFinanceDetail(id, opt);
            });
        }

        public Task<List<Finance>> GetManage(string paid_From, string paid_Till)
        {
            return Task.Run(() =>
            {
                return _financialRI.GetManage(paid_From, paid_Till);
            });
        }
        public async Task<SummayData> GetSummaryData()
        {
            return await _financialRI.GetSummaryData();
        }
        public async Task<byte[]> ExportExcelofFinance(string webRootPath, string paid_From, string paid_Till)
        {
            return await _financialRI.ExportExcelofFinance( webRootPath,  paid_From,  paid_Till);
        }

        public Task<int> UpdateFinancialDeail(Finance info)
        {
            return Task.Run(() =>
            {
                return _financialRI.UpdateFinancialDetail(info);
            });
        }
    }
}
