namespace CleanLand.Data.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool IsResolved { get; set; }

    }
}
