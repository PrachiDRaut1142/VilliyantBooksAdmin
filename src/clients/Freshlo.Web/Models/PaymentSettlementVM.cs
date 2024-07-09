using Freshlo.DomainEntities;
using Freshlo.DomainEntities.PaymentSettlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models
{
    public class PaymentSettlementVM
    {
        public List<Sales> GetPaymentListToSettled { get; set; }
        public List<PaymentSettlement> GetPaymentListToSummary { get; set; }

    }
}
