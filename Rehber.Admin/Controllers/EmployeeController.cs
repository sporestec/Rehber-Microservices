using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rehber.Core.Helpers;
using Rehber.Model.DataModels;
using Rehber.Model.SearchModels;
using Rehber.Model.ViewModels;

namespace Rehber.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeesApiHelper _employeesApiHelper = null;
        ElasticsearchApiHelper _elasticsearchApiHelper = null;

        public EmployeeController()
        {
            _employeesApiHelper = new EmployeesApiHelper();
            _elasticsearchApiHelper = new ElasticsearchApiHelper();
            _employeesApiHelper.DeleteEmployee(4);
        }


        public IActionResult EmployeeMainPage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddEmployee([FromBody]Employees employee)
        {
            var result = _employeesApiHelper.AddEmployee(employee);
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetWithFilter([FromQuery]EmployeesSearch filter)
        {
            var result = _elasticsearchApiHelper.SearchEmployees(filter);
            return Json(result);
        }

    }
}