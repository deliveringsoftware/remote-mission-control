namespace AzureDevops.Client.Services.Builds.Models
{
    public class Links
    {
        public Link Self { get; set; }
        public Link Web { get; set; }
        public Link SourceVersionDisplayUri { get; set; }
        public Link Timeline { get; set; }
        public Link Badge { get; set; }
        public Link Avatar { get; set; }
    }
}