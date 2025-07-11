using Automation.Access;
using Automation.HelperMethods;
using Automation.Pages;
using Automation.Pages.PracticeForm;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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

            WebTablesData webTablesData = new(2);
            
            homePage.ClickOnOption(0);
            commonPage.GoToDesiredMenuItem("Web Tables");

            webTablesPageDDT.AddNewRecordButton();
            webTablesPageDDT.FillFields(webTablesData);

            webTablesPageDDT.FormSubmit();
            Thread.Sleep(1000);
        }
    }
}
