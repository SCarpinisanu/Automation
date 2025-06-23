using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Automation.HelperMethods
{
    public class ElementMethods(IWebDriver driver)
    {
        private readonly IWebDriver _driver = driver ?? throw new ArgumentNullException(nameof(driver));

        public static void ClickOnElement(IWebElement element)
        {
            element.Click();
        }

        public static void FillElement(IWebElement element, string text)
        {
            element.SendKeys(text);
        }

        public void SelectElementFromListByText(IList<IWebElement> elementList, string text)
        {
            foreach (IWebElement element in elementList)
            {
                if (element.Text == text)
                {
                    ClickOnElement(element);
                }
            }
        }

        public void ScrollPageToElement(IWebElement element)
        {
            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'nearest'});", element);
        }

        public void HandleIframeAndClick(IWebElement element)
        {
            var iframe = _driver.FindElements(By.TagName("iframe"))
                                .FirstOrDefault(static f => f.GetAttribute("id")?.Contains("google_ads_iframe") == true);
            if (iframe != null)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].style.display='none';", iframe);
            }
            ClickOnElement(element);
        }
    }
}
