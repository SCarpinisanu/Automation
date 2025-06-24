using Automation.HelperMethods;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Pages.PracticeForm
{
    public class ChooseGender : PracticeForm
    {
        private readonly IWebDriver _webDriver;
        private readonly ChooseGender? _chooseGender;
        public ChooseGender(IWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
            _chooseGender = chooseGender;
        }

        public new void SelectGender(Access.PracticeFormsData practiceFormsData)
        {
            IWebElement genderMale = WebDriver.FindElement(By.XPath("//label[@for='gender-radio-1']"));
            IWebElement genderFemale = WebDriver.FindElement(By.XPath("//label[@for='gender-radio-2']"));
            IWebElement genderOther = WebDriver.FindElement(By.XPath("//label[@for='gender-radio-3']"));

            switch (practiceFormsData.GenderChosen)
            {
                case "Male":
                    ElementMethods.ClickOnElement(genderMale);
                    break;
                case "Female":
                    ElementMethods.ClickOnElement(genderFemale);
                    break;
                default:
                    Console.WriteLine("No gender was selected or wrong text - using default 'Other'!!!");
                    ElementMethods.ClickOnElement(genderOther);
                    break;
            }
        }
    }
}
