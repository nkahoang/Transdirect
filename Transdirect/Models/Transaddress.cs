using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Transdirect.Converter;

namespace Transdirect.Models
{
    public class Transaddress
    {
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Postcode { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string Suburb { get; set; }
        public string Country { get; set; } = "AU";

        [JsonConverter(typeof(LowercaseStringEnumConverter))]

        public AddressType Type { get; set; }
    }
}