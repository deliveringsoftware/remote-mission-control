using AzureDevops.Client.Services.Definitions.Models;
using Polly.Retry;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureDevops.Client.Services.Definitions
{
    internal class Definitions : ServiceBase, IDefinitions
    {
        public Definitions(HttpClient httpClient, AsyncRetryPolicy asyncRetryPolicy)
            : base(httpClient, asyncRetryPolicy) { }

        public async Task<Result<Items<Definition>>> ListAll(string projectName)
        {
            var url = $"{projectName}/_apis/build/definitions?api-version=5.1";
            return await this.Get<Items<Definition>>(url);
        }
    }
}
