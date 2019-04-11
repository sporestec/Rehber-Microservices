using Newtonsoft.Json;
using Rehber.Model.DataModels;
using Rehber.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rehber.WebApps.Admin.Core
{
    public class UnitCrudWithApi
    {
        static string Url = "https://localhost:44349/api/Units/";
        public IEnumerable<UnitViewModel> GetAllUnits()
        {
            using (WebClient httpClient = new WebClient())
            {
                var jsonData = httpClient.DownloadString(Url + "GetAllUnits");
                var data = JsonConvert.DeserializeObject<IEnumerable<UnitViewModel>>(jsonData);
                return data;
            }

        }

        public string DeleteUnit(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.DeleteAsync(Url + "DeleteUnit?id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return "Done";
                }
                return "Not Done";

            }

        }

        public UnitViewModel GetUnitById(int id)
        {
            using (WebClient httpClient = new WebClient())
            {
                var jsonData = httpClient.DownloadString(Url + "GetUnitById?id=" + id);
                var data = JsonConvert.DeserializeObject<UnitViewModel>(jsonData);
                return data;
            }
        }

        public async Task<Units> EditUnit(UnitViewModel unitViewModel)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var stringData = JsonConvert.SerializeObject(unitViewModel);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PutAsync(Url + unitViewModel.UnitId, contentData).Result;
                Units editedUnit = new Units();
                var jsonString = await response.Content.ReadAsStringAsync();
                var responseUnit = JsonConvert.DeserializeObject<Units>(jsonString);
                editedUnit.UnitId = responseUnit.UnitId;
                editedUnit.ParentId = responseUnit.ParentId;
                editedUnit.UnitName = responseUnit.UnitName;
                return editedUnit;
            }
        }

        public async Task<Units> AddUnit(string unitName, string parentName)
        {
            UnitViewModel unitViewModel = new UnitViewModel();
            unitViewModel.UnitName = unitName;
            unitViewModel.ParentName = parentName;
            using (HttpClient httpClient = new HttpClient())
            {
                var stringData = JsonConvert.SerializeObject(unitViewModel);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(Url, contentData).Result;
                var jsonString = await response.Content.ReadAsStringAsync();
                var responseUnit = JsonConvert.DeserializeObject<Units>(jsonString);
                return responseUnit;
            }
        }
    }
}
