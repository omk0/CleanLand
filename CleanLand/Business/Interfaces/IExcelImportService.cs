namespace CleanLand.Business.Interfaces
{
    public interface IExcelImportService
    {
        Task ImportPondsAsync(string filePath);
    }
}
