using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class DashboardCount
    {
        public int TotalOrderReceived { get; set; }

        public int TotalOrderReceivedTotalAmountCount { get; set; }

        public double TotalOrderReceivedAmount { get; set; }
        //public string TotalOrderReceivedAmount { get; set; }

        public int TotalPendingOrder { get; set; }

        public int TotalpendingPaymentCount { get; set; }

        public double TotalpendingPaymentAmount { get; set; }

        public int todaysTotalPendingOrder { get; set; }

        public int todaysTotalpendingPaymentCount { get; set; }

        public float todaysTotalpendingPaymentAmount { get; set; }

        public int TotalOrderDelivered { get; set; }

        public int TotalOrderDeliveredTotalAmountCount { get; set; }

        public double TotalOrderDeliveredAmount { get; set; }

        public int TotalOrderCancelled { get; set; }

        public int TotalOrderCancelledTotalAmountCount { get; set; }

        public double TotalOrderCancelledAmount { get; set; }

        public int Todaypaymentcount { get; set; }

        public int Todaypaymentamount { get; set; }

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


        public int TotalCardSaleAmountCount { get; set; }
        
        public double TotalCardSaleTotalAmount { get; set; }

        public int TotalUPISaleAmountCount { get; set; }

        public float TotalUPISaleTotalAmount { get; set; }

        public int TotalGpaySaleAmountCount { get; set; }

        public float TotalGpaySaleTotalAmount { get; set; }

        public int TotalPhonePaySaleAmountCount { get; set; }

        public float TotalPhonePaySaleTotalAmount { get; set; }

        public int TotalPaytmSaleAmountCount { get; set; }

        public float TotalPaytmSaleTotalAmount { get; set; }


        public int TotalOnlineSaleAmountCount { get; set; }

        public double TotalOnlineSaleTotalAmount { get; set; }

        public int TotalWalletSaleAmountCount { get; set; }

        public float TotalWalletSaleTotalAmount { get; set; }

        public int TotalOverallPendingPaymentCount { get; set; }

        public float TotalOverallPendingPaymenttotalAmount { get; set; }



        public int TotalCashDiscount { get; set; }

        public int TotalDiscountAmountCount { get; set; }
        public int TOTAlGpayphoneCount { get; set; }

        public double TotalDiscountAmount { get; set; }
        public float TOTAlGpayphoneAmount { get; set; }

        public double totalgstitemamount { get; set; }
        public int totalgstitemcount { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }

    }
}
