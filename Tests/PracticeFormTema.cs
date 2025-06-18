using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Access;
using Automation.HelperMethods;
using Automation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace Automation.Tests
{
    public class PracticeFormTema : BasePage.BasePage
    {
        [Test]
        public void PracticeFormTemaTest()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            elementMethods = new ElementMethods(webDriver!);
            practiceFormsPage = new PracticeFormPage(webDriver!);

            var practiceFormsData = new PracticeFormsData(3);

            homePage.ClickOnOption(1);
            commonPage.GoToDesiredMenuItem("Practice Form");
            practiceFormsPage.CompleteForm(practiceFormsData);
            practiceFormsPage.SubmitForm();

            // Wait for the modal to appear
            var wait = new WebDriverWait(webDriver!, TimeSpan.FromSeconds(5));
            var modalTitle = wait.Until(driver =>
                driver.FindElement(By.CssSelector("div.modal-title.h4#example-modal-sizes-title-lg")));

            Thread.Sleep(1000); // Optional: Wait for the modal to fully render
            Assert.That(modalTitle.Text, Is.EqualTo("Thanks for submitting the form"));
            practiceFormsPage.CloseModal();
        }
    }
}
