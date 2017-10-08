using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Transdirect.Models
{
    public class Item
    {
        public int? Id { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Quantity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ItemDescription Description { get; set; }
    }
}