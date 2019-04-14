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

        [HttpPost("{command}")]
        public IActionResult Post(string command)
        {
            if (command == "REINDEX")
            {
                ElasticsearchDbIndexer esdi = new ElasticsearchDbIndexer();
                esdi.IndexAllEmployeesAndUnits();
                //esdi.IndexAllUnits();
            }
            return Ok();
        }


    }
}
