using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Offer;
using Freshlo.DomainEntities.PriceList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace Freshlo.RI
{
    public interface IOfferRI
    {
        // offer list, create, detail ,update,delete 
        List<Offer> GetOfferlist(string hubId);
        string AddOffer(Offer info);
        Offer GetOfferDetail(string Id);
        int EditOffer(Offer info);
        bool DeleteOffer(int id);
        // mapping function offer 
        List<PriceList> Getallpricelist(PricelistFilter details);

        List<PriceList> Getallitemlist(string maincategory, string Category, int type, string hubId);
        List<PriceList> GetMappedPricelist(PricelistFilter details, string id);
        int DeleteOfferItem(int Id);
        int AddItem(PriceList info, string offerid, string startdate);
        int AddItemList(Offer list);
        int AddItemList(PriceList info, string offerid);
        bool DeleteMappingItem(string ids);
        List<SelectListItem> GetOfferTypeList();
    }
}
