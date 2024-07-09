using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Freshlo.Web.Controllers
{
    public class DeliveryAppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OrderDetail()
        {
            return View();
        }
    }
}
