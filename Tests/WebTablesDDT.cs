using Automation.Access;
using Automation.HelperMethods;
using Automation.Pages;
using Automation.Pages.PracticeForm;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Tests
{
    public class WebTablesDDT : BasePage.BasePage
    {
        public WebTablesData? GetWebTablesData()
        {
            return webTablesData;
        }

        [Test]
        public void WebTablesPageDDTTest()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            elementMethods = new ElementMethods(webDriver!);
            webTablesPageDDT = new WebTablesPageDDT(webDriver!);

            WebTablesData webTablesData = new(1);
            
            homePage.ClickOnOption(0);
            commonPage.GoToDesiredMenuItem("Web Tables");

            webTablesPageDDT.AddNewRecordButton();
            webTablesPageDDT.FillFields(webTablesData);

            webTablesPageDDT.FormSubmit();
            Thread.Sleep(1000);

            var actualRow = webTablesPageDDT.GetRowDataByEmail(webTablesData.UserEmail);

            string[] expectedRow =
            {
                webTablesData.FirstName,
                webTablesData.LastName,
                webTablesData.Age,
                webTablesData.UserEmail,
                webTablesData.Salary,
                webTablesData.Department
            };

            Assert.That(actualRow, Is.EqualTo(expectedRow).AsCollection, 
                "Datele găsite în tabel nu corespund celor introduse.");

        }
    }
}
