using System.Net.Http;
using System.Threading.Tasks;

namespace Hotel.Booking.Website.Tests.Common.Interfaces
{
    internal interface IController
    {
        HttpClient client { get; }

        Task<string> GetAllBookings();

        Task<string> GetBookingId(string bookingId);

        Task DeleteBookingId(string bookingId);
    }
}
