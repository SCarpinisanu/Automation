using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Access;
using Automation.HelperMethods;
using Automation.Pages;

namespace Automation.Tests
{
    public class WebTables : BasePage.BasePage
    {
        [Test]
        public void WebTablesTest()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            elementMethods = new ElementMethods(webDriver!);
            webTablesPage = new WebTablesPage(webDriver!);
            var webTablesData = new WebTablesData(1); // Assuming 1 is the dataset number

            homePage.ClickOnOption(0); // Click on the first option in the home page

            commonPage.GoToDesiredMenuItem("Web Tables"); // Navigate to the Web Tables page

            webTablesPage.ClickAddNewRecordButton(); // Click on the "Add New Record" button

            webTablesPage.FillFieldsXML(webTablesData); // Fill data from XML file

            webTablesPage.SubmitForm(); // Submit the form
        }
    }
}
