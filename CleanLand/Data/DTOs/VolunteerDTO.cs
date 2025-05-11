namespace CleanLand.Data.DTOs;

public class VolunteerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime AppliedAt { get; set; }
}