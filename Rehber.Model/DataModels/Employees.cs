using System;
using System.Collections.Generic;

namespace Rehber.Model.DataModels
{
    public class Employees
    {
        public int EmployeeId { get; set; }
        public int UnitId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string ExtraInfo { get; set; }
    }
}
