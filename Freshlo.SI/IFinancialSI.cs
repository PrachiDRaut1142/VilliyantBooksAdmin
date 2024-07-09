using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
   public interface IFinancialSI
    {
        Task<int> CreateFinance(Finance info);
        Task<List<Finance>> GetManage(string paid_From, string paid_Till);
        Task<SummayData> GetSummaryData();
        Task<byte[]> ExportExcelofFinance(string webRootPath, string paid_From, string paid_Till);

        Task<Finance> GetFinanceDetail(int id, int opt);
        Task<int> UpdateFinancialDeail(Finance info);

    }
}
