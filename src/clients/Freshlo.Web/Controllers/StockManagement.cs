using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Stock;
using Freshlo.SI;
using Freshlo.Web.Models;
using Freshlo.Web.Models.ItemMaster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Freshlo.Web.Controllers
{
    public class StockManagement : Controller
    {
        private IStockSI _stockSI;
        private IItemSI _itemSI;
        private ISettingSI _settingSI;
        private readonly IHostingEnvironment _hostingEnvironment;

        public StockManagement(IStockSI stockSI, IItemSI itemSI, ISettingSI settingSI, IHostingEnvironment hostingEnvironment)
        {
            _stockSI = stockSI;
            _itemSI = itemSI;
            _settingSI = settingSI;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> StockUpdate()
        {
            try
            {
                var hubId = Convert.ToString(User.FindFirst("branch").Value);
                Task<List<SelectListItem>> getMainCattegorylist = _stockSI.GetMainCategoryList(hubId);
                Task<Stock> getstock = _stockSI.GetStock(hubId);
                await Task.WhenAll(getMainCattegorylist, getstock);
                var vm = new ItemMasterVM
                {
                    GetMainCategoryList = getMainCattegorylist.Result,
                    getstockcount = getstock.Result,
                };
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public async Task<PartialViewResult> _StockUpdate(ItemMasters item)
        {
            var hubId = Convert.ToString(User.FindFirst("branch").Value);
            Task<Stock> getstock = _stockSI.GetStock(hubId);
            Task<List<Stock>> getstocklist = _stockSI.GetStockItemlist(item.MainCategory, item.Category, item.type, hubId);
            await Task.WhenAll( getstock, getstocklist);
            var vm = new ItemMasterVM
            {
                getstockcount = getstock.Result,
                getstocklist = getstocklist.Result,
            };
            vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = vm.businessInfo.hotel_name;
            ViewBag.logoUrl = vm.businessInfo.logo_url;

            return PartialView(vm);   
        } 
        public async Task<JsonResult> UpdateSellingPrice(Stock st)
        {
            try
            {
                st.Hub = Convert.ToString(User.FindFirst("branch").Value);
                int result = await _stockSI.UpdatePrice(st);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }
        public async Task<JsonResult> UpdateStock(Stock st)
        {
            try
            {
                st.Hub = Convert.ToString(User.FindFirst("branch").Value);
                int result = await _stockSI.UpdateStock(st);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });            
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }
        public async Task<JsonResult> UpdateStockVis(Stock st)
        {
            try
            {
                st.Hub = Convert.ToString(User.FindFirst("branch").Value);
                int result = await _stockSI.UpdateItemIsVisiable(st);
                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = result });
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }

        public async Task<FileContentResult> ExportStockExcel(string maincategory,string Category,int type)
        {
            var hubId = Convert.ToString(User.FindFirst("branch").Value);
            var role = Convert.ToString(User.FindFirst("userRole").Value);
            string webRootPath = _hostingEnvironment.WebRootPath;
            return File(await _stockSI.ExportExcelofStock(hubId, role, webRootPath, maincategory, Category, type),
                "application/vnd.openxmlformats-officedocument.Stock.sheet", "StockInfo" + DateTime.Now.ToString("MMddyyyyhhmm") + ".xlsx");
        }
        public PartialViewResult _HeaderModal(TblListcs list)
        {
            try
            {
                //var bl = new ListBL();
                list.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                if (list.File != null)
                {
                    //var listId = _stockSI.InsertList(list);
                    var listId = 1;
                    list.Id = listId;
                    if (listId > 0)
                    {
                        string folderName = "Upload";
                        var rootFolder = _hostingEnvironment.WebRootPath;
                        string newPath = Path.Combine(rootFolder, folderName);
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        string fullPath = Path.Combine(newPath, list.File.FileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            list.File.CopyTo(stream);
                        }
                        var vm = new _HeaderModalVM
                        {
                            HeaderList = _stockSI.GetHeaderList(newPath, list.File.FileName),
                            FileName = list.File.FileName,
                        };
                        return PartialView(vm);
                    }
                }
                return default(PartialViewResult);
            }

            catch (Exception ex)
            {
                return default(PartialViewResult);
            }
        }
        public async Task<JsonResult> UploadExcel(TblListcs camp)
        {
            try
            {
                var vm = new ItemMasterVM();
                var trucoldata = _stockSI.DeleteStockStaggingData();
                camp.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);
                camp.hubId = Convert.ToString(User.FindFirst("branch").Value);
                string folderName = "Upload";
                var rootFolder = _hostingEnvironment.WebRootPath;
                camp.path = Path.Combine(rootFolder, folderName);
                camp.ListId = _stockSI.InsertList(camp);
                var staginglevel = _stockSI.UploadStatgingListFromExcels(camp);
                camp.SuccessfulCount = _stockSI.UploadStaggingToStock(camp);
                vm.GetListDetail = await _stockSI.GetCountDetail(camp);
                var data = camp.SuccessfulCount + "," + vm.GetListDetail.RejectedCount + "," +camp.ListId;
                camp.RejectedCount = vm.GetListDetail.RejectedCount;
                System.IO.File.Delete(Path.Combine(camp.path, camp.FileName));
                return Json(data + ",");
            }
            catch (Exception ex)
            {
                //_stockSI.DeleteAll(camp.ListId);
                return Json("");
            }
        }
        public async Task<FileContentResult> ExportRejectedData(int ListId)
        {
            var hubId = Convert.ToString(User.FindFirst("branch").Value);
            var role = Convert.ToString(User.FindFirst("userRole").Value);
            string webRootPath = _hostingEnvironment.WebRootPath;
            return File(await _stockSI.ExportRejectedData(hubId, role, webRootPath, ListId),
                "application/vnd.openxmlformats-officedocument.Stock.sheet", "RejectedData" + DateTime.Now.ToString("MMddyyyyhhmm") + ".xlsx");
        }
    }
}
