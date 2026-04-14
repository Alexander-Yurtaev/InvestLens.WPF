using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.Model.Portfolio;

namespace InvestLens.DataAccess.Repositories;

public interface IPortfolioRepository
{
    Task<Portfolio?> CreatePortfolio(CreateModel model);
    Task<Portfolio?> GetPortfolioById(int id);

    Task<List<Portfolio>> GetAllPortfolios(int ownerId);
    Task<List<Portfolio>> GetAllPortfolios(int ownerId, PortfolioType portfolioType);
    Task<bool> CheckNameUniqueAsync(int ownerId, string name);
}