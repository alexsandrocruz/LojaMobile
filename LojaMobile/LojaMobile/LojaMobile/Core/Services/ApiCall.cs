using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LojaMobile.Core.Services
{
    public class ApiCall
    {
        static readonly string ApiUrl = "http://loja-mobile.azurewebsites.net/api/{0}";

        public async Task<T> GetResponse<T>(string url) where T : class
        {
            var client = new System.Net.Http.HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(string.Format(ApiUrl, url));

            var jsonResult = response.Content.ReadAsStringAsync().Result;
            
            var rootobject = JsonConvert.DeserializeObject<T>(jsonResult);

            return rootobject;
        }

        public async Task<T> Post<T>(string url, T objeto) where T : class
        {
            var client = new System.Net.Http.HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var serializado = JsonConvert.SerializeObject(objeto);

            HttpContent content = new StringContent(serializado, Encoding.UTF8, "text/json");

            var response = await client.PostAsync(string.Format(ApiUrl, url), content);

            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var rootobject = JsonConvert.DeserializeObject<T>(jsonResult);

            return rootobject;
        }

        public async Task<string> Delete(string url, int id)
        {
            var client = new System.Net.Http.HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.DeleteAsync(string.Format(ApiUrl, $"{url}/{id}"));

            var result = response.Content.ReadAsStringAsync().Result;

            return result;
        }

        public async Task<T> Update<T>(string url, int id, T objeto) where T : class
        {
            var client = new System.Net.Http.HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var serializado = JsonConvert.SerializeObject(objeto);

            HttpContent content = new StringContent(serializado, Encoding.UTF8, "text/json");

            var response = await client.PutAsync(string.Format(ApiUrl, $"{url}/{id}"), content);

            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var rootobject = JsonConvert.DeserializeObject<T>(jsonResult);

            return rootobject;
        }
    }
}
