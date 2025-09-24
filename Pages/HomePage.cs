using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AmazonTests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        private By searchBox = By.Id("twotabsearchtextbox");
        private By searchButton = By.Id("nav-search-submit-button");

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Search(string keyword)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var test = AmazonTests.ExtentReportSetup.test;

            // Handle 'Continue Shopping' button if present
            try
            {
                var continueShopping = By.XPath("//input[@aria-labelledby='attachSiNoCoverage-announce'] | //input[@name='proceedToRetailCheckout'] | //input[@data-action='a-button-action'] | //input[@value='Continue shopping']");
                wait.Timeout = TimeSpan.FromSeconds(3);
                if (driver.FindElements(continueShopping).Count > 0)
                {
                    driver.FindElement(continueShopping).Click();
                    CaptureStepScreenshot("ContinueShoppingClicked", test);
                }
            }
            catch (Exception ex) { test?.Warning($"Continue Shopping not present: {ex.Message}"); }
            finally { wait.Timeout = TimeSpan.FromSeconds(10); }

            // Handle Amazon cookie consent popup if present
            try
            {
                var cookieAccept = By.Id("sp-cc-accept");
                wait.Timeout = TimeSpan.FromSeconds(3);
                if (driver.FindElements(cookieAccept).Count > 0)
                {
                    driver.FindElement(cookieAccept).Click();
                    CaptureStepScreenshot("CookieAccepted", test);
                }
            }
            catch (Exception ex) { test?.Warning($"Cookie popup not present: {ex.Message}"); }
            finally { wait.Timeout = TimeSpan.FromSeconds(10); }

            wait.Until(ExpectedConditions.ElementIsVisible(searchBox));
            driver.FindElement(searchBox).SendKeys(keyword);
            CaptureStepScreenshot("SearchTextEntered", test);
            wait.Until(ExpectedConditions.ElementToBeClickable(searchButton));
            driver.FindElement(searchButton).Click();
            CaptureStepScreenshot("SearchClicked", test);
        }

        private void CaptureStepScreenshot(string step, AventStack.ExtentReports.ExtentTest test)
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var dir = TestContext.CurrentContext.WorkDirectory;
                string filePath = Path.Combine(dir, $"Step_{step}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                screenshot.SaveAsFile(filePath);
                test?.Info($"Screenshot: {step}").AddScreenCaptureFromPath(filePath);
            }
            catch { }
        }
    }
}
