using System;
using System.Collections.Generic;
using System.Text;

namespace Rehber.Model.SearchModels
{
    public class EmployeesSearch
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UnitName { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string ExtraInfo { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
