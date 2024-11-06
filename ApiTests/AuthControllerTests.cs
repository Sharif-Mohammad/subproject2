using System.Net;
using System.Text.Json.Nodes;
using System.Text;
using System.Text.Json;
using Auth.Models;

namespace ApiTests
{
    public class AuthControllerTests : AuthControllerTestsBase
    {
        private const string AuthApi = $"{BaseApi}/api/auth";



        [Fact]
        public async Task Register_ValidUser_OkAndBadforInvalid()
        {
            var newUser = new RegisterModel
            {
                Email = AppTestUserInfo.Email,
                Password = AppTestUserInfo.Password,
                ConfirmPassword = AppTestUserInfo.Password,
            };

            var (response, statusCode) = await PostData($"{AuthApi}/register", newUser);

            if (statusCode == HttpStatusCode.OK) {

                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
            else
            {
                // User was created once
                Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            }

        }

        [Fact]
        public async Task Register_InvalidUser_BadRequest()
        {
            var invalidUser = new RegisterModel
            {
                Email = "", 
                Password = "Test@12345"
            };

            var (_, statusCode) = await PostData($"{AuthApi}/register", invalidUser);

            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Login_ValidCredentials_OkAndToken()
        {
            var loginDetails = new LoginModel
            {
                Email = AppTestUserInfo.Email,
                Password = AppTestUserInfo.Password
            };

            var (response, statusCode) = await PostData($"{AuthApi}/login", loginDetails);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.NotNull(response?.Value("token"));
        }

        [Fact]
        public async Task Login_InvalidCredentials_Unauthorized()
        {
            var invalidLoginDetails = new LoginModel
            {
                Email = "nonexistentuser",
                Password = "WrongPassword"
            };

            var (_, statusCode) = await PostData($"{AuthApi}/login", invalidLoginDetails);

            Assert.Equal(HttpStatusCode.Unauthorized, statusCode);
        }

        [Fact]
        public async Task Logout_AuthorizedUser_NoContent()
        {
            var token = await GetAuthToken(); // Assume GetAuthToken fetches a valid token for a logged-in user

            var statusCode = await PostDataWithAuth($"{AuthApi}/logout", token);

            Assert.Equal(HttpStatusCode.NoContent, statusCode);
        }

       
    }
}