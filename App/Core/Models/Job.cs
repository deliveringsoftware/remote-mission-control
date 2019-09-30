using AzureDevops.Client.Services.Builds.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevops.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        public int Depth { get; set; }
        public string Name { get; set; }
        public TimeSpan? Duration { get; set; }
        public int Order { get; set; }
        public TaskResult? Result { get; set; }
        public int? LogId { get; set; }

        public Job(TimelineRecord record, int depth)
        {
            Depth = depth;

            Id = record.Id;
            Name = record.Name;
            Duration = record.Duration;
            Order = record.Order;
            Result = record.Result;
            LogId = record.Log?.Id;
        }

        public static IList<Job> CreateJobs(IEnumerable<TimelineRecord> records)
        {
            var jobs = new List<Job>();

            void CreateJobs(Guid? id, int depth)
            {
                var items = records.Where(r => r.ParentId == id)
                                   .OrderBy(r => r.Order);

                foreach (var item in items)
                {
                    //????
                    if (!item.Name.Equals("__default", StringComparison.InvariantCultureIgnoreCase)
                        && !item.Name.Equals("Checkpoint", StringComparison.InvariantCultureIgnoreCase))
                    {
                        jobs.Add(new Job(item, depth));
                    }

                    CreateJobs(item.Id, depth + 1);
                }
            }

            CreateJobs(null, 0);

            return jobs;
        }
    }
}