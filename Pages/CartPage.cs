using OpenQA.Selenium;

namespace AmazonTests.Pages
{
    public class CartPage
    {
        private readonly IWebDriver driver;
        private By cartItems = By.CssSelector(".sc-list-item");

        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public int GetCartItemsCount()
        {
            return driver.FindElements(cartItems).Count;
        }
    }
}
