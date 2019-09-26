using AzureDevops.Client.Services.Projects.Models;
using System.Threading.Tasks;

namespace AzureDevops.Client.Services.Projects
{
    public interface IProjects
    {
        Task<Result<Items<Project>>> ListAll();
    }
}