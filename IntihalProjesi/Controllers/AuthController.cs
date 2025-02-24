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

            // role göre token oluşturma
            var token = _jwtHelper.GenerateToken(kullanici.KullaniciId, kullanici.Rol);

            return Ok(new
            {
                Token = token,
                Rol = kullanici.Rol,
                Id = kullanici.KullaniciId
            });
        }
    }
}
