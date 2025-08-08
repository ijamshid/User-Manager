using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UserManager.Application.Options;

namespace UserManager.Application.Services;

public class AuthService(IOptions<JwtSettings> settings) : IAuthService
{
    private readonly IOptions<JwtSettings> _settings = settings;
    private readonly JwtSecurityTokenHandler _handler = new();

    public async Task<string> GetToken(int userId, string username)
    {



        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            "UserManager.uz",
            "UserManager.uz",
            claims: [],
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            signingCredentials: credentials
        );

        return _handler.WriteToken(token);

    }
}