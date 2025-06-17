using Automation.BasePage;
using Automation.HelperMethods;
using Automation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automation
{
    public class DemoQAPractice : BasePage.BasePage
    {
        [Test]
        public void ClickOnOneCardFromHomePageMethod()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            elementMethods = new ElementMethods(webDriver!);

            homePage.ClickOnOption(1);
        }

        [Test]
        public void PracticeFormsDatePickerMethod()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            practiceFormsPage = new PracticeFormsPage(webDriver!);

            homePage.ClickOnOption(1);

            commonPage.GoToDesiredMenuItem("Practice Form");

            practiceFormsPage.SelectDOB("November", "2009", "13");
        }

        [Test]
        public void ElementsOptions()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            practiceFormsPage = new PracticeFormsPage(webDriver!);

            homePage.ClickOnOption(1);

            commonPage.GoToDesiredMenuItem("Practice Form");

            practiceFormsPage.CompleteFirstRegion(
                "Ion", "Popescu", "Ion.Pop@email.com", "0775 236 225", "Pitesti, str. Popa Sapca, 2");
            practiceFormsPage.SelectDOB("February", "1967", "06");
            practiceFormsPage.ChooseGender("Male");
            practiceFormsPage.SelectHobbiesByName("Reading", "Musical");
        }

        [Test]
        public void WebTablesPractice()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            webTablesPage = new WebTablesPage(webDriver!);

            homePage.ClickOnOption(0);

            commonPage.GoToDesiredMenuItem("Web Tables");

            webTablesPage.ClickAddNewRecordButton();

            webTablesPage.FillFields("Ion", "Ionescu-Mizil", "ion.ionescu@mail.com", "28", "7800", "IT");
            webTablesPage.SubmitForm();

            // Assertion for the new record
            Assert.That(
                webTablesPage.IsRecordPresent("Ion", "Ionescu-Mizil", "ion.ionescu@mail.com", "28", "7800", "IT"),
                Is.True,
                "The new record was not found in the table with the expected values."
            );
        }

        [Test]
        public void FramesPagePractice()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            framesPage = new FramesPage(webDriver!);

            homePage.ClickOnOption(2);

            commonPage.GoToDesiredMenuItem("Frames");

            framesPage.GetTextFromFrameOne();
            System.Threading.Thread.Sleep(1000);

            framesPage.ScrollOnFrameTwo();
            System.Threading.Thread.Sleep(1000);
        }

        [Test]
        public void ModalDialogsPageTest()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            modalDialogsPage = new ModalDialogsPage(webDriver!);

            homePage.ClickOnOption(2);
            commonPage.GoToDesiredMenuItem("Modal Dialogs");

            // Small Modal
            var (smallWords, smallPunctuation) = modalDialogsPage.HandleSmallModal();
            Console.WriteLine($"Small Modal - Word count: {smallWords}, Punctuation count: {smallPunctuation}");
            System.Threading.Thread.Sleep(1000);
            modalDialogsPage.CloseSmallModal();

            // Large Modal
            var (largeWords, largePunctuation) = modalDialogsPage.HandleLargeModal();
            Console.WriteLine($"Large Modal - Word count: {largeWords}, Punctuation count: {largePunctuation}");
            System.Threading.Thread.Sleep(1000);
            modalDialogsPage.CloseLargeModal();
        }

        [Test]
        public void TabsPagePractice()
        {
            // Initialize required pages and methods
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);

            // Navigate to the section containing the Tabs page
            homePage.ClickOnOption(3); // Adjust index if needed for your navigation
            commonPage.GoToDesiredMenuItem("Tabs");

            // Create and use TabsPage
            var tabsPage = new TabsPage(webDriver!);
            tabsPage.ProcessTabs();

            // Optionally, add assertions here if ProcessTabs returns values to verify
            // For now, ProcessTabs writes output to the console as per requirements
        }
    }
}
