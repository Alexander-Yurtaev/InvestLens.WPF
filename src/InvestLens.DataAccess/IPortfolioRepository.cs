using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.Model.Portfolio;

namespace InvestLens.DataAccess;

public interface IPortfolioRepository
{
    Task<Portfolio?> CreatePortfolio(CreateModel model);
    Task<List<Portfolio>> GetAllPortfolios(int ownerId);
    Task<List<Portfolio>> GetAllPortfolios(int ownerId, PortfolioType portfolioType);
}