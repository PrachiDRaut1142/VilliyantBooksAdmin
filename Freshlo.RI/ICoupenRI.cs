using Freshlo.DomainEntities.Coupen;
using System.Collections.Generic;

namespace Freshlo.RI
{
    public interface ICoupenRI
    {
        List<Coupen> GetCoupenList(string hubId);
        int CreateCoupen(Coupen info);
        string GetExistCoupenCode(string id);
        bool DeleteCoupen(int id);
        int UpdateCoupen(Coupen info);
        Coupen GetCoupenDetails(string id);
        string CheckUniqueCouponcode(string CoupenCode);
    }
}
