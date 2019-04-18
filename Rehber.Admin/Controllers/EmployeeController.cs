using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Rehber.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult EmployeeMainPage()
        {
            return View();
        }
    }
}