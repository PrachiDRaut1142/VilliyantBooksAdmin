using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoDecodeURLParameters.Security
{
   
    public class DomainModel
    {
        public int BankId { get; set; }

        [NotMapped]
        public string DecodeId { get; set; }
    }
}
