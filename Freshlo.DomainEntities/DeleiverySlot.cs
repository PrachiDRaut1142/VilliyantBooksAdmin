using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
   public class DeleiverySlot
    {
        public int Id { get; set; }
        public string SlotId { get; set; }
        public string Timing { get; set; }
        public int SlotType { get; set; }
        public string SlotTypeDesc { get; set; }
    }
}
