using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AmazonTests.Pages
{
    public class SearchResultsPage
    {
        private readonly IWebDriver driver;
    // Updated selector for Amazon UK: first product link in search results
    private By firstProduct = By.CssSelector("div.s-main-slot[data-component-type='s-search-result'] h2 a, div.s-main-slot .s-result-item h2 a");

        public SearchResultsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenFirstProduct()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var test = AmazonTests.ExtentReportSetup.test;
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(firstProduct));
                CaptureStepScreenshot("FirstProductClickable", test);
                driver.FindElement(firstProduct).Click();
                CaptureStepScreenshot("FirstProductClicked", test);
            }
            catch (Exception ex)
            {
                CaptureStepScreenshot("FirstProductError", test);
                test?.Fail($"Error clicking first product: {ex.Message}");
                throw;
            }
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
