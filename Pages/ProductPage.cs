using OpenQA.Selenium;

namespace AmazonTests.Pages
{
    public class ProductPage
    {
        private readonly IWebDriver driver;
        private By addToCart = By.Id("add-to-cart-button");
        private By cartCount = By.Id("nav-cart-count");

        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void AddToCart()
        {
            driver.FindElement(addToCart).Click();
        }

        public string GetCartCount()
        {
            return driver.FindElement(cartCount).Text;
        }
    }
}
