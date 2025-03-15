using System.ComponentModel.DataAnnotations;

namespace CleanLand.Data.DTOs
{
    public class CreateLesseeDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string IdentificationCode { get; set; }
    }
}
