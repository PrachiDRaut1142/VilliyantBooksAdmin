using Freshlo.DomainEntities.Wastage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models
{
    public class WastageVM
    {
        public List<Wastage> GetItemList { get; set; }
    }
}
