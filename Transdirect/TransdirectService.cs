using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Refit;
using Transdirect.Api;
using Transdirect.Models;
using Newtonsoft.Json;
using System.Linq;
using Transdirect.Resolver;
using Microsoft.Extensions.Caching.Memory;

namespace Transdirect
{
    public class TransdirectService : ITransdirectService
    {
        TransdirectOptions _options;
        ITransdirectApiService _apiService;
        IMemoryCache _cache;

        protected const string COURIER_CACHE_KEY = "TRANSDIRECT_COURIERS";

        public TransdirectService(TransdirectOptions options, IMemoryCache memoryCache = null)
        {
            _options = options;
            _cache = (memoryCache != null) ? memoryCache : new MemoryCache(new MemoryCacheOptions()
            {
            });

            var _client = new HttpClient()
            {
                BaseAddress = new Uri(_options.BaseUrl),
            };
            _client.DefaultRequestHeaders.Add("Api-key", _options.ApiKey);
            _apiService = RestService.For<ITransdirectApiService>(_client, new RefitSettings()
            {
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new SnakeCasePropertyNamesContractResolver()
                },
            });
        }

        public TransdirectService(IOptions<TransdirectOptions> options) : this(options.Value) { }

        public Task<Quote> GetQuote(Quote quote)
            => _apiService.GetQuote(quote);
        public Task<Booking> CreateBooking(Booking booking)
            => _apiService.CreateBooking(booking);
        public Task<IEnumerable<Booking>> GetBookings(DateTime? since = null, string sort = null)
            => _apiService.GetBookings(since?.ToUniversalTime().ToString("o"), sort);
        public Task<Booking> GetSingleBooking(int bookingId)
            => _apiService.GetSingleBooking(bookingId);
        public Task<Booking> UpdateBooking(int bookingId, Booking booking)
            => _apiService.UpdateBooking(bookingId, booking);
        public Task DeleteBooking(int bookingId)
            => _apiService.DeleteBooking(bookingId);
        public Task ConfirmBooking(int bookingId, BookingConfirmation confirmation)
            => _apiService.ConfirmBooking(bookingId, confirmation);
        public Task<string> TrackBooking(int bookingId)
            => _apiService.TrackBooking(bookingId);
        public Task<IEnumerable<Item>> GetBookingItems(int bookingId)
            => _apiService.GetBookingItems(bookingId);
        public Task<IEnumerable<Item>> AddItemToBooking(int bookingId, Item item)
            => _apiService.AddItemToBooking(bookingId, item);
        public Task<Item> GetBookingSingleItem(int bookingId, int itemId)
            => _apiService.GetBookingSingleItem(bookingId, itemId);
        public Task<Item> UpdateBookingItem(int bookingId, int itemId, Item item)
            => _apiService.UpdateBookingItem(bookingId, itemId, item);
        public Task RemoveBookingItem(int bookingId, int itemId)
            => _apiService.RemoveBookingItem(bookingId, itemId);
        public Task<Member> GetMember()
            => _apiService.GetMember();

        /// <summary>
        /// Getting all supported couriers.
        /// This method does cache the list of couriers and can the behaviour can be overwrote by setting forceReload to true
        /// </summary>
        /// <param name="forceReload">Whether to always force querying Couriers from Transdirect</param>
        /// <returns>All supported couriers in an IEnumerable</returns>
        public async Task<IEnumerable<Courier>> GetCouriers(bool forceReload = false)
        {
            IEnumerable<Courier> couriers;

            if (_cache.TryGetValue(COURIER_CACHE_KEY, out couriers) &&
                (couriers?.Any() ?? false))
            {
                return couriers;
            }

            var rawCouriers = await _apiService.GetCouriers();

            couriers = rawCouriers.SelectMany(kvp =>
            {
                try
                {
                    var courierOptions = JsonConvert.DeserializeObject<IDictionary<string, string>>(kvp.Value.ToString());
                    return courierOptions.Select(coptions => new Courier()
                    {
                        Name = $"{kvp.Key} - {coptions.Key}",
                        Group = kvp.Key,
                        Value = coptions.Value,
                    });
                }
                catch (Exception)
                {
                    // courier only have one option
                    return new[] { new Courier() {
                        Name = kvp.Key,
                        Group = kvp.Key,
                        Value = kvp.Value.ToString(),
                    } };
                }
            }).ToList();

            return _cache.Set(
                COURIER_CACHE_KEY,
                couriers,
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(_options.CouriersCacheDuration)
            );
        }
    }
}
