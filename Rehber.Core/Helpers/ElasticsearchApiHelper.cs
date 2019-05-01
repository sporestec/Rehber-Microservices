using Newtonsoft.Json;
using Rehber.Core.Extensions;
using Rehber.Model.DataModels;
using Rehber.Model.SearchModels;
using Rehber.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rehber.Core.Helpers
{
    public class ElasticsearchApiHelper
    {
        private readonly static string URL = "http://localhost:1500/api/Employees";

        public List<EmployeeViewModel> SearchEmployees(EmployeesSearch filter)
        {
            string query = filter.ToQueryString();
            using (WebClient httpClient = new WebClient())
            {
                try
                {
                    var jsonData = httpClient.DownloadString($"{URL}?{query}");
                    var data = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(jsonData);
                    return data;
                }
               catch (Exception e)
                {
                    return null;
                }
            }
        }

    }
}
