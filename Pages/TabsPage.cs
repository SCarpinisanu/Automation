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
    public class TabsPage
    {
        public IWebDriver webDriver;
        public ElementMethods elementMethods;

        public TabsPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            elementMethods = new ElementMethods(webDriver);
        }

        public void ProcessTabs()
        {
            // 1. Check if the "What" tab is displayed and active
            var whatTab = webDriver.FindElement(By.Id("demo-tab-what"));
            if (whatTab != null && whatTab.Displayed && whatTab.GetAttribute("class")?.Contains("active") == true)
            {
                Console.WriteLine("What tab is displayed and active.");
            }

            // 2. Get and process text from the first paragraph under "What"
            var whatParagraph = webDriver.FindElement(By.CssSelector("#demo-tabpane-what .mt-3"));
            if (whatParagraph != null)
            {
                ProcessAndLogText(whatParagraph.Text, "What Tab Paragraph");
            }

            // 3. Click the "Origin" tab
            var originTab = webDriver.FindElement(By.Id("demo-tab-origin"));
            if (originTab != null)
            {
                originTab.Click();
            }

            // 4. Get and process text from the first paragraph under "Origin"
            var originParagraph1 = webDriver.FindElement(By.CssSelector("#demo-tabpane-origin .mt-3"));
            if (originParagraph1 != null)
            {
                ProcessAndLogText(originParagraph1.Text, "Origin Tab Paragraph 1");
            }

            // 5. Get and process text from the second paragraph under "Origin"
            var originParagraph2 = webDriver.FindElement(By.CssSelector("#demo-tabpane-origin p:not(.mt-3)"));
            if (originParagraph2 != null)
            {
                ProcessAndLogText(originParagraph2.Text, "Origin Tab Paragraph 2");
            }

            // 6. Click the "Use" tab
            var useTab = webDriver.FindElement(By.Id("demo-tab-use"));
            if (useTab != null)
            {
                useTab.Click();
            }

            // 7. Get and process text from the paragraph under "Use"
            var useParagraph = webDriver.FindElement(By.CssSelector("#demo-tabpane-use .mt-3"));
            if (useParagraph != null)
            {
                ProcessAndLogText(useParagraph.Text, "Use Tab Paragraph");
            }
        }

        private void ProcessAndLogText(string text, string label)
        {
            int wordCount = CountWords(text);
            int punctuationCount = CountPunctuation(text);

            Console.WriteLine($"{label}:");
            Console.WriteLine($"  Words: {wordCount}");
            Console.WriteLine($"  Punctuation marks: {punctuationCount}");
        }

        private int CountWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;
            return text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private int CountPunctuation(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            return text.Count(char.IsPunctuation);
        }
    }
}