using IntihalProjesi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IntihalProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenzerlikSonuclariController : ControllerBase
    {
        private readonly IBenzerlikSonucuService _service;

        public BenzerlikSonuclariController(IBenzerlikSonucuService service)
        {
            _service = service;
        }

        [HttpGet("icerik/{icerikId}")]
        public async Task<IActionResult> GetBenzerlikSonuclari(int icerikId)
        {
            var sonuclar = await _service.GetBenzerlikSonuclariByIcerikIdAsync(icerikId);
            return Ok(sonuclar);
        }

    }
}
