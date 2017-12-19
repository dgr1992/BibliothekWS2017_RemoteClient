using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BibliothekWS2017_RemoteClient
{
    public class RestClient
    {
        private HttpClient _client;
        private String _url;

        /// <summary>
        /// Creates a new instance of a HTTP client for the given URL and the expected data type to be received
        /// </summary>
        /// <param name="url">URL to the web service. Example: http://test.at/test/ add a / at the end of the URL</param>
        /// <param name="dataType">Expected data type from the web service. Example: application/json </param>
        public RestClient(String url, String dataType)
        {
            _url = url;
            if(url[url.Length - 1] != '/'){
                _url +="/";
            }
            
            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(dataType));
        }

        /// <summary>
        /// Performs a put request at the web service
        /// </summary>
        /// <param name="path">name of the function to call</param>
        /// <param name="data">Data to send. Any object that can be converted to a JSON object</param>
        /// <returns>Returns the resived data as string</returns>
        public async Task<String> Put(String path, Object data)
        {
            String result = null;

            String jsonObject = JsonConvert.SerializeObject(data);

            //Perform the request
            HttpResponseMessage response = await _client.PostAsync(_url + path, new StringContent(jsonObject));
            //Check if request was successful and process the answer
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        /// <summary>
        /// Performs a get request at the web service
        /// </summary>
        /// <param name="path">name of the function</param>
        /// <returns>Returns the resived data as string</returns>
        public async Task<String> Get(String path)
        {
            String result = null;
            //Perform request
            HttpResponseMessage response = await _client.GetAsync(_url + path);
            //Check if request was successful and process the answer
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        public void SetAuthorizationHeader(String authType, String data){
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authType, data);
        }

        public void ClearAuthorizationHeader(){
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
