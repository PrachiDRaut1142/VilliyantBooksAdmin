using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freshlo.Web.Controllers
{
    public class StockController : Controller
    {
        private IStockSI _stockSI;

        public StockController(IStockSI stockSI)
        {
            _stockSI = stockSI;
        }
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        public IActionResult Manage()
        {
            return View();
        }
        public async Task<PartialViewResult> _productList(string ItemName, string hub)
        {
            try
            {
                var hubId = Convert.ToString(User.FindFirst("branch").Value);
                return PartialView(await _stockSI.GetStockList(ItemName,hub));
            }
            catch( Exception ex)
            {
                return PartialView();
            }
          
        }
        public IActionResult Detail()
        {
            return View();
        }
      
    }
}