using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Hotel.Booking.Website.Tests.Common.Abstracts;
using Hotel.Booking.Website.Tests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;

namespace Hotel.Booking.Website.Tests.TestBases
{
    internal class OneTimeTestBase : AbstractOneTimeTestBase
    {
        public IWebDriver driver = new ChromeDriver();
        public Screenshot? screenshot;

        public BookingDataHelper bookingDataHelper = new BookingDataHelper();
        public BookingFormsHelper? bookingFormsHelper;
        
        public ExtentTest? testReport;
        public ExtentHtmlReporter? htmlReporter;
        public ExtentReports? extent;

        public override async Task SetupAsync()
        {
            await bookingDataHelper.CleanUp();

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://hotel-test.equalexperts.io/");

            bookingFormsHelper = new BookingFormsHelper(driver);
            ReportingSetup();
        }

        public override void TearDown()
        {
            driver.Quit();
            extent?.Flush();
        }

        public void ReportingSetup()
        {
            //Current issue with extent report where name is always deaulted to index.html https://github.com/extent-framework/extentreports-csharp/issues/132
            var reportPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "TestReport.html";
            htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Browser", "Chrome");
            extent.AddSystemInfo("Host", "PC");
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Config\\ExtentConfig.xml";
            htmlReporter.LoadConfig(xmlPath);
        }

        public void AddScreenshot(string fileName)
        {
            screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + fileName, ScreenshotImageFormat.Png);
            testReport?.AddScreenCaptureFromPath(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + fileName);
        }
    }
}
