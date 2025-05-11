using System.ComponentModel.DataAnnotations;

namespace CleanLand.Data.Models;
public abstract class EnvironmentalAsset
{
    [Key]
    public int Id { get; set; }
    public string AssetType { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? District { get; set; }
    public double XLocation { get; set; }
    public double YLocation { get; set; }
    public double CriticalityScore { get; set; }
    
}