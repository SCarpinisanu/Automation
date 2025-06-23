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
            ElementMethods.FillElement(FirstNameElement, firstName);
            ElementMethods.FillElement(LastNameElement, lastName);
            ElementMethods.FillElement(UserEmailElement, userEmail);
            ElementMethods.FillElement(UserNumberElement, userNumber);
            ElementMethods.FillElement(CurrentAddressElement, currentAddress);

        }

        public void CompleteForm(PracticeFormsData practiceFormsData)
        {
            if (string.IsNullOrWhiteSpace(practiceFormsData.FirstName))
            {
                Console.WriteLine("First Name is mandatory but is missing - using 'Ion'");
                practiceFormsData.FirstName = "Ion"; // Default value
                ElementMethods.FillElement(FirstNameElement, practiceFormsData.FirstName!);
            }
            else
            {
                ElementMethods.FillElement(FirstNameElement, practiceFormsData.FirstName!);
            }

            if (string.IsNullOrWhiteSpace(practiceFormsData.LastName))
            {
                Console.WriteLine("Last Name is mandatory but is missing - using 'Popescu'");
                practiceFormsData.LastName = "Popescu"; // Default value
                ElementMethods.FillElement(LastNameElement, practiceFormsData.LastName!);
            }
            else
            {
                ElementMethods.FillElement(LastNameElement, practiceFormsData.LastName!);
            }

            if (string.IsNullOrWhiteSpace(practiceFormsData.UserEmail))
            {
                Console.WriteLine("User email is empty or null, skipping email field fill.");
            }
            else
            {
                ElementMethods.FillElement(UserEmailElement, practiceFormsData.UserEmail!);
            }

            if (string.IsNullOrWhiteSpace(practiceFormsData.UserNumber))
            {
                Console.WriteLine("User number is mandatory - using 1234567890.");
                practiceFormsData.UserNumber = "1234567890"; // Default value
                ElementMethods.FillElement(UserNumberElement, practiceFormsData.UserNumber!);
            }
            else 
            {
                ElementMethods.FillElement(UserNumberElement, practiceFormsData.UserNumber!);
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
                ElementMethods.FillElement(CurrentAddressElement, practiceFormsData.CurrentAddress!);
            }

            //if (string.IsNullOrWhiteSpace(practiceFormsData.StateChosen) || string.IsNullOrWhiteSpace(practiceFormsData.CityChosen))
            //{
            //    Console.WriteLine("State or City is empty or null, skipping state and city selection.");
            //}
            //else
            //{
            this.SelectStateAndCity(practiceFormsData);
            //}
        }

        public void SelectDOB(string monthPick, string yearPick, string dayPick)
        {
            var dateSelected = webDriver.FindElement(By.Id("dateOfBirthInput"));
            dateSelected.Click();

            var selectMonthList = webDriver.FindElement(By.ClassName("react-datepicker__month-select"));
            var selectMonth = new SelectElement(selectMonthList);
            if (int.TryParse(monthPick, out int monthNumber) && monthNumber >= 1 && monthNumber <= 12)
            {
                Console.WriteLine("Value of month from xml file is valid: " + monthNumber);
                selectMonth.SelectByText(monthPick);
            }
            else
            {
                Console.WriteLine("Value of month from xml file is invalid: " + monthPick);
                Console.WriteLine("Using default value of '01' (January)");
                selectMonth.SelectByText("01"); // Default to January if invalid month
            }

            var selectYearList = webDriver.FindElement(By.ClassName("react-datepicker__year-select"));
            var selectYear = new SelectElement(selectYearList);
            if (int.TryParse(yearPick, out int yearNumber) && yearNumber >= 1900 && yearNumber <= DateTime.Now.Year)
            {
                Console.WriteLine("Value of year from xml file is valid: " + yearNumber);
            }
            else
            {
                Console.WriteLine("Value of year from xml file is invalid: " + yearPick);
                Console.WriteLine("Using default value of current year: " + DateTime.Now.Year);
                yearPick = DateTime.Now.Year.ToString(); // Default to current year if invalid
            }
            selectYear.SelectByText(yearPick); // <-- Corrected line

            if (int.TryParse(yearPick, out int year) &&
                int.TryParse(monthPick, out int month) &&
                int.TryParse(dayPick, out int day))
            {
                try
                {
                    DateTime date = new(year, month, day);
                    Console.WriteLine($"Data este validă: {date.ToShortDateString()}");
                    // If the date is valid, you can proceed with further actions
                    var dayOfBirthSelect = webDriver.FindElement(By.CssSelector($"div.react-datepicker__day--0{dayPick}:not(.react-datepicker__day--outside-month)"));
                    dayOfBirthSelect.Click();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Data NU este validă!");
                }
            }
            else
            {
                Console.WriteLine("Valori numerice invalide!");
            }
            
        }

        public void DateOfBirth(PracticeFormsData practiceFormsData)
        {
            // Open the date picker
            var selectDate = webDriver.FindElement(By.Id("dateOfBirthInput"));
            selectDate.Click();

            // Select year
            var yearSelect = new SelectElement(webDriver.FindElement(By.CssSelector(".react-datepicker__year-select")));
            if (practiceFormsData.YearPick == null || !int.TryParse(practiceFormsData.YearPick, out int year) || year < 1900 || year > DateTime.Now.Year)
            {
                Console.WriteLine("Year is invalid or not provided - using current year as default.");
                practiceFormsData.YearPick = DateTime.Now.Year.ToString();
            }
            yearSelect.SelectByValue(practiceFormsData.YearPick!);

            // Select month (0-based index)
            var monthSelect = new SelectElement(webDriver.FindElement(By.CssSelector(".react-datepicker__month-select")));
            if (practiceFormsData.MonthPick == null || !int.TryParse(practiceFormsData.MonthPick, out int month) || month < 1 || month > 12)
            {
                Console.WriteLine("Month is invalid or not provided - using January as default.");
                practiceFormsData.MonthPick = "01"; // Default to January
            }
            monthSelect.SelectByValue((int.Parse(practiceFormsData.MonthPick!) - 1).ToString());

            // Select day
            if (practiceFormsData.DayPick == null || !int.TryParse(practiceFormsData.DayPick, out int day) || day < 1 || day > 31)
            {
                Console.WriteLine("Day is invalid or not provided - using 1 as default.");
                practiceFormsData.DayPick = "01"; // Default to 1st
            }
            else
            {
                if (int.TryParse(practiceFormsData.YearPick, out int yearPractice) &&
                    int.TryParse(practiceFormsData.MonthPick, out int monthPractice) &&
                    int.TryParse(practiceFormsData.DayPick, out int dayPractice))
                {
                    try
                    {
                        DateTime date = new(yearPractice, monthPractice, dayPractice);
                        Console.WriteLine($"Data este validă: {date.ToShortDateString()}");
                        string daySelector = $".react-datepicker__day--0{int.Parse(practiceFormsData.DayPick!):D2}:not(.react-datepicker__day--outside-month)";
                        var dayElement = webDriver.FindElement(By.CssSelector(daySelector));
                        dayElement.Click();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Data NU este validă!");
                    }
                }
            }
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
                ElementMethods.FillElement(SubjectsInput, subject);
                //elementMethods.ScrollPageToElement(SubjectsInput);
                var subjectOption = webDriver.FindElement(By.XPath($"//div[contains(@class, 'subjects-auto-complete__option') and text()='{subject}']"));
                ElementMethods.ClickOnElement(subjectOption);
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
                    ElementMethods.ClickOnElement(label);
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
                    ElementMethods.ClickOnElement(label);
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"The hobby named {hobby} is not in the list");
                }
            }
        }

        IWebElement SelectState => webDriver.FindElement(By.Id("state"));
        public void SelectStateAndCity(PracticeFormsData practiceFormsData)
        {
            // Scroll to the state dropdown
            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", SelectState);

            // Select State
            var stateDropdown = webDriver.FindElement(By.Id("state"));
            stateDropdown.Click();

            if (string.IsNullOrWhiteSpace(practiceFormsData.StateChosen))
            {
                Console.WriteLine("State is empty or null, skipping state selection.");
                Console.WriteLine("No City selection available.");
                return;
            }
            else
            {
                var stateOptions = webDriver.FindElements(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.StateChosen}']"));
                if (stateOptions.Count == 0)
                {
                    Console.WriteLine($"State '{practiceFormsData.StateChosen}' not found in the dropdown.");
                    Console.WriteLine("Skipping state and city selection.");
                    return;
                }
                else 
                {
                    var stateWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5)); // Renamed 'wait' to 'stateWait'
                    stateWait.Until(d => d.FindElement(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.StateChosen}']")));

                    var stateOption = webDriver.FindElement(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.StateChosen}']"));
                    stateOption.Click();

                    // Așteaptă actualizarea dropdownului pentru city
                    var cityDropdown = webDriver.FindElement(By.Id("city"));
                    cityDropdown.Click();

                    if (string.IsNullOrWhiteSpace(practiceFormsData.CityChosen))
                    {
                        Console.WriteLine("City is empty or null, skipping state and city selection.");
                        return;
                    }
                    else
                    {                        
                        var cityOptions = webDriver.FindElements(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.CityChosen}']"));
                        if (cityOptions.Count == 0)
                        {
                            Console.WriteLine($"City '{practiceFormsData.CityChosen}' not found in the dropdown.");
                            Console.WriteLine("Skipping state and city selection");
                            return;
                        }
                        else
                        {
                            var cityWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5)); // Renamed 'wait' to 'cityWait'
                            cityWait.Until(d => d.FindElement(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.CityChosen}']")));

                            var cityOption = webDriver.FindElement(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.CityChosen}']"));
                            cityOption.Click();
                        }
                    }
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
