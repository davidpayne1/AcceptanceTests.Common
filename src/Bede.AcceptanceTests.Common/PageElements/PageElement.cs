using Bede.AcceptanceTests.Common.Driver;

namespace Bede.AcceptanceTests.Common.PageElements
{
        public class PageElement
        {
            public string Locator { get; private set; }
            public PageElementLocatorTypes LocatorType { get; private set; }
            public string FriendlyDescriptor { get; private set; }

            public PageElement(string elementLocator, PageElementLocatorTypes elementLocatorType, string elementFriendlyDescriptor)
            {
                Locator = elementLocator;
                LocatorType = elementLocatorType;
                FriendlyDescriptor = elementFriendlyDescriptor;
            }
        }
}
