using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.PaymentSettlement
{
    public class PaymentSettlement
    {
        public int Id { get; set; }
        public string PSId { get; set; }
        public string SalesOrderId { get; set; }
        public string Settlement_Status { get; set; }
        public string Description { get; set; }
        public string Payment_settledof { get; set; }
        public string Createdby { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string PaymentMode { get; set; }
        //summary Detail
        public string PaymentSettled_Name { get; set; }
        public string PaymentSettled_for { get; set; }
        public string Payment_settledPrice { get; set; }
        public int PendingPaymentCount { get; set; }
        public string[] Checkboxids { get; set; }
        public string[] Checkboxid { get; set; }
        public string[] multiDescription { get; set; }
    }
}
