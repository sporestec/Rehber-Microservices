using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rehber.WebApps.Admin.Core;

namespace Rehber.WebApps.App.Controllers
{
    public class HomeController : Controller
    {
        UnitCrudWithApi unitCrudWithApi = new UnitCrudWithApi();
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllUnits()
        { 
            
                var unitList = unitCrudWithApi.GetAllUnits();
                return Json(unitList);
        }
    }
}