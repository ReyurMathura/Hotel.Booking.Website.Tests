using Hotel.Booking.Website.Tests.API;
using Hotel.Booking.Website.Tests.Common.Abstracts;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hotel.Booking.Website.Tests.Helpers
{
    internal class BookingDataHelper : AbstractBookingDataHelper
    {
        private Controllers controllers = new Controllers();

        public override async Task CleanUp()
        {
            await SetBookingIds();

            foreach (var bookingId in bookingIds)
            {
                await controllers.DeleteBookingId(bookingId.ToString());
            }
        }

        public override async Task<string> FindBookingID(string firstName, string lastName, string totalPrice, string depositPaid, string checkInDate, string checkoutDate)
        {
            await SetBookingIds();

            foreach (var bookingId in bookingIds)
            {
                var jsonResponseBody = JsonDocument.Parse(await controllers.GetBookingId(bookingId.ToString()));

                var jsonRootElement = jsonResponseBody.RootElement;

                var fn = jsonRootElement.GetProperty("firstname").ToString();
                var ln = jsonRootElement.GetProperty("lastname").ToString();
                var tp = jsonRootElement.GetProperty("totalprice").ToString();
                var dp = jsonRootElement.GetProperty("depositpaid").ToString();
                var ci = jsonRootElement.GetProperty("bookingdates").GetProperty("checkin").ToString();
                var co = jsonRootElement.GetProperty("bookingdates").GetProperty("checkout").ToString();

                if (fn.Equals(firstName) && ln.Equals(lastName) && tp.Equals(totalPrice) && dp.Equals(depositPaid) && ci.Equals(checkInDate) && co.Equals(checkoutDate))
                {
                    return bookingId.ToString();
                }
            }
            return string.Empty;
        }

        public override async Task SetBookingIds()
        {
            bookingIds.Clear();

            var jsonResponseBody = JsonDocument.Parse(await controllers.GetAllBookings());

            for (int i = 0; i < jsonResponseBody.RootElement.GetArrayLength(); i++)
            {
                var jsonRootElement = jsonResponseBody.RootElement[i];

                var bookingId = jsonRootElement.GetProperty("bookingid");

                bookingIds.Add(int.Parse(bookingId.ToString()));
            }
        }
    }
}
