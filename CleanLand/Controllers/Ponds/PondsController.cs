using Microsoft.AspNetCore.Mvc;
using CleanLand.Business.Interfaces;
using CleanLand.Data.Models;

[Route("api/[controller]")]
[ApiController]
public class PondsController : ControllerBase
{
    private readonly IPondService _pondService;

    public PondsController(IPondService pondService)
    {
        _pondService = pondService;
    }

    // GET: api/Ponds
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pond>>> GetPonds()
    {
        var ponds = await _pondService.GetAllPondsAsync();
        return Ok(ponds);
    }

    // GET: api/Ponds/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pond>> GetPond(int id)
    {
        var pond = await _pondService.GetPondByIdAsync(id);
        if (pond == null)
        {
            return NotFound();
        }
        return Ok(pond);
    }

    // POST: api/Ponds
    [HttpPost]
    public async Task<ActionResult<Pond>> PostPond(Pond pond)
    {
        await _pondService.AddPondAsync(pond);
        return CreatedAtAction("GetPond", new { id = pond.Id }, pond);
    }

    // PUT: api/Ponds/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPond(int id, Pond pond)
    {
        if (id != pond.Id)
        {
            return BadRequest();
        }

        await _pondService.UpdatePondAsync(pond);
        return NoContent();
    }

    // DELETE: api/Ponds/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePond(int id)
    {
        await _pondService.DeletePondAsync(id);
        return NoContent();
    }
}