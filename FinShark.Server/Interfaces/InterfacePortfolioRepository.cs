using FinShark.Server.Models;

namespace FinShark.Server.Interfaces
{
    public interface InterfacePortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol);
    }
}
