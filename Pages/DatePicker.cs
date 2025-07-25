using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.Linq;

namespace Pages
{
    public class DatePicker
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public DatePicker(IWebDriver driver, int waitSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitSeconds));
        }

        // Navigates to the Date Picker page
        public void GoTo()
        {
            _driver.Navigate().GoToUrl("https://demoqa.com/widgets");
            _wait.Until(d => d.FindElement(By.XPath("//span[text()='Date Picker']"))).Click();
            _wait.Until(d => d.FindElement(By.Id("datePickerMonthYearInput")));
        }

        // Selects a date in the "Select Date" field (e.g., March, 2010, 19)
        public void SelectDate(string month, int year, int day)
        {
            var input = _wait.Until(d => d.FindElement(By.Id("datePickerMonthYearInput")));
            input.Click();

            // Select month
            var monthSelect = new SelectElement(_wait.Until(d => d.FindElement(By.ClassName("react-datepicker__month-select"))));
            monthSelect.SelectByText(month);

            // Select year
            var yearSelect = new SelectElement(_wait.Until(d => d.FindElement(By.ClassName("react-datepicker__year-select"))));
            yearSelect.SelectByText(year.ToString());

            // Select day
            var dayCells = _driver.FindElements(By.XPath($"//div[contains(@class,'react-datepicker__day') and not(contains(@class,'outside-month')) and text()='{day}']"));
            foreach (var cell in dayCells)
            {
                if (cell.Displayed && cell.Enabled)
                {
                    cell.Click();
                    break;
                }
            }
        }

        private int GetMaxDisplayedYear()
        {
            var yearOptions = _driver.FindElements(By.XPath("//div[contains(@class,'react-datepicker__year-option') and not(contains(@class,'navigation'))]"));
            int maxYear = yearOptions
                .Select(opt => int.TryParse(opt.Text.Trim(), out int y) ? y : int.MinValue)
                .Max();
            return maxYear;
        }

        public void SelectDateAndTime(string month, int year, int day, string time)
        {
            var input = _wait.Until(d => d.FindElement(By.Id("dateAndTimePickerInput")));
            input.Click();

            // Open month dropdown and select month
            var monthReadView = _wait.Until(d => d.FindElement(By.ClassName("react-datepicker__month-read-view")));
            monthReadView.Click();
            var monthOption = _wait.Until(d => d.FindElement(By.XPath($"//div[contains(@class,'react-datepicker__month-option') and text()='{month}']")));
            monthOption.Click();

            // Open year dropdown
            var yearReadView = _wait.Until(d => d.FindElement(By.ClassName("react-datepicker__year-read-view")));
            yearReadView.Click();

            // Calculate how many times to click "years-upcoming"
            int maxDisplayedYear = GetMaxDisplayedYear();
            int clicksNeeded = Math.Max(0, year - maxDisplayedYear);

            for (int i = 0; i < clicksNeeded; i++)
            {
                var nextYearsButton = _driver.FindElements(By.ClassName("react-datepicker__navigation--years-upcoming")).FirstOrDefault();
                if (nextYearsButton != null)
                {
                    nextYearsButton.Click();
                    _wait.Until(d => GetMaxDisplayedYear() > maxDisplayedYear);
                    maxDisplayedYear = GetMaxDisplayedYear();
                }
                else
                {
                    throw new Exception("No navigation button present to show more years.");
                }
            }

            // Now select the year
            var yearOptions = _driver.FindElements(By.XPath("//div[contains(@class,'react-datepicker__year-option') and not(contains(@class,'navigation'))]"));
            var yearOption = yearOptions.FirstOrDefault(opt => opt.Text.Trim() == year.ToString());
            if (yearOption != null)
            {
                yearOption.Click();
            }
            else
            {
                throw new Exception($"Year {year} not found after navigation.");
            }

            // Select day
            var dayCells = _driver.FindElements(By.XPath($"//div[contains(@class,'react-datepicker__day') and not(contains(@class,'outside-month')) and text()='{day}']"));
            foreach (var cell in dayCells)
            {
                if (cell.Displayed && cell.Enabled)
                {
                    cell.Click();
                    break;
                }
            }

            // Select time
            var timeListItem = _wait.Until(d => d.FindElement(By.XPath($"//li[contains(@class,'react-datepicker__time-list-item') and text()='{time}']")));
            timeListItem.Click();
        }
    }
}