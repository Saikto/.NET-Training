using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace TestsLibrary.SOLR
{
    public class SolrWorker
    {
        private static string SolrUri = "https://platformservices.azure-api.net";

        private static HttpResponseMessage SendSearchRequest(string request)
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(SolrUri);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var requestUri = "/search/generic";
                response = client.PostAsync(requestUri, content).Result;
            }
            return response;
        }

        private static string GetResponseContent(HttpResponseMessage response)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        public static SearchResponse GetSearchResults(string request)
        {
            var jsonRequest = JObject.Parse(request);
            HttpResponseMessage response = SendSearchRequest(request);
            var json = JObject.Parse(GetResponseContent(response));
            SearchResponse jsonSearchResponse = json.ToObject<SearchResponse>();
            return jsonSearchResponse;
        }
    }
}
