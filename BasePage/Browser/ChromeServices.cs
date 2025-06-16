using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automation.BasePage.Browser
{
    public class ChromeServices : IBrowserService
    {
        public WebDriver webDriver { get; private set; } = null!;

        public object BrowserOptions()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("--disable-infobars");
            chromeOptions.AddArgument("--disable-extensions");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--remote-allow-origins=*");
            return chromeOptions;
        }

        public void OpenBrowser()
        {
            ChromeOptions options = (ChromeOptions)BrowserOptions();
            webDriver = new ChromeDriver(options);
        }
    }
}
