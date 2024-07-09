using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.Financial
{
    public class ManageViewModel
    {
        public List<Finance> FinanceList { get; set; }
        public Task<SummayData> FinanceSummary { get; set; }
    }
}
