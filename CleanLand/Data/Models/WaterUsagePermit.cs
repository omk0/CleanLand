namespace CleanLand.Data.Models
{
    public class WaterUsagePermit
    {
        public int Id { get; set; }
        public string Number { get; set; } // Номер
        public DateTime StartDate { get; set; } // Дата початку
        public int TermInYears { get; set; } // Термін дії у роках
    }
}
