using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class CustomerSalesHistory
    {
        //public int Id { get; set; }
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string DecodeId1 { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Contact { get; set; }
        public string wallet { get; set; }
        public string PendingPaymentAmount { get; set; }
        public string OpenOrders { get; set; }
        public string ClosedOrders { get; set; }
        public string CancelledOrderCount { get; set; }
        public string CancelledOrderAmount { get; set; }
        public string PendingPaymentCount { get; set; }
        public string ClosedOrderAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal WalletUsed { get; set; } 
        public decimal RewardPoint { get; set; }
        public decimal RedeemedPoint { get; set; }
        public decimal InWallet { get; set; }
        public int DaysLeft { get; set; }
        public string EmailId { get; set; }
        public string Status { get; set; }
        public DateTime LastLogin { get; set; }
        public string CreatedBy { get; set; }
        public string Source { get; set; }


    }
}
