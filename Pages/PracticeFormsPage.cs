using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.HelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Automation.Access; // Assuming you have a PracticeFormsData class in this namespace

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

        IWebElement firstNameElement => webDriver.FindElement(By.Id("firstName"));
        IWebElement lastNameElement => webDriver.FindElement(By.Id("lastName"));
        IWebElement userEmailElement => webDriver.FindElement(By.Id("userEmail"));
        IWebElement userNumberElement => webDriver.FindElement(By.Id("userNumber"));
        IWebElement currentAddressElement => webDriver.FindElement(By.Id("currentAddress"));

        public void CompleteFirstRegion(string firstName, string lastName, string userEmail, string userNumber, string currentAddress)
        {
            this.elementMethods.FillElement(firstNameElement, firstName);
            this.elementMethods.FillElement(lastNameElement, lastName);
            this.elementMethods.FillElement(userEmailElement, userEmail);
            this.elementMethods.FillElement(userNumberElement, userNumber);
            this.elementMethods.FillElement(currentAddressElement, currentAddress);

        }

        public void CompleteForm(PracticeFormsData practiceFormsData)
        {
            this.elementMethods.FillElement(firstNameElement, practiceFormsData.FirstName!);
            this.elementMethods.FillElement(lastNameElement, practiceFormsData.LastName!);
            this.elementMethods.FillElement(userEmailElement, practiceFormsData.UserEmail!);
            this.elementMethods.FillElement(userNumberElement, practiceFormsData.UserNumber!);
            this.DateOfBirth(practiceFormsData);
            this.FillSubjects(practiceFormsData);
            this.elementMethods.FillElement(currentAddressElement, practiceFormsData.CurrentAddress!);
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
            SelectDOB(practiceFormsData.MonthPick!, practiceFormsData.YearPick!, practiceFormsData.DayPick!);
        }

        IWebElement SubjectsInput => webDriver.FindElement(By.Id("subjectsInput"));
        public void FillSubjects(PracticeFormsData practiceFormsData)
        {
            if (practiceFormsData.SubjectsChosenList.Count == 0)
            {
                Console.WriteLine("No subject has been selected");
                return;
            }
            foreach (var subject in practiceFormsData.SubjectsChosenList)
            {
                elementMethods.FillElement(SubjectsInput, subject);
                elementMethods.ScrollPageToElement(SubjectsInput);
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
    }
}
