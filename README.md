Overview
This project is a C# Selenium WebDriver framework using NUnit for automated testing of the Amazon website. It includes a Page Object Model (POM) structure, ExtentReports integration for detailed HTML reports, and screenshot capture on test failures.
Features
Fully automated test cases for Amazon (at least 5 main pages)
Page Object Model (POM) design
NUnit framework for structured tests
ExtentReports for HTML reports
Screenshot capture for failed steps
Cross-browser support via Selenium WebDriver
CI/CD ready (can integrate with GitHub Actions or Azure DevOps)
Project Structure
AmazonTests/
│
├─ Drivers/            # WebDriver factory
├─ Pages/              # Page Object classes (Home, Product, Cart, SearchResults, SignIn)
├─ Tests/              # NUnit test cases
├─ bin/                # Build artifacts (ignored in Git)
├─ obj/                # Build metadata (ignored in Git)
├─ BaseTest.cs         # Base class for setup/teardown
├─ ExtentReportSetup.cs # ExtentReports initialization
├─ AmazonTests.csproj
└─ .gitignore
Installation
Clone the repository
git clone https://github.com/sonalipatilqaa-art/AmazonNUnitFramework.git
cd AmazonNUnitFramework
Open in Visual Studio / VS Code (Mac M3 supported)
Install NuGet packages
dotnet restore
Required NuGet packages:
Selenium.WebDriver
Selenium.Support
NUnit
NUnit3TestAdapter
ExtentReports
WebDriverManager
Run tests
dotnet test
How to Use
Tests are in the Tests folder.
Each test automatically generates an ExtentReport in the working directory with screenshots.
Page objects are in the Pages folder for easy maintenance.
Example Test
[Test]
public void Test_SearchAndOpenFirstProduct()
{
    var homePage = new HomePage(driver);
    homePage.SearchProduct("Laptop");

    var searchResults = new SearchResultsPage(driver);
    searchResults.OpenFirstProduct();

    var productPage = new ProductPage(driver);
    Assert.IsTrue(productPage.IsProductTitleDisplayed());
}
Notes
Make sure Chrome or your preferred browser is installed.
WebDriverManager automatically downloads the required ChromeDriver.
.gitignore excludes all bin and obj folders to keep the repo clean.
Contributing
Fork the repository
Create your branch: git checkout -b feature/xyz
Commit your changes: git commit -m "Add feature xyz"
Push to the branch: git push origin feature/xyz
Open a Pull Request
