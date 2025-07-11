using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Automation.HelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automation.Pages
{
    public class ModalDialogsPage(IWebDriver webDriver)
    {
        public IWebDriver webDriver = webDriver;
        public ElementMethods elementMethods = new ElementMethods(webDriver);
        private readonly WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

        public (int smallModalWord, int smallModalPunctuation) HandleSmallModal()
        {
            webDriver.FindElement(By.Id("showSmallModal")).Click();
            IWebElement smallModalText = webDriver.FindElement(By.XPath("//div[@class='modal-body' and contains(text(), 'This is a small modal')]"));
            smallModalText.Click();

            string smallModalBodyText = smallModalText.Text;

            int smallModalWords = Regex.Matches(smallModalBodyText, @"\b\w+\b").Count;
            int smallModalPunctuation = smallModalBodyText.Count(char.IsPunctuation);
            return (smallModalWords, smallModalPunctuation);
        }

        public (int largeModalWord, int largeModalPunctuation) HandleLargeModal()
        {
            webDriver.FindElement(By.Id("showLargeModal")).Click();

            IWebElement largeModalText = webDriver.FindElement(By.CssSelector("div.modal-body p"));
            largeModalText.Click();

            string largeModalBodyText = largeModalText.Text;

            int largeModalWords = Regex.Matches(largeModalBodyText, @"\b\w+\b").Count;
            int largeModalPunctuation = largeModalBodyText.Count(char.IsPunctuation);
            return (largeModalWords, largeModalPunctuation);
        }

        public void CloseSmallModal()
        {
            System.Threading.Thread.Sleep(1000);
            webDriver.FindElement(By.Id("closeSmallModal")).Click();
        }

        public void CloseLargeModal()
        {
            System.Threading.Thread.Sleep(1000);
            webDriver.FindElement(By.Id("closeLargeModal")).Click();
        }
    }
}
