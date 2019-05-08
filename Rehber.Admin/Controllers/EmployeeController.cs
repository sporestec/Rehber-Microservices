using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rehber.Core.Helpers;
using Rehber.Data.DbContexts;
using Rehber.Model.DataModels;
using Rehber.Model.SearchModels;
using Rehber.Model.ViewModels;

namespace Rehber.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeesApiHelper _employeesApiHelper = null;
        ElasticsearchApiHelper _elasticsearchApiHelper = null;
       ImageApiHelper imageApiHelper = null;

        public EmployeeController()
        {
            _employeesApiHelper = new EmployeesApiHelper();
            _elasticsearchApiHelper = new ElasticsearchApiHelper();
            imageApiHelper = new ImageApiHelper();
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
        [HttpPost]
        public async Task<ActionResult> AddEmployeeImage(IFormFile file, [FromQuery] string firstName, [FromQuery]string lastName, [FromQuery] int employeeId)
        {
            UserImages userImage = new UserImages();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                userImage.BinaryData = Convert.ToBase64String(stream.ToArray());
                userImage.EmployeeId = employeeId;
                userImage.EmployeeName = firstName + " " + lastName;
            }
            var result = imageApiHelper.AddEmployeeImage(userImage);
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetWithFilter([FromQuery]EmployeesSearch filter)
        {
            var result = _elasticsearchApiHelper.SearchEmployees(filter);
            return Json(result);
        }

        public JsonResult DeleteEmployee(int id)
        {
            var result = _employeesApiHelper.DeleteEmployee(id);
            return Json(result);
        }
        [HttpGet]
        public JsonResult GetEmployeeById([FromQuery]int employeeId)
        {
            var result = _employeesApiHelper.GetEmployeeById(employeeId);
            return Json(result);
        }
        [HttpPost]
        public IActionResult SaveEditing([FromBody] EmployeeViewModel edtEmployee)
        {
            var result = _employeesApiHelper.EditEmployee(edtEmployee);
            return Json(result);
        }
    }
}