using AzureDevops.Client.Services.Definitions.Models;
using System.Threading.Tasks;

namespace AzureDevops.Client.Services.Definitions
{
    public interface IDefinitions
    {
        Task<Result<Items<Definition>>> ListAll(string projectName);
    }
}
