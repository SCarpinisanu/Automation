using Automation.Access;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Pages.PracticeForm
{
    public class StateAndCity(IWebDriver webDriver) : PracticeForm(webDriver)
    {
        private readonly IWebDriver webDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
    
        IWebElement SelectState => webDriver.FindElement(By.Id("state"));
        public void SelectStateAndCity(PracticeFormsData practiceFormsData)
        {
            // Scroll to the state dropdown
            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", SelectState);

            // Select State
            var stateDropdown = webDriver.FindElement(By.Id("state"));
            stateDropdown.Click();

            if (string.IsNullOrWhiteSpace(practiceFormsData.StateChosen))
            {
                Console.WriteLine("State is empty or null, skipping state selection.");
                Console.WriteLine("No City selection available.");
                return;
            }
            else
            {
                var stateOptions = webDriver.FindElements(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.StateChosen}']"));
                if (stateOptions.Count == 0)
                {
                    Console.WriteLine($"State '{practiceFormsData.StateChosen}' not found in the dropdown.");
                    Console.WriteLine("Skipping state and city selection.");
                    return;
                }
                else
                {
                    var stateWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5)); // Renamed 'wait' to 'stateWait'
                    stateWait.Until(d => d.FindElement(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.StateChosen}']")));

                    var stateOption = webDriver.FindElement(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.StateChosen}']"));
                    stateOption.Click();

                    // Așteaptă actualizarea dropdownului pentru city
                    var cityDropdown = webDriver.FindElement(By.Id("city"));
                    cityDropdown.Click();

                    if (string.IsNullOrWhiteSpace(practiceFormsData.CityChosen))
                    {
                        Console.WriteLine("City is empty or null, skipping state and city selection.");
                        return;
                    }
                    else
                    {
                        var cityOptions = webDriver.FindElements(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.CityChosen}']"));
                        if (cityOptions.Count == 0)
                        {
                            Console.WriteLine($"City '{practiceFormsData.CityChosen}' not found in the dropdown.");
                            Console.WriteLine("Skipping state and city selection");
                            return;
                        }
                        else
                        {
                            var cityWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5)); // Renamed 'wait' to 'cityWait'
                            cityWait.Until(d => d.FindElement(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.CityChosen}']")));

                            var cityOption = webDriver.FindElement(By.XPath($"//div[contains(@id, 'react-select') and text()='{practiceFormsData.CityChosen}']"));
                            cityOption.Click();
                        }
                    }
                }
            }
        }
    }
}
