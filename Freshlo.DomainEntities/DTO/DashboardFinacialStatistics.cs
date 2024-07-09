using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.DTO
{
    public class DashboardFinacialStatistics
    {
        public int TotalOrderReceived { get; set; }

        public int TotalOrderReceivedTotalAmountCount { get; set; }

        public float TotalOrderReceivedAmount { get; set; }

        public int TotalPendingOrder { get; set; }

        public int TotalpendingPaymentCount { get; set; }

        public float TotalpendingPaymentAmount { get; set; }

        public int todaysTotalPendingOrder { get; set; }

        public int todaysTotalpendingPaymentCount { get; set; }

        public float todaysTotalpendingPaymentAmount { get; set; }

        public int TotalOrderDelivered { get; set; }

        public int TotalOrderDeliveredTotalAmountCount { get; set; }

        public float TotalOrderDeliveredAmount { get; set; }

        public int TotalOrderCancelled { get; set; }

        public int TotalOrderCancelledTotalAmountCount { get; set; }

        public float TotalOrderCancelledAmount { get; set; }

        public int TotalItemPurchaseSum { get; set; }

        public int TotalItemPurchaseCount { get; set; }

        public int TotalAmountPurchaseSpentCount { get; set; }

        public float TotalAmountPurchaseSpent { get; set; }

        public int TotalExpenseOnPurchaseSpentCount { get; set; }

        public float TotalExpenseOnPurchaseSpent { get; set; }

        public int TotalDailyOperationExpensesCount { get; set; }

        public float TotalDailyOperationExpensesAmount { get; set; }

        public int TotalCashSaleAmountCount { get; set; }

        public float TotalCashSaleTotalAmount { get; set; }

        public int TotalOnlineSaleAmountCount { get; set; }

        public float TotalOnlineSaleTotalAmount { get; set; }

        public int TotaWalletAmountCount { get; set; }

        public float TotalWalletTotalAmount { get; set; }

        public int TotalOverallPendingPaymentCount { get; set; }

        public float TotalOverallPendingPaymenttotalAmount { get; set; }

        public int TotalCashDiscount { get; set; }

        public int TotalDiscountAmountCount { get; set; }

        public float TotalDiscountAmount { get; set; }



    }
}
