using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.DomainEntities;
using Freshlo.SI;
using Freshlo.Web.Models;
using Freshlo.Web.Models.Financial;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace Freshlo.Web.Controllers
{
    public class FinancialController : Controller
    {
        private IFinancialSI _financialSI;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FinancialController(IFinancialSI financialSI, IHostingEnvironment hostingEnvironment)
        {
            _financialSI = financialSI;
            _hostingEnvironment = hostingEnvironment;
        }
        [Authorize]
        public async Task<IActionResult> Manage()
        {
            try
            {
                BaseViewModel vm = new BaseViewModel();
                if (TempData["ViewMessage"] != null)
                    vm.ViewMessage = TempData["ViewMessage"] as string;

                if (TempData["ErrorMessage"] != null)
                    vm.ErrorMessage = TempData["ErrorMessage"] as string;

                return View(await _financialSI.GetSummaryData());
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
          
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Finance info)
        {
            try
            {
                info.Created_By = Convert.ToInt32(User.FindFirst("id").Value);
                int Id =await _financialSI.CreateFinance(info);
                if (Id > 0) return RedirectToAction("Manage");
                else
                {
                    TempData["TempError"] = "error";
                    return RedirectToAction("Create");
                }
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
          
        }
        [Authorize]
        public async Task<IActionResult> Detail(int id, int opt)
        {
            DetailViewModel vm = new DetailViewModel();
            try
            {
                vm.FinanceDetails = await _financialSI.GetFinanceDetail(id, opt);

                if (TempData["ViewMessage"] != null)
                    vm.ViewMessage = TempData["ViewMessage"] as string;

                if (TempData["ErrorMessage"] != null)
                    vm.ErrorMessage = TempData["ErrorMessage"] as string;
                return View(vm);
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        [HttpPost]
        [ActionName("UpdateFinancialDetail")]
        public async Task<IActionResult> UpdateFinancialDetail(Finance info)
        {
            try
            {
                info.Updated_By = Convert.ToInt32(User.FindFirst("id").Value);
                int Result = await _financialSI.UpdateFinancialDeail(info);
                return RedirectToAction("Manage");
                
               
            }
            catch (SqlException se) when (se.Number == 2627 || se.Number == 50001 || se.Number == 50002)
            {
                TempData["ErrorMessage"] = se.Number == 2627 ? " Id already in use." : se.Message;
                return RedirectToAction("Detail", "Financial", new { @id = info.Id });
            }
            catch (Exception ex)
            {

                return new StatusCodeResult(404);
            }

        }
        public IActionResult FinPDF()
        {
            return View();
        }

        public async Task<PartialViewResult> _manage(string dateRange,string paid_From, string paid_Till)
        {
            try
            {
                if(dateRange!=null)
                {
                    paid_From = dateRange.Split('/')[0];
                    paid_Till = dateRange.Split('/')[1];
                }
               
                return PartialView(await _financialSI.GetManage(paid_From, paid_Till));
            }
            catch(Exception ex)
            {
               
            }
            return PartialView();
        }
        public async Task<IActionResult> FinancialPDf(string dateRange, string paid_From, string paid_Till)
        {
            ManageViewModel vm = new ManageViewModel();
            try
            {
                if (dateRange != null)
                {
                    paid_From = dateRange.Split('/')[0];
                    paid_Till = dateRange.Split('/')[1];
                }
                vm.FinanceList = await _financialSI.GetManage(paid_From, paid_Till);
                vm.FinanceSummary =  _financialSI.GetSummaryData();
                return new ViewAsPdf(vm) { FileName = string.Format("FinanceList.pdf") };
            }
            catch (Exception ex)
            {
                return new ViewAsPdf();
            }
        }
        public async Task<FileContentResult> ExportFinancialListtoExcel(string dateRange, string paid_From, string paid_Till)
        {

            string webRootPath = _hostingEnvironment.WebRootPath;
            if (dateRange != null)
                {
                    paid_From = dateRange.Split('/')[0];
                    paid_Till = dateRange.Split('/')[1];
                }

            return File(await _financialSI.ExportExcelofFinance(webRootPath,  paid_From,  paid_Till),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FinancialView" + DateTime.Now.ToString("MMddyyyyhhmm") + ".xlsx");

        }
    }
}