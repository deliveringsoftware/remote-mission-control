using System;

namespace AzureDevops.Client.Services.Builds.Models
{
    public class Change
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
        public string MessageTruncated { get; set; }
        public string Type { get; set; }
        public string Pusher { get; set; }
        public Person Author { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
