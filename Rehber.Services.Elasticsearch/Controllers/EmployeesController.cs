using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Rehber.Model.DataModels;

namespace Rehber.Services.Elasticsearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        ElasticsearchClient _elasticClient;

        public EmployeesController()
        {
            _elasticClient = new ElasticsearchClient();
        }

        // PUT
        [HttpPut]
        public void Put([FromBody] Employees employee)
        {
            if (employee.EmployeeId > 0)
            {
                _elasticClient.IndexEmployee(employee);
            }
        }

        // GET
        [HttpGet, Route("{employeeId}")]
        public ActionResult<IEnumerable<string>> Get(int employeeId)
        {
            var employee = _elasticClient.GetEmployeeById(employeeId);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound();
        }

        // GET
        [HttpGet, Route("unit/{unitId}")]
        public ActionResult<IEnumerable<string>> GetByUnitId(int unitId)
        {
            var employees = _elasticClient.GetEmployeesByUnitId(unitId);
            if (employees != null)
            {
                return Ok(employees);
            }
            return NotFound();
        }

        // GET
        [HttpGet, Route("{name}/unit/{unitId}")]
        public ActionResult<IEnumerable<string>> GetByNameAndUnitId(string name, int unitId)
        {
            var employees = _elasticClient.GetEmployeesByNameAndUnitId(name, unitId);
            if (employees != null && employees.Count > 0)
            {
                return Ok(employees);
            }
            return NotFound();
        }

    }
}
