using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleanLand.Data;
using CleanLand.Data.Data;
using CleanLand.Data.DTOs;
using CleanLand.Data.Models;
using CleanLand.Data.DTOs;

namespace CleanLand.Api.Controllers
{
    // DTO for applying a volunteer
    public class VolunteerApplyDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }

    [ApiController]
    [Route("api/vacancies/{vacancyId}/[controller]")]
    public class VolunteersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VolunteersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/vacancies/{vacancyId}/volunteers
        // Apply a volunteer to a vacancy
        [HttpPost]
        public async Task<ActionResult<VolunteerDto>> ApplyAsync(int vacancyId, VolunteerApplyDto dto)
        {
            var vacancy = await _context.Vacancies.FindAsync(vacancyId);
            if (vacancy == null)
                return NotFound($"Vacancy with Id={vacancyId} not found.");

            var volunteer = new Volunteer
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                AppliedAt = DateTime.UtcNow,
                VacancyId = vacancyId
            };

            _context.Volunteers.Add(volunteer);
            vacancy.AppliedPeople += 1;
            await _context.SaveChangesAsync();

            var result = new VolunteerDto
            {
                Id = volunteer.Id,
                Name = volunteer.Name,
                Email = volunteer.Email,
                Phone = volunteer.Phone,
                AppliedAt = volunteer.AppliedAt
            };

            return Created();
        }

        // GET: api/vacancies/{vacancyId}/volunteers
        // List volunteers for a vacancy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolunteerDto>>> GetAllAsync(int vacancyId)
        {
            if (!await _context.Vacancies.AnyAsync(v => v.Id == vacancyId))
                return NotFound($"Vacancy with Id={vacancyId} not found.");

            var volunteers = await _context.Volunteers
                .Where(vol => vol.VacancyId == vacancyId)
                .Select(vol => new VolunteerDto
                {
                    Id = vol.Id,
                    Name = vol.Name,
                    Email = vol.Email,
                    Phone = vol.Phone,
                    AppliedAt = vol.AppliedAt
                })
                .ToListAsync();

            return volunteers;
        }

        // GET: api/vacancies/{vacancyId}/volunteers/count
        // Get volunteer count for a vacancy
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCountAsync(int vacancyId)
        {
            if (!await _context.Vacancies.AnyAsync(v => v.Id == vacancyId))
                return NotFound($"Vacancy with Id={vacancyId} not found.");

            var count = await _context.Volunteers.CountAsync(vol => vol.VacancyId == vacancyId);
            return count;
        }
    }
}
