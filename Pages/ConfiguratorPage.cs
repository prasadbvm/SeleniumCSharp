using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomotiveBddProject.Pages
{
    public class ConfiguratorPage : BasePage
    {
        public ConfiguratorPage(IWebDriver driver) : base(driver)
        {
        }

        public string Url => Driver.Url;

        public bool IsOnConfigurator()
        {
            var url = Url.ToLowerInvariant();
            return url.Contains("byo") || url.Contains("configurator") || url.Contains("shopping");
        }
    }
}
