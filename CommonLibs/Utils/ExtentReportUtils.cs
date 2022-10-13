using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace CommonLibs.Utils
{
    public class ExtentReportUtils
    {
        private ExtentHtmlReporter extentHtmlReporter;

        private ExtentReports extentReports;

        private ExtentTest extentTest;

        public ExtentReportUtils(string htmlReportFilename)
        {
            extentHtmlReporter = new ExtentHtmlReporter(htmlReportFilename);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(extentHtmlReporter);
        }

        public void createATestCase(string testcaseName)
        {
            extentTest = extentReports.CreateTest(testcaseName);
        }

        public void addTestLog(Status status, string comment)
        {
            extentTest.Log(status, comment);
        }

        public void addScreenshot(string screenshotFilename)
        {
            extentTest.AddScreenCaptureFromPath(screenshotFilename);
        }

        public void flushReport()
        {
            extentReports.Flush();
        }
    }
}