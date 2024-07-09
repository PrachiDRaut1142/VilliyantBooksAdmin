using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models
{
    public class SalesVM : BaseViewModel
    {
        public List<ItemCategory> CategoryList { get; set; }
        public Customer GetCustomerDetail { get; set; }
        public List<Item> ItemList { get; set; }
        public List<ItemSizeInfo> ItemVarainceList { get; set; }
        public Item ItemDetails { get; set; }
        public List<Customer> CustomerList { get; set; }
        public List<SelectListItem> GetCustomerContactDetail { get; set; }
        public List<SalesList> GetSalesList { get; set; }
        public Sales GetSalesOrderdetail { get; set; }
        public string CashReceieved { get; set; }
        public string CashRemaining { get; set; }
        public string Saved { get; set; }
        public string Deliverycharges { get; set; }
        public string Totalamt { get; set; }
        public List<Item> GetObject { get; set; }
        public List<SalesList> GetSalesList2 { get; set; }
        public Item AddIteminCart { get; set; }
        public Sales GetSalesOrderdetail2 { get; set; }
        public string Quantity { get; set; }
        public string TotalItem { get; set; }
        public string ItemType { get; set; }
        public List<SelectListItem>SlotList { get; set; }
        public double SubTotal { get; set; }
        public string DiscountPercentage { get; set; }
        public double TotalDiscountAmount { get; set; }
        public List<SelectListItem> GetCoupenList { get; set; }
        public double ActualDiscountAmt { get; set; }
        public List<Sales> GetTodaySalesList { get; set; }
        public Sales GetTodaySalesDetail { get; set; }
        public List<PrintSalesList> GetSalesPrintList { get; set; }
        public List<PrintSalesList> GetSalesPrintListDetails { get; set; }

        public List<TaxPercentageMst> GetGSTCount { get; set; }
        public SalesDetail TotalGST { get; set; }
        public Sales SummaryDetail { get; set; }
        public double CashRecevd { get; set; }
        public double TotalSaleAmount { get; set; }

        public List<TableInfo> tableList { get; set; }

        public string LastUpdatedBy { get; set; }

        public string Orderstatus { get; set; }
        public string Id { get; set; }
   
        public string SalesOrderId { get; set; }
        public List<Item> getmainList { get; set; }

        public Task<List<SelectListItem>> getstats { get; set; }

        public TableInfo GetTableDetails { get; set; }
        public BusinessInfo getBusinessdetails { get; set; }
        public BusinessInfo gethublist { get; set; }
        public TaxationInfo getTaxationInfo { get; internal set; }
        public List<CurrencyMST> currencyInfo { get; set; }
        public Customer CustomerDetail { get; set; }

    }
}
