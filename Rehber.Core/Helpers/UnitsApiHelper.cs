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
    public class UnitsApiHelper
    {
        string ServiceUrl = "http://localhost:61310/api/Units/";

        public IEnumerable<UnitViewModel> GetAllUnits()
        {
            using (WebClient httpClient = new WebClient())
            {
                var jsonData = httpClient.DownloadString(ServiceUrl + "GetAllUnits");
                var data = JsonConvert.DeserializeObject<IEnumerable<UnitViewModel>>(jsonData);
                return data;
            }

        }

        public UnitViewModel GetById(int unitId)
        {
            using (WebClient httpClient = new WebClient())
            {
                try
                {
                    var jsonData = httpClient.DownloadString(ServiceUrl + "GetUnitById?id=" + unitId);
                    var data = JsonConvert.DeserializeObject<UnitViewModel>(jsonData);
                    return data;
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }

        public async Task<Units> AddNewUnit(string unitName, string parentName)
        {
            UnitViewModel unitViewModel = new UnitViewModel();
            unitViewModel.UnitName = unitName;
            unitViewModel.ParentName = parentName;
            using (HttpClient httpClient = new HttpClient())
            {
                var stringData = JsonConvert.SerializeObject(unitViewModel);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(ServiceUrl, contentData).Result;
                var jsonString = await response.Content.ReadAsStringAsync();
                var responseUnit = JsonConvert.DeserializeObject<Units>(jsonString);
                return responseUnit;
            }
        }
        public string DeleteUnit(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.DeleteAsync(ServiceUrl + "DeleteUnit?id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return "Done";
                }
                return "Not Done";
            }
        }

    }
}
