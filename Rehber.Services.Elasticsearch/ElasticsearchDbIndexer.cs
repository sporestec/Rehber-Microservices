using Rehber.Data.Contexts;
using Rehber.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rehber.Services.Elasticsearch
{
    public class ElasticsearchDbIndexer
    {
        ElasticsearchClient _elasticsearchClient;

        public ElasticsearchDbIndexer()
        {
            _elasticsearchClient = new ElasticsearchClient();
        }

        public void IndexAllEmployees() {
            RehberEmployeeServiceDbContext context = new RehberEmployeeServiceDbContext();
            var employees = context.Employees.ToList();
            foreach (var employee in employees)
            {
                _elasticsearchClient.IndexEmployee(employee);
            }
        }

        public void IndexAllUnits()
        {
            RehberUnitServiceDbContext context = new RehberUnitServiceDbContext();
            var units = context.Units.ToList();
            foreach (var unit in units)
            {
                _elasticsearchClient.IndexUnit(unit);
            }
        }

    }
}
