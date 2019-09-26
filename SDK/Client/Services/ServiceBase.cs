using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly.Retry;
using System.Net;
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
            this._httpClient = httpClient;
            this._asyncRetryPolicy = asyncRetryPolicy;
        }

        protected async Task<Result<T>> Get<T>(string url)
        {
            return await this._asyncRetryPolicy.ExecuteAsync(async () =>
            {
                var response = await this._httpClient.GetAsync(url);
                return await this.GetRequestContent<T>(response);
            });
        }

        protected async Task<Result<T>> Post<T>(string url, object request)
            => await this.Post<T, object>(url, request);

        protected async Task<Result<T>> Post<T, R>(string url, R request)
        {
            return await this._asyncRetryPolicy.ExecuteAsync(async () =>
            {
                var content = this.GetHttpContent<R>(request);
                var response = await this._httpClient.PostAsync(url, content);
                return await this.GetRequestContent<T>(response);
            });
        }

        private async Task<Result<T>> GetRequestContent<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.Forbidden)
                    return new Result<T>(true, response.StatusCode.ToString());
                else
                    response.EnsureSuccessStatusCode();
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(json);
            return new Result<T>(data);
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
