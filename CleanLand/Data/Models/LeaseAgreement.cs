namespace CleanLand.Data.Models
{
    public class LeaseAgreement
    {
        public int Id { get; set; }
        public string Number { get; set; } // Номер
        public DateTime Date { get; set; } // Дата
        public int TermInYears { get; set; } // Термін дії у роках
        public string EconomicActivities { get; set; } // Види господарської діяльності
        public ICollection<Pond> Ponds { get; set; } // Navigation property
    }
}
