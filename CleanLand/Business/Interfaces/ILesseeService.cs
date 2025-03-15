using CleanLand.Data.DTOs;
using CleanLand.Data.Models;

namespace CleanLand.Business.Interfaces
{
    public interface ILesseeService
    {
        Task<IEnumerable<Lessee>> GetAllLesseesAsync();
        Task<Lessee> GetLesseeByIdAsync(int id);
        Task<Lessee> AddLesseeAsync(CreateLesseeDto createLesseeDto);
        Task UpdateLesseeAsync(Lessee lessee);
        Task DeleteLesseeAsync(int id);
    }
}
