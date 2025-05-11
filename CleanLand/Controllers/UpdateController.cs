using CleanLand.Business.Interfaces;
using CleanLand.Data.Data;
using CleanLand.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanLand.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UpdateController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IForestService _forestService;
    private readonly IPondService _pondService;

    public UpdateController(ApplicationDbContext context, IForestService forestService, IPondService pondService)
    {
        _context = context;
        _forestService = forestService;
        _pondService = pondService;
    }

    [HttpPost]
    public async Task<ActionResult> UpdateCriticalityScores()
    {
        var forests = await _context.Forests.ToListAsync();
        var ponds = await _context.Ponds.ToListAsync();
        
        foreach (var f in forests)
        {
            f.CriticalityScore =
                _forestService.CalculateCriticalityScore(f);
        }

        foreach (var p in ponds)
        {
            p.CriticalityScore =
                _pondService.CalculateCriticalityScore(p);
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }
}