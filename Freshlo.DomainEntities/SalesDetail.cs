using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class SalesDetail
    {
        public int Id { get; set; }
        public string SalesOrderId { get; set; }
        public string Source { get; set; }
        public string OrderdStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMode { get; set; }
        public string Coupencode { get; set; }
        public double MaxDiscount { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string SlotId { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }
        public string DeliveryCharges { get; set; }
        public double TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string Name { get; set; }
        public string CustomerNo { get; set; }
        public double PLU_Count { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalWeight { get; set; }
        public string BuildingName { get; set; }
        public string RoomNo { get; set; }
        public string Sector { get; set; }
        public string Locality { get; set; }
        public string Landmark { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string state { get; set; }
        public string Country { get; set; }
        public string DeliveryPerson { get; set; }
        public string CustomerId { get; set; }
        public string CreatedBy { get; set; }
        public string DeliveryNotes { get; set; }
        public DateTime OrderStatusDate { get; set; }
        public DateTime ConfirmedStatusDate { get; set; }
        public DateTime PackedStatusDate { get; set; }
        public string ConfirmedStatusPersonName { get; set; }
         public string PackedStatusPersonName { get; set; }
        public string DiscountPer { get; set; }
        public string DiscountApplied { get; set; }
        public int CoupenId { get; set; }
        public string Hub { get; set; }
        public string PackedBy { get; set; }
        public DateTime PackedOn { get; set; }
        public string ConfirmedBy { get; set; }
        public DateTime ConfirmedOn { get; set; }
        public DateTime DeliveredOn { get; set; }
        public double WalletAmount { get; set; }
        public string ComputedCoupen { get; set; }
        public string fullName { get; set; }
        public string PhoneNumber { get; set; }
        public string TransactionId { get; set; }
        public DateTime OrderedOn { get; set; }
        public double GST_Sum { get; set; }
        public double TaxableAmount { get; set; }
        public double CGSTValue { get; set; }
        public double SGSTValue { get; set; }

        public string tableId { get; set; }
        public string tableName { get; set; }
        public string userRole { get; set; }

        public int CustomerOnlyId { get; set; }
        public string[] SalesOrderIds { get; set; }
        public string OrderType { get; set; }
        public string AWBNumber { get; set; }
        public string AWBShipLink { get; set; }
        public int taxType { get; set; }
        public int taxBillType { get; set; }
        public float vatTax { get; set; }
        public float gstTax { get; set; }
        public string EmailId { get; set; }

        public object Select(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
