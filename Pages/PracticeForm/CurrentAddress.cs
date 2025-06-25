using Automation.Access;
using Automation.HelperMethods;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Pages.PracticeForm
{
    public class CurrentAddress(IWebDriver webDriver) : PracticeForm(webDriver)
    {
        private readonly IWebDriver webDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        public IWebElement CurrentAddressField => webDriver.FindElement(By.Id("currentAddress"));
        public void FillCurrentAddress(PracticeFormsData practiceFormsData)
        {
            if (string.IsNullOrWhiteSpace(practiceFormsData.CurrentAddress))
            {
                Console.WriteLine("Current Address is not mandatory but, is missing - using '123 Main St'");
                practiceFormsData.CurrentAddress = "123 Main St";
            }
            ElementMethods.FillElement(CurrentAddressField, practiceFormsData.CurrentAddress!);
        }
    }
}
