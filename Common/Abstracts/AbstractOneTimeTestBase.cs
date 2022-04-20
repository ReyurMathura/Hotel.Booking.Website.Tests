using Hotel.Booking.Website.Tests.Common.Interfaces;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Hotel.Booking.Website.Tests.Common.Abstracts
{
    internal abstract class AbstractOneTimeTestBase : ITestBase
    {
        [OneTimeSetUp]
        public abstract Task SetupAsync();

        [OneTimeTearDown]
        public abstract void TearDown();
    }
}
