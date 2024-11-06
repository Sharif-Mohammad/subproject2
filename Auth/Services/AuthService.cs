using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Models;
using Auth.Services;
using Domain.Entities.Auth;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<string> RegisterAsync(RegisterModel model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            throw new ApplicationException($"User registration failed.Reason: {string.Join(",", result.Errors.Select(x=>x.Description))}");

        return "User registered successfully";
    }

    public async Task<string> LoginAsync(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            throw new UnauthorizedAccessException("Invalid login attempt.");

        return GenerateJwtToken(user);
    }

    public Task LogoutAsync()
    {
        // Additional logout logic if needed (e.g., token invalidation).
        return Task.CompletedTask;
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
