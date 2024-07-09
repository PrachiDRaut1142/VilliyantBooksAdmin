using Freshlo.DomainEntities.Coupen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface ICoupenSI
    {
        Task<List<Coupen>> GetCoupenList(string hubId);
        Task<int> CreateCoupen(Coupen info);
        string GetExistCoupenCode(string id);
        Task<bool> DeleteCoupen(int id);
        Task<Coupen> GetCoupenDetails(string id);
        Task<int> UpdateCoupen(Coupen info);
        Task<string> CheckUniqueCouponcode(string CoupenCode);
    }
}
