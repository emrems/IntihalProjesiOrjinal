using IntihalProjesi.Dtos.Bildirim;
using IntihalProjesi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IntihalProjesi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BildirimController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BildirimController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> ShowUnreadNotifications(int userid)
        {

            var kullanici = await _manager.KullaniciService.GetByIdAsync(userid);
            if (kullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }
            var bildirimler = await _manager.BildirimService.ShowUnreadNotifications(userid);
            if (bildirimler == null)
            {
                return NotFound("Bildirimler bulunamadı");
            }
            return Ok(bildirimler);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNotification([FromBody] BildirimCreateDTO bildirimCreateDTO)
        {
            if (bildirimCreateDTO == null)
            {
                return BadRequest("Bildirim bilgileri eksik");
            }
            var notification = await _manager.BildirimService.CreateNotificationAsync(bildirimCreateDTO);
            return Ok(notification);
        }


        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BildirimDTO>>> GetAllBildirimler()
        {
            var bildirimler = await _manager.BildirimService.GetAllBildirimlerAsync();
            return Ok(bildirimler);
        }

        [HttpPut("mark-as-read/{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            await _manager.BildirimService.MarkNotificationAsReadAsync(notificationId);
            return Ok("Bildirim okundu olarak işaretlendi.");
        }
    }
}
