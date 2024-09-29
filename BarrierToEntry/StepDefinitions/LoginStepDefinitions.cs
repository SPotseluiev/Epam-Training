using BarrierToEntry.Drivers;
using BarrierToEntry.PageObjects;
using NUnit.Framework;

namespace BarrierToEntry.StepDefinitions
{
    [Binding]
    public sealed class LoginStepDefinitions
    {
        private Context _testContext;
        public LoginStepDefinitions(Context testContext)
        {
            _testContext = testContext;
            _testContext.LoginPage = new LoginPage(DriverHelper.driver);
            _testContext.HomePage = new HomePage(DriverHelper.driver);
        }

        [Given("I am on the login screen for the site '(.*)'")]
        public void GivenIAmOnTheLoginPageForTheSite(string site)
        {
            _testContext.LoginPage.NavigateTo(site);
        }

        [When("I enter a valid username '(.*)' and password '(.*)' and submit")]
        public void WhenIEnterValidUserNameAndPasswordAndSubmit(string userName, string password)
        {
            _testContext.LoginPage.Login(userName, password);
        }

        [Then("I am logged in successfully to the '(.*)'")]
        public void ThenIAmLoggedInSuccessfullyToTheSite(string site)
        {
            Assert.AreEqual(_testContext.HomePage.GetCurrentURL(), site);
            Assert.True(_testContext.HomePage.IsHomePageElementDisplayed(), 
                "Home Page element is not visible on the webpage.");
        }

        [Given("I am not logged in with a genuine user")]
        public void GivenIAmNotLoggedInWithAGenuineUser()
        {
            _testContext.HomePage.ClickLogOut();
        }

        [When("I navigate to the home page on the tracking site")]
        public void WhenINavigateToThePageOfTheTrackingSite()
        {
            _testContext.HomePage.NavigateToHomePage();
        }

        [Then("I am presented with a login screen for the '(.*)'")]
        public void ThenIAmPresentedWithALoginScreenForTheSite(string page)
        {
            Assert.AreEqual(_testContext.LoginPage.GetCurrentUrl(), page);
        }
    }
}
