using Automation.Access;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Pages.PracticeForm
{
    public class DateOfBirth : PracticeForm
    {
        private readonly IWebDriver _webDriver;
        private readonly DateOfBirth? _dateOfBirth;

        public DateOfBirth(IWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
            _dateOfBirth = dateOfBirth;
        }

        public new void SelectDateOfBirth(PracticeFormsData practiceFormsData)
        {
            // Open the date picker after scroll to the date of birth input field
            var selectDate = _webDriver.FindElement(By.Id("dateOfBirthInput"));
            ((IJavaScriptExecutor)_webDriver)
                .ExecuteScript("arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });", selectDate);
            
            selectDate.Click();

            // Select year
            var yearSelect = new SelectElement(_webDriver.FindElement(By.CssSelector(".react-datepicker__year-select")));
            if (practiceFormsData.YearPick == null || !int.TryParse(practiceFormsData.YearPick, out int year) || year < 1900 || year > DateTime.Now.Year)
            {
                Console.WriteLine("Year is invalid or not provided - using current year as default.");
                practiceFormsData.YearPick = DateTime.Now.Year.ToString();
            }
            yearSelect.SelectByValue(practiceFormsData.YearPick);

            // Select month (0-based index)
            var monthSelect = new SelectElement(_webDriver.FindElement(By.CssSelector(".react-datepicker__month-select")));
            if (practiceFormsData.MonthPick == null || !int.TryParse(practiceFormsData.MonthPick, out int month) || month < 1 || month > 12)
            {
                Console.WriteLine($"Month is invalid or not provided - using {practiceFormsData.MonthPick} as default.");
                practiceFormsData.MonthPick = "2"; // Default to February
            }
            monthSelect.SelectByValue((int.Parse(practiceFormsData.MonthPick!) - 1).ToString());

            // Select day
            if (practiceFormsData.DayPick == null || !int.TryParse(practiceFormsData.DayPick, out int day) || day < 1 || day > 31)
            {
                practiceFormsData.DayPick = "01"; // Default to 1s
                Console.WriteLine($"Day is invalid or not provided - using {practiceFormsData.DayPick} as default.");
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
                        var dayElement = _webDriver.FindElement(By.CssSelector(daySelector));
                        dayElement.Click();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Data NU este validă!");
                    }
                }
            }
        }
    }
}
