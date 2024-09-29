using BarrierToEntry.Models;
using BarrierToEntry.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using System.Net;

namespace BarrierToEntry.Features
{
    [TestFixture]
    public class LoginFailTotalTests
    {
        LoginFailTotalService _loginFailTotalService;

        [SetUp]
        public void Setup()
        {
            _loginFailTotalService = new LoginFailTotalService();
        }

        [TestCase("validUser", null, 1, HttpStatusCode.OK)]
        [TestCase(null, 3, null, HttpStatusCode.OK)]
        [TestCase(null, 2, 10, HttpStatusCode.OK)]
        [TestCase("nonExistentUser", null, 1, HttpStatusCode.NoContent)]
        public async Task GetLoginFailTotalReturnsCorrectStatusCode(string userName, int? failCount, int? fetchLimit, HttpStatusCode expectedStatusCode)
        {
            var response = await _loginFailTotalService.GetLoginFailTotal(userName, failCount, fetchLimit);

            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [TestCase("validUser", HttpStatusCode.OK)]
        [TestCase("nonExistentUser", HttpStatusCode.NotFound)]
        public async Task ResetLoginFailTotalReturnsCorrectStatusCode(string userName, HttpStatusCode expectedStatusCode)
        {
            var response = await _loginFailTotalService.ResetLoginFailTotal(userName, new User());

            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [TestCase("validUser", null, 1, "User.json")]
        [TestCase(null, 3, 10, "Users.json")]
        public async Task GetLoginFailTotalUserSchemaIsCorrect(string userName, int? failCount, int? fetchLimit, string jsonFile)
        {
            var response = await _loginFailTotalService.GetLoginFailTotal(userName, failCount, fetchLimit);
            var responseBody = await response.Content.ReadAsStringAsync();

            var schemaJson = File.ReadAllText($"../Schemas/{jsonFile}");
            var schema = JsonSchema.Parse(schemaJson);

            var responseJson = JObject.Parse(responseBody);

            Assert.IsTrue(responseJson.IsValid(schema), $"Response body does not match the {jsonFile} schema");
        }

        [TestCase("resetTestUser")]
        public async Task ResetFailedLoginAsExpected(string userName)
        {
            // Set User
            var testUser = new User() { UserName = "User1", FailCount = 3 };

            await _loginFailTotalService.ResetLoginFailTotal(userName, testUser);

            var atualUser = await _loginFailTotalService.GetUser(userName);
            Assert.AreEqual(testUser.FailCount, atualUser?.FailCount);

            // Reset user
            await _loginFailTotalService.ResetLoginFailTotal(userName, new User());

            var user = await _loginFailTotalService.GetUser(userName);
            Assert.True(user?.FailCount == 0);
        }

        [TearDown]
        public void Teardown()
        {
            _loginFailTotalService.Dispose();
        }
    }
}
