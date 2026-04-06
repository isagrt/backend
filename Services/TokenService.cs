using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BoschPizza.Services;

public class TokenService
{
    public string GenerateToken(string username, string key, string issuer, string audience)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var credencial = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //Cria o Token JWT com emissor, audienciam claims, expiração e assisnatura
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credencial
        );
    
        return new JwtSecurityTokenHandler().WriteToken(token); 
    }
}

