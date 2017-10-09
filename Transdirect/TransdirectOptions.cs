using System;

namespace Transdirect
{
    public class TransdirectOptions
    {
        public string BaseUrl { get; set; } = "https://www.transdirect.com.au/api";
        public string ApiKey { get; set; }
        public TimeSpan CouriersCacheDuration { get; set; } = new TimeSpan(0, 5, 0);
    }
}