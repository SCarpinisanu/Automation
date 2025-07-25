﻿using Automation.Access;
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
            Submit submitClick = new(webDriver!);
            CloseModalPage closeModal = new(webDriver!);
            InputSubjects inputSubjects = new(webDriver!);
            CurrentAddress currentAddress = new(webDriver!);
            StateAndCity stateAndCity = new(webDriver!);

            PracticeFormsData practiceFormsData = new(1);

            homePage.ClickOnOption(1);
            commonPage.GoToDesiredMenuItem("Practice Form");

            practiceForm.FillFirstFields(practiceFormsData);
            practiceForm.SelectGender(practiceFormsData);

            inputSubjects.FillSubjects(practiceFormsData); 
            
            dateOfBirth2.SelectDOB(practiceFormsData);

            currentAddress.FillCurrentAddress(practiceFormsData);

            stateAndCity.SelectStateAndCity(practiceFormsData);

            submitClick.SubmitAction();

            try
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(webDriver!, TimeSpan.FromSeconds(5));
                var modalTitle = wait.Until(driver =>
                    driver.FindElement(By.CssSelector("div.modal-title.h4#example-modal-sizes-title-lg")));
                Thread.Sleep(1000); // Optional: Wait for the modal to fully render  
                Assert.That(modalTitle.Text, Is.EqualTo("Thanks for submitting the form"));
            }
            catch (WebDriverTimeoutException ex)
            {
                Assert.Fail($"Modal did not appear within the expected time: {ex.Message}");
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail($"Modal title element not found: {ex.Message}");
            }
            catch (AssertionException ex)
            {
                Assert.Fail($"Assertion failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Unexpected error: {ex.Message}");
            }

            closeModal.CloseModal();
        }
    }
}
