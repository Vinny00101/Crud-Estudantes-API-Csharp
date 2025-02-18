using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BackendAPICrud.security;

public class TokenService
{
    public string GerarToken(string nameUsuario)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, nameUsuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var VariableKey = Environment.GetEnvironmentVariable("Key");
        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(VariableKey!));
        var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

        var Token = new JwtSecurityToken(

            issuer: Environment.GetEnvironmentVariable("Issuer"),
            audience: Environment.GetEnvironmentVariable("Audience"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(Environment.GetEnvironmentVariable("ExpiresInMinutes"))),
            signingCredentials: creds

        );

        return new JwtSecurityTokenHandler().WriteToken(Token);
    }
}
