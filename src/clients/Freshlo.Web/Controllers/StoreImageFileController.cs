using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Freshlo.Web.Controllers
{

    [Authorize]
    public class StoreImageFileController : Controller
    {
        private readonly IMemoryCache _MemoryCache;

        public StoreImageFileController(IMemoryCache memCache)
        {
            _MemoryCache = memCache;
        }

        [AllowAnonymous]
        public async Task StoreFileAsync()
        {
            //generate random file name
            string randFileName = "MyFile-" + Guid.NewGuid().ToString();
            //save image content to the Cache
            var ret = Url.ActionContext.HttpContext.Request.Form["base64ImageContent"].ToString();
            _MemoryCache.Set(randFileName, ret);
            //return file name back to client
            HttpContext.Response.ContentType = "text/plain";
            await HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(randFileName));
        }
    }

}