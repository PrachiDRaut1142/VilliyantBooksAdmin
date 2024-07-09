using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class Finance
    {
    public int Id {get; set;}
    public int Entry_Type {get; set;}
    public string Entry_Type_Desc {get; set;}
    public int Payment_Type { get; set; }
    public int Inward_Payment_Type {get; set;}
    public string Inward_PaymentTypeDesc { get; set; }
    public int Outward_Payment_Type {get; set;}
    public string Outward_PaymentTypeDesc { get; set; }
    public string Other_Payment {get; set;}
    public int Payment_Status {get; set;}
    public string Payment_Status_Desc {get; set;}
    public string Reference_No {get; set;}
    public string Received_From {get; set;}
    public string Paid_To {get; set;}
    public int Payment_Mode {get; set;}
    public string Payment_Mode_Desc {get; set;}
    public DateTime Paid_On {get; set;}
    public string Remark {get; set;}
    public DateTime Created_On {get; set;}
    public int Created_By {get; set;}
    public DateTime Updated_On {get; set;}
    public int Updated_By { get; set; }
    public float Total_Amount { get; set; }
    public float Partail_Amount { get; set; }
    public DateTime Transaction_On { get; set; }
    public string FullName { get; set; }
    }
}
