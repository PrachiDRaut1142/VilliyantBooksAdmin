namespace Freshlo.DomainEntities
{
    public class ColorSizeMapping
    {
        public int MappingId { get; set; }
        public string ItemId { get; set; }
        public string PriceId { get; set; }
        public string ColorId { get; set; }
        public string MPriceId { get; set; }
        public string MColorId { get; set; }
        public string ColorDetail { get; set; }
        public float PriceDetail { get; set; }
        public float Stock { get; set; }
        public string Size { get; set; }
    }
}