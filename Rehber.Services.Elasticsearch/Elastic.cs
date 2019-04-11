using Nest;
using Rehber.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rehber.Services.Elasticsearch
{
    public class Elastic
    {
        ElasticClient _client;

        public Elastic()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            _client = new ElasticClient(settings);
        }

        public void IndexEmployee(Employees employee)
        {
            var res = _client.Index(employee, idx => idx.Index("employeeid"));
        }

        public Employees GetEmployeeById(int employeeId)
        {

            var request = new SearchRequest
            {
                From = 0,
                Size = 10,
                Query = new TermQuery { Field = "employeeid", Value = employeeId }

                /*
                         ||
                        new MatchQuery { Field = "Name", Query = "NAME" }
                */
            };
            var response = _client.Search<Employees>(request);
            return response.Documents.FirstOrDefault() ;
        }

    }
}
