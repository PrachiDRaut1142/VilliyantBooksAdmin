using Freshlo.DomainEntities.Inventory;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models.InventoryVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshlo.Web.Controllers
{
    public class InventoryController : Controller
    {
        private ISettingSI _settingSI;
        private Inventory _InventorySI;
        public string hubId { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;


        public InventoryController(Inventory InventorySI,ISettingSI settingSI, IHttpContextAccessor httpContextAccessor)
        {
            _settingSI = settingSI;
            _InventorySI = InventorySI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
        }

        public IActionResult create()
        {
            try
            {

                InventoryVM vm = new InventoryVM();
                vm.Adhoc_Inventory = _InventorySI.New_AuditList(hubId).Result;
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch
            {
                return View("");
            }

        }
        public IActionResult list()
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                InventoryVM vm = new InventoryVM();
               vm.Inventory_Logs= _settingSI.Get_Inventory_Assets_List(hubId).Result;
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch
            {
                return View("");
            }
        }

        public async Task<IActionResult> manage(string id)
        {
            try
            {
                if(hubId == null)
                {
                    hubId = "HID01";
                }
                Task<List<InventoryAsset>> getAdhoclist = _InventorySI.Adhoc_Inventory(hubId);
                await Task.WhenAll(getAdhoclist);
                var vm = new InventoryVM
                {
                    Adhoc_Inventory = getAdhoclist.Result
                };
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public JsonResult CreateAdhoc(InventoryAsset Id)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                Id.Hub = hubId;
                Id.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                return Json(_InventorySI.Adhoc_Updates(Id));
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }

        public PartialViewResult _InventoryLogs(string id)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                var data = _InventorySI.Inventory_Logs(hubId);
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }
        public JsonResult CreateAudit(InventoryAsset info)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                info.Hub = hubId;
                //string [] waslist = { Convert.ToString(wastagedetail.Wastage_Quan) + "," + Convert.ToString(wastagedetail.WastageItemPrice) + "," + "011", Convert.ToString(wastagedetail.TotalWastageQuan) + "," + Convert.ToString(wastagedetail.ItemwastagePrice) + "," + Convert.ToString(User.FindFirst("branch").Value) } ;
                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                int AuditCreate = _InventorySI.CreateAudit(info);

                return Json("result");
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }

        public PartialViewResult _AuditLogs()
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                var data = _InventorySI.AuditLogs(hubId);
                return PartialView(data.Result);
            }
            catch
            {
                return PartialView("");
            }
        }
        public JsonResult GetAuditlist(string id)
        {
            try
            {
                if (hubId == null)
                {
                    hubId = "HID01";
                }
                return Json(_InventorySI.GetAuditlist(id,hubId));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }


    }

}
