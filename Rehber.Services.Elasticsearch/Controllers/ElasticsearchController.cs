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
    public class ElasticsearchController : ControllerBase
    {

        Elastic _elasticClient;

        public ElasticsearchController()
        {
            _elasticClient = new Elastic();
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
        [HttpGet , Route("{userId}")]
        public ActionResult<IEnumerable<string>> Get(int employeeId)
        {
            var employee = _elasticClient.GetEmployeeById(employeeId);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound();
        }



        //// GET
        //[HttpGet("UserId")]
        //public ActionResult<IEnumerable<string>> GetFiltered(int UserId)
        //{
        //    var request = new SearchRequest
        //    {
        //        From = 0,
        //        Size = 10,
        //        Query = new TermQuery { Field = "UserId", Value = UserId }

        //        /*
        //                 ||
        //                new MatchQuery { Field = "Name", Query = "NAME" }
        //        */
        //    };

        //    var response = _client.Search<Users>(request);
        //    return new JsonResult(response);
        //}

    }
}
