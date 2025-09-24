using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System;
using System.IO;

namespace AmazonTests
{
    [SetUpFixture]
    public class ExtentReportSetup
    {
        public static ExtentReports extent;
        public static ExtentTest test;
    private static AventStack.ExtentReports.Reporter.ExtentSparkReporter htmlReporter;
        public static string ReportPath;

        [OneTimeSetUp]
        public void ReportSetup()
        {
            var dir = TestContext.CurrentContext.WorkDirectory;
            ReportPath = Path.Combine(dir, $"AmazonTestReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");
            htmlReporter = new AventStack.ExtentReports.Reporter.ExtentSparkReporter(ReportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [OneTimeTearDown]
        public void ReportTearDown()
        {
            extent.Flush();
        }
    }

    // Removed incorrect internal ExtentHtmlReporter class. Use the one from AventStack.ExtentReports.Reporter
}
