using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.DTO
{
    public class Pur_ItemSummary
    {
        public int Id { get; set; }
        public string ItemId { get; set; }
        public string Pluname { get; set; }

        public string Measurement { get; set; }
        public double POQuantity { get; set; }
        public double POPrice { get; set; }
        public double SalesQuantity { get; set; }
        public double SalesPrice { get; set; }
        public double Stock { get; set; }
        public double ItemPrice { get; set; }
        public double Wastage_Quan { get; set; }
        public double  Weight {get;set;}
        public string Type { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ItemNames { get; set; }


    }
}
