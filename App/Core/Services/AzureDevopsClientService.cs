using System;
using AzureDevops.Client;

namespace AzureDevops.Services
{
    public sealed class AzureDevopsClientService : IAzureDevopsClientService
    {
        private IAzureDevopsClient azureDevopsClient;
        public IAzureDevopsClient Client => azureDevopsClient;

        public void RegisterAzureDevopsClient(string organization, string personalAccessToken)
           => azureDevopsClient = AzureDevopsClientFactory.CreateUsingPersonalAccessToken(organization, personalAccessToken);
    }
}
