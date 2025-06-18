using Automation.HelperMethods;
using Automation.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation
{
    public class AlertsTest : BasePage.BasePage
    {
        public IWebDriver? webDriver;
        [Test]
        public void AlertButtonsTests()
        {
            homePage = new HomePage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            elementMethods = new ElementMethods(webDriver!);
            alertsPage = new AlertsPage(webDriver!);
            commonPage = new CommonPage(webDriver!);
            homePage.ClickOnOption(2);
            commonPage.GoToDesiredMenuItem("Alerts");

            alertsPage.ClickAlertButton();
            Thread.Sleep(1000);
            alertsPage.AcceptAlert();

            alertsPage.ClickTimerAlertButton();
            //Thread.Sleep(6000);
            alertsPage.WaitForAlertToBePresent(6);
            Thread.Sleep(1000);
            alertsPage.AcceptAlert();

            alertsPage.ClickConfirmButton();
            Thread.Sleep(1000);
            alertsPage.DismissAlert();

            alertsPage.ClickConfirmButton();
            Thread.Sleep(1000);
            alertsPage.AcceptAlert();
            Thread.Sleep(1000);

            alertsPage.ClickPromptButton();
            Thread.Sleep(1000);
            alertsPage.SendKeysToAlert("Hello, Sorinel!");
            Thread.Sleep(1000);
            alertsPage.AcceptAlert();
            Thread.Sleep(1000);
        }
    }
}
