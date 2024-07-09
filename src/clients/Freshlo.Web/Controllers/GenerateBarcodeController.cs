
using Freshlo.DomainEntities;
using IronBarCode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using ZXing;
using ZXing.Common;

namespace Freshlo.Web.Controllers
{
    public class GenerateBarcodeController : Controller
    {


        //public IActionResult Index()
        //{
        //    ItemStorage.AddItem(new Items { Name = "Item 1", Price = 10.99m, Barcode = "123456789" });
        //    ItemStorage.AddItem(new Items { Name = "Item 2", Price = 19.99m, Barcode = "987654321" });
        //    var items = ItemStorage.GetAllItems();
        //    return View(items);
        //}

        public ActionResult GenerateBarcode(string Barcode, AliyunCredential credential)
        {
            var item = new Items
            {
                Name = "Imthiyas",
                Barcode = "7975073656"
            };

            var barcodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 100,
                    Width = 300
                }
            };

            var pixelData = barcodeWriter.Write(item.Barcode);
            var barcodeBitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb);

            using (var ms = new MemoryStream())
            {
                var bitmapData = barcodeBitmap.LockBits(
                    new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    ImageLockMode.WriteOnly,
                    barcodeBitmap.PixelFormat
                );

                try
                {
                    Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    barcodeBitmap.UnlockBits(bitmapData);
                }

                barcodeBitmap.Save(ms, ImageFormat.Png);
                item.BarcodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
            }




            return View(item);
        }

        //public ActionResult GenerateBarcode1(string scannedBarcode)
        //{
        //    var item = ItemStorage.GetItemByBarcode(scannedBarcode);

        //    if (item != null)
        //    {
        //        return Json(item);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}


    }



        //    private readonly IHostingEnvironment _hostingEnvironment;

        //    public GenerateBarcodeController (IHostingEnvironment hostingEnvironment)
        //    {
        //        _hostingEnvironment = hostingEnvironment;
        //    }
        //    //[HttpPost]
        //    //public ActionResult Index(BarCodeModel model)
        //    //{
        //    //    barcodecs objbar = new barcodecs();
        //    //    //Product objprod = new Product()
        //    //    //{
        //    //    //    Name = model.PriceId,

        //    //    //    Barcode = objbar.generateBarcode(),
        //    //    //    BarCodeImage = objbar.getBarcodeImage(objbar.generateBarcode(), model.PriceId)
        //    //    //};
        //    //    //context.Products.InsertOnSubmit(objprod);
        //    //    //context.SubmitChanges();
        //    //    return RedirectToAction("BarCode", "Home");
        //    //}


        //    //public IActionResult CreateBarcode()    
        //    //{
        //    //    return View();
        //    //}

        //    //[HttpPost]  
        //    //public IActionResult CreateBarcode(GenerateBarcodeModel generateBarcode)
        //    //{
        //    //    try
        //    //    {
        //    //        GeneratedBarcode barcode = BarcodeWriter.CreateBarcode(generateBarcode.BarcodeText, BarcodeWriterEncoding.Code128);
        //    //        barcode.ResizeTo(400, 120);
        //    //        barcode.AddBarcodeValueTextBelowBarcode();
        //    //        // Styling a Barcode and adding annotation text
        //    //        barcode.ChangeBarCodeColor(Color.BlueViolet);
        //    //        barcode.SetMargins(10);
        //    //        string path = Path.Combine(_hostingEnvironment.WebRootPath, "GeneratedBarcode");
        //    //        if (!Directory.Exists(path))
        //    //        {
        //    //            Directory.CreateDirectory(path);
        //    //        }
        //    //        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "GeneratedBarcode/barcode.png");
        //    //        barcode.SaveAsPng(filePath);
        //    //        string fileName = Path.GetFileName(filePath);
        //    //        string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/GeneratedBarcode/" + fileName;
        //    //        ViewBag.QrCodeUri = imageUrl;

        //    //    }
        //    //    catch (Exception)
        //    //    {
        //    //        throw;
        //    //    }
        //    //    return View();
        //    //}

        //    public ActionResult CreateBarcode(string barcodeValue)
        //    {
        //        var barcodeWriter = new BarcodeWriterPixelData
        //        {
        //            Format = BarcodeFormat.CODE_128,
        //            Options = new EncodingOptions
        //            {
        //                Height = 100,
        //                Width = 300
        //            }
        //        };

        //        var pixelData = barcodeWriter.Write("Fahad");
        //        var barcodeBitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb);

        //        using (var ms = new MemoryStream())
        //        {
        //            var bitmapData = barcodeBitmap.LockBits(
        //                new Rectangle(0, 0, pixelData.Width, pixelData.Height),
        //                ImageLockMode.WriteOnly,
        //                barcodeBitmap.PixelFormat
        //            );

        //            try
        //            {
        //                Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
        //            }
        //            finally
        //            {
        //                barcodeBitmap.UnlockBits(bitmapData);
        //            }

        //            barcodeBitmap.Save(ms, ImageFormat.Png);
        //            return File(ms.ToArray(), "image/png");
        //        }
        //    }
        //    public ActionResult ScanBarcode(string scannedBarcode)
        //    {
        //        var item = ItemStorage.GetItemByBarcode(scannedBarcode);

        //        if (item != null)
        //        {
        //            return Json(item);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }


        //}
    }