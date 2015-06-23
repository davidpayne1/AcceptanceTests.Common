using System;
using Bede.AcceptanceTests.Common.Driver;
using OpenQA.Selenium;

namespace Bede.AcceptanceTests.Common.Driver
{
    public static class Extensions
    {
        public static IWebElement FindElement(this IWebDriver driver, PageElement pageElement)
        {
            try
            {
                Console.WriteLine("Searching for element {0} by {1}...", pageElement.Locator, pageElement.LocatorType);
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
                        Console.WriteLine("{0} is not a valid locator type.", pageElement.LocatorType);
                        throw new NoSuchElementException();
                }
                Console.WriteLine("Element {0}  was successfully located by its {1}.", pageElement.Locator,
                    pageElement.LocatorType);
                return foundElement;
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element {0} was NOT located by its {1}.", pageElement.Locator,
                    pageElement.LocatorType);
                throw;
            }
        }
    }
}
