using Automation.Access;
using Automation.HelperMethods;
using Automation.Pages;
using OpenQA.Selenium;
using Automation.Pages.PracticeForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace Automation.Tests
{
    public class PracticeFormNew : BasePage.BasePage
    {
        [Test]
        public void PracticeFormNewTest()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            elementMethods = new ElementMethods(webDriver!);
            practiceForm = new PracticeForm(webDriver!);

            CompleteFirstRegion completeFirstRegion = new(webDriver!);
            ChooseGender chooseGender = new(webDriver!);
            //DateOfBirth dateOfBirth = new(webDriver!);
            DateOfBirth2 dateOfBirth2 = new(webDriver!);
            Pages.PracticeForm.Submit submitClick = new(webDriver!);
            Pages.PracticeForm.CloseModalPage closeModal = new(webDriver!);
            Pages.PracticeForm.InputSubjects inputSubjects = new(webDriver!);

            PracticeFormsData practiceFormsData = new(1);

            homePage.ClickOnOption(1);
            commonPage.GoToDesiredMenuItem("Practice Form");

            practiceForm.FillFirstFields(practiceFormsData);
            practiceForm.SelectGender(practiceFormsData);

            inputSubjects.FillSubjects(practiceFormsData); 
            
            //dateOfBirth.SelectDateOfBirth(practiceFormsData);
            dateOfBirth2.SelectDOB(practiceFormsData);

            submitClick.SubmitAction();

            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(webDriver!, TimeSpan.FromSeconds(5));
            var modalTitle = wait.Until(driver =>
                driver.FindElement(By.CssSelector("div.modal-title.h4#example-modal-sizes-title-lg")));
            Thread.Sleep(1000); // Optional: Wait for the modal to fully render  
            Assert.That(modalTitle.Text, Is.EqualTo("Thanks for submitting the form"));

            closeModal.CloseModal();
        }
    }
}
