using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.Model.Portfolio;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess;

public class PortfolioRepository(InvestLensDataContext db) : IPortfolioRepository
{
    private readonly InvestLensDataContext _db = db;

    public async Task<Portfolio?> CreatePortfolio(CreateModel model)
    {
        var portfolio = new Portfolio
        {
            Name = model.Name,
            Description = model.Description,
            PortfolioType = model.PortfolioType,
            OwnerId = model.OwnerId
        };
        _db.Portfolios.Add(portfolio);
        var count = await _db.SaveChangesAsync();

        return count == 0 ? null : portfolio;
    }

    public async Task<List<Portfolio>> GetAllPortfolios(int ownerId)
    {
        return await _db.Portfolios.Where(p => p.OwnerId == ownerId).ToListAsync();
    }

    public async Task<List<Portfolio>> GetAllPortfolios(int ownerId, PortfolioType portfolioType)
    {
        return await _db.Portfolios
            .Where(p => p.OwnerId == ownerId && p.PortfolioType == portfolioType)
            .ToListAsync();
    }
}