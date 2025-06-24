using Automation.Access;
using Automation.HelperMethods;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Pages.PracticeForm
{
    public class PracticeForm
    {
        public IWebDriver WebDriver { get; }

        public CompleteFirstRegion? completeFirstRegion;
        public ChooseGender? chooseGender;
        public DateOfBirth? dateOfBirth;

        public PracticeForm(IWebDriver webDriver)
        {
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        }

        public void FillFirstFields(PracticeFormsData practiceFormsData)
        {
            completeFirstRegion = new CompleteFirstRegion(WebDriver);
            if (completeFirstRegion == null)
            {
                throw new InvalidOperationException("CompleteFirstRegion is not initialized.");
            }
            completeFirstRegion.FillFirstRegion(practiceFormsData);
        }

        public void SelectGender(PracticeFormsData practiceFormsData)
        {
            // Updated constructor call to match the signature of ChooseGender
            chooseGender = new ChooseGender(WebDriver);
            if (chooseGender == null)
            {
                throw new InvalidOperationException("ChooseGender is not initialized.");
            }
            chooseGender.SelectGender(practiceFormsData);
        }

        public void SelectDateOfBirth(PracticeFormsData practiceFormsData)
        {
            dateOfBirth = new DateOfBirth(WebDriver);
            if (dateOfBirth == null)
            {
                throw new InvalidOperationException("DateOfBirth is not initialized.");
            }
            dateOfBirth.SelectDateOfBirth(practiceFormsData);
        }
    }
}
