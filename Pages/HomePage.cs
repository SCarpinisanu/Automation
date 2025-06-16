using Automation.BasePage;
using Automation.HelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Automation.Pages
{
    public class HomePage : BasePage.BasePage
    {
        public IWebDriver driver;
        public ElementMethods ElementMethods { get; set; }

        public HomePage(IWebDriver webDriver)
        {
            driver = webDriver ?? throw new ArgumentNullException(nameof(webDriver), "webDriver cannot be null.");
            ElementMethods = new ElementMethods(driver);
        }

        private IList<IWebElement> GetElementsInHomePage()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//div[@class='card mt-4 top-card']"))); // Fully qualify ExpectedConditions
            return driver.FindElements(By.XPath("//div[@class='card mt-4 top-card']")).ToList();
        }

        public void ClickOnOption(int indexOfElement)
        {
            var elementsInHomePage = GetElementsInHomePage();
            if (indexOfElement < 0 || indexOfElement >= elementsInHomePage.Count)
            {
                Console.WriteLine($"The index {indexOfElement} doesn't exist. There are only {elementsInHomePage.Count}, so the last index is {elementsInHomePage.Count - 1}");
            }
            else
            {
                var h5Element = elementsInHomePage[indexOfElement].FindElement(By.TagName("h5"));
                Console.WriteLine(h5Element.Text);

                var element = elementsInHomePage[indexOfElement];

                ScrollToElement(element);

                element.Click();
            }
        }

        private void ScrollToElement(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
    }
}
