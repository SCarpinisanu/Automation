using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.HelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automation.Pages
{
    public class CommonPage
    {
        public IWebDriver webDriver;
        private ElementMethods elementMethods;
        private IJavaScriptExecutor js;

        public CommonPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            elementMethods = new ElementMethods(webDriver);
            js = (IJavaScriptExecutor)webDriver;
        }

        List<IWebElement> elements => webDriver.FindElements(By.XPath("//span[@class='text']")).ToList();
        // public ElementMethods ElementMethods => elementMethods;

        public void GoToDesiredMenuItem(string menuName)
        {
            js.ExecuteScript("window.scrollTo(0, 1000)");
            elementMethods.SelectElementFromListByText(elements, menuName);
            Console.WriteLine(menuName);
        }



    }
}
