using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.KapiDunyasi.Infrastructure.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var issuer = _configuration["Jwt:Issuer"] ?? "kapidunyasi";
        var audience = _configuration["Jwt:Audience"] ?? "kapidunyasi";
        var key = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key missing");
        var expiresMinutes = int.TryParse(_configuration["Jwt:ExpiresMinutes"], out var minutes)
            ? minutes
            : 60;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
