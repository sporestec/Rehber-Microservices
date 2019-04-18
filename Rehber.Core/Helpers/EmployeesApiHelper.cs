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
        private readonly static string URL = "http://localhost:50484/api/Employees/";


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


        //public IEnumerable<UnitViewModel> GetAllUnits()
        //{
        //    using (WebClient httpClient = new WebClient())
        //    {
        //        var jsonData = httpClient.DownloadString(URL + "GetAllUnits");
        //        var data = JsonConvert.DeserializeObject<IEnumerable<UnitViewModel>>(jsonData);
        //        return data;
        //    }
        //}

        //public UnitViewModel GetById(int unitId)
        //{

        //    using (WebClient httpClient = new WebClient())
        //    {
        //        try
        //        {
        //            var jsonData = httpClient.DownloadString(URL + "GetUnitById?id=" + unitId);
        //            var data = JsonConvert.DeserializeObject<UnitViewModel>(jsonData);
        //            return data;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }
        //}


        //public async Task<Units> EditUnit(UnitViewModel unitViewModel)
        //{
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        var stringData = JsonConvert.SerializeObject(unitViewModel);
        //        var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
        //        var response = httpClient.PutAsync(URL + unitViewModel.UnitId, contentData).Result;
        //        Units editedUnit = new Units();
        //        var jsonString = await response.Content.ReadAsStringAsync();
        //        var responseUnit = JsonConvert.DeserializeObject<Units>(jsonString);
        //        editedUnit.UnitId = responseUnit.UnitId;
        //        editedUnit.ParentId = responseUnit.ParentId;
        //        editedUnit.UnitName = responseUnit.UnitName;
        //        return editedUnit;
        //    }
        //}

        //public string DeleteUnit(int id)
        //{
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        var response = httpClient.DeleteAsync(URL + "DeleteUnit?id=" + id).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return "Done";
        //        }
        //        return "Not Done";
        //    }
        //}

    }
}
