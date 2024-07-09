using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Freshlo.Web.Controllers
{
    using Freshlo.Web.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Caching.Memory;
    using Neodynamic.SDK.Web;

    [Authorize]
    public class PrintHtmlCardController : Controller
    {
        private readonly IMemoryCache _MemoryCache;
        public string hubId { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PrintHtmlCardController(IMemoryCache memCache,IHttpContextAccessor httpContextAccessor)
        {
            _MemoryCache = memCache;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
        }
 
        public IActionResult Index()
        {
            ViewData["WCPScript"] = Neodynamic.SDK.Web.WebClientPrint.CreateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, Url.ActionContext.HttpContext.Request.Scheme), Url.Action("PrintImage", "PrintHtmlCard", null, Url.ActionContext.HttpContext.Request.Scheme), Url.ActionContext.HttpContext.Session.Id);

            return View();
        }

        [AllowAnonymous]
        public void PrintImage(string useDefaultPrinter, string printerName, string imageFileName)
        {

            //create a temp file name for our image file...

            //Because we know the Card size is 3.125in x 4.17in, we can specify 
            //it through a special format in the file name (see http://goo.gl/Owzr9o) so the
            //printing output size is honored; otherwise the output will be sized to Page Width & Height
            //specified by the printer driver default setting
            string fileName = imageFileName + "-PW=3.125-PH=4.17" + ".png";

            //Create a PrintFile object with the image file
            PrintFile file = new PrintFile(Convert.FromBase64String(_MemoryCache.Get<string>(imageFileName)), fileName);
            //Create a ClientPrintJob and send it back to the client!
            ClientPrintJob cpj = new ClientPrintJob();
            //set file to print...
            cpj.PrintFile = file;


            //set client printer...
            if (useDefaultPrinter == "checked" || printerName == "null")
                cpj.ClientPrinter = new DefaultPrinter();
            else
                cpj.ClientPrinter = new InstalledPrinter(printerName);

            //send it...
             File(cpj.GetContent(), "application/octet-stream");

        }

    }

}