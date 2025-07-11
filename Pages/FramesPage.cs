using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.HelperMethods;
using OpenQA.Selenium;

namespace Automation.Pages
{
    public class FramesPage(IWebDriver webDriver)
    {
        public IWebDriver webDriver = webDriver;
        private readonly ElementMethods elementMethods = new ElementMethods(webDriver);
        private readonly JavaScriptMethods javaScriptMethods = new JavaScriptMethods(webDriver);
        private IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;

        IWebElement frame1 => webDriver.FindElement(By.Id("frame1"));
        IWebElement frame2 => webDriver.FindElement(By.Id("frame2"));
        IWebElement frame1Text => webDriver.FindElement(By.Id("sampleHeading"));

        public void GetTextFromFrameOne()
        {
            webDriver.SwitchTo().Frame(frame1);

            Console.WriteLine(frame1Text.Text);

            webDriver.SwitchTo().DefaultContent();
        }

        public void ScrollOnFrameTwo()
        {
            javaScriptMethods.ScrollPageVertically(1000);
            webDriver.SwitchTo().Frame(frame2);

            javaScriptMethods.ScrollPageDynamic(25, 25);

            Console.WriteLine("Second frame");
            webDriver.SwitchTo().DefaultContent();
        }
    }
}
