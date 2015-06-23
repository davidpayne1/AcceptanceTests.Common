namespace Bede.AcceptanceTests.Common.Driver
{
        public class PageElement
        {
            public string Locator { get; private set; }
            public PageElementLocatorTypes LocatorType { get; private set; }

            public PageElement(string elementLocator, PageElementLocatorTypes elementLocatorType)
            {
                Locator = elementLocator;
                LocatorType = elementLocatorType;
            }
        }
}
