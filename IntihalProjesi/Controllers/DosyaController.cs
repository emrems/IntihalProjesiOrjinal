using IntihalProjesi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IntihalProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosyaController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public DosyaController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> GetallDosya()
        {
            var dosyalar=  await _manager.DosyaService.GetAllDosya();
            if(dosyalar == null)
            {
                return NotFound("dosyalar bulunamadı");
            }
            return Ok(dosyalar);
        }
    }
}
