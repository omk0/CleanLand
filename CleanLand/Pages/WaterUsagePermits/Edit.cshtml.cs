using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CleanLand.Data.Data;
using CleanLand.Data.Models;

public class EditWaterUsagePermitModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditWaterUsagePermitModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public WaterUsagePermit WaterUsagePermit { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        WaterUsagePermit = await _context.WaterUsagePermits.FindAsync(id);
        if (WaterUsagePermit == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {

        _context.Attach(WaterUsagePermit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.WaterUsagePermits.Any(e => e.Id == WaterUsagePermit.Id))
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