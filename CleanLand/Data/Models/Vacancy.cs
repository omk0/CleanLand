using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CleanLand.Data.Models
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Wage { get; set; }
        public int NeededPeople { get; set; }
        public int AppliedPeople { get; set; }
        public EnvironmentalAsset Object { get; set; } = null!;

        public int ObjectId { get; set; }
    }
}