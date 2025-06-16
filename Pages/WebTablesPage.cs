using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Automation.HelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automation.Pages
{
    public class WebTablesPage
    {
        public IWebDriver webWebDriver;
        public ElementMethods elementMethods;

        public WebTablesPage(IWebDriver webWebDriver)
        {
            this.webWebDriver = webWebDriver;
            elementMethods = new ElementMethods(webWebDriver);
        }

        IWebElement AddNewRecordButton => webWebDriver.FindElement(By.Id("addNewRecordButton"));

        public void ClickAddNewRecordButton()
        {
            elementMethods.ClickOnElement(AddNewRecordButton);
        }

        IWebElement FirstName => webWebDriver.FindElement(By.Id("firstName"));
        IWebElement LastName => webWebDriver.FindElement(By.Id("lastName"));
        IWebElement Email => webWebDriver.FindElement(By.Id("userEmail"));
        IWebElement Age => webWebDriver.FindElement(By.Id("age"));
        IWebElement Salary => webWebDriver.FindElement(By.Id("salary"));
        IWebElement Department => webWebDriver.FindElement(By.Id("department"));

        public void FillFields(string firstName, string lastName, string email, string age, string salary, string department)
        {
            elementMethods.FillElement(FirstName, firstName);
            elementMethods.FillElement(LastName, lastName);
            elementMethods.FillElement(Email, email);
            elementMethods.FillElement(Age, age);
            elementMethods.FillElement(Salary, salary);
            elementMethods.FillElement(Department, department);
        }

        private IWebElement SubmitButton => webWebDriver.FindElement(By.Id("submit"));

        public void SubmitForm()
        {
            SubmitButton.Click();
        }

        public bool IsRecordPresent(string firstName, string lastName, string email, string age, string salary, string department)
        {
            var rows = webWebDriver.FindElements(By.CssSelector(".rt-tbody .rt-tr-group"));
            foreach (var row in rows)
            {
                var cells = row.FindElements(By.CssSelector(".rt-td"));
                if (cells.Count < 6) continue;
                if (cells[0].Text == firstName &&
                    cells[1].Text == lastName &&
                    cells[2].Text == age &&
                    cells[3].Text == email &&
                    cells[4].Text == salary &&
                    cells[5].Text == department)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
