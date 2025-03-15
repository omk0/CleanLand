using CleanLand.Data.Models;

namespace CleanLand.Business.Interfaces
{
    public interface IPondService
    {
        Task<IEnumerable<Pond>> GetAllPondsAsync();
        Task<Pond> GetPondByIdAsync(int id);
        Task AddPondAsync(Pond pond);
        Task UpdatePondAsync(Pond pond);
        Task DeletePondAsync(int id);
    }
}
