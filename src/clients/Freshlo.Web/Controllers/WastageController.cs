using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.DomainEntities.Wastage;
using Freshlo.SI;
using Freshlo.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freshlo.Web.Controllers
{
    public class WastageController : Controller
    {
        private IWastageSI _wastageSI;
        public WastageController(IWastageSI wastageSI)
        {
            _wastageSI = wastageSI;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            try
            {
                var wastagevm = new WastageVM();
                wastagevm.GetItemList = await _wastageSI.GetallItemList();
                return View(wastagevm);
            }
            catch(Exception ex)
            {
                return new StatusCodeResult(500);
            }
           
        }
        [Authorize]
        public JsonResult InsertWastage(Wastage wastagedetail)
        {
            try
            {
                //string [] waslist = { Convert.ToString(wastagedetail.Wastage_Quan) + "," + Convert.ToString(wastagedetail.WastageItemPrice) + "," + "011", Convert.ToString(wastagedetail.TotalWastageQuan) + "," + Convert.ToString(wastagedetail.ItemwastagePrice) + "," + Convert.ToString(User.FindFirst("branch").Value) } ;

                wastagedetail.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                wastagedetail.Hub = Convert.ToString(User.FindFirst("branch").Value);
                 int resultwastage = _wastageSI.CreateorUpdateWastageDetail(wastagedetail);
                int wastagelog = _wastageSI.CreateWastageLog(wastagedetail);
                int resultstock = _wastageSI.UpdateStockDetail(wastagedetail);
                return Json("result");
            }
            catch(Exception ex)
            {
                return Json("");
            }
        }
        [Authorize]
        public IActionResult Manage()
        {
            return View();
        }
        [Authorize]
        public IActionResult Detail()
        {
            return View();
        }
    }
}