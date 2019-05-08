using System;
using System.Collections.Generic;

namespace Rehber.Model.DataModels
{
    public partial class UserImages
    {
        public int FotId { get; set; }
        public string BinaryData { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
