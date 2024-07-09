using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class TaxPercentageMst
    {
        public int Id { get; set; }
        public double IGST_Per { get; set; }
        public double VAT_Per { get; set; }
        public double SGST_Per { get; set; }
        public double CGST_Per { get; set; }
        public double TotalAmount { get; set; }
        public double TaxableAmount { get; set; }
        public double CGSTValue { get; set; }
        public double SGSTValue { get; set; }



        public string SalesOrderId { get; set; }
        public string ItemId { get; set; }
        public string PluName { get; set; }
        public float TotalPrice { get; set; }
        public float SgstPer { get; set; }
        public float SgstTaxvalue { get; set; }
        public float SgstTaxValueInclue { get; set; }
        public float Cgstper { get; set; }
        public float CgstTaxvalue { get; set; }
        public float CgstTaxValueInclue { get; set; }
        public float TotalgstPer { get; set; }
        public float TotalGstTaxValue { get; set; }
        public float TotalAmtIncludeGst { get; set; }
        public DateTime TaxDate { get; set; }
        public DateTime CreatedOn { get; set; }


        public float TotalCgstAmount { get; set; }
        public float TotalSgstAmount { get; set; }
        public float TotalIgstAmount { get; set; }

        public float TotaltaxAmount { get; set; }
        public int ItemCount { get; set; }

    }
}
