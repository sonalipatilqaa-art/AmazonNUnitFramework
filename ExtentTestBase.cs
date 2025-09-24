using AventStack.ExtentReports;
using NUnit.Framework;
using System;
using System.IO;
using OpenQA.Selenium;

namespace AmazonTests
{
    public class ExtentTestBase : BaseTest
    {
        protected ExtentTest test;

        [SetUp]
        public void StartTest()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            test = ExtentReportSetup.extent.CreateTest(testName);
        }

        [TearDown]
        public void EndTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMsg = TestContext.CurrentContext.Result.Message;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            var screenshotPath = "";

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                // Take screenshot on failure
                try
                {
                    Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    var dir = TestContext.CurrentContext.WorkDirectory;
                    screenshotPath = Path.Combine(dir, $"Failure_{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                    screenshot.SaveAsFile(screenshotPath);
                    test.AddScreenCaptureFromPath(screenshotPath);
                }
                catch { }
                test.Fail($"Test failed: {errorMsg}").Fail(stackTrace);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                test.Pass("Test passed");
            }
            else
            {
                test.Warning($"Test finished with status: {status}");
            }
        }
    }
}
