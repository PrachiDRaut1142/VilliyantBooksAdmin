using Freshlo.DomainEntities.Coupen;
using Freshlo.RI;
using Freshlo.SI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class CoupenService : ICoupenSI
    {
        private ICoupenRI _coupenRI;
        public CoupenService(ICoupenRI coupenRI)
        {
            _coupenRI = coupenRI;
        }
        public Task<int> CreateCoupen(Coupen info)
        {
            return Task.Run(() =>
            {
                return _coupenRI.CreateCoupen(info);
            });
        }
        public Task<bool> DeleteCoupen(int id)
        {
            return Task.Run(() =>
            {
                return _coupenRI.DeleteCoupen(id);
            });
        }
        public Task<Coupen> GetCoupenDetails(string id)
        {
            return Task.Run(() =>
            {
                return _coupenRI.GetCoupenDetails(id);
            });
        }
        public Task<List<Coupen>> GetCoupenList(string hubId)
        {
            return Task.Run(() =>
            {
                return _coupenRI.GetCoupenList(hubId);
            });
        }
        public string GetExistCoupenCode(string id)
        {
            return _coupenRI.GetExistCoupenCode(id);
        }
        public Task<int> UpdateCoupen(Coupen info)
        {
            return Task.Run(() =>
            {
                return _coupenRI.UpdateCoupen(info);
            });
        }
        public Task<string> CheckUniqueCouponcode(string CoupenCode)
        {
            return Task.Run(() =>
            {
                return _coupenRI.CheckUniqueCouponcode(CoupenCode);
            });
        }
    }
}
