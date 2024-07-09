using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.Sale
{
    public class SalesDetailVM : BaseViewModel
    {
       public List<SelectListItem> OrderStatus { get; set; }
        public SalesDetail SaleData { get; set; }
        public SalesDetail SaleProductStatus { get; set; }
        public List<Sales> GetSalesList { get; set; }

        public List<SalesList> productSalesList { get; set; }
        public List<SalesList> OrderTracking { get; set; }
        public List<SelectListItem> EmployeeName { get; set; }
        public List<SelectListItem> GetCoupenList { get; set; }
        public int salesListcount { get; set; }
        public SalesDetail GSTDetail { get; set; }
        public List<Item> ItemList { get; set; }
        public List<Item> getmainList { get; set; }
        public List<SalesList> GetTableStateList { get; set; }

        public TableInfo TableDetailst { get; set; }
        public List<SelectListItem> tableList { get; set; }
        public List<SalesList> kotSaleList { get; set; }
        public List<SalesList> kotTblSaleList { get; set; }

        public List<PendingData> pendingIteMlIST { get; set; }
        public List<SalesList> itemSalesList { get; set; }
        public List<SalesList> AllitemSalesList { get; set; }
        public TaxationInfo taxdetails { get; set; }
        public List<CurrencyMST> Currencyinfo { get; set; }
        public List<KotLogs> kotLOGsList { get; set; }


    }
}
