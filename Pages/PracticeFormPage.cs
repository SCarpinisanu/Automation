using Automation.Access; // Assuming you have a PracticeFormsData class in this namespace
using Automation.HelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Automation.BasePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Automation.BasePage.Browser;

namespace Automation.Pages
{
    public class PracticeFormPage
    {
        public IWebDriver webDriver;
        public ElementMethods elementMethods;

        public PracticeFormPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            elementMethods = new ElementMethods(webDriver);
        }

        public IWebElement FirstNameElement => webDriver.FindElement(By.Id("firstName"));
        public IWebElement LastNameElement => webDriver.FindElement(By.Id("lastName"));
        public IWebElement UserEmailElement => webDriver.FindElement(By.Id("userEmail"));
        public IWebElement UserNumberElement => webDriver.FindElement(By.Id("userNumber"));
        public IWebElement CurrentAddressElement => webDriver.FindElement(By.Id("currentAddress"));

        public void CompleteFirstRegion(string firstName, string lastName, string userEmail, string userNumber, string currentAddress)
        {
            this.elementMethods.FillElement(FirstNameElement, firstName);
            this.elementMethods.FillElement(LastNameElement, lastName);
            this.elementMethods.FillElement(UserEmailElement, userEmail);
            this.elementMethods.FillElement(UserNumberElement, userNumber);
            this.elementMethods.FillElement(CurrentAddressElement, currentAddress);

        }

        public void CompleteForm(PracticeFormsData practiceFormsData)
        {
            if (string.IsNullOrWhiteSpace(practiceFormsData.FirstName))
            {
                Console.WriteLine("First Name is mandatory but is missing - using 'Ion'");
                practiceFormsData.FirstName = "Ion"; // Default value
                this.elementMethods.FillElement(FirstNameElement, practiceFormsData.FirstName!);
            }
            else
            {
                this.elementMethods.FillElement(FirstNameElement, practiceFormsData.FirstName!);
            }

            if (string.IsNullOrWhiteSpace(practiceFormsData.LastName))
            {
                Console.WriteLine("Last Name is mandatory but is missing - using 'Popescu'");
                practiceFormsData.LastName = "Popescu"; // Default value
                this.elementMethods.FillElement(LastNameElement, practiceFormsData.LastName!);
            }
            else
            {
                this.elementMethods.FillElement(LastNameElement, practiceFormsData.LastName!);
            }

            if (string.IsNullOrWhiteSpace(practiceFormsData.UserEmail))
            {
                Console.WriteLine("User email is empty or null, skipping email field fill.");
            }
            else
            {
                this.elementMethods.FillElement(UserEmailElement, practiceFormsData.UserEmail!);
            }

            if (string.IsNullOrWhiteSpace(practiceFormsData.UserNumber))
            {
                Console.WriteLine("User number is mandatory - using 1234567890.");
                practiceFormsData.UserNumber = "1234567890"; // Default value
                this.elementMethods.FillElement(UserNumberElement, practiceFormsData.UserNumber!);
            }
            else 
            {
                this.elementMethods.FillElement(UserNumberElement, practiceFormsData.UserNumber!);
            }

            this.ChooseGender(practiceFormsData.GenderChosen);

            if (string.IsNullOrWhiteSpace(practiceFormsData.MonthPick) || string.IsNullOrWhiteSpace(practiceFormsData.YearPick) || string.IsNullOrWhiteSpace(practiceFormsData.DayPick))
            {
                Console.WriteLine("Date of Birth is not mandatory - skipping field");
            }
            else
            {
                this.DateOfBirth(practiceFormsData);
            }
            
            if (practiceFormsData.SubjectsChosenList.Count == 0)
            {
                Console.WriteLine("No subject has been selected - skipping field.");
            }
            else 
            {
                this.FillSubjects(practiceFormsData);
            }

            if (string.IsNullOrWhiteSpace(practiceFormsData.HobbiesChosen))
            {
                Console.WriteLine("No hobbies chosen - skipping field");
            }
            else
            {
                this.SelectHobbiesByData(practiceFormsData);
            }


            if (string.IsNullOrWhiteSpace(practiceFormsData.CurrentAddress))
            {
                Console.WriteLine("Current address is empty or null, skipping current address.");
            }
            else
            {
                this.elementMethods.FillElement(CurrentAddressElement, practiceFormsData.CurrentAddress!);
            }
        }

        public void SelectDOB(string monthPick, string yearPick, string dayPick)
        {
            var dateSelected = webDriver.FindElement(By.Id("dateOfBirthInput"));
            dateSelected.Click();

            var selectMonthList = webDriver.FindElement(By.ClassName("react-datepicker__month-select"));
            var selectMonth = new SelectElement(selectMonthList);
            selectMonth.SelectByText(monthPick);

            var selectYearList = webDriver.FindElement(By.ClassName("react-datepicker__year-select"));
            var selectYear = new SelectElement(selectYearList);
            selectYear.SelectByText(yearPick); // <-- Corrected line

            var dayOfBirthSelect = webDriver.FindElement(By.CssSelector($"div.react-datepicker__day--0{dayPick}:not(.react-datepicker__day--outside-month)"));
            dayOfBirthSelect.Click();
        }

        public void DateOfBirth(PracticeFormsData practiceFormsData)
        {
            // Open the date picker
            var selectDate = webDriver.FindElement(By.Id("dateOfBirthInput"));
            selectDate.Click();

            // Select year
            var yearSelect = new SelectElement(webDriver.FindElement(By.CssSelector(".react-datepicker__year-select")));
            yearSelect.SelectByValue(practiceFormsData.YearPick!);

            // Select month (0-based index)
            var monthSelect = new SelectElement(webDriver.FindElement(By.CssSelector(".react-datepicker__month-select")));
            monthSelect.SelectByValue((int.Parse(practiceFormsData.MonthPick!) - 1).ToString());

            // Select day
            string daySelector = $".react-datepicker__day--0{int.Parse(practiceFormsData.DayPick!):D2}:not(.react-datepicker__day--outside-month)";
            var dayElement = webDriver.FindElement(By.CssSelector(daySelector));
            dayElement.Click();
        }

        IWebElement SubjectsInput => webDriver.FindElement(By.Id("subjectsInput"));
        public void FillSubjects(PracticeFormsData practiceFormsData)
        {
            ((IJavaScriptExecutor)webDriver)
                .ExecuteScript("arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });", SubjectsInput);

            if (practiceFormsData.SubjectsChosenList.Count == 0)
            {
                Console.WriteLine("No subject has been selected");
                return;
            }
            foreach (var subject in practiceFormsData.SubjectsChosenList)
            {
                elementMethods.FillElement(SubjectsInput, subject);
                //elementMethods.ScrollPageToElement(SubjectsInput);
                var subjectOption = webDriver.FindElement(By.XPath($"//div[contains(@class, 'subjects-auto-complete__option') and text()='{subject}']"));
                elementMethods.ClickOnElement(subjectOption);
            }
        }

        IWebElement genderMale => webDriver.FindElement(By.XPath("//label[@for='gender-radio-1']"));
        IWebElement genderFemale => webDriver.FindElement(By.XPath("//label[@for='gender-radio-2']"));
        IWebElement genderOther => webDriver.FindElement(By.XPath("//label[@for='gender-radio-3']"));

        public void ChooseGender(string? genderChosen)
        {
            switch (genderChosen)
            {
                case "Male":
                    elementMethods.ClickOnElement(genderMale);
                    break;
                case "Female":
                    elementMethods.ClickOnElement(genderFemale);
                    break;
                default:
                    Console.WriteLine("No gender was selected or wrong text - using default 'Other'!!!");
                    elementMethods.ClickOnElement(genderOther);
                    break;
            }
        }

        public void SelectHobbiesByName(params string[] hobbyNames)
        {
            if (hobbyNames.Length == 0)
            {
                Console.WriteLine("No hobby has been selected");
                return;
            }
            foreach (var hobby in hobbyNames)
            {
                if (hobby == "Sports" || hobby == "Reading" || hobby == "Music")
                {
                    var label = webDriver.FindElement(By.XPath($"//label[text()='{hobby}']"));
                    elementMethods.ClickOnElement(label);
                }
                else
                {
                    Console.WriteLine($"The hobby named {hobby} is not in the list");
                }
            }
        }

        public void SelectHobbiesByData(PracticeFormsData practiceFormsData)
        {
            if (string.IsNullOrWhiteSpace(practiceFormsData.HobbiesChosen))
            {
                Console.WriteLine("No hobby has been selected");
                return;
            }

            // Split hobbies if they are comma-separated in XML
            var hobbies = practiceFormsData.HobbiesChosen
                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(h => h.Trim())
                .ToArray();

            foreach (var hobby in hobbies)
            {
                // Find the label by its text and click it
                try
                {
                    var label = webDriver.FindElement(By.XPath($"//label[text()='{hobby}']"));
                    elementMethods.ClickOnElement(label);
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"The hobby named {hobby} is not in the list");
                }
            }
        }

        public void SubmitForm()
        {
            // Locate and wait for the submit button to be visible and clickable
            var submitButton = webDriver.FindElement(By.Id("submit"));
            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);

            // Click the submit button
            submitButton.Click();
        }

        public void CloseModal()
        {
            Thread.Sleep(2000); // Wait for 2 seconds to see the modal
            var closeButton = webDriver.FindElement(By.Id("closeLargeModal"));
            closeButton.Click();
        }
    }
}
