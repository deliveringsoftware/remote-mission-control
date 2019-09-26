using Newtonsoft.Json;

namespace AzureDevops.Client.Services.Builds.Models
{
    public enum Result
    {
        Canceled,
        Failed,
        None,
        PartiallySucceeded,
        Succeeded
    }
}
