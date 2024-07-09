using Freshlo.DomainEntities.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class PritVeiwList
   {
        public List<PrintSalesList> PrintList { get; set; }

        public List<PrintDetails> PrintDetailsList { get; set; }
    }
}
