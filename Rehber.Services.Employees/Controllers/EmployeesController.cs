using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using Rehber.Model.ViewModels;
using Rehber.Model.MessageContracts;
using MassTransit;
using System.Net;
using System.Threading;
using Rehber.Model.DataModels;
using Rehber.Data.DbContexts;
using System.Net.Http;
using System.Runtime;
using Microsoft.Extensions.Configuration;
using Rehber.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Rehber.Services.EmployeesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly RehberEmployeeServiceDbContext _db = null;
        private readonly IRequestClient<IEmployeeAdded, IEmployeeAdded> _requestClient;
        private readonly IConfiguration _configuration;
        private readonly UnitsApiHelper _unitApiHelper;

        public EmployeesController(IRequestClient<IEmployeeAdded, IEmployeeAdded> requestClient, IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _db = new RehberEmployeeServiceDbContext();
            _configuration = configuration;
            _requestClient = requestClient;
            _unitApiHelper = new UnitsApiHelper();
        }

        [HttpGet("{employeeId}")]
        public ActionResult<IEnumerable<string>> Get(int employeeId)
        {
            try
            {
                var employee = _db.Employees.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
                if (employee != null)
                {
                    var unit = _unitApiHelper.GetById(employeeId);
                    if (unit != null)
                    {
                        return Ok(employee.ToViewModel(unit.UnitName));
                    }
                    return Ok(employee);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employees employee)
        {
            var unit = _unitApiHelper.GetById(employee.UnitId);
            if (employee != null)
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();

                await _bus.Publish<IEmployeeAdded>(new
                {
                    Employee = employee.ToViewModel(unit.UnitName)
                });

                return Ok(employee);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Employees employee)
        {
            if (employee != null)
            {
                //_db.Entry(employee).State = EntityState.Modified;
                _db.Employees.Update(employee);
                _db.SaveChanges();
                await _bus.Publish<IEmployeeUpdated>(new { Employee = employee });
                return Ok(employee);
            }
            return BadRequest();
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteAsync(int employeeId)
        {
            try
            {
                if (employeeId > 0)
                {
                    Employees employee = new Employees() { EmployeeId = employeeId };
                    _db.Employees.Attach(employee);
                    _db.Employees.Remove(employee);
                    _db.SaveChanges();
                    await _bus.Publish<IEmployeeDeleted>(new { EmployeeId = employeeId });
                }
                //await _requestClient.Request(new { UserId = id }, cancellationToken);
                return Ok();
            }
            catch (RequestTimeoutException exception)
            {
                return StatusCode((int)HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
