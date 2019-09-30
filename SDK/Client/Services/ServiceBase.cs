using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly.Retry;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureDevops.Client.Services
{
    public abstract class ServiceBase
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy _asyncRetryPolicy;

        public ServiceBase(HttpClient httpClient, AsyncRetryPolicy asyncRetryPolicy)
        {
            _httpClient = httpClient;
            _asyncRetryPolicy = asyncRetryPolicy;
        }

        protected async Task<Result<T>> Get<T>(string url)
        {
            return await _asyncRetryPolicy.ExecuteAsync(async () =>
            {
                var response = await _httpClient.GetAsync(url);
                return await GetRequestContent<T>(response);
            });
        }

        protected async Task<Result<T>> Post<T>(string url, object request)
            => await Post<T, object>(url, request);

        protected async Task<Result<T>> Post<T, R>(string url, R request)
        {
            return await _asyncRetryPolicy.ExecuteAsync(async () =>
            {
                var content = GetHttpContent<R>(request);
                var response = await _httpClient.PostAsync(url, content);
                return await GetRequestContent<T>(response);
            });
        }

        private async Task<Result<T>> GetRequestContent<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return new Result<T>(true, response.StatusCode.ToString());
            }

            try
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(json);
                return new Result<T>(data);
            }
            catch (System.Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex);
#endif
                return new Result<T>(true, ex.Message);
            }
        }

        private HttpContent GetHttpContent<T>(T content)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var json = JsonConvert.SerializeObject(content, serializerSettings);
            return new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        }
    }
}