using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class Wallet
    {
        public int id {get;set;}
        public string WalId {get;set;}
        public string CustomerId {get;set;}
        public string Description {get;set;}
        public string Status {get;set;}
        public float Amount {get;set;}
        public string Type {get;set;}
        public DateTime CreatedOn {get;set;}
        public string CreatedBy {get;set;}
        public DateTime ExpiryDate { get; set; }
    }
}
