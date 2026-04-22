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
        Task<Portfolio> CreatePortfolio(Portfolio portfolio);
        Task Delete(int id);
        Task<List<Portfolio>> GetAllPortfolios();
        Task<List<Portfolio>> GetAllPortfolios(PortfolioType portfolioType);
        Task<Portfolio?> GetPortfolioById(int id);
        Task<int> Save();
        Task Update(Portfolio portfolio, List<int> portfolios);
        Task<int> Merge(List<Transaction> transactions);
        Task<int> Recreate(List<Transaction> transactions);
        Task<List<Transaction>> GetTransactions(int portfolioId);
        Task<List<Transaction>> GetAllTransactions();
        Task<List<Transaction>> GetLastTtransactions(int count);
    }
}