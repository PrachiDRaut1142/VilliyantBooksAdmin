using Microsoft.AspNetCore.Http;

namespace Freshlo.DomainEntities
{
    public class ItemColorInfo
    {
        public int Id { get; set; }
        public string ItemId { get; set; }
        public string PriceId { get; set; }
        public int ProductColorId { get; set; }
        public string ColorId { get; set; }
        public string Color { get; set; }
        public IFormFile Image { get; set; }
        public int Stock { get; set; }
        public int ColorStock { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}