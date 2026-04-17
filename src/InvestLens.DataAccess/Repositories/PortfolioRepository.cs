using AutoMapper;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess.Repositories;

public class PortfolioRepository(InvestLensDataContext db, IMapper mapper) : IPortfolioRepository
{
    private readonly InvestLensDataContext _db = db;
    private readonly IMapper _mapper = mapper;

    public async Task CreatePortfolio(Portfolio portfolio)
    {
        _db.Portfolios.Add(portfolio);
        await Task.Delay(0);
    }

    public async Task<Portfolio?> GetPortfolioById(int id)
    {
        var portfolio = await _db.Portfolios
            .FirstOrDefaultAsync(p => p.Id == id);

        return portfolio;
    }

    public async Task<List<Portfolio>> GetAllPortfolios(int ownerId)
    {
        return await _db.Portfolios
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task<List<Portfolio>> GetAllPortfolios(int ownerId, PortfolioType portfolioType)
    {
        return await _db.Portfolios
            .Where(p => p.OwnerId == ownerId && p.PortfolioType == portfolioType)
            .ToListAsync();
    }

    public async Task<bool> CheckNameUniqueAsync(int portfolioId, int ownerId, string name)
    {
        var isExists = await _db.Portfolios
            .AnyAsync(p => p.OwnerId == ownerId 
                           && (portfolioId == 0 || p.Id != portfolioId) 
                           && p.Name == name);

        return !isExists;
    }

    public async Task Update(InvestLens.Model.Crud.Portfolio.UpdateModel model)
    {
        var portfolio = await GetPortfolioById(model.Id);
        if (portfolio is null)
        {
            await Task.FromException(new KeyNotFoundException($"Портфель с {model.Id} не найден."));
            return;
        }

        _mapper.Map(model, portfolio);
    }

    public async Task Delete(int id)
    {
        var portfolio = await _db.Portfolios
            .FirstOrDefaultAsync(p => p.Id == id);
        if (portfolio is null) return;

        _db.Portfolios.Remove(portfolio);
    }

    public async Task<int> Save()
    {
        return await _db.SaveChangesAsync();
    }
}