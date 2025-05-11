using System.ComponentModel.DataAnnotations;

namespace CleanLand.Data.Models;

public class Forest : EnvironmentalAsset
{
    public string AssetType { get; set; } = "Forest";
    public string? NGO { get; set; }
    public bool IsProtectedByLaw { get; set; }
    public long? TreesAmount { get; set; }
    public List<TreeSpecie> TreeSpecies { get; set; } = new();
    public double TonsOfSequesteredToDate { get; set; }
    public double TonsOfSequesteredPotential { get; set; }
    public List<AreaData> AreaDatas { get; set; } = new();
    public double AverageYearTemperature { get; set; }
    public double AverageYearHumidity { get; set; }
    public int FireIncidentsAmount { get; set; }
    public List<Issue>? Issues { get; set; }
}

public class TreeSpecie
{
    [Key]
    public int Id { get; set; }
    public string? ScientificName { get; set; }
    public string? CommonName { get; set; }
    public string? Description { get; set; }
    public string? TaxonomicClassification { get; set; }
    public bool IsEndemic { get; set; }
    public bool IsInvasive { get; set; }
}

public class AreaData
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Area { get; set; }
}
