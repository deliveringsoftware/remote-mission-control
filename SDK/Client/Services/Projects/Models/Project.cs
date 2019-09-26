using System;

namespace AzureDevops.Client.Services.Projects.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProjectVisibility Visibility { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
    }
}
