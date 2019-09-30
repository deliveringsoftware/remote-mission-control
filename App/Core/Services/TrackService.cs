using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;

namespace AzureDevops.Services
{
    internal class TrackService : ITrackService
    {
        public void Event(string name, IDictionary<string, string> properties = null)
            => Analytics.TrackEvent(name, properties);

        public void Error(Exception exception, IDictionary<string, string> properties = null)
            => Crashes.TrackError(exception, properties);
    }
}