using BarrierToEntry.Drivers;
using OpenQA.Selenium;

namespace BarrierToEntry.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private IWebDriver _driver = DriverHelper.driver;

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            _driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
        }
    }
}
