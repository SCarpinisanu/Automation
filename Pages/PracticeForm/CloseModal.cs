using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Pages.PracticeForm
{
    public class CloseModalPage(IWebDriver webDriver) : BasePage.BasePage
    {
        // Fix: Remove the invalid field initializer and properly initialize the field in the constructor.
        public new IWebDriver? webDriver = webDriver;

        public void CloseModal()
        {
            if (webDriver == null)
            {
                throw new InvalidOperationException("webDriver is not initialized.");
            }

            CloseModalAction(webDriver);
        }

        private static void CloseModalAction(IWebDriver webDriver)
        {
            Thread.Sleep(2000); // Wait for 2 seconds to see the modal  
            var closeButton = webDriver.FindElement(By.Id("closeLargeModal"));
            closeButton.Click();
        }
    }
}
