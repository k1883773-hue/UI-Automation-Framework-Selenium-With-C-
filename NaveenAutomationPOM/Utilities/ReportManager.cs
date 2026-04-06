using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;

namespace NaveenAutomationPOM.Utilities
{
    public static class ReportManager
    {
        private static ExtentReports? extent;
        public static ExtentTest? test;

        public static void InitReport()
        {
            string reportPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Reports",
                "AutomationReport.html");

            var sparkReporter = new ExtentSparkReporter(reportPath);

            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);

            extent.AddSystemInfo("Tester", "Khushi");
            extent.AddSystemInfo("Framework", "Selenium C#");
        }

        public static void CreateTest(string testName)
        {
            test = extent.CreateTest(testName);
        }

        public static void FlushReport()
        {
            extent.Flush();
        }

        public static void CaptureScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                string folderPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "Screenshots");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath,
                    testName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png");

                screenshot.SaveAsFile(filePath);

                test?.Fail("Test Failed. Screenshot below:",
                    MediaEntityBuilder.CreateScreenCaptureFromPath(filePath).Build());
            }
            catch (Exception e)
            {
                test?.Fail("Screenshot capture failed: " + e.Message);
            }
        }
    }
}