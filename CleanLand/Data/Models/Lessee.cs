namespace CleanLand.Data.Models
{
    public class Lessee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string IdentificationCode { get; set; }

        // Initialize the collection to avoid null references
        public ICollection<Pond> Ponds { get; set; } = new List<Pond>();
    }
}
