using System.Collections.Generic;

namespace Transdirect.Models
{
    public class Quote
    {
        public decimal DeclaredValue { get; set; }
        public decimal InsuredValue { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IDictionary<string, ServiceDetails> Quotes { get; set; }
        public Transaddress Sender { get; set; }
        public Transaddress Receiver { get; set; }
    }
}