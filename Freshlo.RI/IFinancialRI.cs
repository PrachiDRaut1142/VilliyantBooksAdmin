using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.RI
{
   public interface IFinancialRI
    {
        int CreateFinance(Finance info);
        List<Finance> GetManage(string paid_From, string paid_Till);
        Task<SummayData> GetSummaryData();
        Finance GetFinanceDetail(int id, int opt);
        Task<byte[]> ExportExcelofFinance(string webRootPath, string paid_From, string paid_Till);
        int UpdateFinancialDetail(Finance info);
     


    }
}
