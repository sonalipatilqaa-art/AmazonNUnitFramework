using NUnit.Framework;
using AmazonTests.Pages;

namespace AmazonTests.Tests
{
    public class AmazonTests : ExtentTestBase
    {
        [Test]
        public void Test_SearchProduct()
        {
            Assert.That(driver, Is.Not.Null, "WebDriver instance is null.");
            Assert.That(driver, Is.Not.Null, "WebDriver instance is null.");
            var home = new HomePage(driver);
            home.Search("laptop");
            Assert.That(driver.Title, Does.Contain("laptop"));
        }

        [Test]
        public void Test_OpenFirstProduct()
        {
            Assert.That(driver, Is.Not.Null, "WebDriver instance is null.");
            var home = new HomePage(driver);
            home.Search("headphones");
            var results = new SearchResultsPage(driver);
            results.OpenFirstProduct();
            Assert.That(driver.Title, Does.Contain("Headphones").IgnoreCase);
        }

        [Test]
        public void Test_AddToCart()
        {
            Assert.That(driver, Is.Not.Null, "WebDriver instance is null.");
            var home = new HomePage(driver);
            home.Search("mouse");
            var results = new SearchResultsPage(driver);
            results.OpenFirstProduct();
            var product = new ProductPage(driver);
            product.AddToCart();
            Assert.That(product.GetCartCount(), Is.Not.EqualTo("0"));
        }

        [Test]
        public void Test_ViewCart()
        {
            var home = new HomePage(driver);
            home.Search("keyboard");
            var results = new SearchResultsPage(driver);
            results.OpenFirstProduct();
            var product = new ProductPage(driver);
            product.AddToCart();

            driver.Navigate().GoToUrl("https://www.amazon.com/gp/cart/view.html");
            var cart = new CartPage(driver);
            Assert.That(cart.GetCartItemsCount(), Is.GreaterThan(0));
        }

        [Test]
        public void Test_SignInPage()
        {
            Assert.That(driver, Is.Not.Null, "WebDriver instance is null.");
            driver.Navigate().GoToUrl("https://www.amazon.com/ap/signin");
            var signIn = new SignInPage(driver);
            Assert.That(signIn.IsEmailFieldDisplayed(), Is.True);
        }
    }
}