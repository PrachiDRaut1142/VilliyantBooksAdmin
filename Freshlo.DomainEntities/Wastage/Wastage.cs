using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Wastage
{
    public  class Wastage
    {
       public int  Id {get;set;}
        public string ItemId{get;set;}
        public double TotalStock{get;set;}
        public string Measurement{get;set;}
        public double  Wastage_Quan {get;set;}
        public int WastageType{get;set;}
        public string Wastage_Description{get;set;}
        public double? WastageItemPrice { get;set;}
        public string Hub{get;set;}
        public DateTime UpdatedOn{get;set;}
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string PluName { get; set; }
        public double PurchasePrice { get; set; }
        public double stockPo { get; set; }
        public double Wastagestock { get; set; }
        public int stockId { get; set; }
        public int wastageid { get; set; }
        public double ItemwastagePrice { get; set; }
        public double TotalWastageQuan { get; set; }
        public float wastageLogPrice { get; set; }
        public float wastageLogCount { get; set; }
    }
}
