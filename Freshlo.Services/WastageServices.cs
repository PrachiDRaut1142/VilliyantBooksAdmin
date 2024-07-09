using Freshlo.DomainEntities.Wastage;
using Freshlo.RI;
using Freshlo.SI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class WastageServices:IWastageSI
    {
        private readonly IWastageRI _wastageRI;
        public WastageServices(IWastageRI wastageRI)
        {
            _wastageRI = wastageRI;
        }
        public  async Task<List<Wastage>> GetallItemList()
        {
            return await _wastageRI.GetallItemList();
        }
        public int CreateorUpdateWastageDetail(Wastage wastagedata)
        {
            return _wastageRI.CreateorUpdateWastageDetail(wastagedata);
        }
        public int UpdateStockDetail(Wastage wastagedata)
        {
            return _wastageRI.UpdateStockDetail(wastagedata);
        }
        public int CreateWastageLog(Wastage wastagedata)
        {
            return _wastageRI.CreateWastageLog(wastagedata);
        }

    }
}
