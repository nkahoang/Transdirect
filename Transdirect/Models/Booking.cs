using System;
using System.Collections.Generic;

namespace Transdirect.Models
{
    public class Booking: Quote
    {
        public int? Id { get; set; }
        public string Referrer { get; set; }
        public string RequestingSite { get; set; }
        public DateTime BookedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public bool TailgatePickup { get; set; }
        public bool TailgateDelivery { get; set; }
        public Notification Notifications { get; set; }
        public string Connote { get; set; }
        public double ChargedWeight { get; set; }
        public double ScannedWeight { get; set; }
        public string SpecialInstructions { get; set; }
        public string Status { get; set; }
        public IEnumerable<DateTime> PickupWindow { get; set; }
    }
}