using System;
using System.Collections.Generic;

namespace Freshlo.DomainEntities
{
    public class Sales
    {
        public int Id { get; set; }
        public string SalesOrderId { get; set; }
        public string DecodeId { get; set; }
        public string DecodeId1 { get; set; }
        public string CustomerId { get; set; }
        public string SalesPerson { get; set; }
        public double PLU_Count { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalWeight { get; set; }
        public double TotalPrice { get; set; }
        public double Wallet { get; set; }
        public double Discount { get; set; }
        public double Taxable_Amount { get; set; }
        public string OrderdStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string DeliveryDate { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Bags { get; set; }
        public string PaymentMode { get; set; }
        public string AddressId { get; set; }
        public string SlotId { get; set; }
        public string DeliveryType { get; set; }
        public string DeliveryCharges { get; set; }
        public string HubId { get; set; }
        
        public string Branch { get; set; }
        public string PurchaseId { get; set; }
        public string[] MultipleItem { get; set; }
        public string[] MultipleItem1 { get; set; }

        public string Name { get; set; }
        public string currencytype { get; set; }
        public string  ContactNo { get; set; }
        public string phonenumber { get; set; }
        public int PluCount { get; set; }
        public double TotalAmount { get; set; }
        public double CashReceived { get; set; }
        public double CashRemaining { get; set; }
        public string EmpName { get; set; }
        public string Source { get; set; }
        public string ModeFullForm { get; set; }


        public string[] Multiplestockrecord { get; set; }

        public string ItemName { get; set; }
        public double TotaldisAmt { get; set; }
        public string ActualDiscAmt { get; set; }
        public int coupenId { get; set; }
        public double? TotalAmt { get; set; }
        public int? TotalCount { get; set; }
        public double? AmountCollected { get; set; }
        public double? AmountCancelled { get; set; }
        public DateTime Deliverydt { get; set; }
        public string CreatedName { get; set; }
        public string UpdatedPersonName { get; set; }
        public string TodayDate { get; set; }
        public string StartDate { get; set; }
        public float Remaining_Amount { get; set; }


        public string EndDate { get; set; }
        public double DeliveryCharges1 { get; set; }
        public string CreatedOn1 { get; set; }
        public string Coupencode { get; set; }

        public string CustomerName { get; set; }
        public string CustomerNo { get; set; }
        public string DeliveryNotes { get; set; }
        public double CoupenCalculation { get; set; }
        public double TotalSaleAmount { get; set; }
        public string DeliveredPerson { get; set; }
        public string PaymentSettled_for { get; set; }
        public string PaymentSettled_By { get; set; }
        public DateTime PaymentDeliveredDate { get; set; }
        public string Settlement_Status { get; set; }
        public string Description { get; set; }
        public string ZipCode { get; set; }
        public string BuildingName { get; set; }
        public string RoomNo { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
        public string Sector { get; set; }
        public float DiscountedAmount { get; set; }
        public string TransactionId { get; set; }
        public DateTime DeliveryDate1 { get; set; }
        public string CustId { get; set; }
        public string tableId { get; set; }
        public string tableName { get; set; }
        public string status { get; set; }
        public string ItemId { get; set; }
        public string PluName { get; set; }

        public string approval { get; set; }
        public string Remark { get; set; }


        public List<string> salesId { get; set; }
        public List<string> salesListId { get; set; }
        public List<string> salesStatus { get; set; }

        public double? CashPaymentCollect { get; set; }
        public double? CardPaymentCollect { get; set; }

        public double? UPIPaymentCollect { get; set; }

        public int CustomerOnlyId { get; set; }

        public int KOT_Status { get; set; }
        public List<string> kitChenListId { get; set; }
        public string OrderType { get; set; }
        public int taxType { get; set; }
        public int taxbillType { get; set; }
        public int calculateTaxType { get; set; }
        public float vatTax { get; set; }
        public float gstTax { get; set; }
        public string bookingIds { get; set; }
        public string Symbol { get; set; }
        public string fullname { get; set; }
        public string AWBnumber { get; set; }
        public string AWBshippedlink { get; set; }
        public double TaxableAmount { get; set; }

    }
}
