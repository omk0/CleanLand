using OfficeOpenXml;
using CleanLand.Business.Interfaces;
using CleanLand.Data.Data;
using CleanLand.Data.Models;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace CleanLand.Business.Services
{
    public class ExcelImportService : IExcelImportService
    {
        private readonly ApplicationDbContext _context;

        public ExcelImportService(ApplicationDbContext context)
        {
            _context = context;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        }

        public async Task ImportPondsAsync(string filePath)
        {
            var file = new FileInfo(filePath);
            using var package = new ExcelPackage(file);

            foreach (var worksheet in package.Workbook.Worksheets)
            {
                var territorialCommunity = worksheet.Name;

                for (int row = 4; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (IsRowEmpty(worksheet, row)) continue;

                    var pond = new Pond
                    {
                        TerritorialCommunity = territorialCommunity,
                        Settlement = worksheet.Cells[row, 3].Text.Trim(),
                        Name = worksheet.Cells[row, 4].Text.Trim(),
                        River = worksheet.Cells[row, 5].Text.Trim(),
                        WaterSurfaceArea = ParseDouble(worksheet.Cells[row, 6].Text),
                        Volume = ParseDouble(worksheet.Cells[row, 7].Text),
                        Lessee = new Lessee
                        {
                            Name = worksheet.Cells[row, 10].Text.Trim()
                        },
                        LeaseAgreement = new LeaseAgreement
                        {
                            TermInYears = ParseInt(worksheet.Cells[row, 11].Text)
                        },
                        WaterUsagePermit = new WaterUsagePermit
                        {
                            Number = worksheet.Cells[row, 12].Text
                        }
                    };

                    _context.Ponds.Add(pond);
                }
            }

            await _context.SaveChangesAsync();
        }

        private bool IsRowEmpty(ExcelWorksheet worksheet, int row)
        {
            return string.IsNullOrWhiteSpace(worksheet.Cells[row, 3].Text);
        }

        private double ParseDouble(string value)
        {
            return double.TryParse(value, out var result) ? result : 0;
        }

        private int ParseInt(string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }
    }
}
