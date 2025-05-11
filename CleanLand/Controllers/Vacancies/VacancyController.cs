using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleanLand.Data;
using CleanLand.Data.Data;
using CleanLand.Data.Models;

namespace CleanLand.Api.Controllers
{
    // DTOs for create/update operations
    public class VacancyCreateDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Wage { get; set; }
        public int NeededPeople { get; set; }
        public int AppliedPeople { get; set; }
        public int ObjectId { get; set; }
    }

    public class VacancyUpdateDto : VacancyCreateDto
    {
        public int Id { get; set; }
    }

    [ApiController]
    [Route("api/Vacancies")]
    public class VacancyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VacancyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Vacancy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacancy>>> GetVacancies()
        {
            return await _context.Vacancies
                                 .Include(v => v.Object)
                                 .ToListAsync();
        }

        // GET: api/Vacancy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacancy>> GetVacancy(int id)
        {
            var vacancy = await _context.Vacancies
                                        .Include(v => v.Object)
                                        .FirstOrDefaultAsync(v => v.Id == id);

            if (vacancy == null)
                return NotFound();

            return vacancy;
        }

        // POST: api/Vacancy
        [HttpPost]
        public async Task<ActionResult<Vacancy>> CreateVacancy(VacancyCreateDto dto)
        {
            var asset = await _context.Set<EnvironmentalAsset>()
                                      .FindAsync(dto.ObjectId);
            if (asset == null)
                return BadRequest($"Environmental asset with Id={dto.ObjectId} not found.");

            var vacancy = new Vacancy
            {
                Title = dto.Title,
                Description = dto.Description,
                Wage = dto.Wage,
                NeededPeople = dto.NeededPeople,
                AppliedPeople = dto.AppliedPeople,
                Object = asset
            };

            _context.Vacancies.Add(vacancy);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVacancy), new { id = vacancy.Id }, vacancy);
        }

        // PUT: api/Vacancy/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacancy(int id, VacancyUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var vacancy = await _context.Vacancies
                                        .Include(v => v.Object)
                                        .FirstOrDefaultAsync(v => v.Id == id);
            if (vacancy == null)
                return NotFound();

            var asset = await _context.Set<EnvironmentalAsset>().FindAsync(dto.ObjectId);
            if (asset == null)
                return BadRequest($"Environmental asset with Id={dto.ObjectId} not found.");

            // update fields
            vacancy.Title = dto.Title;
            vacancy.Description = dto.Description;
            vacancy.Wage = dto.Wage;
            vacancy.NeededPeople = dto.NeededPeople;
            vacancy.AppliedPeople = dto.AppliedPeople;
            vacancy.Object = asset;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Vacancies.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Vacancy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null)
                return NotFound();

            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        // POST: api/Vacancy/batch
        [HttpPost("batch")]
        public async Task<ActionResult<IEnumerable<Vacancy>>> CreateVacanciesBatch(
            [FromBody] List<VacancyCreateDto> dtos)
        {
            var created = new List<Vacancy>();

            foreach(var dto in dtos)
            {
                var asset = await _context.Set<EnvironmentalAsset>()
                    .FindAsync(dto.ObjectId);
                if (asset == null)
                    return BadRequest($"Asset with Id={dto.ObjectId} not found.");

                var vac = new Vacancy {
                    Title        = dto.Title,
                    Description  = dto.Description,
                    Wage         = dto.Wage,
                    NeededPeople = dto.NeededPeople,
                    AppliedPeople= dto.AppliedPeople,
                    Object       = asset
                };
                _context.Vacancies.Add(vac);
                created.Add(vac);
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVacancies), created);
        }

    }
}
