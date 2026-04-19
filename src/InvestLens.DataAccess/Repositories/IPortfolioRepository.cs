using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using Microsoft.EntityFrameworkCore.Storage;

namespace InvestLens.DataAccess.Repositories
{
    public interface IPortfolioRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        Task<bool> CheckNameUniqueAsync(int portfolioId, int ownerId, string name);
        Task<Portfolio> CreatePortfolio(CreateModel model);
        Task Delete(int id);
        Task<List<Portfolio>> GetAllPortfolios(int ownerId);
        Task<List<Portfolio>> GetAllPortfolios(int ownerId, PortfolioType portfolioType);
        Task<Portfolio?> GetPortfolioById(int id);
        Task<int> Save();
        Task Update(UpdateModel model);
        Task Merge(List<Transaction> transactions);
        Task Recreate(List<Transaction> transactions);
    }
}