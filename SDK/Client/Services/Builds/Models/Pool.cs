namespace AzureDevops.Client.Services.Builds.Models
{
    public class Pool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHosted { get; set; }
    }
}