using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.DomainEntities
{
   public class TableInfo
    {
        public int id { get; set; }
        public string tableId { get; set; }
        public string tableName { get; set; }
        public string branch { get; set; }
        public string Hub { get; set; }
        public DateTime created_on { get; set; }
        public string created_by { get; set; }
        public DateTime updated_on { get; set; }
        public string updated_by { get; set; }
        public string TableCode { get; set; }
        public string tblPerfernce { get; set; }
        public string custId { get; set; }
        public string custNumber { get; set; }
        public string custName { get; set; }
        public int totalGuest { get; set; }
        public string perferencType { get; set; }
        public string custEmail { get; set; }
        public string source { get; set; }
        public int customerId { get; set; }
        public string bookingId { get; set; }
        public string status { get; set; }
        public int WaitingStatus { get; set; }
        public int AdvanceStatus { get; set; }
        public int BookingCount { get; set; }
        public string slotTime { get; set; }
        public string timeType { get; set; }
        public string  OTP { get; set; }
    }
}
