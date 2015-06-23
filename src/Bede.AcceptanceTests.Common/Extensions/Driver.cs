using System;
using Bede.AcceptanceTests.Common.Driver;
using Bede.AcceptanceTests.Common.PageElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Bede.AcceptanceTests.Common.Extensions
{
    public static class Driver
    {
        private const int PageElementTimeout = 10;

        public static IWebElement FindElement(this IWebDriver driver, PageElement pageElement)
        {
            try
            {
                LogSearchAttempt(pageElement);
                IWebElement foundElement;
                switch (pageElement.LocatorType)
                {
                    case PageElementLocatorTypes.ClassName:
                        foundElement = driver.FindElement(By.ClassName(pageElement.Locator));
                        break;
                    case PageElementLocatorTypes.Id:
                        foundElement = driver.FindElement(By.Id(pageElement.Locator));
                        break;
                    case PageElementLocatorTypes.XPath:
                        foundElement = driver.FindElement(By.XPath(pageElement.Locator));
                        break;
                    default:
                        LogInvalidLocatorType(pageElement);
                        throw new NoSuchElementException();
                }
                LogSearchSuccess(pageElement);
                return foundElement;
            }
            catch (NoSuchElementException)
            {
                LogSearchFailure(pageElement);
                throw;
            }
        }

        public static IWebElement TryFindElement(this IWebDriver driver, PageElement pageElement)
        {
            try
            {
                LogSearchAttempt(pageElement);
                return driver.FindElement(pageElement);
            }
            catch (NoSuchElementException)
            {
                LogSearchFailure(pageElement);
                return null;
            }
        }

        public static Func<IWebDriver, IWebElement> ElementIsClickable(PageElement pageElement)
        {
            return driver =>
            {
                var element = driver.TryFindElement(pageElement);
                try
                {
                    LogIsClickableAttempt(pageElement);
                    return (element != null && element.Displayed && element.Enabled) ? element : null;
                }
                catch (Exception)
                {
                    LogIsClickableFailure(pageElement);
                }
                return null;
            };
        }

        public static IWebElement FindAsyncElement(this IWebDriver driver, PageElement pageElement)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(PageElementTimeout));
            return wait.Until(drv => drv.TryFindElement(pageElement));
        }

        public static IWebElement FindClickableElement(this IWebDriver driver, PageElement pageElement)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(PageElementTimeout));
            wait.Until(ElementIsClickable(pageElement));

            return driver.FindElement(pageElement);
        }

        static void LogInvalidLocatorType(PageElement pageElement)
        {
            Console.WriteLine("{0} is not a valid locator type.", pageElement.LocatorType);
        }

        static void LogSearchAttempt(PageElement pageElement)
        {
            Console.WriteLine("Searching for the {0} using {1}: {2}...", 
                pageElement.FriendlyDescriptor, pageElement.LocatorType, pageElement.Locator);
        }

        static void LogSearchSuccess(PageElement pageElement)
        {
            Console.WriteLine("The {0}  was successfully located using {1}: {2}.",
                pageElement.FriendlyDescriptor, pageElement.LocatorType, pageElement.Locator);
        }

        static void LogSearchFailure(PageElement pageElement)
        {
            Console.WriteLine("Element {0} was NOT located by {1}: {2}.",
                pageElement.FriendlyDescriptor, pageElement.LocatorType, pageElement.Locator);
        }

        static void LogIsClickableAttempt(PageElement pageElement)
        {
            Console.WriteLine("Checking the {0} is clickable...", pageElement.FriendlyDescriptor);
        }

        static void LogIsClickableFailure(PageElement pageElement)
        {
            Console.WriteLine("The {0} was not clickable.", pageElement.FriendlyDescriptor);
        }
    }
}
