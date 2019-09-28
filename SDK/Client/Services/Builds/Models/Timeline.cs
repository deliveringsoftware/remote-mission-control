using System;
using System.Collections.Generic;

namespace AzureDevops.Client.Services.Builds.Models
{
    public class Timeline
    {
        public IEnumerable<TimelineRecord> Records { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime LastChangedOn { get; set; }
        public string Id { get; set; }
        public int ChangeId { get; set; }
        public string Url { get; set; }
    }

    public class TimelineRecord
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public TimeSpan Duration => this.FinishTime - this.StartTime;
        public int? PercentComplete { get; set; }
        public TimelineRecordState State { get; set; }
        public TaskResult Result { get; set; }
        public int ChangeId { get; set; }
        public DateTime LastModified { get; set; }
        public string WorkerName { get; set; }
        public int Order { get; set; }
        public int ErrorCount { get; set; }
        public int WarningCount { get; set; }
        public Log Log { get; set; }
        public TaskReference Task { get; set; }
        public int Attempt { get; set; }
        public string Identifier { get; set; }
        public IEnumerable<Issue> Issues { get; set; }
    }

    public class Issue
    {
        public string Category { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public IssueType Type { get; set; }
    }

    public enum IssueType
    {
        Error,
        Warning
    }

    public class Log
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }

    public class TaskReference
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
    }

    public enum TimelineRecordState
    {
        Completed,
        InProgress,
        Pending
    }

    public enum TaskResult
    {
        Abandoned,
        Canceled,
        Failed,
        Skipped,
        Succeeded,
        SucceededWithIssues
    }
}