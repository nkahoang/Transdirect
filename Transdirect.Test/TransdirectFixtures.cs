using Transdirect.Models;

namespace Transdirect.Test
{
    public class TransdirectFixtures
    {
        public static Quote NewQuoteObject() {
            return new Quote() {
                DeclaredValue = 1000.00M,
                Items = new [] {
                    new Item() {
                        Weight = 38.63,
                        Height = 0.25,
                        Width = 1.65,
                        Length = 3.32,
                        Quantity = 1,
                        Description = ItemDescription.Carton,
                    },
                    new Item() {
                        Weight = 39.63,
                        Height = 1.25,
                        Width = 2.65,
                        Length = 4.32,
                        Quantity = 2,
                        Description = ItemDescription.Carton
                    },
                },
                Sender = new Transaddress() {
                    Postcode = "2000",
                    Suburb = "SYDNEY",
                    Type = AddressType.Business,
                    Country = "AU",
                },
                Receiver = new Transaddress() {
                    Postcode = "3000",
                    Suburb = "MELBOURNE",
                    Type = AddressType.Business,
                    Country = "AU",
                },
            };
        }
        public static Booking NewBookingObject() {
            return new Booking() {
                Referrer = "API",
                TailgatePickup = true,
                TailgateDelivery = true,
                DeclaredValue = 1000.00M,
                Items = new [] {
                    new Item() {
                        Weight = 38.63,
                        Height = 0.25,
                        Width = 1.65,
                        Length = 3.32,
                        Quantity = 1,
                        Description = ItemDescription.Carton,
                    },
                    new Item() {
                        Weight = 39.63,
                        Height = 1.25,
                        Width = 2.65,
                        Length = 4.32,
                        Quantity = 2,
                        Description = ItemDescription.Carton
                    },
                },
                Sender = new Transaddress() {
                    Address = "21 Kirksway Place",
                    CompanyName = "Test Company Name",
                    Email = "sender@test.com",
                    Name = "Sender Name",
                    Phone = "123456789",
                    Postcode = "2000",
                    Suburb = "SYDNEY",
                    State = "NSW",
                    Type = AddressType.Business,
                    Country = "AU",
                },
                Receiver = new Transaddress() {
                    Address = "216 Moggill Rd",
                    CompanyName = "Test Receiving Company",
                    Email = "receiver@test.com",
                    Name = "John Smith",
                    Postcode = "3000",
                    Phone = "123456789",
                    Suburb = "MELBOURNE",
                    Type = AddressType.Business,
                    Country = "AU",
                },
            };
        }
    }
}