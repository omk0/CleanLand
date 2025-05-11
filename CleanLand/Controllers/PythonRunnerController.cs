namespace CleanLand.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PythonRunnerController : ControllerBase
{
    private readonly IHostEnvironment _env;

    public PythonRunnerController(IHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet("run")]
    public async Task<IActionResult> RunPythonScript([FromQuery] string name = "World")
    {
        // 1) Use "python" (relies on Python being on the system PATH)
        var pythonExe = "python";

        // 2) Build the script path relative to the content root (project folder)
        var scriptPath = Path.Combine(_env.ContentRootPath, "Scripts", "myscript.py");

        if (!System.IO.File.Exists(scriptPath))
            return NotFound(new { error = $"Script not found at {scriptPath}" });

        // 3) Quote the script path and append any arguments
        var psi = new ProcessStartInfo
        {
            FileName            = pythonExe,
            Arguments           = $"\"{scriptPath}\" --name {name}",
            RedirectStandardOutput = true,
            RedirectStandardError  = true,
            UseShellExecute       = false,
            CreateNoWindow        = true
        };

        try
        {
            using var process = Process.Start(psi);
            var output = await process.StandardOutput.ReadToEndAsync();
            var errors = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();

            if (!string.IsNullOrWhiteSpace(errors))
                return BadRequest(new { error = errors.Trim() });

            return Ok(new { result = output.Trim() });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
