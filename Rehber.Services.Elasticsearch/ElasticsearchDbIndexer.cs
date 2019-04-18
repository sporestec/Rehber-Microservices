using Rehber.Data.DbContexts;
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

        public void IndexAllEmployeesAndUnits() {
            RehberUnitServiceDbContext unitsContext = new RehberUnitServiceDbContext();
            RehberEmployeeServiceDbContext employeesContext = new RehberEmployeeServiceDbContext();

            var units = unitsContext.Units.ToList();
            var employees = employeesContext.Employees.ToList();

            foreach (var employee in employees)
            {
                var unit = units.Where(r => r.UnitId == employee.UnitId).FirstOrDefault();
                var view = employee.ToViewModel(unit?.UnitName);
                _elasticsearchClient.IndexEmployee(view);
            }
            foreach (var unit in units)
            {
                _elasticsearchClient.IndexUnit(unit);
            }

        }

        public void IndexAllEmployees()
        {
            RehberEmployeeServiceDbContext context = new RehberEmployeeServiceDbContext();
            var employees = context.Employees.ToList();
            foreach (var employee in employees)
            {
                _elasticsearchClient.IndexEmployee(employee.ToViewModel(null));
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
