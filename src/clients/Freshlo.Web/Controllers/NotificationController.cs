using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Notification;
using Freshlo.SI;
using Freshlo.Web.Helpers;
using Freshlo.Web.Models.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freshlo.Web.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationSI _notificationSI;
        private readonly IHostingEnvironment _hostingEnvironment;
        public string hubId { get; set; }
        public ISettingSI _settingSI { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NotificationController(INotificationSI notificationSI, IHostingEnvironment hostingEnvironment, ISettingSI settingSI, IHttpContextAccessor httpContextAccessor)
        {
            _notificationSI = notificationSI;
            _hostingEnvironment = hostingEnvironment;
            _settingSI = settingSI;
            this._httpContextAccessor = httpContextAccessor;
            hubId = new CookieHelper(_httpContextAccessor).GetCookiesValue("BranchId");
        }
        public IActionResult Success()
        {
            return View();
        }


        
        [Authorize]
        [HttpGet]
        public IActionResult Create(string value)
        {
            CreateViewModel vm = new CreateViewModel();
            try
            {
                if (hubId == "null")
                {
                    hubId = "HID01";
                }
                vm.CustomerList = _notificationSI.GetCustomerList();
                vm.CustomerContactList = _notificationSI.GetCustomerContactList();
                if (TempData["ErrorMessage"] != null)
                {
                    vm.ErrorMsg = TempData["ErrorMessage"] as string;
                }

                vm.ImagePath = value;

                vm.businessInfo = _settingSI.GetbusinessInfoDetails(0);
                ViewBag.businessName = vm.businessInfo.hotel_name;
                ViewBag.logoUrl = vm.businessInfo.logo_url;
                return View(vm);
            }
            catch (Exception ex)
            {

            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Notification info, AliyunCredential credential)
        {
            try
            {
                var aliyunfolder = info.Aliyunkey;
                if (info.NotificationType == 1)
                {             
                    foreach (var contactno in info.CustomerContact)
                        await _notificationSI.SendSmsP(info.Description, contactno, info.senderId);
                }
                else
                {
                    CreateViewModel vm = new CreateViewModel();
                    string imagePath = "";
                    try
                    {
                        var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                        info.Created_By = Convert.ToString(User.FindFirst("empId").Value);

                        if (info.UploadImage != null)
                        {
                            string fullPath = "";
                            fullPath = Path.Combine(FilePath() + info.UploadImage.FileName);
                            var filepath = Path.GetTempFileName();
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                await info.UploadImage.CopyToAsync(stream);
                            }
                            var endResult = BlAliyun.PutIconObjectFromFile(info.UploadImage.FileName,Convert.ToString(Timestamp), fullPath, credential, "Notification",aliyunfolder);

                            if (endResult.Equals("true"))
                            {
                                System.IO.File.Delete(fullPath);
                                imagePath = "https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/Notification/" + Timestamp + ".png";
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "Error while uploading Image.....";
                                return RedirectToAction("Create", "Notification");
                            }
                        }

                        //if (info.UploadImage != null && info.Id != null)
                        //{
                        //    string fullPath = "";
                        //    var newfilepath = FilePath();
                        //    fullPath = Path.Combine(newfilepath + info.UploadImage.FileName);
                        //    var filepath = Path.GetTempFileName();
                        //    using (var stream = new FileStream(fullPath, FileMode.Create))
                        //    {
                        //        await info.UploadImage.CopyToAsync(stream);
                        //    }
                        //    var endResult = BlAliyun.PutIconObjectFromFile(info.UploadImage.FileName, Convert.ToString(info.Id), fullPath, credential, "Notification");

                        //    if (endResult.Equals("true"))
                        //    {
                        //        System.IO.File.Delete(fullPath);
                        //    }
                        //    else
                        //    {
                        //        TempData["Image"] = "Error while uploading Image.....";
                        //    }
                        //     return RedirectToAction("Create", "Notification");

                        //}
                        //_notificationSI.Create(info);
                        string url = "http://dailyfreshapi.cxengine.net/api/MakeNotify";
                        using (WebClient client = new WebClient())
                        {
                            client.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                            //string myParameters = "{date:\"03-12-2019\",description:\"test\",title:\"test\",img:\"NA\",id:\"02\"}";
                            var d = "{\"date\":\"" + DateTime.Now + "\",\"description\":\"@description\",\"title\":\"@title\",\"img\":\"" + imagePath + "\",\"id\":\"@id\"}";
                            if (info.Customer == null)
                            {
                                d = d.Replace("@description", info.Description).Replace("@title", info.Title).Replace("@id", "All");
                                client.UploadString(url, "POST", d);
                            }
                            else
                            {
                                foreach (var custid in info.Customer)
                                {
                                    try
                                    {
                                     string newBody = d.Replace("@description", info.Description).Replace("@title", info.Title).Replace("@id", custid);
                                       var response =  client.UploadString(url, "POST", newBody);
                                    }
                                    catch(Exception e)
                                    {
                                        //{date:"13-07-2020 12:45:36",description:"Freshlo test",title:"Freshlo test",img:"",id:"CI0139",}
                                        //{"date":"13-07-2020 12:45:36","description":"Freshlo test","title":"Freshlo test","img":"","id":"CI0139"}
                                        //
                                    }

                                }
                            }
                            var image = "" + imagePath + "";
                            TempData["ErrorMessage"] = image;
                            return RedirectToAction("Create", new { value = image });
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }



            return RedirectToAction("Create", "Notification");
        }

        public string FilePath()
        {
            string folderName = "Documents\\";
            var rootFolder = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(rootFolder, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            return newPath;
        }

        public IActionResult CreateNew()
        {
            return View();
        }

        // Based On Condition Check
        [HttpGet]
        public async Task<JsonResult> TriggerCustomerList(int a, int b)
        {
            try
            {
                if (a == 0)
                {
                    return Json(await _notificationSI.GetCustomerListTrigger(a, b));

                }
                else
                {
                    return Json(await _notificationSI.GetCustomerContactListTrigger(a, b));

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}