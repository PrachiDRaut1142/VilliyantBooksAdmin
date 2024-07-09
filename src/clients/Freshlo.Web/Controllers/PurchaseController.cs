using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.DomainEntities.Purchase;
using Freshlo.RI;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models;
using Freshlo.Web.Models.PricelistVM;
using Freshlo.Web.Models.PurchaseVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;

namespace Freshlo.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private  IPurchaseSI _purchaseSI;
        private ISalesSI _salesSI;
        public string hubId { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public PurchaseController(IPurchaseSI purchaseSI, ISalesSI salesSI, IHttpContextAccessor httpContextAccessor)
        {
            _purchaseSI = purchaseSI;
            _salesSI = salesSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Create([FromServices] IItemRI _itemRI)
        {
            try
            {
                CreateVM vm = new CreateVM();
                vm.HubList = await _purchaseSI.GetHubList();
                vm.SupplierList = await _purchaseSI.GetSupplierNameList();
                return View(vm);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        public async Task<IActionResult> Manage()
        {
            try
            {
                return View(await _purchaseSI.GetSummaryData());
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        public async Task<IActionResult> Detail(string id)
        {
            DetailVM vm = new DetailVM();
            try
            {
               vm.Detail=await _purchaseSI.GetPurchaseDetail(id);
                vm.List = await _purchaseSI.GetPurchaseItemList(id);
                vm.SupplierList = await _purchaseSI.GetSupplierNameList();
                return View(vm);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
        [HttpGet]
        public async Task<JsonResult> ItemSelectList(string name,string purchaseId)
        {
            try
            {
                List<Item> itemList = await _purchaseSI.GetItemList(name, purchaseId);
                return Json(new Message<List<Item>> { IsSuccess = true, ReturnMessage = "success", Data = itemList });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }
        public async Task<JsonResult> InsertPurchaseOrder(PurchaseDetail info)
        {
            try            {
                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                string purchaseId = await _purchaseSI.InsertPurchase(info);
                if (purchaseId != "")
                {
                    info.PurchaseId = purchaseId;
                    await _purchaseSI.UpdatePriceList(info);
                }
                return Json(new Message<string> { IsSuccess = true, ReturnMessage = "success", Data = purchaseId });
              
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });

            }
        }
        public async Task<PartialViewResult> _manage()
        {
            return PartialView( await _purchaseSI.GetAPurchaseList());
        }
        [Authorize]
        public async Task<JsonResult> UpdatePurchaseList(PurchaseDetail info)
        {
            try
            {
                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                string purchaseId = await _purchaseSI.UpdatePurchase(info);
                    await _purchaseSI.UpdatePriceList(info);
                return Json(new Message<string> { IsSuccess = true, ReturnMessage = "success", Data = purchaseId });

            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });

            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult Detail(Purchase info)
        {
            DetailVM vm = new DetailVM();
            try
            {
                info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                _purchaseSI.UpdatePurchaseDetail(info);
                return RedirectToAction("Detail");
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }
        [Authorize]
        public async Task<IActionResult> ItemSummary([FromServices] IPricelistSI _pricelistSI, [FromServices] IItemSI _itemSI)
        {
            Task<List<PricelistCategory>> getCattegorylist = _pricelistSI.GetCategoriesAsync();
            Task<List<SelectListItem>> getSubCategorylist = _itemSI.GetSubCategoryList(hubId);
            Task<List<SelectListItem>> getMainCategorylist = _itemSI.GetMainCategoryList(hubId);


            await Task.WhenAll(getCattegorylist);
            var vm = new SummaryVM
            {
                Categorylist = getCattegorylist.Result,
                SubCategorylist=getSubCategorylist.Result,
                MainCategorylist = getMainCategorylist.Result,

            };
            return View(vm);
        }
        public async Task<JsonResult> CreateFinance(Finance info,[FromServices] IFinancialSI _financialSI)
        {
            try
            {
                info.Created_By = Convert.ToInt32(User.FindFirst("id").Value);
                info.Paid_On = DateTime.UtcNow.Date;
                info.Remark = "Purchase Order";
                info.Paid_To = "FreshLo";
                int Id = await _financialSI.CreateFinance(info);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = Id });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });

            }
        }
        public async Task<PartialViewResult> _itemSummary(SummaryFilter Options)
        {
            try
            {
                return PartialView(await _purchaseSI.GetItemSummary(Options));
            }
            catch(Exception e)
            {
                return PartialView();
            }
            
        }
        public async Task<IActionResult> PDf(string purchaseId)
        {
            DetailVM vm = new DetailVM();
            try
            {
                vm.Detail = await _purchaseSI.GetPurchaseDetail(purchaseId);
                vm.List = await _purchaseSI.GetPurchaseItemList(purchaseId);
                return new ViewAsPdf(vm) { FileName = string.Format("PurchaseList.pdf") };
            }
            catch(Exception e)
            {
                return new ViewAsPdf();
            }
            
        }
        public async Task<PartialViewResult> _categorySummary(SummaryFilter Options)
        {
            try
            {
                return PartialView(await _purchaseSI.GetCategorySummary(Options));
            }
            catch (Exception e)
            {
                return PartialView();
            }

        }
        public IActionResult CreateOrder()
        {
            return View();
        }
       
    }
}