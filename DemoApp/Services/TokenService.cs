using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DemoApp.Interfaces;
using DemoApp.Models;
using Microsoft.IdentityModel.Tokens;

namespace DemoApp.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    private readonly SymmetricSecurityKey _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
        config["Jwt:Secret"] ?? throw new InvalidOperationException("JWT:SECRET can't be NULL")));

    public string GenerateToken(AppUser user)
    {
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
        ];
        var signInCredential = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = config["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT:ISSUER can't be NULL"),
            Audience = config["Jwt:Audience"] ?? throw new InvalidOperationException("JWT:AUDIENCE can't be NULL"),
            SigningCredentials = signInCredential,
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

}