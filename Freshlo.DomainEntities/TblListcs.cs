using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class TblListcs
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string  headervalues { get; set; }
        public int ListId { get; set; }
        public string path { get; set; }
        public List<int> HeaderValues { get; set; }
        public List<int> MultipleHeaderValue { get; set; }
        public int SuccessfulCount { get; set; }
        public int RejectedCount { get; set; }
        public string hubId { get; set; }
    }
}
