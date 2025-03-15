using Microsoft.EntityFrameworkCore;
using CleanLand.Business.Interfaces;
using CleanLand.Data.Data;
using CleanLand.Data.DTOs;
using CleanLand.Data.Models;

namespace CleanLand.Business.Services
{
    public class LesseeService : ILesseeService
    {
        private readonly ApplicationDbContext _context;

        public LesseeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lessee>> GetAllLesseesAsync()
        {
            return await _context.Lessees.ToListAsync();
        }

        public async Task<Lessee> GetLesseeByIdAsync(int id)
        {
            return await _context.Lessees.FindAsync(id);
        }

        public async Task<Lessee> AddLesseeAsync(CreateLesseeDto createLesseeDto)
        {
            var lessee = new Lessee
            {
                Name = createLesseeDto.Name,
                Type = createLesseeDto.Type,
                IdentificationCode = createLesseeDto.IdentificationCode
            };

            _context.Lessees.Add(lessee);
            await _context.SaveChangesAsync(); // EF Core populates the Id after saving

            return lessee; // Return the created entity with the Id
        }

        public async Task UpdateLesseeAsync(Lessee lessee)
        {
            _context.Entry(lessee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLesseeAsync(int id)
        {
            var lessee = await _context.Lessees.FindAsync(id);
            if (lessee != null)
            {
                _context.Lessees.Remove(lessee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
