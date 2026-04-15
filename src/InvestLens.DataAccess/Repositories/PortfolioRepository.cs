using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.Model.Portfolio;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess.Repositories;

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

    public async Task<Portfolio?> GetPortfolioById(int id)
    {
        var portfolio = await _db.Portfolios.FirstOrDefaultAsync(p => p.Id == id);
        return portfolio;
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

    public async Task<bool> CheckNameUniqueAsync(int ownerId, string name)
    {
        var isExists = await _db.Portfolios.AnyAsync(p => p.OwnerId == ownerId && p.Name == name);
        return !isExists;
    }
}