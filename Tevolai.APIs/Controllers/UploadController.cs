using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Tevolai.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        private static readonly string[] AllowedExtensions =
        {
            ".jpg", ".jpeg", ".png", ".webp", ".pdf"
        };

        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // Upload file
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!AllowedExtensions.Contains(ext))
                return BadRequest("File type not allowed");

            if (file.Length > MaxFileSize)
                return BadRequest("File size exceeds limit (5MB)");

            var uploadsPath = Path.Combine(_env.ContentRootPath, "uploads");
            Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsPath, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Ok(new { name = fileName, url = $"/uploads/{fileName}" });
        }

        // Get all uploads
        [HttpGet]
        public IActionResult GetAllUploads()
        {
            var uploadsPath = Path.Combine(_env.ContentRootPath, "uploads");

            if (!Directory.Exists(uploadsPath))
                return Ok(new List<object>());

            var files = Directory.GetFiles(uploadsPath)
                .Select(f =>
                {
                    var name = Path.GetFileName(f);
                    return new { name, url = $"/uploads/{name}" };
                });

            return Ok(files);
        }

        // Delete file
        [HttpDelete("{fileName}")]
        public IActionResult DeleteUpload(string fileName)
        {
            fileName = Path.GetFileName(fileName); // prevent path traversal

            var uploadsPath = Path.Combine(_env.ContentRootPath, "uploads");
            var filePath = Path.Combine(uploadsPath, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found");

            System.IO.File.Delete(filePath);
            return NoContent();
        }
    }
}