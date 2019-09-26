using AzureDevops.Client.Services.Definitions.Models;
using AzureDevops.Client.Services.Projects.Models;
using System;
using System.Linq;

namespace AzureDevops.Client.Services.Builds.Models
{
    public class Build
    {
        public int Id { get; set; }
        public string BuildNumber { get; set; }
        public Status Status { get; set; }
        public Result Result { get; set; }
        public DateTime QueueTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public TimeSpan Duration => this.FinishTime - this.StartTime;
        public string Url { get; set; }
        public Definition Definition { get; set; }
        public int BuildNumberRevision { get; set; }
        public Project Project { get; set; }
        public string Uri { get; set; }
        public string SourceBranch { get; set; }
        public string SourceVersion { get; set; }
        public Queue Queue { get; set; }
        public Priority Priority { get; set; }
        public Reason Reason { get; set; }
        public Person RequestedFor { get; set; }
        public Person RequestedBy { get; set; }
        public DateTime LastChangedDate { get; set; }
        public Person LastChangedBy { get; set; }
        public Repository Repository { get; set; }
        public bool KeepForever { get; set; }
        public bool RetainedByRelease { get; set; }
        public object TriggeredByBuild { get; set; }
        public string BuildSourceInfo => this.CreateBuildSourceInfo();

        private string CreateBuildSourceInfo()
        {
            var parts = this.SourceBranch.Split('/');
            var branchName = parts.Last();
            var versionShort = String.Empty;
            if (!string.IsNullOrEmpty(this.SourceVersion))
                versionShort = this.SourceVersion.Substring(0, 7);
            return $"{branchName} {versionShort}";
        }
    }
}