using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomotiveBddProject.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; }
        protected WebDriverWait Wait { get; }

        protected BasePage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
    }
}
