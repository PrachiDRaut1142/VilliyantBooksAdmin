using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Freshlo.Client.Models;
using Freshlo.Web.Models;
using Freshlo.SI;
using Freshlo.DomainEntities;
using Freshlo.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Freshlo.Web.Helpers;

namespace Freshlo.Client.Controllers
{
    public class HomeController : Controller
    {
        private ISalesSI _salesSI;
        private IItemSI _itemSI;
        public string hubId { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ISalesSI salesSI, IItemSI itemSI, IHttpContextAccessor httpContextAccessor)
        {
            _salesSI = salesSI;
            _itemSI = itemSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
        }

        public async Task<IActionResult> Index(string ItemName)
        {
            try
            {
                var salesvm = new SalesVM();
                salesvm.ItemList = await _salesSI.GetallItemList_1(ItemName,null);
                salesvm.CategoryList = await _itemSI.GetCategoriesAsync(hubId);
                return View(salesvm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<JsonResult> _ItemList(string ItemName)
        {
            try
            {
                var salesvm = new SalesVM();
                salesvm.ItemList = await _salesSI.GetallItemList_1(ItemName,null);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<SalesVM>("_ItemList", salesvm) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });
            }
        }

        public async Task<JsonResult> _ItemList1(string ItemName, string CatogeryName)
        {
            try
            {
                var salesvm = new SalesVM();
                salesvm.ItemList = await _salesSI.GetallItemList_1(ItemName, CatogeryName);
                return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<SalesVM>("_ItemList1", salesvm) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
