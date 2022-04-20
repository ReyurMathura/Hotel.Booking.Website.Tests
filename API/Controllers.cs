using Hotel.Booking.Website.Tests.Common.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hotel.Booking.Website.Tests.API
{
    internal class Controllers : IController
    {
        public HttpClient client => new HttpClient();

        public List<int> bookingIds => new List<int>();

        private readonly string URL = "http://hotel-test.equalexperts.io/booking/";

        public async Task DeleteBookingId(string bookingId)
        {
            var msg = new HttpRequestMessage(HttpMethod.Delete, URL + bookingId);
            msg.Headers.Add("Accept", "*/*");
            msg.Headers.Add("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");

            await client.SendAsync(msg);
        }

        public async Task<string> GetBookingId(string bookingId)
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, URL + bookingId);
            msg.Headers.Add("Accept", "*/*");

            var res = await client.SendAsync(msg);
            res.EnsureSuccessStatusCode();

            var responseBody = await res.Content.ReadAsStringAsync();

            return responseBody;
        }

        public async Task<string> GetAllBookings()
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, URL);

            var res = await client.SendAsync(msg);
            res.EnsureSuccessStatusCode();

            var responseBody = await res.Content.ReadAsStringAsync();

            return responseBody;
        }
    }
}
