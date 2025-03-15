using CleanLand.Business.Interfaces;
using CleanLand.Controllers.Forest;
using CleanLand.Data.Models;

namespace CleanLand.Business.Services
{
    public class ForestService : IForestService
    {
        /// <summary>
        /// Розраховує індекс критичності (CriticalityScore) для лісу на основі множини факторів
        /// </summary>
        /// <param name="forest">Об'єкт лісу з усіма даними</param>
        /// <returns>Розрахований CriticalityScore від 0 до 10</returns>
        public  double CalculateCriticalityScore(Forest forest)
        {
            if (forest == null)
                throw new ArgumentNullException(nameof(forest));

            // Ваги для кожного фактора (сума = 1)
            var weights = new Dictionary<string, double>
        {
            { "deforestation", 0.15 },
            {"technicalFactor", 0.15},
            { "protection", 0.15 },
            { "biodiversity", 0.20 },
            { "carbon", 0.15 },
            { "fireRisk", 0.10 },
            { "endemic", 0.10 }
        };

            // 1. Фактор вирубки (DeforestationFactor)
            double deforestationFactor = CalculateDeforestationFactor(forest);

            // 2. Фактор юридичного захисту (ProtectionFactor)
            double protectionFactor = forest.IsProtectedByLaw ? 0 : 1;

            // 3. Фактор біорізноманіття (BiodiversityFactor)
            double biodiversityFactor = CalculateBiodiversityFactor(forest);

            // 4. Фактор секвестрації вуглецю (CarbonFactor)
            double carbonFactor = CalculateCarbonFactor(forest);

            // 5. Фактор ризику пожеж (FireRiskFactor)
            double fireRiskFactor = CalculateFireRiskFactor(forest);

            // 6. Фактор ендемічності (EndemicFactor)
            double endemicFactor = CalculateEndemicFactor(forest);
            
            
            double technicalFactor = CalculateTechnicalStateFactor(forest);

            // Розрахунок загального показника з вагами
            double criticalityScore =
                weights["deforestation"] * deforestationFactor +
                weights["protection"] * protectionFactor +
                weights["biodiversity"] * biodiversityFactor +
                weights["carbon"] * carbonFactor +
                weights["fireRisk"] * fireRiskFactor +
                weights["endemic"] * endemicFactor +
                weights["technicalFactor"] * technicalFactor;

            // Нормалізуємо значення від 0 до 10 для зручності інтерпретації
            return Math.Min(10, criticalityScore * 5);
        }

        /// <summary>
        /// Розраховує фактор вирубки на основі зміни площі за останні роки
        /// </summary>
        private  double CalculateDeforestationFactor(Forest forest)
        {
            // Якщо немає даних або лише один запис, повертаємо 0
            if (forest.AreaDatas == null || forest.AreaDatas.Count <= 1)
                return 0;

            // Сортуємо дані за датою
            var sortedAreaData = forest.AreaDatas.OrderByDescending(d => d.Date).ToList();

            // Беремо дані за останні 3 роки (або менше, якщо даних менше)
            DateTime threeYearsAgo = DateTime.Now.AddYears(-3);
            var recentData = sortedAreaData.Where(d => d.Date >= threeYearsAgo).ToList();

            if (recentData.Count <= 1)
                return 0;

            // Розрахунок середньорічної зміни площі у відсотках
            List<double> yearlyChanges = new List<double>();

            for (int i = 0; i < recentData.Count - 1; i++)
            {
                // Обчислюємо відсоток зміни площі
                double oldArea = recentData[i + 1].Area;
                double newArea = recentData[i].Area;

                // Уникаємо ділення на нуль
                if (oldArea > 0)
                {
                    double percentChange = ((oldArea - newArea) / oldArea) * 100;

                    // Якщо площа зменшилась (тобто відбулася вирубка), враховуємо цю зміну
                    if (percentChange > 0)
                    {
                        yearlyChanges.Add(percentChange);
                    }
                }
            }

            // Повертаємо середній відсоток вирубки або 0, якщо немає даних про вирубку
            return yearlyChanges.Any() ? yearlyChanges.Average() / 100 : 0;
        }

        /// <summary>
        /// Розраховує фактор біорізноманіття
        /// </summary>
        private  double CalculateBiodiversityFactor(Forest forest)
        {
            int totalSpecies = forest.TreeSpecies?.Count ?? 0;

            if (totalSpecies == 0)
                return 1; // Максимальний фактор ризику, якщо немає даних про види

            int endemicSpecies = forest.TreeSpecies.Count(s => s.IsEndemic);
            int invasiveSpecies = forest.TreeSpecies.Count(s => s.IsInvasive);

            // Чим менше ендемічних видів і більше інвазивних, тим вищий фактор ризику
            double endemicRatio = (double)endemicSpecies / totalSpecies;
            double invasiveRatio = (double)invasiveSpecies / totalSpecies;

            return 1 - endemicRatio + invasiveRatio;
        }

        /// <summary>
        /// Розраховує фактор секвестрації вуглецю
        /// </summary>
        private  double CalculateCarbonFactor(Forest forest)
        {
            // Якщо потенціал нульовий або не визначений, повертаємо максимальний фактор ризику
            if (forest.TonsOfSequesteredPotential <= 0)
                return 1;

            // Обчислюємо невикористаний потенціал секвестрації
            double unusedPotential = 1 - (forest.TonsOfSequesteredToDate / forest.TonsOfSequesteredPotential);

            // Обмежуємо значення між 0 і 1
            return Math.Max(0, Math.Min(1, unusedPotential));
        }

        /// <summary>
        /// Розраховує фактор ризику пожеж
        /// </summary>
        private  double CalculateFireRiskFactor(Forest forest)
        {
            // Отримуємо поточну площу лісу або використовуємо усереднене значення
            double currentArea = 1;

            if (forest.AreaDatas != null && forest.AreaDatas.Any())
            {
                var latestAreaData = forest.AreaDatas.OrderByDescending(d => d.Date).FirstOrDefault();
                if (latestAreaData != null && latestAreaData.Area > 0)
                {
                    currentArea = latestAreaData.Area;
                }
            }

            // Ризик пожежі збільшується з підвищенням температури та зниженням вологості
            double temperatureCoefficient = forest.AverageYearTemperature / 30; // Нормалізація температури
            double humidityFactor = Math.Max(0, 1 - (forest.AverageYearHumidity / 100)); // Вища вологість = нижчий ризик

            // Кількість пожеж на одиницю площі, помножену на коефіцієнти температури та вологості
            double fireIncidentsPerArea = (forest.FireIncidentsAmount / currentArea);
            double finalFireRisk = fireIncidentsPerArea * temperatureCoefficient * (1 + humidityFactor);

            // Обмежуємо значення між 0 і 1
            return Math.Min(1, finalFireRisk);
        }

        /// <summary>
        /// Розраховує фактор ендемічності
        /// </summary>
        private  double CalculateEndemicFactor(Forest forest)
        {
            int totalSpecies = forest.TreeSpecies?.Count ?? 0;

            if (totalSpecies == 0)
                return 1; // Максимальний фактор ризику, якщо немає даних про види

            int endemicSpecies = forest.TreeSpecies.Count(s => s.IsEndemic);

            // Чим менше ендемічних видів, тим вищий фактор ризику
            return 1 - ((double)endemicSpecies / totalSpecies);
        }

        /// <summary>
        /// Отримує текстову інтерпретацію значення CriticalityScore
        /// </summary>
        public  string GetCriticalityInterpretation(double score)
        {
            if (score < 3)
                return "Низький рівень загрози - ліс у хорошому стані"; 
            if (score < 5)
                return "Середній рівень загрози - потрібен моніторинг";
            if (score < 7)
                return "Високий рівень загрози - потрібні активні заходи збереження";

            return "Критичний рівень загрози - потрібні невідкладні заходи порятунку";
        }

        public double CalculateTechnicalStateFactor(Forest forest)
        {
            double technicalRisk = 0.5; // Середнє початкове значення

            // Оцінка стану на основі наявних даних

            // Аналіз проблем на основі поля Issues (якщо містить інформацію про технічні проблеми)
            if (forest.Issues != null)
            {
                foreach (var issue in forest.Issues)
                {
                    string issues = issue.Description.ToLower();

                    if (issues.Contains("пожеж") || issues.Contains("хім"))
                        technicalRisk = Math.Max(technicalRisk, 0.9);
                    else if (issues.Contains("відход") || issues.Contains("мертв"))
                        technicalRisk = Math.Max(technicalRisk, 0.7);
                    else if (issues.Contains("смітт"))
                        technicalRisk = Math.Max(technicalRisk, 0.6);
                }
            }

            return technicalRisk;
        }
    }
}
