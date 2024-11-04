using ControllerAPI_1721030861.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ControllerAPI_1721030861.Consume
{
    public class ConsumeAPI : IConsumeAPI
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly Uri _baseAddress;
        public ConsumeAPI(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            string urlApi = _configuration["ApiSettings:UrlApi"]!;
            _baseAddress = new Uri(urlApi);
            _httpClient = clientFactory.CreateClient();
        }

        public async Task<APIResponse> GetAsync(string endpoint, Dictionary<string, dynamic> dictPars, string accessToken = "")
        {
            APIResponse res = new APIResponse();
            string uriApi = _baseAddress.ToString() + endpoint;
            if (dictPars != null)
            {
                string parameters = "?";
                int i = 0;
                foreach (KeyValuePair<string, dynamic> item in dictPars)
                {
                    parameters += (i == 0 ? "" : "&") + string.Format("{0}={1}", item.Key, item.Value == null ? "" : item.Value.ToString());
                    i++;
                }
                uriApi += parameters;
            }

            if (!string.IsNullOrEmpty(accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await _httpClient.GetAsync(uriApi);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    res = JsonConvert.DeserializeObject<APIResponse>(result!)!;
                }
            }
            return res;
        }

        public async Task<APIResponse> DeleteAsync(string endpoint, Dictionary<string, dynamic> dictPars, string accessToken = "")
        {
            // Authorization.
            if (!string.IsNullOrEmpty(accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            APIResponse res = new APIResponse();
            string uriApi = _baseAddress.ToString() + endpoint;
            if (dictPars != null)
            {
                string parameters = "?";
                int i = 0;
                foreach (KeyValuePair<string, dynamic> item in dictPars)
                {
                    parameters += (i == 0 ? "" : "&") + string.Format("{0}={1}", item.Key, item.Value == null ? "" : item.Value.ToString());
                    i++;
                }
                uriApi += parameters;
            }

            // Delete req.
            HttpResponseMessage response = await _httpClient.DeleteAsync(uriApi);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    res = JsonConvert.DeserializeObject<APIResponse>(result!)!;
                }
            }
            return res;
        }

        public async Task<APIResponse> PostAsync(string endpoint, Dictionary<string, dynamic> dictPars, string accessToken = "")
        {
            // Authorization.
            if (!string.IsNullOrEmpty(accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            APIResponse res = new APIResponse();
            string uriApi = _baseAddress.ToString() + endpoint;

            // Json body.
            string jsonBody = JsonConvert.SerializeObject(dictPars);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Delete req.
            HttpResponseMessage response = await _httpClient.PostAsync(uriApi, content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    res = JsonConvert.DeserializeObject<APIResponse>(result!)!;
                }
            }
            return res;
        }

        public async Task<APIResponse> PutAsync(string endpoint, Dictionary<string, dynamic> dictPars, string accessToken = "")
        {
            // Authorization.
            if (!string.IsNullOrEmpty(accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            APIResponse res = new APIResponse();
            string uriApi = _baseAddress.ToString() + endpoint;

            // Json body.
            string jsonBody = JsonConvert.SerializeObject(dictPars);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Delete req.
            HttpResponseMessage response = await _httpClient.PostAsync(uriApi, content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    res = JsonConvert.DeserializeObject<APIResponse>(result!)!;
                }
            }
            return res;
        }
    }
}
