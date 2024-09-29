using OpenQA.Selenium;

namespace BarrierToEntry.PageObjects
{
    public class LoginPage
    {
        private IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        IWebElement _userNameField => _driver.FindElement(By.Id("userName"));
        IWebElement _passwordField => _driver.FindElement(By.Id("password"));
        IWebElement _submitButton => _driver.FindElement(By.Id("submitButton"));

        public void EnterUserName(string userName)
        {
            _userNameField.Clear();
            _userNameField.SendKeys(userName);
        }

        public void EnterPassword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);
        }

        public void ClickSubmit()
        {
            _submitButton.Click();
        }

        public void Login(string userName, string password)
        {
            EnterUserName(userName);
            EnterPassword(password);
            ClickSubmit();
        }

        public string GetCurrentUrl() => _driver.Url;

        public void NavigateTo(string site)
        {
            _driver.Navigate().GoToUrl(site);
        }
    }
}
