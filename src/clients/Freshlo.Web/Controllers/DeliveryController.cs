using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Freshlo.Web.Controllers
{
    public class DeliveryController : Controller
    {
        public IActionResult Track()
        {
            return View();
        }
    }
}