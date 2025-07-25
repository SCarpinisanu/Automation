﻿using Automation.Access;
using OpenQA.Selenium;
using Automation.HelperMethods;

namespace Automation.Pages
{
    public class WebTablesPageDDT(IWebDriver webDriver)
    {
        private readonly IWebDriver webDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));

        public ElementMethods elementMethods { get; } = new ElementMethods(webDriver);

        private IWebElement FirstName => webDriver.FindElement(By.Id("firstName"));
        private IWebElement LastName => webDriver.FindElement(By.Id("lastName"));
        private IWebElement Email => webDriver.FindElement(By.Id("userEmail"));
        private IWebElement Age => webDriver.FindElement(By.Id("age"));
        private IWebElement Salary => webDriver.FindElement(By.Id("salary"));
        private IWebElement Department => webDriver.FindElement(By.Id("department"));

        private IWebElement AddNewRecord => webDriver.FindElement(By.Id("addNewRecordButton"));

        public void AddNewRecordButton()
        {
            ElementMethods.ClickOnElement(AddNewRecord); 
        }

        public void FillFields(WebTablesData webTablesData)
        {
            ArgumentNullException.ThrowIfNull(webTablesData);

            ElementMethods.FillElement(FirstName, webTablesData.FirstName!); 
            ElementMethods.FillElement(LastName, webTablesData.LastName!); 
            ElementMethods.FillElement(Email, webTablesData.UserEmail!); 
            ElementMethods.FillElement(Age, webTablesData.Age!); 
            ElementMethods.FillElement(Salary, webTablesData.Salary!); 
            ElementMethods.FillElement(Department, webTablesData.Department!); 
        }

        private IWebElement ButtonSubmit => webDriver.FindElement(By.Id("submit"));

        public void FormSubmit()
        {
            ButtonSubmit.Click();
        }

        public string[] GetRowDataByEmail(string targetEmail)
        {
            var rows = webDriver.FindElements(By.CssSelector(".rt-tr-group"));

            foreach (var row in rows)
            {
                var cells = row.FindElements(By.CssSelector(".rt-td"));
                if (cells.Count >= 6 && cells[3].Text.Equals(targetEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return [.. cells.Take(6).Select(cell => cell.Text)];
                }
            }

            throw new Exception($"Rândul cu emailul '{targetEmail}' nu a fost găsit.");
        }

    }
}