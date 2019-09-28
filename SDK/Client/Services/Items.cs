using Newtonsoft.Json;
using System.Collections.Generic;

namespace AzureDevops.Client.Services
{
    public class Items<T>
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("value")]
        public IEnumerable<T> Value { get; set; }
    }
}