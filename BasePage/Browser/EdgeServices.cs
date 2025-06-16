using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Automation.BasePage.Browser
{
    public class EdgeServices : IBrowserService
    {
        public IWebDriver webDriver { get; private set; } = null!;
        public object BrowserOptions()
        {
            EdgeOptions edgeOptions = new EdgeOptions();
            edgeOptions.AddArgument("--start-maximized");
            edgeOptions.AddArgument("--disable-infobars");
            edgeOptions.AddArgument("--disable-extensions");
            edgeOptions.AddArgument("--disable-gpu");
            edgeOptions.AddArgument("--no-sandbox");
            edgeOptions.AddArgument("--disable-dev-shm-usage");
            edgeOptions.AddArgument("--remote-allow-origins=*");
            return edgeOptions;
        }
        public void OpenBrowser()
        {
            EdgeOptions options = (EdgeOptions)BrowserOptions();
            webDriver = new EdgeDriver(options);
        }
    }
}
