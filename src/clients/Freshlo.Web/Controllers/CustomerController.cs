using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freshlo.DomainEntities;
using Freshlo.SI;
using Freshlo.Web.Extensions;
using Freshlo.Web.Models;
using Freshlo.Web.Models.CustomerVM;
using Microsoft.AspNetCore.Authorization;
using Freshlo.DomainEntities.Hub;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using DemoDecodeURLParameters.Security;

namespace Freshlo.Web.Controllers
{

    public class CustomerController : Controller
    {
        private ICustomerSI _customerSI;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IEmployeeSI _employeeSI;
        private readonly CustomIDataProtection protector;

        private ISettingSI _settingSI;

        public CustomerController(ICustomerSI customerSI, CustomIDataProtection customIDataProtection, IHostingEnvironment hostingEnvironment, IEmployeeSI employeeSI, ISettingSI settingSI)
        {
            _customerSI = customerSI;
            _hostingEnvironment = hostingEnvironment;
            _employeeSI = employeeSI;
            _settingSI = settingSI;
            protector = customIDataProtection;

        }

        public IActionResult Index()
        {
            throw new Exception("This is unhandled exception");
            return View();
        }
        public async Task<IActionResult> Dashboard()
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Task<List<Hub>> getHublist = _employeeSI.GetHublist();
         
            await Task.WhenAll(getHublist);
            var VM = new CustomerManageVM
            {
                getHublist = getHublist.Result
            };


            VM.businessInfo = _settingSI.GetbusinessInfoDetails(0);
            ViewBag.businessName = VM.businessInfo.hotel_name;
            ViewBag.logoUrl = VM.businessInfo.logo_url;
            if (TempData["ViewMessage"] != null)
                VM.ViewMessage = TempData["ViewMessage"] as string;

            if (TempData["ErrorMessage"] != null)
                VM.ErrorMessage = TempData["ErrorMessage"] as string;
            return View(VM);
        }
        [HttpPost]
        public IActionResult Create(Customer cust)
        {
            try
            {
                cust.CreatedBy = Convert.ToString(User.FindFirst("empID").Value);
                if (cust.CustomerId == null)
                {
                    cust.custId = _customerSI.CreateCustomer(cust);
                    cust.CustomerId = "CI0" + cust.custId;
                    cust.Id = _customerSI.AddAddress(cust);
                }
                else
                {
                    cust.Id = _customerSI.AddAddress(cust);
                }
                TempData["ViewMessage"] = "Customer Created successfully.";
                return RedirectToAction("Manage", "Customer");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult Manage(int a, int b)
        {
            CustomerManageVM vm = new CustomerManageVM();
            try
            {
                vm.a = a;
                vm.b = b;

                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }
        [Authorize]
        public async Task<IActionResult> Detail(string id)
        {
            id = protector.Encode(id);
            DetailViewModel vm = new DetailViewModel();
            try
            {
                vm.Summary = await _customerSI.GetSalesSummary(id);
                vm.CustomerDetail = await _customerSI.GetCustomerDetail(id);
                vm.CustomerDetailist = await _customerSI.GetCustomerDetaillist(id);
                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch (Exception e)
            {

            }
            return View();
        }


        public async Task<JsonResult> AddToWallet(Wallet info)
        {
            try
            {
                info.CreatedBy = Convert.ToString(User.FindFirst("empId").Value);


                return Json(new Message<int> { IsSuccess = true, ReturnMessage = "success", Data = await _customerSI.AddToWallet(info)
                });

            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });

            }
        }
        public async Task<PartialViewResult> _manage(int a, int b)
        {
            try
            {
                var hubId = Convert.ToString(User.FindFirst("branch").Value);
                var role = Convert.ToString(User.FindFirst("userRole").Value);

                CustomerManageVM vm = new CustomerManageVM();

                if (a != 0 && b != 0)
                {
                    Task<List<Customer>> getregCustomerhistorylist = _customerSI.GetCustomerOrderlist(a, b);
                    vm.GetCustomerRegOrderlist = await getregCustomerhistorylist;
                    var list = vm.GetCustomerRegOrderlist.Select(p => new
                    {
                        v = p.DecodeId = protector.Decode(p.CustomerId.ToString()),
                    }).ToList();
                    return PartialView(vm);

                }
                else
                {
                    Task<List<CustomerSalesHistory>> getCustomerhistorylist = _customerSI.GetCustomerHistory(hubId, role);

                    vm.getCustomerHistoryList = await getCustomerhistorylist;
                    var list = vm.getCustomerHistoryList.Select(p => new
                    {
                        v = p.DecodeId1 = protector.Decode(p.CustomerId.ToString()),
                    }).ToList();
                    return PartialView(vm);
                }



            }
            catch (Exception e)
            {
                return PartialView();
            }
        }
        public async Task<PartialViewResult> _salesList(string customerId)
        {
            DetailViewModel vm = new DetailViewModel();
            try
            {
                vm.SalesList = await _customerSI.GetSalesOrderList(customerId);
                return PartialView(vm);
            }
            catch (Exception ex)
            {
                return PartialView();
            }
        }


        public async Task<PartialViewResult> _addressadd(string customerId)
        {
            DetailViewModel vm = new DetailViewModel();
            try
            {
                vm.CustomerDetailist = await _customerSI.GetCustomerDetaillist(customerId);
                return PartialView(vm);
            }
            catch (Exception ex)
            {
                return PartialView();
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult UnregisteredCustomer(int a, int b)
        {
            CustomerManageVM vm = new CustomerManageVM();

            try
            {

                vm.a = a;
                vm.b = b;

                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }
        //public async Task<PartialViewResult> _customerlist(int a, int b)
        //{


        //    try
        //    {
        //        CustomerManageVM vm = new CustomerManageVM();

        //        if (a != 0 && b != 0)
        //        {
        //            Task<List<Customer>> getUnregCustomerhistorylist = _customerSI.GetCustomerOrderlist(a, b);
        //            vm.GetCustomerUnregOrderlist = await getUnregCustomerhistorylist;
        //            return PartialView(vm);

        //        }
        //        else
        //        {
        //            Task<List<Customer>> getCustomerList = _customerSI.GetCustomerList();
        //            vm.getCustomerList = await getCustomerList;
        //            return PartialView(vm);

        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        return PartialView();
        //    }
        //}


        public async Task<JsonResult> _customerlist(int a, int b)
        {
            try
            {

                CustomerManageVM vm = new CustomerManageVM();
                if (a != 0 && b != 0)
                {
                    vm.GetCustomerUnregOrderlist = await _customerSI.GetCustomerOrderlist(a, b);
                    return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<CustomerManageVM>("_customerlist", vm) });

                }
                else
                {
                    vm.getCustomerList = await _customerSI.GetCustomerList();
                    return Json(new Message<string>() { IsSuccess = true, ReturnMessage = "Success", Data = await this.RenderPartialViewAsync<CustomerManageVM>("_customerlist", vm) });

                }
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = ex.Message, Data = null });
            }
        }

        public async Task<FileContentResult> GetFilterExcel()
        {
            var hubId = Convert.ToString(User.FindFirst("branch").Value);
            var role = Convert.ToString(User.FindFirst("userRole").Value);
            string webRootPath = _hostingEnvironment.WebRootPath;
            return File(await _customerSI.FilterExcelofCustomer(hubId, role, webRootPath),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerInfo" + DateTime.Now.ToString("MMddyyyyhhmm") + ".xlsx");

        }

        [HttpGet]
        public async Task<JsonResult> Delete(string Id)
        {
            try
            {
                return Json(new Message<bool>() { IsSuccess = true, ReturnMessage = "success", Data = await _customerSI.delcustomer(Id) });
            }
            catch (Exception ex)
            {
                return Json(new Message<string>() { IsSuccess = false, ReturnMessage = "Server side error. Try again later.", Data = null });
            }
        }

        [HttpPost]
        public IActionResult Edit(Customer cust)
        {
            cust.CreatedBy = Convert.ToString(User.FindFirst("empID").Value);
                var Address =  _customerSI.editaddress(cust);
            return RedirectToAction("Manage", "Customer");

        }

        [HttpPost]
        public IActionResult AddAddress(Customer cust)
        {
            cust.CreatedBy = Convert.ToString(User.FindFirst("empID").Value);
           var address = _customerSI.AddsecAddress(cust); ;
            return RedirectToAction("Manage", "Customer");

        }
        //[HttpPost]
        //public IActionResult editAddress(Customer cust)
        //{
        //    cust.CreatedBy = Convert.ToString(User.FindFirst("empID").Value);
        //   var address = _customerSI.AddsecAddress(cust); ;
        //    return View();
                
        //}

        [HttpGet]
        public async Task<JsonResult> GetcustDetail(string Type, string custId)
        {
            try
            {
                Customer customerList = await _customerSI.GetCustomerDataId(Type, custId);
                return Json(new Message<Customer> { IsSuccess = true, ReturnMessage = "success", Data = customerList });
            }
            catch (Exception ex)
            {
                return Json(new Message<string> { IsSuccess = false, ReturnMessage = "Server Error. Try again later.", Data = null });
            }
        }


        public JsonResult updateaddress(Customer info)
        {
            try
            {
              
                return Json(_customerSI.EditAddress(info));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        public JsonResult editAddress(Customer cust)
        {
            try
            {
                cust.CreatedBy = Convert.ToString(User.FindFirst("empID").Value);
                return Json(_customerSI.AddsecAddress(cust));
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
    }
}