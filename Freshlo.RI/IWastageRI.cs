using Freshlo.DomainEntities.Wastage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.RI
{
   public interface IWastageRI
    {
        Task<List<Wastage>> GetallItemList();
        int CreateorUpdateWastageDetail(Wastage wastagedata);
        int UpdateStockDetail(Wastage wastagedata);
        int CreateWastageLog(Wastage wastagedata);
    }
}
