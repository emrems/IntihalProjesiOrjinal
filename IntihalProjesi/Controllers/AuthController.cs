using IntihalProjesi.Dtos.LoginDto;
using IntihalProjesi.Helpers.Contract;
using IntihalProjesi.Models;
using IntihalProjesi.Repositories.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntihalProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly OrjinalIntihalDbContext _context;
        private readonly IJwtHelper _jwtHelper;

        public AuthController(OrjinalIntihalDbContext context, IJwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            // Kullanıcı doğrulaması
            var kullanici = await _context.Set<Kullanici>()
                .FirstOrDefaultAsync(u => u.Eposta == loginModel.Eposta && u.Sifre == loginModel.Sifre);

            if (kullanici == null)
            {
                return Unauthorized("Geçersiz e-posta veya şifre.");
            }

            // Access ve Refresh Token oluşturma
            var accessToken = _jwtHelper.GenerateToken(kullanici.KullaniciId, kullanici.Rol);
            var refreshToken = _jwtHelper.GenerateRefreshToken();


            // Refresh Token'ı Cookie'ye ekle
            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(new
            {
                token = accessToken,
                refreshToken = refreshToken, 
                rol = kullanici.Rol,
                id = kullanici.KullaniciId,
                userName = kullanici.Ad+" "+kullanici.Soyad
            });
        }

        
        [HttpPost("refresh")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("Refresh token bulunamadı");

            // Refresh Token'dan kullanıcı ID ve rolünü al
            var userInfo = _jwtHelper.DecodeToken(refreshToken);
            if (userInfo == null)
                return Unauthorized("Geçersiz refresh token");

            // Yeni Access Token oluştur
            var newAccessToken = _jwtHelper.GenerateToken(userInfo.Value.KullaniciId, userInfo.Value.Rol);
            var newRefreshToken = _jwtHelper.GenerateRefreshToken();

            // Yeni Refresh Token'ı Cookie'ye ekle
            Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(new { Token = newAccessToken });
        }

        
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("refreshToken");
            return Ok(new { Message = "Başarıyla çıkış yapıldı." });
        }
    }
}
