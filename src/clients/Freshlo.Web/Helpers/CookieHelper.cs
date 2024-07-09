using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Helpers
{
    public class CookieHelper
    {
        private IHttpContextAccessor _httpContextAccessor { get; set; }
        public CookieHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCookiesValue(string key, string value, CookieOptions options)
        {            
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, options);     
        }

        public string GetCookiesValue(string key) 
        {
            var hubId = _httpContextAccessor.HttpContext.Request.Cookies[key];
            return hubId;
        }
    }
}
