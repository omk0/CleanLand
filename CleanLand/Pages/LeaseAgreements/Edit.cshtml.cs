using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CleanLand.Data.Data;
using CleanLand.Data.Models;

public class EditLeaseAgreementModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditLeaseAgreementModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public LeaseAgreement LeaseAgreement { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        LeaseAgreement = await _context.LeaseAgreements.FindAsync(id);
        if (LeaseAgreement == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {

        _context.Attach(LeaseAgreement).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.LeaseAgreements.Any(e => e.Id == LeaseAgreement.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("../Ponds/Index");
    }
}