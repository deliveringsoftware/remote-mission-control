using Newtonsoft.Json;

namespace AzureDevops.Client.Services.Builds.Models
{
    public class Person
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }

        public string Id { get; set; }
        public string UniqueName { get; set; }
        public string ImageUrl { get; set; }
        public string Descriptor { get; set; }
    }
}