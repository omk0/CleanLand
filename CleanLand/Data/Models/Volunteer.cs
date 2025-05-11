using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanLand.Data.Models
{
    public class Volunteer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; } = null!;
        
        [Required]
        public DateTime AppliedAt { get; set; }
        
        [ForeignKey(nameof(Vacancy))]
        public int VacancyId { get; set; }
        
        public virtual Vacancy Vacancy { get; set; } = null!;
    }
}