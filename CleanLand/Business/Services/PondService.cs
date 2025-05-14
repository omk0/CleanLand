using Microsoft.EntityFrameworkCore;
using CleanLand.Business.Interfaces;
using CleanLand.Data.Data;
using CleanLand.Data.Models;

namespace CleanLand.Business.Services
{
    public class PondService : IPondService
    {
        private readonly ApplicationDbContext _context;

        public PondService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pond>> GetAllPondsAsync()
        {
            return await _context.Ponds
                .Include(p => p.Lessee)
                .Include(p => p.LeaseAgreement)
                .Include(p => p.WaterUsagePermit)
                .Include(f=>f.Issues)
                .ToListAsync();
        }

        public async Task<Pond> GetPondByIdAsync(int id)
        {
            return await _context.Ponds
                .Include(p => p.Lessee)
                .Include(p => p.LeaseAgreement)
                .Include(p => p.WaterUsagePermit)
                .Include(f=>f.Issues)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPondAsync(Pond pond)
        {
            _context.Ponds.Add(pond);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePondAsync(Pond pond)
        {
            _context.Entry(pond).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePondAsync(int id)
        {
            var pond = await _context.Ponds.FindAsync(id);
            if (pond != null)
            {
                _context.Ponds.Remove(pond);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Розраховує індекс критичності для ставка на основі наявних даних
        /// </summary>
        /// <param name="pond">Об'єкт ставка</param>
        /// <returns>Індекс критичності від 0 до 10, де вищі значення означають вищий рівень критичності</returns>
        public double CalculateCriticalityScore(Pond pond)
        {
            if (pond == null)
                throw new ArgumentNullException(nameof(pond));

            // Ваги для кожного фактора (сума = 1)
            var weights = new Dictionary<string, double>
            {
                { "waterQuality", 0.30 }, // Фактор якості води
                { "technicalState", 0.20 }, // Фактор технічного стану
                { "legalStatus", 0.15 }, // Фактор правового статусу
                { "environmentalImpact", 0.20 }, // Фактор впливу на довкілля
                { "economicActivity", 0.15 } // Фактор економічної діяльності
            };

            // 1. Розрахунок фактора якості води
            double waterQualityFactor = CalculateWaterQualityFactor(pond);

            // 2. Розрахунок фактора технічного стану
            double technicalStateFactor = CalculateTechnicalStateFactor(pond);

            // 3. Розрахунок фактора правового статусу
            double legalStatusFactor = CalculateLegalStatusFactor(pond);

            // 4. Розрахунок фактора впливу на довкілля
            double environmentalImpactFactor = CalculateEnvironmentalImpactFactor(pond);

            // 5. Розрахунок фактора економічної діяльності
            double economicActivityFactor = CalculateEconomicActivityFactor(pond);

            // Розрахунок загального індексу критичності
            double criticalityScore =
                weights["waterQuality"] * waterQualityFactor +
                weights["technicalState"] * technicalStateFactor +
                weights["legalStatus"] * legalStatusFactor +
                weights["environmentalImpact"] * environmentalImpactFactor +
                weights["economicActivity"] * economicActivityFactor;

            // Нормалізація до шкали від 0 до 10
            return Math.Min(10, criticalityScore * 10);
        }

        /// <summary>
        /// Розраховує фактор якості води
        /// </summary>
        private double CalculateWaterQualityFactor(Pond pond)
        {
            // Оцінка якості води на основі явних показників
            double qualityRisk = 0;

            // Якщо є прямий індекс якості води, використовуємо його
            if (pond.WaterQualityIndex > 0)
            {
                // Перетворюємо шкалу 0-100 (де 100 найкраща) на шкалу 0-1 (де 1 найгірша)
                qualityRisk = 1 - (pond.WaterQualityIndex / 100);
            }
            else
            {
                // Інакше розраховуємо на основі інших показників

                // Фактор вмісту кисню (менше кисню = вищий ризик)
                double oxygenFactor = 0;
                if (pond.OxygenSaturation > 0)
                {
                    oxygenFactor = 1 - Math.Min(1, pond.OxygenSaturation / 100);
                }
                else
                {
                    oxygenFactor = 0.5; // Середнє значення, якщо дані відсутні
                }

                // Фактор забруднення (більше забруднень = вищий ризик)
                double pollutantFactor =
                    Math.Min(1, pond.PollutantConcentration / 10); // Припускаємо, що 10 мг/л - це критична межа

                // Фактор евтрофікації
                double eutrophicationFactor = pond.IsEutrophicated ? 1 : 0;

                // Фактор цвітіння води
                double algalBloomFactor =
                    Math.Min(1, pond.AlgalBloomFrequency / 10); // Припускаємо, що 10 випадків на рік - це критична межа

                // Середнє значення усіх факторів
                int countFactors = 0;
                double sumFactors = 0;

                // Додаємо тільки ті фактори, для яких є дані
                if (pond.OxygenSaturation > 0)
                {
                    sumFactors += oxygenFactor;
                    countFactors++;
                }

                if (pond.PollutantConcentration > 0)
                {
                    sumFactors += pollutantFactor;
                    countFactors++;
                }

                if (pond.IsEutrophicated)
                {
                    sumFactors += eutrophicationFactor;
                    countFactors++;
                }

                if (pond.AlgalBloomFrequency > 0)
                {
                    sumFactors += algalBloomFactor;
                    countFactors++;
                }

                qualityRisk = countFactors > 0 ? sumFactors / countFactors : 0.5;
            }

            // Обмеження значення між 0 та 1
            return Math.Max(0, Math.Min(1, qualityRisk));
        }

        /// <summary>
        /// Розраховує фактор технічного стану
        /// </summary>
        private double CalculateTechnicalStateFactor(Pond pond)
        {
            double technicalRisk = 0.5; // Середнє початкове значення

            // Оцінка стану на основі наявних даних

            // Аналіз проблем на основі поля Issues (якщо містить інформацію про технічні проблеми)
            if (pond.Issues != null)
            {
                foreach (var issue in pond.Issues)
                {
                    string issues = issue.Description.ToLower();

                    if (issues.Contains("fire") || issues.Contains("chem"))
                        technicalRisk = Math.Max(technicalRisk, 0.9);
                    else if (issues.Contains("pollut") || issues.Contains("dead"))
                        technicalRisk = Math.Max(technicalRisk, 0.7);
                    else if (issues.Contains("garb"))
                        technicalRisk = Math.Max(technicalRisk, 0.6);
                }
            }

            return technicalRisk;
        }

        /// <summary>
        /// Розраховує фактор правового статусу
        /// </summary>
        private double CalculateLegalStatusFactor(Pond pond)
        {
            if (pond.LeaseAgreement == null || pond.Lessee == null || pond.WaterUsagePermit == null)
                return 0;

            double legalRisk = 0.5; // Середнє початкове значення

            // Перевірка наявності договору оренди
            bool hasValidLeaseAgreement = pond.LeaseAgreement != null;

            // Перевірка наявності дозволу на водокористування
            bool hasValidWaterUsagePermit = pond.WaterUsagePermit != null;

            // Оцінка ризику на основі наявності документів
            if (hasValidLeaseAgreement && hasValidWaterUsagePermit)
                legalRisk = 0.1; // Найнижчий ризик - всі документи в порядку
            else if (hasValidLeaseAgreement || hasValidWaterUsagePermit)
                legalRisk = 0.5; // Середній ризик - відсутні деякі документи
            else
                legalRisk = 0.9; // Високий ризик - відсутні всі документи

            // Якщо є накладені штрафи/збитки - підвищуємо ризик
            if (pond.ImposedFines > 0 || pond.ImposedDamages > 0)
            {
                // Розраховуємо відсоток стягнутих штрафів/збитків
                decimal totalImposed = pond.ImposedFines + pond.ImposedDamages;
                decimal totalCollected = pond.CollectedFines + pond.CollectedDamages;

                if (totalImposed > 0)
                {
                    decimal collectionRate = totalCollected / totalImposed;

                    // Якщо багато нестягнутих штрафів - вищий ризик
                    if (collectionRate < 0.5m)
                    {
                        legalRisk = Math.Min(1, legalRisk + 0.3);
                    }
                    else
                    {
                        legalRisk = Math.Min(1, legalRisk + 0.1);
                    }
                }
            }

            return legalRisk;
        }

        /// <summary>
        /// Розраховує фактор впливу на довкілля
        /// </summary>
        private double CalculateEnvironmentalImpactFactor(Pond pond)
        {
            double environmentalRisk = 0.5; // Середнє початкове значення

            // Фактори ризику
            int riskFactors = 0;
            double riskSum = 0;

            // Сільськогосподарська діяльність поблизу
            if (pond.HasAgricultureNearby)
            {
                riskSum += 0.7; // Високий ризик через можливе забруднення добривами
                riskFactors++;
            }

            // Промислова діяльність поблизу
            if (pond.HasIndustryNearby)
            {
                riskSum += 0.9; // Дуже високий ризик через можливе промислове забруднення
                riskFactors++;
            }

            // Ознаки евтрофікації
            if (pond.IsEutrophicated)
            {
                riskSum += 0.8; // Високий ризик через порушення екосистеми
                riskFactors++;
            }

            // Частота цвітіння води
            if (pond.AlgalBloomFrequency > 0)
            {
                double algalBloomRisk = Math.Min(1, pond.AlgalBloomFrequency / 10.0);
                riskSum += algalBloomRisk;
                riskFactors++;
            }

            // Наявність забруднюючих речовин
            if (pond.PollutantConcentration > 0)
            {
                double pollutantRisk = Math.Min(1, pond.PollutantConcentration / 10.0);
                riskSum += pollutantRisk;
                riskFactors++;
            }

            // Розрахунок середнього ризику, якщо є дані
            if (riskFactors > 0)
            {
                environmentalRisk = riskSum / riskFactors;
            }

            return environmentalRisk;
        }

        /// <summary>
        /// Розраховує фактор економічної діяльності
        /// </summary>
        private double CalculateEconomicActivityFactor(Pond pond)
        {
            double economicRisk = 0.5; // Середнє початкове значення

            // Аналіз наявності орендаря
            bool hasLessee = pond.Lessee != null;

            // Аналіз наявності договору оренди
            bool hasLeaseAgreement = pond.LeaseAgreement != null;

            // Оцінка ризику на основі даних про оренду
            if (hasLessee && hasLeaseAgreement)
                economicRisk = 0.3; // Нижчий ризик - є орендар і договір
            else if (hasLessee || hasLeaseAgreement)
                economicRisk = 0.6; // Середній ризик - є або орендар, або договір
            else
                economicRisk = 0.8; // Високий ризик - немає ні орендаря, ні договору

            // Аналіз співвідношення площі водної поверхні до орендованої площі
            if (pond.WaterSurfaceArea > 0 && pond.LeasedArea > 0)
            {
                double waterToLeaseRatio = pond.WaterSurfaceArea / pond.LeasedArea;

                // Якщо площа води становить малу частку від орендованої площі - вищий ризик
                if (waterToLeaseRatio < 0.5)
                {
                    economicRisk = Math.Min(1, economicRisk + 0.2);
                }
            }

            return economicRisk;
        }

        /// <summary>
        /// Повертає текстову інтерпретацію індексу критичності
        /// </summary>
        public string GetCriticalityInterpretation(double score)
        {
            if (score < 3)
                return "Низький рівень ризику - водойма в задовільному стані";
            else if (score < 5)
                return "Помірний рівень ризику - потрібен регулярний моніторинг";
            else if (score < 7)
                return "Високий рівень ризику - необхідні активні заходи для поліпшення стану";
            else return "Найвищий рівень ризику";
        }
    }
}
