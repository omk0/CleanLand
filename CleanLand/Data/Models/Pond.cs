namespace CleanLand.Data.Models
{
    public class Pond
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? District { get; set; }
        public string? TerritorialCommunity { get; set; }
        public string? Settlement { get; set; }
        public string? Coordinates { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public double WaterLevel { get; set; }
        public double Volume { get; set; }
        public double LeasedArea { get; set; }
        public string? CadastralNumber { get; set; }
        public double WaterSurfaceArea { get; set; }
        public bool IsDrainable { get; set; }
        public bool HasHydraulicStructure { get; set; }
        public string? HydraulicStructureOwner { get; set; }
        public int LesseeId { get; set; }
        public Lessee? Lessee { get; set; }
        public int LeaseAgreementId { get; set; }
        public LeaseAgreement? LeaseAgreement { get; set; }
        public int WaterUsagePermitId { get; set; }
        public WaterUsagePermit? WaterUsagePermit { get; set; }
        public string? River { get; set; }
        public string? Basin { get; set; }
        public string? Status { get; set; }
        public List<Issue?>? Issues { get; set; }
        public string? Notes { get; set; }
        public decimal ImposedFines { get; set; } = 0;
        public decimal ImposedDamages { get; set; } = 0;
        public decimal CollectedFines { get; set; } = 0;
        public decimal CollectedDamages { get; set; } = 0;

        // Додаткові екологічні показники
        public double WaterQualityIndex { get; set; } // Індекс якості води (від 0 до 100)
        public double OxygenSaturation { get; set; } // Насиченість киснем (%)
        public double PollutantConcentration { get; set; } // Концентрація забруднюючих речовин (мг/л)
        public bool IsEutrophicated { get; set; } // Чи є ознаки евтрофікації
        public int AlgalBloomFrequency { get; set; } // Частота цвітіння води (кількість за рік)

        // Чинники впливу на водний об'єкт
        public bool HasAgricultureNearby { get; set; } // Наявність сільськогосподарських угідь поряд
        public bool HasIndustryNearby { get; set; } // Наявність промислових об'єктів поряд

        // Індекс критичності
        public double CriticalityScore { get; set; } // Загальний індекс критичності
    }
}
