using Auth.Models;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ApiTests
{
    public class AuthControllerTestsBase
    {
        public const string BaseApi = "https://localhost:5001";


        // Helpers

        public async Task<(JsonObject?, HttpStatusCode)> PostData(string url, object content)
        {
            var client = new HttpClient();
            var requestContent = new StringContent(
                JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync(url, requestContent);
            var data = await response.Content.ReadAsStringAsync();
            return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
        }

        public async Task<HttpStatusCode> PostDataWithAuth(string url, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync(url, null);
            return response.StatusCode;
        }

        public async Task<string> GetAuthToken()
        {
            var loginDetails = new LoginModel
            {
                Email = AppTestUserInfo.Email,
                Password = AppTestUserInfo.Password
            }; ;

            var (response, _) = await PostData($"{BaseApi}/api/auth/login", loginDetails);
            return response?.Value("token") ?? string.Empty;
        }

       public async Task<string> GetUserIdFromToken(string token)
        {
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;
            return jsonToken?.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value ?? string.Empty;
        }


        public async Task<(JsonObject?, HttpStatusCode)> GetData(string url, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
        }

        public async Task<(JsonArray?, HttpStatusCode)> GetArray(string url, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            return (JsonSerializer.Deserialize<JsonArray>(data), response.StatusCode);
        }

        public async Task<(JsonObject?, HttpStatusCode)> PutData(string url, object content, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var requestContent = new StringContent(
                JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json");
            var response = await client.PutAsync(url, requestContent);
            var data = await response.Content.ReadAsStringAsync();
            return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
        }

        public async Task<HttpStatusCode> DeleteData(string url, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync(url);
            return response.StatusCode;
        }

        public async Task<(JsonObject?, HttpStatusCode)> PostDataWithAuth(string url, object content, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var requestContent = new StringContent(
                JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync(url, requestContent);
            var data = await response.Content.ReadAsStringAsync();
            return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
        }
    }
}