using System.Threading.Tasks;

namespace Hotel.Booking.Website.Tests.Common.Interfaces
{
    internal interface ITestBase
    {
        abstract Task SetupAsync();
        abstract void TearDown();
    }
}
