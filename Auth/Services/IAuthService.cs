using Auth.Models;

namespace Auth.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterModel model);
    Task<string> LoginAsync(LoginModel model);
    Task LogoutAsync();
}