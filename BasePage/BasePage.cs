using Automation.Access;
using Automation.BasePage.Browser;
using Automation.HelperMethods;
using Automation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automation.BasePage
{
    public class BasePage
    {
        private IWebDriver? driver; // Changed to private to encapsulate the field

        public ElementMethods? elementMethods;
        public HomePage? homePage;
        public CommonPage? commonPage;
        public PracticeFormPage? practiceFormsPage;
        public WebTablesPage? webTablesPage;
        public FramesPage? framesPage;
        public JavaScriptMethods? javaScriptMethods;
        public ModalDialogsPage? modalDialogsPage;
        public TabsPage? tabsPage;
        public AlertsPage? alertsPage;
        public WebTablesData? webTablesData;
        public PracticeFormsData? practiceFormsData;

        [SetUp]
        public void SetUp()
        {
            driver = new BrowserFactory().GetBrowserFactory();
            driver.Navigate().GoToUrl("https://demoqa.com/");
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose(); // Explicitly dispose of the webDriver to satisfy NUnit1032
            }
        }
    }
}