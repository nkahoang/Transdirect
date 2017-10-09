using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transdirect.Models;
namespace Transdirect
{
    public interface ITransdirectService
    {
        Task<IEnumerable<Courier>> GetCouriers(bool forceReload = false);
        Task<Quote> GetQuote(Quote quote);
        Task<Booking> CreateBooking(Booking booking);
        Task<IEnumerable<Booking>> GetBookings(DateTime? since = null, string sort = null);
        Task<Booking> GetSingleBooking(int bookingId);
        Task<Booking> UpdateBooking(int bookingId, Booking booking);
        Task DeleteBooking(int bookingId);
        Task ConfirmBooking(int bookingId, BookingConfirmation confirmation);
        Task<string> TrackBooking(int bookingId);
        Task<IEnumerable<Item>> GetBookingItems(int bookingId);
        Task<IEnumerable<Item>> AddItemToBooking(int bookingId, Item item);
        Task<Item> GetBookingSingleItem(int bookingId, int itemId);
        Task<Item> UpdateBookingItem(int bookingId, int itemId, Item item);
        Task RemoveBookingItem(int bookingId, int itemId);
        Task<Member> GetMember();
    }
}