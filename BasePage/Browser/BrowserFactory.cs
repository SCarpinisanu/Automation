using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automation.BasePage.Browser
{
    public class BrowserFactory
    {
        public IWebDriver GetBrowserFactory()
        {
            string browser = "Chrome";

            switch (browser)
            {
                case BrowserType.BROWSER_CHROME:
                    var chromeDriver = new ChromeServices();
                    chromeDriver.OpenBrowser();
                    return chromeDriver.webDriver;
                case BrowserType.BROWSER_EDGE:
                    var edgeDriver = new EdgeServices();
                    edgeDriver.OpenBrowser();
                    return edgeDriver.webDriver;
                default:
                    throw new NotSupportedException($"Browser '{browser}' is not supported.");
            }
        }
    }
}
