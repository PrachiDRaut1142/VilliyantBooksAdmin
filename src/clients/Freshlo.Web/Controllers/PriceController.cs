using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.PriceList;
using Freshlo.SI;
using Freshlo.Web.Extensions;
using Freshlo.Web.Models;
using Freshlo.Web.Models.ItemMaster;
using Freshlo.Web.Models.PricelistVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IronBarCode;
using System.Drawing;
using System.IO;

namespace Freshlo.Web.Controllers
{
    public class PriceController : Controller
    {
        private IPricelistSI  _pricelistSI;
        private readonly IHostingEnvironment _hostingEnvironment;
        public PriceController(IPricelistSI pricelistSI, IHostingEnvironment hostingEnvironment)
        {
            _pricelistSI = pricelistSI;
            _hostingEnvironment = hostingEnvironment;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                Task<List<PricelistCategory>> getMainCattegorylist = _pricelistSI.GetMainCategoryList();
                Task<List<PricelistCategory>> getCattegorylist = _pricelistSI.GetCategoriesAsync();
                Task<List<SelectListItem>> GetItemType = _pricelistSI.GetItemTypeList();
                await Task.WhenAll(getMainCattegorylist, getCattegorylist, GetItemType);
                var vm = new PricelistVM
                {
                    GetMainCategoryList = getMainCattegorylist.Result,
                    getCattegorylist = getCattegorylist.Result,
                    GetitemType = GetItemType.Result
                };
                return View(vm);

            }
            catch (Exception ex)
            {

                return new StatusCodeResult(500);
            }
        }

        public async Task<PartialViewResult> _Index(PricelistFilter detail)
        {
            try
            {

                Task<List<PriceList>> getItemPricelist = _pricelistSI.GetPricelistData(detail);
                await Task.WhenAll(getItemPricelist);
                var VM = new PricelistVM
                {
                    getItemPricelist = getItemPricelist.Result,
                };
                return PartialView("_Index", VM);


            }
            catch (Exception ex)
            {
                return PartialView("_Index");
            }

        }


        [Authorize]
        public async Task<IActionResult> ItemAvaliable()
        {
            try
            {
                Task<List<PricelistCategory>> getMainCattegorylist = _pricelistSI.GetMainCategoryList();
                Task<List<PricelistCategory>> getCattegorylist = _pricelistSI.GetCategoriesAsync();
                Task<List<SelectListItem>> GetItemType = _pricelistSI.GetItemTypeList();
                await Task.WhenAll(getMainCattegorylist, getCattegorylist, GetItemType);
                var vm = new PricelistVM
                {
                    GetMainCategoryList = getMainCattegorylist.Result,
                    getCattegorylist = getCattegorylist.Result,
                    GetitemType = GetItemType.Result
                };
                return View(vm);

            }
            catch (Exception ex)
            {

                return new StatusCodeResult(500);
            }
        }


        public async Task<PartialViewResult> _ItemAvaliable(PricelistFilter detail)
        {
            try
            {

                Task<List<PriceList>> getItemList = _pricelistSI.GetPricelistDataforCook(detail);
                await Task.WhenAll(getItemList);
                var VM = new PricelistVM
                {
                    getItemAvaliablePricelist = getItemList.Result,
                };

                return PartialView("_ItemAvaliable", VM);

            }
            catch (Exception ex)
            {
                return PartialView("_ItemAvaliable");
            }

        }




        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(PriceList list)
        {
            try
            {
                _pricelistSI.UpdaetPricelist(list);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ItemAvaliable(PriceList list)
        {
            try
            {
                _pricelistSI.UpdaetPricelist(list);
                return RedirectToAction("ItemAvaliable");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }

        //public async Task<JsonResult> _Index(PricelistFilter detail)
        //{
        //    var vm = new PricelistVM();
        //    try
        //    {
        //        vm.getItemPricelist = await _pricelistSI.GetPricelistData(detail);
        //        return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<PricelistVM>("_Index", vm) });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Error", Data = await this.RenderPartialViewAsync<PricelistVM>("_Index", vm) });
        //    }

        //}
        [Authorize]
        public async Task<IActionResult> List()
        {
            try
            {
                Task<List<PricelistCategory>> getMainCattegorylist = _pricelistSI.GetMainCategoryList();
                Task<List<PricelistCategory>> getCattegorylist = _pricelistSI.GetCategoriesAsync();
                Task<List<SelectListItem>> GetItemType = _pricelistSI.GetItemTypeList();
                await Task.WhenAll(getMainCattegorylist, getCattegorylist, GetItemType);
                var vm = new PricelistVM
                {
                    GetMainCategoryList = getMainCattegorylist.Result,
                    getCattegorylist = getCattegorylist.Result,
                    GetitemType = GetItemType.Result
                };
                return View(vm);

            }
            catch (Exception ex)
            {

                return new StatusCodeResult(500);
            }
        }
        public async Task<PartialViewResult> _List(PricelistFilter detail)
        {
            try
            {
                detail.HubId = Convert.ToString(User.FindFirst("branch").Value);
                Task<List<PriceList>> getItemPricelist = _pricelistSI.GetHubPricelist(detail);
                await Task.WhenAll(getItemPricelist);
                var VM = new PricelistVM
                {
                    getItemPricelist = getItemPricelist.Result,
                };
                return PartialView("_List", VM);


            }
            catch (Exception ex)
            {
                return PartialView("_List");
            }

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> List(PriceList list)
        {
            try
            {
                _pricelistSI.HubUpdatePrice(list);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}