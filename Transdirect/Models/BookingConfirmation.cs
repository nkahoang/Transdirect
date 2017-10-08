using System;
using Newtonsoft.Json;
using Transdirect.Converter;

namespace Transdirect.Models
{
    public class BookingConfirmation
    {
        public string Courier;

        [JsonProperty(PropertyName="pickup-date")]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime PickupDate;
    }
}