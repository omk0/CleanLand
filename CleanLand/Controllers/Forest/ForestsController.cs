using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CleanLand.Business.Interfaces;
using CleanLand.Data.Data;
using CleanLand.Data.Models;

namespace CleanLand.Controllers.Forest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IForestService _forestService;

        public ForestController(ApplicationDbContext context, IForestService forestService)
        {
            _context = context;
            _forestService = forestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.Models.Forest>>> GetForests()
        {
            return await _context.Forests.Include(f => f.TreeSpecies).Include(f => f.AreaDatas)
                .Include(f => f.AreaDatas)
                .Include(f => f.Issues).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Models.Forest>> GetForest(int id)
        {
            return await _context.Forests.Include(f => f.TreeSpecies).Include(f => f.Issues)
                .Include(f => f.AreaDatas)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<Data.Models.Forest>> CreateForest(Data.Models.Forest forest)
        {
            forest.CriticalityScore = _forestService.CalculateCriticalityScore(forest);
            _context.Forests.Add(forest);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetForests), new { id = forest.Id }, forest);
        }

        [HttpPost("batch")]
        public async Task<ActionResult> CreateForests(List<Data.Models.Forest> forests)
        {
            foreach (var forest in forests)
            {
                forest.CriticalityScore = _forestService.CalculateCriticalityScore(forest);
                _context.Forests.Add(forest);
            }

            await _context.SaveChangesAsync();
            return Ok(forests);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForest(int id, Data.Models.Forest forest)
        {
            if (id != forest.Id)
            {
                return BadRequest();
            }

            _context.Entry(forest).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var dbForest = await _context.Forests.FirstAsync(f => f.Id == forest.Id);
            dbForest.CriticalityScore = _forestService.CalculateCriticalityScore(forest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForest(int id)
        {
            var forest = await _context.Forests.FindAsync(id);
            if (forest == null)
            {
                return NotFound();
            }

            _context.Forests.Remove(forest);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}