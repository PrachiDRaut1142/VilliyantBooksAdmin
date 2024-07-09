using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Hub;
using System;
using System.Collections.Generic;

namespace BarCode.Models
{
    public class BarCodeModel
    {
        public int Id { get; set; }
        public string PriceId { get; set; }
       
        public byte[] BarcodeImage { get; set; }
        public string Barcode { get; set; }
        public string ImageUrl { get; set; }
    }
}