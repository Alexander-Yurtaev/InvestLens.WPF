using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;

namespace InvestLens.DataAccess.Repositories;

public interface IPortfolioRepository
{
    Task<Portfolio?> CreatePortfolio(Portfolio portfolio);
    Task<Portfolio?> GetPortfolioById(int id);

    Task<List<Portfolio>> GetAllPortfolios(int ownerId);
    Task<List<Portfolio>> GetAllPortfolios(int ownerId, PortfolioType portfolioType);
    Task<bool?> Delete(int id);
    Task<bool> CheckNameUniqueAsync(int ownerId, string name);
}