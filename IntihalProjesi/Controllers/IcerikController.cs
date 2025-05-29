using IntihalProjesi.Dtos.IcerikDtos;
using IntihalProjesi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntihalProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IcerikController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public IcerikController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet]

        public async Task<IActionResult> GettAll()
        {
            var Icerikler = await _manager.IcerikService.GetAllAsync();
            return Ok(Icerikler);
        }



        [HttpGet("id")]

        public async Task<IActionResult> GetById( int id)
        {
            try
            {
                var icerik = await _manager.IcerikService.GetByIdAsync(id);
                if(icerik == null)
                {
                    return NotFound(new { Message = "Bu ID'ye ait içerik bulunamadı." });
                }
                return Ok(icerik);

            }catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Bir hata oluştu.", Error = ex.Message });
            }
        }

        [HttpGet("details/{contentId}")]
        public async Task<IActionResult> GetDetailsByIcerikId( int contentId)
        {
            try
            {
                var ıcerikler= await _manager.IcerikService.GetDetailsIcerik(contentId);
                return Ok(ıcerikler);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Bir hata oluştu.", Error = ex.Message });
            }
        }


        [HttpGet("teacher/{teacherId}/all")]
        public async Task<IActionResult> GetAssigmentByTeacherId(int teacherId)
        {
            try
            {
                var assigments = await _manager.IcerikService.GetByTeacherIdAsync(teacherId);
                if (assigments == null)
                {
                    return BadRequest($"{teacherId} id'sine ait ödev bulunamadı.");
                }
                return Ok(assigments);

            }catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Bir hata oluştu.", Error = ex.Message });
            }
        }



        [HttpGet("teacher/{teacherName}")]

        public async Task<IActionResult> GetAllByTeacherId(string teacherName)
        {
            var icerikler = await _manager.IcerikService.GetByTeacherNameAsync(teacherName);
            if(icerikler == null)
            {
                return BadRequest($"{teacherName} isimli ögretmen içeriği yok");
            }
            return Ok(icerikler);
        }

        [HttpGet("active")]
        public async  Task<IActionResult> GetActiveIcerik()
        {
            var icerikler = await _manager.IcerikService.GetActiveAssignmentsAsync();
            return Ok(icerikler);

        }

        
        [HttpPost]

        public async Task<IActionResult> CreateAssigment([FromBody] IcerikCreateDto icerik)
        {
            if(icerik == null)
            {
                return BadRequest();

            }
            await _manager.IcerikService.CreateAsync(icerik);
            return Ok(icerik);
        }



        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAssigment(int id, [FromBody] IcerikUpdateDto icerik) 
        {
            var guncellenecekIcerik = await _manager.IcerikService.GetByIdAsync(id);
            if(guncellenecekIcerik == null)
            {
                return NotFound($"ID'si {id} olan ödev bulunamadı.");
            }

            if(icerik is null)
            {
                return BadRequest();
            }
            await _manager.IcerikService.UpdateAsync(id,icerik);
            
            return Ok(icerik);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Silinecek ödevin var olup olmadığını kontrol et
            var existingIcerik = await _manager.IcerikService.GetByIdAsync(id);
            if (existingIcerik == null)
                return NotFound($"ID'si {id} olan ödev bulunamadı.");

            await _manager.IcerikService.DeleteAsync(id);
            return Ok("Ödev başarıyla silindi.");
        }
    }
}
