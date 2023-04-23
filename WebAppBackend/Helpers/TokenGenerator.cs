using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAppBackend.Helpers;

public class TokenGenerator
{
    private readonly IConfiguration _config;

    public TokenGenerator(IConfiguration configuration)
    {
        _config = configuration;
    }

    public string GenerateJwtToken(ClaimsIdentity claimsIdentity, DateTime expiresAt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = _config.GetSection("TokenValidation").GetValue<string>("Issuer")!,
            Audience = _config.GetSection("TokenValidation").GetValue<string>("Audience")!,
            Subject = claimsIdentity,
            Expires = expiresAt,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("TokenValidation").GetValue<string>("SecretKey")!)),
                SecurityAlgorithms.HmacSha512Signature
            )
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}
