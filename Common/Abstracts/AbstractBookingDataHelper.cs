using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Booking.Website.Tests.Common.Abstracts
{
    internal abstract class AbstractBookingDataHelper
    {
        public List<int> bookingIds = new List<int>();

        public abstract Task SetBookingIds();

        public abstract Task<string> FindBookingID(string firstName, string lastName, string totalPrice, string depositPaid, string checkInDate, string checkoutDate);

        public abstract Task CleanUp();
    }
}
