using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleanLand.Business.Interfaces;
using CleanLand.Data.Data;
using CleanLand.Data.DTOs;
using CleanLand.Data.Models;

[Route("api/[controller]")]
[ApiController]
public class LesseesController : ControllerBase
{
    private readonly ILesseeService _lesseeService;
    private readonly ApplicationDbContext _context;

    public LesseesController(ILesseeService lesseeService, ApplicationDbContext context)
    {
        _lesseeService = lesseeService;
        _context = context;
    }

    // GET: api/Lessees
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lessee>>> GetLessees()
    {
        var lessees = await _lesseeService.GetAllLesseesAsync();
        return Ok(lessees);
    }

    // POST: api/Lessees
    [HttpPost]
    public async Task<ActionResult<Lessee>> PostLessee(CreateLesseeDto createLesseeDto)
    {
        // Call the service and get the created Lessee
        var lessee = await _lesseeService.AddLesseeAsync(createLesseeDto);

        // Return the 201 Created response with the new Lessee
        return CreatedAtAction(
            nameof(GetLessee), // Name of the GET action to generate the URL
            new { id = lessee.Id }, // Route parameter for the GET action
            lessee // The created entity to include in the response body
        );
    }

    // GET: api/Lessees/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Lessee>> GetLessee(int id)
    {
        var lessee = await _lesseeService.GetLesseeByIdAsync(id);
        if (lessee == null)
        {
            return NotFound();
        }
        return Ok(lessee);
    }

    // PUT: api/Lessees/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLessee(int id, Lessee lessee)
    {
        if (id != lessee.Id)
        {
            return BadRequest();
        }

        await _lesseeService.UpdateLesseeAsync(lessee);
        return NoContent();
    }

    // DELETE: api/Lessees/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLessee(int id)
    {
        await _lesseeService.DeleteLesseeAsync(id);
        return NoContent();
    }

    [HttpPut("{pondId}/assign-lessee/{lesseeId}")]
    public async Task<IActionResult> AssignLesseeToPond(int pondId, int lesseeId)
    {
        var pond = await _context.Ponds.FindAsync(pondId);
        if (pond == null) return NotFound("Pond not found");

        var lessee = await _context.Lessees.FindAsync(lesseeId);
        if (lessee == null) return NotFound("Lessee not found");

        pond.Lessee = lessee; // Assign the lessee to the pond
        await _context.SaveChangesAsync();

        return NoContent();
    }
}