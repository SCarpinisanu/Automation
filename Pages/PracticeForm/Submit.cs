using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Pages.PracticeForm
{
    public class Submit(IWebDriver webDriver) : BasePage.BasePage()
    {
        public new IWebDriver? webDriver = webDriver;

        public void SubmitAction()
        {
            if (webDriver == null)
            {
                throw new InvalidOperationException("webDriver is not initialized.");
            }

            PerformSubmit(webDriver);
        }

        private static void PerformSubmit(IWebDriver webDriver)
        {
            // Locate and wait for the submit button to be visible and clickable
            var submitButton = webDriver.FindElement(By.Id("submit"));
            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);

            // Click the submit button
            submitButton.Click();
        }
    }
}
