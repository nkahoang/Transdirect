using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Transdirect.Models;
using Xunit;

namespace Transdirect.Test
{
    public class TransdirectTest
    {
        ITransdirectService _transdirectService;
        public TransdirectTest() {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            var options = new TransdirectOptions();
            config.GetSection("Transdirect").Bind(options);

            _transdirectService = new TransdirectService(options);

            Log($"Transdirect initialised with API Key {options.ApiKey} with BaseURL {options.BaseUrl} and cache duration {options.CouriersCacheDuration}");
        }

        private void Log(string message) {
            System.Diagnostics.Debug.WriteLine(message);
            Console.WriteLine(message);
        }


        [Fact]
        public async Task GetCouriers()
        {
            Log($"== Test: querying all couriers");
            var couriers = await _transdirectService.GetCouriers();
            Assert.NotEmpty(couriers);
            couriers.ToList().ForEach(c => {
                Log($"Courier: Name {c.Name} | Group: {c.Group} | Value: {c.Value}");
                Assert.NotNull(c.Name);
                Assert.NotNull(c.Group);
                Assert.NotNull(c.Value);
            });
        }

        
        [Fact]
        public async Task GetQuote()
        {
            Log($"== Test: getting simple quote");
            
            var quote = await _transdirectService.GetQuote(TransdirectFixtures.NewQuoteObject());

            Assert.NotEmpty(quote.Quotes);

            quote.Quotes.ToList().ForEach(c => {
                Log($"Service: Name {c.Key} | Cost: {c.Value.Total} | Service: {c.Value.Service}");
                Assert.True(c.Value.Total > 0);
                Assert.NotEmpty(c.Value.PickupDates);
            });
        }

        
        [Fact]
        public async Task CreateBooking()
        {
            Log($"== Test: creating booking");
            
            var booking = await _transdirectService.CreateBooking(TransdirectFixtures.NewBookingObject());

            Assert.NotEmpty(booking.Quotes);
            Assert.NotNull(booking.Id);

            Log($"New booking created {booking.Id}");

            booking.Quotes.ToList().ForEach(c => {
                Log($"Service: Name {c.Key} | Cost: {c.Value.Total} | Service: {c.Value.Service}");
                Assert.True(c.Value.Total > 0);
                Assert.NotEmpty(c.Value.PickupDates);
            });
        }
        
        
        [Fact]
        public async Task CreateConfirmGetBooking()
        {
            Log($"== Test: creating booking then confirm and get");
            
            var newBooking = await _transdirectService.CreateBooking(TransdirectFixtures.NewBookingObject());

            Assert.NotEmpty(newBooking.Quotes);
            Assert.NotNull(newBooking.Id);

            var firstService = newBooking.Quotes.First();
            Assert.NotEmpty(firstService.Value.PickupDates);

            Log($"New booking created {newBooking.Id}. Confirming it using {firstService.Key} courier");

            await _transdirectService.ConfirmBooking(newBooking.Id.Value, new BookingConfirmation() {
                Courier = firstService.Key,
                PickupDate = firstService.Value.PickupDates.First(),
            });
            
            Log($"Getting booking again to make sure it's confirmed");
            var confirmedBooking = await _transdirectService.GetSingleBooking(newBooking.Id.Value);

            Assert.Equal(confirmedBooking.Id.Value, newBooking.Id.Value);
            Assert.NotEmpty(confirmedBooking.PickupWindow);

            Log($"Getting back all bookings placed 5 minutes ago, it should have this latest one");

            var bookings = await _transdirectService.GetBookings(DateTime.Now.AddMinutes(-5));
            
            Assert.NotEmpty(bookings);
            Assert.NotNull(bookings.FirstOrDefault(b => b.Id.Value == confirmedBooking.Id.Value));
        }
    }
}
