using Automation.Access;
using Automation.BasePage.Browser;
using Automation.HelperMethods;
using Automation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automation.BasePage
{
    public class BasePage : IDisposable
    {
        public IWebDriver? webDriver;

        public ElementMethods? elementMethods;
        public HomePage? homePage;
        public CommonPage? commonPage;
        //public BasePage.BasePage? basePage;
        public PracticeFormsPage? practiceFormsPage;
        public WebTablesPage? webTablesPage;
        public FramesPage? framesPage;
        public JavaScriptMethods? javaScriptMethods;
        public ModalDialogsPage? modalDialogsPage;
        public TabsPage? tabsPage;
        public AlertsPage? alertsPage;
        public WebTablesData? webTablesData;

        [SetUp]
        public void SetUp()
        {
            webDriver = new BrowserFactory().GetBrowserFactory();
            //webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl("https://demoqa.com/");
            //webDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }

        public void Dispose()
        {
            System.Threading.Thread.Sleep(1000); // Wait for 1 second before closing the browser 
            if (webDriver != null)
            {
                webDriver.Quit();
                webDriver.Dispose();
                webDriver = null;
            }
        }
    }
}