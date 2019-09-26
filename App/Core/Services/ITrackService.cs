using System;
using System.Collections.Generic;

namespace AzureDevops.Services
{
    public interface ITrackService
    {
        void Event(string name, IDictionary<string, string> properties = null);
        void Error(Exception exception, IDictionary<string, string> properties = null);
    }
}
