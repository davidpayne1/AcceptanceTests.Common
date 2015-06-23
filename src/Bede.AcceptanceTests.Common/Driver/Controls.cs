using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Bede.AcceptanceTests.Common.Driver
{
    public class Controls
    {
        public static IWebDriver CreateChromeDriver()
        {
            return new ChromeDriver();
        }

        public static IWebDriver CreateFireFoxDriver()
        {
            return new FirefoxDriver();
        }

        public static void QuitDriver(IWebDriver driver)
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
