using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Transdirect.Models;

namespace Transdirect.Api
{
    public interface ITransdirectApiService
    {
        [Get("/couriers")]
        Task<IDictionary<string, object>> GetCouriers();
        
        [Post("/quotes")]
        Task<Quote> GetQuote([Body] Quote quote);

        [Post("/bookings/v4")]
        Task<Booking> CreateBooking([Body] Booking booking);
        
        [Get("/bookings/v4")]
        Task<IEnumerable<Booking>> GetBookings(string since = null, string sort = null);

        [Get("/bookings/v4/{id}")]
        Task<Booking> GetSingleBooking([AliasAs("id")] int bookingId);

        [Put("/bookings/v4/{id}")]
        Task<Booking> UpdateBooking([AliasAs("id")] int bookingId, [Body] Booking booking);
        
        [Delete("/bookings/v4/{id}")]
        Task DeleteBooking([AliasAs("id")] int bookingId);

        [Post("/bookings/v4/{id}/confirm")]
        Task ConfirmBooking([AliasAs("id")] int bookingId, [Body] BookingConfirmation confirmation);

        [Get("/bookings/track/v4/{id}")]
        Task<string> TrackBooking([AliasAs("id")] int bookingId);

        [Get("/bookings/track/v4/{id}/items")]
        Task<IEnumerable<Item>> GetBookingItems([AliasAs("id")] int bookingId);

        [Post("/bookings/track/v4/{id}/items")]
        Task<IEnumerable<Item>> AddItemToBooking([AliasAs("id")] int bookingId, [Body] Item item);

        [Get("/bookings/track/v4/{id}/items/{item_id}")]
        Task<Item> GetBookingSingleItem([AliasAs("id")] int bookingId, [AliasAs("item_id")] int itemId);

        [Put("/bookings/track/v4/{id}/items/{item_id}")]
        Task<Item> UpdateBookingItem([AliasAs("id")] int bookingId, [AliasAs("item_id")] int itemId, [Body] Item item);

        [Delete("/bookings/track/v4/{id}/items/{item_id}")]
        Task RemoveBookingItem([AliasAs("id")] int bookingId, [AliasAs("item_id")] int itemId);

        [Get("/member")]
        Task<Member> GetMember();
    }
}