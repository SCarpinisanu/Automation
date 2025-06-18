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

namespace Automation.Pages
{
    public class PracticeFormsPage
    {
        public IWebDriver webDriver;
        public ElementMethods elementMethods;

        public PracticeFormsPage(IWebDriver webDriver)
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
            this.elementMethods.FillElement(FirstNameElement, practiceFormsData.FirstName!);
            this.elementMethods.FillElement(LastNameElement, practiceFormsData.LastName!);
            this.elementMethods.FillElement(UserEmailElement, practiceFormsData.UserEmail!);
            this.elementMethods.FillElement(UserNumberElement, practiceFormsData.UserNumber!);
            this.ChooseGender(practiceFormsData.GenderChosen);
            this.DateOfBirth(practiceFormsData);
            this.FillSubjects(practiceFormsData);
            this.SelectHobbiesByData(practiceFormsData);
            this.elementMethods.FillElement(CurrentAddressElement, practiceFormsData.CurrentAddress!);
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
            // Scroll vertically by 500 pixels
            ((IJavaScriptExecutor)webDriver).ExecuteScript("window.scrollBy(0,500);");

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
                case "Other":
                    elementMethods.ClickOnElement(genderOther);
                    break;
                default:
                    Console.WriteLine("No gender was selected!!!");
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
            // Locate the submit button
            var submitButton = webDriver.FindElement(By.Id("submit"));

            // Click the submit button
            submitButton.Click();
        }

        public void CloseModal()
        {
            var closeButton = webDriver.FindElement(By.Id("closeLargeModal"));
            elementMethods.ClickOnElement(closeButton);
        }
    }
}
