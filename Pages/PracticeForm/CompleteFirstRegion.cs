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
    public class CompleteFirstRegion(IWebDriver webDriver) : PracticeForm(webDriver)
    {
        private readonly IWebDriver webDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));

        public IWebElement FirstName => webDriver.FindElement(By.Id("firstName"));
        public IWebElement LastName => webDriver.FindElement(By.Id("lastName"));
        public IWebElement UserEmail => webDriver.FindElement(By.Id("userEmail"));
        public IWebElement UserNumber => webDriver.FindElement(By.Id("userNumber"));

        public void FillFirstRegion(PracticeFormsData practiceFormsData)
        {
            if (string.IsNullOrWhiteSpace(practiceFormsData.FirstName))
            {
                Console.WriteLine("First Name is mandatory but is missing - using 'John'");
                practiceFormsData.FirstName = "John"; // Default value
                ElementMethods.FillElement(FirstName, practiceFormsData.FirstName!);
            }
            else
            {
                ElementMethods.FillElement(FirstName, practiceFormsData.FirstName!);
            }

            if (string.IsNullOrWhiteSpace(practiceFormsData.LastName))
            {
                Console.WriteLine("Last Name is mandatory but is missing - using 'Doe'");
                practiceFormsData.LastName = "Doe"; // Default value
                ElementMethods.FillElement(LastName, practiceFormsData.LastName!);
            }
            else
            {
                ElementMethods.FillElement(LastName, practiceFormsData.LastName!);
            }

            if (string.IsNullOrWhiteSpace(practiceFormsData.UserEmail))
            {
                Console.WriteLine("User email is empty or null, skipping email field fill.");
            }
            else
            {
                ElementMethods.FillElement(UserEmail, practiceFormsData.UserEmail!);
            }

            if (string.IsNullOrWhiteSpace(practiceFormsData.UserNumber))
            {
                Console.WriteLine("User number is mandatory - using 1234567890.");
                practiceFormsData.UserNumber = "1234567890"; // Default value
                ElementMethods.FillElement(UserNumber, practiceFormsData.UserNumber!);
            }
            else
            {
                ElementMethods.FillElement(UserNumber, practiceFormsData.UserNumber!);
            }
        }
    }
}
