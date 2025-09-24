using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AmazonTests.Pages
{
    public class SignInPage
    {
        private readonly IWebDriver driver;
    // Updated selector for Amazon UK sign-in page (may be the same, but add fallback)
    private By emailField = By.CssSelector("#ap_email, input[type='email']");

        public SignInPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool IsEmailFieldDisplayed()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(emailField));
            return driver.FindElement(emailField).Displayed;
        }
    }
}
