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

    }
}
