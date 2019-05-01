using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Rehber.Model.DataModels;
using Rehber.Model.SearchModels;
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

        public void UpdateEmployee(EmployeeViewModel employee)
        {
            var del = _client.Delete<EmployeeViewModel>(employee.EmployeeId,
                     r => r.Index("employeeviewmodel")
                     );
            var ind = _client.Index<EmployeeViewModel>(employee, idx => idx
                    .Index("employeeviewmodel")
                    .Id(employee.EmployeeId)
                    );
        }

        public void IndexEmployee(EmployeeViewModel employee)
        {
            if (!_client.IndexExists(Indices.All, ii => ii.Index("employeeviewmodel")).Exists)
            {
                var result = _client.CreateIndex("employeeviewmodel", i => i
                //Settings
                .Settings(s => s
                .Analysis(a => a
                .TokenFilters(tf => tf.PatternCapture("email", pc => pc
                   .Patterns(new string[] { "([^@]+)", "(\\p{L}+)", "(\\d+)", "@(.+)", "([^-@]+)" })
                   .PreserveOriginal(true)))
                //Analyzers
                .Analyzers(an => an
                .Custom("email", cc => cc
                 .Tokenizer("uax_url_email")
                 .Filters(new string[] { "email", "lowercase", "unique" })
                ))))
                //Mappings
                .Mappings(m => m.Map("employeeviewmodel", mm => mm.Properties<EmployeeViewModel>(ps => ps.Text(
                      te => te.Name(P => P.Email).Analyzer("email")
                      )))));
            }
            var res = _client.Index<EmployeeViewModel>(employee, idx => idx
            .Index("employeeviewmodel")
            .Id(employee.EmployeeId)
            );
        }

        public void DeleteAllIndexes()
        {
            _client.DeleteIndex(Indices.All);
        }

        public void IndexUnit(Units unit)
        {
            var res = _client.Index(unit, idx => idx
            .Index("units")
            .Id(unit.UnitId)
            );
        }

        public List<EmployeeViewModel> GetFilteredEmployees(EmployeesSearch filter)
        {
            var res = _client.Search<EmployeeViewModel>(idx => idx
            .Index("employeeviewmodel")
            .From(filter.PageSize * (filter.PageNumber - 1))
            .Size(filter.PageSize)
            .Query(q =>
                q.Wildcard(m => m.Field("email").Value($"*{filter.Email}*"))
            &&
            q.Wildcard(m => m.Field("firstName").Value($"*{filter.FirstName}*"))
            &&
            q.Wildcard(m => m.Field("lastName").Value($"*{filter.LastName}*"))
            &&
            q.Wildcard(m => m.Field("unitName").Value($"*{filter.UnitName}*"))
            &&
            q.Wildcard(m => m.Field("webSite").Value($"*{filter.WebSite}*"))
            &&
            q.Wildcard(m => m.Field("extraInfo").Value($"*{filter.ExtraInfo}*"))
            &&
            q.Wildcard(m => m.Field("telephoneNumber").Value($"*{filter.TelephoneNumber}*"))
            )
            );
            var empls = res.Documents.ToList();
            return empls;
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

        public bool DeleteEmployee(int employeeId)
        {
            //var res = _client.DeleteIndex(Indices.All,
            //    f=>f.Index("employeeviewmodel")

            //    );
            var res = _client.Delete<EmployeeViewModel>(employeeId,
                r => r.Index("employeeviewmodel")
                );
            return res.IsValid;
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
