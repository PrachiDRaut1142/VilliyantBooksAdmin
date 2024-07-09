using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.DTO
{
    public class SaleOrderss
    {
        public int Id { get; set; }
        public string SalesOrderId { get; set; }
        public string CustomerId { get; set; }
        public string Booking_Id { get; set; }
        public string SalesPerson { get; set; }
        public float PLU_Count { get; set; }
        public float TotalQuantity { get; set; }
        public float TotalWeight { get; set; }
        public float TotalPrice { get; set; }
        public float Discount { get; set; }
        public string OrderdStatus { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime DeliveryDate { get; set; }
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
        public string TransactionId { get; set; }
        public string DeliveryNotes { get; set; }
        public string Savings { get; set; }
        public string Source { get; set; }
        public string otp { get; set; }
        public float Remaining_Amount { get; set; }
        public string tableId { get; set; }
        public int KotPrint { get; set; }
        public int TokenNumber { get; set; }
        public string OrderType { get; set; }
        public int taxType { get; set; }
        public int taxBillType { get; set; }
        public float vatTax { get; set; }
        public float gstTax { get; set; }

    }
}
