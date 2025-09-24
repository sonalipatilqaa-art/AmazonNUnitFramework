using NUnit.Framework;
using OpenQA.Selenium;

namespace AmazonTests
{
    // Suppress NUnit1032 warning
    [System.Diagnostics.CodeAnalysis.SuppressMessage("NUnit.Analyzers", "NUnit1032:Field should be disposed", Justification = "Driver is disposed in TearDown.")]
    public class BaseTest
    {
        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = Drivers.DriverFactory.CreateDriver();
            driver.Navigate().GoToUrl("https://www.amazon.co.uk/");

            // Wait for page to load
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            // Click 'Continue Shopping' button if present
            try
            {
                var continueShopping = By.XPath("//input[@aria-labelledby='attachSiNoCoverage-announce'] | //input[@name='proceedToRetailCheckout'] | //input[@data-action='a-button-action'] | //input[@value='Continue shopping']");
                wait.Timeout = TimeSpan.FromSeconds(3);
                if (driver.FindElements(continueShopping).Count > 0)
                {
                    driver.FindElement(continueShopping).Click();
                }
            }
            catch { /* Ignore if not present */ }
            finally { wait.Timeout = TimeSpan.FromSeconds(10); }

            // Log current URL and title for debugging
            Console.WriteLine($"Navigated to: {driver.Url}");
            Console.WriteLine($"Page title: {driver.Title}");
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                //driver = null;
            }
        }
    }
}
