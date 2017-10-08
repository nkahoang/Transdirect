using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Transdirect.Converter;

namespace Transdirect.Models
{
    public class ServiceDetails
    {
        public decimal Total { get; set; }
        public decimal PriceInsuranceEx { get; set; }
        public decimal Fee { get; set; }
        public decimal InsuredAmount { get; set; }
        public string Service { get; set; }
        public string TransitTime { get; set; }
        public IEnumerable<DateTime> PickupDates { get; set; }
        public TimeRange PickupTime { get; set; }
    }
}