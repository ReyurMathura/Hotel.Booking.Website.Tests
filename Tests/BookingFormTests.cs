using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Hotel.Booking.Website.Tests.TestBases;
using AventStack.ExtentReports;

namespace Hotel.Booking.Website.Tests.Tests
{
    [TestFixture(Description = "Tests for adding and deleting bookings")]
    internal class BookingFormTests : OneTimeTestBase
    {
        [Test, Order(1), Description("Check that a new booking entry can be added")]
        public async Task GIVEN_no_data_exists_WHEN_booking_is_added_THEN_booking_entry_appears_in_list()
        {
            //Arrange
            testReport = extent?.CreateTest("GIVEN_no_data_exists_WHEN_booking_is_added_THEN_booking_entry_appears_in_list", "Check that a new booking entry can be added");

            bookingFormsHelper?.AddFirstName("YourName");
            bookingFormsHelper?.AddLastName("TestSurname");
            bookingFormsHelper?.AddTotalPrice("100");
            bookingFormsHelper?.AddDepositPaid("false");
            bookingFormsHelper?.SelectCheckIn("/html/body/div[2]/table/tbody/tr[5]/td[6]/a");
            bookingFormsHelper?.SelectCheckOut("/html/body/div[2]/table/tbody/tr[5]/td[7]/a");

            //Act
            bookingFormsHelper?.SelectSaveInput();

            var bookingId = await bookingDataHelper.FindBookingID("YourName", "TestSurname", "100", "False", "2022-04-29", "2022-04-30");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            var bookingEntry = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(bookingId)));

            AddScreenshot("BookingAdded.png");

            if (bookingEntry.Text.Equals("YourName\r\nTestSurname\r\n100\r\nfalse\r\n2022-04-29\r\n2022-04-30"))
            {
                //Assert
                bookingEntry.Text.Should().Be("YourName\r\nTestSurname\r\n100\r\nfalse\r\n2022-04-29\r\n2022-04-30");
                testReport?.Log(Status.Pass, bookingEntry.Text);
            }
            else
            {
                testReport?.Log(Status.Fail, bookingEntry.Text);
            }
        }

        [Test, Order(2), Description("Check that the newly added booking entry can be deleted")]
        public async Task GIVEN_booking_entry_exists_WHEN_Deleted_THEN_booking_no_longer_exists()
        {
            //Arrange
            testReport = extent?.CreateTest("GIVEN_booking_entry_exists_WHEN_Deleted_THEN_booking_no_longer_exists", "Check that the newly added booking entry can be deleted");

            var bookingId = await bookingDataHelper.FindBookingID("YourName", "TestSurname", "100", "False", "2022-04-29", "2022-04-30");

            //Act
            bookingFormsHelper?.DeleteBooking(bookingId);

            //Assert
            var deletedBookingId = await bookingDataHelper.FindBookingID("YourName", "TestSurname", "100", "False", "2022-04-29", "2022-04-30");

            AddScreenshot("BookingDeleted.png");

            if (deletedBookingId.Equals(string.Empty))
            {
                //Assert
                deletedBookingId.Should().Be(String.Empty);
                testReport?.Log(Status.Pass, $"Booking ID {bookingId} was deleted successfully");
            }
            else
            {
                testReport?.Log(Status.Fail, $"{deletedBookingId} has not been deleted");
            }
        }
    }
}
