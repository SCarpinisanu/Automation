using Automation.Access;
using Automation.HelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Pages.PracticeForm
{
    public class InputSubjects : BasePage.BasePage
    {
        public InputSubjects(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        private IWebElement SubjectsInputElement => webDriver?.FindElement(By.Id("subjectsInput"))
            ?? throw new NullReferenceException("webDriver is null.");

        public void FillSubjects(PracticeFormsData practiceFormsData)
        {
            if (webDriver == null)
                throw new NullReferenceException("webDriver is null.");

            if (SubjectsInputElement == null)
                throw new NullReferenceException("SubjectsInputElement is null.");

            ((IJavaScriptExecutor)webDriver)
                .ExecuteScript("arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });", SubjectsInputElement);

            if (practiceFormsData.SubjectsChosenList.Count == 0)
            {
                Console.WriteLine("No subject has been selected");
                return;
            }

            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));

            foreach (var subject in practiceFormsData.SubjectsChosenList)
            {
                ElementMethods.FillElement(SubjectsInputElement, subject);

                // Wait for the dropdown option to appear
                var subjectOption = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath($"//div[contains(@class, 'subjects-auto-complete__option') and text()='{subject}']")));

                ElementMethods.ClickOnElement(subjectOption);
            }
        }
    }
}
