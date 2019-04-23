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
        static string Url = "http://localhost:4000/api/Employees/";
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
                WebClient httpClient = new WebClient();
                var jsonData = httpClient.DownloadString(Url + searchViewModel.employeeName + "/unit/" + searchViewModel.unitId);
                var data = JsonConvert.DeserializeObject<IEnumerable<EmployeeViewModel>>(jsonData);
                return Json(data);
            }
            catch (Exception)
            {
                return Json("");
                throw;
            }


        }
    }
}