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
using Rehber.Data.Contexts;

namespace Rehber.Services.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IBus mybus;
        RehberEmployeeServiceDbContext db = new RehberEmployeeServiceDbContext();
        private readonly IRequestClient<IEmployeeAdded, IEmployeeAdded> _requestClient;

        public UsersController(IRequestClient<IEmployeeAdded, IEmployeeAdded> requestClient, IBus busbus)
        {
            mybus = busbus;
            _requestClient = requestClient;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var PersonnelList = db.Employees.ToList();
            return new JsonResult(PersonnelList);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employees employee)
        {
            if (employee != null)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                await mybus.Publish<IEmployeeAdded>(new { Employee = employee });
                return Ok(employee);
            }
            return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                await mybus.Publish<AddUser>(new { UserId = 32 });

                //await _requestClient.Request(new { UserId = id }, cancellationToken);

                return Accepted(33);
            }
            catch (RequestTimeoutException exception)
            {
                return StatusCode((int)HttpStatusCode.RequestTimeout);
            }
        }
    }
}
