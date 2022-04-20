using OpenQA.Selenium;

namespace Hotel.Booking.Website.Tests.Common.Interfaces
{
    internal interface IBookingForms
    {
        IWebElement firstNameInput { get; set; }

        IWebElement lastNameInput { get; set; }

        IWebElement totalPriceInput { get; set; }

        IWebElement depositPaidInput { get; set; }

        IWebElement checkInInput { get; set; }

        IWebElement checkOutInput { get; set; }

        IWebElement saveInput { get; set; }

        IWebElement? deleteButton { get; set; }

        void AddFirstName(string firstName);

        void AddLastName(string lastName);

        void AddTotalPrice(string totalPrice);

        void AddDepositPaid(string depositPaid);

        void SelectCheckIn(string selectedDate);

        void SelectCheckOut(string selectedDate);

        void SelectSaveInput();

        void DeleteBooking(string bookingId);
    }
}
