using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Automation.HelperMethods
{
    public class JavaScriptMethods
    {
        private readonly IWebDriver webDriver;
        private readonly IJavaScriptExecutor executor;

        public JavaScriptMethods(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            executor = (IJavaScriptExecutor)webDriver;
        }

        public void ScrollPageHorizontally(int yAxe)
        {
            executor.ExecuteScript($"window.scrollTo({yAxe},0)");
        }

        public void ScrollPageVertically(int xAxe)
        {
            executor.ExecuteScript($"window.scrollTo(0,{xAxe})");
        }

        public void ScrollPageDynamic(int xAxe, int yAxe)
        {
            executor.ExecuteScript($"window.scrollTo({xAxe}, {yAxe})");
        }
    }
}
