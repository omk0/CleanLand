using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CleanLand.Data.Data;
using CleanLand.Data.Models;

namespace CleanLand.Pages.Ponds
{
    // Ponds/Edit.cshtml.cs
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pond Pond { get; set; } = default!;

        public SelectList LesseeSelectList { get; set; } = default!;
        public SelectList LeaseAgreementSelectList { get; set; } = default!;
        public SelectList WaterUsagePermitSelectList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Pond = await _context.Ponds.FindAsync(id);
            if (Pond == null)
            {
                return NotFound();
            }

            await PopulateSelectListsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(Pond).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Ponds.Any(e => e.Id == Pond.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Index");
        }

        private async Task PopulateSelectListsAsync()
        {
            var lessees = await _context.Lessees.ToListAsync();
            var leaseAgreements = await _context.LeaseAgreements.ToListAsync();
            var waterUsagePermits = await _context.WaterUsagePermits.ToListAsync();

            LesseeSelectList = new SelectList(lessees, "Id", "Name");
            LeaseAgreementSelectList = new SelectList(leaseAgreements, "Id", "Number");
            WaterUsagePermitSelectList = new SelectList(waterUsagePermits, "Id", "Number");
        }
    }
}
