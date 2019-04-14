using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using RestSharp.Serializers;
using System.IO;
using RestSharp.Serialization.Json;
using Rehber.Model.DataModels;

namespace Rehber.Core.Helpers
{
    public class ApiHelperRestsharp
    {
        RestClient client;
        public ApiHelperRestsharp(string baseApiAddress, string apiBearerToken = null)
        {
            client = new RestClient(baseApiAddress);
            client.AddHandler("application/json", () => new JsonDeserializer());

            if (apiBearerToken != null)
            {
                client.AddDefaultParameter("Authorization",
                    string.Format("Bearer " + apiBearerToken),
                    ParameterType.HttpHeader);
            }

        }
        private KeyValuePair<bool, object> GetRequestReturnValue<T>(IRestResponse response)
        {
            var deserializer = new JsonDeserializer();
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                List<object> errorResponse = JsonConvert.DeserializeObject<List<object>>(response.Content);
                return new KeyValuePair<bool, object>(false, errorResponse);
            }
            else
            {
                T returnModel = deserializer.Deserialize<T>(response);
                return new KeyValuePair<bool, object>(true, returnModel);
            }
        }


        #region Units
        public KeyValuePair<bool, object> units_api_get_by_id(int unitId)
        {
            IRestRequest request = new RestRequest("api/units/{unitId}", Method.GET);
            request.AddUrlSegment("unitId", unitId.ToString());
            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            RestRequestAsyncHandle handle = client.ExecuteAsync<Units>(request, r => { taskCompletion.SetResult(r); });
            return GetRequestReturnValue<Units>(taskCompletion.Task.Result);
        }
        #endregion
    }
}

