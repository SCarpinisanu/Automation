﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Automation.HelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Automation.Pages
{
    public class AlertsPage(IWebDriver webDriver)
    {
        public IWebDriver webDriver = webDriver;
        public ElementMethods elementMethods = new ElementMethods(webDriver);

        public IWebElement AlertButton => webDriver.FindElement(By.Id("alertButton"));
        public IWebElement TimerAlertButton => webDriver.FindElement(By.Id("timerAlertButton"));
        public IWebElement ConfirmButton => webDriver.FindElement(By.Id("confirmButton"));
        public IWebElement PromptButton => webDriver.FindElement(By.Id("promtButton"));
        public IAlert Alert => webDriver.SwitchTo().Alert();

        public void ClickAlertButton()
        {
            ElementMethods.ClickOnElement(AlertButton);
            Console.WriteLine("Clicked on alertButton");
        }

        public void ClickTimerAlertButton()
        {
            ElementMethods.ClickOnElement(TimerAlertButton);
            Console.WriteLine("Clicked on timerAlertButton");
        }
        public void ClickConfirmButton()
        {
            ElementMethods.ClickOnElement(ConfirmButton);
            Console.WriteLine("Clicked on confirmButton");
        }
        public void ClickPromptButton()
        {
            ElementMethods.ClickOnElement(PromptButton);
            Console.WriteLine("Clicked on promtButton");
        }

        public void AcceptAlert()
        {
            Alert.Accept();
            Console.WriteLine("Clicked on OK button (Accept)");
        }
        public void DismissAlert()
        {
            Alert.Dismiss();
            Console.WriteLine("Clicked on Candel button (Dismiss)");
        }
        public void SendKeysToAlert(string text)
        {
            Alert.SendKeys(text);
            Console.WriteLine($"Sent text '{text}' to alert");
        }
        public void WaitForAlertToBePresent(int timeoutInSeconds = 10)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(ExpectedConditions.AlertIsPresent());
                Console.WriteLine("Alert displayed after 5 seconds - clicked OK button");
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"Alert was not displayed within {timeoutInSeconds} seconds. Exception: {ex.Message}");
            }
        }
    }
}
