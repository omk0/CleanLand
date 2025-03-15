using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CleanLand.Data.Data;
using CleanLand.Data.Models;

namespace CleanLand.Pages.Expiring
{
    // Expiring/LeaseAgreements.cshtml.cs
    public class LeaseAgreementsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LeaseAgreementsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<LeaseAgreementVM> ExpiringAgreements { get; set; } = new();

        public async Task OnGetAsync()
        {
            var today = DateTime.Today;
            var thresholdDate = today.AddDays(30);

            var agreements = await _context.LeaseAgreements
                .Include(la => la.Ponds)
                .ToListAsync();

            ExpiringAgreements = agreements
                .Select(la => new LeaseAgreementVM
                {
                    Id = la.Id,
                    Number = la.Number,
                    Date = la.Date,
                    TermInYears = la.TermInYears,
                    EconomicActivities = la.EconomicActivities,
                    ExpirationDate = la.Date.AddYears(la.TermInYears),
                    DaysRemaining = (la.Date.AddYears(la.TermInYears) - today).Days,
                    Ponds = la.Ponds
                })
                .Where(la => la.ExpirationDate > today && la.ExpirationDate <= thresholdDate)
                .OrderBy(la => la.ExpirationDate)
                .ToList();
        }

        public class LeaseAgreementVM : LeaseAgreement
        {
            public DateTime ExpirationDate { get; set; }
            public int DaysRemaining { get; set; }
        }
    }
}
