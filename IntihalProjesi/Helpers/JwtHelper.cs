using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IntihalProjesi.Helpers.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtHelper : IJwtHelper
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;

    public JwtHelper(IConfiguration configuration)
    {
        _key = configuration["JWT:Key"];
        _issuer = configuration["JWT:Issuer"];
        _audience = configuration["JWT:Audience"];
    }

    public (int KullaniciId, string Rol)? DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        try
        {
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userIdClaim == null || roleClaim == null)
                return null;

            return (int.Parse(userIdClaim), roleClaim);
        }
        catch
        {
            return null;
        }
    }


    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber);
    }

    public string GenerateToken(int kullaniciId, string rol)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, kullaniciId.ToString()),
            new Claim(ClaimTypes.Role, rol)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1), // Token geçerlilik süresi
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
