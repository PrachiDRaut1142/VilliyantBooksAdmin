using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Offer;
using Freshlo.DomainEntities.PriceList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface IOfferlist
    {
        // offer list, create, detail ,update,delete 
        Task<List<Offer>> GetOfferlist(string hubId);
        Task<string> AddOffer(Offer info);
        Task<Offer> GetOfferDetail(string Id);
        Task<int> EditOffer(Offer info);
        Task<bool> DeleteOffer(int id);
        // mapping function offer 
        Task<List<PriceList>> GetallPricelist(PricelistFilter detail);
        Task<List<PriceList>> Getallitemlist(string maincategory, string Category, int type, string hubId);
        Task<List<PriceList>> GetMappedPricelist(PricelistFilter detail, string id);
        Task<int> AddItem(PriceList info, string offerid, string startdate);
        Task<int> AddItemList(PriceList info, string offerid);
        Task<int>AddItemList(Offer list);
        Task<int> DeleteOfferItem(int Id);
        Task<bool> DeleteMappingItem(string ids);
        Task<List<SelectListItem>> GetOfferTypeList();
    }
}
