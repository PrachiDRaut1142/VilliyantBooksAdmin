using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models.InventoryVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freshlo.Web.Controllers
{
    public class AdminController : Controller
    {
        private ISettingSI _settingSI;
        private Inventory _InventorySI;
        public string hubId { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;


        public AdminController(Inventory InventorySI, ISettingSI settingSI, IHttpContextAccessor httpContextAccessor)
        {
            _settingSI = settingSI;
            _InventorySI = InventorySI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
        }

        public IActionResult Manage()
        {
            try
            {
                InventoryVM vm = new InventoryVM();
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                ViewBag.hubId = hubId;
                ViewBag.UserRole = Convert.ToString(User.FindFirst("userRole").Value);
                if (ViewBag.UserRole == "System Admin")
                {
                    return View(vm);
                }
                else
                {
                 return RedirectToAction("Login", "Account");
                }
                //return View(vm);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}