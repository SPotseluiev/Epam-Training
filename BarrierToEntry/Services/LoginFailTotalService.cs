using BarrierToEntry.Models;
using Newtonsoft.Json;
using System.Text;
using System.Web;

namespace BarrierToEntry.Services
{
    public class LoginFailTotalService
    {
        private HttpClient _client;
        private string _baseUri = "example";

        public LoginFailTotalService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseUri);
        }

        public async Task<HttpResponseMessage> GetLoginFailTotal(string userName, int? failCount, int? fetchLimit)
        {
            var requestUri = new UriBuilder(String.Concat(_baseUri,"/loginfailtotal"));
            var query = HttpUtility.ParseQueryString(requestUri.Query);
            if (userName != null) query["user_name"] = userName;
            if (failCount != null) query["fail_count"] = failCount.ToString();
            if (fetchLimit != null) query["fetch_limit"] = fetchLimit.ToString();
            requestUri.Query = query.ToString();

            return await _client.GetAsync(requestUri.ToString());
        }

        public async Task<HttpResponseMessage> ResetLoginFailTotal(string userName, User user)
        {
            var resetUri = String.Concat(_baseUri,$"/resetloginfailtotal?username={userName}");
            return await _client.PutAsync(resetUri,
                new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
        }

        public async Task<User?> GetUser(string userName)
        {
            var response = await GetLoginFailTotal(userName, null, 1);
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(responseBody);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
