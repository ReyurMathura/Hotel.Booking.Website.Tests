using Hotel.Booking.Website.Tests.Common.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Hotel.Booking.Website.Tests.Helpers
{
    internal class BookingFormsHelper : IBookingForms
    {
        private IWebDriver driver;
        public BookingFormsHelper(IWebDriver driver)
        {
            this.driver = driver;

            firstNameInput = driver.FindElement(By.Id("firstname"));
            lastNameInput = driver.FindElement(By.Id("lastname"));
            totalPriceInput = driver.FindElement(By.Id("totalprice"));
            depositPaidInput = driver.FindElement(By.Id("depositpaid"));
            checkInInput = driver.FindElement(By.Id("checkin"));
            checkOutInput = driver.FindElement(By.Id("checkout"));
            saveInput = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/div[7]/input"));
        }

        public IWebElement firstNameInput { get; set; }
        public IWebElement lastNameInput { get; set; }
        public IWebElement totalPriceInput { get; set; }
        public IWebElement depositPaidInput { get; set; }
        public IWebElement checkInInput { get; set; }
        public IWebElement checkOutInput { get; set; }
        public IWebElement saveInput { get; set; }
        public IWebElement? deleteButton { get; set; }

        public void AddDepositPaid(string depositPaid)
        {
            var selectElement = new SelectElement(depositPaidInput);
            selectElement.SelectByText(depositPaid);
        }

        public void AddFirstName(string firstName)
        {
            firstNameInput.Click();
            firstNameInput.SendKeys(firstName);
        }

        public void AddLastName(string lastName)
        {
            lastNameInput.Click();
            lastNameInput.SendKeys(lastName);
        }

        public void AddTotalPrice(string totalPrice)
        {
            totalPriceInput.Click();
            totalPriceInput.SendKeys(totalPrice);
        }

        public void SelectCheckIn(string xPathToDate)
        {
            checkInInput.Click();
            var checkInDatePicker = driver.FindElement(By.XPath(xPathToDate));
            checkInDatePicker.Click();

        }

        public void SelectCheckOut(string xPathToDate)
        {
            checkOutInput.Click();
            var checkOutDatePicker = driver.FindElement(By.XPath(xPathToDate));
            checkOutDatePicker.Click();
        }

        public void SelectSaveInput()
        {
            saveInput.Click();
        }

        public void DeleteBooking(string bookingId)
        {
            var deleteBtn = driver.FindElement(By.XPath("//*[@id=" + bookingId + "]/div[7]/input"));
            deleteBtn.Click();
        }
    }
}
