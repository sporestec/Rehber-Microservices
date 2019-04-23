using System;
using System.Collections.Generic;
using System.Text;

namespace Rehber.Model.SearchModels
{
    public class EmployeesSearch
    {
        public string FirstName { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
