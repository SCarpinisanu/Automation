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
    public class DateOfBirth2(IWebDriver webDriver) : PracticeForm(webDriver)
    {
        private readonly IWebDriver webDriver = webDriver;

        public void SelectDOB(PracticeFormsData practiceFormsData)
        {
            if (IsValidDate(practiceFormsData.YearPick, practiceFormsData.MonthPick, practiceFormsData.DayPick))
            {
                // The date is valid
                var selectDate = webDriver.FindElement(By.Id("dateOfBirthInput"));
                _ = ((IJavaScriptExecutor)webDriver)
                    .ExecuteScript("arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });", selectDate);
                selectDate.Click();

                var yearSelect = new SelectElement(webDriver.FindElement(By.CssSelector(".react-datepicker__year-select")));
                yearSelect.SelectByValue(practiceFormsData.YearPick!);

                var monthSelect = new SelectElement(webDriver.FindElement(By.CssSelector(".react-datepicker__month-select")));
                monthSelect.SelectByValue((int.Parse(practiceFormsData.MonthPick!) - 1).ToString());

                if (int.TryParse(practiceFormsData.YearPick, out int yearPractice) &&
                    int.TryParse(practiceFormsData.MonthPick, out int monthPractice) &&
                    int.TryParse(practiceFormsData.DayPick, out int dayPractice))
                {
                        DateTime date = new(yearPractice, monthPractice, dayPractice);
                        Console.WriteLine($"Data este validă: {date.ToShortDateString()}");
                        string daySelector = $".react-datepicker__day--0{int.Parse(practiceFormsData.DayPick!):D2}:not(.react-datepicker__day--outside-month)";
                        var dayElement = webDriver.FindElement(By.CssSelector(daySelector));
                        dayElement.Click();
                }
            }
            else
            {
                // The date is invalid
                Console.WriteLine("Data NU este validă!");
            }
        }

        public static bool IsValidDate(string? year, string? month, string? day)
        {
            if (!int.TryParse(year, out int y) || !int.TryParse(month, out int m) || !int.TryParse(day, out int d))
                return false;

            try
            {
                var date = new DateTime(y, m, d);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
