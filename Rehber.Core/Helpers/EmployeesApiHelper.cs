using Newtonsoft.Json;
using Rehber.Model.DataModels;
using Rehber.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rehber.Core.Helpers
{
    public class EmployeesApiHelper
    {
        private readonly static string URL = "http://localhost:2000/api/Employees/";


        public async Task<Employees> AddEmployee(Employees employee)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var stringData = JsonConvert.SerializeObject(employee);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(URL, contentData).Result;
                var jsonString = await response.Content.ReadAsStringAsync();
                var responseEmployee = JsonConvert.DeserializeObject<Employees>(jsonString);
                return responseEmployee;
            }
        }

        public string DeleteEmployee(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.DeleteAsync($"{URL}{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return "Done";
                }
                return "Not Done";
            }
        }

        public EmployeeViewModel GetEmployeeById(int EmployeeId)
        {
            using (WebClient httpClient = new WebClient())
            {
                try
                {
                    var jsonData = httpClient.DownloadString(URL + EmployeeId);
                    var data = JsonConvert.DeserializeObject<EmployeeViewModel>(jsonData);
                    return data;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public async Task<Employees> EditEmployee(EmployeeViewModel employee)
        {
            if (employee != null)
            {
                Employees edtEmployee = new Employees()
                {
                    Email = employee.Email,
                    EmployeeId = employee.EmployeeId,
                    UnitId = employee.UnitId,
                    ExtraInfo = employee.ExtraInfo,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    TelephoneNumber = employee.TelephoneNumber,
                    WebSite = employee.WebSite
                };
                using (HttpClient httpClient = new HttpClient())
                {
                    var stringData = JsonConvert.SerializeObject(edtEmployee);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    var response = httpClient.PutAsync(URL, contentData).Result;
                    Employees editedPersonnel = new Employees();
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var responseUnit = JsonConvert.DeserializeObject<Employees>(jsonString);
                    return responseUnit;
                }

            }
            else
            {
                return null;
            }
        }

    }
}
