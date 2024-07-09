using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.DTO
{
    public class PendingData
    {
        public int Id { get; set; }
        public string SalesOrder { get; set; }
        public string DecodeId { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string fullname { get; set; }
        public string BuildingName {get;set;}
        public string RoomNo { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string TotalPrice { get; set; }
        public string Taxable_Amount { get; set; }
        public string OrderStatus { get; set; }
        public string Sector { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string PaymentStatus { get; set; }
        public string ContactNo { get; set; }
        public string phonenumber { get; set; }
        public  string Branch { get; set; }
        public string Role { get; set; }
        public string activeTab { get; set; }
        public double Wallet { get; set; }
        public double Discount { get; set; }
        public string DeliveryCharges { get; set; }
        public string CustId { get; set; }
        public string tableId { get; set; }
        public string tableName { get; set; }

        public string PaymentMode { get; set; }
        public string OrderType { get; set; }
        public string hubId { get; set; }
        public double SGSTValue { get; set; }
        public double TaxableAmount { get; set; }
        public int taxType { get; set; }
        public int taxbillType { get; set; }
        public int calculateTaxType { get; set; }
        public float vatTax { get; set; }
        public float gstTax { get; set; }
        public string  currencttype { get; set; }
        public List<CurrencyMST> getCurrencysybmollist { get; set; }
        public List<Sales> GetSalesList1 { get; set; }
    }
}
