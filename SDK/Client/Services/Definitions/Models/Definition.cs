using AzureDevops.Client.Services.Projects.Models;

namespace AzureDevops.Client.Services.Definitions.Models
{
    public class Definition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Project Project { get; set; }
    }
}