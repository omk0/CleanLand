namespace CleanLand.Data.Models
{
    public class Pond
    {
        public int Id { get; set; }
        public string Name { get; set; } // Назва ставка
        public string District { get; set; } // Район
        public string TerritorialCommunity { get; set; } // ТГ (територіальна громада)
        public string Settlement { get; set; } // Населений пункт
        public string Coordinates { get; set; } // Географічні координати ставка
        public double Length { get; set; } // Довжина
        public double Width { get; set; } // Ширина
        public double Depth { get; set; } // Глибина
        public double WaterLevel { get; set; } // Підпірний рівень
        public double Volume { get; set; } // Об'єм ставка
        public double LeasedArea { get; set; } // Загальна площа орендованої ділянки
        public string CadastralNumber { get; set; } // Кадастровий номер
        public double WaterSurfaceArea { get; set; } // Площа водного плеса
        public bool IsDrainable { get; set; } // Спускний чи не спускний
        public bool HasHydraulicStructure { get; set; } // Наявність гідроспоруди
        public string HydraulicStructureOwner { get; set; } // Балансоутримувач гідроспоруди
        public int LesseeId { get; set; } // Орендар (foreign key)
        public Lessee Lessee { get; set; } // Navigation property
        public int LeaseAgreementId { get; set; } // Договір оренди (foreign key)
        public LeaseAgreement LeaseAgreement { get; set; } // Navigation property
        public int WaterUsagePermitId { get; set; } // Дозвіл на спеціальне водокористування (foreign key)
        public WaterUsagePermit WaterUsagePermit { get; set; } // Navigation property
        public string River { get; set; } // Річка, на якій знаходиться ставок
        public string Basin { get; set; } // Басейн, до якого він відноситься
        public string Status { get; set; } // Стан водного об’єкту
        public string Issues { get; set; } // Порушення та проблеми
        public string Notes { get; set; } // Примітки (Інша інформація)
        public decimal ImposedFines { get; set; } = 0; // Накладені штрафи
        public decimal ImposedDamages { get; set; } = 0; // Накладені збитки
        public decimal CollectedFines { get; set; } = 0; // Стягнуті штрафи
        public decimal CollectedDamages { get; set; } = 0; // Стягнуті збитки
    }
}
