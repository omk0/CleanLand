using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CleanLand.Data.Data;
using CleanLand.Data.Models;

public class EditLesseeModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditLesseeModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Lessee Lessee { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Lessee = await _context.Lessees.FindAsync(id);
        if (Lessee == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {

        _context.Attach(Lessee).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Lessees.Any(e => e.Id == Lessee.Id))
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