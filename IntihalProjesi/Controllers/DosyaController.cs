using IntihalProjesi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Aspose.Words;
using Microsoft.AspNetCore.Authorization;

namespace IntihalProjesi.Controllers
{
    [Authorize]
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
            var dosyalar = await _manager.DosyaService.GetAllDosya();
            if (dosyalar == null)
            {
                return NotFound("dosyalar bulunamadı");
            }
            return Ok(dosyalar);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDosyaById(int id)
        {
            var dosya = await _manager.DosyaService.GetDosyaByIdAsync(id);
            if (dosya == null)
            {
                return NotFound("Dosya bulunamadı.");
            }
            return Ok(dosya);
        }

        [HttpGet("download/{dosyaId}")]
        public async Task<IActionResult> DownloadFile(int dosyaId)
        {
            var dosya = await _manager.DosyaService.GetById(dosyaId);
            if (dosya == null)
            {
                return NotFound();
            }

            var filePath = dosya.CleanedPath;
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(filePath));
        }

        [HttpGet("view/{dosyaId}")]
        public async Task<IActionResult> ViewFile(int dosyaId)
        {
            var dosya = await _manager.DosyaService.GetById(dosyaId);
            if (dosya == null)
            {
                return NotFound();
            }

            var filePath = dosya.CleanedPath;
            if (!string.IsNullOrEmpty(filePath) && filePath.EndsWith("_cleaned.txt"))
            {
                filePath = filePath.Replace("_cleaned.txt", "");
            }

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            if (extension == ".docx")
            {
                try
                {
                    Document doc = new Document(filePath);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        doc.Save(ms, SaveFormat.Pdf);
                        return File(ms.ToArray(), "application/pdf");
                    }
                }
                catch (Exception ex)
                {
                    
                    return StatusCode(500, $"docx'ten PDF'e dönüştürme sırasında bir hata oluştu: {ex.Message}");
                }
            }
            else
            {
                // Diğer dosya türlerini doğrudan görüntüle
                var contentType = GetContentType(filePath);
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(fileBytes, contentType);
            }
        }

        private string GetContentType(string path)
        {
            var extension = Path.GetExtension(path).ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".txt" => "text/plain",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".ppt" => "application/vnd.ms-powerpoint",
                ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                _ => "application/octet-stream"
            };
        }
    }
}