using OpenQA.Selenium;

namespace BarrierToEntry.PageObjects
{
    public class HomePage
    {
        private IWebDriver _driver;

        public HomePage(IWebDriver driver)
        {
            this._driver = driver;
        }

        private string _homePageUrl => "https://homepage.com";
        IWebElement _logOutButton => _driver.FindElement(By.Id("logOutButton"));
        IWebElement _homePageElement => _driver.FindElement(By.Id("homeElement"));

        public void NavigateToHomePage() => _driver.Navigate().GoToUrl(_homePageUrl);

        public void ClickLogOut() => _logOutButton.Click();

        public bool IsHomePageElementDisplayed() => _homePageElement.Displayed;

        public string GetCurrentURL() => _driver.Url;
    }
}
