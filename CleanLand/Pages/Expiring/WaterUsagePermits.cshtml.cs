using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CleanLand.Data.Data;
using CleanLand.Data.Models;

namespace CleanLand.Pages.Expiring
{
    // Expiring/WaterUsagePermits.cshtml.cs
    public class WaterUsagePermitsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public WaterUsagePermitsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<WaterUsagePermitVM> ExpiringPermits { get; set; } = new();

        public async Task OnGetAsync()
        {
            var today = DateTime.Today;
            var thresholdDate = today.AddDays(30);

            var permits = await _context.WaterUsagePermits
                .Include(wup => wup.Ponds)
                .ToListAsync();

            ExpiringPermits = permits
                .Select(wup => new WaterUsagePermitVM
                {
                    Id = wup.Id,
                    Number = wup.Number,
                    StartDate = wup.StartDate,
                    TermInYears = wup.TermInYears,
                    ExpirationDate = wup.StartDate.AddYears(wup.TermInYears),
                    DaysRemaining = (wup.StartDate.AddYears(wup.TermInYears) - today).Days,
                    Ponds = wup.Ponds
                })
                .Where(wup => wup.ExpirationDate > today && wup.ExpirationDate <= thresholdDate)
                .OrderBy(wup => wup.ExpirationDate)
                .ToList();
        }

        public class WaterUsagePermitVM : WaterUsagePermit
        {
            public DateTime ExpirationDate { get; set; }
            public int DaysRemaining { get; set; }
        }
    }
}
