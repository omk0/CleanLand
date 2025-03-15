using Microsoft.AspNetCore.Mvc;
using CleanLand.Business.Interfaces;

namespace CleanLand.Controllers.Ponds
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IExcelImportService _importService;

        public ImportController(IExcelImportService importService)
        {
            _importService = importService;
        }

        [HttpPost("ponds")]
        public async Task<IActionResult> ImportPonds(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не завантажено.");

            var tempFilePath = Path.GetTempFileName();
            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            try
            {
                await _importService.ImportPondsAsync(tempFilePath);
                return Ok($"Імпортовано успішно! Громада: {Path.GetFileNameWithoutExtension(file.FileName)}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Помилка: {ex.InnerException}");
            }
            finally
            {
                System.IO.File.Delete(tempFilePath);
            }
        }
    }
}
