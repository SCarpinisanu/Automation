using Automation.Access;
using Automation.HelperMethods;
using Automation.Pages;
using OpenQA.Selenium;
using Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Tests
{
    public class DatePickerTest : BasePage.BasePage
    {
        [Test]
        public void DatePickerMethod()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            elementMethods = new ElementMethods(webDriver!);
            datePicker = new DatePicker(webDriver!);
            homePage.ClickOnOption(3); // Click on the first option in the home page
            commonPage.GoToDesiredMenuItem("Date Picker"); // Navigate to the Date Picker page
            
            datePicker.SelectDate("March", 2100, 19); // Select a date in the "Select Date" field
            Thread.Sleep(1000); // Wait for a second to see the selected date
            datePicker.SelectDateAndTime("February", 2052, 6, "10:30"); // Select a date and time in the "Date And Time" field
            Thread.Sleep(1000); // Wait for a second to see the selected date and time
        }
    }
}