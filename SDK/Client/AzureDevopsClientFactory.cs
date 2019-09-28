using AzureDevops.Client.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AzureDevops.Client
{
    public static class AzureDevopsClientFactory
    {
        private static IAzureDevopsClient _azureDevopsClient;

        public static IAzureDevopsClient CreateUsingPersonalAccessToken(string organization,
                                                                        string personalAccessToken,
                                                                        RetryPolicyConfiguration retryPolicyConfiguration = null)
        {
            if (_azureDevopsClient == null)
            {
                var httpClient = CreateHttpClient(organization, personalAccessToken);
                _azureDevopsClient = new AzureDevopsClient(httpClient, retryPolicyConfiguration ?? RetryPolicyConfiguration.Default);
            }
            return _azureDevopsClient;
        }

        private static HttpClient CreateHttpClient(string organization, string personalAccessToken)
        {
            var baseUrl = $"https://dev.azure.com/{organization}/";
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.ConnectionClose = false;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = CreateAuthenticationHeaderValue(personalAccessToken);
            return client;
        }

        private static AuthenticationHeaderValue CreateAuthenticationHeaderValue(string personalAccessToken)
        {
            var byteArray = Encoding.ASCII.GetBytes($":{personalAccessToken}");
            var base64 = Convert.ToBase64String(byteArray);
            return new AuthenticationHeaderValue("Basic", base64);
        }
    }
}