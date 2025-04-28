using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IntihalProjesi.Helpers.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IntihalProjesi.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _durationInSeconds;

        public JwtHelper(IConfiguration configuration)
        {
            _key = configuration["JWT:Key"] ?? throw new ArgumentNullException("JWT:Key");
            _issuer = configuration["JWT:Issuer"] ?? throw new ArgumentNullException("JWT:Issuer");
            _audience = configuration["JWT:Audience"] ?? throw new ArgumentNullException("JWT:Audience");

            // appsettings.json içindeki saniye cinsinden süre
            if (!int.TryParse(configuration["JWT:DurationInSeconds"], out _durationInSeconds))
            {
                _durationInSeconds = 60;  // fallback: 60 saniye
            }
        }

        public (int KullaniciId, string Rol)? DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadJwtToken(token);
                var userIdClaim = jwtToken.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var roleClaim = jwtToken.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

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

            // Burada saniye cinsinden konfigürasyondaki değeri kullanıyoruz
            var expires = DateTime.UtcNow.AddSeconds(_durationInSeconds);

            var jwt = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
    