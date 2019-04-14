using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Rehber.Model.DataModels;
using Rehber.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rehber.Services.Elasticsearch
{
    public class ElasticsearchClient
    {
        ElasticClient _client;

        public ElasticsearchClient()
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

            var settings = new ConnectionSettings(pool, (builtInSerializer, connectionSettings) =>
                new JsonNetSerializer(builtInSerializer, connectionSettings, () => new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

            _client = new ElasticClient(settings);
        }

        public void IndexEmployee(EmployeeViewModel employee)
        {
            var res = _client.Index<EmployeeViewModel>(employee, idx => idx
            .Index("employeeviewmodel")
            .Id(employee.EmployeeId)
            );
        }

        public void IndexUnit(Units unit)
        {
            var res = _client.Index(unit, idx => idx
            .Index("units")
            .Id(unit.UnitId)
            );
        }

        public Employees GetEmployeeById(int employeeId)
        {

            var request = new SearchRequest
            {
                From = 0,
                Size = 10,
                Query = new TermQuery { Field = "employeeId", Value = employeeId }
            };
            var response = _client.Search<Employees>(request);
            return response.Documents.FirstOrDefault();
        }

        public Units GetUnitById(int unitId)
        {
            var response = _client.Search<Units>(s => s
                .Index("units")
                .Query(q => q
                .Bool(b => b
                .Must(
                    e => e
                   .Term(t => t
                   .Field("unitId")
                   .Value(unitId)
                      ))))
            );
            return response.Documents.FirstOrDefault();
        }
        public List<EmployeeViewModel> GetEmployeesByNameAndUnitId(string name, int unitId)
        {
            List<string> unitIds = new List<string>();
            var unit = GetUnitById(unitId);

            if (unit == null)
            {
                unitIds.Add(0.ToString());
            }
            else
            {
                while (unit != null)
                {
                    unitIds.Add(unit.UnitId.ToString());
                    unit = unit.InverseParent?.FirstOrDefault();
                }
            }

            var response = _client.Search<EmployeeViewModel>(s => s
    .Index("employeeviewmodel")
    .Query(q => q
    .Bool(b => b
    .Must(m => m
       .Terms(t => t
       .Field(f => f.UnitId)
       .Terms(unitIds))

       && m.Wildcard(ma => ma.Field(f => f.FirstName).Value($"*{name.ToLower()}*"))
       || m.Wildcard(ma => ma.Field(f => f.LastName).Value($"*{name.ToLower()}*"))
       || m.Wildcard(ma => ma.Field(f => (f.FirstName + " " + f.LastName)).Value($"*{name.ToLower()}*")))
)));

            return response.Documents.ToList();

        }


        public List<EmployeeViewModel> GetEmployeesByUnitId(int unitId)
        {
            List<string> unitIds = new List<string>();
            var unit = GetUnitById(unitId);

            if (unit == null)
            {
                unitIds.Add(0.ToString());
            }
            else
            {
                while (unit != null)
                {
                    unitIds.Add(unit.UnitId.ToString());
                    unit = unit.InverseParent?.FirstOrDefault();
                }
            }


            //var request = new SearchRequest
            //{
            //    //From = 0,
            //    //Size = 10,
            //    Query = new BoolQuery
            //    {
            //        Must = new List<QueryContainer> {
            //            new TermsQuery
            //            {
            //                //Name = "named_query",
            //                //Boost = 1.1,
            //                Field = "unitId",
            //                Terms = unitIds
            //            }
            //        }

            //    } // || new MatchQuery { Field = "firstName", Query = "Taffr" }
            //};

            var response = _client.Search<EmployeeViewModel>(s => s
                .Index("employeeviewmodel")
                .Query(q => q
                .Bool(b => b
                .Must(m => m
                   .Terms(t => t
                   .Field(f => f.UnitId)
                   .Terms(unitIds))
                      )))
            );
            //var response = _client.Search<Employees>(request);
            return response.Documents.ToList();
        }
    }
}
