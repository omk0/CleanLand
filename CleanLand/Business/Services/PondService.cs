using Microsoft.EntityFrameworkCore;
using CleanLand.Business.Interfaces;
using CleanLand.Data.Data;
using CleanLand.Data.Models;

namespace CleanLand.Business.Services
{
    public class PondService : IPondService
    {
        private readonly ApplicationDbContext _context;

        public PondService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pond>> GetAllPondsAsync()
        {
            return await _context.Ponds
                .Include(p => p.Lessee)
                .Include(p => p.LeaseAgreement)
                .Include(p => p.WaterUsagePermit)
                .ToListAsync();
        }

        public async Task<Pond> GetPondByIdAsync(int id)
        {
            return await _context.Ponds
                .Include(p => p.Lessee)
                .Include(p => p.LeaseAgreement)
                .Include(p => p.WaterUsagePermit)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPondAsync(Pond pond)
        {
            _context.Ponds.Add(pond);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePondAsync(Pond pond)
        {
            _context.Entry(pond).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePondAsync(int id)
        {
            var pond = await _context.Ponds.FindAsync(id);
            if (pond != null)
            {
                _context.Ponds.Remove(pond);
                await _context.SaveChangesAsync();
            }
        }
    }
}
