using Freshlo.DomainEntities.Banner;
using Freshlo.DomainEntities.Coupen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Models.CoupenVM
{
    public class CoupenVM : BaseViewModel
    {
        public List<Coupen> GetCoupenList { get; set; }

        public int CreateCoupen { get; set; }
        public Coupen GetCoupen { get; set; }
    }
}
