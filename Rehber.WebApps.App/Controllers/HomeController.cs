using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rehber.WebApps.Admin.Core;
using Rehber.Model;
using Rehber.Model.ViewModels;
using System.Net;
using Newtonsoft.Json;

namespace Rehber.WebApps.App.Controllers
{
    public class HomeController : Controller
    {
        UnitCrudWithApi unitCrudWithApi = new UnitCrudWithApi();
        static string Url = "http://localhost:41024/api/Employees/";
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
            WebClient httpClient = new WebClient();
            var jsonData = httpClient.DownloadString(Url + searchViewModel.employeeName + "/unit/" + searchViewModel.unitId);
            var data = JsonConvert.DeserializeObject<IEnumerable<EmployeeViewModel>>(jsonData);
            return Json(data);

        }
    }
}