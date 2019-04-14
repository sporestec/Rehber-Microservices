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
    public class UnitsController : ControllerBase
    {

        ElasticsearchClient _elasticClient;

        public UnitsController()
        {
            _elasticClient = new ElasticsearchClient();
        }

        // PUT
        [HttpPut]
        public IActionResult Put([FromBody] Units unit)
        {
            if (unit.UnitId > 0)
            {
               _elasticClient.IndexUnit(unit);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // GET
        [HttpGet , Route("{unitId}")]
        public ActionResult<IEnumerable<string>> Get(int unitId)
        {
            var unit = _elasticClient.GetUnitById(unitId);
            if (unit != null)
            {
                return Ok(unit);
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
