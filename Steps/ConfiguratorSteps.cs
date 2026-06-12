using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using AutomotiveBddProject.Pages;
using System.IO;

namespace AutomotiveBddProject.Steps
{
    [Binding]
    public class ConfiguratorSteps
    {
        private IWebDriver _driver;
        private ConfiguratorPage _configuratorPage;
        private readonly ScenarioContext _scenarioContext;

        public ConfiguratorSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headed");
            options.AddArgument("--start-maximized");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-gpu");

            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();

            _configuratorPage = new ConfiguratorPage(_driver);
        }

        [Given(@"I navigate to the automotive portal")]
        public void GivenINavigateToTheAutomotivePortal() => _driver.Navigate().GoToUrl("https://gm.com");

        [When(@"I choose to configure an SUV model")]
        public void WhenIChooseToConfigureAnSUVModel() => _driver.Navigate().GoToUrl("https://gm.com/suvs");

        [Then(@"I should see the configuration page")]
        public void ThenIShouldSeeTheConfigurationPage()
        {
            Console.WriteLine($"[Test Log] Navigated Page Destination URL: {_configuratorPage.Url}");
            Assert.That(_configuratorPage.IsOnConfigurator(), $"Assertion Failed! The driver landed on an unexpected page: {_configuratorPage.Url}");
        }

        [AfterScenario]
        public void Teardown()
        {
            if (_scenarioContext.TestError != null && _driver != null)
            {
                try
                {
                    string screenshotFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FailureScreenshots");
                    Directory.CreateDirectory(screenshotFolder);

                    string fileName = $"Failure_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    string fullPath = Path.Combine(screenshotFolder, fileName);

                    ITakesScreenshot screenshotDriver = (ITakesScreenshot)_driver;
                    Screenshot screenshot = screenshotDriver.GetScreenshot();
                    File.WriteAllBytes(fullPath, screenshot.AsByteArray);

                    Console.WriteLine($"[FAILURE DETECTED] Screenshot saved successfully at: {fullPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Warning] Could not capture failure screenshot: {ex.Message}");
                }
            }

            _driver?.Quit();
            _driver?.Dispose();
        }
    }
}
