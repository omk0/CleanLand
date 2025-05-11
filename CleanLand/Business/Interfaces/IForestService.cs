using CleanLand.Controllers.Forest;
using CleanLand.Data.Models;

namespace CleanLand.Business.Interfaces
{
    public interface IForestService
    {
        double CalculateCriticalityScore(Forest forest);
    }
}
