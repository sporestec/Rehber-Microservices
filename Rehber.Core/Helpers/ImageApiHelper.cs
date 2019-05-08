using Newtonsoft.Json;
using Rehber.Model.DataModels;
using Rehber.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rehber.Core.Helpers
{
    public class ImageApiHelper
    {
        private readonly static string URL = "http://localhost:3000/api/UserImages/";

        public async Task<UserImages> AddEmployeeImage(UserImages userImage)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                var stringData = JsonConvert.SerializeObject(userImage);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(URL, contentData).Result;
                var jsonString = await response.Content.ReadAsStringAsync();
                var responseUnit = JsonConvert.DeserializeObject<UserImages>(jsonString);
                return responseUnit;

            }
        }


        public UserImages GetFotoByEmployeeId(int EmployeeId)
        {
            using (WebClient httpClient = new WebClient())
            {
                try
                {
                    var jsonData = httpClient.DownloadString(URL + EmployeeId);
                    var data = JsonConvert.DeserializeObject<UserImages>(jsonData);
                    return data;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

    }
}
