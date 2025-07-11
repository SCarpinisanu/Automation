using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Automation.Access;
using Automation.HelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automation.Pages
{
    public class WebTablesPage(IWebDriver webWebDriver)
    {
        public IWebDriver webWebDriver = webWebDriver;
        public ElementMethods elementMethods = new(webWebDriver);

        IWebElement AddNewRecordButton => webWebDriver.FindElement(By.Id("addNewRecordButton"));

        public void ClickAddNewRecordButton()
        {
            ElementMethods.ClickOnElement(AddNewRecordButton);
        }

        IWebElement FirstName => webWebDriver.FindElement(By.Id("firstName"));
        IWebElement LastName => webWebDriver.FindElement(By.Id("lastName"));
        IWebElement Email => webWebDriver.FindElement(By.Id("userEmail"));
        IWebElement Age => webWebDriver.FindElement(By.Id("age"));
        IWebElement Salary => webWebDriver.FindElement(By.Id("salary"));
        IWebElement Department => webWebDriver.FindElement(By.Id("department"));

        public void FillFields(string firstName, string lastName, string email, string age, string salary, string department)
        {
            ElementMethods.FillElement(FirstName, firstName);
            ElementMethods.FillElement(LastName, lastName);
            ElementMethods.FillElement(Email, email);
            ElementMethods.FillElement(Age, age);
            ElementMethods.FillElement(Salary, salary);
            ElementMethods.FillElement(Department, department);
        }

        public void FillFieldsXML(WebTablesData webTablesData)
        {
            ElementMethods.FillElement(FirstName, webTablesData.FirstName!);
            ElementMethods.FillElement(LastName, webTablesData.LastName!);
            ElementMethods.FillElement(Email, webTablesData.UserEmail!);
            ElementMethods.FillElement(Age, webTablesData.Age!);
            ElementMethods.FillElement(Salary, webTablesData.Salary!);
            ElementMethods.FillElement(Department, webTablesData.Department!);
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

        public void SwitchToFrame(string frameIdOrName)
        {
            webWebDriver.SwitchTo().Frame(frameIdOrName);
        }
    }
}
