using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rehber.Model;
using Rehber.Model.ViewModels;
using System.Net;
using Newtonsoft.Json;
using Rehber.Core.Helpers;

namespace Rehber.App.Controllers
{
    public class HomeController : Controller
    {
        UnitsApiHelper unitCrudWithApi = new UnitsApiHelper();
        ElasticsearchApiHelper ehelper = new ElasticsearchApiHelper();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllUnits()
        {
            var unitList = unitCrudWithApi.GetAllUnits();
            return Json(unitList);
        }


        [HttpPost]
        public JsonResult GetEmployee([FromBody]SearchViewModel searchViewModel)
        {
            try
            {
                return Json(ehelper.SearchByNameAndUnitId(searchViewModel));
            }
            catch (Exception)
            {
                return Json("");
            }
        }
    }
}