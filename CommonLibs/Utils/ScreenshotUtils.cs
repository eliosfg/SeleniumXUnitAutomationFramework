using OpenQA.Selenium;
using System;
using System.IO;

namespace CommonLibs.Utils
{
    public class ScreenshotUtils
    {
        ITakesScreenshot camera;

        public ScreenshotUtils(IWebDriver driver)
        {
            camera = (ITakesScreenshot) driver;
        }

        public void CaptureAndSaveScreenshot(string filename)
        {
            filename = filename.Trim();

            if (File.Exists(filename))
            {
                throw new Exception("Filename already exists");
            }

            Screenshot screenshot = camera.GetScreenshot();
            screenshot.SaveAsFile(filename);
        }
    }
}
