using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CleanLand.Data.Data;

namespace CleanLand.Controllers.Forest
{
    public class Forest
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double XLocation { get; set; }
        public double YLocation { get; set; }
        public string? NGO { get; set; }
        public bool IsProtectedByLaw { get; set; }
        public long? TreesAmount { get; set; }
        public List<TreeSpecie> TreeSpecies { get; set; } = new();
        public double TonsOfSequesteredToDate { get; set; }
        public double TonsOfSequesteredPotential { get; set; }
        public List<AreaData> AreaDatas { get; set; } = new();
        public double AverageYearTemperature { get; set; }
        public double AverageYearHumidity { get; set; }
        public int FireIncidentsAmount { get; set; }
        public double CriticalityScore { get; set; }
    }

    public class TreeSpecie
    {
        [Key]
        public int Id { get; set; }
        public string? ScientificName { get; set; }
        public string? CommonName { get; set; }
        public string? Description { get; set; }
        public string? TaxonomicClassification { get; set; }
        public bool IsEndemic { get; set; }
        public bool IsInvasive { get; set; }
    }

    public class AreaData
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Area { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ForestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ForestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Forest>>> GetForests()
        {
            return await _context.Forests.Include(f => f.TreeSpecies).Include(f => f.AreaDatas).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Forest>> CreateForest(Forest forest)
        {
            _context.Forests.Add(forest);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetForests), new { id = forest.Id }, forest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForest(int id, Forest forest)
        {
            if (id != forest.Id)
            {
                return BadRequest();
            }

            _context.Entry(forest).State = EntityState.Modified;
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
