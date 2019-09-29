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
        private IProjects projects;

        public IProjects Projects
        {
            get
            {
                if (projects is null)
                    projects = new Projects(httpClient, asyncRetryPolicy);
                return projects;
            }
        }

        private IBuilds builds;

        public IBuilds Builds
        {
            get
            {
                if (builds is null)
                    builds = new Builds(httpClient, asyncRetryPolicy);
                return builds;
            }
        }

        private IDefinitions definitions;

        public IDefinitions Definitions
        {
            get
            {
                if (definitions is null)
                    definitions = new Definitions(httpClient, asyncRetryPolicy);
                return definitions;
            }
        }

        private readonly HttpClient httpClient;
        private readonly AsyncRetryPolicy asyncRetryPolicy;

        public AzureDevopsClient(HttpClient httpClient, RetryPolicyConfiguration retryPolicyConfiguration)
        {
            this.httpClient = httpClient;
            asyncRetryPolicy = CreatePolicy(retryPolicyConfiguration);
        }

        private AsyncRetryPolicy CreatePolicy(RetryPolicyConfiguration retryPolicyConfiguration)
            => Policy.Handle<Exception>(e => true)
                     .WaitAndRetryAsync(retryCount: retryPolicyConfiguration.RetryCount,
                                        retryAttempt => TimeSpan.FromSeconds(retryPolicyConfiguration.RetryAttemptFactor * retryAttempt)
               );
    }
}