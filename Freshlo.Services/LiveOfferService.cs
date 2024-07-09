using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Offer;
using Freshlo.DomainEntities.PriceList;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class LiveOfferService : IOfferlist
    {
        public LiveOfferService(IOfferRI  offerRI)
        {
            _offerRI = offerRI;
        }
        private IOfferRI _offerRI;
        // offer list, create, detail ,update,delete 
        public Task<List<Offer>> GetOfferlist(string hubId)
        {
            return Task.Run(() =>
            {
                return _offerRI.GetOfferlist(hubId);
            });
        }
        public Task<string> AddOffer(Offer info)
        {
            return Task.Run(() =>
            {
                return _offerRI.AddOffer(info);
            });
        }
        public Task<Offer> GetOfferDetail(string Id)
        {
            return Task.Run(() =>
            {
                return _offerRI.GetOfferDetail(Id);
            });
        }
        public Task<int> EditOffer(Offer info)
        {
            return Task.Run(() =>
            {
                return _offerRI.EditOffer(info);
            });
        }
        public Task<bool> DeleteOffer(int id)
        {
            return Task.Run(() => {
                return _offerRI.DeleteOffer(id);
            });
        }
        // mapping function offer 
        public Task<List<PriceList>> GetallPricelist(PricelistFilter detail)
        {
            return Task.Run(() =>
            {
                return _offerRI.Getallpricelist(detail);
            });
        }
        public Task<List<PriceList>> GetMappedPricelist(PricelistFilter detail,string id)
        {
            return Task.Run(() =>
            {
                return _offerRI.GetMappedPricelist(detail, id);
            });
        }
        public Task<int> DeleteOfferItem(int Id)
        {
            return Task.Run(() =>
            {
                return _offerRI.DeleteOfferItem(Id);
            });
        }
        public Task<int> AddItem(PriceList info, string offerid, string startdae)
        {
            return Task.Run(() =>
            {
                return _offerRI.AddItem(info, offerid, startdae);
            });
        }
        public Task<int> AddItemList(Offer list)
        {
            return Task.Run(() =>
            {
                return _offerRI.AddItemList(list);
            });
        }
        public Task<int> AddItemList(PriceList info, string offerid)
        {
            return Task.Run(() =>
            {
                return _offerRI.AddItemList(info, offerid);
            });
        }
        public Task<bool> DeleteMappingItem(string ids)
        {
            return Task.Run(() =>
            {
                return _offerRI.DeleteMappingItem(ids);
            });
        }

        public Task<List<PriceList>> Getallitemlist(string maincategory, string Category, int type, string hubId)
        {
            return Task.Run(() =>
            {
                return _offerRI.Getallitemlist( maincategory,  Category,  type, hubId);
            });
        }
        public Task<List<SelectListItem>> GetOfferTypeList()
        {
            return Task.Run(() =>
            {
                return _offerRI.GetOfferTypeList();
            });
        }
    }
}
