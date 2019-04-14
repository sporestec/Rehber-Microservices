using Rehber.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rehber.Model.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public int UnitId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UnitName { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string ExtraInfo { get; set; }
    }

    public static class EmployeeViewModelExtensions
    {
        public static EmployeeViewModel ToViewModel(this Employees item, string unitName)
        {
            EmployeeViewModel view = new EmployeeViewModel
            {
                UnitName = unitName,
                Email = item.Email,
                EmployeeId = item.EmployeeId,
                ExtraInfo = item.ExtraInfo,
                FirstName = item.FirstName,
                LastName = item.LastName,
                TelephoneNumber = item.TelephoneNumber,
                UnitId = item.UnitId,
                WebSite = item.WebSite
            };
            return view;
        }
    }
}
