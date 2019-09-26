using AzureDevops.Client.Services;
using AzureDevops.Client.Services.Builds;
using AzureDevops.Client.Services.Definitions;
using AzureDevops.Client.Services.Projects;
using Polly;
using Polly.Retry;
using System;
using System.Net.Http;

namespace AzureDevops.Client
{
    internal class AzureDevopsClient : IAzureDevopsClient
    {
        private IProjects _projects;
        public IProjects Projects
        {
            get
            {
                if (this._projects == null)
                    this._projects = new Projects(this._httpClient, this._asyncRetryPolicy);
                return this._projects;
            }
        }

        private IBuilds _builds;
        public IBuilds Builds
        {
            get
            {
                if (this._builds == null)
                    this._builds = new Builds(this._httpClient, this._asyncRetryPolicy);
                return this._builds;
            }
        }

        private IDefinitions _definitions;
        public IDefinitions Definitions
        {
            get
            {
                if (this._definitions == null)
                    this._definitions = new Definitions(this._httpClient, this._asyncRetryPolicy);
                return this._definitions;
            }
        }

        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy _asyncRetryPolicy;
        public AzureDevopsClient(HttpClient httpClient, RetryPolicyConfiguration retryPolicyConfiguration)
        {
            this._httpClient = httpClient;
            this._asyncRetryPolicy = this.CreatePolicy(retryPolicyConfiguration);
        }

        private AsyncRetryPolicy CreatePolicy(RetryPolicyConfiguration retryPolicyConfiguration)
            => Policy.Handle<Exception>(e => true)
                     .WaitAndRetryAsync(retryCount: retryPolicyConfiguration.RetryCount,
                                        retryAttempt => TimeSpan.FromSeconds(retryPolicyConfiguration.RetryAttemptFactor * retryAttempt)
               );
    }
}