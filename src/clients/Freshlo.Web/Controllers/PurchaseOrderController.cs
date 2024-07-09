using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using Freshlo.DomainEntities.Purchase;
using Freshlo.SI;
using Freshlo.Web.Extensions;
using Freshlo.Web.Models;
using Freshlo.Web.Models.PurchaseVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace Freshlo.Web.Controllers
{
    public class PurchaseOrderController : Controller
    {

        private IPurchaseSI _purchaseSI;
        private ISalesSI _salesSI;
        private IPricelistSI _pricelistSI;

        public PurchaseOrderController(IPurchaseSI purchaseSI, ISalesSI salesSI, IPricelistSI pricelist)
        {
            _purchaseSI = purchaseSI;
            _salesSI = salesSI;
            _pricelistSI = pricelist;
        }


        public async Task<IActionResult> Create()
        {
            try
            {
                CreateVM vm = new CreateVM();
                vm.HubList = await _purchaseSI.GetHubList();
                vm.SupplierList = await _purchaseSI.GetSupplierNameList();
                return View(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        public async Task<JsonResult> InsertPurchaseOrder(PurchaseDetail info)
        {
            try
            {
                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                string purchaseId = await _purchaseSI.InsertNewPurchase(info);
                if (purchaseId != "")
                {
                    info.PurchaseId = purchaseId;
                    //await _purchaseSI.UpdatePriceList(info);
                }
                return Json(new Message<string> { IsSuccess = true, ReturnMessage = "success", Data = purchaseId });

            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });

            }
        }


        public async Task<PartialViewResult> _AppendPurchase(string datefrom, string dateto, string CreateFor)
        {
            var VM = new DetailVM();
            Task<List<Purchase>> getpendingPurchaseList = _purchaseSI.GetPendingPurchaseList();
            Task<List<Purchase>> getAddPendingpurchaseList = _purchaseSI.GetAddPnedingPurchaseList(datefrom, dateto, CreateFor);
            await Task.WhenAll(getpendingPurchaseList, getAddPendingpurchaseList);
            if (string.IsNullOrEmpty(datefrom))
            {
                VM = new DetailVM
                {
                    purchaselist = getpendingPurchaseList.Result,
                };
            }
            else
            {
                VM = new DetailVM
                {
                    purchaselist = getAddPendingpurchaseList.Result,
                };
            }
            return PartialView("_AppendPurchase", VM);
        }


        [Authorize]
        public async Task<IActionResult> DownloadPDF(string Id)
        {
            DetailVM vm = new DetailVM();
            try
            {
                vm.Detail = await _purchaseSI.GetPurchaseDetail(Id);
                vm.List = await _purchaseSI.DownloadPdfData(Id);
                return new ViewAsPdf(vm) { FileName = string.Format("PurchaseList.pdf") };

            }
            catch (Exception e)
            {
                return new ViewAsPdf();
            }



        }






        [Authorize]
        public async Task<IActionResult> Detail(string id, PricelistFilter detail)
        {
            DetailVM vm = new DetailVM();
            try
            {
                vm.Detail = await _purchaseSI.GetPurchaseDetail(id);
                vm.List = await _purchaseSI.GetallnewPurchaseListItem(id, detail);
                vm.SupplierList = await _purchaseSI.GetSupplierNameList();
                vm.GetMainCategoryList = await _pricelistSI.GetMainCategoryList();
                vm.getCattegorylist = await _pricelistSI.GetCategoryListext();
                vm.getVendorList = await _pricelistSI.GetVendorList();
                return View(vm);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        public async Task<PartialViewResult> _AppendPurchaseDetail(string id, PricelistFilter detail)
        {
            var VM = new DetailVM();
            Task<List<PurchaseList>> getpendingPurchaseList = _purchaseSI.GetallnewPurchaseListItem(id, detail);
            await Task.WhenAll(getpendingPurchaseList);
            VM = new DetailVM
            {
                List = getpendingPurchaseList.Result,
            };
            return PartialView("_AppendPurchaseDetail", VM);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Detail(Purchase info)
        {
            DetailVM vm = new DetailVM();
            try
            {
                info.LastUpdatedBy = Convert.ToString(User.FindFirst("empId").Value);
                _purchaseSI.NewUpdatePurchaseOrderDetail(info);
                return RedirectToAction("Detail");
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }


        [Authorize]
        public async Task<JsonResult> UpdatePurchaseList(PurchaseDetail info)
        {
            try
            {
                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                string purchaseId = await _purchaseSI.NewUpdatePurchaseOrder(info);
                ////await _purchaseSI.UpdateNewPriceList(info);
                return Json(new Message<string> { IsSuccess = true, ReturnMessage = "success", Data = purchaseId });

            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });

            }
        }

   
      


        public async Task<JsonResult> DeletePurchaseList(string id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _purchaseSI.DeletePurchaseList(id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }

        public IActionResult Manage()
        {
            return View();
        }



    }
}
