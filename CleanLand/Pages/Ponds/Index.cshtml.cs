using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CleanLand.Data.Data;
using CleanLand.Data.Models;

namespace CleanLand.Pages.Ponds;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    // Фільтри для усіх властивостей сутності Pond
    [BindProperty(SupportsGet = true)]
    public int? IdFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? NameFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? DistrictFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? TerritorialCommunityFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? SettlementFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? CoordinatesFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public double? LengthFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public double? WidthFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public double? DepthFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public double? WaterLevelFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public double? VolumeFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public double? LeasedAreaFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? CadastralNumberFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public double? WaterSurfaceAreaFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public bool? IsDrainableFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public bool? HasHydraulicStructureFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? HydraulicStructureOwnerFilter { get; set; }
    // Замість числових Id для пов'язаних сутностей використаємо рядкові фільтри
    [BindProperty(SupportsGet = true)]
    public string? LesseeIdentificationCodeFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? LeaseAgreementNumberFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? WaterUsagePermitNumberFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? RiverFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? BasinFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? StatusFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? IssuesFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? NotesFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public decimal? ImposedFinesFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public decimal? ImposedDamagesFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public decimal? CollectedFinesFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public decimal? CollectedDamagesFilter { get; set; }

    public IList<Pond> Ponds { get; set; } = new List<Pond>();

    public async Task OnGetAsync()
    {
        // Початковий запит із включенням навігаційних властивостей
        IQueryable<Pond> query = _context.Ponds
            .Include(p => p.Lessee)
            .Include(p => p.LeaseAgreement)
            .Include(p => p.WaterUsagePermit)
            .AsQueryable();

        // Фільтрація по кожному критерію
        if (IdFilter.HasValue)
            query = query.Where(p => p.Id == IdFilter.Value);
        if (!string.IsNullOrEmpty(NameFilter))
            query = query.Where(p => p.Name.Contains(NameFilter));
        if (!string.IsNullOrEmpty(DistrictFilter))
            query = query.Where(p => p.District.Contains(DistrictFilter));
        if (!string.IsNullOrEmpty(TerritorialCommunityFilter))
            query = query.Where(p => p.TerritorialCommunity.Contains(TerritorialCommunityFilter));
        if (!string.IsNullOrEmpty(SettlementFilter))
            query = query.Where(p => p.Settlement.Contains(SettlementFilter));
        if (!string.IsNullOrEmpty(CoordinatesFilter))
            query = query.Where(p => p.Coordinates.Contains(CoordinatesFilter));
        if (LengthFilter.HasValue)
            query = query.Where(p => p.Length == LengthFilter.Value);
        if (WidthFilter.HasValue)
            query = query.Where(p => p.Width == WidthFilter.Value);
        if (DepthFilter.HasValue)
            query = query.Where(p => p.Depth == DepthFilter.Value);
        if (WaterLevelFilter.HasValue)
            query = query.Where(p => p.WaterLevel == WaterLevelFilter.Value);
        if (VolumeFilter.HasValue)
            query = query.Where(p => p.Volume == VolumeFilter.Value);
        if (LeasedAreaFilter.HasValue)
            query = query.Where(p => p.LeasedArea == LeasedAreaFilter.Value);
        if (!string.IsNullOrEmpty(CadastralNumberFilter))
            query = query.Where(p => p.CadastralNumber.Contains(CadastralNumberFilter));
        if (WaterSurfaceAreaFilter.HasValue)
            query = query.Where(p => p.WaterSurfaceArea == WaterSurfaceAreaFilter.Value);
        if (IsDrainableFilter.HasValue)
            query = query.Where(p => p.IsDrainable == IsDrainableFilter.Value);
        if (HasHydraulicStructureFilter.HasValue)
            query = query.Where(p => p.HasHydraulicStructure == HasHydraulicStructureFilter.Value);
        if (!string.IsNullOrEmpty(HydraulicStructureOwnerFilter))
            query = query.Where(p => p.HydraulicStructureOwner.Contains(HydraulicStructureOwnerFilter));
        // Фільтрація по пов'язаним сутностям за їх властивостями
        if (!string.IsNullOrEmpty(LesseeIdentificationCodeFilter))
            query = query.Where(p => p.Lessee != null && p.Lessee.IdentificationCode.Contains(LesseeIdentificationCodeFilter));
        if (!string.IsNullOrEmpty(LeaseAgreementNumberFilter))
            query = query.Where(p => p.LeaseAgreement != null && p.LeaseAgreement.Number.Contains(LeaseAgreementNumberFilter));
        if (!string.IsNullOrEmpty(WaterUsagePermitNumberFilter))
            query = query.Where(p => p.WaterUsagePermit != null && p.WaterUsagePermit.Number.Contains(WaterUsagePermitNumberFilter));
        if (!string.IsNullOrEmpty(RiverFilter))
            query = query.Where(p => p.River.Contains(RiverFilter));
        if (!string.IsNullOrEmpty(BasinFilter))
            query = query.Where(p => p.Basin.Contains(BasinFilter));
        if (!string.IsNullOrEmpty(StatusFilter))
            query = query.Where(p => p.Status.Contains(StatusFilter));
        if (!string.IsNullOrEmpty(IssuesFilter))
            query = query.Where(p => p.Issues.Contains(IssuesFilter));
        if (!string.IsNullOrEmpty(NotesFilter))
            query = query.Where(p => p.Notes.Contains(NotesFilter));
        if (ImposedFinesFilter.HasValue)
            query = query.Where(p => p.ImposedFines == ImposedFinesFilter.Value);
        if (ImposedDamagesFilter.HasValue)
            query = query.Where(p => p.ImposedDamages == ImposedDamagesFilter.Value);
        if (CollectedFinesFilter.HasValue)
            query = query.Where(p => p.CollectedFines == CollectedFinesFilter.Value);
        if (CollectedDamagesFilter.HasValue)
            query = query.Where(p => p.CollectedDamages == CollectedDamagesFilter.Value);

        Ponds = await query.ToListAsync();
    }
}
