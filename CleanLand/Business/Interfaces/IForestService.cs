using CleanLand.Controllers.Forest;

namespace CleanLand.Business.Interfaces
{
    public interface IForestService
    {
        double CalculateCriticalityScore(Forest forest);
    }
}
