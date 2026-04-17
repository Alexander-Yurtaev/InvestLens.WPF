using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;

namespace InvestLens.DataAccess.Repositories
{
    public interface IPortfolioRepository
    {
        Task<bool> CheckNameUniqueAsync(int portfolioId, int ownerId, string name);
        Task CreatePortfolio(Portfolio portfolio);
        Task Delete(int id);
        Task<List<Portfolio>> GetAllPortfolios(int ownerId);
        Task<List<Portfolio>> GetAllPortfolios(int ownerId, PortfolioType portfolioType);
        Task<Portfolio?> GetPortfolioById(int id);
        Task<int> Save();
        Task Update(UpdateModel model);
    }
}