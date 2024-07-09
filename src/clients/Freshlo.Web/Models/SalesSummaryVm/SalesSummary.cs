using Freshlo.DomainEntities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.SaleSummaryVM
{
    public class SalesSummary
    {
        public List<SaleSummary> cashSummary { get; set; }
        public List<SaleSummary> cardSummary { get; set; }
        public List<SaleSummary> upiSummary { get; set; }
        public List<SaleSummary> pendingSummary { get; set; }
        public List<SaleSummary> discountsummary { get; set; }

        public List<SaleSummary> monthcashSummary { get; set; }
        public List<SaleSummary> monthcardSummary { get; set; }
        public List<SaleSummary> monthupiSummary { get; set; }
        public List<SaleSummary> monthpendingSummary { get; set; }

    }
}

