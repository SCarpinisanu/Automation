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
    public class PracticeFormsTest : BasePage.BasePage
    {
        [Test]
        public void PracticeFormsTest1()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            elementMethods = new ElementMethods(webDriver!);
            practiceFormsPage = new PracticeFormsPage(webDriver!);

            var practiceFormsData = new PracticeFormsData(1); // Assuming 1 is the dataset number

            homePage.ClickOnOption(2);

            commonPage.GoToDesiredMenuItem("Practice Forms");

            practiceFormsPage.CompleteForm(practiceFormsData); // Fill the form with data from XML file

        }
    }
}
