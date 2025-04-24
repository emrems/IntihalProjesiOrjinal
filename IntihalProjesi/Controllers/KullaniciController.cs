using IntihalProjesi.Dtos;
using IntihalProjesi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntihalProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "Admin")] 
    public class KullaniciController : ControllerBase
    {

        private readonly IServiceManager _manager;

        public KullaniciController(IServiceManager manager)
        {
            _manager = manager;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _manager.KullaniciService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _manager.KullaniciService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KullaniciCreateDto kullaniciDto)
        {
            var createdUser = await _manager.KullaniciService.CreateAsync(kullaniciDto);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.KullaniciId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] KullaniciUpdateDto kullaniciDto)
        {
            await _manager.KullaniciService.UpdateAsync(id, kullaniciDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _manager.KullaniciService.DeleteAsync(id);
            return NoContent();
        }
    }
}
